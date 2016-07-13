Imports MRFramework
Imports MRFramework.MRPersisting.Core

Public Class DBSqlRevision
    Private Const MaxGranulationPower As Integer = 3

    Public Property Created As Date

    Private _Granulation As Integer = 0
    Public Property Granulation As Integer
        Get
            Return _Granulation
        End Get
        Set(value As Integer)
            If value < 0 OrElse value > Math.Pow(10, MaxGranulationPower) - 1 Then
                Throw New ArgumentOutOfRangeException("Granulation must be between 0 and 999.")
            End If
            _Granulation = value
        End Set
    End Property

    Public Property ObjectFullName As String

    Public Property ObjectTypeOrdinal As Integer
    Public Property ObjectTypeName As String

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

            ObjectTypeName = .Parent.ObjectTypeName
            ObjectTypeOrdinal = .Parent.ObjectTypeOrdinal
            RevisionType = .DBRevisionType

            ModuleKey = .Parent.ModuleKey
            SchemaName = .Parent.SchemaName
            SchemaObjectName = .Parent.SchemaObjectName
            ObjectName = .Parent.Name

            ObjectFullName = .Parent.GetFullName
            Sql = .GetSql 'TODO - razmisliti jel treba ovo odradjivati samo za nove revizije
            Description = Sql

            Parent = dBRevision

            Key = GetDBSqlRevisionKey()
        End With
    End Sub

    Public Sub New(dloColumnValues As Dictionary(Of String, Object), dBTimeLiner As DBTimeLiner)
        Created = CDate(dloColumnValues("Created"))
        Granulation = CInt(dloColumnValues("Granulation"))

        ObjectTypeOrdinal = CInt(dloColumnValues("ObjectTypeOrdinal"))
        ObjectTypeName = CStr(dloColumnValues("ObjectTypeName"))

        RevisionType = CType([Enum].Parse(GetType(eDBRevisionType), CStr(dloColumnValues("RevisionType"))), eDBRevisionType)

        ModuleKey = CStr(dloColumnValues("ModuleKey"))
        SchemaName = ""
        If dloColumnValues("SchemaName") IsNot DBNull.Value Then
            SchemaName = CStr(dloColumnValues("SchemaName"))
        End If

        SchemaObjectName = ""
        If dloColumnValues("SchemaObjectName") IsNot DBNull.Value Then
            SchemaObjectName = CStr(dloColumnValues("SchemaObjectName"))
        End If

        ObjectName = CStr(dloColumnValues("ObjectName"))
        ObjectFullName = CStr(dloColumnValues("ObjectName"))

        If dloColumnValues("Description") IsNot DBNull.Value Then
            Description = CStr(dloColumnValues("Description"))
        End If

        Key = CStr(dloColumnValues("RevisionKey"))

        Parent = FindParent(dBTimeLiner)
    End Sub

    Public Function GetDBSqlRevisionKey() As String
        Dim ret As String = String.Empty

        Dim sbFullName As New Text.StringBuilder
        sbFullName.Append(Created.ToString("yyyy-MM-dd"))
        sbFullName.Append(".")
        sbFullName.Append(Granulation.ToString.PadLeft(MaxGranulationPower, "0"c))
        sbFullName.Append(".")
        sbFullName.Append(ObjectTypeOrdinal.ToString("d3"))
        sbFullName.Append(".")
        sbFullName.Append(CType(RevisionType, Integer).ToString("d3"))
        sbFullName.Append(".")
        sbFullName.Append(ModuleKey)
        sbFullName.Append(".")
        sbFullName.Append(SchemaName)
        sbFullName.Append(".")
        sbFullName.Append(SchemaObjectName)
        sbFullName.Append(".")
        sbFullName.Append(ObjectName)

        ret = sbFullName.ToString

        Return ret
    End Function

    Private Function FindParent(dBTimeLiner As DBTimeLiner) As IDBRevision
        Dim ret As IDBRevision = Nothing

        If dBTimeLiner.SourceDBRevisions.ContainsKey(Key) Then
            ret = dBTimeLiner.SourceDBRevisions(Key)
        End If

        Return ret
    End Function

    Public Function GetDlo() As IMRDLO
        Dim dlo As New MRPersisting.MRDLO
        With dlo.ColumnValues
            .Add("Created", Created)
            .Add("Granulation", Granulation)

            .Add("ObjectTypeOrdinal", ObjectTypeOrdinal)
            .Add("ObjectTypeName", ObjectTypeName)

            .Add("RevisionType", RevisionTypeName)

            .Add("ModuleKey", ModuleKey)
            .Add("SchemaName", SchemaName)
            If Not String.IsNullOrWhiteSpace(SchemaObjectName) Then
                .Add("SchemaObjectName", SchemaObjectName)
            End If

            .Add("ObjectName", ObjectName)

            .Add("ObjectFullName", ObjectFullName)
            .Add("Description", Description)

            .Add("RevisionKey", Key)
        End With
        Return dlo
    End Function

    Public Shared Function CompareRevisionsForDbCreations(rev1 As DBSqlRevision, rev2 As DBSqlRevision) As Integer
        Dim ret As Integer = 0

        If rev1.RevisionType = eDBRevisionType.CreateIfNew AndAlso rev2.RevisionType = eDBRevisionType.CreateIfNew Then
            Dim desc1 = ""
            Dim desc2 = ""
            If rev1.Description IsNot Nothing Then
                desc1 = rev1.Description
            End If
            If rev2.Description IsNot Nothing Then
                desc2 = rev2.Description
            End If

            ret = (rev1.Key & desc1).CompareTo(rev2.Key & desc2)
        ElseIf rev1.RevisionType = eDBRevisionType.CreateIfNew AndAlso rev2.RevisionType <> eDBRevisionType.CreateIfNew Then
            ret = 1
        ElseIf rev1.RevisionType <> eDBRevisionType.CreateIfNew AndAlso rev2.RevisionType = eDBRevisionType.CreateIfNew Then
            ret = -1
        Else
            ret = rev1.Key.CompareTo(rev2.Key)
        End If

        Return ret
    End Function

    Public Shared Function CompareCreateIfNewRevisionsForDbCreations(rev1 As DBSqlRevision, rev2 As DBSqlRevision) As Integer
        Dim ret As Integer = 0

        ret = (rev1.Key & rev1.Description).CompareTo(rev2.Key & rev2.Description)

        Return ret
    End Function

#Region "EqualityComparer"
    Public Class DBSqlRevisionCreateIfNewEqualityComparer
        Implements IEqualityComparer(Of DBSqlRevision)

        Public Shadows Function Equals(x As DBSqlRevision, y As DBSqlRevision) As Boolean Implements IEqualityComparer(Of DBSqlRevision).Equals
            Dim ret As Boolean = False

            If CompareCreateIfNewRevisionsForDbCreations(x, y) = 0 Then
                ret = True
            End If

            Return ret
        End Function

        Public Shadows Function GetHashCode(obj As DBSqlRevision) As Integer Implements IEqualityComparer(Of DBSqlRevision).GetHashCode
            Return obj.Key.GetHashCode
        End Function
    End Class

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
    Public Class DBSqlRevisionPersisterNew
        Inherits Persisting.Persister

        Protected Overrides ReadOnly Property DataBaseTableName As String
            Get
                Return "DBTimeLine.Revision"
            End Get
        End Property

        Protected Overrides ReadOnly Property Sql As String
            Get
                Return _
    "WITH RevCTE AS
(
	SELECT 
		ID, 
		RevisionKey, 
		Created, 
		Granulation, 
		ObjectTypeOrdinal, 
		ObjectTypeName, 
		RevisionType, 
		ModuleKey, 
		SchemaName, 
		SchemaObjectName, 
		ObjectName, 
		ObjectFullName
	FROM 
		DBTimeLine.Revision t
)

SELECT 
	rev.*,
	Description = CAST(NULL AS NVARCHAR(MAX))
FROM 
	RevCTE rev
WHERE
	rev.RevisionType <> 'CreateIfNew'

UNION ALL

SELECT 
	rev.*,
	Description = lastCreateIfNew.Description
FROM 
	RevCTE rev
	INNER JOIN
	(
		SELECT 
			RevisionKey,
			ID,
			Description
		FROM
			(
				SELECT 
					RevisionKey,
					ID,
					Description,
					RBR = ROW_NUMBER() OVER(PARTITION BY RevisionKey ORDER BY t.Executed DESC)
				FROM 
					DBTimeLine.Revision t
				WHERE
					t.RevisionType = 'CreateIfNew'
			) t
		WHERE
			t.RBR = 1
	) lastCreateIfNew ON rev.RevisionKey = lastCreateIfNew.RevisionKey AND rev.ID = lastCreateIfNew.ID
"
            End Get
        End Property
    End Class

    Public Class DBSqlRevisionPersister
        Inherits MRPersisting.MRPersister

        Public Overrides ReadOnly Property DataBaseTableName As String
            Get
                Return "DBTimeLine.Revision"
            End Get
        End Property
        Public Overrides ReadOnly Property SQL As String
            Get
                Return "SELECT RevisionKey, ID FROM " & DataBaseTableName
            End Get
        End Property
    End Class

    Public Class DBSqlAlwaysExecutingTaskPersister
            Inherits MRPersisting.MRPersister

            Public Overrides ReadOnly Property DataBaseTableName As String
                Get
                    Return "DBTimeLine.AlwaysExecutingTask"
                End Get
            End Property
            Public Overrides ReadOnly Property SQL As String
                Get
                    Return "SELECT Key, ID FROM " & DataBaseTableName
                End Get
            End Property
        End Class

#End Region


    End Class
