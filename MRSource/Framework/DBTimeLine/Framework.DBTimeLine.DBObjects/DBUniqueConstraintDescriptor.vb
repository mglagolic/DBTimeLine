Public Class DBUniqueConstraintDescriptor
    Implements IDBUniqueConstraintDescriptor

    Public Property ConstraintName As String Implements IDBUniqueConstraintDescriptor.ConstraintName

    Public ReadOnly Property Columns As New List(Of String) Implements IDBUniqueConstraintDescriptor.Columns

    Public Overridable Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBUniqueConstraint() With {.Parent = parent, .Descriptor = Me}
    End Function

    Private Sub New()

    End Sub

    Public Sub New(column As String, ParamArray columns() As String)
        MyClass.New(Nothing, column, columns)

    End Sub
    Public Sub New(constraintName As String, column As String, ParamArray columns() As String)
        MyClass.New()

        Me.ConstraintName = constraintName
        Me.Columns.Add(column)
        If columns IsNot Nothing Then
            Me.Columns.AddRange(columns)
        End If
    End Sub

    Public Sub New(descriptor As IDBObjectDescriptor)
        MyClass.New()

        With CType(descriptor, IDBUniqueConstraintDescriptor)
            ConstraintName = .ConstraintName
            Columns.AddRange(.Columns)
        End With
    End Sub

    Public Function GetConstraintName(schemaName As String, tableName As String) As String Implements IDBConstraintDescriptor.GetConstraintName
        Dim ret As String = ConstraintName
        If String.IsNullOrWhiteSpace(ret) Then
            Dim cols As String = ""
            For Each col As String In Columns
                cols &= col & ","
            Next
            cols = cols.TrimEnd(","c)

            ret = "PK_" & schemaName & "_" & tableName & "_" & cols.Replace(","c, "_")
        End If
        Return ret
    End Function
End Class