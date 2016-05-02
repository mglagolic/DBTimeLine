Public Class DBSqlGenerator
    Implements IDBSqlGenerator

    Public Property DBViewGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBViewGenerator
    Public Property DBFieldGenerator As IDBFieldGenerator Implements IDBSqlGenerator.DBFieldGenerator
    Public Property DBSchemaGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBSchemaGenerator
    Public Property DBTableGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBTableGenerator
    Public Property DBPrimaryKeyConstraintGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBPrimaryKeyConstraintGenerator
    Public Property DBForeignKeyConstraintGenerator As IDBObjectGenerator Implements IDBSqlGenerator.DBForeignKeyConstraintGenerator
    Public Property DBIndexGenerator As IDBIndexGenerator Implements IDBSqlGenerator.DBIndexGenerator

    Public Sub New()
        DBViewGenerator = New DBViewGenerator
        DBFieldGenerator = New DBFieldGenerator
        DBSchemaGenerator = New DBSchemaGenerator
        DBTableGenerator = New DBTableGenerator With {.Parent = Me}
        DBPrimaryKeyConstraintGenerator = New DBPrimaryKeyConstraintGenerator
        DBForeignKeyConstraintGenerator = New DBForeignKeyConstraintGenerator
        DBIndexGenerator = New DBIndexGenerator
    End Sub

    Public Overridable Function GetSqlCreate(dbObject As IDBObject) As String Implements IDBSqlGenerator.GetSqlCreate
        Dim ret As String = ""

        If TypeOf dbObject Is IDBSchema Then
            ret = DBSchemaGenerator.GetSqlCreate(dbObject)

        ElseIf TypeOf dbObject Is IDBTable Then
            ret = DBTableGenerator.GetSqlCreate(dbObject)

        ElseIf TypeOf dbObject Is IDBField Then
            ret = DBFieldGenerator.GetSqlCreate(dbObject)

        ElseIf TypeOf dbObject Is IDBPrimaryKeyConstraint Then
            ret = DBPrimaryKeyConstraintGenerator.GetSqlCreate(dbObject)

        ElseIf TypeOf dbObject Is IDBForeignKeyConstraint Then
            ret = DBForeignKeyConstraintGenerator.GetSqlCreate(dbObject)

        ElseIf TypeOf dbObject Is IDBView Then
            ret = DBViewGenerator.GetSqlCreate(dbObject)

        ElseIf TypeOf dbObject Is IDBIndex Then
            ret = DBIndexGenerator.GetSqlCreate(dbObject)

        End If

        Return ret
    End Function

    Public Overridable Function GetSqlModify(dbObject As IDBObject) As String Implements IDBSqlGenerator.GetSqlModify
        Dim ret As String = ""

        If TypeOf dbObject Is IDBSchema Then
            ret = DBSchemaGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBTable Then
            ret = DBTableGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBField Then
            ret = DBFieldGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBPrimaryKeyConstraint Then
            ret = DBPrimaryKeyConstraintGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBForeignKeyConstraint Then
            ret = DBForeignKeyConstraintGenerator.GetSqlModify(dbObject)

        ElseIf TypeOf dbObject Is IDBView Then
            ret = DBViewGenerator.GetSqlModify(CType(dbObject, IDBView))

        ElseIf TypeOf dbObject Is IDBIndex Then
            ret = DBIndexGenerator.GetSqlModify(dbObject)

        End If

        Return ret
    End Function

    Public Overridable Function GetSqlDelete(dbObject As IDBObject) As String Implements IDBSqlGenerator.GetSqlDelete
        Dim ret As String = ""

        If TypeOf dbObject Is IDBSchema Then
            ret = DBSchemaGenerator.GetSqlDelete(dbObject)

        ElseIf TypeOf dbObject Is IDBTable Then
            ret = DBTableGenerator.GetSqlDelete(dbObject)

        ElseIf TypeOf dbObject Is IDBField Then
            ret = DBFieldGenerator.GetSqlDelete(dbObject)

        ElseIf TypeOf dbObject Is IDBPrimaryKeyConstraint Then
            ret = DBPrimaryKeyConstraintGenerator.GetSqlDelete(dbObject)

        ElseIf TypeOf dbObject Is IDBView Then
            ret = DBViewGenerator.GetSqlDelete(CType(dbObject, IDBView))

        ElseIf TypeOf dbObject Is IDBForeignKeyConstraint Then
            ret = DBForeignKeyConstraintGenerator.GetSqlDelete(CType(dbObject, IDBForeignKeyConstraint))

        ElseIf TypeOf dbObject Is IDBIndex Then
            ret = DBIndexGenerator.GetSqlDelete(dbObject)

        End If

        Return ret
    End Function

    Public Overridable Function GetSqlCreateSystemAlwaysExecutingTaskTable() As String Implements IDBSqlGenerator.GetSqlCreateSystemAlwaysExecutingTaskTable
        Dim ret As String = "
IF OBJECT_ID('DBCreator.AlwaysExecutingTask') IS NULL
BEGIN
	CREATE TABLE [DBCreator].[AlwaysExecutingTask]
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
		[Executed] [datetime] NOT NULL CONSTRAINT DF_DBCreator_AlwaysExecutingTask_Executed DEFAULT GETDATE(),
        [ObjectFullName] [varchar](100) NOT NULL,
        [Description] [nvarchar](MAX) NULL
	)
	IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name='IX_DBCreatorAlwaysExecutingTask_Clustered' AND object_id = OBJECT_ID('DBCreator.AlwaysExecutingTask'))
	BEGIN
		DROP INDEX IX_DBCreatorAlwaysExecutingTask_Clustered ON DBCreator.AlwaysExecutingTask 
	END
    CREATE CLUSTERED INDEX IX_DBCreatorAlwaysExecutingTask_Clustered ON DBCreator.AlwaysExecutingTask (Executed, ID)
END
"

        Return ret
    End Function
    Public Overridable Function GetSqlCreateSystemModuleTable() As String Implements IDBSqlGenerator.GetSqlCreateSystemModuleTable
        Dim ret As String = "
IF OBJECT_ID('DBCreator.Module') IS NULL
BEGIN
	CREATE  TABLE [DBCreator].[Module]
	(
        [ModuleKey] [varchar](50) NOT NULL PRIMARY KEY,
        [Name] [nvarchar](50) NOT NULL,
        [Created] [datetime] NOT NULL,
        [Active] bit NOT NULL,
        [Description] [nvarchar](MAX) NULL
	)
END
"
        Return ret
    End Function

    Public Overridable Function GetSqlCreateSystemSchema() As String Implements IDBSqlGenerator.GetSqlCreateSystemSchema
        Dim ret As String = "CREATE SCHEMA DBCreator"

        Return ret
    End Function

    Public Overridable Function GetSqlCreateSystemRevisionTable() As String Implements IDBSqlGenerator.GetSqlCreateSystemRevisionTable
        Dim ret As String = "
IF OBJECT_ID('DBCreator.Revision') IS NULL
BEGIN
	CREATE TABLE [DBCreator].[Revision]
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
		[Executed] [datetime] NOT NULL CONSTRAINT DF_DBCreator_Revision_Executed DEFAULT GETDATE(),
        [ObjectFullName] [varchar](100) NOT NULL,
        [Description] [nvarchar](MAX) NULL
	)
	IF EXISTS(SELECT TOP 1 1 FROM sys.indexes WHERE name='IX_DBCreatorRevision_Clustered' AND object_id = OBJECT_ID('DBCreator.Revision'))
	BEGIN
		DROP INDEX IX_DBCreatorRevision_Clustered ON DBCreator.Revision 
	END
    CREATE CLUSTERED INDEX IX_DBCreatorRevision_Clustered ON DBCreator.Revision (RevisionKey, ID)
END
"

        Return ret
    End Function

    Public Overridable Function GetSqlCheckIfSystemSchemaExists() As String Implements IDBSqlGenerator.GetSqlCheckIfSchemaExists
        Dim ret As String = "SELECT TOP 1 1 FROM sys.schemas WHERE name = 'DBCreator'"

        Return ret
    End Function
End Class
