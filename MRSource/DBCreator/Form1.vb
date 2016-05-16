Option Strict On

Imports Framework.Persisting
Imports MRFramework.MRPersisting.Factory
Imports Framework.DBCreator
Imports Framework.DBCreator.DBObjects
Imports System.ComponentModel

Imports Framework.Persisting.Interfaces

Public Class Form1
    Public Class myPersister
        Inherits Persister

        Protected Overrides ReadOnly Property DataBaseTableName As String
            Get
                Return "Place.Table1"
            End Get
        End Property

        Protected Overrides ReadOnly Property Sql As String
            Get
                Return _
"SELECT
	t1.ID,
    Table2Naziv = t2.Naziv,
    Table2ID = t2.TableKey
FROM
	Place.Table1 t1
	LEFT JOIN Place.Table2 t2 on t1.Table2Key = t2.TableKey"
            End Get
        End Property

    End Class

    Private Sub persistingTests()

        Dim per As IPersister = New myPersister
        per.CNN = MRC.GetConnection
        per.Where = "t1.Broj = 1"
        per.OrderItems.Add(New Implementation.OrderItem() With {.SqlName = "t1.Broj"})
        per.OrderItems.Add(New Implementation.OrderItem() With {.SqlName = "t1.id"})
        per.CNN.Open()
        Dim ts1 = New TimeSpan(Now.Ticks)
        Dim data = per.GetData(Nothing)
        Dim ts2 = New TimeSpan(Now.Ticks)
        Dim dataPage = per.GetData(Nothing, 20)
        Dim ts3 = New TimeSpan(Now.Ticks)
        per.PageSize = 30000
        Dim totalPages As Integer = CInt(data.Count / per.PageSize)
        For i As Integer = 1 To totalPages
            per.GetData(Nothing, i)
        Next
        Dim ts4 = New TimeSpan(Now.Ticks)

        Console.WriteLine((ts2 - ts1).ToString & ":" & data.Count.ToString())
        Console.WriteLine((ts3 - ts2).ToString & ":" & dataPage.Count.ToString())
        Console.WriteLine((ts4 - ts2).ToString & " ukupno stranica:" & totalPages.ToString())
        per.CNN.Close()
        Dim o = data.Single(Function(itm) CType(itm.ColumnValues("ID"), Guid) = New Guid("3aca7383-5ae4-4a17-b9b0-000a761a505c"))

    End Sub

    Dim ts1 As TimeSpan
    Dim ts2 As TimeSpan

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Button1.PerformClick()

        MRC.GetInstance().ConnectionString = CType(My.Settings.Item(My.Settings.DefaultConnectionString), String)
        MRC.GetInstance().ProviderName = CType(My.Settings.Item(My.Settings.DefaultProvider), String)
        PersistingSettings.Instance.SqlGeneratorFactory = New Implementation.SqlGeneratorFactory()

        'persistingTests()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ts1 = New TimeSpan(Now.Ticks)
        rtb1.Text = ""
        If backWorker.IsBusy Then
            backWorker.CancelAsync()
        Else
            backWorker.RunWorkerAsync(Nothing)
        End If
    End Sub

    Delegate Sub BatchExecutingCallback(sender As Object, e As BatchExecutingEventArgs)
    Private Sub BatchExecutingHandler(sender As Object, e As BatchExecutingEventArgs)
        If rtb1.InvokeRequired Then
            Dim d As New BatchExecutingCallback(AddressOf BatchExecutingHandler)
            rtb1.Invoke(d, sender, e)
            Exit Sub
        End If
        rtb1.SelectionColor = Color.Yellow
        rtb1.AppendText("-- EXECUTING..." & vbNewLine)
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

        If e.TotalRevisionsCount <> 0 Then
            backWorker.ReportProgress(CInt(e.ExecutedRevisionsCount / e.TotalRevisionsCount * 100))
        End If

        If e.ResultType = eBatchExecutionResultType.Failed Then
            rtb1.SelectionColor = Color.Red
            rtb1.AppendText(e.ErrorMessage & vbNewLine)
        End If
        rtb1.SelectionColor = Color.Yellow
        rtb1.AppendText("-- Duration: " & e.Duration.ToString() & vbNewLine & vbNewLine)
        rtb1.ScrollToCaret()
    End Sub

    Private Sub DbCreate()
        MRC.GetInstance().ConnectionString = CType(My.Settings.Item(My.Settings.DefaultConnectionString), String)
        MRC.GetInstance().ProviderName = CType(My.Settings.Item(My.Settings.DefaultProvider), String)
        Dim per As New myPersister

        Dim creator As New DBTimeLine(eDBType.TransactSQL, New DBSqlGeneratorFactory)
        AddHandler creator.BatchExecuting, AddressOf BatchExecutingHandler
        AddHandler creator.BatchExecuted, AddressOf BatchExecutedHandler

        creator.CreateSystemObjects()

        ' TODO - isprogramirati podrsku za store
        ' TODO - isprogramirati podrsku za triggere
        ' TODO - odraditi code generation adventureWorks baze
        ' TODO - odraditi novi persister do kraja (snimanje, cacheiranje shema i sl.)
        ' TODO - module dodavati reflectionom citajuci dll-ove iz app foldera. dodati property dll name u module tablicu ili slicno
        '      - smisao je da samo postojanje dll-a odradjuje posao, fleg active ga moze ukljuciti ili iskljuciti
        ' TODO - isprogramirati podršku za Role

        ' CONSIDER - db objekte (vieove, tablice, itd) drzati u posebnim classama koje se mogu MEFom aktivirati

        creator.AddModule(New DBCreators.DS())
        creator.AddModule(New DBCreators.Nadzor())
        creator.AddModule(New DBCreators.Common.DBO())

        creator.LoadModuleKeysFromDB()

        For i As Integer = 0 To creator.DBModules.Count - 1
            If creator.ActiveModuleKeys.Contains(creator.DBModules(i).ModuleKey) Then
                creator.DBModules(i).LoadRevisions()
            End If
        Next

        'For i As Integer = 0 To creator.DBModules.Count - 1
        '    creator.DBModules(i).LoadRevisions()
        'Next

        Using cnn As Common.DbConnection = MRC.GetConnection()
            cnn.Open()

            Using trn As Common.DbTransaction = cnn.BeginTransaction

                creator.LoadExecutedDBSqlRevisionsFromDB(cnn, trn)

                creator.ExecuteDBSqlRevisions(cnn, trn)

                Dim newDBSqlRevisions As List(Of DBSqlRevision) = creator.SourceDBSqlRevisions.Except(creator.ExecutedDBSqlRevisions, New DBSqlRevision.DBSqlRevisionEqualityComparer).ToList
                newDBSqlRevisions.Sort(AddressOf DBSqlRevision.CompareRevisionsForDbCreations)

                'Dim imaUBaziNemaUSource = creator.ExecutedDBSqlRevisions.Except(creator.SourceDBSqlRevisions).ToList()
                'Dim unija = imaUSourceuNemaUBazi.Union(imaUBaziNemaUSource).ToList()
                trn.Rollback()
                'trn.Commit()
            End Using

        End Using

        RemoveHandler creator.BatchExecuted, AddressOf BatchExecutedHandler
        RemoveHandler creator.BatchExecuting, AddressOf BatchExecutingHandler
    End Sub

    ' TODO - ovo odraditi reactive programmingom
    Private Sub DoWork(sender As Object, e As DoWorkEventArgs) Handles backWorker.DoWork
        backWorker.ReportProgress(0)
        DbCreate()
    End Sub

    Private Sub backWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles backWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub backWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles backWorker.RunWorkerCompleted
        ProgressBar1.Value = 100
        ts2 = New TimeSpan(Now.Ticks)

        rtb1.SelectionColor = Color.LightBlue
        rtb1.AppendText("-- Total time: " & (ts2 - ts1).ToString() & vbNewLine & vbNewLine)
        rtb1.ScrollToCaret()
    End Sub

    Private Sub zoomRtb_Scroll(sender As Object, e As EventArgs) Handles zoomRtb.Scroll
        Dim tb As TrackBar = CType(sender, TrackBar)
        Dim value As Decimal = tb.Value - tb.Minimum

        Dim zoomFactor As Decimal = value / (tb.Maximum - tb.Minimum) * 2
        If zoomFactor <= 0.015625D Then
            zoomFactor = 0.02D
        ElseIf zoomFactor >= 64 Then
            zoomFactor = 63D
        End If

        rtb1.ZoomFactor = zoomFactor
    End Sub
End Class
