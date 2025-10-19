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
'            If info.Version <> &H1F4 Then
'                ' redefine globals
'                ' dwVersion always holds "5.00" pseudo-string
'                ProjectInfo = new None(ServiceReturns.BadData, "bad version value")
'                ProjectInfoOffset = New None(ServiceReturns.BadData, "bad version value")
'                ObjectInfo = New None(ServiceReturns.BadOperation, "project info unloaded")
'                ObjectTable = New None(ServiceReturns.BadOperation, "project info unloaded")
'            End If
            ' -- 1st level of VB Project --
            
            ProjectInfo = New Some(info)
            ProjectInfoOffset = New Some(offset)
            
            ' -- 2nd level --
            _reader.BaseStream.Position = info.ObjectTablePointer - imageBase
            Dim objTable = Fill(Of Vb5ObjectTable)(_reader)
            Dim objPointers = New List(Of Vb5ObjectDescriptor)()
            Dim count = objTable.TotalObjectsCount
            
            ' $descriptors = 31
            If count > 255 Then
                count = 255
            End If
            
            Try
                _reader.BaseStream.Position = FindRVA(objTable.ObjectsArrayPointer)
                ' iterate data
                ' MethodNames array extract
                For i = 1 To count
                    objPointers.Add(Fill(Of Vb5ObjectDescriptor)(_reader))
                Next
                
                For Each i In objPointers
                    ' iterate object descriptors
                    ' Find Object string
                    _reader.BaseStream.Position = i.ObjectStringPointer - imageBase
                    Dim str = FillCString(_reader)
                    
                    str.Concat("\0")
                    'Dim mc = i.MethodsCount
                    'If mc > 500 Then mc = 256
                    
                    'For j = 1 To mc
                        
                    'Next
                Next
            Catch
                ' ignore next details
                
                Return
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
            
            _reader.BaseStream.Position = FindRVA(info.ExternalTablePointer)' RVA points to non-allocated space
            Dim ctls = New List(Of ExternalApiDescriptor)
            Try
                For i As UInteger = 1 To info.ExternalTableCount 
                    Dim d = New ExternalApiDescriptor()
                    
                    d.EntryType = _reader.ReadUInt32()
                    d.EntryPointer = _reader.ReadUInt32()
                    
                    ctls.Add(d)
                Next
            Catch e As Exception
                ExternalDescriptors = New None(ServiceReturns.BadOperation, e.Message)
                ExternalProcedures = New None(ServiceReturns.BadOperation, "unreachable memory space")
                ExternalGUIDs = New None(ServiceReturns.BadOperation, "unreachable memory space")
                Return
            End Try
            Dim imp = New List(Of Vb5ImportByName)()
            Dim guid = New List(Of Vb5ImportByGuid)()
            
            Dim count = info.ExternalTableCount
            If count > 256 Then
                count = 256
            End If
            
            For j As UInteger = 1 To count
                _reader.BaseStream.Position = ctls(j).EntryPointer
                If ctls(j).EntryType = 7 Then
                    Dim lpModuleName = _reader.ReadUInt32()
                    Dim lpProcedureName = _reader.ReadUInt32()
                    Dim lpBase = _reader.ReadUInt32()
                    _reader.ReadUInt64() ' dwNULL + dwNULL skip
                    Try 
                        _reader.BaseStream.Position = lpModuleName
                        Dim name = FillCString(_reader)
                        _reader.BaseStream.Position = lpProcedureName
                        Dim procName = FillCString(_reader)
                        imp.Add(New Vb5ImportByName(lpModuleName, name, lpProcedureName, procName, lpBase))
                    Catch ' some pointers may locate undefined or "non-allocated" memory scope
                        imp.Add(New Vb5ImportByName(lpModuleName, "", lpProcedureName, "", lpBase))
                    End Try
                Else If ctls(j).EntryType = 6 Then
                    Dim lpGuid = _reader.ReadUInt32()
                    Dim unknown = _reader.ReadUInt32()
                    Try
                        _reader.BaseStream.Position = FindRVA(lpGuid)
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
                        '' !!!!
                        Dim lpszLibraryName = _reader.ReadUInt32()
                        Dim lpszImportFunction = _reader.ReadUInt32()
                        
                        _reader.BaseStream.Position = FindRVA(lpszImportFunction)
                        Dim functionName = FillCString(_reader)
                        
                        _reader.BaseStream.Position = FindRVA(lpszLibraryName)
                        Dim libraryName = FillCString(_reader)
                        
                        If Not String.IsNullOrEmpty(libraryName) AndAlso Not String.IsNullOrEmpty(functionName) Then
                            importsList.Add($"{libraryName}::{functionName}")
                        End If
                    End If
                    
                ElseIf item.EntryType = 6 Then
                    Dim importOffset = FindRVA(item.EntryPointer)
                    _reader.BaseStream.Position = importOffset
                    
                    If importOffset >= 0 Then
                        Dim lpUuid = _reader.ReadUInt32() ' importOffset
                        Dim uuidOffset = FindRVA(lpUuid)
                        
                        If uuidOffset >= 0 Then
                            _reader.BaseStream.Position = uuidOffset
                            Dim guid = _reader.ReadUInt64()
                            importsList.Add($"_::{guid}")
                        End If
                    End If
                End If
            Next
            
            Return importsList
        End Function
    End Class
End Namespace