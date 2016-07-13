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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("New revisions (Created - Granulation - DBObject - Revision type)")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DBTimeLiner")
        Me.rtb1 = New System.Windows.Forms.RichTextBox()
        Me.btnCommit = New System.Windows.Forms.Button()
        Me.zoomRtb = New System.Windows.Forms.TrackBar()
        Me.pnlRtb = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.gbSqlRevInfo = New System.Windows.Forms.GroupBox()
        Me.lblSqlRevInfoRevisonType = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblSqlRevInfoDBObjectType = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.rtbSqlRevInfoDescription = New System.Windows.Forms.RichTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblSqlRevInfoKey = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblSqlRevInfoModule = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.treeNewRevisions = New System.Windows.Forms.TreeView()
        Me.pnlControl = New System.Windows.Forms.Panel()
        Me.pnlConnect = New System.Windows.Forms.Panel()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.lblDatabase = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlDBTimeLinerControls = New System.Windows.Forms.Panel()
        Me.btnAnalyze = New System.Windows.Forms.Button()
        Me.btnRollback = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.gbSteps = New System.Windows.Forms.GroupBox()
        Me.StepProgressBar1 = New Framework.GUI.Controls.MRStepProgressBar()
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
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.treeDatabaseObjects = New System.Windows.Forms.TreeView()
        Me.btnHotFix = New System.Windows.Forms.Button()
        CType(Me.zoomRtb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRtb.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.gbSqlRevInfo.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlControl.SuspendLayout()
        Me.pnlConnect.SuspendLayout()
        Me.pnlDBTimeLinerControls.SuspendLayout()
        Me.gbSteps.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        Me.rtb1.Size = New System.Drawing.Size(595, 628)
        Me.rtb1.TabIndex = 0
        Me.rtb1.Text = "... welcome ..."
        Me.rtb1.WordWrap = False
        '
        'btnCommit
        '
        Me.btnCommit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCommit.BackColor = System.Drawing.Color.Maroon
        Me.btnCommit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCommit.ForeColor = System.Drawing.Color.White
        Me.btnCommit.Location = New System.Drawing.Point(1164, 3)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(66, 45)
        Me.btnCommit.TabIndex = 1
        Me.btnCommit.TabStop = False
        Me.btnCommit.Text = "Commit"
        Me.btnCommit.UseVisualStyleBackColor = False
        '
        'zoomRtb
        '
        Me.zoomRtb.AutoSize = False
        Me.zoomRtb.Dock = System.Windows.Forms.DockStyle.Right
        Me.zoomRtb.LargeChange = 10
        Me.zoomRtb.Location = New System.Drawing.Point(575, 0)
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
        Me.pnlRtb.Location = New System.Drawing.Point(300, 88)
        Me.pnlRtb.Name = "pnlRtb"
        Me.pnlRtb.Padding = New System.Windows.Forms.Padding(0, 0, 8, 0)
        Me.pnlRtb.Size = New System.Drawing.Size(603, 628)
        Me.pnlRtb.TabIndex = 5
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.gbSqlRevInfo)
        Me.Panel2.Controls.Add(Me.GroupBox2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(903, 88)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(347, 628)
        Me.Panel2.TabIndex = 7
        '
        'gbSqlRevInfo
        '
        Me.gbSqlRevInfo.Controls.Add(Me.lblSqlRevInfoRevisonType)
        Me.gbSqlRevInfo.Controls.Add(Me.Label6)
        Me.gbSqlRevInfo.Controls.Add(Me.lblSqlRevInfoDBObjectType)
        Me.gbSqlRevInfo.Controls.Add(Me.Label5)
        Me.gbSqlRevInfo.Controls.Add(Me.rtbSqlRevInfoDescription)
        Me.gbSqlRevInfo.Controls.Add(Me.Label4)
        Me.gbSqlRevInfo.Controls.Add(Me.lblSqlRevInfoKey)
        Me.gbSqlRevInfo.Controls.Add(Me.Label2)
        Me.gbSqlRevInfo.Controls.Add(Me.lblSqlRevInfoModule)
        Me.gbSqlRevInfo.Controls.Add(Me.Label3)
        Me.gbSqlRevInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbSqlRevInfo.ForeColor = System.Drawing.Color.Black
        Me.gbSqlRevInfo.Location = New System.Drawing.Point(0, 204)
        Me.gbSqlRevInfo.Name = "gbSqlRevInfo"
        Me.gbSqlRevInfo.Size = New System.Drawing.Size(347, 424)
        Me.gbSqlRevInfo.TabIndex = 3
        Me.gbSqlRevInfo.TabStop = False
        Me.gbSqlRevInfo.Text = "Selected revision info"
        '
        'lblSqlRevInfoRevisonType
        '
        Me.lblSqlRevInfoRevisonType.AutoEllipsis = True
        Me.lblSqlRevInfoRevisonType.BackColor = System.Drawing.Color.Transparent
        Me.lblSqlRevInfoRevisonType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSqlRevInfoRevisonType.Location = New System.Drawing.Point(79, 89)
        Me.lblSqlRevInfoRevisonType.Name = "lblSqlRevInfoRevisonType"
        Me.lblSqlRevInfoRevisonType.Size = New System.Drawing.Size(258, 23)
        Me.lblSqlRevInfoRevisonType.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(6, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 21)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Revision type"
        '
        'lblSqlRevInfoDBObjectType
        '
        Me.lblSqlRevInfoDBObjectType.AutoEllipsis = True
        Me.lblSqlRevInfoDBObjectType.BackColor = System.Drawing.Color.Transparent
        Me.lblSqlRevInfoDBObjectType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSqlRevInfoDBObjectType.Location = New System.Drawing.Point(79, 68)
        Me.lblSqlRevInfoDBObjectType.Name = "lblSqlRevInfoDBObjectType"
        Me.lblSqlRevInfoDBObjectType.Size = New System.Drawing.Size(258, 23)
        Me.lblSqlRevInfoDBObjectType.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(6, 70)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 21)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Object type"
        '
        'rtbSqlRevInfoDescription
        '
        Me.rtbSqlRevInfoDescription.BackColor = System.Drawing.Color.LightYellow
        Me.rtbSqlRevInfoDescription.Location = New System.Drawing.Point(79, 115)
        Me.rtbSqlRevInfoDescription.Name = "rtbSqlRevInfoDescription"
        Me.rtbSqlRevInfoDescription.ReadOnly = True
        Me.rtbSqlRevInfoDescription.Size = New System.Drawing.Size(258, 303)
        Me.rtbSqlRevInfoDescription.TabIndex = 7
        Me.rtbSqlRevInfoDescription.Text = ""
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(6, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 21)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Description"
        '
        'lblSqlRevInfoKey
        '
        Me.lblSqlRevInfoKey.AutoEllipsis = True
        Me.lblSqlRevInfoKey.BackColor = System.Drawing.Color.LightYellow
        Me.lblSqlRevInfoKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSqlRevInfoKey.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.lblSqlRevInfoKey.Location = New System.Drawing.Point(79, 22)
        Me.lblSqlRevInfoKey.Name = "lblSqlRevInfoKey"
        Me.lblSqlRevInfoKey.Size = New System.Drawing.Size(258, 23)
        Me.lblSqlRevInfoKey.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 21)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Key"
        '
        'lblSqlRevInfoModule
        '
        Me.lblSqlRevInfoModule.AutoEllipsis = True
        Me.lblSqlRevInfoModule.BackColor = System.Drawing.Color.Transparent
        Me.lblSqlRevInfoModule.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSqlRevInfoModule.Location = New System.Drawing.Point(79, 45)
        Me.lblSqlRevInfoModule.Name = "lblSqlRevInfoModule"
        Me.lblSqlRevInfoModule.Size = New System.Drawing.Size(258, 23)
        Me.lblSqlRevInfoModule.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(6, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 21)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Module key"
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.treeNewRevisions)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
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
        TreeNode1.BackColor = System.Drawing.Color.DarkSeaGreen
        TreeNode1.Checked = True
        TreeNode1.Name = "Node0"
        TreeNode1.Text = "New revisions (Created - Granulation - DBObject - Revision type)"
        Me.treeNewRevisions.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1})
        Me.treeNewRevisions.Size = New System.Drawing.Size(334, 185)
        Me.treeNewRevisions.TabIndex = 0
        '
        'pnlControl
        '
        Me.pnlControl.BackColor = System.Drawing.Color.CornflowerBlue
        Me.pnlControl.Controls.Add(Me.pnlConnect)
        Me.pnlControl.Controls.Add(Me.pnlDBTimeLinerControls)
        Me.pnlControl.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlControl.ForeColor = System.Drawing.Color.Black
        Me.pnlControl.Location = New System.Drawing.Point(10, 34)
        Me.pnlControl.Name = "pnlControl"
        Me.pnlControl.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.pnlControl.Size = New System.Drawing.Size(1240, 54)
        Me.pnlControl.TabIndex = 8
        '
        'pnlConnect
        '
        Me.pnlConnect.Controls.Add(Me.btnConnect)
        Me.pnlConnect.Controls.Add(Me.lblServer)
        Me.pnlConnect.Controls.Add(Me.lblDatabase)
        Me.pnlConnect.Controls.Add(Me.Label7)
        Me.pnlConnect.Controls.Add(Me.Label1)
        Me.pnlConnect.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlConnect.Location = New System.Drawing.Point(0, 0)
        Me.pnlConnect.Name = "pnlConnect"
        Me.pnlConnect.Size = New System.Drawing.Size(375, 54)
        Me.pnlConnect.TabIndex = 23
        '
        'btnConnect
        '
        Me.btnConnect.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnConnect.ForeColor = System.Drawing.Color.Black
        Me.btnConnect.Location = New System.Drawing.Point(212, 3)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(78, 45)
        Me.btnConnect.TabIndex = 20
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnConnect.UseVisualStyleBackColor = False
        '
        'lblServer
        '
        Me.lblServer.BackColor = System.Drawing.Color.Transparent
        Me.lblServer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblServer.ForeColor = System.Drawing.Color.Black
        Me.lblServer.Location = New System.Drawing.Point(80, 5)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(126, 21)
        Me.lblServer.TabIndex = 14
        '
        'lblDatabase
        '
        Me.lblDatabase.BackColor = System.Drawing.Color.Transparent
        Me.lblDatabase.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDatabase.ForeColor = System.Drawing.Color.Black
        Me.lblDatabase.Location = New System.Drawing.Point(80, 26)
        Me.lblDatabase.Name = "lblDatabase"
        Me.lblDatabase.Size = New System.Drawing.Size(126, 21)
        Me.lblDatabase.TabIndex = 21
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(7, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 19)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Database"
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(7, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 21)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Server"
        '
        'pnlDBTimeLinerControls
        '
        Me.pnlDBTimeLinerControls.Controls.Add(Me.btnHotFix)
        Me.pnlDBTimeLinerControls.Controls.Add(Me.btnAnalyze)
        Me.pnlDBTimeLinerControls.Controls.Add(Me.btnCommit)
        Me.pnlDBTimeLinerControls.Controls.Add(Me.btnRollback)
        Me.pnlDBTimeLinerControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDBTimeLinerControls.Location = New System.Drawing.Point(0, 0)
        Me.pnlDBTimeLinerControls.Name = "pnlDBTimeLinerControls"
        Me.pnlDBTimeLinerControls.Size = New System.Drawing.Size(1230, 54)
        Me.pnlDBTimeLinerControls.TabIndex = 22
        '
        'btnAnalyze
        '
        Me.btnAnalyze.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnalyze.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnAnalyze.ForeColor = System.Drawing.Color.White
        Me.btnAnalyze.Location = New System.Drawing.Point(893, 3)
        Me.btnAnalyze.Name = "btnAnalyze"
        Me.btnAnalyze.Size = New System.Drawing.Size(131, 45)
        Me.btnAnalyze.TabIndex = 7
        Me.btnAnalyze.Text = "Analyze"
        Me.btnAnalyze.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAnalyze.UseVisualStyleBackColor = False
        '
        'btnRollback
        '
        Me.btnRollback.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRollback.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnRollback.ForeColor = System.Drawing.Color.White
        Me.btnRollback.Location = New System.Drawing.Point(1030, 3)
        Me.btnRollback.Name = "btnRollback"
        Me.btnRollback.Size = New System.Drawing.Size(128, 45)
        Me.btnRollback.TabIndex = 9
        Me.btnRollback.Text = "Rollback"
        Me.btnRollback.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.CornflowerBlue
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(10, 716)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1240, 36)
        Me.Panel5.TabIndex = 10
        '
        'gbSteps
        '
        Me.gbSteps.BackColor = System.Drawing.Color.White
        Me.gbSteps.Controls.Add(Me.StepProgressBar1)
        Me.gbSteps.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbSteps.Location = New System.Drawing.Point(0, 0)
        Me.gbSteps.Name = "gbSteps"
        Me.gbSteps.Size = New System.Drawing.Size(290, 346)
        Me.gbSteps.TabIndex = 1
        Me.gbSteps.TabStop = False
        Me.gbSteps.Text = "Steps"
        '
        'StepProgressBar1
        '
        Me.StepProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StepProgressBar1.ForeColor = System.Drawing.Color.Black
        Me.StepProgressBar1.Location = New System.Drawing.Point(3, 16)
        Me.StepProgressBar1.Name = "StepProgressBar1"
        Me.StepProgressBar1.Size = New System.Drawing.Size(284, 327)
        Me.StepProgressBar1.TabIndex = 0
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(300, 88)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(10, 628)
        Me.Splitter1.TabIndex = 11
        Me.Splitter1.TabStop = False
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter2.Location = New System.Drawing.Point(893, 88)
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
        Me.MenuStrip1.Size = New System.Drawing.Size(1240, 24)
        Me.MenuStrip1.TabIndex = 13
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.ForeColor = System.Drawing.Color.Black
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.ForeColor = System.Drawing.Color.Black
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(92, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'ActionsToolStripMenuItem
        '
        Me.ActionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnalyzeToolStripMenuItem, Me.RollbackToolStripMenuItem, Me.CommitToolStripMenuItem})
        Me.ActionsToolStripMenuItem.ForeColor = System.Drawing.Color.Black
        Me.ActionsToolStripMenuItem.Name = "ActionsToolStripMenuItem"
        Me.ActionsToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.ActionsToolStripMenuItem.Text = "Actions"
        '
        'AnalyzeToolStripMenuItem
        '
        Me.AnalyzeToolStripMenuItem.ForeColor = System.Drawing.Color.Black
        Me.AnalyzeToolStripMenuItem.Name = "AnalyzeToolStripMenuItem"
        Me.AnalyzeToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.AnalyzeToolStripMenuItem.Text = "Analyze"
        '
        'RollbackToolStripMenuItem
        '
        Me.RollbackToolStripMenuItem.ForeColor = System.Drawing.Color.Black
        Me.RollbackToolStripMenuItem.Name = "RollbackToolStripMenuItem"
        Me.RollbackToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.RollbackToolStripMenuItem.Text = "Rollback"
        '
        'CommitToolStripMenuItem
        '
        Me.CommitToolStripMenuItem.ForeColor = System.Drawing.Color.Black
        Me.CommitToolStripMenuItem.Name = "CommitToolStripMenuItem"
        Me.CommitToolStripMenuItem.Size = New System.Drawing.Size(119, 22)
        Me.CommitToolStripMenuItem.Text = "Commit"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem1})
        Me.AboutToolStripMenuItem.ForeColor = System.Drawing.Color.Black
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.AboutToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem1
        '
        Me.AboutToolStripMenuItem1.ForeColor = System.Drawing.Color.Black
        Me.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1"
        Me.AboutToolStripMenuItem1.Size = New System.Drawing.Size(107, 22)
        Me.AboutToolStripMenuItem1.Text = "About"
        '
        'pnlLeft
        '
        Me.pnlLeft.Controls.Add(Me.GroupBox1)
        Me.pnlLeft.Controls.Add(Me.gbSteps)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlLeft.Location = New System.Drawing.Point(10, 88)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(290, 628)
        Me.pnlLeft.TabIndex = 14
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.treeDatabaseObjects)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.ForeColor = System.Drawing.Color.Black
        Me.GroupBox1.Location = New System.Drawing.Point(0, 346)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 3, 10, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(290, 282)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Database objects"
        '
        'treeDatabaseObjects
        '
        Me.treeDatabaseObjects.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeDatabaseObjects.Location = New System.Drawing.Point(3, 16)
        Me.treeDatabaseObjects.Name = "treeDatabaseObjects"
        TreeNode2.BackColor = System.Drawing.Color.DarkSeaGreen
        TreeNode2.Checked = True
        TreeNode2.Name = "Node0"
        TreeNode2.Text = "DBTimeLiner"
        Me.treeDatabaseObjects.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode2})
        Me.treeDatabaseObjects.Size = New System.Drawing.Size(277, 263)
        Me.treeDatabaseObjects.TabIndex = 0
        '
        'btnHotFix
        '
        Me.btnHotFix.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHotFix.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnHotFix.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnHotFix.ForeColor = System.Drawing.Color.Red
        Me.btnHotFix.Location = New System.Drawing.Point(381, 5)
        Me.btnHotFix.Name = "btnHotFix"
        Me.btnHotFix.Size = New System.Drawing.Size(131, 45)
        Me.btnHotFix.TabIndex = 10
        Me.btnHotFix.Text = "HotFix"
        Me.btnHotFix.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnHotFix.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1260, 762)
        Me.Controls.Add(Me.Splitter2)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.pnlRtb)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.pnlControl)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Padding = New System.Windows.Forms.Padding(10)
        Me.Text = "Form1"
        CType(Me.zoomRtb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRtb.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.gbSqlRevInfo.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.pnlControl.ResumeLayout(False)
        Me.pnlConnect.ResumeLayout(False)
        Me.pnlDBTimeLinerControls.ResumeLayout(False)
        Me.gbSteps.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
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
    Friend WithEvents gbSteps As GroupBox
    Friend WithEvents btnAnalyze As Button
    Friend WithEvents btnRollback As Button
    Friend WithEvents StepProgressBar1 As Framework.GUI.Controls.MRStepProgressBar
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
    Friend WithEvents pnlLeft As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents treeDatabaseObjects As TreeView
    Friend WithEvents gbSqlRevInfo As GroupBox
    Friend WithEvents lblSqlRevInfoModule As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblSqlRevInfoKey As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents rtbSqlRevInfoDescription As RichTextBox
    Friend WithEvents lblSqlRevInfoDBObjectType As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblSqlRevInfoRevisonType As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblServer As Label
    Friend WithEvents btnConnect As Button
    Friend WithEvents lblDatabase As Label
    Friend WithEvents pnlConnect As Panel
    Friend WithEvents pnlDBTimeLinerControls As Panel
    Friend WithEvents btnHotFix As Button
End Class
