Imports System.IO
Imports SunFlower.Vb.Handlers
Imports SunFlower.Vb.Headers
Imports SunFlower.Vb.Managers

Namespace Services
    Public Class Vb5ComRegistrationManager
        Inherits MemoryManager
        Private ReadOnly _header As Vb5Header
        Property ComInfoList As List(Of Vb5ComInfoModel)
        Property ComData As Vb5ComData
        Public Sub New(imageBase As Long, header As Vb5Header, reader As BinaryReader, sections As List(Of PeSection))
            MyBase.New(imageBase := imageBase, 
                       sections := sections)
            _header = header
            _reader = reader
            
        End Sub
        Public Sub ParseComData()
            
        End Sub
        Public Sub ParseComInformation()
            
        End Sub
    End Class
    Public Class Vb5ComInfoModel
        Sub New(comInfo As Vb5ComInfo, name As String, description As String, designerInfo As Vb5DesignerInfo)
            Me.ComInfo = comInfo
            Me.Name = name
            Me.Description = description
            Me.DesignerInfo = designerInfo
        End Sub

        Property ComInfo As Vb5ComInfo
        Property Name As String
        Property Description As String
        Property DesignerInfo As Vb5DesignerInfo
    End Class
End Namespace