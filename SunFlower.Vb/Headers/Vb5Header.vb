Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure Vb5Header
        <MarshalAs(UnmanagedType.ByValArray, SizeConst := 4)>
        Public VbMagic As Char()
        Public RuntimeBuild As UShort
        <MarshalAs(UnmanagedType.ByValArray, SizeConst := 14)>
        Public LanguageDll As Char()
        <MarshalAs(UnmanagedType.ByValArray, SizeConst := 14)>
        Public SecondLanguageDll As Char()
        Public RuntimeRevision As UShort
        Public LanguageId As UInteger
        Public SecondLanguageId As UInteger
        Public SubMainAddress As UInteger
        Public ProjectDataPointer As UInteger
        Public ControlsFlagLow As UInteger
        Public ControlsFlagHigh As UInteger
        Public ThreadFlags As UInteger
        Public ThreadCount As UInteger
        Public FormCtlsCount As UShort
        Public ExternalCtlsCount As UShort
        Public ThunkCount As UInteger
        Public GuiTablePointer As UInteger
        Public ExternalTablePointer As UInteger
        Public ComRegisterDataPointer As UInteger
        Public ProjectDescriptionOffset As UInteger
        Public ProjectExeNameOffset As UInteger
        Public ProjectHelpOffset As UInteger
        Public ProjectNameOffset As UInteger
    End Structure
End Namespace