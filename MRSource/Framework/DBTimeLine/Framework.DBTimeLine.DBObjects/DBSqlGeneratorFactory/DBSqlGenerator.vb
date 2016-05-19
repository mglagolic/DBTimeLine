Public Class DBSqlGenerator
    Implements IDBSqlGenerator

    Public Sub New()

    End Sub

    Public Overridable Function GetSqlCreateSystemModuleTable() As String Implements IDBSqlGenerator.GetSqlCreateSystemModuleTable
        Dim ret As String = "
IF OBJECT_ID('DBTimeLine.Module') IS NULL
BEGIN
	CREATE  TABLE [DBTimeLine].[Module]
	(
        [ID] INT PRIMARY KEY,
        [ClassName] [varchar](250) NOT NULL,
        [DefaultSchemaName] [nvarchar](250) NOT NULL,
        [Name] [nvarchar](250) NOT NULL,
        [Created] [datetime] NOT NULL,
        [Active] bit NOT NULL,
        [AssemblyName] [varchar](250) NULL,
        [Description] [nvarchar](MAX) NULL
	)
END
"
        Return ret
    End Function

    Public Overridable Function GetSqlCreateSystemSchema() As String Implements IDBSqlGenerator.GetSqlCreateSystemSchema
        Dim ret As String = "CREATE SCHEMA DBTimeLine"

        Return ret
    End Function

    Private Enum eRevisionTable
        Revision = 0
        AlwaysExecutingTask = 1
    End Enum

    Private Function GetTableRevisionSql(revisionTable As eRevisionTable)
        Dim tableName As String = "Revision"
        Dim clusteredIndexFirstKeyColumn As String = "RevisionKey"

        If revisionTable = eRevisionTable.AlwaysExecutingTask Then
            tableName = "AlwaysExecutingTask"
            clusteredIndexFirstKeyColumn = "Executed"
        End If

        Dim ret As String = String.Format("
IF OBJECT_ID('DBTimeLine.{0}') IS NULL
BEGIN
	CREATE TABLE [DBTimeLine].[{0}]
	(
		[ID] [uniqueidentifier] NOT NULL PRIMARY KEY NONCLUSTERED,
        [RevisionKey] [varchar](800) NOT NULL,
		[Created] [date] NOT NULL,
		[Granulation] [int] NOT NULL,
        ObjectTypeOrdinal INT NOT NULL,
        ObjectTypeName VARCHAR(50) NOT NULL,
        [RevisionType] [varchar](50) NOT NULL,
        [ModuleKey] [varchar](50) NOT NULL,
        [SchemaName] [varchar](250),
        [SchemaObjectName] [varchar](250),
        [ObjectName] [varchar](250) NOT NULL,
		[Executed] [datetime] NOT NULL CONSTRAINT DF_DBTimeLine_{0}_Executed DEFAULT GETDATE(),
        [ObjectFullName] [varchar](512) NOT NULL,
        [Description] [nvarchar](MAX) NULL
	)
	IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name='IX_DBTimeLine{0}_Clustered' AND object_id = OBJECT_ID('DBTimeLine.{0}'))
	BEGIN
		DROP INDEX IX_DBTimeLine{0}_Clustered ON DBTimeLine.{0} 
	END
    CREATE CLUSTERED INDEX IX_DBTimeLine{0}_Clustered ON DBTimeLine.{0} ({1}, ID)
END
", tableName, clusteredIndexFirstKeyColumn)

        Return ret
    End Function

    Public Overridable Function GetSqlCreateSystemAlwaysExecutingTaskTable() As String Implements IDBSqlGenerator.GetSqlCreateSystemAlwaysExecutingTaskTable
        Return GetTableRevisionSql(eRevisionTable.AlwaysExecutingTask)
    End Function

    Public Overridable Function GetSqlCreateSystemRevisionTable() As String Implements IDBSqlGenerator.GetSqlCreateSystemRevisionTable
        Return GetTableRevisionSql(eRevisionTable.Revision)
    End Function

    Public Overridable Function GetSqlCheckIfSystemSchemaExists() As String Implements IDBSqlGenerator.GetSqlCheckIfSchemaExists
        Dim ret As String = "SELECT TOP 1 1 FROM sys.schemas WHERE name = 'DBTimeLine'"

        Return ret
    End Function
End Class
