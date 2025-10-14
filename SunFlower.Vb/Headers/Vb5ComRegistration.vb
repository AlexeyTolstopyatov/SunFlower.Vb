Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure Vb5ComData
        ''' Offset from the COM registration data ENDS
        Public RegInfoOffset As UInteger
        Public ProjectNameOffset As UInteger
        Public HelpDirectoryOffset As UInteger
        Public ProjectDescriptionOffset As UInteger
        Public UuidProjectClsId As ULong
        Public TypeLibraryLanguageId As UInteger
        Public Unknown As UInteger
        Public TypeLibraryMajor As UShort
        Public TypeLibraryMinor As UShort
    End Structure
    
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure Vb5ComInfo
        ''' If not zero -> another COM information structure goes next
        Public NextObjectOffset As UInteger
        Public ObjectNameOffset As UInteger
        Public ObjectDescriptionOffset As UInteger
        Public InstancingMode As UInteger
        Public ObjectId As UInteger
        Public UuidObject As ULong
        Public IsInterface As UInteger
        Public UuidObjectInterfaceOffset As UInteger
        Public UuidEventsInterfaceOffset As UInteger
        Public HasEvents As UInteger
        Public MiscStatus As UInteger
        Public ClassType As Byte
        Public ObjectType As Byte
        Public ToolBoxBitmap32 As UShort
        Public DefaultIcon As UShort
        Public IsDesigner As UShort
        Public DesignerDataOffset As UInteger
    End Structure
End Namespace