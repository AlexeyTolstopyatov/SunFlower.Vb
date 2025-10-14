Imports System.Data
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports SunFlower.Abstractions

Namespace Managers
    Public Class TableManager
        Private Function GetSafeSize(fieldType As Type) As Integer
            Select Case Type.GetTypeCode(fieldType)
                Case TypeCode.Byte, TypeCode.SByte : Return 1
                Case TypeCode.Int16, TypeCode.UInt16 : Return 2
                Case TypeCode.Int32, TypeCode.UInt32, TypeCode.Single : Return 4
                Case TypeCode.Int64, TypeCode.UInt64, TypeCode.Double : Return 8
                Case Else
                    If fieldType.IsArray Then
                        Dim attr = fieldType.GetCustomAttribute(Of MarshalAsAttribute)()
                        If attr IsNot Nothing AndAlso attr.SizeConst > 0 Then
                            Return attr.SizeConst
                        End If
                    End If
                    Return 0
            End Select
        End Function
        
        ''' Reinterprets any struct type 
        Public Function ToDataTable(Of TStruct) ([struct] As TStruct) As DataTable
            Dim table As DataTable = New DataTable()
            table.Columns.Add("Offset")
            table.Columns.Add("Name")
            table.Columns.Add("Value")
            
            ' Make it like a SunFlower datatypes support
            Dim currentOffset As Long = 0
            Dim structDetails = [struct].GetType().GetFields(
                BindingFlags.Instance Or BindingFlags.Public
            )
            
            For Each field In structDetails
                ' Explicit (or transparent) reinterpretation of unmanaged type by value
                Dim size = GetSafeSize(field.FieldType)
                Dim row = table.NewRow()
                row("Offset") = currentOffset
                row("Name") = FlowerReport.ForColumn(field.Name, field.FieldType)
                ' row("Value") = (value, field.FieldType)
                
                table.Rows.Add(row)
                currentOffset += size
            Next
            
            Return table
        End Function
    End Class
End NameSpace