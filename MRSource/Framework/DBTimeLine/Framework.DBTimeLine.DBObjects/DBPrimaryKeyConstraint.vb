Public Class DBPrimaryKeyConstraint
    Inherits DBObject
    Implements IDBPrimaryKeyConstraint

    Public Sub New()

    End Sub

    Public Overrides ReadOnly Property ObjectTypeOrdinal As Integer
        Get
            Return 40
        End Get
    End Property

    Public Overrides ReadOnly Property ObjectTypeName As String
        Get
            Return "PrimaryKey"
        End Get
    End Property

    Public Overrides Function GetSqlCreate(dBType As eDBType) As String
        Dim ret As String = ""

        With Me
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

    Public Overrides Function GetSqlModify(dBType As eDBType) As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetSqlDelete(dBType As eDBType) As String
        Dim ret As String = ""

        Dim constraintName As String = DirectCast(Descriptor, IDBPrimaryKeyConstraintDescriptor).ConstraintName
        If String.IsNullOrWhiteSpace(constraintName) Then
                If String.IsNullOrWhiteSpace(constraintName) Then
                constraintName = DirectCast(Descriptor, IDBPrimaryKeyConstraintDescriptor).GetConstraintName(SchemaName, DirectCast(Parent, IDBObject).Name)
            End If
            End If

        ret = String.Format("ALTER TABLE {0}.{1}
DROP Constraint {2}
", SchemaName, DirectCast(Parent, IDBObject).Name, constraintName)

        Return ret
    End Function

End Class

