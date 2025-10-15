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
                    
                    Dim projectInfo = New Vb5ProjectInfoManager(vbParam.ImageBase, header, reader, vbParam.Sections)
                    
                    Dim prj As Some = projectInfo.ProjectInfo
                    Dim objT As Some = projectInfo.ObjectTable
                    
                    Dim p = prj.Cast(Of Vb5ProjectInfo)()
                    Dim ot = objT.Cast(Of Vb5ObjectTable)()
                End Using
            End Using
            
            
            Assert.Pass()
        End Sub
    End Class
End Namespace