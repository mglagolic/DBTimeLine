Imports MRFramework.MRPersisting.Core
Namespace MREventArgs

    Friend Class MRRowReadEventArgs
        Inherits EventArgs
        Implements IMRRowReadEventArgs


        Public Property RowIndex As Long Implements IMRRowReadEventArgs.RowIndex
        Public Property Dlo As IMRDLO = Nothing Implements IMRRowReadEventArgs.Dlo

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(rowIndex As Long)
            MyClass.New(rowIndex, Nothing)
        End Sub
        'Public Sub New(rowIndex As Long, columnValues As MRRowColumnValues)
        Public Sub New(rowIndex As Long, dlo As IMRDLO)
            MyClass.New()
            Me.RowIndex = rowIndex
            Me.Dlo = dlo
        End Sub
    End Class

End Namespace