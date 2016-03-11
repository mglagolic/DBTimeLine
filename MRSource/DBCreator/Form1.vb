Imports MRFramework.MRPersisting.Factory
Imports MRFramework.MRDBCreator

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MRC.GetInstance().ConnectionString = Properties.Settings.Default[Properties.Settings.Default.DefaultConnectionString].ToString()
        'MRC.GetInstance().ProviderName = Properties.Settings.Default[Properties.Settings.Default.DefaultProvider].ToString()

        MRC.GetInstance().ConnectionString = My.Settings.Item(My.Settings.DefaultConnectionString)
        MRC.GetInstance().ProviderName = My.Settings.Item(My.Settings.DefaultProvider)

        Dim modules As New List(Of IDBModule)

        modules.Add(New DBCreators.Common.Common)

        Using cnn As Common.DbConnection = MRFramework.MRPersisting.Factory.MRC.GetConnection()
            Using trn As Common.DbTransaction = cnn.BeginTransaction
                For i As Integer = 0 To modules.Count - 1
                    modules(i).DBCreate(cnn)
                Next
            End Using
        End Using

    End Sub
End Class
