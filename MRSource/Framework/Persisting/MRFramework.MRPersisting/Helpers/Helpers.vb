Imports MRFramework.MRPersisting.Core
Imports MRFramework.MRPersisting.Cache
Imports System.Data.Common

Namespace Helpers
    Public Module Helpers

#Region "Cache"


        Friend Function GetCacheValueSchemaTable(dbo As MRPersister) As BaseQuerySchemaCache.CacheValue
            If Not BaseQuerySchemaCache.ContainsKey(dbo.GetType) Then
                BaseQuerySchemaCache.Push(dbo.GetType, New BaseQuerySchemaCache.CacheValue(dbo.GetSchema(dbo.SQLBase)))
            End If
            Return BaseQuerySchemaCache.Pop(dbo.GetType)
        End Function

        Private Function GetIdentityColumnName(schemaTable As DataTable) As String
            Dim ret As String = Nothing

            Dim drowsIdentity() As DataRow = schemaTable.Select("IsIdentity= 1")
            If drowsIdentity IsNot Nothing AndAlso drowsIdentity.Count > 0 Then
                If drowsIdentity.Count > 1 Then
                    Throw New Exception("There is only one identity column allowed in base table schema.")
                End If
                ret = drowsIdentity(0)("columnname").ToString
            End If
            Return ret
        End Function


        Friend Function GetOriginalPkParameter(dbo As MRPersister, pars As DbParameterCollection) As DbParameter
            Dim ret As DbParameter = Nothing
            For Each p As DbParameter In pars
                If p.ParameterName.Equals("@Original_" & dbo.PrimaryKey(0).ColumnName) Then
                    ret = p
                    Exit For
                End If
            Next
            Return ret
        End Function

        Friend Function GetInsertCommandCachedValue(dbo As MRPersister, schemaTable As DataTable) As InsertCommandCache.CacheValue
            If Not InsertCommandCache.ContainsKey(dbo.GetType) Then
                Dim tmpCmd As IDbCommand = dbo.GetCommand(MRPersister.eCommandType.Insert)
                Dim pars(tmpCmd.Parameters.Count - 1) As DbParameter
                tmpCmd.Parameters.CopyTo(pars, 0)
                Dim drowsIdentity() As DataRow = schemaTable.Select("IsIdentity= 1")
                InsertCommandCache.Push(dbo.GetType, New InsertCommandCache.CacheValue(tmpCmd.CommandText, pars, GetIdentityColumnName(schemaTable)))
            End If
            Return InsertCommandCache.Pop(dbo.GetType)
        End Function

        Friend Function GetUpdateCommandCachedValue(dbo As MRPersister, schemaTable As DataTable) As UpdateCommandCache.CacheValue
            If Not UpdateCommandCache.ContainsKey(dbo.GetType) Then
                Dim tmpCmd As IDbCommand = dbo.GetCommand(MRPersister.eCommandType.Update)
                Dim lsPars As New List(Of DbParameter)
                For Each p As DbParameter In tmpCmd.Parameters
                    If Not ((p.ParameterName.StartsWith("@Original_") OrElse p.ParameterName.StartsWith("@IsNull_")) AndAlso Not p.ParameterName.Equals("@Original_" & dbo.PrimaryKey(0).ColumnName) AndAlso Not p.ParameterName.Equals("@" & dbo.PrimaryKey(0).ColumnName) AndAlso Not p.ParameterName.Equals("@Original_" & dbo.SYS_GUID)) Then
                        lsPars.Add(p)
                    End If
                Next
                Dim tmpCmd_LastWins As IDbCommand = dbo.GetCommand(MRPersister.eCommandType.UpdateLastWins)
                Dim lsPars_LastWins As New List(Of DbParameter)
                For Each p As DbParameter In tmpCmd_LastWins.Parameters
                    If Not ((p.ParameterName.StartsWith("@Original_") OrElse p.ParameterName.StartsWith("@IsNull_")) AndAlso Not p.ParameterName.Equals("@" & dbo.PrimaryKey(0).ColumnName) AndAlso Not p.ParameterName.Equals("@Original_" & dbo.PrimaryKey(0).ColumnName)) Then
                        lsPars_LastWins.Add(p)
                    End If
                Next
                UpdateCommandCache.Push(dbo.GetType, New UpdateCommandCache.CacheValue(tmpCmd.CommandText, lsPars.ToArray, tmpCmd_LastWins.CommandText, lsPars_LastWins.ToArray))
            End If
            Return UpdateCommandCache.Pop(dbo.GetType)
        End Function

        Friend Function GetDeleteCommandCachedValue(dbo As MRPersister, schemaTable As DataTable) As DeleteCommandCache.CacheValue
            If Not DeleteCommandCache.ContainsKey(dbo.GetType) Then
                Dim tmpCmd As IDbCommand = dbo.GetCommand(MRPersister.eCommandType.Delete)
                Dim lsPars As New List(Of DbParameter)
                For Each p As DbParameter In tmpCmd.Parameters
                    If Not ((p.ParameterName.StartsWith("@Original_") OrElse p.ParameterName.StartsWith("@IsNull_")) AndAlso Not p.ParameterName.Equals("@Original_" & dbo.PrimaryKey(0).ColumnName) AndAlso Not p.ParameterName.Equals("@" & dbo.PrimaryKey(0).ColumnName) AndAlso Not p.ParameterName.Equals("@Original_" & dbo.SYS_GUID)) Then
                        lsPars.Add(p)
                    End If
                Next
                Dim tmpCmd_LastWins As IDbCommand = dbo.GetCommand(MRPersister.eCommandType.DeleteLastWins)
                Dim lsPars_LastWins As New List(Of DbParameter)
                For Each p As DbParameter In tmpCmd_LastWins.Parameters
                    If Not ((p.ParameterName.StartsWith("@Original_") OrElse p.ParameterName.StartsWith("@IsNull_")) AndAlso Not p.ParameterName.Equals("@" & dbo.PrimaryKey(0).ColumnName) AndAlso Not p.ParameterName.Equals("@Original_" & dbo.PrimaryKey(0).ColumnName)) Then
                        lsPars_LastWins.Add(p)
                    End If
                Next
                DeleteCommandCache.Push(dbo.GetType, New DeleteCommandCache.CacheValue(tmpCmd.CommandText, lsPars.ToArray, tmpCmd_LastWins.CommandText, lsPars_LastWins.ToArray))
            End If
            Return DeleteCommandCache.Pop(dbo.GetType)
        End Function

#End Region

        Friend Sub InsertDloIntoCommand(dlo As IMRDLO, cachedParameters() As DbParameter, ByRef cmd As DbCommand)
            'Friend Sub InsertDloIntoCommand(dlo As IMRDLO, cachedParameters() As DbParameter, ByRef cmd As DbCommand, Optional sourceVersion As DataRowVersion = DataRowVersion.Current)
            Using typeConverter As New MRTypeConverter
                For i As Integer = 0 To cachedParameters.Length - 1
                    'If cachedParameters(i).SourceVersion = sourceVersion Then
                    Dim input As New MRDbParameterCopierInput
                    input.Parameter = cachedParameters(i)
                    input.Command = cmd
                    Dim output As MRDbParameterCopierOutput = CType(MRCopierFactory.GetCopier(Of DbParameter).Copy(input), MRDbParameterCopierOutput)
                    If dlo.ColumnValues.ContainsKey(output.Parameter.SourceColumn) Then
                        output.Parameter.Value = typeConverter.GetValue(dlo.ColumnValues(output.Parameter.SourceColumn))
                    Else
                        output.Parameter.Value = DBNull.Value
                    End If

                    cmd.Parameters.Add(output.Parameter)
                    'End If
                Next
            End Using
        End Sub
    End Module

End Namespace
