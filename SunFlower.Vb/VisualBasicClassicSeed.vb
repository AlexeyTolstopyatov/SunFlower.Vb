Imports System.IO
Imports SunFlower.Abstractions
Imports SunFlower.Abstractions.Types
Imports SunFlower.Vb.Handlers
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Services
Imports SunFlower.Vb.Visualizers

<FlowerSeedContract(4, 0,0)>
Public Class VisualBasicClassicSeed 
    Implements IFlowerSeed
    
    Public ReadOnly Property Seed As String = "Visual Basic 5.0+ Runtime walker" _ 
        Implements IFlowerSeed.Seed
    Public ReadOnly Property Status As FlowerSeedStatus =
        New FlowerSeedStatus() _ 
        Implements IFlowerSeed.Status
    
    Public Function Main(path As String) As Integer Implements IFlowerSeed.Main
        Try
            Status.Results = New List(Of FlowerSeedResult)()
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
                    Dim projInfoOption = DirectCast(proj.ProjectInfo, Some)
                    Dim projInfo = DirectCast(projInfoOption.Data, Vb5ProjectInfo)
                    Dim objOption = DirectCast(proj.ObjectTable, Some)
                    Dim objTable = DirectCast(objOption.Data, Vb5ObjectTable)
                    
                    Dim comParser = New Vb5ComRegistrationParser(vbParam.ImageBase, header, reader, vbParam.Sections)
                    Dim comData = comParser.ParseComRegistrationData()
                    
                    Dim objParser = New Vb5ObjectsParser(reader, vbParam.Sections, vbParam.ImageBase)
                    Dim obj = objParser.ParseObjects(objTable.ObjectsArrayPointer, objTable.TotalObjectsCount)
                    
                    Dim impParser = New Vb5ImportsParser(reader, vbParam.Sections, vbParam.ImageBase)
                    Dim imps = impParser.ParseImports(projInfo.ExternalTablePointer, projInfo.ExternalTableCount)
                    
                    Dim headv = New Vb5HeaderVisualizer(header).ToRegion()
                    Dim projv = New Vb5ProjectInfoVisualizer(projInfo).ToRegion()
                    Dim imptv = New Vb5ExternalApiVisualizer(imps).ToRegion()
                    Dim objsv = Vb5ObjectsIterator.Iterate(obj)
                    
                    Dim reg = New List(Of Region)()
                    reg.Add(headv)
                    reg.Add(projv)
                    reg.Add(imptv)
                    
                    Status.Results.Add(New FlowerSeedResult(FlowerSeedEntryType.Regions) With {
                        .BoxedResult = reg                      
                    })
                    Status.Results.Add(New FlowerSeedResult(FlowerSeedEntryType.Regions) With {
                        .BoxedResult = objsv
                    })
                    Status.IsEnabled = True
                    reader.Close()
                End Using
            End Using
            
        Catch e As Exception
            Status.LastError = e
        End Try
        
        Return 1 'https://t.me/overfriends/1087
    End Function
End Class
