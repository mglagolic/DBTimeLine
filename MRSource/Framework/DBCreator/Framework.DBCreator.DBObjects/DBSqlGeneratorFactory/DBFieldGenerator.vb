Public Class DBFieldGenerator
    Implements IDBFieldGenerator

    Public Overridable Function GetFieldTypeSql(descriptor As IDBFieldDescriptor) As String Implements IDBFieldGenerator.GetFieldTypeSql
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

    Public Overridable Function GetSqlCreate(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlCreate
        Dim ret As String = ""
        With DirectCast(dbObject, IDBField)
            ret = String.Format("ALTER TABLE {0}.{1} ADD
    {2} {3}
", .SchemaName, DirectCast(.Parent, IDBObject).Name, .Name, GetFieldTypeSql(.Descriptor))
        End With


        Return ret
    End Function

    Public Overridable Function GetSqlDelete(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlDelete
        Dim ret As String = ""

        With DirectCast(dbObject, IDBField)
            ret = String.Format("ALTER TABLE {0}.{1} DROP COLUMN
    {2}
", .SchemaName, DirectCast(.Parent, IDBObject).Name, .Name)
        End With

        Return ret
    End Function

    Public Overridable Function GetSqlModify(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlModify
        Dim ret As String = ""

        With CType(dbObject, IDBField)
            ret = String.Format("ALTER TABLE {0}.{1} ALTER COLUMN
    {2} {3}
", .SchemaName, DirectCast(.Parent, IDBObject).Name, .Name, GetFieldTypeSql(.Descriptor))
        End With

        Return ret
    End Function
End Class
