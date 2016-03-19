Imports Framework.DBCreator
Imports Framework.DBCreator.DBObjects

Public Class DBO
    Inherits DBModule

    Public Overrides Sub CreateTimeLine()

        Dim rev As New DBRevision(DateSerial(2016, 3, 15), 0, eDBRevisionType.Create)
        With AddSchema("Customization", New DBSchemaDescriptor)
            .AddRevision(New DBRevision(rev))

            With .AddTable("Test1", New DBTableDescriptor() With {.CreatorFieldName = "ID", .CreatorFieldDescriptor = New myfieldDesc With {.FieldType = eFieldType.Guid, .NekiNoviKojegNemaUGetDescriptoru = "-- bok 1" & vbNewLine}},
                    New DBRevision(rev))

                Dim fld As myField = .AddField("Naziv", New myfieldDesc With {.FieldType = eFieldType.Nvarchar, .Size = 50, .NekiNoviKojegNemaUGetDescriptoru = "-- bok kaj ima" & vbNewLine},
                          New DBRevision(rev))
                With fld
                    .AddRevision(New DBRevision(DateSerial(2016, 3, 19), 0, eDBRevisionType.Modify),
                                 New myfieldDesc(.GetDescriptor()) With {.FieldType = eFieldType.Nvarchar, .Size = 150})
                End With
            End With
        End With

        With AddSchema("dbo", New DBSchemaDescriptor())
            With .AddTable("Table1", New DBTableDescriptor() With {.CreatorFieldName = "ID", .CreatorFieldDescriptor = New DBFieldDescriptor() With {.FieldType = eFieldType.Guid}},
                           New DBRevision(rev))

                With .DBObjects(.CreatorFieldName)
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
