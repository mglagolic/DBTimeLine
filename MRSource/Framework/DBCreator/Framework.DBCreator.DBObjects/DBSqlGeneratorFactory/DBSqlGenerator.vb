Imports Framework.DBCreator

Public Class DBSqlGenerator
    Implements IDBSqlGenerator

    'TODO - ovisno o ovdje izvrsavati batcheve, GO split mozda svuda i bok ...

    Public Overridable Function GetSqlCreateSchema(name As String) As String Implements IDBSqlGenerator.GetSqlCreateSchema
        Dim ret As String = ""

        ret = String.Format("GO
CREATE SCHEMA {0}
GO
", name)
        Return ret
    End Function

    Public Overridable Function GetSqlDeleteSchema(name As String) As String Implements IDBSqlGenerator.GetSqlDeleteSchema
        Dim ret As String = ""

        ret = String.Format("GO
DROP SCHEMA {0}
GO
", name)
        Return ret
    End Function

    Public Overridable Function GetSqlCreateField(schemaName As String, tableName As String, name As String, descriptor As IDBFieldDescriptor) As String Implements IDBSqlGenerator.GetSqlCreateField
        Dim ret As String = String.Format("ALTER TABLE {0}.{1} ADD
{2} {3}
", schemaName, tableName, name, GetFieldTypeSql(descriptor))

        Return ret
    End Function

    Public Overridable Function GetSqlModifyField(schemaName As String, tableName As String, name As String, descriptor As IDBFieldDescriptor) As String Implements IDBSqlGenerator.GetSqlModifyField
        Dim ret As String = String.Format("ALTER TABLE {0}.{1} ALTER COLUMN
{2} {3}
", schemaName, tableName, name, GetFieldTypeSql(descriptor))

        Return ret
    End Function

    Public Overridable Function GetSqlDeleteField(schemaName As String, tableName As String, name As String) As String Implements IDBSqlGenerator.GetSqlDeleteField
        Dim ret As String = String.Format("ALTER TABLE {0}.{1} DROP COLUMN
{2}
", schemaName, tableName, name)

        Return ret
    End Function

    Public Overridable Function GetSqlCreateTable(schemaName As String, name As String, descriptor As IDBTableDescriptor) As String Implements IDBSqlGenerator.GetSqlCreateTable
        Dim ret As String = ""
        With descriptor
            ret = String.Format("CREATE TABLE {0}.{1}
(
    {2} {3}
)
", schemaName, name, .CreatorFieldName, GetFieldTypeSql(.CreatorFieldDescriptor))
        End With

        Return ret
    End Function

    Public Overridable Function GetSqlDeleteTable(schemaName As String, name As String) As String Implements IDBSqlGenerator.GetSqlDeleteTable
        Dim ret As String = ""

        ret = String.Format("DROP TABLE {0}.{1}
", schemaName, name)

        Return ret
    End Function

    Public Overridable Function GetFieldTypeSql(descriptor As IDBFieldDescriptor) As String Implements IDBSqlGenerator.GetFieldTypeSql
        Dim ret As String = ""

        With descriptor

            Select Case .FieldType
                Case eDBFieldType.Guid
                    ret = "UNIQUEIDENTIFIER"
                Case eDBFieldType.Integer
                    ret = "INTEGER"
                Case eDBFieldType.Nvarchar
                    ret = "NVARCHAR(" & CStr(IIf(.Size = -1, "MAX", .Size.ToString)) & ")"
                Case eDBFieldType.Datetime
                    ret = "DATETIME"
                Case eDBFieldType.Decimal
                    ret = "DECIMAL(" & .Size.ToString & ", " & .Precision.ToString & ")"
                Case Else
                    Throw New NotSupportedException("Unsupported eDBFieldType.")
            End Select

            If .Nullable Then
                ret &= " NULL"
            Else
                ret &= " NOT NULL"
            End If
        End With
        Return ret
    End Function
End Class
