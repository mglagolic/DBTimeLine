﻿Imports Framework.DBCreator

Public Class DBSqlGenerator
    Implements IDBSqlGenerator

    'TODO - ovisno o ovdje izvrsavati batcheve, GO split mozda svuda i bok ...
    'TODO - sve sql get funkcije promijeniti tako da im argument bude samo idbobject
    Public Overridable Function GetSqlCreateSchema(schema As IDBSchema) As String Implements IDBSqlGenerator.GetSqlCreateSchema
        Dim ret As String = ""

        ret = String.Format("GO
CREATE SCHEMA {0}
GO
", schema.Name)
        Return ret
    End Function

    Public Overridable Function GetSqlDeleteSchema(schema As IDBSchema) As String Implements IDBSqlGenerator.GetSqlDeleteSchema
        Dim ret As String = ""

        ret = String.Format("GO
DROP SCHEMA {0}
GO
", schema.Name)
        Return ret
    End Function

    Public Overridable Function GetSqlCreateField(field As IDBField) As String Implements IDBSqlGenerator.GetSqlCreateField
        With field
            Dim ret As String = String.Format("ALTER TABLE {0}.{1} ADD
{2} {3}
", .SchemaName, DirectCast(.Parent, IDBObject).Name, .Name, GetFieldTypeSql(.Descriptor))

            Return ret
        End With
    End Function

    Public Overridable Function GetSqlModifyField(field As IDBField) As String Implements IDBSqlGenerator.GetSqlModifyField
        With field
            Dim ret As String = String.Format("ALTER TABLE {0}.{1} ALTER COLUMN
{2} {3}
", .SchemaName, DirectCast(.Parent, IDBObject).Name, .Name, GetFieldTypeSql(.Descriptor))

            Return ret
        End With
    End Function

    Public Overridable Function GetSqlDeleteField(field As IDBField) As String Implements IDBSqlGenerator.GetSqlDeleteField
        With field
            Dim ret As String = String.Format("ALTER TABLE {0}.{1} DROP COLUMN
{2}
", .SchemaName, DirectCast(.Parent, IDBObject).Name, .Name)

            Return ret
        End With
    End Function

    Public Overridable Function GetSqlCreateTable(table As IDBTable) As String Implements IDBSqlGenerator.GetSqlCreateTable
        Dim ret As String = ""
        With table
            ret = String.Format("CREATE TABLE {0}.{1}
(
    {2} {3}
)
", .SchemaName, .Name, CType(.Descriptor, IDBTableDescriptor).CreatorFieldName, GetFieldTypeSql(CType(.Descriptor, IDBTableDescriptor).CreatorFieldDescriptor))
        End With

        Return ret
    End Function

    Public Overridable Function GetSqlDeleteTable(table As IDBTable) As String Implements IDBSqlGenerator.GetSqlDeleteTable
        Dim ret As String = ""

        ret = String.Format("DROP TABLE {0}.{1}
", table.SchemaName, table.Name)

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

    Public Function GetSqlCreateConstraint(constraint As IDBConstraint) As String Implements IDBSqlGenerator.GetSqlCreateConstraint
        Dim ret As String = ""
        ' TODO - implementirati PK i FK constraints za sada. Dodati constraint type enum
        With CType(constraint.Descriptor, IDBConstraintDescriptor)
            Select Case .ConstraintType
                Case eDBConstraintType.PrimaryKey
                    Throw New NotImplementedException
                Case eDBConstraintType.ForeignKey
                    Throw New NotImplementedException
            End Select
        End With

        'ALTER TABLE Place.Table1 ADD CONSTRAINT
        '	PK_Table1 PRIMARY KEY CLUSTERED 
        '	(
        '	ID
        '	)

        Return ret
    End Function
End Class
