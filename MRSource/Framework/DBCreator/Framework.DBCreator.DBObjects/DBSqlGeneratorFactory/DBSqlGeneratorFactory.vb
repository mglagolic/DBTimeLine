Imports Framework.DBCreator

Public Class DBSqlGeneratorFactory
    Implements IDBSqlGeneratorFactory

    'TODO - implementirati eDBType.MySql
    Public Function GetDBSqlGenerator(dbType As eDBType) As IDBSqlGenerator Implements IDBSqlGeneratorFactory.GetDBSqlGenerator
        Dim ret As IDBSqlGenerator
        Select Case dbType
            Case eDBType.TransactSQL
                ret = New DBSqlGenerator
                ret.DBViewGenerator = New DBViewGenerator
                ret.DBFieldGenerator = New DBFieldGenerator
                ret.DBSchemaGenerator = New DBSchemaGenerator
                ret.DBTableGenerator = New DBTableGenerator With {.Parent = ret}
                ret.DBPrimaryKeyConstraintGenerator = New DBPrimaryKeyConstraintGenerator
                ret.DBForeignKeyConstraintGenerator = New DBForeignKeyConstraintGenerator
            Case eDBType.SqlServer
                ret = New DBSqlGeneratorSqlServer
                ret.DBViewGenerator = New DBViewGenerator
                ret.DBFieldGenerator = New DBFieldGenerator
                ret.DBSchemaGenerator = New DBSchemaGenerator
                ret.DBTableGenerator = New DBTableGenerator
                ret.DBTableGenerator = New DBTableGenerator With {.Parent = ret}
                ret.DBPrimaryKeyConstraintGenerator = New DBPrimaryKeyConstraintGenerator
                ret.DBForeignKeyConstraintGenerator = New DBForeignKeyConstraintGenerator
            Case eDBType.MySql
                Throw New NotSupportedException()
            Case Else
                Throw New NotSupportedException()
        End Select

        Return ret
    End Function

End Class
