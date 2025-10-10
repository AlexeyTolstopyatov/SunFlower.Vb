Namespace Managers
    Public Class PointersManager 
        Inherits UnsafeManager
        
        Private imageBase As Long
        
        Public Sub New(imageBase As Long)
            ' There's no any right methods to define runtime
            ' I specially say "f_ck it" and going to hell with only once
            ' knowing data field from PE32/+ "Optional" header
            Me.imageBase = imageBase
        End Sub
        
        Public Function LongPtrToOffset(ptr As Long) As Long 
            Dim weed = ptr - imageBase
            If weed < 0 Then 
                weed *= -1
            End If
            
            Return weed
        End Function
    End Class
End Namespace
