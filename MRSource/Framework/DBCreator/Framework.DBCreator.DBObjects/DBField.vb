﻿Public Class DBField
    Inherits DBObject
    Implements IDBField

    Public Sub New()

    End Sub

    Public Overrides ReadOnly Property ObjectType As eDBObjectType
        Get
            Return eDBObjectType.Field
        End Get
    End Property

End Class

'ALTER TABLE Place.Table1 ADD CONSTRAINT
'	PK_Table1 PRIMARY KEY CLUSTERED 
'	(
'	ID
'	)