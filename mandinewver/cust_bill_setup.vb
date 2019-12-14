Imports System.Data.SqlClient
Public Class cust_bill_setup
    Dim a7 As New predefined
    Dim customerid, companyid As Integer
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        a7.DoConvert(TextBox1)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and cname like @cname + '%' order by cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        ListBox1.Visible = True
        a7.Listbox_add(abc, ListBox1)
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Down Then
            ListBox1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            If ListBox1.Items.Count = 0 Then
                Exit Sub
            End If
            TextBox1.Text = Trim(ListBox1.Items(0))
            Dim cname As String
            cname = TextBox1.Text
            customerid = a7.get_customer_id(companyid, cname)
            ListBox1.Visible = False
            TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Escape Then
            TextBox1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Escape Then
            TextBox2.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim abc As SqlCommand
        abc = New SqlCommand("insert into cust_bill_setup(customerid,companyid,laga,arhat)values(@customerid,@companyid,@laga,@arhat)")
        abc.Parameters.AddWithValue("@customerid", SqlDbType.Int).Value = customerid
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid 
        abc.Parameters.AddWithValue("@laga", SqlDbType.Int).Value = Val(TextBox3.Text)
        abc.Parameters.AddWithValue("@arhat", SqlDbType.Int).Value = Val(TextBox2.Text)
        a7.insert_data1(abc)
        ComboBox1.Items.Clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub cust_bill_setup_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub cust_bill_setup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
        ListBox1.Visible = False
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
    End Sub
End Class