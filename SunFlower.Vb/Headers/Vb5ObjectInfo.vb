Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure Vb5ObjectDescriptor
        Public ObjectInfoPointer As UInt32
        Public Reserved As UInt32
        Public PublicBytesPointer As UInt32 '--> RESDESC_Table
        Public StaticBytesPointer As UInt32 '--> another RESDESC_Table
        Public ObjectStringPointer As UInt32
        Public MethodsCount As UInt32
        Public MethodsPointer As UInt32 ' --> [lpMethodNamem, lpMethodName, lpMethodName ...]
        Public StaticVars As UInt32
        Public ObjectFlag As UInt32
        Public Null As UInt32
    End Structure
    
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure Vb5ObjectInfo
        Public ReferencesCount As UInt16
        Public ObjectIndex As UInt16
        Public ObjectInfoPointer As UInt32
        Public IdeDataPointer As UInt32
        Public PrivateObjectPointer As UInt32 ' --> PrivateObject_Descriptor
        Public Reserved As UInt32
        Public Null As UInt32
        Public ObjectPointer As UInt32
        Public ProjectData As UInt32
        Public MethodCount As UInt16
        Public MethodCount2 As UInt16
        Public MethodsPointer As UInt32 ' --> [lpMethodName, lpMethodName, ...]
        Public Constants As UInt16
        Public MaxConstants As UInt16
        Public IdeDataPointer2 As UInt32
        Public IdeDataPointer3 As UInt32
        Public ConstantsPointer As UInt32
        ' Next struct what follows -- Optional Object Info
    End Structure
End Namespace