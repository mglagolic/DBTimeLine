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
            Dim newTable As New DBTable(descriptor) With {.Name = tableName, .Parent = Me}
            DBTables.Add(tableName, newTable)
            Creator.DBTables.Add(tableName, newTable)
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

    Public Overrides Function GetSqlCreate() As String
        Dim ret As String =
<string>;CREATE SCHEMA <%= Name %>
</string>.Value

        Return ret
    End Function

    Public Overrides Function GetSqlModify() As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetSqlDelete() As String
        Dim ret As String =
<string>;DROP SCHEMA <%= Name %></string>.Value

        Return ret
    End Function
End Class
