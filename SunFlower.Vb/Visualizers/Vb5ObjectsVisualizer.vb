Imports System.Data
Imports SunFlower.Abstractions
Imports SunFlower.Abstractions.Types
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Services

Namespace Visualizers
Public Class Vb5ObjectsVisualizer
    Inherits AbstractStructVisualizer(Of Vb5ObjectDetails)

    Public Sub New(struct As Vb5ObjectDetails)
        MyBase.New(struct)
    End Sub

    Public Function ObjectDescriptorsToRegion() As Region
        Return New Region( _
            $"## {_struct.ObjectName} | Descriptor", _
            "Heading structure represents object names `PublicObjectDescriptor`", _
            FlowerReflection.GetNameValueTable(_struct.Descriptor))
    End Function
    
    Public Function ObjectInfoToRegion() As Region
        Return New Region(
            $"## {_struct.ObjectName} | Information",
                "Second structure following next of current object",
            FlowerReflection.GetNameValueTable(_struct.ObjectInfo)
        )
    End Function
    
    Public Function PublicVariablesToRegion As Region
        Return New Region(
            $"### {_struct.ObjectName} | Public variables",
            "Types iterated and placed here are public-declared",
            FlowerReflection.ListToDataTable(_struct.PublicVariables)
        )
    End Function
    
    Public Function StaticVariablesToRegion As Region
        Return New Region(
            $"### {_struct.ObjectName} | Public variables",
            "",
            FlowerReflection.ListToDataTable(_struct.StaticVariables)
            )
    End Function
    
    Public Function MethodsToRegion As Region
        Dim methodData As List(Of String)
        If _struct.MethodNames Is Nothing Then
            methodData = New List(Of String)()
        Else 
            methodData = _struct.MethodNames
        End If
        
        Dim dt = New DataTable()
        
        dt.Columns.Add("`Object:sz`")
        dt.Columns.Add("`Public method:sz`")
        
        For Each i In methodData
            dt.Rows.Add(_struct.ObjectName, i)
        Next
        
        Return New Region(
            $"### {_struct.ObjectName} | Methods Declarations",
            "Types iterated and placed here are public-declared",
            dt
            )
    End Function
    
    Public Function MethodsDataToRegion As Region
        Dim methodData As List(Of Vb5Method)
        If _struct.Methods Is Nothing Then
            methodData = New List(Of Vb5Method)()
        Else 
            methodData = _struct.Methods.Select(Function(t) t.Method).ToList()
        End If
        
        Return New Region(
            $"### {_struct.ObjectName} | Private Methods data",
            "Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space",
            FlowerReflection.ListToDataTable(methodData)
            )
    End Function
    
    Public Function TypeInfoToRegion () As Region
        Return New Region(
            $"# {_struct.ObjectName} | Type Declaration",
            "Type info is custom structure what describes current object",
            FlowerReflection.GetNameValueTable(_struct.TypeInfo)
            )
    End Function
    
    Public Overrides Function ToDataTable() As DataTable
        Throw New NotImplementedException
    End Function

    Public Overrides Function ToRegion() As Region
        Throw New NotImplementedException
    End Function
End Class
End Namespace