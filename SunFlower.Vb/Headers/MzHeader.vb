Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure MzHeader
        Public Magic As UInt16
        <MarshalAs(UnmanagedType.ByValArray, SizeConst := 58)>
        Public Data() As Byte ' skip unused data
        Public Offset As UInt32
    End Structure
End Namespace