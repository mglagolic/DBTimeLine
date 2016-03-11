Imports MRFramework.MRDBCreator
Public Class Common
    Inherits DBModule

    Public Overrides Sub Create()

        Dim rev As New DBRevision(DateSerial(2016, 3, 10), 0, eDBRevisionType.Create)

        With AddSchema(New DBSchema("Common"),
                          New DBRevision(rev))

            With .AddTable(New DBTable("Revision"),
                           New DBRevision(rev))


                With .AddField(New DBField("ID", eFieldType.Guid),
                          New DBRevision(rev))

                    .AddRevision(New DBRevision(DateSerial(2016, 3, 11), 1, eDBRevisionType.Modify,
                                 New DBField(.Name, eFieldType.Nvarchar, 50, 0)))

                    .AddRevision(New DBRevision(DateSerial(2016, 3, 11), 2, eDBRevisionType.Modify,
                                 New DBField(.Name, eFieldType.Guid)))
                End With

                .AddField(New DBField("Created", eFieldType.Datetime),
                          New DBRevision(rev))

                .AddField(New DBField("Granulation", eFieldType.Integer),
                          New DBRevision(rev))

                .AddField(New DBField("DBObjectFullName", eFieldType.Nvarchar, 250, 0),
                          New DBRevision(rev))

                .AddField(New DBField("DBObjectType", eFieldType.Nvarchar, 250, 0),
                          New DBRevision(rev))

                .AddField(New DBField("DBRevisionType", eFieldType.Nvarchar, 250, 0),
                          New DBRevision(rev))


            End With
        End With

    End Sub

End Class
