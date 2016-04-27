Public Class DBSqlGenerator
    Implements IDBSqlGenerator

    Public Property DBViewGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBViewGenerator
    Public Property DBFieldGenerator As IDBFieldGenerator Implements IDBSqlGenerator.DBFieldGenerator
    Public Property DBSchemaGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBSchemaGenerator
    Public Property DBTableGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBTableGenerator
    Public Property DBPrimaryKeyConstraintGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBPrimaryKeyConstraintGenerator
    Public Property DBForeignKeyConstraintGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBForeignKeyConstraintGenerator
    Public Property DBIndexGenerator As IDBIndexGenerator Implements IDBSqlGenerator.DBIndexGenerator

    Public Sub New()
        DBViewGenerator = New DBViewGenerator
        DBFieldGenerator = New DBFieldGenerator
        DBSchemaGenerator = New DBSchemaGenerator
        DBTableGenerator = New DBTableGenerator With {.Parent = Me}
        DBPrimaryKeyConstraintGenerator = New DBPrimaryKeyConstraintGenerator
        DBForeignKeyConstraintGenerator = New DBForeignKeyConstraintGenerator
        DBIndexGenerator = New DBIndexGenerator
    End Sub

    Public Overridable Function GetSqlCreate(dbObject As IDBObject) As String Implements IDBSqlGenerator.GetSqlCreate
        Dim ret As String = ""

        If TypeOf dbObject Is IDBSchema Then
            ret = DBSchemaGenerator.GetSqlCreate(dbObject)

        ElseIf TypeOf dbObject Is IDBTable Then
            ret = DBTableGenerator.GetSqlCreate(dbObject)

        ElseIf TypeOf dbObject Is IDBField Then
            ret = DBFieldGenerator.GetSqlCreate(dbObject)

        ElseIf TypeOf dbObject Is IDBPrimaryKeyConstraint Then
            ret = DBPrimaryKeyConstraintGenerator.GetSqlCreate(dbObject)

        ElseIf TypeOf dbObject Is IDBForeignKeyConstraint Then
            ret = DBForeignKeyConstraintGenerator.GetSqlCreate(dbObject)

        ElseIf TypeOf dbObject Is IDBView Then
            ret = DBViewGenerator.GetSqlCreate(dbObject)

        ElseIf TypeOf dbObject Is IDBIndex Then
            ret = DBIndexGenerator.GetSqlCreate(dbObject)

        End If

        Return ret
    End Function

    Public Overridable Function GetSqlModify(dbObject As IDBObject) As String Implements IDBSqlGenerator.GetSqlModify
        Dim ret As String = ""

        If TypeOf dbObject Is IDBSchema Then
            ret = DBSchemaGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBTable Then
            ret = DBTableGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBField Then
            ret = DBFieldGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBPrimaryKeyConstraint Then
            ret = DBPrimaryKeyConstraintGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBForeignKeyConstraint Then
            ret = DBForeignKeyConstraintGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBView Then
            ret = DBViewGenerator.GetSqlModify(CType(dbObject, IDBView))

        ElseIf TypeOf dbObject Is IDBIndex Then
            ret = DBIndexGenerator.GetSqlModify(dbObject)

        End If

        Return ret
    End Function

    Public Overridable Function GetSqlDelete(dbObject As IDBObject) As String Implements IDBSqlGenerator.GetSqlDelete
        Dim ret As String = ""

        If TypeOf dbObject Is IDBSchema Then
            ret = DBSchemaGenerator.GetSqlDelete(dbObject)

        ElseIf TypeOf dbObject Is IDBTable Then
            ret = DBTableGenerator.GetSqlDelete(dbObject)

        ElseIf TypeOf dbObject Is IDBField Then
            ret = DBFieldGenerator.GetSqlDelete(dbObject)

        ElseIf TypeOf dbObject Is IDBPrimaryKeyConstraint Then
            ret = DBPrimaryKeyConstraintGenerator.GetSqlDelete(dbObject)

        ElseIf TypeOf dbObject Is IDBView Then
            ret = DBViewGenerator.GetSqlDelete(CType(dbObject, IDBView))

        ElseIf TypeOf dbObject Is IDBForeignKeyConstraint Then
            ret = DBForeignKeyConstraintGenerator.GetSqlDelete(CType(dbObject, IDBForeignKeyConstraint))

        ElseIf TypeOf dbObject Is IDBIndex Then
            ret = DBIndexGenerator.GetSqlDelete(dbObject)

        End If

        Return ret
    End Function

End Class
