Imports Framework.DBCreator

Public MustInherit Class MyDBObject
    Implements IDBObject

    Public ReadOnly Property DBCreator As DBCreator Implements IDBObject.DBCreator
        Get
            Dim ret As DBCreator = Nothing

            Dim p As IDBChained = Me
            While p IsNot Nothing
                If TypeOf p Is DBCreator Then
                    ret = CType(p, DBCreator)
                    Exit While
                Else
                    p = p.Parent
                End If
            End While

            Return ret
        End Get
    End Property
    Public ReadOnly Property DBObjects As New Dictionary(Of String, IDBObject) Implements IDBObject.DBObjects
    Public Property Descriptor As IDBObjectDescriptor Implements IDBObject.Descriptor
    Public Property Name As String Implements IDBObject.Name
    Public MustOverride ReadOnly Property ObjectType As eDBObjectType Implements IDBObject.ObjectType
    Public Property Parent As IDBChained Implements IDBChained.Parent
    Public ReadOnly Property Revisions As New List(Of IDBRevision) Implements IDBObject.Revisions

    ReadOnly Property ModuleKey() As String Implements IDBObject.ModuleKey
        Get
            Dim ret As String = ""

            Dim p As IDBChained = Me
            While p IsNot Nothing
                If TypeOf p Is IDBModule Then
                    ret = CType(p, IDBModule).ModuleKey
                    Exit While
                Else
                    p = p.Parent
                End If
            End While

            Return ret
        End Get
    End Property

    Public ReadOnly Property SchemaName As String Implements IDBObject.SchemaName
        Get
            Dim ret As String = String.Empty

            Dim p As IDBChained = Me
            While p IsNot Nothing
                If TypeOf p Is IDBSchema Then
                    ret = DirectCast(p, IDBSchema).Name
                    Exit While
                Else
                    p = p.Parent
                End If
            End While

            Return ret
        End Get
    End Property

    ReadOnly Property SchemaObjectName() As String Implements IDBObject.SchemaObjectName
        Get
            Dim ret As String = ""

            Dim p As IDBChained = Me
            While p IsNot Nothing
                If TypeOf p Is IDBTable Then
                    ret = CType(p, IDBTable).Name
                    Exit While
                Else
                    p = p.Parent
                End If
            End While

            Return ret
        End Get
    End Property

    Public Function AddRevision(revision As IDBRevision, Optional descriptor As IDBObjectDescriptor = Nothing) As IDBRevision Implements IDBObject.AddRevision
        Revisions.Add(revision)

        revision.Parent = Me

        If descriptor IsNot Nothing Then
            Me.Descriptor = descriptor
        End If

        DBCreator.SourceDBSqlRevisions.Add(New DBSqlRevision(revision))

        Return revision
    End Function

    Public Function FindRevision(created As Date, granulation As Integer) As IDBRevision Implements IDBObject.FindRevision
        Dim ret As IDBRevision = Nothing
        ret = Revisions.Find(Function(rev) (rev.Created = created AndAlso rev.Granulation = granulation))

        Return ret
    End Function


    Public Function GetFullName() As String Implements IDBObject.GetFullName
        Dim sbFullName As New Text.StringBuilder

        Dim p As IDBObject = Me
        While p IsNot Nothing
            sbFullName.Insert(0, p.Name & ".")
            If TypeOf p.Parent Is IDBObject Then
                p = CType(p.Parent, IDBObject)
            Else
                p = Nothing
            End If
        End While

        Return sbFullName.ToString().TrimEnd("."c)
    End Function

    MustOverride Function GetSqlCreate() As String Implements IDBObject.GetSqlCreate
    MustOverride Function GetSqlModify() As String Implements IDBObject.GetSqlModify
    MustOverride Function GetSqlDelete() As String Implements IDBObject.GetSqlDelete
End Class

Public Class MyDBSchemaDescriptor
    Implements IDBSchemaDescriptor

    Public Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New MyDBSchema() With {.Parent = parent, .Descriptor = Me}
    End Function

End Class

Public Class MyDBSchema
    Inherits MyDBObject
    Implements IDBSchema

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
            Get
                Return eDBObjectType.Schema
            End Get
        End Property

        Public Function AddTable(tableName As String, descriptor As IDBTableDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBTable Implements IDBSchema.AddTable
            If Not DBObjects.ContainsKey(tableName) Then
                Dim newDBObject As IDBObject = descriptor.GetDBObjectInstance(Me)
                With newDBObject
                    .Name = tableName
                End With
                ' TODO - dodati fullobject name u idbobject tako da dic ne bi pucao ako postoje npr dvije scheme s istom tablicom
                DBObjects.Add(tableName, newDBObject)
            End If
            Dim table As IDBTable = DBObjects(tableName)

            If createRevision IsNot Nothing Then
                table.AddRevision(createRevision)
            End If

            Return table
        End Function

        Public Overrides Function GetSqlCreate() As String Implements IDBObject.GetSqlCreate
            Dim ret As String = ""

            ret = <string>GO
CREATE SCHEMA <%= Name %>
GO
</string>.Value

            Return ret
        End Function

        Public Overrides Function GetSqlModify() As String Implements IDBObject.GetSqlModify
            Throw New NotImplementedException()
        End Function

        Public Overrides Function GetSqlDelete() As String Implements IDBObject.GetSqlDelete
            Dim ret As String = ""

            ret =
<string>GO
DROP SCHEMA <%= Name & vbNewLine %>
GO
</string>.Value

            Return ret
        End Function
    End Class


Public Class MyDBTableDescriptor
    Implements IDBTableDescriptor

    Public Property CreatorFieldDescriptor As IDBFieldDescriptor Implements IDBTableDescriptor.CreatorFieldDescriptor
    Public Property CreatorFieldName As String Implements IDBTableDescriptor.CreatorFieldName
    Public Function GetDBObjectInstance(Optional parent As IDBChained = Nothing) As IDBObject Implements IDBObjectDescriptor.GetDBObjectInstance
        Return New MyDBTable() With {.Parent = parent, .Descriptor = Me}
    End Function
End Class

Public Class MyDBTable
    Inherits MyDBObject
    Implements IDBTable

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.Table
        End Get
    End Property

    Public Function AddField(fieldName As String, descriptor As IDBFieldDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBObject Implements IDBTable.AddField
        If Not DBObjects.ContainsKey(fieldName) Then
            Dim newDBObject As IDBObject = descriptor.GetDBObjectInstance(Me)
            With newDBObject
                .Name = fieldName
            End With
            ' TODO - dodati fullobject name u idbobject tako da dic ne bi pucao ako postoje npr dvije scheme s istom tablicom
            DBObjects.Add(fieldName, newDBObject)
        End If
        Dim table As IDBTable = DBObjects(fieldName)

        If createRevision IsNot Nothing Then
            table.AddRevision(createRevision)
        End If

        Return table
    End Function

    Public Overrides Function GetSqlCreate() As String Implements IDBObject.GetSqlCreate
        Dim ret As String = ""
        With CType(Descriptor, IDBTableDescriptor)
            ret =
<string>
CREATE TABLE <%= SchemaName %>.<%= Name %> 
(
    <%= .CreatorFieldName & " " %><%= .CreatorFieldDescriptor.GetFieldTypeSql %>
)
</string>.Value
        End With

        Return ret
    End Function

    Public Overrides Function GetSqlDelete() As String Implements IDBObject.GetSqlDelete
        Dim ret As String = ""

        ret =
<string>
DROP TABLE <%= SchemaName %>.<%= Name %>
</string>.Value


        Return ret
    End Function

    Public Overrides Function GetSqlModify() As String Implements IDBObject.GetSqlModify
        Throw New NotImplementedException()
    End Function

End Class
