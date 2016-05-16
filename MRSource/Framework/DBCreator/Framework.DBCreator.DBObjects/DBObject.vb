Option Strict On

Public MustInherit Class DBObject
    Inherits DBParent
    Implements IDBObject

#Region "IDBObject"
    Public Property Name As String Implements IDBObject.Name

    Public Property Parent As IDBChained Implements IDBChained.Parent
    Public Property Descriptor As IDBObjectDescriptor Implements IDBObject.Descriptor
    Public ReadOnly Property Revisions As New List(Of IDBRevision) Implements IDBObject.Revisions

    Public MustOverride ReadOnly Property ObjectType As eDBObjectType Implements IDBObject.ObjectType

    Public ReadOnly Property DBTimeLine As DBTimeLine Implements IDBObject.DBTimeLine
        Get
            Dim ret As DBTimeLine = Nothing

            Dim p As IDBChained = Me
            While p IsNot Nothing
                If TypeOf p Is DBTimeLine Then
                    ret = CType(p, DBTimeLine)
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

        Dim sqlRevision As New DBSqlRevision(revision)
        DBTimeLine.SourceDBSqlRevisions.Add(sqlRevision)

        'If Not DBCreator.SourceDBRevisions.ContainsKey(sqlRevision.Key) Then
        DBTimeLine.SourceDBRevisions.Add(sqlRevision.Key, revision)
        'End If

        Return revision
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

    Public Overridable Function GetSqlCreate(sqlGenerator As IDBSqlGenerator) As String Implements IDBObject.GetSqlCreate
        Dim ret As String = ""

        ret = sqlGenerator.GetSqlCreate(Me)

        Return ret
    End Function

    Public Overridable Function GetSqlModify(sqlGenerator As IDBSqlGenerator) As String Implements IDBObject.GetSqlModify
        Dim ret As String = ""

        ret = sqlGenerator.GetSqlModify(Me)

        Return ret
    End Function

    Public Overridable Function GetSqlDelete(sqlGenerator As IDBSqlGenerator) As String Implements IDBObject.GetSqlDelete
        Dim ret As String = ""

        ret = sqlGenerator.GetSqlDelete(Me)

        Return ret
    End Function

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
                If p.Parent IsNot Nothing AndAlso TypeOf p.Parent Is IDBSchema Then
                    ret = CType(p, IDBObject).Name
                    Exit While
                Else
                    p = p.Parent
                End If
            End While

            Return ret
        End Get
    End Property

#End Region

End Class
