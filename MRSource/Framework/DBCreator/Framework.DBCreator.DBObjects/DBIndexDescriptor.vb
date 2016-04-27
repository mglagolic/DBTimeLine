Imports Framework.DBCreator

Public Class DBIndexDescriptor
    Implements IDBIndexDescriptor

    Public Property Unique As Boolean Implements IDBIndexDescriptor.Unique
    Public Property Clustered As Boolean Implements IDBIndexDescriptor.Clustered
    Public ReadOnly Property Keys As New List(Of String) Implements IDBIndexDescriptor.Keys
    Public ReadOnly Property Include As New List(Of String) Implements IDBIndexDescriptor.Include
    Public Property IndexName As String Implements IDBIndexDescriptor.IndexName

    Public Overridable Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New DBIndex() With {.Parent = parent, .Descriptor = Me}
    End Function

    Public Sub New(keys As List(Of String))
        Me.Keys.AddRange(keys)
    End Sub

    Public Sub New(keys As List(Of String), include As List(Of String))
        MyClass.New(keys)

        Me.Include.AddRange(include)
    End Sub

    Public Sub New(descriptor As IDBObjectDescriptor)
        MyClass.New(CType(descriptor, IDBIndexDescriptor).Keys, CType(descriptor, IDBIndexDescriptor).Include)

        With CType(descriptor, IDBIndexDescriptor)
            Unique = .Unique
            Clustered = .Clustered
        End With
    End Sub

    Public Function GetIndexName(schemaName As String, tableName As String) As String Implements IDBIndexDescriptor.GetIndexName
        Dim ret As String = ""

        Dim strKeys As String = ""
        Dim sb As New Text.StringBuilder()
        For i As Integer = 0 To Keys.Count - 1
            sb.Append(Keys(i))
            sb.Append("_")
        Next
        sb.Length -= 1
        strKeys = sb.ToString
        sb.Clear()

        ret = "IX_" & schemaName & "_" & tableName & "_" & strKeys

        Return ret
    End Function
End Class