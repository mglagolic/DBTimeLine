Public Class DBForeignKeyConstraint
    Inherits DBObject
    Implements IDBForeignKeyConstraint

    Public Sub New()

    End Sub

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.Constraint
        End Get
    End Property

    Public Overrides Function GetSqlCreate(dBType As eDBType) As String
        Dim ret As String = ""

        With Me
            Dim descriptor As IDBForeignKeyConstraintDescriptor = DirectCast(.Descriptor, IDBForeignKeyConstraintDescriptor)

            Dim constraintName As String = descriptor.ConstraintName

            Dim FKColumns As String = ""
            For Each col As String In descriptor.FKTableColumns
                FKColumns &= col & ","
            Next
            FKColumns = FKColumns.TrimEnd(","c)

            Dim PKColumns As String = ""
            For Each col As String In descriptor.PKTableColumns
                PKColumns &= col & ","
            Next
            PKColumns = PKColumns.TrimEnd(","c)

            If String.IsNullOrWhiteSpace(constraintName) Then
                constraintName = descriptor.GetConstraintName(.SchemaName, DirectCast(.Parent, IDBObject).Name)
            End If

            ret = String.Format("ALTER TABLE {0}.{1} WITH CHECK 
ADD CONSTRAINT {2} FOREIGN KEY ({3}) REFERENCES {4}.{5} ({6})
GO
ALTER TABLE {0}.{1} CHECK CONSTRAINT {2}
", .SchemaName, DirectCast(.Parent, IDBObject).Name, constraintName, FKColumns, descriptor.PKTableSchemaName, descriptor.PKTableName, PKColumns)

        End With

        Return ret
    End Function

    Public Overrides Function GetSqlModify(dBType As eDBType) As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetSqlDelete(dBType As eDBType) As String
        Dim ret As String = ""

        With Me
            Dim descriptor As IDBForeignKeyConstraintDescriptor = DirectCast(.Descriptor, IDBForeignKeyConstraintDescriptor)

            Dim constraintName As String = descriptor.ConstraintName
            If String.IsNullOrWhiteSpace(constraintName) Then
                constraintName = descriptor.GetConstraintName(.SchemaName, DirectCast(.Parent, IDBObject).Name)
            End If

            ret = String.Format("ALTER TABLE {0}.{1}
DROP Constraint {2}
", .SchemaName, DirectCast(.Parent, IDBObject).Name, constraintName)

        End With

        Return ret
    End Function
End Class

