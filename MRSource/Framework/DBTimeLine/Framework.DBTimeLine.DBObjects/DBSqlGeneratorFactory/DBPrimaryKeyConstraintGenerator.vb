Public Class DBPrimaryKeyConstraintGenerator
    Implements IDBObjectGenerator

    Public Function GetSqlCreate(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlCreate
        Dim ret As String = ""

        With DirectCast(dbObject, IDBPrimaryKeyConstraint)
            Dim descriptor As IDBPrimaryKeyConstraintDescriptor = DirectCast(.Descriptor, IDBPrimaryKeyConstraintDescriptor)

            Dim constraintName As String = descriptor.ConstraintName

            Dim columns As String = ""
            For Each col As String In descriptor.Columns
                columns &= col & ","
            Next
            columns = columns.TrimEnd(","c)

            If String.IsNullOrWhiteSpace(constraintName) Then
                constraintName = descriptor.GetConstraintName(.SchemaName, DirectCast(.Parent, IDBObject).Name)
            End If

            ret = String.Format("ALTER TABLE {0}.{1}
ADD CONSTRAINT {2} PRIMARY KEY ({3})
", .SchemaName, DirectCast(.Parent, IDBObject).Name, constraintName, columns)

        End With

        Return ret
    End Function

    Public Function GetSqlDelete(dbObject As IDBObject) As String Implements IDBObjectGenerator.GetSqlDelete
        Dim ret As String = ""

        With DirectCast(dbObject, IDBPrimaryKeyConstraint)
            Dim descriptor As IDBPrimaryKeyConstraintDescriptor = DirectCast(.Descriptor, IDBPrimaryKeyConstraintDescriptor)

            Dim constraintName As String = descriptor.ConstraintName
            If String.IsNullOrWhiteSpace(constraintName) Then
                If String.IsNullOrWhiteSpace(constraintName) Then
                    constraintName = descriptor.GetConstraintName(.SchemaName, DirectCast(.Parent, IDBObject).Name)
                End If
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
