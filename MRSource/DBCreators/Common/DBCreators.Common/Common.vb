Imports MRFramework.MRDBCreator

Public Class DBO
    Inherits DBModule

    Public Overrides Sub CreateTimeLine()

        Dim rev As New DBRevision(DateSerial(2016, 3, 15), 0, eDBRevisionType.Create)

        With AddSchema("dbo", New DBSchemaDescriptor())

            With .AddTable("Table1", New DBTableDescriptor() With {.CreatorFieldName = "ID", .CreatorFieldDescriptor = New DBFieldDescriptor() With {.FieldType = eFieldType.Guid}},
                           New DBRevision(rev))

                With .DBFields(.CreatorFieldName)
                    .AddRevision(New DBRevision(DateSerial(2016, 3, 16), 1, eDBRevisionType.Modify),
                          New DBFieldDescriptor(.GetDescriptor()) With {.FieldType = eFieldType.Nvarchar, .Size = 50})

                End With
                With .AddField("DatumOd", New DBFieldDescriptor With {.FieldType = eFieldType.Datetime, .Nullable = True})
                    .AddRevision(New DBRevision(DateSerial(2016, 3, 18), 0, eDBRevisionType.Create))
                End With
            End With
        End With

    End Sub

End Class


'Public Class Common
'    Inherits DBModule

'    Public Overrides Sub CreateTimeLine()

'        Dim rev As New DBRevision(DateSerial(2016, 3, 10), 0, eDBRevisionType.Create)

'        With AddSchema("Common", Nothing,
'                          New DBRevision(rev))

'            With .AddTable("Table1", Nothing,
'                           New DBRevision(rev))

'                With .AddField("ID", New DBFieldDescriptor() With {.FieldType = eFieldType.Guid},
'                          New DBRevision(rev))

'                End With
'            End With
'        End With

'    End Sub

'End Class



'Public Class Common2
'    Inherits DBModule

'    Public Overrides Sub CreateTimeLine()

'        Dim rev As New DBRevision(DateSerial(2016, 3, 13), 0, eDBRevisionType.Create)

'        With AddSchema("Common", Nothing)
'            With .AddTable("Table1", Nothing)
'                With .AddField("ID", New DBFieldDescriptor())
'                    .AddRevision(New DBRevision(DateSerial(2016, 3, 15), 0, eDBRevisionType.Modify),
'                          New DBFieldDescriptor(.GetDescriptor()) With {.FieldType = eFieldType.Nvarchar, .Size = 250})
'                End With
'            End With
'        End With

'    End Sub

'End Class
