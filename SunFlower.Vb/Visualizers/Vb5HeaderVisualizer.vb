Imports System.Data
Imports SunFlower.Abstractions
Imports SunFlower.Abstractions.Types
Imports SunFlower.Vb.Headers

Namespace Visualizers
    Public Class Vb5HeaderVisualizer
        Inherits AbstractStructVisualizer(Of Vb5Header)

        Public Sub New(struct As Vb5Header)
            MyBase.New(struct)
        End Sub
        
        Public Overrides Function ToDataTable() As DataTable
            Return FlowerReflection.GetNameValueTable(_struct)
        End Function

        Public Overrides Function ToString() As String
            Return """ 
Microsoft Visual Basic virtual machine embed itself
in compiled program/library. This is the first main file structure (starting point), it
contains links to other structures and also some information related to VB project (like user
specified project name and description). 
 - EntryPoint contains IA32 opcodes and VA address of this structure;
 - Generation code is located in `vb6.exe!WriteRubyExeData`;

"""
        End Function

        Public Overrides Function ToRegion() As Region
            Return New Region("## Visual Basic 5.0+ Header", ToString(), ToDataTable())
        End Function
    End Class
End Namespace