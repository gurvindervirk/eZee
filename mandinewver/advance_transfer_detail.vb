Imports System.Data.SqlClient
Public Class advance_transfer_detail
    Dim a7 As New predefined
    Public companyid As Integer
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Public Sub show_transfer_detail()
        Dim abc As SqlCommand
        abc = New SqlCommand("select a.transferdate,b.date,a.fromcustid,a.tocustid,b.amount from transfer_advance a,voucher b where a.voucherid=b.voucher_id and a.transferdate>=@dt1 and a.transferdate<=@dt2 and a.companyid=@companyid")
        abc.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@dt2", SqlDbType.DateTime).Value = DateTimePicker2.Value.Date
        abc.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        a7.list_add1(abc, ListView1)
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        show_transfer_detail()
    End Sub
    Private Sub advance_transfer_detail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
        show_transfer_detail()
        ListView1.Focus()
    End Sub
End Class