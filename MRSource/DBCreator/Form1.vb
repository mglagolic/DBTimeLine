Imports MRFramework.MRPersisting.Factory
Imports Framework.DBCreator
Imports Framework.DBCreator.DBObjects

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.PerformClick()
    End Sub

    Private Sub BatchExecutedHandler(sender As Object, e As BatchExecutedEventArgs)
        rtb1.SelectionColor = Color.LawnGreen
        rtb1.AppendText(e.Sql & vbNewLine)
        rtb1.SelectionColor = Color.Yellow
        rtb1.AppendText("Duration: " & e.Duration.ToString() & vbNewLine & vbNewLine)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' TODO - ovo sve u progressbar i thread

        MRC.GetInstance().ConnectionString = My.Settings.Item(My.Settings.DefaultConnectionString)
        MRC.GetInstance().ProviderName = My.Settings.Item(My.Settings.DefaultProvider)

        Dim dbSqlFactory As New DBSqlGeneratorFactory

        rtb1.Text = ""
        Dim creator As New Framework.DBCreator.DBCreator With {.DBSqlGenerator = dbSqlFactory.GetDBSqlGenerator(eDBType.TransactSQL)}
        AddHandler creator.BatchExecuted, AddressOf BatchExecutedHandler


        creator.CreateSystemObjects()

        ' TODO  - module dodavati reflectionom citajuci dll-ove iz app foldera. dodati property dll name u module tablicu ili slicno
        '       - smisao je da samo postojanje dll-a odradjuje posao, fleg active ga moze ukljuciti ili iskljuciti

        'creator.AddModule(New DBCreators.Common.DBO)
        creator.AddModule(New DBCreators.Common.CorePlace)

        'creator.LoadModuleKeysFromDB()

        'For i As Integer = 0 To creator.DBModules.Count - 1
        '    If creator.ActiveModuleKeys.Contains(creator.DBModules(i).ModuleKey) Then
        '        creator.DBModules(i).LoadRevisions()
        '    End If
        'Next
        For i As Integer = 0 To creator.DBModules.Count - 1
            creator.DBModules(i).LoadRevisions()
        Next

        Using cnn As Common.DbConnection = MRC.GetConnection()
            cnn.Open()

            Using trn As Common.DbTransaction = cnn.BeginTransaction

                creator.LoadExecutedDBSqlRevisionsFromDB(cnn, trn)

                'rtb1.Text = 
                creator.ExecuteDBSqlRevisions(cnn, trn)


                Dim newDBSqlRevisions As List(Of DBSqlRevision) = creator.SourceDBSqlRevisions.Except(creator.ExecutedDBSqlRevisions, New DBSqlRevision.DBSqlRevisionEqualityComparer).ToList
                newDBSqlRevisions.Sort(AddressOf DBSqlRevision.CompareRevisionsForDbCreations)


                'Dim imaUBaziNemaUSource = creator.ExecutedDBSqlRevisions.Except(creator.SourceDBSqlRevisions).ToList()
                'Dim unija = imaUSourceuNemaUBazi.Union(imaUBaziNemaUSource).ToList()
                trn.Rollback()
            End Using

        End Using

        RemoveHandler creator.BatchExecuted, AddressOf BatchExecutedHandler

    End Sub
End Class
