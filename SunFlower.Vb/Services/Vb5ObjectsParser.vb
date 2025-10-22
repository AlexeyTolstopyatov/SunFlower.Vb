Imports System.IO
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Managers

Namespace Services
    Public Class Vb5ObjectsParser
        Inherits MemoryManager
        Private _objectsTable As Vb5ObjectTable
        
        Public Sub New(objectsTable As Vb5ObjectTable, 
                       reader As BinaryReader, 
                       sections As List(Of PeSection), 
                       imageBase As UInteger)
            MyBase.New(imageBase, sections)
            _objectsTable = objectsTable
            _reader = reader
            Me.sections = sections
            Me.imageBase = imageBase
        End Sub

        Public Function ParseObjects() As List(Of Vb5Object)
            ' Remember: pointers already set. --->|ObjectsTable|
            ' ObjectTable -> [ObjectDescriptor, ObjectDescriptor]
            '                       |
            '                +----------------------+
            '                | ...                  |
            '                | dwTotalObjects       |
            '                | lpObjectDescriptors  |
            Dim count = Math.Min(_objectsTable.TotalObjectsCount, 256)
            Dim offset = VaToFileOffset(_objectsTable.ObjectsArrayPointer)
            Dim names = New List(Of String)
            Dim objects = New List(Of Vb5Object)()
            Dim objPointers = New List(Of Vb5ObjectDescriptor)()
            _reader.BaseStream.Position = offset ' <-- set at the start of VM objects
            
            For i = 1 To count Step 1
                objPointers.Add(Fill(Of Vb5ObjectDescriptor)(_reader))
            Next
            
            For Each o In objPointers
                If o.ObjectInfoPointer < &H10000 Then
                    Continue For
                End If
                
                Dim name = ReadStringAtVa(o.ObjectStringPointer)
                If name = "" Then 
                    name = "[?]"
                End If
                
                names.Add(name)
            Next
            
            Return New List(Of Vb5Object)
        End Function
    End Class
    
    Public Class Vb5Object
        Sub New(name As String, procedures As List(Of String))
            Me.Name = name
            Me.Procedures = procedures
        End Sub

        Public Property Name As String = String.Empty
        Public Property Procedures As List(Of String) = New List(Of String)
    End Class
End Namespace