Option Strict On
Imports Framework.GUI.Helpers
Imports Framework.Persisting
Imports MRFramework.MRPersisting.Factory
Imports Framework.DBTimeLine
Imports System.ComponentModel
Imports Framework.DBTimeLine.DBObjects
Imports Customizations.Core.EventArgs
Imports DBTimeLiners.DBModules.EventArgs

Public Class Form1

    Dim ts1 As TimeSpan
    Dim ts2 As TimeSpan

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MRC.GetInstance().ConnectionString = CType(My.Settings.Item(My.Settings.DefaultConnectionString), String)
        MRC.GetInstance().ProviderName = CType(My.Settings.Item(My.Settings.DefaultProvider), String)
        PersistingSettings.Instance.SqlGeneratorFactory = New Implementation.SqlGeneratorFactory()

    End Sub

#Region "Writing rtb"
    Private Sub BatchExecutingHandler(sender As Object, e As BatchExecutingEventArgs)
        Dim executingLine As String = "-- EXECUTING ... {0}"
        Dim info As String = ""
        If e.DbSqlRevisions IsNot Nothing Then
            For Each sqlRev As DBSqlRevision In e.DbSqlRevisions
                info += String.Format("* {0} {1} ({2}), {3} ", sqlRev.Parent.Parent.ObjectTypeName, sqlRev.Parent.Parent.GetFullName(), sqlRev.Parent.Created.ToString("yyyy, M, d"), sqlRev.Parent.Granulation)
            Next
            executingLine = String.Format(executingLine, info.Trim)
        End If
        String.Format(executingLine, info)

        WriteTextToRtb(rtb1, executingLine & vbNewLine, Color.Yellow)
        WriteTextToRtb(rtb1, e.Sql & vbNewLine, Color.LawnGreen)
    End Sub

    Private Sub BatchExecutedHandler(sender As Object, e As BatchExecutedEventArgs)
        If e.ResultType = eBatchExecutionResultType.Failed Then
            If e.Exception IsNot Nothing Then
                WriteException(e.Exception)
            Else
                WriteTextToRtb(rtb1, "/* " & e.ErrorMessage.ToString() & "*/" & vbNewLine, Color.Green)
            End If
        End If
        WriteTextToRtb(rtb1, "-- Duration: " & e.Duration.ToString() & vbNewLine & vbNewLine, Color.Yellow)

    End Sub

    Private Sub ProgressReportedHandler(sender As Object, e As ProgressReportedEventArgs)
        If e.TotalSteps <> 0 Then
            StepProgressBar1.ReportProgress(CInt(e.CurrentStep / (e.TotalSteps + 1) * 100))
        End If
    End Sub

    Private Sub ModuleLoadedHandler(sender As Object, e As ModuleLoadedEventArgs)
        If e.Message <> "" Then
            WriteTextToRtb(rtb1, "/* " & e.Message.ToString() & "*/" & vbNewLine, Color.Yellow)
        End If
        If e.ErrorMessage <> "" Then
            WriteTextToRtb(rtb1, "/* " & e.ErrorMessage.ToString() & "*/" & vbNewLine, Color.Red)
        End If
    End Sub

    Private Sub CustomizationLoadedHandler(sender As Object, e As CustomizationLoadedEventArgs)
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
        Dim pars As New ArrayList
        pars.Add(text)
        pars.Add(color)

        CrossThreadingHelpers.InvokeControl(rtb, pars, Sub(x)
                                                           Dim input As ArrayList = CType(x, ArrayList)
                                                           rtb.SelectionColor = CType(input(1), Color)
                                                           rtb.AppendText(CType(input(0), String))
                                                           rtb.ScrollToCaret()
                                                       End Sub)

    End Sub

    Delegate Sub WriteErrorToMessageBoxCallback(text As String)
    Private Sub WriteErrorToMessageBox(text As String)
        Dim pars As New ArrayList
        pars.Add(text)
        CrossThreadingHelpers.InvokeControl(Me, pars, Sub(x)
                                                          Dim str As String = CStr(DirectCast(x, ArrayList)(0))
                                                          MessageBox.Show(str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                      End Sub)
    End Sub

#End Region

    Private creator As DBTimeLiner = Nothing
    Private customizationLoader As Customizations.Core.Loader = Nothing
    Private moduleLoader As DBTimeLiners.DBModules.Loader = Nothing

    Private Class CreateTimeLineDBInputs
        Public Property ActionType As eActionType = 0
    End Class

    Public Enum eActionType
        None = -1
        Analyze = 0
        Rollback = 1
        Commit = 2
    End Enum

    Private Sub CreateTimeLineDB(worker As BackgroundWorker, inputs As CreateTimeLineDBInputs)
        creator = New DBTimeLiner(eDBType.TransactSQL, New DBSqlGeneratorFactory, worker)
        customizationLoader = New Customizations.Core.Loader()
        moduleLoader = New DBTimeLiners.DBModules.Loader()
        Dim cnn As Common.DbConnection = Nothing
        Dim trn As Common.DbTransaction = Nothing

        Try
            AddHandler creator.BatchExecuting, AddressOf BatchExecutingHandler
            AddHandler creator.BatchExecuted, AddressOf BatchExecutedHandler
            AddHandler moduleLoader.ModuleLoaded, AddressOf ModuleLoadedHandler
            AddHandler creator.ProgressReported, AddressOf ProgressReportedHandler
            AddHandler customizationLoader.CustomizationLoaded, AddressOf CustomizationLoadedHandler

            If worker.CancellationPending Then
                Exit Sub
            End If

            creator.CreateSystemObjects()
            StepProgressBar1.NextStep(True, 250)

            If worker.CancellationPending Then
                Exit Sub
            End If

            ' TODO - dodati novi eRevisionType = AlwaysExecuteCreate (koristiti za viewove i procedure. omoguciti ucitavanje body-a iz fajla .sql)
            ' TODO - napraviti prozor za commit naredbi s generiranjem infa tko je odradio i sto. Slati info na mail.
            ' TODO - omoguciti prikaz novih revizija, bez executea
            ' TODO - testirati "deklarativno" programiranje, vise puta pozvati isti modul
            ' TODO - isprogramirati podrsku za triggere
            ' TODO - odraditi novi persister do kraja (snimanje, cacheiranje shema i sl.)
            ' CONSIDER - u novom persisteru maknuti implementation u posebni dll 
            ' TODO - isprogramirati podršku za Role

            ' CONSIDER - db objekte (vieove, tablice, itd) drzati u posebnim classama koje se mogu MEFom aktivirati
            ' CONSIDER - odraditi code generation adventureWorks baze

            'Dim dbo As New DBTimeLiners.DBModules.dbo

            moduleLoader.LoadModulesFromDB(creator)

            If worker.CancellationPending Then
                Exit Sub
            End If

            customizationLoader.LoadCustomizers()

            If worker.CancellationPending Then
                Exit Sub
            End If

            StepProgressBar1.NextStep(True, 250)

            For i As Integer = 0 To moduleLoader.DBModules.Count - 1
                If worker.CancellationPending Then
                    Exit Sub
                End If

                moduleLoader.DBModules(i).LoadRevisions()
            Next

            Dim callMethodsInputs As New Dictionary(Of String, Object)
            callMethodsInputs.Add("DBTimeLiner", creator)
            customizationLoader.CallMethods("CreateTimeLine", callMethodsInputs)

            If worker.CancellationPending Then
                Exit Sub
            End If

            Try
                cnn = MRC.GetConnection
                cnn.Open()
                trn = cnn.BeginTransaction

                StepProgressBar1.NextStep(True, 250)
                creator.LoadExecutedDBSqlRevisionsFromDB(cnn, trn)

                If worker.CancellationPending Then
                    Exit Sub
                End If

                If inputs.ActionType = eActionType.Commit OrElse inputs.ActionType = eActionType.Rollback Then
                    StepProgressBar1.NextStep(False)
                    creator.ExecuteDBSqlRevisions(cnn, trn)

                    StepProgressBar1.NextStep(False)
                    creator.ExecuteDBSqlRevisionsAlwaysExecutingTasks(cnn, trn)

                    StepProgressBar1.NextStep(True, 250)
                    If inputs.ActionType = eActionType.Commit Then
                        If worker.CancellationPending Then
                            Exit Sub
                        End If

                        ts1 = Now.TimeOfDay
                        trn.Commit()
                        ts2 = Now.TimeOfDay
                        WriteTextToRtb(rtb1, "-- Transaction commited. Duration: " & (ts2 - ts1).ToString() & vbNewLine, Color.Blue)
                    Else
                        Dim ts1 As TimeSpan
                        Dim ts2 As TimeSpan
                        Try
                            ts1 = Now.TimeOfDay
                            trn.Rollback()
                            ts2 = Now.TimeOfDay
                            WriteTextToRtb(rtb1, "-- Transaction rolled back. Duration: " & (ts2 - ts1).ToString() & vbNewLine, Color.Orange)
                        Catch sqlEx As SqlClient.SqlException
                            ts2 = Now.TimeOfDay
                            If sqlEx.Number = -2 Then
                                If Debugger.IsAttached Then
                                    Debugger.Break()
                                End If
                                WriteTextToRtb(rtb1, "-- Timeout expired during transaction rolling back. Check database activity monitory for residual locks." & vbNewLine & sqlEx.Message, Color.Orange)
                            Else
                                Throw
                            End If
                        Catch ex As Exception
                            Throw
                        End Try

                    End If
                End If
            Finally
                If trn IsNot Nothing AndAlso trn.Connection IsNot Nothing Then
                    Try
                        trn.Rollback()
                    Catch ex As Exception
                        ' samo progutaj
                    End Try
                    trn.Dispose()
                    trn = Nothing
                End If
                If cnn IsNot Nothing Then
                    cnn.Dispose()
                    cnn = Nothing
                End If
            End Try

        Finally
            If creator IsNot Nothing Then
                RemoveHandler creator.BatchExecuted, AddressOf BatchExecutedHandler
                RemoveHandler creator.BatchExecuting, AddressOf BatchExecutingHandler
                RemoveHandler moduleLoader.ModuleLoaded, AddressOf ModuleLoadedHandler
                RemoveHandler creator.ProgressReported, AddressOf ProgressReportedHandler
                RemoveHandler customizationLoader.CustomizationLoaded, AddressOf CustomizationLoadedHandler
            End If
        End Try
    End Sub



#Region "Extras"

    Private Sub FillTreeView()
        treeDatabaseObjects.Nodes.Clear()
        Dim root As TreeNode = treeDatabaseObjects.Nodes.Add("root", "DBTimeLiner")
        root.BackColor = Color.DarkSeaGreen
        If creator IsNot Nothing Then
            For Each dBModule As IDBModule In creator.DBModules
                Dim nodModule = root.Nodes.Add(dBModule.ModuleKey & " (Module)")
                nodModule.BackColor = Color.CornflowerBlue

                FillTreeViewRecursive(nodModule, dBModule, 0)
            Next
        End If
    End Sub

    Private Function GetNodeColor(level As Integer) As Color
        Dim lsColor As New List(Of Color)
        lsColor.Add(Color.LightGoldenrodYellow)

        Dim index As Integer = level Mod lsColor.Count

        Return lsColor(index)
    End Function

    Private Sub FillTreeViewRecursive(currentNode As TreeNode, dbParent As IDBParent, level As Integer)
        For Each dBObject As IDBObject In dbParent.DBObjects.Values
            Dim treeNode = currentNode.Nodes.Add(dBObject.GetFullName & " (" & dBObject.ObjectTypeName & ")")

            treeNode.BackColor = GetNodeColor(level)

            If TypeOf dBObject Is IDBParent Then
                FillTreeViewRecursive(treeNode, dBObject, level + 1)
            End If
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

#Region "DoWork"
    Private Sub actionButton_Click(sender As Object, e As EventArgs) Handles btnAnalyze.Click, btnCommit.Click, btnRollback.Click, AnalyzeToolStripMenuItem.Click, RollbackToolStripMenuItem.Click, CommitToolStripMenuItem.Click
        Dim actionType As eActionType = eActionType.None
        If sender Is btnAnalyze OrElse sender Is AnalyzeToolStripMenuItem Then
            actionType = eActionType.Analyze
        ElseIf sender Is btnRollback OrElse sender Is RollbackToolStripMenuItem Then
            actionType = eActionType.Rollback
        ElseIf sender Is btnCommit OrElse sender Is CommitToolStripMenuItem Then
            actionType = eActionType.Commit
        End If
        If actionType <> eActionType.None Then
            ts1 = New TimeSpan(Now.Ticks)
            pnlControl.Enabled = False
            MenuStrip1.Enabled = False

            rtb1.Text = ""
            WriteTextToRtb(rtb1, "...Started (" & Now.ToString("yyyy-MM-dd hh:mm.sss") & ")" & vbNewLine, Color.Yellow)

            InitSteps()
            Dim inputs As New CreateTimeLineDBInputs With {.ActionType = actionType}
            StepProgressBar1.StartWork(inputs)
        End If
    End Sub

#End Region

#Region "Progress"
    Private Sub InitSteps()
        StepProgressBar1.CurrentStepIndex = -1
        With StepProgressBar1.Grid
            .Groups.Clear()
            .Items.Clear()

            Dim grpInit As New ListViewGroup() With {.Header = "Initializing", .Name = "1"}
            Dim grpCreate As New ListViewGroup() With {.Header = "Creating timeline", .Name = "2"}
            Dim grpApply As New ListViewGroup() With {.Header = "Applying timeline", .Name = "3"}
            Dim grpFinish As New ListViewGroup() With {.Header = "finishing tasks", .Name = "4"}

            .Groups.Add(grpInit)
            .Groups.Add(grpCreate)
            .Groups.Add(grpApply)
            .Groups.Add(grpFinish)

            .Items.Add(New ListViewItem() With {.Group = grpInit, .Tag = "1", .Text = "Checking system objects"})
            .Items.Add(New ListViewItem() With {.Group = grpCreate, .Tag = "2", .Text = "Loading modules"})
            .Items.Add(New ListViewItem() With {.Group = grpCreate, .Tag = "3", .Text = "Loading customizations"})
            .Items.Add(New ListViewItem() With {.Group = grpApply, .Tag = "4", .Text = "Applying changes"})
            .Items.Add(New ListViewItem() With {.Group = grpApply, .Tag = "5", .Text = "Running always execute tasks"})
            .Items.Add(New ListViewItem() With {.Group = grpFinish, .Tag = "6", .Text = "Finishing work"})
        End With
    End Sub

    Private Sub StepProgressBar1_DoWork(sender As Object, e As DoWorkEventArgs) Handles StepProgressBar1.DoWork
        StepProgressBar1.ReportProgress(0)
        Try
            CreateTimeLineDB(CType(sender, BackgroundWorker), DirectCast(e.Argument, CreateTimeLineDBInputs))
        Catch ex As Exception
            'WriteException(ex)
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
            WriteErrorToMessageBox(ex.Message & vbNewLine & "StackTrace:" & vbNewLine & ex.StackTrace)
        End Try
    End Sub

    Private Sub StepProgressBar1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles StepProgressBar1.RunWorkerCompleted
        ts2 = New TimeSpan(Now.Ticks)

        WriteTextToRtb(rtb1, "-- Total time: " & (ts2 - ts1).ToString() & vbNewLine, Color.LightBlue)
        WriteTextToRtb(rtb1, "...Finished (" & Now.ToString("yyyy-dd-MM hh:mm.sss") & ")", Color.Yellow)
        pnlControl.Enabled = True
        MenuStrip1.Enabled = True
        FillTreeView()
        FillNewRevisions()
    End Sub

    Private Sub StepProgressBar1_Aborted(sender As Object, e As EventArgs) Handles StepProgressBar1.Aborted
        ts2 = New TimeSpan(Now.Ticks)
        WriteTextToRtb(rtb1, "-- Trying to cancel worker: " & (ts2 - ts1).ToString() & vbNewLine, Color.LightBlue)
    End Sub

    Private Sub MenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        If sender Is ExitToolStripMenuItem Then
            Close()
        End If
    End Sub

#End Region

    Private Sub FillNewRevisions()
        'Dim newDBSqlRevisions As List(Of DBSqlRevision) = creator.SourceDBSqlRevisions.Where(Function(rev) rev.RevisionType <> eDBRevisionType.AlwaysExecuteTask).Except(creator.ExecutedDBSqlRevisions, New DBSqlRevision.DBSqlRevisionEqualityComparer).ToList
        Dim newDBSqlRevisions As List(Of DBSqlRevision) = creator.SourceDBSqlRevisions.Except(creator.ExecutedDBSqlRevisions, New DBSqlRevision.DBSqlRevisionEqualityComparer).ToList
        newDBSqlRevisions.Sort(AddressOf DBSqlRevision.CompareRevisionsForDbCreations)

        Dim root As TreeNode = treeNewRevisions.TopNode
        root.Nodes.Clear()

        For Each sqlRev As DBSqlRevision In newDBSqlRevisions
            Dim rev As IDBRevision = sqlRev.Parent
            Dim text As String = rev.Created.ToString("yyyy-MM-dd") & " - " & rev.Granulation.ToString & " - " & sqlRev.RevisionTypeName & " - " & sqlRev.ObjectFullName
            Dim newNode As New TreeNode(text)
            root.Nodes.Add(newNode)
        Next
        'Dim imaUBaziNemaUSource = creator.ExecutedDBSqlRevisions.Except(creator.SourceDBSqlRevisions).ToList()
        'Dim unija = imaUSourceuNemaUBazi.Union(imaUBaziNemaUSource).ToList()

    End Sub

End Class
