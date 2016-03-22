Public NotInheritable Class Helpers
    Private Sub New()

    End Sub

    Public Shared Function AddDBObjectToParent(parent As IDBParent, objectName As String, descriptor As IDBObjectDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBObject
        If Not parent.DBObjects.ContainsKey(objectName) Then
            Dim newDBObject As IDBObject = descriptor.GetDBObjectInstance(parent)
            With newDBObject
                .Name = objectName
            End With
            parent.DBObjects.Add(objectName, newDBObject)
        End If
        Dim dbObject As IDBObject = parent.DBObjects(objectName)

        If createRevision IsNot Nothing Then
            dbObject.AddRevision(createRevision)
        End If

        Return dbObject
    End Function

End Class
