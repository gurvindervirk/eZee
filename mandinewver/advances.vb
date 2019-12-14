Imports System.Data.SqlClient
Public Class advances_entry
    Dim a7 As New predefined
    Public companyid, customerid As Integer
    Dim voucherid, entryid As Integer
    Private Sub ListBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox1.Text = ListBox1.Text
            customer_id_no()
            TextBox2.Focus()
            ListBox1.Visible = False
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox1.SelectedIndex = 0 Then
            End If
        End If
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Down Then
            ListBox1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then
                ListBox1.Visible = False
                Exit Sub
            End If
            If ListBox1.Items.Count = 0 Then
                Exit Sub
            End If
            TextBox1.Text = Trim(ListBox1.Items(0))
            customer_id_no()
            ListBox1.Visible = False
            TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then Exit Sub
            Dim abc1 As SqlCommand
            Dim type, desc As String
            desc = "DR"
            type = "DRvoucher"
            abc1 = New SqlCommand("insert into voucher(cid,date,amount,particular,voucher_type,account_desc,companyid,sessionid)values(@cid,@date,@amount,@particular,@voucher_type,@account_desc,@companyid,@sessionid)")

            abc1.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid
            abc1.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
            abc1.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(TextBox3.Text)
            abc1.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = TextBox2.Text
            abc1.Parameters.AddWithValue("@voucher_type", SqlDbType.VarChar).Value = type
            abc1.Parameters.AddWithValue("@account_desc", SqlDbType.VarChar).Value = desc

            abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            abc1.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
            a7.insert_data1(abc1)
            show_list()
            TextBox1.Focus()
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox1.Focus()
            ListBox1.Visible = False
            a7.Refresh_all(GroupBox1)
        End If
    End Sub

    Private Sub TextBox1_QueryContinueDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.QueryContinueDragEventArgs) Handles TextBox1.QueryContinueDrag

    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        ListBox1.Visible = True
        a7.DoConvert(TextBox1)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and  cname like @cname+ '%'order by cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        ListBox1.Items.Clear()
        a7.Listbox_add(abc, ListBox1)
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        a7.DoConvert(TextBox2)
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If TextBox1.Text = "" Then Exit Sub
        voucherid = a7.get_voucher_id
        entryid = a7.get_receipt_no
        Dim abc1 As SqlCommand
        Dim type, desc As String
        desc = "DR"
        type = "DRvoucher"
        abc1 = New SqlCommand("insert into voucher(cid,date,amount,particular,voucher_type,account_desc,companyid,sessionid)values(@cid,@date,@amount,@particular,@voucher_type,@account_desc,@companyid,@sessionid)")

        abc1.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid
        abc1.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc1.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(TextBox3.Text)
        abc1.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = TextBox2.Text
        abc1.Parameters.AddWithValue("@voucher_type", SqlDbType.VarChar).Value = type
        abc1.Parameters.AddWithValue("@account_desc", SqlDbType.VarChar).Value = desc

        abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc1.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        a7.insert_data1(abc1)
        show_list()
        TextBox1.Focus()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox1.Focus()
        ListBox1.Visible = False
        a7.Refresh_all(GroupBox1)
    End Sub
    Private Sub advances1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub advances_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DateTimePicker1.Value = MDIParent1.entry_date
        If MDIParent1.utype = "ADMIN" Then
            DateTimePicker1.Enabled = True
        Else
            DateTimePicker1.Enabled = False
        End If
        company_add()
        ListBox1.Visible = False
    End Sub
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
        TextBox1.Focus()
        show_list()
    End Sub
    Public Sub customer_id_no()
        Dim itemname As String
        itemname = TextBox1.Text
        customerid = a7.get_customer_id(companyid, itemname)
    End Sub
    Public Sub show_list()
        Dim abc As SqlCommand
        Dim voucher_type As String
        Dim totalamount As Integer
        totalamount = 0
        voucher_type = "DRvoucher"
        abc = New SqlCommand("select b.cname,a.amount,a.voucher_id from voucher a,customer b where a.cid=b.customerid and a.companyid=@companyid and a.voucher_type='" & voucher_type & "' and a.date=@dt1 order by voucher_id ")
        abc.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        a7.list_add1(abc, ListView1)
        Dim rcount, i As Integer
        rcount = ListView1.Items.Count
        i = 0
        Do While i < rcount
            totalamount = Val(totalamount) + Val(ListView1.Items(i).SubItems(2).Text)
            i = i + 1
        Loop
        TextBox4.Text = totalamount
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        Dim abc As SqlCommand
        Dim eno As Integer
        eno = Val(ListView1.SelectedItems.Item(0).SubItems(3).Text)
        If e.KeyCode = Keys.D Then
            abc = New SqlCommand("delete from voucher where voucher_id=" & eno)
            a7.Delete_data(abc)
            show_list()
            TextBox2.Focus()
        End If
        If e.KeyValue = Keys.S Then
            modify_advances.Label7.Text = Val(ListView1.SelectedItems.Item(0).SubItems(3).Text)
            modify_advances.voucherid = Val(ListView1.SelectedItems.Item(0).SubItems(3).Text)
            modify_advances.companyid = companyid
            modify_advances.TextBox4.Text = ComboBox1.Text
            modify_advances.Show()
        End If
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class