<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HotFixForm
    Inherits Framework.GUI.Forms.MRStatusForm

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HotFixForm))
        Me.gbActions = New System.Windows.Forms.GroupBox()
        Me.btnAnalyze = New System.Windows.Forms.Button()
        Me.btnCommit = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.gbQuery = New System.Windows.Forms.GroupBox()
        Me.MrRichTextBox1 = New Framework.GUI.Controls.MRRichTextBox(Me.components)
        Me.gbDetails = New System.Windows.Forms.GroupBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbActions.SuspendLayout()
        Me.gbQuery.SuspendLayout()
        Me.gbDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbActions
        '
        Me.gbActions.BackColor = System.Drawing.Color.WhiteSmoke
        Me.gbActions.Controls.Add(Me.btnAnalyze)
        Me.gbActions.Controls.Add(Me.btnCommit)
        Me.gbActions.Controls.Add(Me.Label6)
        Me.gbActions.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbActions.ForeColor = System.Drawing.Color.Black
        Me.gbActions.Location = New System.Drawing.Point(0, 0)
        Me.gbActions.Name = "gbActions"
        Me.gbActions.Size = New System.Drawing.Size(844, 54)
        Me.gbActions.TabIndex = 1
        Me.gbActions.TabStop = False
        Me.gbActions.Text = "Actions"
        '
        'btnAnalyze
        '
        Me.btnAnalyze.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAnalyze.BackColor = System.Drawing.Color.CornflowerBlue
        Me.btnAnalyze.ForeColor = System.Drawing.Color.White
        Me.btnAnalyze.Location = New System.Drawing.Point(216, 6)
        Me.btnAnalyze.Name = "btnAnalyze"
        Me.btnAnalyze.Size = New System.Drawing.Size(131, 45)
        Me.btnAnalyze.TabIndex = 9
        Me.btnAnalyze.Text = "Analyze"
        Me.btnAnalyze.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAnalyze.UseVisualStyleBackColor = False
        '
        'btnCommit
        '
        Me.btnCommit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCommit.BackColor = System.Drawing.Color.Maroon
        Me.btnCommit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnCommit.ForeColor = System.Drawing.Color.White
        Me.btnCommit.Location = New System.Drawing.Point(487, 6)
        Me.btnCommit.Name = "btnCommit"
        Me.btnCommit.Size = New System.Drawing.Size(131, 45)
        Me.btnCommit.TabIndex = 8
        Me.btnCommit.TabStop = False
        Me.btnCommit.Text = "Commit"
        Me.btnCommit.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label6.Location = New System.Drawing.Point(707, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(127, 17)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "timestamp"
        '
        'gbQuery
        '
        Me.gbQuery.Controls.Add(Me.MrRichTextBox1)
        Me.gbQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbQuery.Location = New System.Drawing.Point(0, 132)
        Me.gbQuery.Name = "gbQuery"
        Me.gbQuery.Padding = New System.Windows.Forms.Padding(10)
        Me.gbQuery.Size = New System.Drawing.Size(844, 387)
        Me.gbQuery.TabIndex = 3
        Me.gbQuery.TabStop = False
        Me.gbQuery.Text = "Query"
        '
        'MrRichTextBox1
        '
        Me.MrRichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MrRichTextBox1.Location = New System.Drawing.Point(10, 23)
        Me.MrRichTextBox1.Name = "MrRichTextBox1"
        Me.MrRichTextBox1.Size = New System.Drawing.Size(824, 354)
        Me.MrRichTextBox1.TabIndex = 0
        Me.MrRichTextBox1.Text = resources.GetString("MrRichTextBox1.Text")
        '
        'gbDetails
        '
        Me.gbDetails.Controls.Add(Me.TextBox5)
        Me.gbDetails.Controls.Add(Me.Label7)
        Me.gbDetails.Controls.Add(Me.TextBox4)
        Me.gbDetails.Controls.Add(Me.TextBox3)
        Me.gbDetails.Controls.Add(Me.TextBox2)
        Me.gbDetails.Controls.Add(Me.TextBox1)
        Me.gbDetails.Controls.Add(Me.Label4)
        Me.gbDetails.Controls.Add(Me.Label3)
        Me.gbDetails.Controls.Add(Me.Label2)
        Me.gbDetails.Controls.Add(Me.Label1)
        Me.gbDetails.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbDetails.Location = New System.Drawing.Point(0, 54)
        Me.gbDetails.Name = "gbDetails"
        Me.gbDetails.Size = New System.Drawing.Size(844, 78)
        Me.gbDetails.TabIndex = 4
        Me.gbDetails.TabStop = False
        Me.gbDetails.Text = "Details"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(342, 47)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(100, 20)
        Me.TextBox5.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(254, 47)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 20)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Database name"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(527, 16)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(100, 20)
        Me.TextBox4.TabIndex = 8
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(342, 16)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(100, 20)
        Me.TextBox3.TabIndex = 7
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(121, 44)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 6
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(121, 16)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(466, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 20)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Username"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(247, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "SQL server name"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(35, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Server name"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(35, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Customer name"
        '
        'HotFixForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 541)
        Me.Controls.Add(Me.gbQuery)
        Me.Controls.Add(Me.gbDetails)
        Me.Controls.Add(Me.gbActions)
        Me.Name = "HotFixForm"
        Me.Text = "HotFixForm"
        Me.Controls.SetChildIndex(Me.gbActions, 0)
        Me.Controls.SetChildIndex(Me.gbDetails, 0)
        Me.Controls.SetChildIndex(Me.gbQuery, 0)
        Me.gbActions.ResumeLayout(False)
        Me.gbQuery.ResumeLayout(False)
        Me.gbDetails.ResumeLayout(False)
        Me.gbDetails.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gbActions As GroupBox
    Friend WithEvents gbQuery As GroupBox
    Friend WithEvents btnAnalyze As Button
    Friend WithEvents btnCommit As Button
    Friend WithEvents MrRichTextBox1 As Framework.GUI.Controls.MRRichTextBox
    Friend WithEvents gbDetails As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Label7 As Label
End Class
