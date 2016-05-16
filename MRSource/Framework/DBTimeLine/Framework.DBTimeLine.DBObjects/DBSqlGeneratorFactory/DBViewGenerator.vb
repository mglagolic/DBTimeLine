Imports Framework.DBCreator
Public Class DBViewGenerator
    Implements IDBObjectGenerator

    Public Function GetSqlCreate(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlCreate
        Dim ret As String = ""
        Dim bindingClause As String = ""

        With CType(dbObject.Descriptor, IDBViewDescriptor)
            If .WithSchemaBinding Then
                bindingClause = "WITH SCHEMABINDING"
            End If
            ret = String.Format(
"GO
CREATE VIEW {0}.{1}
{2}
AS
{3}
GO
", dbObject.SchemaName, dbObject.Name, bindingClause, .Body)
        End With

        Return ret
    End Function

    Public Function GetSqlDelete(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlDelete
        Dim ret As String = ""
        ret = String.Format(
"
GO
DROP VIEW {0}.{1}
GO
", dbObject.SchemaName, dbObject.SchemaObjectName)
        Return ret
    End Function

    Public Function GetSqlModify(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlModify
        Dim ret As String = ""
        Dim bindingClause As String = ""

        With CType(dbObject.Descriptor, IDBViewDescriptor)
            If .WithSchemaBinding Then
                bindingClause = "WITH SCHEMABINDING"
            End If
            ret = String.Format(
"GO
ALTER VIEW {0}.{1}
{2}
AS
{3}
GO
", dbObject.SchemaName, dbObject.SchemaObjectName, bindingClause, .Body)
        End With

        Return ret
    End Function
End Class
