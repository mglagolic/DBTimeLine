Public Class DBView
    Inherits DBObject
    Implements IDBView

    Public Sub New()

    End Sub

    Public Sub New(descriptor As IDBViewDescriptor)
        MyClass.New

        Me.Descriptor = descriptor
    End Sub

    Public Overrides ReadOnly Property ObjectTypeOrdinal As Integer
        Get
            Return 30
        End Get
    End Property

    Public Overrides ReadOnly Property ObjectTypeName As String
        Get
            Return "View"
        End Get
    End Property

    Public Function AddIndex(descriptor As IDBIndexDescriptor, Optional createRevision As IDBRevision = Nothing) As IDBIndex Implements IDBView.AddIndex
        Return CType(MyBase.AddDBObject(descriptor.GetIndexName(SchemaName, Name), descriptor, createRevision), IDBIndex)
    End Function

    Public Overrides Function GetSqlCreate(dBType As eDBType) As String
        Dim ret As String = ""

        Dim bindingClause As String = ""

        With CType(Descriptor, IDBViewDescriptor)
            If .WithSchemaBinding Then
                bindingClause = "WITH SCHEMABINDING"
            End If
            ret = String.Format(
"
GO
CREATE VIEW {0}.{1}
{2}
AS
{3}
GO
", SchemaName, Name, bindingClause, .Body)
        End With

        Return ret
    End Function

    Public Overrides Function GetSqlModify(dBType As eDBType) As String
        Dim ret As String = ""
        Dim bindingClause As String = ""

        With CType(Descriptor, IDBViewDescriptor)
            If .WithSchemaBinding Then
                bindingClause = "WITH SCHEMABINDING"
            End If
            ret = String.Format(
"GO
ALTER VIEW {0}.{1}
{2}
AS
{3}
GO
", SchemaName, SchemaObjectName, bindingClause, .Body)
        End With

        Return ret
    End Function

    Public Overrides Function GetSqlDelete(dBType As eDBType) As String
        Dim ret As String = ""
        ret = String.Format(
"
GO
DROP VIEW {0}.{1}
GO
", SchemaName, SchemaObjectName)
        Return ret
    End Function

    'Public Sub AddClaims(created As Date) Implements IDBView.AddClaims
    '    AddRevision(New DBRevision(created, 0, eDBRevisionType.Task, AddressOf AddViewClaims))
    'End Sub

    'Private Function AddViewClaims(sender As IDBRevision, dBType As eDBType) As String
    '    Return "
    '    INSERT INTO Common.Claim (ID, Name)
    '    SELECT ID, NAME 
    '    FROM 
    '    (
    '        SELECT ID = NEWID(), Name = '" & CType(sender.Parent, IDBTable).GetFullName() & ".Read' 
    '    ) a 
    '    WHERE
    '        a.name NOT IN (SELECT NAME FROM Common.Claim)"

    'End Function
End Class
