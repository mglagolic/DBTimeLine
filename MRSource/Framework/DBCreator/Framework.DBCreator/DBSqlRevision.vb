Imports MRFramework
Imports MRFramework.MRPersisting.Core

Public Class DBSqlRevision
    Public Property Created As Date
    Public Property Granulation As Integer
    Public Property ObjectFullName As String
    Public Property ObjectType As eDBObjectType
    Public ReadOnly Property ObjectTypeName As String
        Get
            Return [Enum].GetName(GetType(eDBObjectType), ObjectType)
        End Get
    End Property
    Public Property RevisionType As eDBRevisionType
    Public ReadOnly Property RevisionTypeName As String
        Get
            Return [Enum].GetName(GetType(eDBRevisionType), RevisionType)
        End Get
    End Property
    Public Property Parent As IDBRevision

    Public Property ModuleKey As String
    Public Property SchemaName As String
    Public Property SchemaObjectName As String
    Public Property ObjectName As String

    Public Property Sql As String
    Public Property Description As String

    Public Sub New(dBRevision As IDBRevision)
        With dBRevision
            Created = .Created
            Granulation = .Granulation

            ObjectType = .Parent.ObjectType
            RevisionType = .DBRevisionType

            ModuleKey = .Parent.ModuleKey
            SchemaName = .Parent.SchemaName
            SchemaObjectName = .Parent.SchemaObjectName
            ObjectName = .Parent.Name

            ObjectFullName = .Parent.GetFullName
            Sql = .GetSql
            Description = Sql
            Parent = dBRevision
        End With
    End Sub
    Public Sub New(dlo As IMRDLO, dBCreator As DBCreator)
        Created = CDate(dlo.ColumnValues("Created"))
        Granulation = CInt(dlo.ColumnValues("Granulation"))

        ObjectType = CType([Enum].Parse(GetType(eDBObjectType), CStr(dlo.ColumnValues("ObjectType"))), eDBObjectType)
        RevisionType = CType([Enum].Parse(GetType(eDBRevisionType), CStr(dlo.ColumnValues("RevisionType"))), eDBRevisionType)

        ModuleKey = CStr(dlo.ColumnValues("ModuleKey"))
        SchemaName = CStr(dlo.ColumnValues("SchemaName"))
        SchemaObjectName = CStr(dlo.ColumnValues("SchemaObjectName"))
        ObjectName = CStr(dlo.ColumnValues("ObjectName"))

        ObjectFullName = CStr(dlo.ColumnValues("ObjectName"))

        Parent = FindParent(dBCreator)
    End Sub

    'TODO - ovo popraviti
    Private Function FindParent(dBCreator As DBCreator) As IDBRevision
        Dim ret As IDBRevision = Nothing
        Dim dbObject As IDBObject = Nothing
        Select Case ObjectType
            Case eDBObjectType.Field
                If dBCreator.DBFields.ContainsKey(ObjectName) Then
                    dbObject = dBCreator.DBFields(ObjectName)
                End If

            Case eDBObjectType.Table
                If dBCreator.DBTables.ContainsKey(ObjectName) Then
                    dbObject = dBCreator.DBTables(ObjectName)
                End If
        End Select
        If dbObject IsNot Nothing Then
            ret = dbObject.FindRevision(Created, Granulation)
        End If

        Return ret
    End Function

    Public Function GetDlo() As IMRDLO
        Dim dlo As New MRPersisting.MRDLO
        With dlo.ColumnValues
            .Add("Created", Created)
            .Add("Granulation", Granulation)

            .Add("ObjectType", ObjectTypeName)
            .Add("RevisionType", RevisionTypeName)

            .Add("ModuleKey", ModuleKey)
            .Add("SchemaName", SchemaName)
            .Add("SchemaObjectName", SchemaObjectName)
            .Add("ObjectName", ObjectName)

            .Add("ObjectFullName", ObjectFullName)
            .Add("Description", Description)

        End With
        Return dlo
    End Function



    Public Shared Function CompareRevisionsForDbCreations(rev1 As DBSqlRevision, rev2 As DBSqlRevision) As Integer
        Dim ret As Integer = 0

        If ret = 0 Then
            ret = rev1.Created.CompareTo(rev2.Created)
        End If
        If ret = 0 Then
            ret = rev1.Granulation.CompareTo(rev2.Granulation)
        End If
        If ret = 0 Then
            ret = rev1.ObjectType.CompareTo(rev2.ObjectType)
        End If
        If ret = 0 Then
            ret = rev1.RevisionType.CompareTo(rev2.RevisionType)
        End If
        If ret = 0 Then
            ret = rev1.ModuleKey.CompareTo(rev2.ModuleKey)
        End If
        If ret = 0 Then
            ret = rev1.SchemaName.CompareTo(rev2.SchemaName)
        End If
        If ret = 0 Then
            ret = rev1.SchemaObjectName.CompareTo(rev2.SchemaObjectName)
        End If
        If ret = 0 Then
            ret = rev1.ObjectName.CompareTo(rev2.ObjectName)
        End If

        Return ret
    End Function

#Region "EqualityComparer"
    Public Class DBSqlRevisionEqualityComparer
        Implements IEqualityComparer(Of DBSqlRevision)

        Public Shadows Function Equals(x As DBSqlRevision, y As DBSqlRevision) As Boolean Implements IEqualityComparer(Of DBSqlRevision).Equals
            Dim ret As Boolean = False

            Dim compareResult As Integer = DBSqlRevision.CompareRevisionsForDbCreations(x, y)
            If compareResult = 0 Then
                ret = True
            End If

            Return ret
        End Function

        Public Shadows Function GetHashCode(obj As DBSqlRevision) As Integer Implements IEqualityComparer(Of DBSqlRevision).GetHashCode
            With obj
                Return _
                    .Created.GetHashCode Xor
                    .Granulation.GetHashCode Xor
                    .ObjectType.GetHashCode Xor
                    .RevisionType.GetHashCode Xor
                    .ModuleKey.GetHashCode Xor
                    .SchemaName.GetHashCode Xor
                    .SchemaObjectName.GetHashCode Xor
                    .ObjectName.GetHashCode
            End With
        End Function
    End Class

#End Region

#Region "Persister"

    Public Class DBSqlRevisionPersister
        Inherits MRPersisting.MRPersister

        Public Overrides ReadOnly Property DataBaseTableName As String
            Get
                Return "DBCreator.Revision"
            End Get
        End Property
        Public Overrides ReadOnly Property SQL As String
            Get
                Return "SELECT ID, Created, Granulation, ObjectType, RevisionType, ModuleKey, SchemaName, SchemaObjectName, ObjectName, ObjectFullName FROM " & DataBaseTableName
            End Get
        End Property
    End Class

#End Region


End Class
