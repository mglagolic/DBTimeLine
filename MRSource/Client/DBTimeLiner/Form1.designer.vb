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
        Me.rtb1 = New System.Windows.Forms.RichTextBox()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.backWorker = New System.ComponentModel.BackgroundWorker()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.zoomRtb = New System.Windows.Forms.TrackBar()
        Me.pnlRtb = New System.Windows.Forms.Panel()
        Me.chxCommit = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.treeRevisions = New System.Windows.Forms.TreeView()
        Me.pnlControl = New System.Windows.Forms.Panel()
        Me.btnAnalyze = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.gbModules = New System.Windows.Forms.GroupBox()
        Me.dgvModules = New System.Windows.Forms.DataGridView()
        Me.cName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSchemaName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cClassName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cAssemblyName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cCreated = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.zoomRtb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRtb.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pnlControl.SuspendLayout()
        Me.gbModules.SuspendLayout()
        CType(Me.dgvModules, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.rtb1.Size = New System.Drawing.Size(818, 347)
        Me.rtb1.TabIndex = 0
        Me.rtb1.Text = "bok" & Global.Microsoft.VisualBasic.ChrW(10) & "kaj" & Global.Microsoft.VisualBasic.ChrW(10) & "ima"
        Me.rtb1.WordWrap = False
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApply.BackColor = System.Drawing.Color.Maroon
        Me.btnApply.ForeColor = System.Drawing.Color.White
        Me.btnApply.Location = New System.Drawing.Point(1069, 42)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(85, 48)
        Me.btnApply.TabIndex = 1
        Me.btnApply.Text = "Apply"
        Me.btnApply.UseVisualStyleBackColor = False
        '
        'backWorker
        '
        Me.backWorker.WorkerReportsProgress = True
        Me.backWorker.WorkerSupportsCancellation = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ProgressBar1.ForeColor = System.Drawing.Color.SteelBlue
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 0)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1154, 36)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.TabIndex = 2
        '
        'zoomRtb
        '
        Me.zoomRtb.AutoSize = False
        Me.zoomRtb.Dock = System.Windows.Forms.DockStyle.Right
        Me.zoomRtb.LargeChange = 10
        Me.zoomRtb.Location = New System.Drawing.Point(798, 0)
        Me.zoomRtb.Maximum = 50
        Me.zoomRtb.Minimum = 10
        Me.zoomRtb.Name = "zoomRtb"
        Me.zoomRtb.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.zoomRtb.Size = New System.Drawing.Size(20, 347)
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
        Me.pnlRtb.Location = New System.Drawing.Point(10, 259)
        Me.pnlRtb.Name = "pnlRtb"
        Me.pnlRtb.Size = New System.Drawing.Size(818, 347)
        Me.pnlRtb.TabIndex = 5
        '
        'chxCommit
        '
        Me.chxCommit.AutoSize = True
        Me.chxCommit.Location = New System.Drawing.Point(12, 42)
        Me.chxCommit.Name = "chxCommit"
        Me.chxCommit.Size = New System.Drawing.Size(60, 17)
        Me.chxCommit.TabIndex = 6
        Me.chxCommit.Text = "Commit"
        Me.chxCommit.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(828, 106)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(336, 500)
        Me.Panel2.TabIndex = 7
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.treeRevisions)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(336, 500)
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
        Me.treeRevisions.Size = New System.Drawing.Size(330, 481)
        Me.treeRevisions.TabIndex = 0
        '
        'pnlControl
        '
        Me.pnlControl.BackColor = System.Drawing.Color.CornflowerBlue
        Me.pnlControl.Controls.Add(Me.btnAnalyze)
        Me.pnlControl.Controls.Add(Me.btnApply)
        Me.pnlControl.Controls.Add(Me.chxCommit)
        Me.pnlControl.Controls.Add(Me.ProgressBar1)
        Me.pnlControl.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlControl.Location = New System.Drawing.Point(10, 10)
        Me.pnlControl.Name = "pnlControl"
        Me.pnlControl.Size = New System.Drawing.Size(1154, 96)
        Me.pnlControl.TabIndex = 8
        '
        'btnAnalyze
        '
        Me.btnAnalyze.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnalyze.BackColor = System.Drawing.Color.Blue
        Me.btnAnalyze.ForeColor = System.Drawing.Color.White
        Me.btnAnalyze.Location = New System.Drawing.Point(539, 42)
        Me.btnAnalyze.Name = "btnAnalyze"
        Me.btnAnalyze.Size = New System.Drawing.Size(85, 48)
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
        'gbModules
        '
        Me.gbModules.BackColor = System.Drawing.Color.White
        Me.gbModules.Controls.Add(Me.dgvModules)
        Me.gbModules.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbModules.Location = New System.Drawing.Point(10, 106)
        Me.gbModules.Name = "gbModules"
        Me.gbModules.Size = New System.Drawing.Size(818, 153)
        Me.gbModules.TabIndex = 1
        Me.gbModules.TabStop = False
        Me.gbModules.Text = "Modules"
        '
        'dgvModules
        '
        Me.dgvModules.AllowUserToAddRows = False
        Me.dgvModules.AllowUserToDeleteRows = False
        Me.dgvModules.BackgroundColor = System.Drawing.Color.DarkSeaGreen
        Me.dgvModules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvModules.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cName, Me.colSchemaName, Me.cClassName, Me.cAssemblyName, Me.cCreated, Me.cDescription})
        Me.dgvModules.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvModules.GridColor = System.Drawing.Color.SeaGreen
        Me.dgvModules.Location = New System.Drawing.Point(3, 16)
        Me.dgvModules.Name = "dgvModules"
        Me.dgvModules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvModules.Size = New System.Drawing.Size(812, 134)
        Me.dgvModules.TabIndex = 0
        '
        'cName
        '
        Me.cName.HeaderText = "Module name"
        Me.cName.MinimumWidth = 50
        Me.cName.Name = "cName"
        '
        'colSchemaName
        '
        Me.colSchemaName.HeaderText = "SchemaName"
        Me.colSchemaName.MinimumWidth = 50
        Me.colSchemaName.Name = "colSchemaName"
        '
        'cClassName
        '
        Me.cClassName.HeaderText = "Class Name"
        Me.cClassName.MinimumWidth = 50
        Me.cClassName.Name = "cClassName"
        '
        'cAssemblyName
        '
        Me.cAssemblyName.HeaderText = "Assembly Name"
        Me.cAssemblyName.MinimumWidth = 150
        Me.cAssemblyName.Name = "cAssemblyName"
        Me.cAssemblyName.Width = 150
        '
        'cCreated
        '
        Me.cCreated.HeaderText = "Created"
        Me.cCreated.MinimumWidth = 50
        Me.cCreated.Name = "cCreated"
        '
        'cDescription
        '
        Me.cDescription.HeaderText = "Description"
        Me.cDescription.MinimumWidth = 200
        Me.cDescription.Name = "cDescription"
        Me.cDescription.Width = 200
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1174, 652)
        Me.Controls.Add(Me.pnlRtb)
        Me.Controls.Add(Me.gbModules)
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
        Me.pnlControl.PerformLayout()
        Me.gbModules.ResumeLayout(False)
        CType(Me.dgvModules, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents rtb1 As RichTextBox
    Friend WithEvents btnApply As Button
    Friend WithEvents backWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents zoomRtb As TrackBar
    Friend WithEvents pnlRtb As Panel
    Friend WithEvents chxCommit As CheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents pnlControl As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents treeRevisions As TreeView
    Friend WithEvents gbModules As GroupBox
    Friend WithEvents dgvModules As DataGridView
    Friend WithEvents cName As DataGridViewTextBoxColumn
    Friend WithEvents colSchemaName As DataGridViewTextBoxColumn
    Friend WithEvents cClassName As DataGridViewTextBoxColumn
    Friend WithEvents cAssemblyName As DataGridViewTextBoxColumn
    Friend WithEvents cCreated As DataGridViewTextBoxColumn
    Friend WithEvents cDescription As DataGridViewTextBoxColumn
    Friend WithEvents btnAnalyze As Button
End Class
