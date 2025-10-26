Imports SunFlower.Vb.Services
Imports System.Collections.Generic
Imports SunFlower.Abstractions.Types

Namespace Visualizers
Public Class Vb5ObjectsIterator
    Public Shared Function Iterate(list As List(Of Vb5ObjectDetails)) As List(Of Region)
        Dim processed = New List(Of Region)()

        For Each item in list
            Dim v = new Vb5ObjectsVisualizer(item)

            processed.Add(v.TypeInfoToRegion())
            processed.Add(v.ObjectDescriptorsToRegion())
            processed.Add(v.ObjectInfoToRegion())
            processed.Add(v.PublicVariablesToRegion())
            processed.Add(v.StaticVariablesToRegion())
            processed.Add(v.MethodsToRegion())
            processed.Add(v.MethodsDataToRegion())
        Next
        
        Return processed
    End Function 
End Class
End Namespace