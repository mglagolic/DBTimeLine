Public Class DBSqlGenerator
    Implements IDBSqlGenerator

    Public Sub New()

    End Sub

    Public Overridable Function GetSqlCreateSystemAlwaysExecutingTaskTable() As String Implements IDBSqlGenerator.GetSqlCreateSystemAlwaysExecutingTaskTable
        Dim ret As String = "
IF OBJECT_ID('DBTimeLine.AlwaysExecutingTask') IS NULL
BEGIN
	CREATE TABLE [DBTimeLine].[AlwaysExecutingTask]
	(
		[ID] [uniqueidentifier] NOT NULL PRIMARY KEY NONCLUSTERED,
        [RevisionKey] [varchar](800) NOT NULL,
		[Created] [date] NOT NULL,
		[Granulation] [int] NOT NULL,
        [ObjectType] [varchar](50) NOT NULL,        
        [RevisionType] [varchar](50) NOT NULL,
        [ModuleKey] [varchar](50),
        [SchemaName] [varchar](50),
        [SchemaObjectName] [varchar](150),
        [ObjectName] [varchar](150) NOT NULL,
		[Executed] [datetime] NOT NULL CONSTRAINT DF_DBTimeLine_AlwaysExecutingTask_Executed DEFAULT GETDATE(),
        [ObjectFullName] [varchar](100) NOT NULL,
        [Description] [nvarchar](MAX) NULL
	)
	IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name='IX_DBTimeLineAlwaysExecutingTask_Clustered' AND object_id = OBJECT_ID('DBTimeLine.AlwaysExecutingTask'))
	BEGIN
		DROP INDEX IX_DBTimeLineAlwaysExecutingTask_Clustered ON DBTimeLine.AlwaysExecutingTask 
	END
    CREATE CLUSTERED INDEX IX_DBTimeLineAlwaysExecutingTask_Clustered ON DBTimeLine.AlwaysExecutingTask (Executed, ID)
END
"

        Return ret
    End Function
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

    Public Overridable Function GetSqlCreateSystemRevisionTable() As String Implements IDBSqlGenerator.GetSqlCreateSystemRevisionTable
        Dim ret As String = "
IF OBJECT_ID('DBTimeLine.Revision') IS NULL
BEGIN
	CREATE TABLE [DBTimeLine].[Revision]
	(
		[ID] [uniqueidentifier] NOT NULL PRIMARY KEY NONCLUSTERED,
        [RevisionKey] [varchar](800) NOT NULL,
		[Created] [date] NOT NULL,
		[Granulation] [int] NOT NULL,
        [ObjectType] [varchar](50) NOT NULL,        
        [RevisionType] [varchar](50) NOT NULL,
        [ModuleKey] [varchar](50),
        [SchemaName] [varchar](50),
        [SchemaObjectName] [varchar](150),
        [ObjectName] [varchar](150) NOT NULL,
		[Executed] [datetime] NOT NULL CONSTRAINT DF_DBTimeLine_Revision_Executed DEFAULT GETDATE(),
        [ObjectFullName] [varchar](100) NOT NULL,
        [Description] [nvarchar](MAX) NULL
	)
	IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name='IX_DBTimeLineRevision_Clustered' AND object_id = OBJECT_ID('DBTimeLine.Revision'))
	BEGIN
		DROP INDEX IX_DBTimeLineRevision_Clustered ON DBTimeLine.Revision 
	END
    CREATE CLUSTERED INDEX IX_DBTimeLineRevision_Clustered ON DBTimeLine.Revision (RevisionKey, ID)
END
"

        Return ret
    End Function

    Public Overridable Function GetSqlCheckIfSystemSchemaExists() As String Implements IDBSqlGenerator.GetSqlCheckIfSchemaExists
        Dim ret As String = "SELECT TOP 1 1 FROM sys.schemas WHERE name = 'DBTimeLine'"

        Return ret
    End Function
End Class
