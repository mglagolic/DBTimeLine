Imports System.Data.Common

Public Module PersistingFactoryHelpers

    Public Function GetPrimaryKeyFromDB(dataBaseTableName As String) As List(Of DataColumn)
        Dim ret As List(Of DataColumn) = Nothing

        Using cnn As DbConnection = MRC.GetConnection()
            Using da As DbDataAdapter = MRC.GetDataAdapter()
                Using cmd As DbCommand = MRC.GetCommand(cnn)
                    cmd.CommandText = String.Format("SELECT * FROM {0}", dataBaseTableName)
                    da.SelectCommand = cmd
                    Try
                        cnn.Open()
                        Using table As New DataTable(dataBaseTableName)
                            ret = da.FillSchema(table, SchemaType.Mapped).PrimaryKey.ToList()
                        End Using
                    Catch ex As System.Exception
                        Throw New System.Exception("Error connecting to database.", ex)
                    End Try
                End Using
            End Using
        End Using

        Return ret
    End Function

    Public Function GetSchema(sql As String) As DataTable
        Dim ret As DataTable = Nothing

        Using lookupCnn As DbConnection = MRC.GetConnection()
            Using cmd As DbCommand = MRC.GetCommand(lookupCnn)
                cmd.CommandText = sql
                Try
                    lookupCnn.Open()
                    Using reader As DbDataReader = cmd.ExecuteReader(CommandBehavior.Default Or CommandBehavior.KeyInfo Or CommandBehavior.SchemaOnly)
                        ret = reader.GetSchemaTable()
                        If Not reader.IsClosed Then
                            reader.Close()
                        End If
                    End Using
                Catch ex As Exception
                    Throw New Exception("Error connection to database.", ex)
                End Try
            End Using
        End Using

        Return ret
    End Function

    Public Function GetNewGuid() As Guid
        Dim ret As Guid = Nothing
        Using cnn As IDbConnection = MRC.GetConnection
            Using cmd As IDbCommand = MRC.GetCommand
                Try
                    cmd.CommandText = <string>select newID()</string>.Value
                    cmd.Connection = cnn
                    If cnn.State <> ConnectionState.Open Then
                        cnn.Open()
                    End If
                    ret = CType(cmd.ExecuteScalar, Guid)
                Catch ex As Exception
                    If Debugger.IsAttached Then
                        Debugger.Break()
                    End If
                    Throw
                End Try
            End Using
        End Using

        Return ret
    End Function

    Public Function SqlObjectExists(objectName As String) As Boolean

        Dim ret As Boolean = False
        Using cnn As IDbConnection = MRC.GetConnection
            Using cmd As IDbCommand = MRC.GetCommand
                Try
                    cmd.CommandText = <string>select object_id('<%= objectName %>')</string>.Value
                    cmd.Connection = cnn
                    If cnn.State <> ConnectionState.Open Then
                        cnn.Open()
                    End If
                    Dim o As Object = cmd.ExecuteScalar
                    If Not Object.Equals(o, DBNull.Value) Then
                        ret = True
                    End If

                Catch ex As Exception
                    If Debugger.IsAttached Then
                        Debugger.Break()
                    End If
                    Throw
                End Try
            End Using
        End Using

        Return ret
    End Function
End Module

