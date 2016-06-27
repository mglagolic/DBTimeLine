Option Strict On

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
        WriteTextToRtb(rtb1, "-- EXECUTING..." & vbNewLine, Color.Yellow)
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
            backWorker.ReportProgress(CInt(e.CurrentStep / e.TotalSteps * 100))
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
        If rtb.InvokeRequired Then
            Dim d As New WriteTextToRtbCallback(AddressOf WriteTextToRtb)
            rtb.Invoke(d, rtb, text, color)
            Exit Sub
        End If

        rtb.SelectionColor = color
        rtb.AppendText(text)
        rtb.ScrollToCaret()
    End Sub

    Delegate Sub WriteErrorToMessageBoxCallback(text As String)
    Private Sub WriteErrorToMessageBox(text As String)
        If InvokeRequired Then
            Dim d As New WriteErrorToMessageBoxCallback(AddressOf WriteErrorToMessageBox)
            Invoke(d, text)
            Exit Sub
        End If
        MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

#End Region

    Private Class CreateTimeLineDBInputs
        Public Property Commit As Boolean
        Public Property Analyze As Boolean
    End Class

    Private creator As DBTimeLiner = Nothing
    Private customizationLoader As Customizations.Core.Loader = Nothing
    Private moduleLoader As DBTimeLiners.DBModules.Loader = Nothing

    Private Sub CreateTimeLineDB(inputs As CreateTimeLineDBInputs)
        creator = New DBTimeLiner(eDBType.TransactSQL, New DBSqlGeneratorFactory)
        customizationLoader = New Customizations.Core.Loader()
        moduleLoader = New DBTimeLiners.DBModules.Loader()

        AddHandler creator.BatchExecuting, AddressOf BatchExecutingHandler
        AddHandler creator.BatchExecuted, AddressOf BatchExecutedHandler
        AddHandler moduleLoader.ModuleLoaded, AddressOf ModuleLoadedHandler
        AddHandler creator.ProgressReported, AddressOf ProgressReportedHandler
        AddHandler customizationLoader.CustomizationLoaded, AddressOf CustomizationLoadedHandler

        creator.CreateSystemObjects()

        ' TODO - popraviti progress bar 
        ' TODO - omoguciti samo dohvat novih revizija, bez executea
        ' TODO - prikazati module u gridu
        ' TODO - isprogramirati podrsku za triggere
        ' TODO - odraditi novi persister do kraja (snimanje, cacheiranje shema i sl.)
        ' TODO - u novom persisteru maknuti implementation u posebni dll
        ' TODO - testirati "deklarativno" programiranje, vise puta pozvati isti modul
        ' TODO - isprogramirati podršku za Role

        ' CONSIDER - db objekte (vieove, tablice, itd) drzati u posebnim classama koje se mogu MEFom aktivirati
        ' CONSIDER - odraditi code generation adventureWorks baze

        'Dim dbo As New DBTimeLiners.DBModules.dbo

        moduleLoader.LoadModulesFromDB(creator)
        customizationLoader.LoadCustomizers()

        'For i As Integer = 0 To creator.DBModules.Count - 1
        '    creator.DBModules(i).LoadRevisions()
        'Next

        For i As Integer = 0 To moduleLoader.DBModules.Count - 1
            moduleLoader.DBModules(i).LoadRevisions()
        Next

        ' TODO - ovdje pozivati customizere i njihove metode "CreateTimeLine"
        Dim callMethodsInputs As New Dictionary(Of String, Object)
        callMethodsInputs.Add("DBTimeLiner", creator)
        customizationLoader.CallMethods("CreateTimeLine", callMethodsInputs)

        Using cnn As Common.DbConnection = MRC.GetConnection()
            cnn.Open()

            Using trn As Common.DbTransaction = cnn.BeginTransaction

                creator.LoadExecutedDBSqlRevisionsFromDB(cnn, trn)

                If Not inputs.Analyze Then
                    creator.ExecuteDBSqlRevisions(cnn, trn)


                    If inputs.Commit Then
                        trn.Commit()
                    Else
                        trn.Rollback()
                    End If

                End If

                'Dim newDBSqlRevisions As List(Of DBSqlRevision) = creator.SourceDBSqlRevisions.Except(creator.ExecutedDBSqlRevisions, New DBSqlRevision.DBSqlRevisionEqualityComparer).ToList
                'newDBSqlRevisions.Sort(AddressOf DBSqlRevision.CompareRevisionsForDbCreations)

                'Dim imaUBaziNemaUSource = creator.ExecutedDBSqlRevisions.Except(creator.SourceDBSqlRevisions).ToList()
                'Dim unija = imaUSourceuNemaUBazi.Union(imaUBaziNemaUSource).ToList()

            End Using

        End Using

        RemoveHandler creator.BatchExecuted, AddressOf BatchExecutedHandler
        RemoveHandler creator.BatchExecuting, AddressOf BatchExecutingHandler
        RemoveHandler moduleLoader.ModuleLoaded, AddressOf ModuleLoadedHandler
        RemoveHandler creator.ProgressReported, AddressOf ProgressReportedHandler
        RemoveHandler customizationLoader.CustomizationLoaded, AddressOf CustomizationLoadedHandler
    End Sub

    Private Sub backWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles backWorker.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub backWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles backWorker.RunWorkerCompleted
        ProgressBar1.Value = 100
        ts2 = New TimeSpan(Now.Ticks)

        WriteTextToRtb(rtb1, "-- Total time: " & (ts2 - ts1).ToString() & vbNewLine, Color.LightBlue)
        WriteTextToRtb(rtb1, "...Finished (" & Now.ToString("yyyy-dd-MM hh:mm.sss") & ")", Color.Yellow)
        pnlControl.Enabled = True
        FillTreeView()
    End Sub

#Region "Extras"
    Private Sub FillTreeView()
        treeRevisions.Nodes.Clear()
        Dim root As TreeNode = treeRevisions.Nodes.Add("root", "DBTimeLiner")
        root.BackColor = Color.DarkSeaGreen

        For Each dBModule As IDBModule In creator.DBModules
            Dim nodModule = root.Nodes.Add(dBModule.ModuleKey & " (Module)")
            nodModule.BackColor = Color.CornflowerBlue

            FillTreeViewRecursive(nodModule, dBModule, 0)
        Next
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
    Private Sub CreateTimeLineDB(sender As Object, e As DoWorkEventArgs) Handles backWorker.DoWork
        backWorker.ReportProgress(0)
        Try
            CreateTimeLineDB(DirectCast(e.Argument, CreateTimeLineDBInputs))
        Catch ex As Exception
            'WriteException(ex)
            If Debugger.IsAttached Then
                Debugger.Break()
            End If
            WriteErrorToMessageBox(ex.Message)
        End Try
    End Sub

    Private Sub StartWorker(analyze As Boolean)
        ts1 = New TimeSpan(Now.Ticks)
        pnlControl.Enabled = False

        rtb1.Text = ""
        WriteTextToRtb(rtb1, "...Started (" & Now.ToString("yyyy-MM-dd hh:mm.sss") & ")" & vbNewLine, Color.Yellow)
        If backWorker.IsBusy Then
            backWorker.CancelAsync()
        Else
            backWorker.RunWorkerAsync(New CreateTimeLineDBInputs() With {.Commit = chxCommit.Checked, .Analyze = analyze})
        End If
    End Sub

    Private Sub btnAnalyze_Click(sender As Object, e As EventArgs) Handles btnAnalyze.Click
        StartWorker(True)
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        StartWorker(False)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim steps As New List(Of frmProgress.StepInfo)
        'steps.Add(New frmProgress.StepInfo() With {.Title = "", .MaxValue = 100, .ProgressIncrement = 1, .Worker = AddressOf CreateTimeLineDB})
        Using frm As New Form2()
            frm.ShowDialog()
        End Using

    End Sub

#End Region


End Class
