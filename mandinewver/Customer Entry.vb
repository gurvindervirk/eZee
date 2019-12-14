Imports System.Data.SqlClient
Public Class Customer_Entry
    Dim a7 As New predefined
    Public companyid, customerid As Integer
    Dim voucherid, entryid, voucherid1, entryid1, voucherid2, entryid2 As Integer
    Dim ab2, ab3, ab4, ab5 As New SqlCommand
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then Exit Sub
            ListBox1.Visible = False
            TextBox3.Focus()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        ListBox1.Visible = True
        a7.DoConvert(TextBox1)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and  cname like @cname+ '%' order by cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        ListBox1.Items.Clear()
        a7.Listbox_add(abc, ListBox1)
    End Sub
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then Exit Sub
        Dim abc9 As SqlCommand
        Dim chk As Integer
        abc9 = New SqlCommand("select * from customer where companyid =@cpid and cname=@cname")
        abc9.Parameters.AddWithValue("@cpid", SqlDbType.Int).Value = companyid
        abc9.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        chk = a7.check_rec(abc9)
        If chk > 0 Then
            MsgBox("Can't Accept Duplicate Customer Name...!!", MsgBoxStyle.Critical, "Vega 3.5")
            Exit Sub
        End If
        customerid = a7.get_customer_id
        ab2 = New SqlCommand("insert into customer(cname,companyid,customerid)values(@cname,@companyid,@customerid)")
        ab2.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        ab2.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        ab2.Parameters.AddWithValue("@customerid", SqlDbType.Int).Value = customerid
       
        Dim part, type, desc As String
        part = "Opening Balance"
        type = "OB"
        desc = "DR"
        ab3 = New SqlCommand("insert into voucher(cid,date,amount,particular,voucher_type,account_desc,companyid,sessionid)values(@cid,@date,@amount,@particular,@voucher_type,@account_desc,@companyid,@sessionid)")

        ab3.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid
        ab3.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        ab3.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(TextBox2.Text)
        ab3.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = part
        ab3.Parameters.AddWithValue("@voucher_type", SqlDbType.VarChar).Value = type
        ab3.Parameters.AddWithValue("@account_desc", SqlDbType.VarChar).Value = desc

        ab3.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        ab3.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        dummy_record()
        data_add()
        show_list()
        TextBox1.Focus()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ListBox1.Visible = False
    End Sub
    Private Sub Customer_Entry_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If bikribook.Enabled = True Then
            bikribook.TextBox3.Focus()
        End If
    End Sub
    Private Sub Customer_Entry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DateTimePicker1.Value = Today
        ListBox1.Visible = False
        company_add()

    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
        TextBox1.Focus()
        show_list()
    End Sub
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If ComboBox1.Text = "" Then
            MsgBox("Select Company Before Customer Entry", MsgBoxStyle.Information, "Vega 3.5")
            Exit Sub
        End If
        customer_entry_form.DateTimePicker1.Value = DateTimePicker1.Value
        customer_entry_form.Show()
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        a7.DoConvert(TextBox3)
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        'If e.KeyValue = Keys.Enter Then
        '    If ListView1.SelectedItems.Count = 0 Then Exit Sub
        '    modify_op_balance.Label7.Text = ListView1.SelectedItems.Item(0).SubItems(0).Text
        '    modify_op_balance.Show()
        'End If
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
    End Sub
    Public Sub dummy_record()
        Dim part, type, desc As String
        part = "Dummy"
        type = "Dummy"
        desc = "DR"
        Dim amount As Integer
        amount = 0
        ab4 = New SqlCommand("insert into voucher(cid,date,amount,particular,voucher_type,account_desc,companyid,sessionid)values(@cid,@date,@amount,@particular,@voucher_type,@account_desc,@companyid,@sessionid)")
        ab4.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid
        ab4.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        ab4.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(amount)
        ab4.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = part
        ab4.Parameters.AddWithValue("@voucher_type", SqlDbType.VarChar).Value = type
        ab4.Parameters.AddWithValue("@account_desc", SqlDbType.VarChar).Value = desc
        ab4.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        ab4.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        voucherid2 = a7.get_voucher_id + 2
        entryid2 = a7.get_receipt_no + 2
        desc = "CR"
        ab5 = New SqlCommand("insert into voucher(cid,date,amount,particular,voucher_type,account_desc,companyid,sessionid)values(@cid,@date,@amount,@particular,@voucher_type,@account_desc,@companyid,@sessionid)")
        ab5.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid
        ab5.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        ab5.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(amount)
        ab5.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = part
        ab5.Parameters.AddWithValue("@voucher_type", SqlDbType.VarChar).Value = type
        ab5.Parameters.AddWithValue("@account_desc", SqlDbType.VarChar).Value = desc
        ab5.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        ab5.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
    End Sub
    Public Sub show_list()
        Dim abc As SqlCommand
        Dim voucher_type As String
        Dim totalamount As Integer
        totalamount = 0
        voucher_type = "OB"
        abc = New SqlCommand("select b.cname,a.amount,a.voucher_id from voucher a,customer b where a.cid=b.customerid and a.companyid=@companyid and a.voucher_type='" & voucher_type & "' and a.date=@dt1 and a.sessionid=@sessionid order by voucher_id ")
        abc.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
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
    Public Sub data_add()
        Dim cnn As New SqlConnection
        Dim adapter As New SqlDataAdapter
        Dim tran As SqlTransaction = Nothing
        cnn = New SqlConnection(a7.connetionstring)
        Try
            cnn.Open()
            tran = cnn.BeginTransaction("Transaction1")
            ab2.Connection = cnn
            ab2.Transaction = tran
            adapter.InsertCommand = ab2
            adapter.InsertCommand.ExecuteNonQuery()
            tran.Save("Save")

            ab3.Connection = cnn
            ab3.Transaction = tran
            adapter.InsertCommand = ab3
            adapter.InsertCommand.ExecuteNonQuery()
            tran.Save("Save1")

            ab4.Connection = cnn
            ab4.Transaction = tran
            adapter.InsertCommand = ab4
            adapter.InsertCommand.ExecuteNonQuery()
            tran.Save("Save3")

            ab5.Connection = cnn
            ab5.Transaction = tran
            adapter.InsertCommand = ab5
            adapter.InsertCommand.ExecuteNonQuery()
            tran.Save("Save3")

            tran.Commit()
        Catch ex As Exception
            MsgBox(ex.Message)
            tran.Rollback()
        Finally
            cnn.Close()
        End Try
    End Sub
End Class