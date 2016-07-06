<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node0")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node2")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node3")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node4")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node5")
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Node1", New System.Windows.Forms.TreeNode() {TreeNode9, TreeNode10, TreeNode11, TreeNode12})
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("New revisions (Created - Granulation - DBObject - Revision type)", New System.Windows.Forms.TreeNode() {TreeNode8, TreeNode13})
        Me.rtb1 = New System.Windows.Forms.RichTextBox()
        Me.btnCommit = New System.Windows.Forms.Button()
        Me.zoomRtb = New System.Windows.Forms.TrackBar()
        Me.pnlRtb = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.treeDatabaseObjects = New System.Windows.Forms.TreeView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.treeNewRevisions = New System.Windows.Forms.TreeView()
        Me.pnlControl = New System.Windows.Forms.Panel()
        Me.btnRollback = New System.Windows.Forms.Button()
        Me.btnAnalyze = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.gbSteps = New System.Windows.Forms.GroupBox()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnalyzeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RollbackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.StepProgressBar1 = New Framework.GUI.Controls.StepProgressBar()
        CType(Me.zoomRtb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRtb.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlControl.SuspendLayout()
        Me.gbSteps.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
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
        Me.rtb1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.rtb1.ShowSelectionMargin = True
        Me.rtb1.Size = New System.Drawing.Size(613, 628)
        Me.rtb1.TabIndex = 0
        Me.rtb1.Text = "... welcome ..."
        Me.rtb1.WordWrap = False
        '
        'btnCommit
        '
        Me.btnCommit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCommit.BackColor = System.Drawing.Color.Maroon
        Me.btnCommit.ForeColor = System.Drawing.Color.White
        Me.btnCommit.Location = New System.Drawing.Point(897, 3)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(337, 48)
        Me.btnCommit.TabIndex = 1
        Me.btnCommit.Text = "Commit"
        Me.btnCommit.UseVisualStyleBackColor = False
        '
        'zoomRtb
        '
        Me.zoomRtb.AutoSize = False
        Me.zoomRtb.Dock = System.Windows.Forms.DockStyle.Right
        Me.zoomRtb.LargeChange = 10
        Me.zoomRtb.Location = New System.Drawing.Point(593, 0)
        Me.zoomRtb.Maximum = 50
        Me.zoomRtb.Minimum = 10
        Me.zoomRtb.Name = "zoomRtb"
        Me.zoomRtb.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.zoomRtb.Size = New System.Drawing.Size(20, 628)
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
        Me.pnlRtb.Location = New System.Drawing.Point(286, 88)
        Me.pnlRtb.Name = "pnlRtb"
        Me.pnlRtb.Padding = New System.Windows.Forms.Padding(0, 0, 8, 0)
        Me.pnlRtb.Size = New System.Drawing.Size(621, 628)
        Me.pnlRtb.TabIndex = 5
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(907, 88)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(347, 628)
        Me.Panel2.TabIndex = 7
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.treeDatabaseObjects)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 204)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 3, 10, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(347, 424)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Database objects"
        '
        'treeDatabaseObjects
        '
        Me.treeDatabaseObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeDatabaseObjects.Location = New System.Drawing.Point(3, 16)
        Me.treeDatabaseObjects.Name = "treeDatabaseObjects"
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
        Me.treeDatabaseObjects.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode7})
        Me.treeDatabaseObjects.Size = New System.Drawing.Size(334, 405)
        Me.treeDatabaseObjects.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.treeNewRevisions)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 3, 10, 3)
        Me.GroupBox2.Size = New System.Drawing.Size(347, 204)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "New revisions"
        '
        'treeNewRevisions
        '
        Me.treeNewRevisions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeNewRevisions.Location = New System.Drawing.Point(3, 16)
        Me.treeNewRevisions.Name = "treeNewRevisions"
        TreeNode8.BackColor = System.Drawing.Color.CornflowerBlue
        TreeNode8.Name = "Node0"
        TreeNode8.Text = "Node0"
        TreeNode9.BackColor = System.Drawing.Color.LightGoldenrodYellow
        TreeNode9.Name = "Node2"
        TreeNode9.Text = "Node2"
        TreeNode10.Name = "Node3"
        TreeNode10.Text = "Node3"
        TreeNode11.Name = "Node4"
        TreeNode11.Text = "Node4"
        TreeNode12.Name = "Node5"
        TreeNode12.Text = "Node5"
        TreeNode13.BackColor = System.Drawing.Color.CornflowerBlue
        TreeNode13.Name = "Node1"
        TreeNode13.Text = "Node1"
        TreeNode14.BackColor = System.Drawing.Color.DarkSeaGreen
        TreeNode14.Checked = True
        TreeNode14.Name = "Node0"
        TreeNode14.Text = "New revisions (Created - Granulation - DBObject - Revision type)"
        Me.treeNewRevisions.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode14})
        Me.treeNewRevisions.Size = New System.Drawing.Size(334, 185)
        Me.treeNewRevisions.TabIndex = 0
        '
        'pnlControl
        '
        Me.pnlControl.BackColor = System.Drawing.Color.CornflowerBlue
        Me.pnlControl.Controls.Add(Me.btnRollback)
        Me.pnlControl.Controls.Add(Me.btnAnalyze)
        Me.pnlControl.Controls.Add(Me.btnCommit)
        Me.pnlControl.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlControl.Location = New System.Drawing.Point(10, 34)
        Me.pnlControl.Name = "pnlControl"
        Me.pnlControl.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.pnlControl.Size = New System.Drawing.Size(1244, 54)
        Me.pnlControl.TabIndex = 8
        '
        'btnRollback
        '
        Me.btnRollback.BackColor = System.Drawing.Color.Blue
        Me.btnRollback.ForeColor = System.Drawing.Color.White
        Me.btnRollback.Location = New System.Drawing.Point(282, 3)
        Me.btnRollback.Name = "btnRollback"
        Me.btnRollback.Size = New System.Drawing.Size(276, 48)
        Me.btnRollback.TabIndex = 9
        Me.btnRollback.Text = "Rollback"
        Me.btnRollback.UseVisualStyleBackColor = False
        '
        'btnAnalyze
        '
        Me.btnAnalyze.BackColor = System.Drawing.Color.Blue
        Me.btnAnalyze.ForeColor = System.Drawing.Color.White
        Me.btnAnalyze.Location = New System.Drawing.Point(3, 3)
        Me.btnAnalyze.Name = "btnAnalyze"
        Me.btnAnalyze.Size = New System.Drawing.Size(273, 48)
        Me.btnAnalyze.TabIndex = 7
        Me.btnAnalyze.Text = "Analyze"
        Me.btnAnalyze.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAnalyze.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(10, 716)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1244, 36)
        Me.Panel5.TabIndex = 10
        '
        'gbSteps
        '
        Me.gbSteps.BackColor = System.Drawing.Color.White
        Me.gbSteps.Controls.Add(Me.StepProgressBar1)
        Me.gbSteps.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbSteps.Location = New System.Drawing.Point(10, 88)
        Me.gbSteps.Name = "gbSteps"
        Me.gbSteps.Size = New System.Drawing.Size(276, 628)
        Me.gbSteps.TabIndex = 1
        Me.gbSteps.TabStop = False
        Me.gbSteps.Text = "Steps"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(286, 88)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(10, 628)
        Me.Splitter1.TabIndex = 11
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(897, 88)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(10, 628)
        Me.Splitter2.TabIndex = 12
        Me.Splitter2.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ActionsToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(10, 10)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1244, 24)
        Me.MenuStrip1.TabIndex = 13
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(92, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ActionsToolStripMenuItem
        '
        Me.ActionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnalyzeToolStripMenuItem, Me.RollbackToolStripMenuItem, Me.CommitToolStripMenuItem})
        Me.ActionsToolStripMenuItem.Name = "ActionsToolStripMenuItem"
        Me.ActionsToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.ActionsToolStripMenuItem.Text = "Actions"
        '
        'AnalyzeToolStripMenuItem
        '
        Me.AnalyzeToolStripMenuItem.Name = "AnalyzeToolStripMenuItem"
        Me.AnalyzeToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.AnalyzeToolStripMenuItem.Text = "Analyze"
        '
        'RollbackToolStripMenuItem
        '
        Me.RollbackToolStripMenuItem.Name = "RollbackToolStripMenuItem"
        Me.RollbackToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.RollbackToolStripMenuItem.Text = "Rollback"
        '
        'CommitToolStripMenuItem
        '
        Me.CommitToolStripMenuItem.Name = "CommitToolStripMenuItem"
        Me.CommitToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.CommitToolStripMenuItem.Text = "Commit"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem1})
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.AboutToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem1
        '
        Me.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1"
        Me.AboutToolStripMenuItem1.Size = New System.Drawing.Size(107, 22)
        Me.AboutToolStripMenuItem1.Text = "About"
        '
        'StepProgressBar1
        '
        Me.StepProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StepProgressBar1.Location = New System.Drawing.Point(3, 16)
        Me.StepProgressBar1.Name = "StepProgressBar1"
        Me.StepProgressBar1.Size = New System.Drawing.Size(270, 609)
        Me.StepProgressBar1.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1264, 762)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlRtb)
        Me.Controls.Add(Me.gbSteps)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlControl)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Padding = New System.Windows.Forms.Padding(10)
        Me.Text = "Form1"
        CType(Me.zoomRtb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRtb.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.pnlControl.ResumeLayout(False)
        Me.gbSteps.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents rtb1 As RichTextBox
    Friend WithEvents btnCommit As Button
    Friend WithEvents zoomRtb As TrackBar
    Friend WithEvents pnlRtb As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents pnlControl As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents treeDatabaseObjects As TreeView
    Friend WithEvents gbSteps As GroupBox
    Friend WithEvents btnAnalyze As Button
    Friend WithEvents btnRollback As Button
    Friend WithEvents StepProgressBar1 As Framework.GUI.Controls.StepProgressBar
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents Splitter2 As Splitter
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ActionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AnalyzeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RollbackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CommitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents treeNewRevisions As TreeView
End Class
