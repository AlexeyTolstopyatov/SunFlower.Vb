Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential)>
    Public Structure ExternalApiDescriptor
        Public EntryType As UInt32
        Public EntryPointer As UInt32
    End Structure
    ''' If dwType of external descriptor == 7
    ''' pointer to info locates this structure
    Public Class Vb5ImportByName
        Public Property LibraryNamePointer As UInt32
        Public Property LibraryName As String
        Public Property FunctionNamePointer As UInt32
        Public Property FunctionName As String
        Public Property Base As UInt32
        Sub New(libraryNamePointer As UInteger, libraryName As String, functionNamePointer As UInteger, functionName As String, base As UInteger)
            Me.LibraryNamePointer = libraryNamePointer
            Me.LibraryName = libraryName
            Me.FunctionNamePointer = functionNamePointer
            Me.FunctionName = functionName
            Me.Base = base
        End Sub
        ' dwNULL_1
        ' dwNULL_2
    End Class
    ''' If dwType of External descriptor == 6
    ''' pointer to information locates this structure
    Public Class Vb5ImportByGuid
        Public Property GuidPointer As UInt32
        Public Property Guid As UInt64
        Public Property UnknownPointer As UInt32

        Sub New(guidPointer As UInteger, guid As ULong, unknownPointer As UInteger)
            Me.GuidPointer = guidPointer
            Me.Guid = guid
            Me.UnknownPointer = unknownPointer
        End Sub
    End Class
    
End Namespace
