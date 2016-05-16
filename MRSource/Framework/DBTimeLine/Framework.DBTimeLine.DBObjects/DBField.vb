Public Class DBField
    Inherits DBObject
    Implements IDBField

    Public Sub New()

    End Sub

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.Field
        End Get
    End Property

End Class
