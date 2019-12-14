Imports System.Data.SqlClient
Public Class Copybikri
    Dim a7 As New predefined
    Dim companyid, companyid1 As Integer
    Dim customerid, customerid1 As Integer
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox1)
    End Sub
    Public Sub company_add1()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox2)
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
        ComboBox2.Focus()
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid1 = a7.get_company_id(itemname)
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim abc As SqlCommand
        abc = New SqlCommand("insert into copybikribook(companyid,companyid1)values(@companyid,@customerid1)")
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@companyid1", SqlDbType.Int).Value = companyid1
        a7.insert_data1(abc)
        ComboBox1.Items.Clear()
        ComboBox2.Items.Clear()
        company_add()
        company_add1()
    End Sub
End Class