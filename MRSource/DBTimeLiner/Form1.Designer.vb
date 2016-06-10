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
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Zona")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Nadzor")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("ND (Nadzor)", New System.Windows.Forms.TreeNode() {TreeNode6, TreeNode7})
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("ND", New System.Windows.Forms.TreeNode() {TreeNode8})
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DBTimeLine", New System.Windows.Forms.TreeNode() {TreeNode9})
        Me.rtb1 = New System.Windows.Forms.RichTextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.backWorker = New System.ComponentModel.BackgroundWorker()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.zoomRtb = New System.Windows.Forms.TrackBar()
        Me.pnlRtb = New System.Windows.Forms.Panel()
        Me.chxCommit = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.treeRevisions = New System.Windows.Forms.TreeView()
        Me.gbModules = New System.Windows.Forms.GroupBox()
        Me.CheckedListBox1 = New System.Windows.Forms.CheckedListBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        CType(Me.zoomRtb, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRtb.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.gbModules.SuspendLayout()
        Me.Panel3.SuspendLayout()
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
        Me.rtb1.Size = New System.Drawing.Size(918, 284)
        Me.rtb1.TabIndex = 0
        Me.rtb1.Text = "bok" & Global.Microsoft.VisualBasic.ChrW(10) & "kaj" & Global.Microsoft.VisualBasic.ChrW(10) & "ima"
        Me.rtb1.WordWrap = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(1069, 77)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
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
        Me.ProgressBar1.Location = New System.Drawing.Point(10, 10)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1134, 36)
        Me.ProgressBar1.TabIndex = 2
        '
        'zoomRtb
        '
        Me.zoomRtb.AutoSize = False
        Me.zoomRtb.Dock = System.Windows.Forms.DockStyle.Right
        Me.zoomRtb.LargeChange = 10
        Me.zoomRtb.Location = New System.Drawing.Point(898, 0)
        Me.zoomRtb.Maximum = 50
        Me.zoomRtb.Minimum = 10
        Me.zoomRtb.Name = "zoomRtb"
        Me.zoomRtb.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.zoomRtb.Size = New System.Drawing.Size(20, 284)
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
        Me.pnlRtb.Location = New System.Drawing.Point(10, 123)
        Me.pnlRtb.Name = "pnlRtb"
        Me.pnlRtb.Size = New System.Drawing.Size(918, 284)
        Me.pnlRtb.TabIndex = 5
        '
        'chxCommit
        '
        Me.chxCommit.AutoSize = True
        Me.chxCommit.Location = New System.Drawing.Point(10, 55)
        Me.chxCommit.Name = "chxCommit"
        Me.chxCommit.Size = New System.Drawing.Size(60, 17)
        Me.chxCommit.TabIndex = 6
        Me.chxCommit.Text = "Commit"
        Me.chxCommit.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.gbModules)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(928, 123)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(236, 284)
        Me.Panel2.TabIndex = 7
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.treeRevisions)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 159)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(236, 159)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Revisions"
        '
        'treeRevisions
        '
        Me.treeRevisions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeRevisions.Location = New System.Drawing.Point(3, 16)
        Me.treeRevisions.Name = "treeRevisions"
        TreeNode6.Name = "nodZona"
        TreeNode6.Text = "Zona"
        TreeNode7.Name = "Node4"
        TreeNode7.Text = "Nadzor"
        TreeNode8.Name = "ndNadzor"
        TreeNode8.Text = "ND (Nadzor)"
        TreeNode9.Name = "ndND"
        TreeNode9.Text = "ND"
        TreeNode10.Name = "Node0"
        TreeNode10.Text = "DBTimeLine"
        Me.treeRevisions.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode10})
        Me.treeRevisions.Size = New System.Drawing.Size(230, 140)
        Me.treeRevisions.TabIndex = 0
        '
        'gbModules
        '
        Me.gbModules.Controls.Add(Me.CheckedListBox1)
        Me.gbModules.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbModules.Location = New System.Drawing.Point(0, 0)
        Me.gbModules.Name = "gbModules"
        Me.gbModules.Size = New System.Drawing.Size(236, 159)
        Me.gbModules.TabIndex = 0
        Me.gbModules.TabStop = False
        Me.gbModules.Text = "Modules"
        '
        'CheckedListBox1
        '
        Me.CheckedListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CheckedListBox1.FormattingEnabled = True
        Me.CheckedListBox1.Items.AddRange(New Object() {"Nadzor", "Decision support"})
        Me.CheckedListBox1.Location = New System.Drawing.Point(3, 16)
        Me.CheckedListBox1.Name = "CheckedListBox1"
        Me.CheckedListBox1.Size = New System.Drawing.Size(230, 140)
        Me.CheckedListBox1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Controls.Add(Me.chxCommit)
        Me.Panel3.Controls.Add(Me.ProgressBar1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(10, 10)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(10)
        Me.Panel3.Size = New System.Drawing.Size(1154, 113)
        Me.Panel3.TabIndex = 8
        '
        'Panel5
        '
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(10, 407)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1154, 152)
        Me.Panel5.TabIndex = 10
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(1174, 569)
        Me.Controls.Add(Me.pnlRtb)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel5)
        Me.Name = "Form1"
        Me.Padding = New System.Windows.Forms.Padding(10)
        Me.Text = "Form1"
        CType(Me.zoomRtb, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRtb.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.gbModules.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents rtb1 As RichTextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents backWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents zoomRtb As TrackBar
    Friend WithEvents pnlRtb As Panel
    Friend WithEvents chxCommit As CheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents gbModules As GroupBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents CheckedListBox1 As CheckedListBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents treeRevisions As TreeView
End Class
