Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports Framework.GUI.Controls
Imports Framework.GUI.Controls.GUIEventArgs

Public Class Form2
    Public Sub New()
        InitializeComponent()
    End Sub


    Public Sub New(title As String, worker As StepProgressBar.WorkEventHandler, args As WorkEventArgs, steps As List(Of StepInfo), stepGroups As List(Of StepGroupInfo))
        Me.New()

        With StepProgressBar1
            .Worker = worker
            .Args = args
            .Args.Parent = StepProgressBar1
            .InitializeSteps(steps, stepGroups)
        End With

        Text = title
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StepProgressBar1.StartWork()
    End Sub

    Private Sub StepProgressBar1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles StepProgressBar1.RunWorkerCompleted
        Me.DialogResult = DialogResult.OK
    End Sub
End Class