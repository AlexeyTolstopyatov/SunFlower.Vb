# Sunflower Report
**Generated at**: 2025-12-17 22:51:04


Visual Basic 5.0+ Runtime walker


## Visual Basic 5.0+ Header


Microsoft Visual Basic virtual machine embed itself
in compiled program/library. This is the first main file structure (starting point), it
contains links to other structures and also some information related to VB project (like user
specified project name and description). 
 - EntryPoint contains IA32 opcodes and VA address of this structure;
 - Generation code is located in `vb6.exe!WriteRubyExeData`;


| Name                           | Value                           |
|--------------------------------|---------------------------------|
| `VbMagic:sz`                   | `VB5!`                          |
| `RuntimeBuild:2`               | 0x2636                          |
| `LanguageDll:sz`               | `*%0%0%0%0%0%0%0%0%0%0%0%0%0`   |
| `SecondLanguageDll:sz`         | `~%0%0%0%0%0%0%0%0%0%0%0%0%0`   |
| `RuntimeRevision:2`            | 0x000A                          |
| `LanguageId:4`                 | 0x00000409                      |
| `SecondLanguageId:4`           | 0x00000000                      |
| `SubMainAddress:4`             | 0x00000000                      |
| `ProjectDataPointer:4`         | 0x0041E9A0                      |
| `ControlsFlagLow:4`            | 0x01B8F1FF                      |
| `ControlsFlagHigh:4`           | 0xFFFFFF00                      |
| `ThreadFlags:4`                | 0x00000008                      |
| `ThreadCount:4`                | 0x00000001                      |
| `FormCtlsCount:2`              | 0x0009                          |
| `ExternalCtlsCount:2`          | 0x0009                          |
| `ThunkCount:4`                 | 0x000000E9                      |
| `GuiTablePointer:4`            | 0x0041EBDC                      |
| `ExternalTablePointer:4`       | 0x00423790                      |
| `ComRegisterDataPointer:4`     | 0x00405D18                      |
| `ProjectDescriptionOffset:4`   | 0x00000078                      |
| `ProjectExeNameOffset:4`       | 0x00000089                      |
| `ProjectHelpOffset:4`          | 0x0000009C                      |
| `ProjectNameOffset:4`          | 0x0000009D                      |



### COM | General


Com registration info result contains `LPCSTR`s and resolved
long pointers are:
 - Project Name: `VBDecompiler`
 - Project Description: `Visual Basic Decompiler`
 - Help directory: ``

Error message: 
Processed successfully: True





## COM | Registration Data

Heading structure of next registry records chain. 
If offset of COM registry info structure equals zero - next record is invalid and not shows
| Name                           | Value                   |
|--------------------------------|-------------------------|
| `RegInfoOffset:4`              | 0x00000000              |
| `ProjectNameOffset:4`          | 0x00000030              |
| `HelpDirectoryOffset:4`        | 0x00000058              |
| `ProjectDescriptionOffset:4`   | 0x00000040              |
| `UuidProjectClsId:ps`          | `?????C%x1DE??H?+L??`   |
| `TlbLcid:4`                    | 0x00000000              |
| `Unknown:2`                    | 0x0000                  |
| `TlbVerMajor:2`                | 0x0001                  |
| `TlbVerMinor:2`                | 0x0000                  |



### Project Info


`tPROJINFO` or ProjectInfo structure has nested
complex types. All pointers in structure are VA.
 - Fills by `vba6.dll!0x0FB11783`;
 - Application is N-Code compiled; 


| Name                       | Value             |
|----------------------------|-------------------|
| `Version:4`                | 0x000001F4        |
| `ObjectTablePointer:4`     | 0x00426560        |
| `Null:4`                   | 0x00000000        |
| `CodeStartsPointer:4`      | 0x0044DCA0        |
| `CodeEndsPointer:4`        | 0x005006F0        |
| `DataSize:4`               | 0x000062D8        |
| `ThreadSpacePointer:4`     | 0x00502008        |
| `VbaSEHPointer:4`          | 0x004057F6        |
| `NativeCodePointer:4`      | 0x00502000        |
| `PrimitivePath:ps`         | `%0%0%0%0%0%0`    |
| `ProjectPath:?`            | System.UInt16[]   |
| `ExternalTablePointer:4`   | 0x0041E20C        |
| `ExternalTableCount:4`     | 0x0000001F        |



### Declared External API


Despite the fact that VB5/6 files have no PE import 
entries besides runtime library, any program
can freely use WinAPI functions it wants. 
This implemented using another internal structure.

```vb
Public Declare Function GetSystemMenu Lib "user32"
```

| ImportType   | LibraryName      | FunctionName                             | RawPointer   | #    |
|--------------|----------------|----------------------------------------|------------|------|
| 0x00000007   | `olly.dll`       | `Disasm`                                 | 0x000415D4   | 0    |
| 0x00000006   | `[GUID]`         | `fcfb3d23-a0fa-1068-a738-08002b3371b5`   | 0x0042FDDC   | 1    |
| 0x00000007   | `advapi32.dll`   | `RegQueryValueExA`                       | 0x0002DBA4   | 2    |
| 0x00000007   | `advapi32.dll`   | `RegOpenKeyA`                            | 0x0002DB70   | 3    |
| 0x00000007   | `advapi32.dll`   | `RegCloseKey`                            | 0x0002DB2C   | 4    |
| 0x00000007   | `kernel32`       | `LoadLibraryA`                           | 0x0002A3C0   | 5    |
| 0x00000007   | `advapi32`       | `RegCloseKey`                            | 0x00029AF0   | 6    |
| 0x00000007   | `advapi32`       | `RegQueryValueExA`                       | 0x00029AAC   | 7    |
| 0x00000007   | `advapi32`       | `RegOpenKeyExA`                          | 0x00029A60   | 8    |
| 0x00000007   | `oleaut32`       | `SysFreeString`                          | 0x000291FC   | 9    |
| 0x00000007   | `oleaut32`       | `LoadTypeLib`                            | 0x000291B4   | 10   |
| 0x00000007   | `ole32`          | `StringFromGUID2`                        | 0x00029180   | 11   |
| 0x00000007   | `ole32.dll`      | `ProgIDFromCLSID`                        | 0x0002912C   | 12   |
| 0x00000007   | `kernel32`       | `RtlFillMemory`                          | 0x000290D8   | 13   |
| 0x00000007   | `user32`         | `CallWindowProcA`                        | 0x00029090   | 14   |
| 0x00000007   | `oleaut32`       | `VariantChangeType`                      | 0x0002903C   | 15   |
| 0x00000007   | `kernel32`       | `IsBadReadPtr`                           | 0x00028FE0   | 16   |
| 0x00000007   | `ole32.dll`      | `CLSIDFromString`                        | 0x00028E64   | 17   |
| 0x00000007   | `oleaut32.dll`   | `RegisterTypeLib`                        | 0x00028E1C   | 18   |
| 0x00000007   | `oleaut32.dll`   | `LoadTypeLib`                            | 0x00028DD4   | 19   |
| 0x00000007   | `shell32.dll`    | `ShellExecuteA`                          | 0x0002837C   | 20   |
| 0x00000007   | `kernel32`       | `lstrlenA`                               | 0x00028324   | 21   |
| 0x00000007   | `kernel32`       | `lstrcpyA`                               | 0x000282E0   | 22   |
| 0x00000007   | `kernel32`       | `RtlMoveMemory`                          | 0x000282AC   | 23   |
| 0x00000007   | `Version.dll`    | `VerQueryValueA`                         | 0x00028254   | 24   |
| 0x00000007   | `Version.dll`    | `GetFileVersionInfoSizeA`                | 0x0002820C   | 25   |
| 0x00000007   | `Version.dll`    | `GetFileVersionInfoA`                    | 0x000281BC   | 26   |
| 0x00000007   | `comdlg32.dll`   | `GetFileTitleA`                          | 0x00027E3C   | 27   |
| 0x00000007   | `shell32`        | `SHGetPathFromIDList`                    | 0x00027DE0   | 28   |
| 0x00000007   | `shell32`        | `SHBrowseForFolder`                      | 0x00027D88   | 29   |
| 0x00000007   | `ole32.dll`      | `CoTaskMemFree`                          | 0x00027D30   | 30   |





# frmMain | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018083   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## frmMain | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x004246D8   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00427F24   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426EDC   |
| `MethodCount:4`           | 0x00000041   |
| `MethodNamesPointer:4`    | 0x00426B84   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018083   |
| `Null:4`                  | 0x00000000   |



## frmMain | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0000       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D5C4   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x004265B4   |
| `ProjectDataPointer:4`     | 0x00502008   |
| `MethodCount:2`            | 0x0004       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x00424764   |
| `ConstantsCount:2`         | 0x0005       |
| `MaxConstants:2`           | 0x0020       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x038B5794   |
| `ConstantsPointer:4`       | 0x00424750   |



### frmMain | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0016       | 0x0148   | 0x0000   | 0x0001   | 0   |



### frmMain | Public variables





### frmMain | Methods Declarations

Types iterated and placed here are public-declared
| `Object:sz`   | `Public method:sz`   |
|---------------|----------------------|
| frmMain       | OpenVBExe            |
| frmMain       | AddToRecentList      |
| frmMain       | MakeDir              |
| frmMain       | GetStdPicture        |
| frmMain       | ProccessControls     |
| frmMain       | ColorWord            |
| frmMain       | GetControlSize       |
| frmMain       | GetFontProperty      |
| frmMain       | LoadLanguageList     |
| frmMain       | GetListType          |
| frmMain       | VB3Decompile         |



### frmMain | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space
| ObjectInfoPointer   | Flag1    | Flag2    | CodeLength   | Flag3        | Flag4    | Null     | Flag5        | Flag6    | #   |
|---------------------|--------|--------|------------|------------|--------|--------|------------|--------|-----|
| 0x00000024          | 0x0025   | 0x0000   | 0x0026       | 0x00270000   | 0x0000   | 0x74E8   | 0x74F80042   | 0x0042   | 0   |
| 0x00010040          | 0x0040   | 0x0000   | 0x7508       | 0x00190042   | 0x0003   | 0x0000   | 0x00000000   | 0x0000   | 1   |
| 0x00425270          | 0xA91C   | 0x0379   | 0x7518       | 0x00190042   | 0x0003   | 0x0040   | 0x00440001   | 0x0000   | 2   |
| 0x00427508          | 0x0018   | 0x0003   | 0x0000       | 0x00000000   | 0x0000   | 0x528C   | 0xA91C0042   | 0x0379   | 3   |



# modGlobals | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modGlobals | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041E104   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x004284A0   |
| `StaticBytesPointer:4`    | 0x004355E8   |
| `ModulePublicPointer:4`   | 0x00502024   |
| `ModuleStaticPointer:4`   | 0x0050308C   |
| `ObjectNamePointer:4`     | 0x00426EE4   |
| `MethodCount:4`           | 0x00000024   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x000005A4   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modGlobals | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0001       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x004265E4   |
| `ProjectDataPointer:4`     | 0x0050201C   |
| `MethodCount:2`            | 0x0007       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0041E144   |
| `ConstantsCount:2`         | 0x0002       |
| `MaxConstants:2`           | 0x0020       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x0379E7A4   |
| `ConstantsPointer:4`       | 0x0041E13C   |



### modGlobals | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x03A6       | 0x05AC   | 0x0000   | 0x003A   | 0   |



### modGlobals | Public variables


| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Static`   | 0x0028       | 0x002C   | 0x0000   | 0x0001   | 0   |



### modGlobals | Methods Declarations

Types iterated and placed here are public-declared



### modGlobals | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space
| ObjectInfoPointer   | Flag1    | Flag2    | CodeLength   | Flag3        | Flag4    | Null     | Flag5        | Flag6    | #   |
|---------------------|--------|--------|------------|------------|--------|--------|------------|--------|-----|
| 0x0AFF5C04          | 0x0003   | 0x0014   | 0x0836       | 0x3CFF5C00   | 0x1CFF   | 0xFCFF   | 0x03431EFE   | 0x0200   | 0   |
| 0x6C3A1A00          | 0x4256   | 0x2135   | 0x2636       | 0x0000002A   | 0x0000   | 0x0000   | 0x00000000   | 0x0000   | 1   |
| 0x0000007E          | 0x0000   | 0x0000   | 0x0000       | 0x00000000   | 0x000A   | 0x0409   | 0x00000000   | 0x0000   | 2   |
| 0x00000000          | 0xE9A0   | 0x0041   | 0xF1FF       | 0xFF0001B8   | 0xFFFF   | 0x0008   | 0x00010000   | 0x0000   | 3   |
| 0x00090009          | 0x00E9   | 0x0000   | 0xEBDC       | 0x37900041   | 0x0042   | 0x5D18   | 0x00780040   | 0x0000   | 4   |
| 0x00000089          | 0x009C   | 0x0000   | 0x009D       | 0x00000000   | 0x0000   | 0x0000   | 0x00000000   | 0x0000   | 5   |
| 0x00000000          | 0x6553   | 0x696D   | 0x4256       | 0x6F636544   | 0x706D   | 0x6C69   | 0x53007265   | 0x6D65   | 6   |



# modOutput | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modOutput | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041DE00   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00428848   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x005025D8   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426EF0   |
| `MethodCount:4`           | 0x0000000E   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modOutput | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0002       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426614   |
| `ProjectDataPointer:4`     | 0x005025D0   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x03754DA8   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041DE38   |



### modOutput | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x00A8       | 0x0028   | 0x0000   | 0x0009   | 0   |



### modOutput | Public variables





### modOutput | Methods Declarations

Types iterated and placed here are public-declared



### modOutput | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# modPeSkeleton | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modPeSkeleton | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041DF18   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00429800   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00502750   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426EFC   |
| `MethodCount:4`           | 0x00000015   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modPeSkeleton | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0003       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426644   |
| `ProjectDataPointer:4`     | 0x00502748   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0020DCC0   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041DF50   |



### modPeSkeleton | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0172       | 0x0578   | 0x0001   | 0x000E   | 0   |



### modPeSkeleton | Public variables





### modPeSkeleton | Methods Declarations

Types iterated and placed here are public-declared



### modPeSkeleton | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# modNative | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modNative | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041DE38   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00428B90   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00502690   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426F0C   |
| `MethodCount:4`           | 0x00000003   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modNative | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0004       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426674   |
| `ProjectDataPointer:4`     | 0x00502688   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0383F438   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041DE70   |



### modNative | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x004C       | 0x0010   | 0x0000   | 0x0002   | 0   |



### modNative | Public variables





### modNative | Methods Declarations

Types iterated and placed here are public-declared



### modNative | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# modControls | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modControls | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041DFFC   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x0042A2C4   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00502DD0   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426F18   |
| `MethodCount:4`           | 0x0000001C   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modControls | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0005       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x004266A4   |
| `ProjectDataPointer:4`     | 0x00502DC8   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x032CB3F0   |
| `ConstantsCount:2`         | 0x0001       |
| `MaxConstants:2`           | 0x0020       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x05447DD4   |
| `ConstantsPointer:4`       | 0x0041E034   |



### modControls | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0054       | 0x0044   | 0x0000   | 0x0004   | 0   |



### modControls | Public variables





### modControls | Methods Declarations

Types iterated and placed here are public-declared



### modControls | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# frmOptions | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018083   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## frmOptions | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x00422890   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x0042B244   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426F24   |
| `MethodCount:4`           | 0x0000000F   |
| `MethodNamesPointer:4`    | 0x00426C88   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018083   |
| `Null:4`                  | 0x00000000   |



## frmOptions | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0006       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D384   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x004266D4   |
| `ProjectDataPointer:4`     | 0x0050302C   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x05309568   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x00422908   |



### frmOptions | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x000C       | 0x00A0   | 0x0000   | 0x0000   | 0   |



### frmOptions | Public variables





### frmOptions | Methods Declarations

Types iterated and placed here are public-declared



### frmOptions | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# modCOM | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modCOM | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041DF50   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x004294FC   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00502724   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426F30   |
| `MethodCount:4`           | 0x00000008   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modCOM | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0007       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426704   |
| `ProjectDataPointer:4`     | 0x0050271C   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x054CD220   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041DF88   |



### modCOM | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0010       | 0x0008   | 0x0000   | 0x0001   | 0   |



### modCOM | Public variables





### modCOM | Methods Declarations

Types iterated and placed here are public-declared



### modCOM | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# clsMemoryMap | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value         |
|--------------------------|---------------|
| `Flags:4`                | 0x00118003    |
| `TypeName:s`             | UserControl   |
| `IsForm:f`               | False         |
| `IsModule:f`             | False         |
| `IsClass:f`              | True          |
| `HasPublicInterface:f`   | False         |
| `HasPublicEvents:f`      | False         |



## clsMemoryMap | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041E514   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00429E08   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426F38   |
| `MethodCount:4`           | 0x00000008   |
| `MethodNamesPointer:4`    | 0x00426CC4   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00118003   |
| `Null:4`                  | 0x00000000   |



## clsMemoryMap | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0008       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D644   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426734   |
| `ProjectDataPointer:4`     | 0x00502D18   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x001B5AF0   |
| `ConstantsCount:2`         | 0x0001       |
| `MaxConstants:2`           | 0x0020       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x052D333C   |
| `ConstantsPointer:4`       | 0x0041E58C   |



### clsMemoryMap | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x002C       | 0x0048   | 0x0000   | 0x0001   | 0   |



### clsMemoryMap | Public variables





### clsMemoryMap | Methods Declarations

Types iterated and placed here are public-declared
| `Object:sz`    | `Public method:sz`   |
|----------------|----------------------|
| clsMemoryMap   | AddSector            |
| clsMemoryMap   | ExportToHTML         |



### clsMemoryMap | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# clsFile | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value         |
|--------------------------|---------------|
| `Flags:4`                | 0x00118003    |
| `TypeName:s`             | UserControl   |
| `IsForm:f`               | False         |
| `IsModule:f`             | False         |
| `IsClass:f`              | True          |
| `HasPublicInterface:f`   | False         |
| `HasPublicEvents:f`      | False         |



## clsFile | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041E7C8   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00429D54   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426F48   |
| `MethodCount:4`           | 0x00000010   |
| `MethodNamesPointer:4`    | 0x00426CE4   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00118003   |
| `Null:4`                  | 0x00000000   |



## clsFile | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0009       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D684   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426764   |
| `ProjectDataPointer:4`     | 0x00502D04   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x037A7BE8   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041E840   |



### clsFile | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0014       | 0x0050   | 0x0000   | 0x0002   | 0   |



### clsFile | Public variables





### clsFile | Methods Declarations

Types iterated and placed here are public-declared
| `Object:sz`   | `Public method:sz`   |
|---------------|----------------------|
| clsFile       | Setup                |
| clsFile       | GetGUID              |
| clsFile       | GetByte              |
| clsFile       | GetBytes             |
| clsFile       | GetString            |
| clsFile       | GetInteger           |
| clsFile       | GetLong              |
| clsFile       | GetDouble            |
| clsFile       | GetSingle            |
| clsFile       | GetCurrency          |
| clsFile       | Position             |
| clsFile       | length               |
| clsFile       | FileNumber           |
| clsFile       | Filename             |
| clsFile       | ShortFileName        |



### clsFile | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# modFrx | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modFrx | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041DDC8   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x0042A350   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00502E1C   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426F50   |
| `MethodCount:4`           | 0x00000000   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modFrx | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x000A       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426794   |
| `ProjectDataPointer:4`     | 0x00502E14   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0331D000   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041DE00   |



### modFrx | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x002C       | 0x0008   | 0x0000   | 0x0001   | 0   |



### modFrx | Public variables





### modFrx | Methods Declarations

Types iterated and placed here are public-declared



### modFrx | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# frmPcode | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018083   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## frmPcode | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x00421704   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x0042B694   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426F58   |
| `MethodCount:4`           | 0x00000009   |
| `MethodNamesPointer:4`    | 0x00426D24   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018083   |
| `Null:4`                  | 0x00000000   |



## frmPcode | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x000B       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D544   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x004267C4   |
| `ProjectDataPointer:4`     | 0x00503054   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x03750150   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0042177C   |



### frmPcode | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x000C       | 0x0070   | 0x0000   | 0x0000   | 0   |



### frmPcode | Public variables





### frmPcode | Methods Declarations

Types iterated and placed here are public-declared



### frmPcode | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# modAntiDecompiler | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modAntiDecompiler | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041DF88   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00429668   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00502734   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426F64   |
| `MethodCount:4`           | 0x00000002   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modAntiDecompiler | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x000C       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x004267F4   |
| `ProjectDataPointer:4`     | 0x0050272C   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0015B760   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0020       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x0375D084   |
| `ConstantsPointer:4`       | 0x0041DFC0   |



### modAntiDecompiler | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0016       | 0x0014   | 0x0000   | 0x0001   | 0   |



### modAntiDecompiler | Public variables





### modAntiDecompiler | Methods Declarations

Types iterated and placed here are public-declared



### modAntiDecompiler | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# modPCodeToVB | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modPCodeToVB | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041DEA8   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00428E98   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x005030D4   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426F78   |
| `MethodCount:4`           | 0x00000003   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modPCodeToVB | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x000D       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426824   |
| `ProjectDataPointer:4`     | 0x005030CC   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x03772438   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041DEE0   |



### modPCodeToVB | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x000C       | 0x0008   | 0x0000   | 0x0000   | 0   |



### modPCodeToVB | Public variables





### modPCodeToVB | Methods Declarations

Types iterated and placed here are public-declared



### modPCodeToVB | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# modPCode | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modPCode | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041E038   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00428A18   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00502608   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426F88   |
| `MethodCount:4`           | 0x00000027   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modPCode | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x000E       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426854   |
| `ProjectDataPointer:4`     | 0x00502600   |
| `MethodCount:2`            | 0x0001       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0041E074   |
| `ConstantsCount:2`         | 0x0001       |
| `MaxConstants:2`           | 0x0020       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x03828BD4   |
| `ConstantsPointer:4`       | 0x0041E070   |



### modPCode | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x00F8       | 0x0080   | 0x0002   | 0x000B   | 0   |



### modPCode | Public variables





### modPCode | Methods Declarations

Types iterated and placed here are public-declared



### modPCode | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space
| ObjectInfoPointer   | Flag1    | Flag2    | CodeLength   | Flag3        | Flag4    | Null     | Flag5        | Flag6    | #   |
|---------------------|--------|--------|------------|------------|--------|--------|------------|--------|-----|
| 0x00300036          | 0x0001   | 0x0017   | 0x6560       | 0x00000042   | 0x0000   | 0xFFFF   | 0xFFFFFFFF   | 0xFFFF   | 0   |



# modTypeLB | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modTypeLB | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041E0BC   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00428E98   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00502700   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426F94   |
| `MethodCount:4`           | 0x00000006   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modTypeLB | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x000F       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426884   |
| `ProjectDataPointer:4`     | 0x005026F8   |
| `MethodCount:2`            | 0x0004       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0041E0F4   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041E0F4   |



### modTypeLB | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x000C       | 0x0008   | 0x0000   | 0x0000   | 0   |



### modTypeLB | Public variables





### modTypeLB | Methods Declarations

Types iterated and placed here are public-declared



### modTypeLB | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space
| ObjectInfoPointer   | Flag1    | Flag2    | CodeLength   | Flag3        | Flag4    | Null     | Flag5        | Flag6    | #   |
|---------------------|--------|--------|------------|------------|--------|--------|------------|--------|-----|
| 0x00000000          | 0x346A   | 0x781D   | 0x0010       | 0x6B736544   | 0x6F74   | 0x0001   | 0x65600001   | 0x0042   | 0   |
| 0x00000000          | 0xFFFF   | 0xFFFF   | 0xFFFF       | 0x0000FFFF   | 0x0000   | 0x65E4   | 0x201C0042   | 0x0050   | 1   |
| 0x00000007          | 0xE144   | 0x0041   | 0x0002       | 0x00000020   | 0x0000   | 0xE7A4   | 0xE13C0379   | 0x0041   | 2   |
| 0x0042813C          | 0x8174   | 0x0042   | 0x5C04       | 0x00030AFF   | 0x0014   | 0x0836   | 0x3CFF5C00   | 0x1CFF   | 3   |



# modLang | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modLang | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041DE70   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x004289A4   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x005026A8   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426FA0   |
| `MethodCount:4`           | 0x00000000   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modLang | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0010       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x004268B4   |
| `ProjectDataPointer:4`     | 0x005026A0   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0335D000   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041DEA8   |



### modLang | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0036       | 0x0050   | 0x0000   | 0x0003   | 0   |



### modLang | Public variables





### modLang | Methods Declarations

Types iterated and placed here are public-declared



### modLang | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# frmAbout | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018083   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## frmAbout | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041F788   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00429B40   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426FA8   |
| `MethodCount:4`           | 0x0000000A   |
| `MethodNamesPointer:4`    | 0x00426D48   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018083   |
| `Null:4`                  | 0x00000000   |



## frmAbout | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0011       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D604   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x004268E4   |
| `ProjectDataPointer:4`     | 0x00502CC8   |
| `MethodCount:2`            | 0x0003       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0041F800   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041F800   |



### frmAbout | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x000C       | 0x006C   | 0x0000   | 0x0000   | 0   |



### frmAbout | Public variables





### frmAbout | Methods Declarations

Types iterated and placed here are public-declared
| `Object:sz`   | `Public method:sz`   |
|---------------|----------------------|
| frmAbout      | StartSysInfo         |
| frmAbout      | GetKeyValue          |



### frmAbout | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space
| ObjectInfoPointer   | Flag1    | Flag2    | CodeLength   | Flag3        | Flag4    | Null     | Flag5        | Flag6    | #   |
|---------------------|--------|--------|------------|------------|--------|--------|------------|--------|-----|
| 0x746C0013          | 0xE4FF   | 0x0EFF   | 0x0052       | 0x97F0000C   | 0x0042   | 0x9460   | 0x00400042   | 0x0012   | 0   |
| 0x00000034          | 0x7ADC   | 0x0042   | 0x000A       | 0x00000003   | 0x0000   | 0x0000   | 0xFA100000   | 0x0041   | 1   |
| 0x0379A9EC          | 0x9120   | 0x0042   | 0x000A       | 0x00400003   | 0x0011   | 0x0038   | 0x75740000   | 0x0042   | 2   |



# frmAddressToFileOffset | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018083   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## frmAddressToFileOffset | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041FF38   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00429B40   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426FB4   |
| `MethodCount:4`           | 0x00000005   |
| `MethodNamesPointer:4`    | 0x00426D70   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018083   |
| `Null:4`                  | 0x00000000   |



## frmAddressToFileOffset | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0012       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D584   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426914   |
| `ProjectDataPointer:4`     | 0x00502CDC   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x037D0DD0   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041FFB0   |



### frmAddressToFileOffset | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x000C       | 0x006C   | 0x0000   | 0x0000   | 0   |



### frmAddressToFileOffset | Public variables





### frmAddressToFileOffset | Methods Declarations

Types iterated and placed here are public-declared



### frmAddressToFileOffset | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# frmCheckUpdate | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018083   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## frmCheckUpdate | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041F29C   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x0042B04C   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426FCC   |
| `MethodCount:4`           | 0x00000003   |
| `MethodNamesPointer:4`    | 0x00426D84   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018083   |
| `Null:4`                  | 0x00000000   |



## frmCheckUpdate | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0013       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D3C4   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426944   |
| `ProjectDataPointer:4`     | 0x00503018   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x038D2E50   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041F314   |



### frmCheckUpdate | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x000C       | 0x005C   | 0x0000   | 0x0000   | 0   |



### frmCheckUpdate | Public variables





### frmCheckUpdate | Methods Declarations

Types iterated and placed here are public-declared



### frmCheckUpdate | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# frmAdvDecompile | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018083   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## frmAdvDecompile | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x004206EC   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00429B40   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426FDC   |
| `MethodCount:4`           | 0x00000005   |
| `MethodNamesPointer:4`    | 0x00426D90   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018083   |
| `Null:4`                  | 0x00000000   |



## frmAdvDecompile | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0014       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D6C4   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426974   |
| `ProjectDataPointer:4`     | 0x00502CF0   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x03776C90   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x00420764   |



### frmAdvDecompile | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x000C       | 0x006C   | 0x0000   | 0x0000   | 0   |



### frmAdvDecompile | Public variables





### frmAdvDecompile | Methods Declarations

Types iterated and placed here are public-declared



### frmAdvDecompile | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# modVB4 | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modVB4 | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041DD90   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00429FF0   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00502D34   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426FEC   |
| `MethodCount:4`           | 0x00000001   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modVB4 | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0015       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x004269A4   |
| `ProjectDataPointer:4`     | 0x00502D2C   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x037F20E0   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041DDC8   |



### modVB4 | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0020       | 0x0094   | 0x0000   | 0x0002   | 0   |



### modVB4 | Public variables





### modVB4 | Methods Declarations

Types iterated and placed here are public-declared



### modVB4 | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# frmNativeDecompile | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018083   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## frmNativeDecompile | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x00420EAC   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x0042B4C0   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00426FF4   |
| `MethodCount:4`           | 0x00000008   |
| `MethodNamesPointer:4`    | 0x00426DA4   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018083   |
| `Null:4`                  | 0x00000000   |



## frmNativeDecompile | Information

Second structure following next of current object

| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0016       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D304   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x004269D4   |
| `ProjectDataPointer:4`     | 0x00503040   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x032BBBF8   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x00420F24   |



### frmNativeDecompile | Public variables

Types iterated and placed here are public-declared

| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0010       | 0x0074   | 0x0000   | 0x0001   | 0   |



### frmNativeDecompile | Public variables





### frmNativeDecompile | Methods Declarations

Types iterated and placed here are public-declared



### frmNativeDecompile | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# modRegistry | Type Declaration

Type info is custom structure what describes current object

| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modRegistry | Descriptor

Heading structure represents object names `PublicObjectDescriptor`

| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041E078   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00428E98   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00503084   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00427008   |
| `MethodCount:4`           | 0x00000004   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modRegistry | Information

Second structure following next of current object

| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0017       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426A04   |
| `ProjectDataPointer:4`     | 0x0050307C   |
| `MethodCount:2`            | 0x0003       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0041E0B0   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041E0B0   |



### modRegistry | Public variables

Types iterated and placed here are public-declared

| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x000C       | 0x0008   | 0x0000   | 0x0000   | 0   |



### modRegistry | Public variables





### modRegistry | Methods Declarations

Types iterated and placed here are public-declared



### modRegistry | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space

| ObjectInfoPointer   | Flag1    | Flag2    | CodeLength   | Flag3        | Flag4    | Null     | Flag5        | Flag6    | #   |
|---------------------|--------|--------|------------|------------|--------|--------|------------|--------|-----|
| 0x00000044          | 0x0104   | 0x0000   | 0x3FFC       | 0x00010338   | 0x000F   | 0x6560   | 0x00000042   | 0x0000   | 0   |
| 0xFFFFFFFF          | 0xFFFF   | 0xFFFF   | 0x0000       | 0x68840000   | 0x0042   | 0x26F8   | 0x00040050   | 0x0000   | 1   |
| 0x0041E0F4          | 0x0000   | 0x0000   | 0x0000       | 0x00000000   | 0x0000   | 0xE0F4   | 0x00000041   | 0x0000   | 2   |



# modVBNET | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modVBNET | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041DFC0   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x0042A41C   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00502E2C   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00427014   |
| `MethodCount:4`           | 0x00000051   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modVBNET | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0018       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426A34   |
| `ProjectDataPointer:4`     | 0x00502E24   |
| `MethodCount:2`            | 0x0001       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0041DFF8   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041DFF8   |



### modVBNET | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0268       | 0x01EC   | 0x0001   | 0x0030   | 0   |



### modVBNET | Public variables





### modVBNET | Methods Declarations

Types iterated and placed here are public-declared



### modVBNET | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space
| ObjectInfoPointer   | Flag1    | Flag2    | CodeLength   | Flag3        | Flag4    | Null     | Flag5        | Flag6    | #   |
|---------------------|--------|--------|------------|------------|--------|--------|------------|--------|-----|
| 0x20202020          | 0x0001   | 0x0005   | 0x6560       | 0x00000042   | 0x0000   | 0xFFFF   | 0xFFFFFFFF   | 0xFFFF   | 0   |



# CInstruction | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value         |
|--------------------------|---------------|
| `Flags:4`                | 0x00118003    |
| `TypeName:s`             | UserControl   |
| `IsForm:f`               | False         |
| `IsModule:f`             | False         |
| `IsClass:f`              | True          |
| `HasPublicInterface:f`   | False         |
| `HasPublicEvents:f`      | False         |



## CInstruction | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041E668   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00443A1C   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00427020   |
| `MethodCount:4`           | 0x00000000   |
| `MethodNamesPointer:4`    | 0x001CF8E0   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00118003   |
| `Null:4`                  | 0x00000000   |



## CInstruction | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x0019       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D444   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426A64   |
| `ProjectDataPointer:4`     | 0x00503104   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x001C9298   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041E6E0   |



### CInstruction | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0018       | 0x0050   | 0x0000   | 0x0003   | 0   |



### CInstruction | Public variables





### CInstruction | Methods Declarations

Types iterated and placed here are public-declared



### CInstruction | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# CDisassembler | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value         |
|--------------------------|---------------|
| `Flags:4`                | 0x00118003    |
| `TypeName:s`             | UserControl   |
| `IsForm:f`               | False         |
| `IsModule:f`             | False         |
| `IsClass:f`              | True          |
| `HasPublicInterface:f`   | False         |
| `HasPublicEvents:f`      | False         |



## CDisassembler | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041E304   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x004416B0   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00427030   |
| `MethodCount:4`           | 0x00000005   |
| `MethodNamesPointer:4`    | 0x00426DC4   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00118003   |
| `Null:4`                  | 0x00000000   |



## CDisassembler | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x001A       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D4C4   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426A94   |
| `ProjectDataPointer:4`     | 0x005030DC   |
| `MethodCount:2`            | 0x0002       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0041E37C   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041E37C   |



### CDisassembler | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x000C       | 0x0040   | 0x0000   | 0x0000   | 0   |



### CDisassembler | Public variables





### CDisassembler | Methods Declarations

Types iterated and placed here are public-declared
| `Object:sz`     | `Public method:sz`   |
|-----------------|----------------------|
| CDisassembler   | DisasmBytes          |
| CDisassembler   | DisasmBlock          |
| CDisassembler   | GetNativeApi         |



### CDisassembler | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space
| ObjectInfoPointer   | Flag1    | Flag2    | CodeLength   | Flag3        | Flag4    | Null     | Flag5        | Flag6    | #   |
|---------------------|--------|--------|------------|------------|--------|--------|------------|--------|-----|
| 0x00000000          | 0x0000   | 0x0000   | 0x8EE8       | 0x0FD40042   | 0x0044   | 0x0040   | 0x00340002   | 0x0000   | 0   |
| 0x00428F08          | 0xFFFF   | 0xFFFF   | 0x0000       | 0x00000000   | 0x0000   | 0xE3C0   | 0x86EC0041   | 0x0393   | 1   |



# modNeFormat | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018001   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## modNeFormat | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041DEE0   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00437C38   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x005030C0   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00427040   |
| `MethodCount:4`           | 0x00000002   |
| `MethodNamesPointer:4`    | 0x00000000   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018001   |
| `Null:4`                  | 0x00000000   |



## modNeFormat | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x001B       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0xFFFFFFFF   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426AC4   |
| `ProjectDataPointer:4`     | 0x005030B8   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x053FE680   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041DF18   |



### modNeFormat | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0036       | 0x000C   | 0x0000   | 0x0002   | 0   |



### modNeFormat | Public variables





### modNeFormat | Methods Declarations

Types iterated and placed here are public-declared



### modNeFormat | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# clsConsole | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value         |
|--------------------------|---------------|
| `Flags:4`                | 0x00118003    |
| `TypeName:s`             | UserControl   |
| `IsForm:f`               | False         |
| `IsModule:f`             | False         |
| `IsClass:f`              | True          |
| `HasPublicInterface:f`   | False         |
| `HasPublicEvents:f`      | False         |



## clsConsole | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041E408   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00441D28   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x0042704C   |
| `MethodCount:4`           | 0x00000004   |
| `MethodNamesPointer:4`    | 0x00426DD8   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00118003   |
| `Null:4`                  | 0x00000000   |



## clsConsole | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x001C       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D484   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426AF4   |
| `ProjectDataPointer:4`     | 0x005030F0   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x037B4FF8   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041E480   |



### clsConsole | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0010       | 0x0044   | 0x0000   | 0x0001   | 0   |



### clsConsole | Public variables





### clsConsole | Methods Declarations

Types iterated and placed here are public-declared
| `Object:sz`   | `Public method:sz`   |
|---------------|----------------------|
| clsConsole    | Clear                |
| clsConsole    | WriteLine            |
| clsConsole    | WriteA               |
| clsConsole    | SaveConsoleToFile    |



### clsConsole | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# frmStartUp | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value        |
|--------------------------|--------------|
| `Flags:4`                | 0x00018083   |
| `TypeName:s`             | Class        |
| `IsForm:f`               | False        |
| `IsModule:f`             | False        |
| `IsClass:f`              | True         |
| `HasPublicInterface:f`   | False        |
| `HasPublicEvents:f`      | False        |



## frmStartUp | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x00421F70   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x0042B814   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00427058   |
| `MethodCount:4`           | 0x00000006   |
| `MethodNamesPointer:4`    | 0x00426DE8   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00018083   |
| `Null:4`                  | 0x00000000   |



## frmStartUp | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x001D       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D2C4   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426B24   |
| `ProjectDataPointer:4`     | 0x00503068   |
| `MethodCount:2`            | 0x0000       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x038D16B0   |
| `ConstantsCount:2`         | 0x0001       |
| `MaxConstants:2`           | 0x0020       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x037FA36C   |
| `ConstantsPointer:4`       | 0x00421FE8   |



### frmStartUp | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x0016       | 0x0084   | 0x0000   | 0x0001   | 0   |



### frmStartUp | Public variables





### frmStartUp | Methods Declarations

Types iterated and placed here are public-declared



### frmStartUp | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space



# clsTypeLibInfo | Type Declaration

Type info is custom structure what describes current object
| Name                     | Value         |
|--------------------------|---------------|
| `Flags:4`                | 0x00118003    |
| `TypeName:s`             | UserControl   |
| `IsForm:f`               | False         |
| `IsModule:f`             | False         |
| `IsClass:f`              | True          |
| `HasPublicInterface:f`   | False         |
| `HasPublicEvents:f`      | False         |



## clsTypeLibInfo | Descriptor

Heading structure represents object names `PublicObjectDescriptor`
| Name                      | Value        |
|---------------------------|--------------|
| `ObjectInfoPointer:4`     | 0x0041EEAC   |
| `Reserved:4`              | 0xFFFFFFFF   |
| `PublicBytesPointer:4`    | 0x00429474   |
| `StaticBytesPointer:4`    | 0x00000000   |
| `ModulePublicPointer:4`   | 0x00000000   |
| `ModuleStaticPointer:4`   | 0x00000000   |
| `ObjectNamePointer:4`     | 0x00427064   |
| `MethodCount:4`           | 0x00000037   |
| `MethodNamesPointer:4`    | 0x00426E00   |
| `StaticVarsOffset:4`      | 0x0000FFFF   |
| `ObjectTypeFlags:4`       | 0x00118003   |
| `Null:4`                  | 0x00000000   |



## clsTypeLibInfo | Information

Second structure following next of current object
| Name                       | Value        |
|----------------------------|--------------|
| `RefCount:2`               | 0x0001       |
| `ObjectIndex:2`            | 0x001E       |
| `ObjectTablePointer:4`     | 0x00426560   |
| `IdeDataPointer:4`         | 0x00000000   |
| `PrivateObjectPointer:4`   | 0x0044D404   |
| `Reserved:4`               | 0xFFFFFFFF   |
| `Null1:4`                  | 0x00000000   |
| `ObjectPointer:4`          | 0x00426B54   |
| `ProjectDataPointer:4`     | 0x00502708   |
| `MethodCount:2`            | 0x000A       |
| `MethodCountUsed:2`        | 0x0000       |
| `MethodsPointer:4`         | 0x0041EF24   |
| `ConstantsCount:2`         | 0x0000       |
| `MaxConstants:2`           | 0x0000       |
| `IdeDataPointer2:4`        | 0x00000000   |
| `IdeDataPointer3:4`        | 0x00000000   |
| `ConstantsPointer:4`       | 0x0041EF24   |



### clsTypeLibInfo | Public variables

Types iterated and placed here are public-declared
| TypeName   | TotalBytes   | Flags1   | Count1   | Count2   | #   |
|------------|------------|--------|--------|--------|-----|
| `Public`   | 0x001A       | 0x019C   | 0x0000   | 0x0002   | 0   |



### clsTypeLibInfo | Public variables





### clsTypeLibInfo | Methods Declarations

Types iterated and placed here are public-declared
| `Object:sz`      | `Public method:sz`   |
|------------------|----------------------|
| clsTypeLibInfo   | Filename             |
| clsTypeLibInfo   | AliasName            |
| clsTypeLibInfo   | ParameterFlags       |
| clsTypeLibInfo   | ParameterType        |
| clsTypeLibInfo   | ParameterName        |
| clsTypeLibInfo   | VariableName         |
| clsTypeLibInfo   | VariableType         |
| clsTypeLibInfo   | VariableValue        |
| clsTypeLibInfo   | VariableKind         |
| clsTypeLibInfo   | VariableCount        |
| clsTypeLibInfo   | ParameterCount       |
| clsTypeLibInfo   | FunctionVTOffset     |
| clsTypeLibInfo   | FunctionReturnType   |
| clsTypeLibInfo   | FunctionName         |
| clsTypeLibInfo   | FunctionInvKind      |
| clsTypeLibInfo   | TypeInfoImplements   |
| clsTypeLibInfo   | TypeInfoFunctions    |
| clsTypeLibInfo   | TypeInfoPrgID        |
| clsTypeLibInfo   | TypeInfoGUID         |
| clsTypeLibInfo   | TypeInfoKind         |
| clsTypeLibInfo   | TypeInfoName         |
| clsTypeLibInfo   | TypeInfoCount        |
| clsTypeLibInfo   | TypeLibName          |
| clsTypeLibInfo   | ImplementGUID        |
| clsTypeLibInfo   | ImplementName        |
| clsTypeLibInfo   | SelectImplement      |
| clsTypeLibInfo   | SelectParameter      |
| clsTypeLibInfo   | SelectVariable       |
| clsTypeLibInfo   | SelectFunction       |
| clsTypeLibInfo   | SelectTypeInfo       |
| clsTypeLibInfo   | OpenTypeLib          |
| clsTypeLibInfo   | CloseTypeLib         |
| clsTypeLibInfo   | ParamFlags2String    |
| clsTypeLibInfo   | VarKind2String       |
| clsTypeLibInfo   | InvKind2String       |
| clsTypeLibInfo   | TKind2String         |



### clsTypeLibInfo | Private Methods data

Types iterated and placed here are private and unnamed. Pointers to names of each method are located unreachable memory space
| ObjectInfoPointer   | Flag1    | Flag2    | CodeLength   | Flag3        | Flag4    | Null     | Flag5        | Flag6    | #   |
|---------------------|--------|--------|------------|------------|--------|--------|------------|--------|-----|
| 0xFFFFFF64          | 0x0008   | 0xFFFF   | 0x0000       | 0x00780000   | 0x0000   | 0x8460   | 0xFFFF0774   | 0xFFFF   | 0   |
| 0xFFFFFF60          | 0x0008   | 0xFFFF   | 0x0000       | 0x005C0000   | 0x0073   | 0x8EE8   | 0x8EF80042   | 0x0042   | 1   |
| 0x00020040          | 0x0190   | 0x0000   | 0x8F08       | 0xFFFF0042   | 0xFFFF   | 0x0000   | 0x00000000   | 0x0000   | 2   |
| 0x0041F030          | 0x86EC   | 0x0393   | 0x8F18       | 0xFFFF0042   | 0xFFFF   | 0xF058   | 0xF0650041   | 0x0041   | 3   |
| 0x0041F072          | 0xF07F   | 0x0041   | 0xF08C       | 0xF0990041   | 0x0041   | 0xF0A6   | 0xF0B30041   | 0x0041   | 4   |
| 0x0041F0C0          | 0xF0CD   | 0x0041   | 0xF0DA       | 0xF0E70041   | 0x0041   | 0xF0F4   | 0xF1010041   | 0x0041   | 5   |
| 0x0041F10E          | 0xF11B   | 0x0041   | 0xF128       | 0xF1350041   | 0x0041   | 0xF142   | 0xF14F0041   | 0x0041   | 6   |
| 0x0041F15C          | 0xF169   | 0x0041   | 0xF176       | 0xF1830041   | 0x0041   | 0xF190   | 0xF19D0041   | 0x0041   | 7   |
| 0x0041F1AA          | 0xF1B7   | 0x0041   | 0xF1C4       | 0xF1D10041   | 0x0041   | 0xF1DE   | 0xF1EB0041   | 0x0041   | 8   |
| 0x0041F26D          | 0xF27A   | 0x0041   | 0xF287       | 0xF2940041   | 0x0041   | 0xF1F8   | 0xF2050041   | 0x0041   | 9   |





