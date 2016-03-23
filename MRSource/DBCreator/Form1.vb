Imports MRFramework.MRPersisting.Factory
Imports Framework.DBCreator
Imports Framework.DBCreator.DBObjects

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MRC.GetInstance().ConnectionString = My.Settings.Item(My.Settings.DefaultConnectionString)
        MRC.GetInstance().ProviderName = My.Settings.Item(My.Settings.DefaultProvider)

        Dim dbSqlFactory As New DBSqlGeneratorFactory

        Dim creator As New Framework.DBCreator.DBCreator With {.DBSqlGenerator = dbSqlFactory.GetDBSqlGenerator(eDBType.TransactSQL)}

        creator.CreateSystemObjects()

        ' TODO  - module dodavati reflectionom citajuci dll-ove iz app foldera. dodati property dll name u module tablicu ili slicno
        '       - smisao je da samo postojanje dll-a odradjuje posao, fleg active ga moze ukljuciti ili iskljuciti

        'creator.AddModule(New DBCreators.Common.DBO)
        creator.AddModule(New DBCreators.Common.CorePlace)
        creator.LoadModuleKeysFromDB()

        For i As Integer = 0 To creator.DBModules.Count - 1
            If creator.ActiveModuleKeys.Contains(creator.DBModules(i).ModuleKey) Then
                creator.DBModules(i).LoadRevisions()
            End If
        Next

        creator.SourceDBSqlRevisions.Sort(AddressOf DBSqlRevision.CompareRevisionsForDbCreations)

        Using cnn As Common.DbConnection = MRC.GetConnection()
            cnn.Open()

            Using trn As Common.DbTransaction = cnn.BeginTransaction

                creator.LoadExecutedDBSqlRevisionsFromDB(cnn, trn)

                RichTextBox1.Text = creator.ExecuteDBSqlRevisions(cnn, trn)

                Dim imaUSourceuNemaUBazi = creator.SourceDBSqlRevisions.Except(creator.ExecutedDBSqlRevisions, New DBSqlRevision.DBSqlRevisionEqualityComparer).ToList()

                'Dim imaUBaziNemaUSource = creator.ExecutedDBSqlRevisions.Except(creator.SourceDBSqlRevisions).ToList()
                'Dim unija = imaUSourceuNemaUBazi.Union(imaUBaziNemaUSource).ToList()
                trn.Rollback()
            End Using

        End Using


    End Sub

End Class
