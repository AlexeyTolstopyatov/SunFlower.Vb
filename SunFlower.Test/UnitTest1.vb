Imports System.IO
Imports NUnit.Framework
Imports Microsoft.VisualBasic
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
            Dim path = "C:\Program Files (x86)\Semi VB Decompiler\SemiVBDecompiler.EXE"
            Dim common = New CommonDumpingService(path)
            
            Dim vbParamsOption As Some = common.Dump()
            
            Dim vbParam As Vb5ServiceParameters = vbParamsOption.Data
            
            Using stream = New FileStream(path, FileMode.Open, FileAccess.Read)
                Using reader = New BinaryReader(stream)
                    Dim entryPointManager = New VbEntryPointManager(reader, vbParam, vbParam.Sections)
                    Dim headerResult As Some = entryPointManager.Vb5Header
                    Dim header = headerResult.Cast(Of Vb5Header)()
                    
                    Dim projectInfo = New Vb5ProjectParser(vbParam.ImageBase, header, reader, vbParam.Sections, entryPointManager.RuntimeHeaderOffset)
                    
                    Dim objOption = DirectCast(projectInfo.ObjectTable, Some)
                    Dim obj = DirectCast(objOption.Data, Vb5ObjectTable)

                    'Dim impParser = New VbImportParser(reader, vbParam.Sections, vbParam.ImageBase)
                    'Dim imps = impParser.ParseImports(info.ExternalTablePointer, info.ExternalTableCount)
                    
                    Dim objParser = New Vb5ObjectsParser(reader, vbParam.Sections, vbParam.ImageBase)
                    Dim objs = objParser.ParseObjects(obj.ObjectsArrayPointer, obj.TotalObjectsCount)
                    
                    objs.Clear()
                End Using
            End Using
            Assert.Pass()
        End Sub
        <Test(Author := "CoffeeLake")>
        Public Sub ModuleImportsTest
            Dim path = "C:\Program Files (x86)\Semi VB Decompiler\SemiVBDecompiler.EXE"
            ' Dim path = "D:\VB3TOOLS\VBDIS3.67e_Reloaded_Rev3_DoDi_s_VB3Decompiler\VBDIS3.67e\VBDIS3.exe"
            Dim common = New CommonDumpingService(path)
            
            Dim vbParamsOption As Some = common.Dump()
            
            Dim vbParam As Vb5ServiceParameters = vbParamsOption.Data
            
            Using stream = New FileStream(path, FileMode.Open, FileAccess.Read)
                Using reader = New BinaryReader(stream)
                    Dim entryPointManager = New VbEntryPointManager(reader, vbParam, vbParam.Sections)
                    Dim headerResult As Some = entryPointManager.Vb5Header
                    Dim header = headerResult.Cast(Of Vb5Header)()
                    
                    Dim projectInfo = New Vb5ProjectParser(vbParam.ImageBase, header, reader, vbParam.Sections, entryPointManager.RuntimeHeaderOffset)
                    Dim infoOption = DirectCast(projectInfo.ProjectInfo, Some)
                    Dim info = DirectCast(infoOption.Data, Vb5ProjectInfo)
                    Dim objOption = DirectCast(projectInfo.ObjectTable, Some)
                    Dim obj = DirectCast(objOption.Data, Vb5ObjectTable)
                    Dim impParser = New VbImportParser(reader, vbParam.Sections, vbParam.ImageBase)
                    Dim imps = impParser.ParseImports(info.ExternalTablePointer, info.ExternalTableCount)
                    
                    Dim objParser = New Vb5ObjectsParser(reader, vbParam.Sections, vbParam.ImageBase)
                    Dim objs = objParser.ParseObjects(obj.ObjectsArrayPointer, obj.TotalObjectsCount)
                    
                    objs.Clear()
                End Using
            End Using
            Assert.Pass()
        End Sub
    End Class
End Namespace