Public Class DBSqlGenerator
    Implements IDBSqlGenerator

    Public Property DBViewGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBViewGenerator

    'TODO - ovisno o ovdje izvrsavati batcheve, GO split mozda svuda i bok ...

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

    Public Function GetSqlCreate(dbObject As IDBObject) As String Implements IDBSqlGenerator.GetSqlCreate
        Dim ret As String = ""

        If TypeOf dbObject Is IDBSchema Then
            ret = String.Format("GO
CREATE SCHEMA {0}
GO
", dbObject.Name)
        ElseIf TypeOf dbObject Is IDBTable Then
            With CType(dbObject, IDBTable)
                ret = String.Format("CREATE TABLE {0}.{1}
(
    {2} {3}
)
", .SchemaName, .Name, CType(.Descriptor, IDBTableDescriptor).CreatorFieldName, GetFieldTypeSql(CType(.Descriptor, IDBTableDescriptor).CreatorFieldDescriptor))
            End With

        ElseIf TypeOf dbObject Is IDBField Then
            With DirectCast(dbObject, IDBField)
                ret = String.Format("ALTER TABLE {0}.{1} ADD
    {2} {3}
", .SchemaName, DirectCast(.Parent, IDBObject).Name, .Name, GetFieldTypeSql(.Descriptor))
            End With

        ElseIf TypeOf dbObject Is IDBPrimaryKeyConstraint Then
            With DirectCast(dbObject, IDBPrimaryKeyConstraint)
                Dim descriptor As IDBPrimaryKeyConstraintDescriptor = DirectCast(.Descriptor, IDBPrimaryKeyConstraintDescriptor)

                Dim constraintName As String = descriptor.ConstraintName
                Dim columns As String = ""
                For Each col As String In descriptor.Columns
                    columns &= col & ","
                Next
                columns = columns.TrimEnd(","c)

                If String.IsNullOrWhiteSpace(constraintName) Then
                    constraintName = "PK_" & .SchemaName & "_" & DirectCast(.Parent, IDBObject).Name & "_" & columns.Replace(","c, "_")
                End If

                ret = String.Format("ALTER TABLE {0}.{1}
ADD CONSTRAINT {2} PRIMARY KEY ({3})
", .SchemaName, DirectCast(.Parent, IDBObject).Name, constraintName, columns)

            End With
        ElseIf TypeOf dbObject Is IDBView Then
            ret = DBViewGenerator.GetSqlCreate(CType(dbObject, IDBView))
        End If

        Return ret
    End Function

    Public Function GetSqlModify(dbObject As IDBObject) As String Implements IDBSqlGenerator.GetSqlModify
        Dim ret As String = ""
        If TypeOf dbObject Is IDBField Then
            With CType(dbObject, IDBField)
                ret = String.Format("ALTER TABLE {0}.{1} ALTER COLUMN
    {2} {3}
", .SchemaName, DirectCast(.Parent, IDBObject).Name, .Name, GetFieldTypeSql(.Descriptor))
            End With

        ElseIf TypeOf dbObject Is IDBView Then
            ret = DBViewGenerator.GetSqlModify(CType(dbObject, IDBView))
        End If


        Return ret
    End Function

    Public Function GetSqlDelete(dbObject As IDBObject) As String Implements IDBSqlGenerator.GetSqlDelete
        Dim ret As String = ""

        If TypeOf dbObject Is IDBSchema Then
            ret = String.Format("GO
DROP SCHEMA {0}
GO
", dbObject.Name)

        ElseIf TypeOf dbObject Is IDBTable Then
            With dbObject
                ret = String.Format("DROP TABLE {0}.{1}
", .SchemaName, .Name)
            End With

        ElseIf TypeOf dbObject Is IDBField Then
            With DirectCast(dbObject, IDBField)
                ret = String.Format("ALTER TABLE {0}.{1} DROP COLUMN
    {2}
", .SchemaName, DirectCast(.Parent, IDBObject).Name, .Name)
            End With

        ElseIf TypeOf dbObject Is IDBPrimaryKeyConstraint Then
            With DirectCast(dbObject, IDBPrimaryKeyConstraint)
                Dim descriptor As IDBPrimaryKeyConstraintDescriptor = DirectCast(.Descriptor, IDBPrimaryKeyConstraintDescriptor)
                Dim columns As String = ""
                For Each col As String In descriptor.Columns
                    columns &= col & ","
                Next
                columns = columns.TrimEnd(","c)

                Dim constraintName As String = descriptor.ConstraintName
                If String.IsNullOrWhiteSpace(constraintName) Then
                    constraintName = "PK_" & .SchemaName & "_" & DirectCast(.Parent, IDBObject).Name & "_" & columns.Replace(","c, "_")
                End If

                ret = String.Format("ALTER TABLE {0}.{1}
DROP Constraint {2}
", .SchemaName, DirectCast(.Parent, IDBObject).Name, constraintName)

            End With

        ElseIf TypeOf dbObject Is IDBView Then
            ret = DBViewGenerator.GetSqlDelete(CType(dbObject, IDBView))
        End If

        Return ret
    End Function

End Class
