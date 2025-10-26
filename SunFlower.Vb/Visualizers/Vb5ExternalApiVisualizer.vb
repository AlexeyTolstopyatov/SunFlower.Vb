Imports System.Data
Imports SunFlower.Abstractions
Imports SunFlower.Abstractions.Types
Imports SunFlower.Vb.Services
Namespace Visualizers
Public Class Vb5ExternalApiVisualizer
    Inherits AbstractStructVisualizer(Of List(Of VbImportInfo))

    Public Sub New(struct As List(Of VbImportInfo))
        MyBase.New(struct)
    End Sub
    
    Public Overrides Function ToDataTable() As DataTable
        Return FlowerReflection.ListToDataTable(_struct)
    End Function

    Public Overrides Function ToString() As String
        Return """ 
Despite the fact that VB5/6 files have no PE import 
entries besides runtime library, any program
can freely use WinAPI functions it wants. 
This implemented using another internal structure.
```vb
Declare Sub ExitProcess(code As Long) Lib Kernel32.dll
```
"""
    End Function

    Public Overrides Function ToRegion() As Region
        Return New Region("### Declared External API", ToString(), ToDataTable())
    End Function
End Class
End Namespace
