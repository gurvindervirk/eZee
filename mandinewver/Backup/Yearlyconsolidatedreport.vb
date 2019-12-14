Imports System.Data.SqlClient
Public Class Yearlyconsolidatedreport
    Dim a7 As New predefined
    Dim abc1 As Integer
    Public sessionid As Integer
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox2)
    End Sub
    Private Sub Yearlyconsolidatedreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
    End Sub
End Class