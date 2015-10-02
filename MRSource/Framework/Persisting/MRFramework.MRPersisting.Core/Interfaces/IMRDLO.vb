Public Interface IMRDLO
    Inherits ICloneable

    'Property WritableColumns As List(Of String)
    'Property ColumnValues As MRRowColumnValues
    Property ColumnValues As Dictionary(Of String, Object)


End Interface
