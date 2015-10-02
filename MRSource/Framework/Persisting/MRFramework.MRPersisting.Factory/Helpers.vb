Public Module Helpers

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
End Module

