Imports MRFramework.MRPersisting.Factory

Public Module GlobalStatics

    Public Property AllDBSqlRevisions As New List(Of DBSqlRevision)

    Public Property ExecutedDBSqlRevisions As New List(Of DBSqlRevision)

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

    Public Sub CreateSystemObjects()
        CreateRevisionTable()
    End Sub
End Module
