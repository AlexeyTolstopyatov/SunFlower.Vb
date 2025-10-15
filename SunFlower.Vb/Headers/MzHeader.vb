Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure MzHeader
        ' Token: 0x04000072 RID: 114
        <MarshalAs(UnmanagedType.U2)>
        Public Magic As UShort

        ' Token: 0x04000073 RID: 115
        <MarshalAs(UnmanagedType.U2)>
        Public CountBytesLastPage As UShort

        ' Token: 0x04000074 RID: 116
        <MarshalAs(UnmanagedType.U2)>
        Public LastBlock As UShort

        ' Token: 0x04000075 RID: 117
        <MarshalAs(UnmanagedType.U2)>
        Public CountRelocations As UShort

        ' Token: 0x04000076 RID: 118
        <MarshalAs(UnmanagedType.U2)>
        Public Paragraphs As UShort

        ' Token: 0x04000077 RID: 119
        <MarshalAs(UnmanagedType.U2)>
        Public MinimumAlloc As UShort

        ' Token: 0x04000078 RID: 120
        <MarshalAs(UnmanagedType.U2)>
        Public MaximumAlloc As UShort

        ' Token: 0x04000079 RID: 121
        <MarshalAs(UnmanagedType.U2)>
        Public StackSegment As UShort

        ' Token: 0x0400007A RID: 122
        <MarshalAs(UnmanagedType.U2)>
        Public StackPointer As UShort

        ' Token: 0x0400007B RID: 123
        <MarshalAs(UnmanagedType.U2)>
        Public Checksum As UShort

        ' Token: 0x0400007C RID: 124
        <MarshalAs(UnmanagedType.U2)>
        Public InstructionPointer As UShort

        ' Token: 0x0400007D RID: 125
        <MarshalAs(UnmanagedType.U2)>
        Public CodeSegment As UShort

        ' Token: 0x0400007E RID: 126
        <MarshalAs(UnmanagedType.U2)>
        Public RelocationsOffset As UShort

        ' Token: 0x0400007F RID: 127
        <MarshalAs(UnmanagedType.U2)>
        Public CurrentOverlayNumber As UShort

        ' Token: 0x04000080 RID: 128
        <MarshalAs(UnmanagedType.ByValArray, SizeConst := 4)>
        Public Reserved As UShort()

        ' Token: 0x04000081 RID: 129
        <MarshalAs(UnmanagedType.U2)>
        Public OemID As UShort

        ' Token: 0x04000082 RID: 130
        <MarshalAs(UnmanagedType.U2)>
        Public OemInfo As UShort

        ' Token: 0x04000083 RID: 131
        <MarshalAs(UnmanagedType.ByValArray, SizeConst := 10)>
        Public Reserved2 As UShort()

        ' Token: 0x04000084 RID: 132
        <MarshalAs(UnmanagedType.U4)>
        Public Offset As UInteger
    End Structure
End Namespace