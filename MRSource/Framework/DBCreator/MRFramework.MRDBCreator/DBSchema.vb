Public Interface IDBSchema

End Interface

Public Class DBSchemaDescriptor
    Implements IDBObjectDescriptor
    Implements IDBSchema

End Class

Public Class DBSchema
    Inherits DBObject
    Implements IDBSchema

    Public Sub New()

    End Sub

    Public Sub New(name As String, descriptor As DBSchemaDescriptor)
        MyClass.New

        ApplyDescriptor(descriptor)
    End Sub

    Public Overrides ReadOnly Property DBObjectType As eDBObjectType
        Get
            Return eDBObjectType.Schema
        End Get
    End Property

    Private ReadOnly _DBTables As New Dictionary(Of String, DBTable)
    Public ReadOnly Property DBTables As Dictionary(Of String, DBTable)
        Get
            Return _DBTables
        End Get
    End Property

    Public Function AddTable(tableName As String, descriptor As DBTableDescriptor, Optional createRevision As DBRevision = Nothing) As DBTable
        If Not DBTables.ContainsKey(tableName) Then
            DBTables.Add(tableName, New DBTable(descriptor) With {.Name = tableName, .Parent = Me})
        End If
        Dim table As DBTable = DBTables(tableName)

        If createRevision IsNot Nothing Then
            table.AddRevision(createRevision)
        End If

        Return table
    End Function

    Public Overrides Sub ApplyDescriptor(descriptor As IDBObjectDescriptor)

    End Sub

    Public Overrides Function GetDescriptor() As IDBObjectDescriptor
        Return New DBSchemaDescriptor
    End Function

End Class
