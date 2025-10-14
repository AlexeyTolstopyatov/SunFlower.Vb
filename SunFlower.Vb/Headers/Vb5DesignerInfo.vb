Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure Vb5DesignerInfo
        <MarshalAs(UnmanagedType.BStr)>
        Public AddInRegKey As String

        <MarshalAs(UnmanagedType.BStr)>
        Public AddInName As String

        <MarshalAs(UnmanagedType.BStr)>
        Public AddInDescription As String
        Public LoadBehaviour As UInteger
        
        <MarshalAs(UnmanagedType.BStr)>
        Public SatelliteDll As String

        <MarshalAs(UnmanagedType.BStr)>
        Public AdditionalRegKey As String
        Public CommandLineSafe As UInteger
    End Structure
End Namespace