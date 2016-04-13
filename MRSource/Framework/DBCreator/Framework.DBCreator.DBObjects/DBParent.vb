Public Class DBParent
    Implements IDBParent

    Public ReadOnly Property DBObjects As New Dictionary(Of String, IDBObject) Implements IDBParent.DBObjects

    Public Overridable Function AddDBObject(objectName As String, descriptor As IDBObjectDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBObject Implements IDBParent.AddDBObject
        If Not DBObjects.ContainsKey(objectName) Then
            Dim newDBObject As IDBObject = descriptor.GetDBObjectInstance(Me)
            newDBObject.Name = objectName
            DBObjects.Add(objectName, newDBObject)
        End If
        Dim dbObject As IDBObject = DBObjects(objectName)
        dbObject.Descriptor = descriptor

        If createRevision IsNot Nothing Then
            dbObject.AddRevision(createRevision)
        End If

        Return dbObject
    End Function
End Class
