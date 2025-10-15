Imports System.IO
Imports System.Runtime.InteropServices

Namespace Managers
    Public Class UnsafeManager
        ''' Calls the Garbage collector and allocates memory
        ''' for the unmanaged bytes range.
        Protected Function Fill(Of TStruct As Structure)(reader As BinaryReader) As TStruct
            Dim bytes As Byte() = reader.ReadBytes(Marshal.SizeOf(GetType(TStruct)))
            Dim handle As GCHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned)
            Dim result = Marshal.PtrToStructure(Of TStruct)(handle.AddrOfPinnedObject())
            handle.Free()
            Return result
        End Function
        Protected Function FillCString (reader As BinaryReader) As String
            Dim str = New List(Of Char)
            Dim c As Char = reader.ReadChar()
            While c <> Chr(0)
                str.Add(c)
            End While
            
            Return New String(str.ToArray())
        End Function
    End Class
End Namespace