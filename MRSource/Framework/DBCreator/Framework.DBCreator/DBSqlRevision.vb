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

    Public Property Key As String

    Private Sub New()

    End Sub

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

            Key = GetDBSqlRevisionKey()
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

        Key = GetDBSqlRevisionKey()

        Parent = FindParent(dBCreator)
    End Sub

    Public Function GetDBSqlRevisionKey() As String
        Dim ret As String = String.Empty

        Dim sbFullName As New Text.StringBuilder
        sbFullName.Append(Created.ToString("yyyy-MM-dd"))
        sbFullName.Append(".")
        sbFullName.Append(Granulation.ToString)
        sbFullName.Append(".")
        sbFullName.Append(ObjectType.ToString)
        sbFullName.Append(".")
        sbFullName.Append(RevisionType.ToString)
        sbFullName.Append(".")
        sbFullName.Append(ModuleKey)
        sbFullName.Append(".")
        sbFullName.Append(SchemaName)

        Select Case ObjectType
            Case eDBObjectType.Table, eDBObjectType.View
                sbFullName.Append(".")
                sbFullName.Append(SchemaObjectName)
            Case eDBObjectType.Field
                sbFullName.Append(".")
                sbFullName.Append(SchemaObjectName)
                sbFullName.Append(".")
                sbFullName.Append(ObjectName)
        End Select

        ret = sbFullName.ToString

        Return ret
    End Function

    Private Function FindParent(dBCreator As DBCreator) As IDBRevision
        Dim ret As IDBRevision = Nothing

        If dBCreator.SourceDBRevisions.ContainsKey(Key) Then
            ret = dBCreator.SourceDBRevisions(Key)
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

        ret = rev1.Key.CompareTo(rev2.Key)

        Return ret
    End Function

#Region "EqualityComparer"
    Public Class DBSqlRevisionEqualityComparer
        Implements IEqualityComparer(Of DBSqlRevision)

        Public Shadows Function Equals(x As DBSqlRevision, y As DBSqlRevision) As Boolean Implements IEqualityComparer(Of DBSqlRevision).Equals
            Dim ret As Boolean = False

            If CompareRevisionsForDbCreations(x, y) = 0 Then
                ret = True
            End If

            Return ret
        End Function

        Public Shadows Function GetHashCode(obj As DBSqlRevision) As Integer Implements IEqualityComparer(Of DBSqlRevision).GetHashCode
            Return obj.Key.GetHashCode
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
