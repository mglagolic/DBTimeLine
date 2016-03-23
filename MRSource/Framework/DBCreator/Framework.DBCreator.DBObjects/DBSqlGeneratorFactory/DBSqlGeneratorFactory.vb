Imports Framework.DBCreator

Public Class DBSqlGeneratorFactory
    Implements IDBSqlGeneratorFactory

    'TODO - implementirati eDBType.MySql, Tiho radi na tome
    Public Function GetDBSqlGenerator(dbType As eDBType) As IDBSqlGenerator Implements IDBSqlGeneratorFactory.GetDBSqlGenerator
        Dim ret As IDBSqlGenerator
        Select Case dbType
            Case eDBType.TransactSQL
                ret = New DBSqlGenerator
            Case eDBType.SqlServer
                ret = New DBSqlGeneratorSqlServer
            Case eDBType.MySql
                Throw New NotSupportedException()
            Case Else
                Throw New NotSupportedException()
        End Select

        Return ret
    End Function

End Class
