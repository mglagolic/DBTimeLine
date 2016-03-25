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

    Public Function AddConstraint(descriptor As IDBObjectDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBObject Implements IDBTable.AddConstraint
        Dim constraintName As String = ""
        With DirectCast(descriptor, IDBPrimaryKeyConstraintDescriptor)
            constraintName = .ConstraintName
            If String.IsNullOrWhiteSpace(constraintName) Then
                Dim columns As String = ""
                For Each col As String In .Columns
                    columns &= col & ","
                Next
                columns = columns.TrimEnd(","c)

                constraintName = "PK_" & SchemaName & "_" & Name & "_" & columns.Replace(","c, "_")
            End If
        End With

        Return MyBase.AddDBObject(constraintName, descriptor, createRevision)
    End Function

    Public Function AddField(fieldName As String, descriptor As IDBFieldDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBObject Implements IDBTable.AddField
        Return MyBase.AddDBObject(fieldName, descriptor, createRevision)
    End Function

End Class
