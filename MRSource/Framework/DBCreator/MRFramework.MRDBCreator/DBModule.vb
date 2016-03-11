Imports MRFramework.MRPersisting.Factory

Public MustInherit Class DBModule
    Implements IDBModule

    Private ReadOnly Property _DBSchemas As New List(Of DBSchema)
    Public ReadOnly Property DBSchemas As List(Of DBSchema) Implements IDBModule.DBSchemas
        Get
            Return _DBSchemas
        End Get
    End Property

    Protected Function AddSchema(schema As DBSchema, createRevision As DBRevision) As DBSchema
        Me.DBSchemas.Add(schema)
        schema.AddRevision(createRevision)

        Return schema
    End Function

    MustOverride Sub Create()

    Public Function DBCreate() As Object Implements IDBModule.DBCreate
        CreateSystemObjects()

        Create()



        Return Nothing
    End Function

    Private Sub CreateSystemObjects()
        CreateRevisionTable()
    End Sub

    Private Sub CreateRevisionTable()
        Using cnn As IDbConnection = MRC.GetConnection
            Using cmd As IDbCommand = MRC.GetCommand
                Try
                    cmd.CommandText =
<string>
IF OBJECT_ID('Common.Revision') IS NULL
BEGIN
	CREATE TABLE [Common].[Revision]
	(
		[ID] [uniqueidentifier] NOT NULL,
		[Created] [date] NOT NULL,
		[Granulation] [int] NOT NULL,
		[DBObjectFullName] [nvarchar](250) NOT NULL,
		[DBObjectType] [nvarchar](250) NOT NULL,
		[DBObjectRevisionType] [nvarchar](250) NOT NULL,
		CONSTRAINT [PK_Revision] PRIMARY KEY CLUSTERED 
		(
			[ID] ASC
		)
	)
END
</string>.Value
                    cmd.Connection = cnn
                    If cnn.State <> ConnectionState.Open Then
                        cnn.Open()
                    End If
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    If Debugger.IsAttached Then
                        Debugger.Break()
                    End If
                    Throw
                End Try
            End Using
        End Using
    End Sub

    'MustOverride Function CreateInMemoryRevisions() As List(Of DBSqlRevision) Implements IDBModule.CreateInMemoryRevisions
    'MustOverride Function LoadAlreadyExecutedRevisions() As List(Of DBSqlRevision) Implements IDBModule.LoadAlreadyExecutedRevisions
    'MustOverride Function GetNewRevisions() As List(Of DBSqlRevision) Implements IDBModule.GetNewRevisions

End Class
