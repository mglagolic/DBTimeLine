Public Class DBPrimaryKeyConstraintDescriptor
    Implements IDBPrimaryKeyConstraintDescriptor

    Public Property ConstraintName As String Implements IDBPrimaryKeyConstraintDescriptor.ConstraintName
    Public ReadOnly Property Columns As New List(Of String) Implements IDBPrimaryKeyConstraintDescriptor.Columns

    Public Overridable Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBPrimaryKeyConstraint() With {.Parent = parent, .Descriptor = Me}
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

        With CType(descriptor, IDBPrimaryKeyConstraintDescriptor)
            ConstraintName = .ConstraintName
            Columns.AddRange(.Columns)
        End With
    End Sub

End Class