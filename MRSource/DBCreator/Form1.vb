Imports MRFramework.MRPersisting.Factory
Imports MRFramework.MRDBCreator

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        MRC.GetInstance().ConnectionString = My.Settings.Item(My.Settings.DefaultConnectionString)
        MRC.GetInstance().ProviderName = My.Settings.Item(My.Settings.DefaultProvider)

        GlobalStatics.CreateSystemObjects()

        Dim modules As New List(Of IDBModule)

        modules.Add(New DBCreators.Common.Common)

        For i As Integer = 0 To modules.Count - 1
            modules(i).LoadRevisions()
        Next

        ' treba dorada sorta kada postoji vise shema. sloziti ovo. kasnije u tablici dodati index
        GlobalStatics.AllDBSqlRevisions.Sort(AddressOf DBSqlRevision.CompareRevisionsForDbCreations)

        Using cnn As Common.DbConnection = MRC.GetConnection()
            cnn.Open()

            Using trn As Common.DbTransaction = cnn.BeginTransaction
                For i As Integer = 0 To modules.Count - 1
                    modules(i).CreateRevisions(cnn)
                Next
            End Using
        End Using

    End Sub
End Class
