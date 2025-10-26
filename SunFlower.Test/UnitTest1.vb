Imports System.IO
Imports NUnit.Framework
Imports Microsoft.VisualBasic
Imports SunFlower.Vb
Imports SunFlower.Vb.Handlers
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Services


Namespace SunFlower.Test
    Public Class Tests
        <SetUp>
        Public Sub Setup()
        End Sub
        ''' Failed. Half of pointers are points to non-allocated space
        <Test(Author := "CoffeeLake", Description := "Checks internal nested structures of Objects and procedures")>
        Public Sub ModuleStructTest()
            Dim path = "D:\VB3TOOLS\VBDIS3.67e_Reloaded_Rev3_DoDi_s_VB3Decompiler\VBDIS3.67e\VBDIS3.exe"
            ' Dim path = "C:\Program Files (x86)\Semi VB Decompiler\SemiVBDecompiler.EXE"
            Dim common = New CommonDumpingService(path)
            
            Dim vbParamsOption As Some = common.Dump()
            
            Dim vbParam As Vb5ServiceParameters = vbParamsOption.Data
            
            Using stream = New FileStream(path, FileMode.Open, FileAccess.Read)
                Using reader = New BinaryReader(stream)
                    Dim entryPointManager = New VbEntryPointManager(reader, vbParam, vbParam.Sections)
                    Dim headerResult As Some = entryPointManager.Vb5Header
                    Dim header = headerResult.Cast(Of Vb5Header)()
                    Dim proj = New Vb5ProjectParser(
                        vbParam.ImageBase,
                        header,
                        reader,
                        vbParam.Sections,
                        entryPointManager.RuntimeHeaderOffset
                    )
                    Dim objOption = DirectCast(proj.ObjectTable, Some)
                    Dim objTable = DirectCast(objOption.Data, Vb5ObjectTable)
                    
                    Dim comParser = New Vb5ComRegistrationParser(vbParam.ImageBase, header, reader, vbParam.Sections)
                    Dim comData = comParser.ParseComRegistrationData()
                    
                    Dim objParser = New Vb5ObjectsParser(reader, vbParam.Sections, vbParam.ImageBase)
                    Dim obj = objParser.ParseObjects(objTable.ObjectsArrayPointer, objTable.TotalObjectsCount)
                    
                    comData.GetType()
                End Using
            End Using
            Assert.Pass()
        End Sub
        
        <Test>
        Public Sub EntryPointTest() 
            Dim path = "D:\VB3TOOLS\VBDIS3.67e_Reloaded_Rev3_DoDi_s_VB3Decompiler\VBDIS3.67e\VBDIS3.exe"
            Dim entry = New VisualBasicClassicSeed()
            entry.Main(path)
        End Sub
    End Class
End Namespace