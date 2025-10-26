Imports System.IO
Imports System.Text
Imports SunFlower.Vb.Headers

Namespace Services
    Public Class Vb5ImportsParser
        Private ReadOnly _reader As BinaryReader
        Private ReadOnly _sections As List(Of PeSection)
        Private ReadOnly _imageBase As UInteger

        Public Sub New(reader As BinaryReader, sections As List(Of PeSection), imageBase As UInteger)
            _reader = reader
            _sections = sections
            _imageBase = imageBase
        End Sub

        Public Function ParseImports(externalTableVa As UInteger, externalCount As UInteger) As List(Of VbImportInfo)
            Dim [imports] = New List(Of VbImportInfo)()

            If externalCount = 0 OrElse externalTableVa = 0 Then
                Return [imports]
            End If

            Dim tableOffset = VaToFileOffset(externalTableVa) ' !!!
            If tableOffset < 0 Then Return [imports]

            _reader.BaseStream.Seek(tableOffset, SeekOrigin.Begin)
            Dim descriptorsList = New List(Of ExternalApiDescriptor)()
            
            For i = 1 To Math.Min(externalCount, 256)
                Dim descriptor = ReadExternalDescriptor()
                If descriptor Is Nothing Then Continue For
                
                descriptorsList.Add(descriptor)
            Next

            For Each i In descriptorsList
                Dim importInfo = ParseImportDescriptor(i)
                If importInfo IsNot Nothing Then
                    [imports].Add(importInfo)
                End If
            Next
            
            Return [imports]
        End Function

        Private Function ReadExternalDescriptor() As ExternalApiDescriptor?
            Try
                Dim descriptor As New ExternalApiDescriptor()
                descriptor.EntryType = _reader.ReadUInt32()
                descriptor.EntryPointer = _reader.ReadUInt32()
                Return descriptor
            Catch
                Return Nothing
            End Try
        End Function

        Private Function ParseImportDescriptor(descriptor As ExternalApiDescriptor) As VbImportInfo
            Select Case descriptor.EntryType
                Case 7 ' Import by Name
                    Return ParseImportByName(descriptor.EntryPointer)
                Case 6 ' Import by GUID  
                    Return ParseImportByGuid(descriptor.EntryPointer)
                Case Else
                    Return New VbImportInfo With {
                        .ImportType = descriptor.EntryType,
                        .LibraryName = $"?{descriptor.EntryType}",
                        .FunctionName = $"+0x{descriptor.EntryPointer:X8}"
                    }
            End Select
        End Function

        Private Function ParseImportByName(descriptorVa As UInteger) As VbImportInfo
            Dim importInfo As New VbImportInfo With {
                .ImportType = 7
            }

            Try
                ' VB_ApiImportType7
                Dim offset = VaToFileOffset(descriptorVa)
                If offset < 0 Then Return importInfo

                _reader.BaseStream.Seek(offset, SeekOrigin.Begin)
                
                Dim importStruct As New Vb5ImportByName()
                importStruct.LibraryNamePointer = _reader.ReadUInt32()
                importStruct.FunctionNamePointer = _reader.ReadUInt32()
                importStruct.Base = _reader.ReadUInt32()
                importStruct.Null1 = _reader.ReadUInt32()
                importStruct.Null2 = _reader.ReadUInt32()

                importInfo.LibraryName = ReadStringAtVa(importStruct.LibraryNamePointer)
                importInfo.FunctionName = ReadStringAtVa(importStruct.FunctionNamePointer)
                importInfo.RawPointer = offset

            Catch ex As Exception
                importInfo.LibraryName = $"![{ex.Message}]"
            End Try

            Return importInfo
        End Function

        Private Function ParseImportByGuid(descriptorVa As UInteger) As VbImportInfo
            Dim importInfo As New VbImportInfo With {.ImportType = 6}

            Try
                Dim offset = VaToFileOffset(descriptorVa)
                If offset < 0 Then Return importInfo

                _reader.BaseStream.Seek(offset, SeekOrigin.Begin)
                
                Dim guidPtr = _reader.ReadUInt32()
                Dim unknownPtr = _reader.ReadUInt32()

                importInfo.LibraryName = "[GUID]"
                importInfo.FunctionName = ReadGuidAtVa(guidPtr)
                importInfo.RawPointer = descriptorVa

            Catch ex As Exception
                importInfo.FunctionName = $"![{ex.Message}]"
            End Try

            Return importInfo
        End Function

        Private Function ReadStringAtVa(va As UInteger) As String
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

        Private Function ReadGuidAtVa(va As UInteger) As String
            If va = 0 Then Return "[?]"

            Try
                Dim offset = VaToFileOffset(va)
                If offset < 0 Then Return $"[?::+0x{va:X8}]"

                _reader.BaseStream.Seek(offset, SeekOrigin.Begin)
                Dim guidBytes = _reader.ReadBytes(16)
                
                If guidBytes.Length = 16 Then
                    Return New Guid(guidBytes).ToString()
                Else
                    Return "[!]"
                End If

            Catch ex As Exception
                Return $"![{ex.Message}]"
            End Try
        End Function

        Private Function VaToFileOffset(va As UInteger) As Long
            Dim rva = If(va - _imageBase > 0,
                         va - _imageBase,
                         _imageBase - va) ' Experiment from last test
            
            For Each section In _sections
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
    End Class

    Public Class VbImportInfo
        Public Property ImportType As UInteger
        Public Property LibraryName As String
        Public Property FunctionName As String
        Public Property RawPointer As UInteger

        Public Overrides Function ToString() As String
            Return $"{LibraryName}::{FunctionName}+0x{RawPointer:X8}"
        End Function
    End Class
End Namespace