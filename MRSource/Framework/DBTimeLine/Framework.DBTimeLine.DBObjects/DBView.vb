Imports Framework.DBCreator

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

    Public Function AddIndex(descriptor As IDBIndexDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBIndex Implements IDBView.AddIndex
        Return CType(MyBase.AddDBObject(descriptor.GetIndexName(SchemaName, Name), descriptor, createRevision), IDBIndex)
    End Function

End Class
