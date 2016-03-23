Option Strict On
Imports Framework.DBCreator.DBObjects
Imports Framework.DBCreator

Public Class DBO
    Inherits DBModule

    Public Overrides ReadOnly Property ModuleKey As String
        Get
            Return "dbo"
        End Get
    End Property

    Private Function preTask(sender As IDBRevision) As String
        Return ""
    End Function

    Private Function postTask(sender As IDBRevision) As String
        Dim sql As String = "
GO 
SELECT Stupac = 'Ovo je post sql: bok kaj ima'
"

        Return sql
    End Function
    Public Overrides Sub CreateTimeLine()

        Dim rev As New DBRevision(DateSerial(2016, 3, 15), 0, eDBRevisionType.Create)
        With AddSchema("Customization", New DBSchemaDescriptor)
            .AddRevision(New DBRevision(rev) With {.PreSqlTask = AddressOf preTask})

            With .AddTable("Test1", New DBTableDescriptor() With {.CreatorFieldName = "ID", .CreatorFieldDescriptor = New myfieldDesc With {.FieldType = eDBFieldType.Guid, .NekiNoviKojegNemaUGetDescriptoru = "-- bok 1" & vbNewLine}},
                    New DBRevision(rev))

                Dim fld As myField = CType(.AddField("Naziv", New myfieldDesc With {.FieldType = eDBFieldType.Nvarchar, .Size = 50, .NekiNoviKojegNemaUGetDescriptoru = "-- bok kaj ima" & vbNewLine},
                          New DBRevision(rev) With {.PostSqlTask = AddressOf postTask}), myField)
                With fld
                    .AddRevision(New DBRevision(DateSerial(2016, 3, 19), 0, eDBRevisionType.Modify),
                                 New myfieldDesc(CType(.Descriptor, myfieldDesc)) With {.FieldType = eDBFieldType.Nvarchar, .Size = 150})
                End With
            End With
        End With

        'With AddSchema("dbo", New DBSchemaDescriptor())
        '    Dim dt As IDBTable
        '    dt = .AddTable("Table1", New DBTableDescriptor() With {.CreatorFieldName = "ID", .CreatorFieldDescriptor = New DBFieldDescriptor() With {.FieldType = eFieldType.Guid}},
        '                   New DBRevision(rev))
        '    With dt
        '        With dt.DBObjects(CType(.Descriptor, IDBTableDescriptor).CreatorFieldName)
        '            .AddRevision(New DBRevision(DateSerial(2016, 3, 16), 1, eDBRevisionType.Modify),
        '                  New DBFieldDescriptor(CType(.Descriptor, DBFieldDescriptor)) With {.FieldType = eFieldType.Nvarchar, .Size = 50})

        '        End With
        '        With .AddField("DatumOd", New DBFieldDescriptor With {.FieldType = eFieldType.Datetime, .Nullable = True})
        '            .AddRevision(New DBRevision(DateSerial(2016, 3, 18), 0, eDBRevisionType.Create))
        '        End With
        '    End With
        'End With

    End Sub

End Class


Public Class CorePlace
    Inherits DBModule

    Public Overrides ReadOnly Property ModuleKey As String
        Get
            Return "CorePlace"
        End Get
    End Property

    Public Overrides Sub CreateTimeLine()

        Dim rev As New DBRevision(DateSerial(2016, 3, 10), 0, eDBRevisionType.Create)

        With AddSchema("Place", New DBSchemaDescriptor(), New DBRevision(rev))
            With .AddTable("Table1", New DBTableDescriptor() With {.CreatorFieldName = "ID", .CreatorFieldDescriptor = New DBFieldDescriptor() With {.FieldType = eDBFieldType.Guid}},
                           New DBRevision(rev))

                With .AddField("DatumOd", New DBFieldDescriptor With {.FieldType = eDBFieldType.Nvarchar, .Size = -1, .Nullable = True},
                                 New DBRevision(DateSerial(2016, 3, 18), 0, eDBRevisionType.Create))

                    .AddRevision(New DBRevision(DateSerial(2016, 3, 23), 0, eDBRevisionType.Modify),
                                 New DBFieldDescriptor(.Descriptor) With {.FieldType = eDBFieldType.Guid})
                End With

                With .AddField("DatumOd", New DBFieldDescriptor With {.FieldType = eDBFieldType.Datetime, .Nullable = True})
                    .AddRevision(New DBRevision(DateSerial(2016, 3, 23), 0, eDBRevisionType.Delete))
                End With

                '.AddRevision(New DBRevision(DateSerial(2016, 3, 23), 0, eDBRevisionType.Delete))
            End With
        End With

    End Sub

End Class



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
