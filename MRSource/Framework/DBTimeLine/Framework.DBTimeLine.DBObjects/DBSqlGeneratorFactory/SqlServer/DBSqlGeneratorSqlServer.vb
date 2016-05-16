Public Class DBSqlGeneratorSqlServer
    Inherits DBSqlGenerator

    Public Sub New()
        MyBase.New

        ' Ovdje dodati specificne implementacije generatora
        DBViewGenerator = New DBViewGenerator
    End Sub

End Class
