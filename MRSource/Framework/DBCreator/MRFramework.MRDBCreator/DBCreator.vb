Imports MRFramework.MRPersisting.Factory

Public Class DBCreator
    Implements IDBChained

    Public ReadOnly Property DBModules As New List(Of DBModule)

    Public ReadOnly Property AllDBSqlRevisions As New List(Of DBSqlRevision)
    Public ReadOnly Property ExecutedDBSqlRevisions As New List(Of DBSqlRevision)

    Public Property Parent As IDBChained Implements IDBChained.Parent

    Public Function AddModule(dBModule As DBModule) As DBModule
        DBModules.Add(dBModule)
        dBModule.Parent = Me

        Return dBModule
    End Function

    Public Function GetModules() As List(Of DBModule)
        Dim ret As New List(Of DBModule)

        ret.AddRange(DBModules)

        Return ret
    End Function

    Public Sub LoadExecutedDBSqlRevisionsFromDB(cnn As Common.DbConnection, Optional trn As Common.DbTransaction = Nothing)
        Using per As New DBSqlRevision.DBSqlRevisionPersister With {.CNN = cnn, .PagingEnabled = False}
            With per.OrderItems
                .Add(New MRCore.MROrderItem("Created", MRCore.Enums.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("Granulation", MRCore.Enums.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("DBObjectType", MRCore.Enums.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("DBRevisionType", MRCore.Enums.Enums.eOrderDirection.Ascending))
                .Add(New MRCore.MROrderItem("DBObjectFullName", MRCore.Enums.Enums.eOrderDirection.Ascending))
            End With

            ' TODO - per.GetData generira bezvezan query, treba smisliti nesto drugo, stignem
            ' ovako:
            ' SELECT Case ID, Created, Granulation, DBObjectType, DBRevisionType, DBObjectFullName, DBObjectName, SchemaName FROM Common.Revision
            ' ORDER BY  Created ASC, Granulation ASC, DBObjectType ASC, DBRevisionType ASC, DBObjectFullName ASC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY
            Dim dicExecutedRevisions As Dictionary(Of Object, MRPersisting.Core.IMRDLO) = per.GetData(trn)
            For Each kv As KeyValuePair(Of Object, MRPersisting.Core.IMRDLO) In dicExecutedRevisions
                Dim sqlRevision As New DBSqlRevision(kv.Value)
                ExecutedDBSqlRevisions.Add(sqlRevision)
            Next
        End Using
    End Sub

#Region "System objects"

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
        [DBObjectName] [nvarchar](250) NOT NULL,
		[DBObjectType] [nvarchar](250) NOT NULL,
		[DBRevisionType] [nvarchar](250) NOT NULL,
        [SchemaName] [nvarchar](250),
        [Description] [nvarchar](max) NULL,
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

#End Region

End Class
