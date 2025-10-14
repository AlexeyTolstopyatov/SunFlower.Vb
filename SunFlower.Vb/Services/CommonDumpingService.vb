Imports System.IO
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Managers

Namespace Services 
    Public Class Vb5ServiceParameters
        Sub New(imageBase As Long, codeBase As Long, dataBase As Long, sectionAlignment As Long, entryPoint As Long)
            Me.ImageBase = imageBase
            Me.CodeBase = codeBase
            Me.DataBase = dataBase
            Me.SectionAlignment = sectionAlignment
            Me.EntryPoint = entryPoint
        End Sub

        Public Property ImageBase as Long
        Public Property CodeBase As Long
        Public Property DataBase As Long
        Public Property SectionAlignment As Long
        Public Property EntryPoint As Long
    End Class
    
    Public Enum ServiceReturns 
        Ok = 0
        BadData = 1
        BadOperation = 2
    End Enum
    
    Public Class CommonDumpingService
        Inherits UnsafeManager
        Const MzHeaderMagic As UInt16 = &H5A4D
        Const ZmHeaderMagic As UInt16 = &H4D5A
        Const MzELfaNewPtr As UInt16 = &H3C
        Const CoffMagic As UInt16 = &H4550
        Const Pe32Magic As UInt16 = &H10B
        
        Private ReadOnly _path As String = String.Empty
        Public Property VbParameters As Vb5ServiceParameters
        
        Public Sub New(path As String)
            _path = path
        End Sub
        
        Public Function Dump As ServiceReturns
            Using stream = New FileStream(path := _path, mode := FileMode.Open, access := FileAccess.Read) 
                Using reader = new BinaryReader(stream)
                    Dim signature = reader.ReadUInt16()
                    If signature <> ZmHeaderMagic And signature <> MzHeaderMagic Then
                        reader.Close()
                        Return ServiceReturns.BadData
                    End If
                    
                    stream.Position = MzELfaNewPtr
                    
                    Dim offset = reader.ReadUInt32()
                    stream.Position = offset
                    
                    ' deserialize COFF / Optional PE32 header
                    Dim coffHeader = reader.ReadUInt32()
                    If coffHeader <> CoffMagic Then
                        reader.Close()
                        Return ServiceReturns.BadData
                    End If
                    
                    Dim pe = Fill(Of PeFileHeader)(reader)
                    Dim pe32 = Fill(Of PeOptionalHeader)(reader)
                    
                    ' Save changes and exit
                    VbParameters = New Vb5ServiceParameters(
                        imageBase := pe32.ImageBase, _
                        codeBase := pe32.BaseOfCode, _
                        dataBase := pe32.BaseOfData, _
                        sectionAlignment := pe32.SectionAlignment,
                        entryPoint := pe32.EntryPointRva
                    )
                    
                    reader.Close()
                    Return ServiceReturns.Ok
                End Using
            End Using
        End Function
    End Class
End Namespace