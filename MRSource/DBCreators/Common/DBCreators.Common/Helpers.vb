Imports System.IO
Imports System.Reflection

Public NotInheritable Class Helpers
    Private Sub New()

    End Sub

    Private Shared _ExecutingAssembly As Assembly = Nothing
    Private Shared ReadOnly Property ExecutingAssembly As Assembly
        Get
            If _ExecutingAssembly Is Nothing Then
                _ExecutingAssembly = Assembly.GetExecutingAssembly
            End If
            Return _ExecutingAssembly
        End Get
    End Property

    Public Shared Function GetEmbeddedResourceText(fileName As String) As String
        Dim ret As String = ""
        Dim path As String = "DBCreators.Common." & fileName

        Using reader As StreamReader = New StreamReader(ExecutingAssembly.GetManifestResourceStream(path))
            ret = reader.ReadToEnd
        End Using

        Return ret
    End Function

End Class
