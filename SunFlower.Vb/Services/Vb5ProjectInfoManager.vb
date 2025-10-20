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
        ''' Some(List of NAMEImport)
        '''     IMPORT_DESCRIPTOR type = 7 --> Import by Name (IAT/ITL binding)
        Public Property ExternalProcedures As [Option]
        ''' Some(List of GUIDImport) type = 6 --> GUID Import record
        Public Property ExternalGUIDs As [Option]
        ''' Some(List of ObjectInfo)
        ''' None(default)
        Public Property ObjectInfo As [Option]
        ''' Some(Dictionary{int}{List(Of string)})
        ''' 
        Public Property MethodNames As [Option]
        
        Public Sub New(imageBase As Long, header As Vb5Header, reader As BinaryReader, sections As List(Of PeSection), vbOffset As Long)
            MyBase.New(imageBase := imageBase,
                       sections := sections)
            SetVbHeaderOffset(vbOffset)
            _header = header
            _reader = reader
            
            FillProjectInfo() ' <-- update models
            ' FillExternalComponents()
            Dim e = ExtractImports()
            
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
            Dim offset = _header.ProjectDataPointer - imageBase
            _reader.BaseStream.Position = offset
            Dim info = Fill(Of Vb5ProjectInfo)(_reader)

            ' info.Version <> 500 = f. f => a || f !=> a
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
            _reader.BaseStream.Position = info.ObjectTablePointer - imageBase
            Dim objTable = Fill(Of Vb5ObjectTable)(_reader)
            Dim objPointers = New List(Of Vb5ObjectDescriptor)()
            Dim count = Math.Min(objTable.TotalObjectsCount, 256)
            
            _reader.BaseStream.Position = FindRVA(objTable.ObjectsArrayPointer)
            ' iterate data
            ' MethodNames array extract
            For i = 1 To count Step 1
                objPointers.Add(Fill(Of Vb5ObjectDescriptor)(_reader))
            Next
                
            ' target String is empty OR pointer to string is invalid. 
            ' babababebebe
            For Each i In objPointers
                If i.MethodsCount = 0 Then Continue For
                If i.MethodsPointer = 0 Then Continue For
                
                Try 
                    _reader.BaseStream.Position = FindRVA(i.ObjectStringPointer)
                    Dim str = FillCString(_reader)
                    str.Concat("\0")
                Catch
                    Continue For
                End Try 
                Dim mc = Math.Min(i.MethodsCount, 10) ' replace to 256
                
'                _reader.BaseStream.Position = FindRVA(i.MethodsPointer) ' check position.
'                For j = 1 To mc Step 1
'                    Try
'                        Dim methodName = FillCString(_reader)
'                        methodName.Concat(0)
'                    Catch
'                        Continue For
'                    End Try
'                Next
            Next
        
            ObjectTable = New Some(objTable)
        End Sub
        
        Private Function ExtractImports() As List(Of String)
            Dim prj As Some = ProjectInfo
            Dim info As Vb5ProjectInfo = prj.Data

            Dim importsList As New List(Of String)()
            Dim externalDescriptors = New List(Of ExternalApiDescriptor)()
            If info.ExternalTableCount = 0 Then Return importsList
            If info.ExternalTablePointer = 0 Then Return importsList
            
            Dim externTableOffset = FindRVA(info.ExternalTablePointer)
            If externTableOffset < 0 Then Return importsList
            
            Dim maxImports = Math.Min(info.ExternalTableCount, 256)
            
            _reader.BaseStream.Position = externTableOffset
            For i = 1 To maxImports
                externalDescriptors.Add(Fill(Of ExternalApiDescriptor)(_reader))
            Next
            
            For Each item In externalDescriptors
                If item.EntryType = 7 Then
                    Dim importOffset = FindRVA(item.EntryPointer)
                    _reader.BaseStream.Position = importOffset
                    If importOffset >= 0 Then
                        Dim importDescriptor = Fill(Of Vb5ImportByName)(_reader)
                        _reader.BaseStream.Position = FindRVA(importDescriptor.LibraryNamePointer)
                        Dim functionName = FillCString(_reader)
                        
                        _reader.BaseStream.Position = FindRVA(importDescriptor.FunctionNamePointer)
                        Dim libraryName = FillCString(_reader)
                        
                        importsList.Add($"{libraryName}::{functionName}")
                    End If
                ElseIf item.EntryType = 6 Then
                    Dim importOffset = FindRVA(item.EntryPointer)
                    _reader.BaseStream.Position = importOffset
                    
                    If importOffset >= 0 Then
                        Dim lpUuid = _reader.ReadUInt32() ' importOffset
                        Dim uuidOffset = FindRVA(lpUuid)
                        
                        If uuidOffset > 0 Then
                            _reader.BaseStream.Position = uuidOffset
                            Dim guidBytes() = _reader.ReadBytes(16)
                            Dim guid = New Guid(guidBytes)
                            
                            importsList.Add($"{guid}") ' <-- implicit cast
                        End If
                    End If
                End If
            Next
            
            Return importsList
        End Function
    End Class
End Namespace