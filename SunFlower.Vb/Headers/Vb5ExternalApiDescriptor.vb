Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential)>
    Public Structure ExternalApiDescriptor
        Public EntryType As UInt32
        Public EntryPointer As UInt32
    End Structure
    ''' If dwType of external descriptor == 7
    ''' pointer to info locates this structure
    <StructLayout(LayoutKind.Sequential)>
    Public Structure Vb5ImportByName
        Public LibraryNamePointer As UInt32
        Public FunctionNamePointer As UInt32
        Public Base As UInt32
        Public Null1 As UInt32
        Public Null2 As UInt32
    End Structure
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
