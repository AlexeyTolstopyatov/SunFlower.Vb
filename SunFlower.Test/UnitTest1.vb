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
            Dim path = "D:\VB3TOOLS\VBDIS3.67e_Reloaded_Rev3_DoDi_s_VB3Decompiler\VBGUARD\vb6\VBGUARD.exe"
            Dim common = New CommonDumpingService(path)
            
            Dim vbParamsOption As Some = common.Dump()
            
            Dim vbParam As Vb5ServiceParameters = vbParamsOption.Data
            
            Using stream = New FileStream(path, FileMode.Open, FileAccess.Read)
                Using reader = New BinaryReader(stream)
                    Dim entryPointManager = New VbEntryPointManager(reader, vbParam, vbParam.Sections)
                    Dim headerResult As Some = entryPointManager.Vb5Header
                    Dim header = headerResult.Cast(Of Vb5Header)()
                    
                    Dim projectInfo = New Vb5ProjectInfoManager(vbParam.ImageBase, header, reader, vbParam.Sections, entryPointManager.RuntimeHeaderOffset)
                    
                    Dim prj As Some = projectInfo.ProjectInfo
                    Dim objT As Some = projectInfo.ObjectTable
                    
                    Dim p = prj.Cast(Of Vb5ProjectInfo)()
                    Dim ot = objT.Cast(Of Vb5ObjectTable)()
                End Using
            End Using
            Assert.Pass()
        End Sub
        
        ''' Second idea: parse and load isolated process in memory
        ''' VA pointers are set in VB embedded data already points
        ''' to allocated space.
        <Test>
        Public Sub ProcessStructTest()
            Dim path = "D:\VB3TOOLS\VBDIS3.67e_Reloaded_Rev3_DoDi_s_VB3Decompiler\VBGUARD\vb6\VBGUARD.exe"
            Dim loader = New ImageLoadingService()
            
            Dim proc = ImageLoadingService.Init(path)
            loader.Load(proc.ProcessName)
            ' now we have
            '   sudden death
            '   bytes and 
            Using fs = New MemoryStream(loader.ProcessBytes, writable := False)
                Using reader = New BinaryReader(fs)
                    
                    
                    
                End Using
            End Using
            
            ImageLoadingService.Free(proc)
        End Sub
    End Class
End Namespace