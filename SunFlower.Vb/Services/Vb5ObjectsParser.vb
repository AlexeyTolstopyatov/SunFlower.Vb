Imports System.IO
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Managers

Namespace Services
    Public Class Vb5ObjectDetails
        Public Property Index As UInteger
        Public Property Descriptor As Vb5ObjectDescriptor
        Public Property ObjectInfo As Vb5ObjectInfo
        Public Property TypeInfo As Vb5ObjectTypeInfo
        Public Property ObjectName As String
        Public Property MethodNames As List(Of String)
        Public Property Methods As List(Of Vb5MethodInfo)
        Public Property PublicVariables As List(Of Vb5ResdescInfo)
        Public Property StaticVariables As List(Of Vb5ResdescInfo)
    End Class

    Public Class Vb5MethodInfo
        Public Property Index As UInteger
        Public Property Method As Vb5Method
    End Class

    Public Class Vb5ResdescInfo
        Public Property TypeName As String
        Public Property TotalBytes As UShort
        Public Property Flags1 As UShort
        Public Property Count1 As UShort
        Public Property Count2 As UShort
    End Class
    Public Class Vb5ObjectsParser
        Inherits MemoryManager

        Public Sub New(reader As BinaryReader, sections As List(Of PeSection), imageBase As UInteger)
            MyBase.New(imageBase, sections)
            _reader = reader
            Me.sections = sections
            Me.imageBase = imageBase
        End Sub

        Public Function ParseObjects(objectArrayRva As UInteger, objectCount As UInteger) As List(Of Vb5ObjectDetails)
            Dim objects = New List(Of Vb5ObjectDetails)()

            If objectCount = 0 OrElse objectArrayRva = 0 Then
                Return objects
            End If

            Dim arrayOffset = VaToFileOffset(objectArrayRva)
            Dim descriptors = New List(Of Vb5ObjectDescriptor)

            If arrayOffset < 0 Then Return objects

            _reader.BaseStream.Seek(arrayOffset, SeekOrigin.Begin)

            For i As UInteger = 1 To objectCount
                Dim descriptor = Fill (Of Vb5ObjectDescriptor)(_reader)
                descriptors.Add(descriptor)
            Next

            Dim index As UInteger = 1

            For Each descriptor In descriptors
                Dim objectDetails = ParseObjectDetails(descriptor, index)
                If objectDetails IsNot Nothing Then
                    objects.Add(objectDetails)
                End If
                index += 1
            Next

            Return objects
        End Function

        Private Function ParseObjectDetails(descriptor As Vb5ObjectDescriptor, index As UInteger) As Vb5ObjectDetails
            Dim details As New Vb5ObjectDetails() With {
                .Index = index,
                .Descriptor = descriptor,
                .TypeInfo = AnalyzeObjectType(descriptor.ObjectTypeFlags)
            }

            If IsValidVa(descriptor.ObjectNamePointer) Then
                details.ObjectName = ReadStringAtVa(descriptor.ObjectNamePointer)
            End If

            If IsValidVa(descriptor.ObjectInfoPointer) Then
                details.ObjectInfo = ReadObjectInfo(descriptor.ObjectInfoPointer)
            End If

            If IsValidVa(descriptor.PublicBytesPointer) Then
                details.PublicVariables = ParseResdescTbl(descriptor.PublicBytesPointer, "Public")
            End If

            If IsValidVa(descriptor.StaticBytesPointer) Then
                details.StaticVariables = ParseResdescTbl(descriptor.StaticBytesPointer, "Static")
            End If

            If descriptor.MethodCount > 0 AndAlso IsValidVa(descriptor.MethodNamesPointer) Then
                details.MethodNames = ReadMethodNames(descriptor.MethodNamesPointer, descriptor.MethodCount)
            End If

            If details.ObjectInfo.MethodCount > 0 AndAlso IsValidVa(details.ObjectInfo.MethodsPointer) Then
                details.Methods = ReadMethods(details.ObjectInfo.MethodsPointer, details.ObjectInfo.MethodCount)
            End If

            Return details
        End Function

        Private Function ReadObjectInfo(objectInfoRva As UInteger) As Vb5ObjectInfo
            Try
                _reader.BaseStream.Seek(VaToFileOffset(objectInfoRva), SeekOrigin.Begin)
                Return Fill (Of Vb5ObjectInfo)(_reader)
            Catch
                Return New Vb5ObjectInfo()
            End Try
        End Function

        Private Function ReadMethodNames(methodNamesRva As UInteger, m As UInteger) As List(Of String)
            Dim names = New List(Of String)()
            Dim methodPointers = New List(Of UInteger)()
            m = Math.Min(m, 255)
            Try
                _reader.BaseStream.Seek(VaToFileOffset(methodNamesRva), SeekOrigin.Begin)
                
                For i As UInteger = 1 To m
                    Dim namePtr = _reader.ReadUInt32()
                    methodPointers.Add(namePtr)
                Next
                
                For Each ptr In methodPointers
                    If IsValidVa(ptr) Then
                        Dim name = ReadStringAtVa(ptr)
                        If Not String.IsNullOrEmpty(name) Then
                            names.Add(name)
                        End If
                    End If
                Next
            Catch
                ' ignoring
            End Try

            Return names
        End Function

        Private Function ReadMethods(methodsRva As UInteger, m As UInteger) As List(Of Vb5MethodInfo)
            Dim methods = New List(Of Vb5MethodInfo)()

            Try
                _reader.BaseStream.Seek(VaToFileOffset(methodsRva), SeekOrigin.Begin)

                For i As UInteger = 0 To m - 1
                    Dim method = Fill (Of Vb5Method)(_reader)
                    methods.Add(New Vb5MethodInfo With {
                                   .Method = method,
                                   .Index = i
                                   })
                Next
            Catch
                ' ignoring
            End Try

            Return methods
        End Function

        Private Function ParseResdescTbl(resdescRva As UInteger, typeName As String) As List(Of Vb5ResdescInfo)
            Dim resdescs = New List(Of Vb5ResdescInfo)()

            Try
                _reader.BaseStream.Seek(VaToFileOffset(resdescRva), SeekOrigin.Begin)

                Dim resdescTbl = Fill (Of Vb5ResdescTbl)(_reader)
                Dim resdescInfo As New Vb5ResdescInfo With {
                        .TypeName = typeName,
                        .TotalBytes = resdescTbl.TotalBytes,
                        .Flags1 = resdescTbl.Flags1,
                        .Count1 = resdescTbl.Count1,
                        .Count2 = resdescTbl.Count2
                        }

                resdescs.Add(resdescInfo)
            Catch
                ' STOP: memory leaks may be dangerous 
            End Try

            Return resdescs
        End Function

        Private Function AnalyzeObjectType(flags As UInteger) As Vb5ObjectTypeInfo
            Dim typeInfo As New Vb5ObjectTypeInfo() With {
                    .Flags = flags
                    }

            Dim hasPublicInterface As Boolean = (flags And &H20000) <> 0
            Dim hasPublicEvents As Boolean = (flags And &H40000) <> 0
            Dim isCreatable As Boolean = (flags And &H80000) <> 0
            Dim isUserControl As Boolean = (flags And &H100000) <> 0
            Dim isOCX As Boolean = (flags And &H200000) <> 0
            Dim isForm As Boolean = (flags And &H400000) <> 0
            Dim isVB5 As Boolean = (flags And &H800000) <> 0
            Dim hasOptInfo As Boolean = (flags And &H1000000) <> 0
            Dim isModule As Boolean = (flags And &H2000000) <> 0

            typeInfo.HasPublicInterface = hasPublicInterface
            typeInfo.HasPublicEvents = hasPublicEvents
            typeInfo.IsForm = isForm
            typeInfo.IsModule = isModule
            typeInfo.IsClass = Not (isForm Or isModule)

            If isForm Then
                typeInfo.TypeName = "Form"
            ElseIf isModule Then
                typeInfo.TypeName = "Module"
            ElseIf isUserControl Then
                typeInfo.TypeName = "UserControl"
            ElseIf isOCX Then
                typeInfo.TypeName = "OCX"
            Else
                typeInfo.TypeName = "Class"
            End If

            Return typeInfo
        End Function
    End Class
End Namespace