Imports System.Data.SqlClient
Public Class modify_cash_receipt
    Dim a7 As New predefined
    Public companyid, customerid As Integer
    Dim voucherid, entryid As Integer
    Private Sub TextBox4_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        If e.KeyCode = Keys.Enter Then
            If Val(TextBox4.Text) > Val(TextBox3.Text) Then
                MsgBox("Sorry !! Cash should be less than equal to Prv.Balance", MsgBoxStyle.Information, "Vega 3.5")
                Exit Sub
            End If
            Dim abc As SqlCommand
            abc = New SqlCommand("update voucher set cid=@cid,amount=@amount where voucher_id=@voucherid")
            abc.Parameters.AddWithValue("@voucherid", SqlDbType.Int).Value = Val(Label9.Text)
            abc.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid
            abc.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(TextBox4.Text)
            a7.Update_data(abc)
            Me.Close()
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        ListBox1.Visible = True
        a7.DoConvert(TextBox2)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and  cname like @cname+ '%' order by cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox2.Text
        ListBox1.Items.Clear()
        a7.Listbox_add(abc, ListBox1)
    End Sub
    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Down Then
            ListBox1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            If ListBox1.Items.Count = 0 Then
                Exit Sub
            End If
            TextBox2.Text = Trim(ListBox1.Items(0))
            customer_id_no()
            ListBox1.Visible = False
            prv_balance()
            TextBox4.Focus()
        End If
    End Sub
    Private Sub ListBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox2.Text = ListBox1.Text
            customer_id_no()
            ListBox1.Visible = False
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox1.SelectedIndex = 0 Then
            End If
        End If
    End Sub
    Private Sub modify_cash_receipt_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If cashrecipt.Visible = True Then
            cashrecipt.show_list()
            cashrecipt.ListView1.Focus()
        End If
    End Sub
    Private Sub cashrecipt_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub cashrecipt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        show_data()
        ListBox1.Visible = False
    End Sub
    Public Sub prv_balance()
        Dim abc, abc1 As SqlCommand
        Dim account_desc, account_desc1 As String
        Dim sum_dr, sum_cr, balance As Integer
        account_desc = "DR"
        account_desc1 = "CR"
        sum_dr = 0
        sum_cr = 0
        abc = New SqlCommand("select sum(amount) from voucher where companyid=" & companyid & " and sessionid=" & MDIParent1.sessionid & " and cid=" & customerid & " and account_desc='" & account_desc & "'")
        Dim xyz(0), xyz1(0) As String
        a7.array_list(abc, xyz, 0)
        sum_dr = xyz(0)
        abc1 = New SqlCommand("select sum(amount) from voucher where companyid=" & companyid & " and sessionid=" & MDIParent1.sessionid & " and cid=" & customerid & " and account_desc='" & account_desc1 & "'")
        a7.array_list(abc1, xyz1, 0)
        sum_cr = xyz1(0)
        balance = sum_dr - sum_cr
        TextBox3.Text = balance
        TextBox5.Text = Val(TextBox3.Text) - Val(TextBox4.Text)
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        TextBox5.Text = Val(TextBox3.Text) - Val(TextBox4.Text)
    End Sub
    Public Sub save_record()
       
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        TextBox2.Focus()
    End Sub
    Public Sub show_data()
        Dim abc As SqlCommand
        abc = New SqlCommand("select a.cname,b.date,b.amount,b.cid from customer a ,voucher b where a.customerid=b.cid and b.voucher_id=@voucherid")
        abc.Parameters.AddWithValue("@voucherid", SqlDbType.Int).Value = Val(Label9.Text)
        Dim xyz(5) As String
        a7.array_list(abc, xyz, 0)
        TextBox2.Text = xyz(0)
        DateTimePicker1.Value = xyz(1)
        customerid = xyz(3)
        prv_balance()
        TextBox4.Text = xyz(2)
        TextBox2.Focus()
    End Sub
    Public Sub customer_id_no()
        Dim itemname As String
        itemname = TextBox2.Text
        customerid = a7.get_customer_id(companyid, itemname)
    End Sub
End Class