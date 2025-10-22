Imports System.IO
Imports SunFlower.Vb.Handlers
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Managers

Namespace Services
    Public Class Vb5ProjectParser
        Inherits MemoryManager

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
        ''' Some(List of ObjectInfo)
        ''' None(default)
        Public Property ObjectInfo As [Option]
        ''' Some(Dictionary{int}{List(Of string)})
        ''' 
        Public Property MethodNames As [Option]
        
        Public Sub New(imageBase As Long, header As Vb5Header, reader As BinaryReader, sections As List(Of PeSection), vbOffset As Long)
            MyBase.New(imageBase := imageBase,
                       sections := sections)
            size = reader.BaseStream.Length ' <-- file size
            _header = header
            _reader = reader
            
            FillProjectInfo() ' <-- update models
        End Sub
        Private Sub FillProjectInfo()
            Dim offset = VaToFileOffset(_header.ProjectDataPointer)
            _reader.BaseStream.Position = offset
            Dim info = Fill(Of Vb5ProjectInfo)(_reader)

            If info.Version <> &H1F4 Then
                ProjectInfo = New None(ServiceReturns.BadData, "bad version value")
                ProjectInfoOffset = New None(ServiceReturns.BadData, "bad version value")
                ObjectInfo = New None(ServiceReturns.BadOperation, "project info unloaded")
                ObjectTable = New None(ServiceReturns.BadOperation, "project info unloaded")
                Return
            End If

            ProjectInfo = New Some(info)
            ProjectInfoOffset = New Some(offset)

            _reader.BaseStream.Position = VaToFileOffset(info.ObjectTablePointer)
            Dim objTable = Fill(Of Vb5ObjectTable)(_reader)
            
            Dim objPointers = New List(Of Vb5ObjectDescriptor)()
            Dim count = Math.Min(objTable.TotalObjectsCount, 256)
            
            _reader.BaseStream.Position = VaToFileOffset(objTable.ObjectsArrayPointer)
            For i = 1 To count
                objPointers.Add(Fill(Of Vb5ObjectDescriptor)(_reader))
            Next

            Dim methodNamesDict = New Dictionary(Of Integer, List(Of String))()
            
            MethodNames = New Some(methodNamesDict)
            ObjectTable = New Some(objTable)
        End Sub
    End Class
End Namespace