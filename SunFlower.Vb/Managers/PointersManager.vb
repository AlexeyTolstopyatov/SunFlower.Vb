Imports System.IO
Imports SunFlower.Vb.Headers

Namespace Managers
    Public Class PointersManager 
        Inherits UnsafeManager
        Private sections As List(Of PeSection)
        Private imageBase As Long
        
        Public Sub New(imageBase As Long, sections As List(Of PeSection))
            ' There's no any right methods to define runtime
            ' I specially say "f_ck it" and going to hell with only once
            ' knowing data field from PE32/+ "Optional" header
            Me.imageBase = imageBase
            Me.sections = sections
        End Sub
        
        Protected Function WrongOffset(ptr As Long) As Long 
            Dim weed = ptr - imageBase
            If weed < 0 Then 
                weed = imageBase - ptr
            End If
            
            Return weed
        End Function
        ''' <returns> Directory exists when someone of 2 parameters not 0 </returns>
        Protected Function IsDirectoryExists(dir As PeDirectory) As Boolean
            Return dir.Size <> 0 OrElse dir.VirtualAddress <> 0
        End Function

        ''' <param name="rva"> Required RVA </param>
        ''' <returns> File offset from RVA of selected section </returns>
        ''' <exception cref="SectionNotFoundException"> If RVA not belongs to any section </exception>
        Protected Function FindOffset(rva As Long) As Long
            Dim section = FindSection(rva)
            Return 0 + section.PointerToRawData + (rva - section.VirtualAddress)
        End Function

        ''' <param name="rva"> Required relative address </param>
        ''' <returns> <see cref="PeSection"/> Which RVA belongs </returns>
        ''' <exception cref="SectionNotFoundException"> If RVA not belongs to any section </exception>
        Private Function FindSection(rva As Long) As PeSection
            ' rva = {uint} 2019914798 
            ' rva = {long} 2019914798 
            ' RVA always 32-bit
            Dim rva32 As UInteger = Convert.ToUInt32(rva) ' instead casting
            For Each FindSection In sections.OrderBy(Function(s) s.VirtualAddress)
                If rva32 >= FindSection.VirtualAddress AndAlso rva32 < FindSection.VirtualAddress + FindSection.VirtualSize Then
                    Return FindSection
                End If
            Next
            Throw New InvalidDataException("Section not found!!!")
        End Function

        ''' <param name="reader"><see cref="BinaryReader"/> instance</param>
        ''' <param name="rva">RVA</param>
        ''' <param name="count">count of elements in segment</param>
        ''' <typeparam name="T">type of array-segment</typeparam>
        ''' <returns>Array of structures</returns>
        Protected Function ReadArray(Of T As Structure)(reader As BinaryReader, rva As UInteger, count As UInteger) As T()
            Dim offset = FindOffset(rva)
            reader.BaseStream.Seek(offset, SeekOrigin.Begin)
            Dim result(count - 1) As T
            For i As UInteger = 0 To count - 1
                result(i) = Fill(Of T)(reader)
            Next
            Return result
        End Function
    End Class
End Namespace
