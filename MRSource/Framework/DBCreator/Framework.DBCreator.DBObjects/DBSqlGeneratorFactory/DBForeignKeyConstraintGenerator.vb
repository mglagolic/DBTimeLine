Public Class DBForeignKeyConstraintGenerator
    Implements IDBObjectGenerator

    Public Function GetSqlCreate(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlCreate
        Dim ret As String = ""

        With DirectCast(dbObject, IDBForeignKeyConstraint)
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

    Public Function GetSqlDelete(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlDelete
        Dim ret As String = ""

        With DirectCast(dbObject, IDBForeignKeyConstraint)
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

    Public Function GetSqlModify(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlModify
        Throw New NotImplementedException()
    End Function
End Class
