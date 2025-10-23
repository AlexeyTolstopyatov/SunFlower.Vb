Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Structure Vb5ComData
        Public RegInfoOffset As UInteger      ' bRegInfo
        Public ProjectNameOffset As UInteger  ' bSZProjectName
        Public HelpDirectoryOffset As UInteger ' bSZHelpDirectory
        Public ProjectDescriptionOffset As UInteger ' bSZProjectDescription
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
        Public UuidProjectClsId As Byte()     ' uuidProjectClsId
        Public TlbLcid As UInteger            ' dwTlbLcid
        Public Unknown As UShort              ' wUnknown
        Public TlbVerMajor As UShort          ' wTlbVerMajor
        Public TlbVerMinor As UShort          ' wTlbVerMinor
    End Structure

    
    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Structure Vb5ComInfo
        Public NextObjectOffset As UInteger   ' bNextObject
        Public ObjectNameOffset As UInteger   ' bObjectName
        Public ObjectDescriptionOffset As UInteger ' bObjectDescription
        Public InstancingMode As UInteger     ' dwInstancing
        Public ObjectId As UInteger           ' dwObjectId
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
        Public UuidObject As Byte()           ' uuidObject
        Public IsInterface As UInteger        ' fIsInterface
        Public UuidObjectInterfaceOffset As UInteger ' bUuidObjectIFace
        Public UuidEventsInterfaceOffset As UInteger ' bUuidEventsIFace
        Public HasEvents As UInteger          ' fHasEvents
        Public MiscStatus As UInteger         ' dwMiscStatus
        Public ClassType As Byte              ' fClassType
        Public ObjectType As Byte             ' fObjectType
        Public ToolboxBitmap32 As UShort      ' wToolboxBitmap32
        Public DefaultIcon As UShort          ' wDefaultIcon
        Public IsDesigner As UShort           ' fIsDesigner
        Public DesignerDataOffset As UInteger ' bDesignerData
    End Structure
End Namespace