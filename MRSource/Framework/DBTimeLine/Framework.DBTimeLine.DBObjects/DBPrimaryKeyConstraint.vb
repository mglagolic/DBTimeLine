Public Class DBPrimaryKeyConstraint
    Inherits DBObject
    Implements IDBPrimaryKeyConstraint

    Public Sub New()

    End Sub

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.Constraint
        End Get
    End Property

End Class

