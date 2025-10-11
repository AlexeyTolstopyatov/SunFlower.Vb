Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure PeDirectory
        ''' RVA of directory
        Public VirtualAddress As UInt32
        ''' Size of data in directory
        Public Size As UInt32
    End Structure
End Namespace