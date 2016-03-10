Imports MRFramework.MRDBCreator
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim modules As New List(Of IDBModule)

        modules.Add(DBCreators.Common.Common.GetInstance())

        For i As Integer = 0 To modules.Count - 1
            modules(i).Create()
        Next
    End Sub
End Class
