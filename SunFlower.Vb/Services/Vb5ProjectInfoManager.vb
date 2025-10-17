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
            FillExternalComponents()
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
            Dim offset = FindOffset(FindRVA(_header.ProjectDataPointer))
            _reader.BaseStream.Position = FindOffset(FindRVA(_header.ProjectDataPointer))
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
            _reader.BaseStream.Position = FindOffset(FindRVA(info.ObjectTablePointer))
            Dim objTable = Fill(Of Vb5ObjectTable)(_reader)
            Dim objPointers = New List(Of Vb5ObjectDescriptor)()
            Dim counter = 1
            Try
                _reader.BaseStream.Position = FindOffset(FindRVA(objTable.ObjectsArrayPointer))
                ' iterate data
            Catch
                ' ignore next details
            End Try
'           
            ObjectTable = New Some(objTable)
        End Sub
        ''' 
        Private Sub FillExternalComponents()
            ' #2.1 | ExternalComponents
            ' Defines in ProjectInfo table with 2 fields:
            '   dwExternalCtlsCount
            '   lpExternalCtlsTable
            ' Contains importing records by name & by GUID
            ' Most PE32 executables has imports by name in the ExternalAPIDescriptors table
            '
            ' External API descriptors table is an array of RVA
            ' pointers to structs embedded in executable image
            Dim prj As Some = ProjectInfo
            Dim info As Vb5ProjectInfo = prj.Data
            
            If info.ExternalTableCount = 0 Then
                ExternalGUIDs = New None(ServiceReturns.BadData, "no GUIDs")
                ExternalProcedures = New None(ServiceReturns.BadData, "no imports")
                ExternalDescriptors = New None(ServiceReturns.BadData, "no external components")
                Return
            End If
            
            _reader.BaseStream.Position = FindRVA(info.ExternalTablePointer) ' RVA points to non-allocated space
            Dim ctls = New List(Of ExternalApiDescriptor)
            
            For i As UInteger = 1 To info.ExternalTableCount 
                Dim d = New ExternalApiDescriptor()
                
                d.EntryType = _reader.ReadUInt32()
                d.EntryPointer = _reader.ReadUInt32()
                
                ctls.Add(d)
            Next
            Dim imp = New List(Of Vb5ImportByName)()
            Dim guid = New List(Of Vb5ImportByGuid)()
            
            For j As UInteger = 1 To info.ExternalTableCount
                _reader.BaseStream.Position = FindRVA(ctls(j).EntryPointer)
                If ctls(j).EntryType = 7 Then
                    Dim lpModuleName = _reader.ReadUInt32()
                    Dim lpProcedureName = _reader.ReadUInt32()
                    Dim lpBase = _reader.ReadUInt32()
                    _reader.ReadUInt64() ' dwNULL + dwNULL skip
                    Try 
                        _reader.BaseStream.Position = FindOffset(FindRVA(lpModuleName))
                        Dim name = FillCString(_reader)
                        _reader.BaseStream.Position = FindOffset(FindRVA(lpProcedureName))
                        Dim procName = FillCString(_reader)
                        imp.Add(New Vb5ImportByName(lpModuleName, name, lpProcedureName, procName, lpBase))
                    Catch ' some pointers may locate undefined or "non-allocated" memory scope
                        imp.Add(New Vb5ImportByName(lpModuleName, "", lpProcedureName, "", lpBase))
                    End Try
                Else If ctls(j).EntryType = 6 Then
                    Dim lpGuid = _reader.ReadUInt32()
                    Dim unknown = _reader.ReadUInt32()
                    Try
                        _reader.BaseStream.Position = FindOffset(FindRVA(lpGuid))
                        Dim g = _reader.ReadUInt64()
                        guid.Add(New Vb5ImportByGuid(lpGuid, g, unknown))
                    Catch
                        guid.Add(New Vb5ImportByGuid(lpGuid, 0, unknown))
                    End Try
                Else
                    Continue For
                End If
            Next
            ExternalDescriptors = New Some(ctls)
            ExternalProcedures = New Some(imp)
            ExternalGUIDs = New Some(guid)
        End Sub
    End Class
End Namespace