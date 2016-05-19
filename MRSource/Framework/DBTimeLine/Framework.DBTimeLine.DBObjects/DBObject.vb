Option Strict On

Public MustInherit Class DBObject
    Inherits DBParent
    Implements IDBObject

#Region "IDBObject"
    Public Property Name As String Implements IDBObject.Name

    Public Property Parent As IDBChained Implements IDBChained.Parent
    Public Property Descriptor As IDBObjectDescriptor Implements IDBObject.Descriptor
    Public ReadOnly Property Revisions As New List(Of IDBRevision) Implements IDBObject.Revisions

    'Public MustOverride ReadOnly Property ObjectType As eDBObjectType Implements IDBObject.ObjectType

    Public MustOverride ReadOnly Property ObjectTypeOrdinal As Integer Implements IDBObject.ObjectTypeOrdinal
    Public MustOverride ReadOnly Property ObjectTypeName As String Implements IDBObject.ObjectTypeName

    Public ReadOnly Property DBTimeLiner As DBTimeLiner Implements IDBObject.DBTimeLiner
        Get
            Dim ret As DBTimeLiner = Nothing

            Dim p As IDBChained = Me
            While p IsNot Nothing
                If TypeOf p Is DBTimeLiner Then
                    ret = CType(p, DBTimeLiner)
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
        DBTimeLiner.SourceDBSqlRevisions.Add(sqlRevision)

        If Not DBTimeLiner.SourceDBRevisions.ContainsKey(sqlRevision.Key) Then
            DBTimeLiner.SourceDBRevisions.Add(sqlRevision.Key, revision)
        End If

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

    Public MustOverride Function GetSqlModify(dBType As eDBType) As String Implements IDBObject.GetSqlModify

    Public MustOverride Function GetSqlDelete(dBType As eDBType) As String Implements IDBObject.GetSqlDelete

    Public MustOverride Function GetSqlCreate(dBType As eDBType) As String Implements IDBObject.GetSqlCreate

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
