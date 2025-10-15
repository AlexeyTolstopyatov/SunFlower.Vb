Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure Vb5ObjectTable
        Public HeapPointer As UInt32 ' not used
        Public ExecProjPointer As UInt32
        Public ProjectInfo2Pointer As UInt32
        Public Reserved As UInt32
        Public Null As UInt32
        Public ProjectObjectPointer As UInt32
        Public UuidObject As UInt64
        Public CompileState As UInt16 ' I suppose it helps to call End-Runtime messages
        Public TotalObjectsCount As UInt16 ' <-- this is a count of API descriptors
        Public CompiledObjects As UInt16   ' <-- not this.
        Public ObjectsInUseCount As UInt16
        Public ObjectsArrayPointer As UInt32 ' Public API Descriptors[]
        Public IdeDataPointer As UInt32
        Public IdeDataPointer2 As UInt32
        Public ProjectNamePointer As UInt32
        Public LCID As UInt32
        Public LCID2 As UInt32
        Public IdeDataPointer3 As UInt32
        Public Identifier As UInt32
    End Structure
End Namespace