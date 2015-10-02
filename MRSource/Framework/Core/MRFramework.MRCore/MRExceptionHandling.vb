Imports System.Windows.Forms

Public Class MRExceptionHandler

#Region "Global exception handlers"

    Public Shared Sub ApplicationThreadExceptionHandler(ByVal sender As Object, ByVal ex As System.Threading.ThreadExceptionEventArgs)
        MsgBox(ex.Exception.Message & vbNewLine & vbNewLine & ex.Exception.StackTrace, MsgBoxStyle.OkOnly, "Greška")
        If Debugger.IsAttached Then
            'Debugger.Break()
        End If

    End Sub
    Public Shared Sub InitializeGlobalExceptionHandling()
        AddHandler Application.ThreadException, AddressOf MRExceptionHandler.ApplicationThreadExceptionHandler
    End Sub

#End Region


End Class
