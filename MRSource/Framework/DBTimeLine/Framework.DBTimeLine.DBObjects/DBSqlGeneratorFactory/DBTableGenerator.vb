Imports Framework.DBCreator
Public Class DBTableGenerator
    Implements IDBTableGenerator

    Public Property Parent As IDBSqlGenerator Implements IDBTableGenerator.Parent

    Public Function GetSqlCreate(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlCreate
        Dim ret As String = ""

        With CType(dbObject, IDBTable)
            ret = String.Format("CREATE TABLE {0}.{1}
(
    {2} {3}
)
", .SchemaName, .Name, CType(.Descriptor, IDBTableDescriptor).CreatorFieldName, Parent.DBFieldGenerator.GetFieldTypeSql(CType(.Descriptor, IDBTableDescriptor).CreatorFieldDescriptor))
        End With

        Return ret
    End Function

    Public Function GetSqlDelete(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlDelete
        Dim ret As String = ""
        With dbObject
            ret = String.Format("DROP TABLE {0}.{1}
", .SchemaName, .Name)
        End With

        Return ret
    End Function

    Public Function GetSqlModify(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlModify
        Throw New NotImplementedException
    End Function
End Class
