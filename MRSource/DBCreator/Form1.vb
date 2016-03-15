Imports MRFramework.MRPersisting.Factory
Imports MRFramework.MRDBCreator

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        MRC.GetInstance().ConnectionString = My.Settings.Item(My.Settings.DefaultConnectionString)
        MRC.GetInstance().ProviderName = My.Settings.Item(My.Settings.DefaultProvider)

        Dim creator As New MRFramework.MRDBCreator.DBCreator


        creator.CreateSystemObjects()



        creator.AddModule(New DBCreators.Common.Common2)
        creator.AddModule(New DBCreators.Common.Common)
        creator.AddModule(New DBCreators.Common.DBO)



        For i As Integer = 0 To creator.DBModules.Count - 1
            creator.DBModules(i).LoadRevisions()
        Next

        ' treba dorada sorta kada postoji vise shema. sloziti ovo. kasnije u tablici dodati index
        creator.AllDBSqlRevisions.Sort(AddressOf DBSqlRevision.CompareRevisionsForDbCreations)

        Using cnn As Common.DbConnection = MRC.GetConnection()
            cnn.Open()

            Using trn As Common.DbTransaction = cnn.BeginTransaction
                For i As Integer = 0 To creator.DBModules.Count - 1
                    creator.DBModules(i).CreateRevisions(cnn)
                Next
            End Using
        End Using

    End Sub
End Class
