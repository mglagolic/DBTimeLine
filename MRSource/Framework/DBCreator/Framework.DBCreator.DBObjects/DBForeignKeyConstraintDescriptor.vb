Public Class DBForeignKeyConstraintDescriptor
    Implements IDBForeignKeyConstraintDescriptor

    Public Property ConstraintName As String Implements IDBForeignKeyConstraintDescriptor.ConstraintName

    Public ReadOnly Property FKTableColumns As New List(Of String) Implements IDBForeignKeyConstraintDescriptor.FKTableColumns
    Public ReadOnly Property PKTableColumns As New List(Of String) Implements IDBForeignKeyConstraintDescriptor.PKTableColumns
    Public Property PKTableSchemaName As String Implements IDBForeignKeyConstraintDescriptor.PKTableSchemaName
    Public Property PKTableName As String Implements IDBForeignKeyConstraintDescriptor.PKTableName
    Public Overridable Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBForeignKeyConstraint() With {.parent = parent, .Descriptor = Me}
    End Function

    Private Sub New()

    End Sub

    Public Sub New(FKTableColumns As List(Of String), PKFullTableName As String, PKTableColumns As List(Of String))
        MyClass.New()

        Me.FKTableColumns.AddRange(FKTableColumns)
        PKTableSchemaName = PKFullTableName.Split(".")(0)
        PKTableName = PKFullTableName.Split(".")(1)
        Me.PKTableColumns.AddRange(PKTableColumns)

    End Sub

    Public Function GetConstraintName(schemaName As String, tableName As String) As String Implements IDBConstraintDescriptor.GetConstraintName
        Dim ret As String = ConstraintName

        If String.IsNullOrWhiteSpace(ret) Then
            Dim FKColumns As String = ""
            For Each col As String In FKTableColumns
                FKColumns &= col & ","
            Next
            FKColumns = FKColumns.TrimEnd(","c)

            Dim PKColumns As String = ""
            For Each col As String In PKTableColumns
                PKColumns &= col & ","
            Next
            PKColumns = PKColumns.TrimEnd(","c)

            ret = "FK_" & schemaName & "_" & tableName & "_" & FKColumns.Replace(","c, "_") & "_" &
                                        PKTableSchemaName & "_" & PKTableName & "_" & PKColumns.Replace(","c, "_")
        End If

        Return ret
    End Function
End Class