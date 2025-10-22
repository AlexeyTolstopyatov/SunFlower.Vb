Imports System.IO
Imports SunFlower.Vb.Handlers
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Managers

Namespace Services 
    Public Class Vb5ServiceParameters
        Sub New(imageBase As Long, codeBase As Long, dataBase As Long, sectionAlignment As Long, entryPoint As Long, sections As List(Of PeSection))
            Me.ImageBase = imageBase
            Me.CodeBase = codeBase
            Me.DataBase = dataBase
            Me.SectionAlignment = sectionAlignment
            Me.EntryPoint = entryPoint
            Me.Sections = sections
        End Sub
        Public Property Sections As List(Of PeSection)
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
        Private Property VbParameters As Vb5ServiceParameters
        
        Public Sub New(path As String)
            _path = path
        End Sub
        ''' Returns Some(VBParameters) structure if all ok
        Public Function Dump As [Option]
            Using stream = New FileStream(path := _path, mode := FileMode.Open, access := FileAccess.Read) 
                Using reader = new BinaryReader(stream)
                    reader.BaseStream.Position = 0
                    Dim mz = Fill(Of MzHeader)(reader)
                    
                    If mz.Magic <> MzHeaderMagic Then
                        If mz.Magic <> ZmHeaderMagic Then
                            reader.Close()
                            Return New None(ServiceReturns.BadData, "MZ signature not found")
                        End If
                    End If
                    
                    ' deserialize COFF / Optional PE32 header
                    stream.Position = mz.Offset
                    Dim coffHeader = reader.ReadUInt32()
                    If coffHeader <> CoffMagic Then
                        reader.Close()
                        Return New None(ServiceReturns.BadData, "PE signature not found")
                    End If
                    
                    Dim pe = Fill(Of PeFileHeader)(reader)
                    Dim pe32 = Fill(Of PeOptionalHeader)(reader)
                    Dim sections = New List(Of PeSection)

                    For i = 1 to pe.NumberOfSections Step 1
                        sections.Add(Fill(Of PeSection)(reader))
                    Next
                    
                    
                    ' Save changes and exit
                    VbParameters = New Vb5ServiceParameters(
                        imageBase := pe32.ImageBase, _
                        codeBase := pe32.BaseOfCode, _
                        dataBase := pe32.BaseOfData, _
                        sectionAlignment := pe32.SectionAlignment, _
                        entryPoint := pe32.AddressOfEntryPoint, _
                        sections := sections _
                    )
                    
                    reader.Close()
                    Return New Some(VbParameters)
                End Using
            End Using
        End Function
    End Class
End Namespace