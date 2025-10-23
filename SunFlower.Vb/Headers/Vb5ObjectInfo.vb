Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure Vb5ObjectDescriptor
        Public ObjectInfoPointer As UInt32       ' lpObjectInfo
        Public Reserved As UInt32                ' dwReserved (всегда -1)
        Public PublicBytesPointer As UInt32      ' lpPublicBytes -> RESDESCTBL
        Public StaticBytesPointer As UInt32      ' lpStaticBytes -> RESDESCTBL  
        Public ModulePublicPointer As UInt32     ' lpModulePublic
        Public ModuleStaticPointer As UInt32     ' lpModuleStatic
        Public ObjectNamePointer As UInt32       ' lpszObjectName
        Public MethodCount As UInt32             ' dwMethodCount
        Public MethodNamesPointer As UInt32      ' lpMethodNames
        Public StaticVarsOffset As UInt32        ' bStaticVars
        Public ObjectTypeFlags As UInt32         ' fObjectType
        Public Null As UInt32                    ' dwNull
    End Structure
    
    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Structure Vb5ObjectInfo
        Public RefCount As UInt16                ' wRefCount
        Public ObjectIndex As UInt16             ' wObjectIndex
        Public ObjectTablePointer As UInt32      ' lpObjectTable
        Public IdeDataPointer As UInt32          ' lpIdeData (IDE only)
        Public PrivateObjectPointer As UInt32    ' lpPrivateObject
        Public Reserved As UInt32                ' dwReserved (-1 after compiling)
        Public Null1 As UInt32                   ' dwNull
        Public ObjectPointer As UInt32           ' lpObject (back to ObjectTable)
        Public ProjectDataPointer As UInt32      ' lpProjectData
        Public MethodCount As UInt16             ' wMethodCount
        Public MethodCountUsed As UInt16         ' wMethodCountUsed (IDE only)
        Public MethodsPointer As UInt32          ' lpMethods
        Public ConstantsCount As UInt16          ' wConstants
        Public MaxConstants As UInt16            ' wMaxConstants
        Public IdeDataPointer2 As UInt32         ' lpIdeData2 (IDE only)
        Public IdeDataPointer3 As UInt32         ' lpIdeData3 (IDE only)
        Public ConstantsPointer As UInt32        ' lpConstants
    End Structure

    Public Class Vb5ObjectTypeInfo
        Public Property Flags As UInt32
        Public Property TypeName As String
        Public Property IsForm As Boolean
        Public Property IsModule As Boolean  
        Public Property IsClass As Boolean
        Public Property HasPublicInterface As Boolean
        Public Property HasPublicEvents As Boolean
    End Class
End Namespace