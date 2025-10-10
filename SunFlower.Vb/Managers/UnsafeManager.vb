Imports System.IO
Imports System.Runtime.InteropServices

Namespace Managers
    Public Class UnsafeManager
        ''' Calls the Garbage collector and allocates memory
        ''' for the unmanaged bytes range.
        Public Function Fill(Of TStruct As Structure) (reader As BinaryReader) As TStruct
            Dim struct = New TStruct()
            Dim rawBytes = reader.ReadBytes(Marshal.SizeOf(struct.GetType()))
            Dim handle = GCHandle.Alloc(rawBytes, GCHandleType.Pinned)
            
            struct = Marshal.PtrToStructure(Of TStruct)(handle)
            
            handle.Free()
            Return struct
        End Function
    End Class
End Namespace