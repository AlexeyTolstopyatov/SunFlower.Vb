Imports System.IO

Namespace Services 
    Public Class Vb5DumpingService
        
        Sub New(path As String)
            Using stream = New FileStream(path := path, mode := FileMode.Open, access := FileAccess.Read) 
                Using reader = new BinaryReader(stream)
                    Dim signature = reader.ReadUInt16()
                    
                    If signature <> &H5A4D And signature <> &H4D5A
                        reader.Close()
                        Return
                    End If
                    
                    stream.Position = &H3C
                    
                    Dim offset = reader.ReadUInt32()
                    
                    stream.Position = offset
                    
                    reader.Close()
                End Using
            End Using
        End Sub
        
    End Class
End Namespace