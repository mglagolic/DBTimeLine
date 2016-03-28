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

End Class

