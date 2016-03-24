Imports Framework.DBCreator

Public Class DBSqlGenerator
    Implements IDBSqlGenerator

    'TODO - ovisno o ovdje izvrsavati batcheve, GO split mozda svuda i bok ...

    '    Public Function GetSqlCreateConstraint(constraint As IDBConstraint) As String Implements IDBSqlGenerator.GetSqlCreateConstraint
    '        Dim ret As String = ""
    '        ' TODO - implementirati PK i FK constraints za sada. Dodati constraint type enum
    '        With CType(constraint.Descriptor, IDBConstraintDescriptor)
    '            Select Case .ConstraintType
    '                Case eDBConstraintType.PrimaryKey
    '                    Throw New NotImplementedException
    '                Case eDBConstraintType.ForeignKey
    '                    Throw New NotImplementedException
    '            End Select
    '        End With

    '        'ALTER TABLE Place.Table1 ADD CONSTRAINT
    '        '	PK_Table1 PRIMARY KEY CLUSTERED 
    '        '	(
    '        '	ID
    '        '	)

    '        Return ret
    '    End Function

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
        End If

        Return ret
    End Function

End Class
