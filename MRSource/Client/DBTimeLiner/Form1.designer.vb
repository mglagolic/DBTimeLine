﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node0")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node2")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node3")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node4")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node5")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node1", New System.Windows.Forms.TreeNode() {TreeNode2, TreeNode3, TreeNode4, TreeNode5})
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DBTimeLiner", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode6})
        Me.rtb1 = New System.Windows.Forms.RichTextBox()
        Me.btnCommit = New System.Windows.Forms.Button()
        Me.backWorker = New System.ComponentModel.BackgroundWorker()
        Me.zoomRtb = New System.Windows.Forms.TrackBar()
        Me.pnlRtb = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.treeRevisions = New System.Windows.Forms.TreeView()
        Me.pnlControl = New System.Windows.Forms.Panel()
        Me.btnRollback = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnAnalyze = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.gbSteps = New System.Windows.Forms.GroupBox()
        Me.StepProgressBar1 = New Framework.GUI.Controls.StepProgressBar()
        CType(Me.zoomRtb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRtb.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pnlControl.SuspendLayout()
        Me.gbSteps.SuspendLayout()
        Me.SuspendLayout()
        '
        'rtb1
        '
        Me.rtb1.AutoWordSelection = True
        Me.rtb1.BackColor = System.Drawing.Color.Black
        Me.rtb1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtb1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtb1.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.rtb1.ForeColor = System.Drawing.Color.LawnGreen
        Me.rtb1.Location = New System.Drawing.Point(0, 0)
        Me.rtb1.Name = "rtb1"
        Me.rtb1.ReadOnly = True
        Me.rtb1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth
        Me.rtb1.ShowSelectionMargin = True
        Me.rtb1.Size = New System.Drawing.Size(581, 542)
        Me.rtb1.TabIndex = 0
        Me.rtb1.Text = "bok" & Global.Microsoft.VisualBasic.ChrW(10) & "kaj" & Global.Microsoft.VisualBasic.ChrW(10) & "ima"
        Me.rtb1.WordWrap = False
        '
        'btnCommit
        '
        Me.btnCommit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCommit.BackColor = System.Drawing.Color.Maroon
        Me.btnCommit.ForeColor = System.Drawing.Color.White
        Me.btnCommit.Location = New System.Drawing.Point(990, 3)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(164, 48)
        Me.btnCommit.TabIndex = 1
        Me.btnCommit.Text = "Commit"
        Me.btnCommit.UseVisualStyleBackColor = False
        '
        'backWorker
        '
        Me.backWorker.WorkerReportsProgress = True
        Me.backWorker.WorkerSupportsCancellation = True
        '
        'zoomRtb
        '
        Me.zoomRtb.AutoSize = False
        Me.zoomRtb.Dock = System.Windows.Forms.DockStyle.Right
        Me.zoomRtb.LargeChange = 10
        Me.zoomRtb.Location = New System.Drawing.Point(561, 0)
        Me.zoomRtb.Maximum = 50
        Me.zoomRtb.Minimum = 10
        Me.zoomRtb.Name = "zoomRtb"
        Me.zoomRtb.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.zoomRtb.Size = New System.Drawing.Size(20, 542)
        Me.zoomRtb.SmallChange = 5
        Me.zoomRtb.TabIndex = 4
        Me.zoomRtb.TickFrequency = 5
        Me.zoomRtb.TickStyle = System.Windows.Forms.TickStyle.None
        Me.zoomRtb.Value = 30
        '
        'pnlRtb
        '
        Me.pnlRtb.Controls.Add(Me.zoomRtb)
        Me.pnlRtb.Controls.Add(Me.rtb1)
        Me.pnlRtb.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRtb.Location = New System.Drawing.Point(286, 64)
        Me.pnlRtb.Name = "pnlRtb"
        Me.pnlRtb.Size = New System.Drawing.Size(581, 542)
        Me.pnlRtb.TabIndex = 5
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(867, 64)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(297, 542)
        Me.Panel2.TabIndex = 7
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.treeRevisions)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(294, 204)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Revisions"
        '
        'treeRevisions
        '
        Me.treeRevisions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeRevisions.Location = New System.Drawing.Point(3, 16)
        Me.treeRevisions.Name = "treeRevisions"
        TreeNode1.BackColor = System.Drawing.Color.CornflowerBlue
        TreeNode1.Name = "Node0"
        TreeNode1.Text = "Node0"
        TreeNode2.BackColor = System.Drawing.Color.LightGoldenrodYellow
        TreeNode2.Name = "Node2"
        TreeNode2.Text = "Node2"
        TreeNode3.Name = "Node3"
        TreeNode3.Text = "Node3"
        TreeNode4.Name = "Node4"
        TreeNode4.Text = "Node4"
        TreeNode5.Name = "Node5"
        TreeNode5.Text = "Node5"
        TreeNode6.BackColor = System.Drawing.Color.CornflowerBlue
        TreeNode6.Name = "Node1"
        TreeNode6.Text = "Node1"
        TreeNode7.BackColor = System.Drawing.Color.DarkSeaGreen
        TreeNode7.Checked = True
        TreeNode7.Name = "Node0"
        TreeNode7.Text = "DBTimeLiner"
        Me.treeRevisions.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode7})
        Me.treeRevisions.Size = New System.Drawing.Size(288, 185)
        Me.treeRevisions.TabIndex = 0
        '
        'pnlControl
        '
        Me.pnlControl.BackColor = System.Drawing.Color.CornflowerBlue
        Me.pnlControl.Controls.Add(Me.btnRollback)
        Me.pnlControl.Controls.Add(Me.Button1)
        Me.pnlControl.Controls.Add(Me.btnAnalyze)
        Me.pnlControl.Controls.Add(Me.btnCommit)
        Me.pnlControl.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlControl.Location = New System.Drawing.Point(10, 10)
        Me.pnlControl.Name = "pnlControl"
        Me.pnlControl.Size = New System.Drawing.Size(1154, 54)
        Me.pnlControl.TabIndex = 8
        '
        'btnRollback
        '
        Me.btnRollback.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRollback.BackColor = System.Drawing.Color.Blue
        Me.btnRollback.ForeColor = System.Drawing.Color.White
        Me.btnRollback.Location = New System.Drawing.Point(335, 3)
        Me.btnRollback.Name = "btnRollback"
        Me.btnRollback.Size = New System.Drawing.Size(111, 48)
        Me.btnRollback.TabIndex = 9
        Me.btnRollback.Text = "Rollback"
        Me.btnRollback.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.Blue
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(3, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 48)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Analyze frmProgress"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnAnalyze
        '
        Me.btnAnalyze.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnalyze.BackColor = System.Drawing.Color.Blue
        Me.btnAnalyze.ForeColor = System.Drawing.Color.White
        Me.btnAnalyze.Location = New System.Drawing.Point(220, 3)
        Me.btnAnalyze.Name = "btnAnalyze"
        Me.btnAnalyze.Size = New System.Drawing.Size(109, 48)
        Me.btnAnalyze.TabIndex = 7
        Me.btnAnalyze.Text = "Analyze"
        Me.btnAnalyze.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(10, 606)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1154, 36)
        Me.Panel5.TabIndex = 10
        '
        'gbSteps
        '
        Me.gbSteps.BackColor = System.Drawing.Color.White
        Me.gbSteps.Controls.Add(Me.StepProgressBar1)
        Me.gbSteps.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbSteps.Location = New System.Drawing.Point(10, 64)
        Me.gbSteps.Name = "gbSteps"
        Me.gbSteps.Size = New System.Drawing.Size(276, 542)
        Me.gbSteps.TabIndex = 1
        Me.gbSteps.TabStop = False
        Me.gbSteps.Text = "Steps"
        '
        'StepProgressBar1
        '
        Me.StepProgressBar1.CurrentStepIndex = -1
        Me.StepProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StepProgressBar1.Location = New System.Drawing.Point(3, 16)
        Me.StepProgressBar1.Name = "StepProgressBar1"
        Me.StepProgressBar1.Size = New System.Drawing.Size(270, 523)
        Me.StepProgressBar1.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1174, 652)
        Me.Controls.Add(Me.pnlRtb)
        Me.Controls.Add(Me.gbSteps)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlControl)
        Me.Controls.Add(Me.Panel5)
        Me.Name = "Form1"
        Me.Padding = New System.Windows.Forms.Padding(10)
        Me.Text = "Form1"
        CType(Me.zoomRtb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRtb.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.pnlControl.ResumeLayout(False)
        Me.gbSteps.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents rtb1 As RichTextBox
    Friend WithEvents btnCommit As Button
    Friend WithEvents backWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents zoomRtb As TrackBar
    Friend WithEvents pnlRtb As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents pnlControl As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents treeRevisions As TreeView
    Friend WithEvents gbSteps As GroupBox
    Friend WithEvents btnAnalyze As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents btnRollback As Button
    Friend WithEvents StepProgressBar1 As Framework.GUI.Controls.StepProgressBar
End Class
