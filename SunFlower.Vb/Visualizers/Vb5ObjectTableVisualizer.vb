Imports System.Data
Imports SunFlower.Abstractions
Imports SunFlower.Abstractions.Types
Imports SunFlower.Vb.Headers

Namespace Visualizers
Public Class Vb5ObjectTableVisualizer
    Inherits AbstractStructVisualizer(Of Vb5ObjectTable)

    Public Sub New(struct As Vb5ObjectTable)
        MyBase.New(struct)
    End Sub
    
    Public Overrides Function ToDataTable() As DataTable
        Return FlowerReflection.GetNameValueTable(_struct)
    End Function

    Public Overrides Function ToString() As String
        Return """
`tOBJTABLE` has pointers to created classes modules
forms or controls used in project in run-time.
"""
    End Function

    Public Overrides Function ToRegion() As Region
        Return New Region("### Objects Table", ToString(), ToDataTable())
    End Function
End Class
End Namespace