Imports System.Data.SqlClient
Public Class change_cname
    Dim a7 As New predefined
    Dim companyid As Integer
    Dim customerid As Integer
    Public Sub show_customer()
        Dim abc As SqlCommand
        ListBox1.Items.Clear()
        abc = New SqlCommand("Select cname from customer where companyid=@companyid")
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        a7.listbox_add(abc, ListBox1)
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        Dim abc1 As SqlCommand
        Dim abc2 As SqlCommand
        Dim rec_check As Integer
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        rec_check = 0
        If e.KeyCode = Keys.Enter Then
            If TextBox2.Text = "" Then Exit Sub
            abc2 = New SqlCommand("select cname from customer where cname=@cname and companyid=@companyid")
            abc2.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            abc2.Parameters.Add("@cname", SqlDbType.VarChar).Value = TextBox2.Text
            rec_check = a7.check_rec(abc2)
            If rec_check = 0 Then
                abc1 = New SqlCommand("update customer set cname=@cname where customerid=@cid and companyid=@companyid")
                abc1.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                abc1.Parameters.Add("@cname", SqlDbType.VarChar).Value = TextBox2.Text
                abc1.Parameters.Add("@cid", SqlDbType.Int).Value = customerid
                a7.update_data(abc1)
                TextBox1.Text = ""
                TextBox2.Text = ""
                show_customer()
            Else
                MsgBox("Can't Change ....Customer Name already exists....", MsgBoxStyle.Information)
                Exit Sub
            End If
            TextBox1.Focus()
        End If
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then Exit Sub
            If ListBox1.Items.Count = 0 Then
                Exit Sub
            End If
            TextBox1.Text = Trim(ListBox1.Items(0))
            customer_id_no()
            ListBox1.Visible = False
            TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        a7.DoConvert(TextBox1)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and  cname like @cname + '%' order by cname")
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        ListBox1.Visible = True
        ListBox1.Items.Clear()
        a7.listbox_add(abc, ListBox1)
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
        TextBox1.Focus()
    End Sub
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox1)
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        a7.DoConvert(TextBox2)
    End Sub
    Private Sub change_cname_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ListBox1.Visible = False
        company_add()
    End Sub
    Public Sub customer_id_no()
        Dim itemname As String
        itemname = TextBox1.Text
        customerid = a7.get_customer_id(companyid, itemname)
    End Sub
End Class