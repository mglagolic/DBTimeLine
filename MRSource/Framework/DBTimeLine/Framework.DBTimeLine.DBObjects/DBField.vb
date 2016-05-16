Public Class DBField
    Inherits DBObject
    Implements IDBField

    Public Sub New()

    End Sub

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.Field
        End Get
    End Property

    Public Function GetFieldTypeSql(dBType As eDBType) As String Implements IDBField.GetFieldTypeSql
        Dim ret As String = ""

        With CType(Descriptor, IDBFieldDescriptor)
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

    Public Overrides Function GetSqlCreate(dBType As eDBType) As String
        Dim ret As String = ""

        ret = String.Format("ALTER TABLE {0}.{1} ADD
    {2} {3}
", SchemaName, DirectCast(Parent, IDBObject).Name, Name, GetFieldTypeSql(dBType))

        Return ret
    End Function

    Public Overrides Function GetSqlModify(dBType As eDBType) As String
        Dim ret As String = ""

        ret = String.Format("ALTER TABLE {0}.{1} ALTER COLUMN
    {2} {3}
", SchemaName, DirectCast(Parent, IDBObject).Name, Name, GetFieldTypeSql(dBType))

        Return ret
    End Function

    Public Overrides Function GetSqlDelete(dBType As eDBType) As String
        Dim ret As String = ""

        ret = String.Format("ALTER TABLE {0}.{1} DROP COLUMN
    {2}
", SchemaName, DirectCast(Parent, IDBObject).Name, Name)

        Return ret
    End Function

End Class
