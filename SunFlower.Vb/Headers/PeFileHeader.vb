Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure PeFileHeader
        Public Machine As UInt16
        Public NumberOfSections As UInt16
        Public TimeDataStamp As UInt32
        Public PointerToSymbolTable As UInt32
        Public NumberOfSymbols As UInt32
        Public SizeOfOptionalHeader As UInt16
        Public Characteristics As UInt16
    End Structure    
    
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure PeOptionalHeader
        Public Magic As UInt16
        Public MajorLinkerVersion As Byte
        Public MinorLinkerVersion As Byte
        Public SizeOfCode As UInt32
        Public SizeOfInit As UInt32
        Public SizeOfUnInit As UInt32
        Public EntryPointRva As UInt32
        Public BaseOfCode As UInt32
        Public BaseOfData As UInt32
        Public ImageBase As UInt32
        Public SectionAlignment As UInt32
        Public FileAlignment As UInt32
        Public MajorOsVersion As UInt16
        Public MinorOsVersion As UInt16
        Public MajorImageVersion As UInt16
        Public MinorImageVersion As UInt16
        Public MajorSubSysVersion As UInt16
        Public MinorSubSysVersion As UInt16
        Public Win32Value As UInt32
        Public Checksum As UInt32
        Public SubSystem As UInt16
        Public DllCharacteristics As UInt16
        Public SizeOfStackReserve As UInt32
        Public SizeOfStackCommit As UInt32
        Public SizeOfHeapReserve As UInt32
        Public SizeOfHeapCommit As UInt32
        Public LoaderFlags As UInt32
        <MarshalAs(UnmanagedType.ByValArray, SizeConst := 16)>
        Public Directories() As PeDirectory
    End Structure
End Namespace
