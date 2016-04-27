Public Class DBIndex
    Inherits DBObject
    Implements IDBIndex

    Public Sub New()

    End Sub

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.Index
        End Get
    End Property

End Class
