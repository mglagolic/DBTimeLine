Imports Framework.DBTimeLine

Public Class frmSqlRevInfo
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(sqlRev As DBSqlRevision)
        MyClass.New()

        SqlRevision = sqlRev
    End Sub

    Public Property SqlRevision As DBSqlRevision

End Class