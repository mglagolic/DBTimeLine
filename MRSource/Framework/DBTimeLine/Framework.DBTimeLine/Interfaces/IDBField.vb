Public Interface IDBField
    Inherits IDBObject

    Function GetFieldTypeSql(dBType As eDBType) As String

End Interface