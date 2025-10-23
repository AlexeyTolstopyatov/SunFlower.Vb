Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Structure Vb5Method
        Public ObjectInfoPointer As UInt32
        Public Flag1 As UInt16
        Public Flag2 As UInt16
        Public CodeLength As UInt16
        Public Flag3 As UInt32
        Public Flag4 As UInt16
        Public Null As UInt16
        Public Flag5 As UInt32
        Public Flag6 As UInt16
    End Structure
    
    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Structure Vb5ResdescTbl
        Public TotalBytes As UShort
        Public Flags1 As UShort
        Public Count1 As UShort
        Public Count2 As UShort
        Public Flags2 As UShort
        Public Flags3 As UShort
    End Structure
End Namespace