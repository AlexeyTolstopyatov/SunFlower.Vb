Imports System.Data
Imports SunFlower.Abstractions
Imports SunFlower.Abstractions.Types
Imports SunFlower.Vb.Headers

Namespace Visualizers
Public Class Vb5ProjectInfoVisualizer
    Inherits AbstractStructVisualizer(Of Vb5ProjectInfo)

    Public Sub New(struct As Vb5ProjectInfo)
        MyBase.New(struct)
    End Sub
    
    Public Overrides Function ToDataTable() As DataTable
        Return FlowerReflection.GetNameValueTable(_struct)
    End Function

    Public Overrides Function ToString() As String
        Return $"""
`tPROJINFO` or ProjectInfo structure has nested
complex types. All pointers in structure are VA.
 - Fills by `vba6.dll!0x0FB11783`;
 - Application is {If (_struct.NativeCodePointer = 0, "P-Code", "N-Code")} compiled; 
"""
    End Function

    Public Overrides Function ToRegion() As Region
        Return New Region("### Project Info", ToString(), ToDataTable())
    End Function
End Class
End Namespace