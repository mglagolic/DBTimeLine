Option Strict On

Imports Framework.Persisting
Imports MRFramework.MRPersisting.Factory
Imports Framework.DBTimeLine
Imports System.ComponentModel

Imports Framework.Persisting.Interfaces
Imports Framework.DBTimeLine.DBObjects

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
        MRC.GetInstance().ConnectionString = CType(My.Settings.Item(My.Settings.DefaultConnectionString), String)
        MRC.GetInstance().ProviderName = CType(My.Settings.Item(My.Settings.DefaultProvider), String)
        PersistingSettings.Instance.SqlGeneratorFactory = New Implementation.SqlGeneratorFactory()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ts1 = New TimeSpan(Now.Ticks)
        rtb1.Text = ""
        If backWorker.IsBusy Then
            backWorker.CancelAsync()
        Else
            backWorker.RunWorkerAsync(New CreateTimeLineDBInputs() With {.Commit = chxCommit.Checked})
        End If
    End Sub

#Region "Writing rtb"
    Private Sub BatchExecutingHandler(sender As Object, e As BatchExecutingEventArgs)
        WriteTextToRtb(rtb1, "-- EXECUTING..." & vbNewLine, Color.Yellow)
        WriteTextToRtb(rtb1, e.Sql & vbNewLine, Color.LawnGreen)
    End Sub

    Private Sub BatchExecutedHandler(sender As Object, e As BatchExecutedEventArgs)
        If e.TotalRevisionsCount <> 0 Then
            backWorker.ReportProgress(CInt(e.ExecutedRevisionsCount / e.TotalRevisionsCount * 100))
        End If

        If e.ResultType = eBatchExecutionResultType.Failed Then
            If e.Exception IsNot Nothing Then
                WriteException(e.Exception)
            Else
                WriteTextToRtb(rtb1, "/* " & e.ErrorMessage.ToString() & "*/" & vbNewLine, Color.Green)
            End If
        End If
        WriteTextToRtb(rtb1, "-- Duration: " & e.Duration.ToString() & vbNewLine & vbNewLine, Color.Yellow)

    End Sub

    Private Sub ModuleLoadedHandler(sender As Object, e As ModuleLoadedEventArgs)
        If e.Message <> "" Then
            WriteTextToRtb(rtb1, "/* " & e.Message.ToString() & "*/" & vbNewLine, Color.Yellow)
        End If
        If e.ErrorMessage <> "" Then
            WriteTextToRtb(rtb1, "/* " & e.ErrorMessage.ToString() & "*/" & vbNewLine, Color.Red)
        End If
    End Sub

    Private Sub WriteException(e As Exception)
        Dim ex As Exception = e
        While ex IsNot Nothing
            WriteTextToRtb(rtb1, "/* " & ex.Message.ToString() & "*/" & vbNewLine, Color.Red)
            ex = ex.InnerException
        End While
    End Sub

    Delegate Sub WriteTextToRtbCallback(rtb As RichTextBox, text As String, color As Color)
    Private Sub WriteTextToRtb(rtb As RichTextBox, text As String, color As Color)
        If rtb.InvokeRequired Then
            Dim d As New WriteTextToRtbCallback(AddressOf WriteTextToRtb)
            rtb.Invoke(d, rtb, text, color)
            Exit Sub
        End If

        rtb.SelectionColor = color
        rtb.AppendText(text)
        rtb.ScrollToCaret()
    End Sub
#End Region

    Private Class CreateTimeLineDBInputs
        Public Property Commit As Boolean
    End Class
    Private creator As DBTimeLiner = Nothing
    Private Sub CreateTimeLineDB(inputs As CreateTimeLineDBInputs)
        Dim per As New myPersister

        creator = New DBTimeLiner(eDBType.TransactSQL, New DBSqlGeneratorFactory)
        AddHandler creator.BatchExecuting, AddressOf BatchExecutingHandler
        AddHandler creator.BatchExecuted, AddressOf BatchExecutedHandler
        AddHandler creator.ModuleLoaded, AddressOf ModuleLoadedHandler

        creator.CreateSystemObjects()

        ' TODO - popraviti always execute tasks
        ' TODO - module dodavati reflectionom citajuci dll-ove iz app foldera. dodati property dll name u module tablicu ili slicno
        '      - smisao je da samo postojanje dll-a odradjuje posao, fleg active ga moze ukljuciti ili iskljuciti
        ' TODO - isprogramirati podrsku za triggere
        ' TODO - odraditi code generation adventureWorks baze
        ' TODO - odraditi novi persister do kraja (snimanje, cacheiranje shema i sl.)
        ' TODO - u novom persisteru maknuti implementation u posebni dll
        ' TODO - testirati "deklarativno" programiranje, vise puta pozvati isti modul
        ' TODO - isprogramirati podršku za Role

        ' CONSIDER - db objekte (vieove, tablice, itd) drzati u posebnim classama koje se mogu MEFom aktivirati

        creator.LoadModulesFromDB()

        For i As Integer = 0 To creator.DBModules.Count - 1
            creator.DBModules(i).LoadRevisions()
        Next

        Using cnn As Common.DbConnection = MRC.GetConnection()
            cnn.Open()

            Using trn As Common.DbTransaction = cnn.BeginTransaction

                creator.LoadExecutedDBSqlRevisionsFromDB(cnn, trn)

                creator.ExecuteDBSqlRevisions(cnn, trn)

                Dim newDBSqlRevisions As List(Of DBSqlRevision) = creator.SourceDBSqlRevisions.Except(creator.ExecutedDBSqlRevisions, New DBSqlRevision.DBSqlRevisionEqualityComparer).ToList
                newDBSqlRevisions.Sort(AddressOf DBSqlRevision.CompareRevisionsForDbCreations)

                'Dim imaUBaziNemaUSource = creator.ExecutedDBSqlRevisions.Except(creator.SourceDBSqlRevisions).ToList()
                'Dim unija = imaUSourceuNemaUBazi.Union(imaUBaziNemaUSource).ToList()

                If inputs.Commit Then
                    trn.Commit()
                Else
                    trn.Rollback()
                End If
            End Using

        End Using

        RemoveHandler creator.BatchExecuted, AddressOf BatchExecutedHandler
        RemoveHandler creator.BatchExecuting, AddressOf BatchExecutingHandler
        RemoveHandler creator.ModuleLoaded, AddressOf ModuleLoadedHandler
    End Sub

#Region "Thread backworker"
    ' TODO - ovo odraditi reactive programmingom
    Private Sub DoWork(sender As Object, e As DoWorkEventArgs) Handles backWorker.DoWork
        backWorker.ReportProgress(0)
        Try
            CreateTimeLineDB(DirectCast(e.Argument, CreateTimeLineDBInputs))
        Catch ex As Exception
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
            WriteException(ex)
        End Try
    End Sub

    Private Sub backWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles backWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub backWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles backWorker.RunWorkerCompleted
        ProgressBar1.Value = 100
        ts2 = New TimeSpan(Now.Ticks)

        WriteTextToRtb(rtb1, "-- Total time: " & (ts2 - ts1).ToString() & vbNewLine & vbNewLine, Color.LightBlue)
        FillTreeView()
    End Sub

#End Region

#Region "Extras"
    Private Sub FillTreeView()
        treeRevisions.Nodes.Clear()
        Dim root As TreeNode = treeRevisions.Nodes.Add("root", "DBTimeLiner")
        Dim modules As TreeNode = root.Nodes.Add("Modules")

        For Each dBModule As IDBModule In creator.DBModules
            Dim nodModule = modules.Nodes.Add(dBModule.ModuleKey)
            For Each dBObject As IDBObject In dBModule.DBObjects.Values
                nodModule.Nodes.Add(dBObject.GetFullName)
                If TypeOf dBObject Is IDBParent Then

                End If
            Next
        Next
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

#End Region

End Class
