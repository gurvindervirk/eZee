Imports System.Data.SqlClient
Public Class list_cust_bill_setup
    Dim a7 As New predefined
    Dim companyid As Integer
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox1)
    End Sub
    Private Sub list_cust_bill_setup_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub list_cust_bill_setup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
    End Sub
    Public Sub list_show()
        Dim abc As SqlCommand
        abc = New SqlCommand("select a.cname,b.arhat,b.laga,b.customerid from  cust_bill_setup b,customer a where a.customerid=b.customerid and  b.companyid=@companyid")
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        a7.list_add1(abc, ListView1)
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyValue = Keys.S Then
            Modify_cust_bill_setup.TextBox4.Text = ComboBox1.Text
            Modify_cust_bill_setup.TextBox1.Text = ListView1.SelectedItems.Item(0).SubItems(1).Text
            Modify_cust_bill_setup.companyid = companyid
            Modify_cust_bill_setup.customerid = Val(ListView1.SelectedItems.Item(0).SubItems(4).Text)
            Modify_cust_bill_setup.show_data()
            Modify_cust_bill_setup.Show()
        End If
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
        list_show()
    End Sub
End Class