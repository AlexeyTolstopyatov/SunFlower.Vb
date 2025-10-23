Imports System.IO
Imports System.Text
Imports SunFlower.Vb.Headers

Namespace Managers
    Public Class MemoryManager
        Inherits UnsafeManager
        
        Protected sections As List(Of PeSection)
        Protected imageBase As Long
        Protected vbHeaderOffset As Long
        Protected size As Long
        Protected _reader As BinaryReader
        
        Protected Sub New(imageBase As Long, sections As List(Of PeSection))
            Me.imageBase = imageBase
            Me.sections = sections
        End Sub

        Protected Function ReadStringAtVa(va As UInteger) As String
            If va = 0 Then Return "[NULL]"

            Try
                Dim offset = VaToFileOffset(va)
                If offset < 0 Then Return $"[::_+0x{va:X8}]"

                _reader.BaseStream.Seek(offset, SeekOrigin.Begin)
                
                Dim bytes As New List(Of Byte)()
                Dim b As Integer
                
                Do
                    b = _reader.ReadByte()
                    If b = 0 Then Exit Do
                    bytes.Add(CByte(b))
                Loop While bytes.Count < 256

                Return Encoding.Default.GetString(bytes.ToArray())

            Catch ex As Exception
                Return $"![{ex.Message}]"
            End Try
        End Function
        Protected Function VaToFileOffset(va As UInteger) As Long
            Dim rva = If(va - imageBase > 0,
                         va - imageBase,
                         imageBase - va) ' Experiment from last test
            
            For Each section In sections
                If rva >= section.VirtualAddress AndAlso 
                   rva < section.VirtualAddress + Math.Max(section.VirtualSize, section.SizeOfRawData) Then
                    
                    Return section.PointerToRawData + (rva - section.VirtualAddress)
                End If
            Next
            
            If rva < &H10000 Then
                Return rva
            End If
            
            Return -1
        End Function
        Protected Function IsValidVa(va As UInteger) As Boolean
            If va = 0 Then Return False
            
            Try
                Dim offset = VaToFileOffset(va)
                Return offset >= 0 AndAlso _reader.BaseStream.Length
            Catch
                Return False
            End Try
        End Function
    End Class
End Namespace