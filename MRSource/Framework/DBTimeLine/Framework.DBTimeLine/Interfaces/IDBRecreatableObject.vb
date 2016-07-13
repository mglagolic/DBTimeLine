Public Interface IDBRecreatableObject
    Inherits IDBObject

    Function GetSqlRecreate(dBType As eDBType) As String
End Interface
