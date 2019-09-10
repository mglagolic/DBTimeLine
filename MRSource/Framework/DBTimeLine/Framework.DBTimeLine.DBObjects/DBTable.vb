Option Strict On

Public Class DBTable
    Inherits DBObject
    Implements IDBTable

    Public Sub New()

    End Sub

    Public Sub New(descriptor As IDBTableDescriptor)
        MyClass.New

        Me.Descriptor = descriptor
        If Not String.IsNullOrWhiteSpace(descriptor.CreatorFieldName) AndAlso descriptor.CreatorFieldDescriptor IsNot Nothing Then
            AddField(descriptor.CreatorFieldName, descriptor.CreatorFieldDescriptor)
        End If
    End Sub

    Public Overrides ReadOnly Property ObjectTypeOrdinal As Integer
        Get
            Return 10
        End Get
    End Property

    Public Overrides ReadOnly Property ObjectTypeName As String
        Get
            Return "Table"
        End Get
    End Property

    'Public Sub AddClaims(created As Date) Implements IDBTable.AddClaims
    '    AddRevision(New DBRevision(created, 0, eDBRevisionType.Task, AddressOf AddTableClaims))
    'End Sub

    'Private Function AddTableClaims(sender As IDBRevision, dBType As eDBType) As String
    '    Return "
    '    INSERT INTO Common.Claim (ID, Name)
    '    SELECT ID, NAME 
    '    FROM 
    '        (
    '            SELECT ID = NEWID(), Name = '" & CType(sender.Parent, IDBTable).GetFullName() & ".Read' UNION ALL
    '            SELECT NEWID(), '" & CType(sender.Parent, IDBTable).GetFullName() & ".Write' 
    '        ) a 
    '    WHERE
    '        a.name NOT IN (SELECT Name FROM Common.Claim)"
    'End Function

    Public Function AddConstraint(descriptor As IDBConstraintDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBConstraint Implements IDBTable.AddConstraint
        Return CType(MyBase.AddDBObject(descriptor.GetConstraintName(SchemaName, Name), descriptor, createRevision), IDBConstraint)
    End Function

    Public Function AddField(fieldName As String, descriptor As IDBFieldDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBField Implements IDBTable.AddField
        Return CType(MyBase.AddDBObject(fieldName, descriptor, createRevision), IDBField)
    End Function

    Public Function AddIndex(descriptor As IDBIndexDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBIndex Implements IDBTable.AddIndex
        Return CType(MyBase.AddDBObject(descriptor.GetIndexName(SchemaName, Name), descriptor, createRevision), IDBIndex)
    End Function

    Public Overrides Function GetSqlCreate(dBType As eDBType) As String
        Dim ret As String = ""

        ret = String.Format("CREATE TABLE {0}.{1}
(
    {2} {3}
)
", SchemaName, Name, CType(Descriptor, IDBTableDescriptor).CreatorFieldName, (New DBField() With {.Descriptor = CType(Descriptor, IDBTableDescriptor).CreatorFieldDescriptor}).GetFieldTypeSql(dBType))

        Return ret
    End Function

    Public Overrides Function GetSqlModify(dBType As eDBType) As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetSqlDelete(dBType As eDBType) As String
        Dim ret As String = ""

        ret = String.Format("DROP TABLE {0}.{1}
", SchemaName, Name)

        Return ret
    End Function

End Class

