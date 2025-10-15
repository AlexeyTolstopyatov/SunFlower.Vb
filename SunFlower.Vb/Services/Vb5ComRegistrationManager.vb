Imports System.IO
Imports SunFlower.Vb.Handlers
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Managers

Namespace Services
    Public Class Vb5ComRegistrationManager
        Inherits PointersManager

        Private ReadOnly _header As Vb5Header
        Private ReadOnly _reader As BinaryReader
        
        ''' Some(size_t)
        ''' None(reason, reasonStr)
        Public Property ComDataOffset As [Option]
        ''' Some(Vb5ComData)
        ''' None(reason, reasonStr)
        Public Property ComData As [Option]
        ''' Some(List(Of Vb5ComInfo))
        ''' None(reason, reasonStr)
        Public Property ComInfo As [Option]
        ''' Some(List(Of Long))
        ''' None(reason, reasonStr)
        Public Property ComInfoOffsets As [Option]
        
        Public Sub New(imageBase As Long, header As Vb5Header, reader As BinaryReader, sections As List(Of PeSection))
            MyBase.New(imageBase := imageBase, 
                       sections := sections)
            _header = header
            _reader = reader
            
            FillComponentObjectData() ' <-- update COM models
        End Sub
        
        ''' COM = Component Object Model
        ''' COM descriptor = abstraction of physical location
        ''' of COM .ocx/.dll process in flat Windows memory
        ''' COM registration data means data what extremely needs to describe all
        ''' external OCX/DLL components and self for a VM process init. /I suppose/
        Private Sub FillComponentObjectData()
            ' #3 | COM Descriptor Nested Tree
            ' If all conditions resolves positive -> reinterpret
            ' wrong pointers to absolute file offsets
            ' [tagREG_DATA] ---+
            ' ...              | dwRegInfoOffset. always not 0. /I suppose/
            ' [tagREG_INFO...<-+
            '   ...
            '   ...
            ' ]
            Dim data = New Vb5ComData() 
            Dim offset As Long
            Try
                offset = WrongOffset(_header.ComRegisterDataPointer)
                _reader.BaseStream.Position = WrongOffset(_header.ComRegisterDataPointer)
                data = Fill(Of Vb5ComData)(_reader)
            Catch e As Exception
                ' Get results of ALL structs
                ComData = New None(ServiceReturns.BadOperation, e.Message) 
                ComInfoOffsets = New None(ServiceReturns.BadOperation, e.Message)
                ComInfo = New None(ServiceReturns.BadOperation, e.Message)
                ComDataOffset = New None(ServiceReturns.BadOperation, e.Message)
            End Try
            ' [[tagREG_INFO], [tagREG_INFO], [tagREG_INFO]...]
            Dim nextObject As Long
            Dim offsets = New List(Of Long)()
            Dim infos = New List(Of Vb5ComInfo)()
            While nextObject <> 0
                ' While dwNextObject offset not 0 -> add next COM registration info into object
                offsets.Add(_reader.BaseStream.Position)
                
                Dim info = Fill(Of Vb5ComInfo)(_reader)
                nextObject = info.NextObjectOffset
                infos.Add(info)
            End While
            
            ' Get results of ALL structs
            ComDataOffset = New Some(offset)
            ComData = New Some(data)
            ComInfo = New Some(infos)
            ComInfoOffsets = New Some(offsets)
        End Sub
    End Class
End Namespace