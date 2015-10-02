Imports MRFramework.MRPersisting.Core
Imports System.ComponentModel

Namespace MREventArgs

    Public Class MRRowReadingEventArgs
        Inherits CancelEventArgs
        Implements IMRRowReadingEventArgs

        Public Shadows Property Cancel As Boolean Implements IMRRowReadingEventArgs.Cancel
        Public Property RowIndex As Long Implements IMRRowReadingEventArgs.RowIndex
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(cancel As Boolean)
            MyBase.New(cancel)
        End Sub

        Public Sub New(cancel As Boolean, rowIndex As Long)
            MyClass.New()
            Me.RowIndex = rowIndex
            Me.Cancel = cancel
        End Sub
    End Class

End Namespace