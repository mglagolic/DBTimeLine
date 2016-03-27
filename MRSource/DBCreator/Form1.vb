Imports MRFramework.MRPersisting.Factory
Imports Framework.DBCreator
Imports Framework.DBCreator.DBObjects
Imports System.ComponentModel

Public Class Form1



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Delegate Sub BatchExecutingCallback(sender As Object, e As BatchExecutingEventArgs)
    Private Sub BatchExecutingHandler(sender As Object, e As BatchExecutingEventArgs)
        If rtb1.InvokeRequired Then
            Dim d As New BatchExecutingCallback(AddressOf BatchExecutingHandler)
            rtb1.Invoke(d, sender, e)
            Exit Sub
        End If
        rtb1.SelectionColor = Color.Yellow
        rtb1.AppendText("EXECUTING..." & vbNewLine)
        rtb1.SelectionColor = Color.LawnGreen
        rtb1.AppendText(e.Sql & vbNewLine)
        rtb1.ScrollToCaret()
    End Sub

    Delegate Sub BatchExecutedCallback(sender As Object, e As BatchExecutedEventArgs)
    Private Sub BatchExecutedHandler(sender As Object, e As BatchExecutedEventArgs)
        If rtb1.InvokeRequired Then
            Dim d As New BatchExecutedCallback(AddressOf BatchExecutedHandler)
            rtb1.Invoke(d, sender, e)
            Exit Sub
        End If

        Dim color As Color = Color.LawnGreen
        If e.ResultType = eBatchExecutionResultType.Failed Then
            color = Color.Red
            rtb1.SelectionColor = color
            rtb1.AppendText(e.Sql & vbNewLine)
        End If
        rtb1.SelectionColor = Color.Yellow
        rtb1.AppendText("Duration: " & e.Duration.ToString() & vbNewLine & vbNewLine)
        rtb1.ScrollToCaret()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        rtb1.Text = ""
        If backWorker.IsBusy Then
            backWorker.CancelAsync()
        Else

            backWorker.RunWorkerAsync(Nothing)
        End If

    End Sub

    Private Sub DbCreate()
        ' TODO - ovo sve u progressbar i thread

        MRC.GetInstance().ConnectionString = My.Settings.Item(My.Settings.DefaultConnectionString)
        MRC.GetInstance().ProviderName = My.Settings.Item(My.Settings.DefaultProvider)

        Dim dbSqlFactory As New DBSqlGeneratorFactory


        Dim creator As New Framework.DBCreator.DBCreator With {.DBSqlGenerator = dbSqlFactory.GetDBSqlGenerator(eDBType.TransactSQL)}
        AddHandler creator.BatchExecuting, AddressOf BatchExecutingHandler
        AddHandler creator.BatchExecuted, AddressOf BatchExecutedHandler


        creator.CreateSystemObjects()

        ' TODO  - module dodavati reflectionom citajuci dll-ove iz app foldera. dodati property dll name u module tablicu ili slicno
        '       - smisao je da samo postojanje dll-a odradjuje posao, fleg active ga moze ukljuciti ili iskljuciti

        creator.AddModule(New DBCreators.Common.DBO)
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
        RemoveHandler creator.BatchExecuting, AddressOf BatchExecutingHandler
    End Sub

    Private Sub DoWork(sender As Object, e As DoWorkEventArgs) Handles backWorker.DoWork
        backWorker.ReportProgress(0)
        DbCreate()
    End Sub

    Private Sub backWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles backWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub backWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles backWorker.RunWorkerCompleted
        ProgressBar1.Value = 100
    End Sub
End Class
