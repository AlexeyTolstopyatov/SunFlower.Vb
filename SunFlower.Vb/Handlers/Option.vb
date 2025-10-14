
Imports SunFlower.Vb.Services

Namespace Handlers
    Public Enum OptionIndex
        None = 0
        Some = 1
    End Enum
    
    Public MustInherit Class [Option]
        Public Property Type As OptionIndex
    End Class
    
    Public Class Some 
        Inherits [Option]
        Public ReadOnly Property Data As Object

        Sub New(data As Object)
            Me.Data = data
        End Sub
    End Class
    
    Public Class None
        Inherits [Option]
        Public ReadOnly Property ReasonString As String
        Public ReadOnly Property Reason As ServiceReturns
        
        Public Sub New(reason as ServiceReturns, reasonString As String)
            Me.ReasonString = reasonString
            Me.Reason = reason
        End Sub
    End Class
End Namespace