Imports System.Runtime.InteropServices

Namespace Headers
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure PeFileHeader
	    ' Token: 0x0400009F RID: 159
	    Public Machine As UShort

	    ' Token: 0x040000A0 RID: 160
	    Public NumberOfSections As UShort

	    ' Token: 0x040000A1 RID: 161
	    Public TimeDateStamp As UInteger

	    ' Token: 0x040000A2 RID: 162
	    Public PointerToSymbolTable As UInteger

	    ' Token: 0x040000A3 RID: 163
	    Public NumberOfSymbols As UInteger

	    ' Token: 0x040000A4 RID: 164
	    Public SizeOfOptionalHeader As UShort

	    ' Token: 0x040000A5 RID: 165
	    Public Characteristics As UShort
    End Structure    
    
    <StructLayout(LayoutKind.Sequential, Pack := 1)>
    Public Structure PeOptionalHeader
        ' Token: 0x040000C5 RID: 197
		Public Magic As UShort

		' Token: 0x040000C6 RID: 198
		Public MajorLinkerVersion As Byte

		' Token: 0x040000C7 RID: 199
		Public MinorLinkerVersion As Byte

		' Token: 0x040000C8 RID: 200
		Public SizeOfCode As UInteger

		' Token: 0x040000C9 RID: 201
		Public SizeOfInitializedData As UInteger

		' Token: 0x040000CA RID: 202
		Public SizeOfUninitializedData As UInteger

		' Token: 0x040000CB RID: 203
		Public AddressOfEntryPoint As UInteger

		' Token: 0x040000CC RID: 204
		Public BaseOfCode As UInteger

		' Token: 0x040000CD RID: 205
		Public BaseOfData As UInteger

		' Token: 0x040000CE RID: 206
		Public ImageBase As UInteger

		' Token: 0x040000CF RID: 207
		Public SectionAlignment As UInteger

		' Token: 0x040000D0 RID: 208
		Public FileAlignment As UInteger

		' Token: 0x040000D1 RID: 209
		Public MajorOperatingSystemVersion As UShort

		' Token: 0x040000D2 RID: 210
		Public MinorOperatingSystemVersion As UShort

		' Token: 0x040000D3 RID: 211
		Public MajorImageVersion As UShort

		' Token: 0x040000D4 RID: 212
		Public MinorImageVersion As UShort

		' Token: 0x040000D5 RID: 213
		Public MajorSubsystemVersion As UShort

		' Token: 0x040000D6 RID: 214
		Public MinorSubsystemVersion As UShort

		' Token: 0x040000D7 RID: 215
		Public Win32VersionValue As UInteger

		' Token: 0x040000D8 RID: 216
		Public SizeOfImage As UInteger

		' Token: 0x040000D9 RID: 217
		Public SizeOfHeaders As UInteger

		' Token: 0x040000DA RID: 218
		Public CheckSum As UInteger

		' Token: 0x040000DB RID: 219
		Public Subsystem As UShort

		' Token: 0x040000DC RID: 220
		Public DllCharacteristics As UShort

		' Token: 0x040000DD RID: 221
		Public SizeOfStackReserve As UInteger

		' Token: 0x040000DE RID: 222
		Public SizeOfStackCommit As UInteger

		' Token: 0x040000DF RID: 223
		Public SizeOfHeapReserve As UInteger

		' Token: 0x040000E0 RID: 224
		Public SizeOfHeapCommit As UInteger

		' Token: 0x040000E1 RID: 225
		Public LoaderFlags As UInteger

		' Token: 0x040000E2 RID: 226
		Public NumberOfRvaAndSizes As UInteger

		' Token: 0x040000E3 RID: 227
		<MarshalAs(UnmanagedType.ByValArray, SizeConst := 16)>
		Public Directories As PeDirectory()
    End Structure
	<StructLayout(LayoutKind.Sequential, Pack := 1)>
	Public Structure PeSection
		' Token: 0x040000E9 RID: 233
		<MarshalAs(UnmanagedType.ByValArray, SizeConst := 8)>
		Public Name As Char()

		' Token: 0x040000EA RID: 234
		Public VirtualSize As UInteger

		' Token: 0x040000EB RID: 235
		Public VirtualAddress As UInteger

		' Token: 0x040000EC RID: 236
		Public SizeOfRawData As UInteger

		' Token: 0x040000ED RID: 237
		Public PointerToRawData As UInteger

		' Token: 0x040000EE RID: 238
		Public PointerToRelocations As UInteger

		' Token: 0x040000EF RID: 239
		Public PointerToLinenumbers As UInteger

		' Token: 0x040000F0 RID: 240
		Public NumberOfRelocations As UShort

		' Token: 0x040000F1 RID: 241
		Public NumberOfLinenumbers As UShort

		' Token: 0x040000F2 RID: 242
		Public Characteristics As UInteger
	End Structure
End Namespace
