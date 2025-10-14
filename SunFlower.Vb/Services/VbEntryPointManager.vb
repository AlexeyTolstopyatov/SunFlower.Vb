Imports System.IO
Imports SunFlower.Vb.Handlers
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Managers
''' VbEntryPointManager defines made by
''' Visual Basic - EntryPoint procedure patterns
''' and resolve next following structures of VB virtual machine
Namespace Services
    Public Class VbEntryPointManager
        Inherits PointersManager
        Const Ia32PushOpcode As Byte = &H68
        Const Ia32CallOpcode As Byte = &H48
        Const Vb5Version As UInt32 = &H1F4
        
        Private _reader As BinaryReader
        Private _runtimeHeaderOffset As Long
        Private _comDataOffset As Long
        Private _projectInfoOffset As Long
        Private _runtimeHeader As Vb5Header
        Private _designerInfoOffsets As List(Of Long) = New List(Of Long)
        Private _comData As Vb5ComData
        Private _projectInfo As Vb5ProjectInfo
        Private ReadOnly _designerInfoList As List(Of Vb5DesignerInfo) = New List(Of Vb5DesignerInfo)()
        Private ReadOnly _comInfoOffsets As List(Of Long) = New List(Of Long)
        Private ReadOnly _comInfoList As List(Of Vb5ComInfo) = New List(Of Vb5ComInfo)
        
        Property CallingRuntimeRva As Long
        Public Sub New(reader As BinaryReader, parameters As Vb5ServiceParameters)
            MyBase.New(parameters.ImageBase)
            
            _reader = reader
            _reader.BaseStream.Position = LongPtrToOffset(parameters.EntryPoint)
            
            _reader.Close() ' destroy all objects before exit
        End Sub
        ''' Compiler/Linker makes the own EntryPoint instead Sub Main()
        ''' for each proceed module because extremely needed to initialize
        ''' runtime from embedded components into module
        Private Function FillEntryPointPattern() As [Option]
            ' TODO: find @100 using ILT/IAT
            Dim push, [call] As Byte
            Dim pushAddress, [callAddress] As UInt32 ' <-- Import table analysis required
            
            push = _reader.ReadByte()
            pushAddress = _reader.ReadUInt32()
            [call] = _reader.ReadByte()          ' Reserved VB.NET keywords ignoring
            [callAddress] = _reader.ReadUInt32() ' same vibe bro
            
            ' <program>::entry()
            ' push 0x00ad345f ;WVA of Vb5Header (~wrong~ RVA here)
            ' call 0x0002444a ;RVA of MSVBVM60.dll!ThunRtMain@100
            If push <> Ia32PushOpcode And [call] <> Ia32CallOpcode Then
                Return New None(
                    ServiceReturns.BadData,
                    "Unable to locate VB virtual machine artefact"
                )
            End If
            
            _reader.BaseStream.Position = LongPtrToOffset(pushAddress)
            _runtimeHeaderOffset = LongPtrToOffset(pushAddress) ' <-- Remember offset
            _runtimeHeader = Fill(Of Vb5Header)(_reader)
            
            ' #1 | Deep Diving into Visual Basic hell
            ' Once legit way to define runtime structure is an imports analysing
            ' virtual_machine.dll!@100 always names like "ThunRtMain" with @100 index
            ' But for a first time -> define VB runtime like this
            If _runtimeHeader.VbMagic <> "VB5!".ToArray() Then
                _runtimeHeaderOffset = 0 ' Bad 
                Return New None(
                    ServiceReturns.BadData, 
                    "Bad signature " & New String(_runtimeHeader.VbMagic)
                ) ' Optional signature. Never checks by virtual machine
            End If

            CallingRuntimeRva = [callAddress]
            
            Return New Some(_runtimeHeader)
        End Function
        ''' ProjectInfo holds information about all external
        ''' components bound to module what not/requires (NCoded/PCoded)
        ''' virtual machine. External Descriptor's API follows like an array after it
        ''' by special ULONG position
        Private Function FillProjectInfo() As [Option]
            ' #2 | ProjectInfo Nested Tree
            ' ProjectInfo is a next following struct by the EXE_PROJ_INFO{...} struct
            ' Structure has nested custom-typed arrays and must reinterpret like them 
            '   dwVersion must be 0x1F4 -> means "500" or "5.00"
            '   lpCodeStarts sometimes equals 0xE9E9E9E9
            '   lpCodeEnds   sometimes equals 0x9E9E9E9E
            '   lpNativeCode 0 -> P-CODE |  !0 -> N-CODE
            _projectInfoOffset = LongPtrToOffset(_runtimeHeader.ProjectDataPointer)
            _reader.BaseStream.Position = LongPtrToOffset(_runtimeHeader.ProjectDataPointer)
            _projectInfo = Fill(Of Vb5ProjectInfo)(_reader)

            If _projectInfo.Version <> Vb5Version Then
                _projectInfo = New Vb5ProjectInfo()
                _projectInfoOffset = 0
                Return New None(ServiceReturns.BadData, "dwVersion fails test. Aborting...")
            End If
            
            Return New Some(_projectInfo)
        End Function
        ''' COM = Component Object Model
        ''' COM descriptor = abstraction of physical location
        ''' of COM .ocx/.dll process in flat Windows memory
        ''' COM registration data means data what extremely needs to describe all
        ''' external OCX/DLL components and self for a VM process init. /I suppose/
        Private Function FillComponentObjectData() As [Option]
            ' #3 | COM Descriptor Nested Tree
            ' If all conditions resolves positive -> reinterpret
            ' wrong pointers to absolute file offsets
            ' [tagREG_DATA] ---+
            ' ...              | dwRegInfoOffset. always not 0. /I suppose/
            ' [tagREG_INFO...<-+
            '   ...
            '   ...
            ' ]
            Try
                _comDataOffset = LongPtrToOffset(_runtimeHeader.ComRegisterDataPointer)
                _reader.BaseStream.Position = LongPtrToOffset(_runtimeHeader.ComRegisterDataPointer)
                _comData = Fill(Of Vb5ComData)(_reader)
            Catch e As Exception
                Return New None(ServiceReturns.BadOperation, e.Message)
            End Try
            ' [[tagREG_INFO], [tagREG_INFO], [tagREG_INFO]...]
            Dim nextObject As Long
            While nextObject <> 0
                ' While dwNextObject offset not 0 -> add next COM registration info into object
                _comInfoOffsets.Add(_reader.BaseStream.Position)
                
                Dim info = Fill(Of Vb5ComInfo)(_reader)
                nextObject = info.NextObjectOffset
                _comInfoList.Add(info)
            End While
            
            Return New Some(_comData)
        End Function
    End Class
End Namespace