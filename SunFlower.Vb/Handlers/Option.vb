
Imports SunFlower.Vb.Services

Namespace Handlers
    Public Enum OptionIndex
        None = 0
        Some = 1
    End Enum
    
    Public MustInherit Class [Option]
        Public Property Type As OptionIndex
        Public Shared Function GetResult(o As [Option])
            If o.Type = OptionIndex.Some
                Dim result As Some = o
                Return result
            Else 
                Dim result As None = o
                Return result
            End If
        End Function
        
    End Class
    
    Public Class Some 
        Inherits [Option]
        Public ReadOnly Property Data As Object

        Sub New(data As Object)
            Me.Data = data
            Me.Type = OptionIndex.Some
        End Sub
        Public Function Cast(Of T) () As T
            Dim obj As T = Data
            Return obj
        End Function
    End Class
    
    Public Class None
        Inherits [Option]
        Public ReadOnly Property ReasonString As String
        Public ReadOnly Property Reason As ServiceReturns
        
        Public Sub New(reason as ServiceReturns, reasonString As String)
            Me.ReasonString = reasonString
            Me.Reason = reason
            Me.Type = OptionIndex.None
        End Sub
    End Class
End Namespace