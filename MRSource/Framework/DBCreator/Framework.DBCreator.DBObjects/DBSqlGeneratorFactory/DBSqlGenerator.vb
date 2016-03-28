Imports Framework.DBCreator

Public Class DBSqlGenerator
    Implements IDBSqlGenerator

    Public Property DBViewGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBViewGenerator
    Public Property DBFieldGenerator As IDBFieldGenerator Implements IDBSqlGenerator.DBFieldGenerator
    Public Property DBSchemaGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBSchemaGenerator
    Public Property DBTableGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBTableGenerator
    Public Property DBPrimaryKeyConstraintGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBPrimaryKeyConstraintGenerator

    'TODO - ovisno o ovdje izvrsavati batcheve, GO split mozda svuda i bok ...

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

        ElseIf TypeOf dbObject Is IDBView Then
            ret = DBViewGenerator.GetSqlCreate(CType(dbObject, IDBView))
        End If

        Return ret
    End Function

    Public Overridable Function GetSqlModify(dbObject As IDBObject) As String Implements IDBSqlGenerator.GetSqlModify
        Dim ret As String = ""
        If TypeOf dbObject Is IDBField Then
            ret = DBFieldGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBView Then
            ret = DBViewGenerator.GetSqlModify(CType(dbObject, IDBView))

        ElseIf TypeOf dbObject Is IDBPrimaryKeyConstraint Then
            ret = DBPrimaryKeyConstraintGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBSchema Then
            ret = DBSchemaGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBTable Then
            ret = DBTableGenerator.GetSqlModify(dbObject)
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
        End If

        Return ret
    End Function

End Class
