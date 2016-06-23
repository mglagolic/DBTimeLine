Public Class DBField
    Inherits DBObject
    Implements IDBField

    Public Sub New()

    End Sub

    Public Overrides ReadOnly Property ObjectTypeOrdinal As Integer
        Get
            Return 20
        End Get
    End Property

    Public Overrides ReadOnly Property ObjectTypeName As String
        Get
            Return "Field"
        End Get
    End Property

    Public Function GetFieldTypeSql(dBType As eDBType) As String Implements IDBField.GetFieldTypeSql
        Dim ret As String = ""

        With CType(Descriptor, IDBFieldDescriptor)
            ret = .FieldType.GetFieldTypeSql(Descriptor, dBType)

            'Select Case .FieldType

            '    Case eDBFieldType.Decimal
            '        ret = "DECIMAL(" & .Size.ToString & ", " & .Precision.ToString & ")"

            '    Case Else
            '        Throw New NotSupportedException("Unsupported eDBFieldType.")
            'End Select

            If .Nullable Then
                ret &= " NULL"
            Else
                ret &= " NOT NULL"
            End If

            If Not String.IsNullOrEmpty(.DefaultValue) Then
                ret &= String.Format(" DEFAULT {0}", .DefaultValue)
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
