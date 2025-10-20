Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text

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
        ''' LPSTR or LPCSTR
        ''' typically C/++ string (zero terminated ASCII sequence)
        Protected Function FillCString (reader As BinaryReader) As String
            Dim i As Char
            Dim strList = New List(Of Char)
            
            While (CByte(i = reader.ReadChar())) <> 0 And strList.Count <> 256
                strList.Add(i)
            End While
            
            Return New String(strList.ToArray())
        End Function
    End Class
End Namespace