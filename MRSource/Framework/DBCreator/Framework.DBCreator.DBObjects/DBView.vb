Public Class DBView
    Inherits DBObject
    Implements IDBView

    Public Sub New()

    End Sub

    Public Sub New(descriptor As IDBViewDescriptor)
        MyClass.New

        Me.Descriptor = descriptor
    End Sub

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.View
        End Get
    End Property

End Class
