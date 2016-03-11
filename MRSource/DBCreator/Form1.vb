Imports MRFramework.MRPersisting.Factory
Imports MRFramework.MRDBCreator

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MRC.GetInstance().ConnectionString = Properties.Settings.Default[Properties.Settings.Default.DefaultConnectionString].ToString()
        'MRC.GetInstance().ProviderName = Properties.Settings.Default[Properties.Settings.Default.DefaultProvider].ToString()

        MRC.GetInstance().ConnectionString = My.Settings.Item(My.Settings.DefaultConnectionString)
        MRC.GetInstance().ProviderName = My.Settings.Item(My.Settings.DefaultProvider)

        Dim modules As New List(Of IDBModule)

        modules.Add(DBCreators.Common.Common.GetInstance())


        ' otvoriti konekciju i transakciju

        'For j As Integer = 0 To 100000
        For i As Integer = 0 To modules.Count - 1
                modules(i).DBCreate()
            Next
        'Next
    End Sub
End Class
