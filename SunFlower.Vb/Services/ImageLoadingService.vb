Imports System.Diagnostics
Imports System.IO
Imports System.Runtime.InteropServices

Namespace Services

Public Class ImageLoadingService
    <DllImport("kernel32.dll")>
    Private Shared Function ReadProcessMemory(
        hProcess As IntPtr, 
        lpBaseAddress As IntPtr, 
        <Out> lpBuffer As Byte(), 
        dwSize As Integer, 
        ByRef lpNumberOfBytesRead As IntPtr
    ) As Boolean
    End Function
    
    <DllImport("kernel32.dll")>
    Private Shared Function OpenProcess(dwDesiredAccess As Integer, bInheritHandle As Boolean, dwProcessId As Integer) As IntPtr
    End Function
    
    Public Property BaseAddress As IntPtr = 0
    Public Property ProcessBytes As Byte()
    
    Public Shared Function Init(exePath As String, Optional timeoutMs As Integer = 5000) As Process
        If Not File.Exists(exePath) Then
            Throw New FileNotFoundException($"VB executable not found: {exePath}")
        End If
        
        Dim processInfo As New ProcessStartInfo(exePath)
        processInfo.UseShellExecute = False
        processInfo.RedirectStandardOutput = True
        processInfo.RedirectStandardError = True
        processInfo.CreateNoWindow = True
        
        Dim proc = Process.Start(processInfo)
        
        If Not proc.WaitForInputIdle(timeoutMs) Then
            Throw New TimeoutException("VB process failed to initialize in time")
        End If
        
        ' freeze thread for a sometime. 
        Threading.Thread.Sleep(1000)
        
        Return proc
    End Function

    Private Shared Function GetProcessByName(processName As String) As Process
        Dim processes = Process.GetProcessesByName(processName)
        Return If(processes.Length > 0, processes(0), Nothing)
    End Function
    
    Public Sub Load(processName As String)
        Dim proc = GetProcessByName(processName)
        If proc Is Nothing Then
            ReDim ProcessBytes(1)
            Return
        End If
        
        Dim hProcess = OpenProcess(&H1F0FFF, False, proc.Id) ' PROCESS_ALL_ACCESS
        
        ' define image base
        Dim basePtr = proc.MainModule.BaseAddress
        Dim moduleSize = proc.MainModule.ModuleMemorySize
        
        Dim buffer(moduleSize - 1) As Byte
        Dim bytesRead As IntPtr
        ReadProcessMemory(hProcess, basePtr, buffer, moduleSize, bytesRead)
        
        ProcessBytes = buffer
        BaseAddress = basePtr
    End Sub
    
    Public Shared Sub Free(proc As Process)
        proc.Kill()
    End Sub
End Class
End Namespace