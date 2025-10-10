Imports SunFlower.Abstractions

<FlowerSeedContract(3, 0,0)>
Public Class VisualBasicClassicSeed 
    Implements IFlowerSeed
    
    Public ReadOnly Property Seed As String = "MSVBVM.dll 5.0+ Runtime walker" _ 
        Implements IFlowerSeed.Seed
    Public ReadOnly Property Status As FlowerSeedStatus =
        New FlowerSeedStatus() _ 
        Implements IFlowerSeed.Status
    
    Public Function Main(path As String) As Integer Implements IFlowerSeed.Main
         
        Return -1
    End Function
End Class
