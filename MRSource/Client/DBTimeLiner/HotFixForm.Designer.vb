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
        Me.gbActions = New System.Windows.Forms.GroupBox()
        Me.btnAnalyze = New System.Windows.Forms.Button()
        Me.btnCommit = New System.Windows.Forms.Button()
        Me.gbQuery = New System.Windows.Forms.GroupBox()
        Me.MrRichTextBox1 = New Framework.GUI.Controls.MRRichTextBox(Me.components)
        Me.gbActions.SuspendLayout()
        Me.gbQuery.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbActions
        '
        Me.gbActions.BackColor = System.Drawing.Color.WhiteSmoke
        Me.gbActions.Controls.Add(Me.btnAnalyze)
        Me.gbActions.Controls.Add(Me.btnCommit)
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
        'gbQuery
        '
        Me.gbQuery.Controls.Add(Me.MrRichTextBox1)
        Me.gbQuery.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbQuery.Location = New System.Drawing.Point(0, 54)
        Me.gbQuery.Name = "gbQuery"
        Me.gbQuery.Padding = New System.Windows.Forms.Padding(10)
        Me.gbQuery.Size = New System.Drawing.Size(844, 465)
        Me.gbQuery.TabIndex = 3
        Me.gbQuery.TabStop = False
        Me.gbQuery.Text = "Query"
        '
        'MrRichTextBox1
        '
        Me.MrRichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MrRichTextBox1.Location = New System.Drawing.Point(10, 23)
        Me.MrRichTextBox1.Name = "MrRichTextBox1"
        Me.MrRichTextBox1.Size = New System.Drawing.Size(824, 432)
        Me.MrRichTextBox1.TabIndex = 0
        Me.MrRichTextBox1.Text = ""
        '
        'HotFixForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 541)
        Me.Controls.Add(Me.gbQuery)
        Me.Controls.Add(Me.gbActions)
        Me.Name = "HotFixForm"
        Me.Text = "HotFixForm"
        Me.Controls.SetChildIndex(Me.gbActions, 0)
        Me.Controls.SetChildIndex(Me.gbQuery, 0)
        Me.gbActions.ResumeLayout(False)
        Me.gbQuery.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents gbActions As GroupBox
    Friend WithEvents gbQuery As GroupBox
    Friend WithEvents btnAnalyze As Button
    Friend WithEvents btnCommit As Button
    Friend WithEvents MrRichTextBox1 As Framework.GUI.Controls.MRRichTextBox
End Class
