Public Class DBSchema
    Inherits DBObject

    Public Sub New(name As String)
        Me.Name = name
    End Sub

    Public Overrides ReadOnly Property DBObjectType As eDBObjectType
        Get
            Return eDBObjectType.Schema
        End Get
    End Property

    Private ReadOnly _DBTables As New List(Of DBTable)
    Public ReadOnly Property DBTables As List(Of DBTable)
        Get
            Return _DBTables
        End Get
    End Property

    Public Function AddTable(table As DBTable, createRevision As DBRevision) As DBTable
        Me.DBTables.Add(table)
        table.Parent = Me
        table.AddRevision(createRevision)

        Return table
    End Function

End Class
