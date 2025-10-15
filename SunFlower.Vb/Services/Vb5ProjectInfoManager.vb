Imports System.IO
Imports SunFlower.Vb.Handlers
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Managers

Namespace Services
    Public Class Vb5ProjectInfoManager
        Inherits PointersManager

        Private ReadOnly _header As Vb5Header
        Private ReadOnly _reader As BinaryReader

        ''' Some(size_t)
        ''' None(default)
        Public Property ProjectInfoOffset As [Option]
        ''' Some(Vb5ProjectInfo)
        ''' None(default)
        Public Property ProjectInfo As [Option]
        ''' Some(ObjectTable)
        ''' None(default)
        Public Property ObjectTable As [Option]
        ''' Some(List of DWORDs)
        ''' None(default)
        Public Property ExternalDescriptors As [Option]
        ''' Some(List of MustInherit(IMPORT_DESCRIPTOR))
        '''     IMPORT_DESCRIPTOR type = 6 --> GUID Import record
        '''                       type = 7 --> Import by Name (IAT/ITL binding)
        Public Property ExternalProcedures As [Option]
        ''' Some(List of ObjectInfo)
        ''' None(default)
        Public Property ObjectInfo As [Option]
        ''' Some(Dictionary{int}{List(Of string)})
        ''' 
        Public Property MethodNames As [Option]
        
        Public Sub New(imageBase As Long, header As Vb5Header, reader As BinaryReader, sections As List(Of PeSection))
            MyBase.New(imageBase := imageBase,
                       sections := sections)
            _header = header
            _reader = reader
            
            FillProjectInfo() ' <-- update models
        End Sub
        ''' ProjectInfo holds information about all external
        ''' components bound to module what not/requires (NCoded/PCoded)
        ''' virtual machine. External Descriptor's API follows like an array after it
        ''' by special ULONG position
        Private Sub FillProjectInfo()
            ' #2 | ProjectInfo Nested Tree
            ' ProjectInfo is a next following struct by the EXE_PROJ_INFO{...} struct
            ' Structure has nested custom-typed arrays and must reinterpret like them 
            '   dwVersion must be 0x1F4 -> means "500" or "5.00"
            '   lpCodeStarts sometimes equals 0xE9E9E9E9
            '   lpCodeEnds   sometimes equals 0x9E9E9E9E
            '   lpNativeCode 0 -> P-CODE |  !0 -> N-CODE
            Dim offset = WrongOffset(_header.ProjectDataPointer)
            _reader.BaseStream.Position = WrongOffset(_header.ProjectDataPointer)
            Dim info = Fill(Of Vb5ProjectInfo)(_reader)

            If info.Version <> &H1F4 Then
                ' redefine globals
                ' dwVersion always holds "5.00" pseudo-string
                ProjectInfo = new None(ServiceReturns.BadData, "bad version value")
                ProjectInfoOffset = New None(ServiceReturns.BadData, "bad version value")
                ObjectInfo = New None(ServiceReturns.BadOperation, "project info unloaded")
                ObjectTable = New None(ServiceReturns.BadOperation, "project info unloaded")
            End If
            ' -- 1st level of VB Project --
            ProjectInfo = New Some(info)
            ProjectInfoOffset = New Some(offset)
            
            ' -- 2nd level --
            _reader.BaseStream.Position = WrongOffset(info.ObjectTablePointer)
            Dim objTable = Fill(Of Vb5ObjectTable)(_reader)
            Dim objPointers = New List(Of Vb5ObjectDescriptor)()
            Dim counter = 1
            
            _reader.BaseStream.Position = WrongOffset(objTable.ObjectsArrayPointer)
            While counter < objTable.TotalObjectsCount
                Dim descriptor = Fill(Of Vb5ObjectDescriptor)(_reader)
                objPointers.Add(descriptor)
                counter += 1
            End While
            
            Dim methods = New Dictionary(Of String, List(Of String))

            For Each i In objPointers
                _reader.BaseStream.Position = WrongOffset(i.ObjectStringPointer)
                Dim modstr = FillCString(_reader)
                Dim methodsList = New List(Of String)
                
                ' _reader.BaseStream.Position = LongPtrToOffset(i.MethodsPointer)
            Next
            
            
            ObjectTable = New Some(objTable)
        End Sub
        ''' 
        Public Sub FillExternalComponents()
            ' #2.1 | ExternalComponents
            ' Defines in ProjectInfo table with 2 fields:
            '   dwExternalCtlsCount
            '   lpExternalCtlsTable
            ' Contains importing records by name & by GUID
            ' Most PE32 executables has imports by name in the ExternalAPIDescriptors table
            '
            ' External API descriptors table is an array of RVA
            ' pointers to structs embedded in executable image
            
        End Sub
    End Class
End Namespace