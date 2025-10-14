Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure Vb5ProjectInfo
        Public Version As UInteger
        Public ObjectTablePointer As UInteger
        Public Null As UInteger
        Public CodeStartsPointer As UInteger
        Public CodeEndsPointer As UInteger
        Public DataSize As UInteger
        Public ThreadSpacePointer As UInteger
        Public VbaSEHPointer As UInteger
        Public NativeCodePointer As UInteger
        
        <MarshalAs(UnmanagedType.ByValArray, SizeConst := 6)>
        Public PrimitivePath As Byte()
        <MarshalAs(UnmanagedType.ByValArray, SizeConst := 530)>
        Public ProjectPath As Byte()
        
        Public ExternalTablePointer As UInteger
        Public ExternalTableCount As UInteger
    End Structure
End Namespace