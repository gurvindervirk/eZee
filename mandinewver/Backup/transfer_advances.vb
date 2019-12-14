Imports System.Data.SqlClient
Public Class transfer_advances
    Dim a7 As New predefined
    Public companyid, voucherid, customerid, customerid1, voucherid1, entryid As Integer
    Public receipt_date As Date
    Public Sub show_data()
        Dim abc As SqlCommand
        abc = New SqlCommand("select a.cname,b.amount,b.date from customer a,voucher b where b.voucher_id=@voucherid and a.customerid=b.cid")
        abc.Parameters.AddWithValue("@voucherid", SqlDbType.Int).Value = voucherid
        Dim xyz(4) As String
        a7.array_list(abc, xyz, 0)
        Label6.Text = xyz(0)
        Label10.Text = xyz(1)
        prv_balance()
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            If ListBox1.Items.Count = 0 Then
                Exit Sub
            End If
            TextBox1.Text = Trim(ListBox1.Items(0))
            customer_id_no()
            ListBox1.Visible = False
        End If
        If e.KeyCode = Keys.Down Then
            ListBox1.Focus()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        ListBox1.Visible = True
        a7.DoConvert(TextBox1)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and cname like @cname + '%' order by cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        ListBox1.Items.Clear()
        a7.listbox_add(abc, ListBox1)
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        e.Handled = a7.trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Label15.Text = Val(Label10.Text) - Val(TextBox3.Text)
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox3.Text = "" Then Exit Sub
        If Val(Label15.Text) < 0 Then
            MsgBox("Transfer Amount is going greater then actual amount", MsgBoxStyle.Information, "Can't Transfer")
            Exit Sub
        End If
        If Val(TextBox3.Text) > Val(Label16.Text) Then
            MsgBox("Customer Balance will be less")
            Exit Sub
        End If
        Dim cnn As New SqlConnection
        Dim adapter As New SqlDataAdapter
        Dim tran As SqlTransaction = Nothing
        cnn = New SqlConnection(a7.connetionstring)
        Try
            cnn.Open()
            tran = cnn.BeginTransaction("Transaction1")
            If Label15.Text = 0 Then
                Dim abc1 As SqlCommand
                abc1 = New SqlCommand("update voucher set cid=@cid where voucher_id=@voucherid")
                abc1.Parameters.AddWithValue("@voucherid", SqlDbType.Int).Value = voucherid
                abc1.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid1
                abc1.Connection = cnn
                abc1.Transaction = tran
                adapter.UpdateCommand = abc1
                adapter.UpdateCommand.ExecuteNonQuery()
                tran.Save("Save")

            Else
                Dim abc1 As SqlCommand
                abc1 = New SqlCommand("update voucher set amount=@amount where voucher_id=@voucherid")
                abc1.Parameters.AddWithValue("@voucherid", SqlDbType.Int).Value = voucherid
                abc1.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(Label15.Text)

                abc1.Connection = cnn
                abc1.Transaction = tran
                adapter.UpdateCommand = abc1
                adapter.UpdateCommand.ExecuteNonQuery()
                tran.Save("Save")

                Dim abc2 As SqlCommand
                Dim part, type, desc As String
                part = "DRvoucher"
                desc = "DR"
                type = "DRvoucher"
                abc2 = New SqlCommand("insert into voucher(cid,date,amount,particular,voucher_type,account_desc,companyid,sessionid)values(,@cid,@date,@amount,@particular,@voucher_type,@account_desc,@companyid,@sessionid)")

                abc2.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid1
                abc2.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                abc2.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(TextBox3.Text)
                abc2.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = part
                abc2.Parameters.AddWithValue("@voucher_type", SqlDbType.VarChar).Value = type
                abc2.Parameters.AddWithValue("@account_desc", SqlDbType.VarChar).Value = desc

                abc2.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
                abc2.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid

                abc2.Connection = cnn
                abc2.Transaction = tran
                adapter.InsertCommand = abc2
                adapter.InsertCommand.ExecuteNonQuery()
                tran.Save("Save1")
            End If
            Dim abc3 As SqlCommand
            abc3 = New SqlCommand("insert into transfer_advance(companyid,sessionid,fromcustid,tocustid,transferdate,entryno,voucherid,amount)values(@companyid,@sessionid,@fromcustid,@customerid,@transferdate,@entryno,@voucherid,@amount)")
            abc3.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            abc3.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
            abc3.Parameters.AddWithValue("@fromcustid", SqlDbType.VarChar).Value = customerledger1.TextBox1.Text
            abc3.Parameters.AddWithValue("@customerid", SqlDbType.VarChar).Value = TextBox1.Text
            abc3.Parameters.AddWithValue("@transferdate", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
            abc3.Parameters.AddWithValue("@entryno", SqlDbType.Int).Value = entryid
            abc3.Parameters.AddWithValue("@voucherid", SqlDbType.Int).Value = voucherid
            abc3.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(TextBox3.Text)

            abc3.Connection = cnn
            abc3.Transaction = tran
            adapter.InsertCommand = abc3
            adapter.InsertCommand.ExecuteNonQuery()
            tran.Save("Save2")
            tran.Commit()
        Catch ex As Exception
            MsgBox(ex.Message)
            tran.Rollback()
        Finally
            cnn.Close()
        End Try
        Me.Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub transfer_advances_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If customerledger1.Visible = True Then
            customerledger1.show_ledger()
        End If
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
        Label16.Text = balance
    End Sub
    Public Sub customer_id_no()
        Dim itemname As String
        itemname = TextBox1.Text
        customerid1 = a7.get_customer_id(companyid, itemname)
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.S Then
            voucherid = ListView1.SelectedItems(0).SubItems(1).Text
            Label4.Text = voucherid
            Label10.Text = ListView1.SelectedItems(0).SubItems(2).Text
            'Label15.Text = ListView1.SelectedItems(0).SubItems(2).Text
        End If
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Sub show_receipts()
        Dim vtype As String
        vtype = "DRvoucher"
        Dim abc As SqlCommand
        abc = New SqlCommand("select voucher_id,amount from voucher where cid=@customerid and sessionid=@sessionid and companyid=@companyid and date=@date and voucher_type=@voucher_type")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@customerid", SqlDbType.Int).Value = customerid
        abc.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        abc.Parameters.AddWithValue("@voucher_type", SqlDbType.VarChar).Value = vtype
        abc.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        a7.list_add1(abc, ListView1)
    End Sub
    Private Sub transfer_advances_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DateTimePicker1.Text = receipt_date
        show_receipts()
        prv_balance()
        If MDIParent1.utype = "USER" Then
            Label3.Visible = False
            Label4.Visible = False
            Label2.Visible = False
            Label9.Visible = False
            Label10.Visible = False
            Label16.Visible = False
            Label11.Visible = False
            TextBox1.Visible = False
            Label13.Visible = False
            TextBox3.Visible = False
            Label14.Visible = False
            Button1.Visible = False
            Button2.Visible = False
        End If
    End Sub
    Private Sub ListBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox1.Text = ListBox1.Text
            customer_id_no()
            ListBox1.Visible = False
            TextBox3.Focus()
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox1.SelectedIndex = 0 Then
                TextBox1.Focus()
            End If
        End If
    End Sub
End Class
