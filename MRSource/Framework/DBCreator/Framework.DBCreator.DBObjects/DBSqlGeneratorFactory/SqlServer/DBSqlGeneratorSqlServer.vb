Public Class DBSqlGeneratorSqlServer
    Inherits DBSqlGenerator

    Public Sub New()
        DBViewGenerator = New DBViewGenerator
        DBFieldGenerator = New DBFieldGenerator
        DBSchemaGenerator = New DBSchemaGenerator
        DBTableGenerator = New DBTableGenerator
        DBTableGenerator = New DBTableGenerator With {.Parent = Me}
        DBPrimaryKeyConstraintGenerator = New DBPrimaryKeyConstraintGenerator
        DBForeignKeyConstraintGenerator = New DBForeignKeyConstraintGenerator
    End Sub

End Class
