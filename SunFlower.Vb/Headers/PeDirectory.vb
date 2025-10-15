Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure PeDirectory
        ''' RVA of directory
        <MarshalAs(UnmanagedType.U4)>
        Public VirtualAddress As UInt32
        ''' Size of data in directory
        <MarshalAs(UnmanagedType.U4)>
        Public Size As UInt32
    End Structure
End Namespace