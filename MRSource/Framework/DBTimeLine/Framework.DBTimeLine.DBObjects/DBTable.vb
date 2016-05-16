Option Strict On
Imports Framework.DBCreator

Public Class DBTable
    Inherits DBObject
    Implements IDBTable

    Public Sub New()

    End Sub

    Public Sub New(descriptor As IDBTableDescriptor)
        MyClass.New

        Me.Descriptor = descriptor
        If Not String.IsNullOrWhiteSpace(descriptor.CreatorFieldName) AndAlso descriptor.CreatorFieldDescriptor IsNot Nothing Then
            AddField(descriptor.CreatorFieldName, descriptor.CreatorFieldDescriptor)
        End If
    End Sub

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.Table
        End Get
    End Property

    Public Function AddConstraint(descriptor As IDBConstraintDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBConstraint Implements IDBTable.AddConstraint
        Return CType(MyBase.AddDBObject(descriptor.GetConstraintName(SchemaName, Name), descriptor, createRevision), IDBConstraint)
    End Function

    Public Function AddField(fieldName As String, descriptor As IDBFieldDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBField Implements IDBTable.AddField
        Return CType(MyBase.AddDBObject(fieldName, descriptor, createRevision), IDBField)
    End Function

    Public Function AddIndex(descriptor As IDBIndexDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBIndex Implements IDBTable.AddIndex
        Return CType(MyBase.AddDBObject(descriptor.GetIndexName(SchemaName, Name), descriptor, createRevision), IDBIndex)
    End Function
End Class
