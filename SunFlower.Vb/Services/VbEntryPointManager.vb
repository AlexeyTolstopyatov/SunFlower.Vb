Imports System.IO
Imports System.Runtime.InteropServices
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
        
        Private ReadOnly _reader As BinaryReader
        Private _runtimeHeader As Vb5Header
        Property CallingRuntimeRva As Long
        Public Property Vb5Header As [Option]
        Public Property RuntimeHeaderOffset as Long

        Public Sub New(reader As BinaryReader, parameters As Vb5ServiceParameters, sections As List(Of PeSection))
            MyBase.New(parameters.ImageBase, sections)
            
            _reader = reader
            _reader.BaseStream.Position = FindOffset(parameters.EntryPoint)
            
            Vb5Header = FillEntryPointPattern()
        End Sub
        ''' Compiler/Linker makes the own EntryPoint instead Sub Main()
        ''' for each proceed module because extremely needed to initialize
        ''' runtime from embedded components into module
        Private Function FillEntryPointPattern() As [Option]
            ' TODO: find @100 using ILT/IAT
            Dim push, [call] As Byte
            Dim pushAddress, [callAddress] As UInt32 ' <-- Import table analysis required
            
            push = _reader.ReadByte() ' EndOfStreamPosition???
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
            
            _reader.BaseStream.Position = FindRVA(pushAddress)
            RuntimeHeaderOffset = FindRVA(pushAddress) ' <-- Remember offset
            _runtimeHeader = Fill(Of Vb5Header)(_reader)
            
            ' #1 | Deep Diving into Visual Basic hell
            ' Once legit way to define runtime structure is an imports analysing
            ' virtual_machine.dll!@100 always names like "ThunRtMain" with @100 index
            ' But for a first time -> define VB runtime like this
            If _runtimeHeader.VbMagic <> "VB5!".ToArray() Then
                RuntimeHeaderOffset = 0 ' Bad 
                Return New None(
                    ServiceReturns.BadData, 
                    "Bad signature " & New String(_runtimeHeader.VbMagic)
                ) ' Optional signature. Never checks by virtual machine
            End If

            CallingRuntimeRva = [callAddress]
            
            Return New Some(_runtimeHeader)
        End Function
    End Class
End Namespace