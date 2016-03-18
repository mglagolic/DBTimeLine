Imports DBCreators.Common
Imports MRFramework.MRDBCreator


Public Class DBO
    Inherits DBModule

    Public Class myfieldDesc
        Inherits DBFieldDescriptor

        Public Overrides Function GetSqlCreate(dBObject As IDBObject) As String
            Return MyBase.GetSqlCreate(dBObject)
        End Function

        ' TODO - overridati DBField GetDescriptor funkciju ili odustati od customizacije
        ' TODO - bit ce mozda lakse ako sve classe preselim u posebni assembly i radim iskljucivo interfaceima
        ' TODO - generator koda za vise baza odjednom, nadje identicne stvari, nadje razlicitosti i podijeli to po fajlovima
    End Class

    Public Overrides Sub CreateTimeLine()

        Dim rev As New DBRevision(DateSerial(2016, 3, 15), 0, eDBRevisionType.Create)

        With AddSchema("dbo", New DBSchemaDescriptor())

            'TODO - omoguciti custom descriptore, custom code generatore, code generator factory (ovisno o bazi, verziji baze itd.)
            'TODO - ovdje exposati samo interface, osim ako nije nuzno (dbCreator mozda ne treba biti interface)
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
