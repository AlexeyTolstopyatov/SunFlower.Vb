Imports System.IO
Imports NUnit.Framework
Imports SunFlower.Vb.Handlers
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Services

Namespace SunFlower.Test
    Public Class Tests
        <SetUp>
        Public Sub Setup()
        End Sub

        ''' Failed. Half of pointers are points to non-allocated space
        <Test>
        Public Sub ModuleStructTest()
            Dim path = "D:\VB3TOOLS\VBDIS3.67e_Reloaded_Rev3_DoDi_s_VB3Decompiler\VBDIS3.67e\VBDIS3.exe"
            Dim common = New CommonDumpingService(path)
            
            Dim vbParamsOption As Some = common.Dump()
            
            Dim vbParam As Vb5ServiceParameters = vbParamsOption.Data
            
            Using stream = New FileStream(path, FileMode.Open, FileAccess.Read)
                Using reader = New BinaryReader(stream)
                    Dim entryPointManager = New VbEntryPointManager(reader, vbParam, vbParam.Sections)
                    Dim headerResult As Some = entryPointManager.Vb5Header
                    Dim header = headerResult.Cast(Of Vb5Header)()
                    
                    Dim projectInfo = New Vb5ProjectParser(vbParam.ImageBase, header, reader, vbParam.Sections, entryPointManager.RuntimeHeaderOffset)
                    
                    Dim prj As Some = projectInfo.ProjectInfo
                    Dim objT As Some = projectInfo.ObjectTable
                    
                    Dim p = prj.Cast(Of Vb5ProjectInfo)()
                    Dim ot = objT.Cast(Of Vb5ObjectTable)()
                End Using
            End Using
            Assert.Pass()
        End Sub
        <Test>
        Public Sub ModuleImportsTest
            ' Dim path = "C:\Program Files (x86)\Semi VB Decompiler\SemiVBDecompiler.EXE"
            Dim path = "D:\VB3TOOLS\VBDIS3.67e_Reloaded_Rev3_DoDi_s_VB3Decompiler\VBDIS3.67e\VBDIS3.exe"
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
                    
                    Dim objParser = New Vb5ObjectsParser(obj, reader, vbParam.Sections, vbParam.ImageBase)
                    Dim objs = objParser.ParseObjects()
                    
                    objs.Clear()
                    imps.Clear()
                End Using
            End Using
            Assert.Pass()
        End Sub
    End Class
End Namespace