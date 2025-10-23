Imports System.IO
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Managers

Namespace Services
    Public Class Vb5ComRegistrationParser
        Inherits MemoryManager
        Private ReadOnly _header As Vb5Header
        Private _comRegDataBaseOffset As UInteger
        Public Sub New(imageBase As Long, header As Vb5Header, reader As BinaryReader, sections As List(Of PeSection))
            MyBase.New(imageBase := imageBase, 
                       sections := sections)
            _header = header
            _reader = reader
        End Sub
                
        Public Function ParseComRegistrationData() As Vb5ComRegistrationResult
            Dim result As New Vb5ComRegistrationResult()

            Try
                _comRegDataBaseOffset = VaToFileOffset(_header.ComRegisterDataPointer)
                _reader.BaseStream.Seek(_comRegDataBaseOffset, SeekOrigin.Begin)
                result.ComRegData = Fill(Of Vb5ComData)(_reader)

                result.ProjectName = ReadStringAtComOffset(result.ComRegData.ProjectNameOffset)
                result.ProjectDescription = ReadStringAtComOffset(result.ComRegData.ProjectDescriptionOffset)
                result.HelpDirectory = ReadStringAtComOffset(result.ComRegData.HelpDirectoryOffset)
                
                If result.ComRegData.RegInfoOffset <> 0 Then
                    result.ComRegInfos = ParseComRegInfoChain(result.ComRegData.RegInfoOffset)
                End If

                result.Success = True
            Catch ex As Exception
                result.ErrorMessage = ex.Message
                result.Success = False
            End Try

            Return result
        End Function

        Private Function ParseComRegInfoChain(startOffset As UInteger) As List(Of Vb5ComInfoDetails)
            Dim comInfos = New List(Of Vb5ComInfoDetails)()
            Dim currentOffset As UInteger = startOffset
            Dim processedOffsets As New HashSet(Of UInteger)()

            While currentOffset <> 0 AndAlso Not processedOffsets.Contains(currentOffset) AndAlso comInfos.Count < 100
                processedOffsets.Add(currentOffset)

                Dim comInfo = ParseSingleComRegInfo(currentOffset)
                If comInfo Is Nothing Then Exit While

                comInfos.Add(comInfo)
                currentOffset = comInfo.ComRegInfo.NextObjectOffset
            End While

            Return comInfos
        End Function

        Private Function ParseSingleComRegInfo(offset As UInteger) As Vb5ComInfoDetails
            Try
                Dim absoluteOffset = _comRegDataBaseOffset + offset
                _reader.BaseStream.Seek(absoluteOffset, SeekOrigin.Begin)

                Dim comRegInfo = Fill(Of Vb5ComInfo)(_reader)
                Dim details As New Vb5ComInfoDetails With {
                    .ComRegInfo = comRegInfo,
                    .Offset = offset
                }

                details.ObjectName = ReadStringAtComOffset(comRegInfo.ObjectNameOffset)
                details.ObjectDescription = ReadStringAtComOffset(comRegInfo.ObjectDescriptionOffset)

                If comRegInfo.UuidObjectInterfaceOffset <> 0 Then
                    details.UuidObjectInterface = ReadGuidAtComOffset(comRegInfo.UuidObjectInterfaceOffset)
                End If

                If comRegInfo.UuidEventsInterfaceOffset <> 0 Then
                    details.UuidEventsInterface = ReadGuidAtComOffset(comRegInfo.UuidEventsInterfaceOffset)
                End If

                If comRegInfo.IsDesigner <> 0 AndAlso comRegInfo.DesignerDataOffset <> 0 Then
                    details.DesignerData = ParseDesignerData(comRegInfo.DesignerDataOffset)
                End If

                Return details

            Catch ex As Exception
                Debug.WriteLine($"Error parsing COM RegInfo at offset {offset}: {ex.Message}")
                Return Nothing
            End Try
        End Function

        Private Function ReadStringAtComOffset(offset As UInteger) As String
            If offset = 0 Then Return String.Empty

            Try
                Dim stringOffset = _comRegDataBaseOffset + offset
                _reader.BaseStream.Seek(stringOffset, SeekOrigin.Begin)
                
                Return ReadStringAtOffset(stringOffset)
            Catch ex As Exception
                Return $"[Error reading string at offset {offset}: {ex.Message}]"
            End Try
        End Function

        Private Function ReadGuidAtComOffset(offset As UInteger) As Guid?
            If offset = 0 Then Return Nothing

            Try
                Dim guidOffset = _comRegDataBaseOffset + offset
                _reader.BaseStream.Seek(guidOffset, SeekOrigin.Begin)
                Dim guidBytes = _reader.ReadBytes(16)
                Return New Guid(guidBytes)
            Catch
                Return Nothing
            End Try
        End Function

        Private Function ParseDesignerData(offset As UInteger) As Vb5DesignerInfo
            Dim designerData As New Vb5DesignerInfo()

            Try
                Dim designerOffset = _comRegDataBaseOffset + offset
                _reader.BaseStream.Seek(designerOffset, SeekOrigin.Begin)

                Dim designerUuidBytes = _reader.ReadBytes(16)
                designerData.DesignerUuid = New Guid(designerUuidBytes)
                designerData.StructSize = _reader.ReadUInt32()

                designerData.AddInRegKey = ReadBStrString(_reader)
                designerData.AddInName = ReadBStrString(_reader) 
                designerData.AddInDescription = ReadBStrString(_reader)
                
                If _reader.BaseStream.Position < designerOffset + designerData.StructSize Then
                    _reader.ReadUInt32()
                End If

                designerData.SatelliteDll = ReadBStrString(_reader)
                designerData.AdditionalRegKey = ReadBStrString(_reader)
                
                If _reader.BaseStream.Position < designerOffset + designerData.StructSize Then
                    designerData.CommandLineSafe = _reader.ReadUInt32()
                End If

            Catch ex As Exception
                Debug.WriteLine($"Error parsing designer data: {ex.Message}")
            End Try

            Return designerData
        End Function

        Private Function ReadBStrString(reader As BinaryReader) As String
            Try
                Dim length = reader.ReadUInt32()
                If length = 0 OrElse length > 1000 Then Return String.Empty

                Dim bytes = reader.ReadBytes(CInt(length) * 2) '<-- fixed [u8; 2]
                Return Text.Encoding.Unicode.GetString(bytes)

            Catch
                Return String.Empty
            End Try
        End Function
    End Class
    Public Class Vb5DesignerInfo
        Public Property DesignerUuid As Guid
        Public Property StructSize As UInteger
        Public Property AddInRegKey As String
        Public Property AddInName As String
        Public Property AddInDescription As String
        Public Property SatelliteDll As String
        Public Property AdditionalRegKey As String
        Public Property CommandLineSafe As UInteger
    End Class
    Public Class Vb5ComRegistrationResult
        Public Property Success As Boolean
        Public Property ErrorMessage As String
        Public Property ComRegData As Vb5ComData
        Public Property ProjectName As String
        Public Property ProjectDescription As String
        Public Property HelpDirectory As String
        Public Property ComRegInfos As List(Of Vb5ComInfoDetails)
    End Class

    Public Class Vb5ComInfoDetails
        Public Property ComRegInfo As Vb5ComInfo
        Public Property Offset As UInteger
        Public Property ObjectName As String
        Public Property ObjectDescription As String
        Public Property UuidObjectInterface As Guid?
        Public Property UuidEventsInterface As Guid?
        Public Property DesignerData As Vb5DesignerInfo
    End Class
End Namespace