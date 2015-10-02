
Public Class MRDataPage

    Private m_Parent As IMRDataPaging = Nothing
    Public Property Parent As IMRDataPaging
        Get
            Return m_Parent
        End Get
        Set(ByVal value As IMRDataPaging)
            m_Parent = value
        End Set
    End Property

    Public Sub New()

    End Sub
    Public Sub New(ByVal first As Integer, ByVal last As Integer)
        MyClass.new()
        Me.First = first
        Me.Last = last
    End Sub

#Region "Borders"
    Private m_First As Integer
    Public Property First As Integer
        Get
            Return m_First
        End Get
        Set(ByVal value As Integer)
            m_First = value
        End Set
    End Property

    Private m_Last As Integer
    Public Property Last As Integer
        Get
            Return m_Last
        End Get
        Set(ByVal value As Integer)
            m_Last = value
        End Set
    End Property

    Public ReadOnly Property NumberOfRecords As Integer
        Get
            Return Me.Last - Me.First
        End Get
    End Property
#End Region


End Class
