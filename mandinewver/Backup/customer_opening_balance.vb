﻿Imports System.Data.SqlClient
Public Class customer_opening_balance
    Dim a7 As New predefined
    Dim customerid As Integer
    Dim voucherid, entryid As Integer
    Public companyid As Integer
    Dim ab, ab1, ab2 As SqlCommand
    Public Sub show_customer()
        Dim abc As SqlCommand
        ListBox1.Items.Clear()
        If bikribook.Visible = True Then
            abc = New SqlCommand("Select cname from customer where companyid=@companyid")
            abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = bikribook.companyid
        ElseIf Customer_Entry.Visible = True Then
            abc = New SqlCommand("Select cname from customer where companyid=@companyid")
            abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = Customer_Entry.companyid
        ElseIf Modifybikri.Visible = True Then
            abc = New SqlCommand("Select cname from customer where companyid=@companyid")
            abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = Modifybikri.companyid
        Else
            abc = New SqlCommand("Select cname from customer where companyid=@companyid")
            abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        End If
        a7.listbox_add(abc, ListBox1)
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then
                TextBox1.Focus()
                ListBox1.Visible = False
                Exit Sub
            End If
            Dim abc9 As SqlCommand
            Dim chk As Integer
            Dim cpid As Integer
            If bikribook.Visible = True Then
                cpid = bikribook.companyid
            ElseIf Customer_Entry.Visible = True Then
                cpid = Customer_Entry.companyid

            ElseIf Modifybikri.Visible = True Then
                cpid = Modifybikri.companyid
            Else
                cpid = companyid
            End If
            abc9 = New SqlCommand("select * from customer where companyid =@cpid and cname=@cname")
            abc9.Parameters.AddWithValue("@cpid", SqlDbType.Int).Value = cpid
            abc9.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
            chk = a7.check_rec(abc9)
            If chk > 0 Then
                MsgBox("Can't Accept Duplicate Customer Name...!!", MsgBoxStyle.Critical, "Vega 3.5")
                ListBox1.Visible = False
                Exit Sub
            Else
                TextBox2.Focus()
                ListBox1.Visible = False
            End If

        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        ListBox1.Visible = True
        a7.DoConvert(TextBox1)
        Dim abc As SqlCommand
        If bikribook.Visible = True Then
            abc = New SqlCommand("Select cname from customer where companyid=@companyid and cname like @cname + '%' order by cname")
            abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = bikribook.companyid
            abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        ElseIf Customer_Entry.Visible = True Then
            abc = New SqlCommand("Select cname from customer where companyid=@companyid and cname like @cname + '%'order by cname")
            abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = Customer_Entry.companyid
            abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        ElseIf Modifybikri.Visible = True Then
            abc = New SqlCommand("Select cname from customer where companyid=@companyid and cname like @cname + '%'order by cname")
            abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = Customer_Entry.companyid
            abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        Else
            abc = New SqlCommand("Select cname from customer where companyid=@companyid and cname like @cname + '%'order by cname")
            abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        End If
        ListBox1.Items.Clear()
        a7.listbox_add(abc, ListBox1)
    End Sub
    Private Sub customer_add_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If bikribook.Enabled = True Then
            bikribook.TextBox3.Focus()
        End If
        If Customer_Entry.Enabled = True Then
            Customer_Entry.TextBox1.Focus()
        End If
        If Modifybikri.Enabled = True Then
            Modifybikri.TextBox3.Focus()
        End If
        If Crate_issue.Enabled = True Then
            Crate_issue.TextBox2.Focus()
        End If
    End Sub
    Private Sub Customer_Entry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ListBox1.Visible = False
        DateTimePicker1.Value = MDIParent1.entry_date
        If bikribook.Visible = True Then
            ComboBox1.Visible = False
            Label4.Visible = False
        End If
        If Customer_Entry.Visible = True Then
            ComboBox1.Visible = False
            Label4.Visible = False
        End If
        If Modifybikri.Visible = True Then
            ComboBox1.Visible = False
            Label4.Visible = False
        End If
        If Crate_issue.Visible = True Then
            ComboBox1.Visible = False
            Label4.Visible = False
        End If
        company_add()
        show_customer()
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
        show_customer()
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
    Public Sub dummy_record()
        Dim part, type, desc As String
        'part = "Dummy"
        'type = "Dummy"
        part = "OB"
        type = "OB"
        desc = "DR"
        Dim amount As Integer
        If TextBox2.Text = "" Then
            amount = 0
        Else
            amount = Val(TextBox2.Text)
        End If
        ab1 = New SqlCommand("insert into voucher(cid,date,amount,particular,voucher_type,account_desc,companyid,sessionid)values(@cid,@date,@amount,@particular,@voucher_type,@account_desc,@companyid,@sessionid)")
        ab1.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid
        ab1.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        ab1.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(amount)
        ab1.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = part
        ab1.Parameters.AddWithValue("@voucher_type", SqlDbType.VarChar).Value = type
        ab1.Parameters.AddWithValue("@account_desc", SqlDbType.VarChar).Value = desc
        ab1.Parameters.AddWithValue("@receipt_no", SqlDbType.Int).Value = entryid
        ab1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        ab1.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        desc = "CR"
        amount = 0
        ab2 = New SqlCommand("insert into voucher(cid,date,amount,particular,voucher_type,account_desc,companyid,sessionid)values(@cid,@date,@amount,@particular,@voucher_type,@account_desc,@companyid,@sessionid)")
        ab2.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid
        ab2.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        ab2.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(amount)
        ab2.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = part
        ab2.Parameters.AddWithValue("@voucher_type", SqlDbType.VarChar).Value = type
        ab2.Parameters.AddWithValue("@account_desc", SqlDbType.VarChar).Value = desc
        ab2.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        ab2.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
    End Sub
    Public Sub data_add()
        Dim cnn As New SqlConnection
        Dim adapter As New SqlDataAdapter
        Dim tran As SqlTransaction = Nothing
        cnn = New SqlConnection(a7.connetionstring)
        Try
            cnn.Open()
            tran = cnn.BeginTransaction("Transaction1")
            ab.Connection = cnn
            ab.Transaction = tran
            adapter.InsertCommand = ab
            adapter.InsertCommand.ExecuteNonQuery()
            tran.Save("Save")

            ab1.Connection = cnn
            ab1.Transaction = tran
            adapter.InsertCommand = ab1
            adapter.InsertCommand.ExecuteNonQuery()
            tran.Save("Save1")

            ab2.Connection = cnn
            ab2.Transaction = tran
            adapter.InsertCommand = ab2
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
    Private Sub ListBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyUp
        If e.KeyCode = Keys.D Then
            Dim abc As SqlCommand
            abc = New SqlCommand("delete from customer where companyid=@companyid and cname=@customername")
            abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            abc.Parameters.AddWithValue("@customername", SqlDbType.VarChar).Value = ListBox1.SelectedItem
            a7.delete_data(abc)
            show_customer()
            TextBox1.Focus()
        End If
    End Sub
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        e.Handled = a7.trapkey(Asc(e.KeyChar))
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then Exit Sub
        customerid = a7.get_customer_id
        If bikribook.Visible = True Then
            ab = New SqlCommand("insert into customer(cname,companyid,customerid)values(@cname,@companyid,@customerid)")
            ab.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = bikribook.companyid
            ab.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
            ab.Parameters.AddWithValue("@customerid", SqlDbType.VarChar).Value = customerid
        ElseIf Customer_Entry.Visible = True Then
            ab = New SqlCommand("insert into customer(cname,companyid,customerid)values(@cname,@companyid,@customerid)")
            ab.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = Customer_Entry.companyid
            ab.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
            ab.Parameters.AddWithValue("@customerid", SqlDbType.VarChar).Value = customerid
        ElseIf Modifybikri.Visible = True Then
            ab = New SqlCommand("insert into customer(cname,companyid,customerid)values(@cname,@companyid,@customerid)")
            ab.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = Modifybikri.companyid
            ab.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
            ab.Parameters.AddWithValue("@customerid", SqlDbType.VarChar).Value = customerid
        Else
            ab = New SqlCommand("insert into customer(cname,companyid,customerid)values(@cname,@companyid,@customerid)")
            ab.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            ab.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
            ab.Parameters.AddWithValue("@customerid", SqlDbType.VarChar).Value = customerid
        End If
        dummy_record()
        data_add()
        TextBox1.Text = ""
        TextBox2.Text = ""
        'show_customer()
        show_list()
        TextBox1.Focus()
    End Sub
    Private Sub Button2_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub
    Public Sub show_list()
        Dim abc As SqlCommand
        Dim part, desc As String
        Dim totalamount As Integer
        totalamount = 0
        'voucher_type = "OB"
        part = "OB"
        desc = "DR"
        abc = New SqlCommand("select b.cname,a.amount,a.voucher_id from voucher a,customer b where a.cid=b.customerid and a.companyid=@companyid and a.sessionid=@sessionid and a.particular =@part and a.amount>@amt order by voucher_id ")
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        'abc.Parameters.Add("@acc_dec", SqlDbType.VarChar).Value = voucher_type
        abc.Parameters.Add("@part", SqlDbType.VarChar).Value = part
        abc.Parameters.Add("@amt", SqlDbType.Int).Value = totalamount
        a7.list_add1(abc, ListView1)
        Dim rcount, i As Integer
        rcount = ListView1.Items.Count
        i = 0
        Do While i < rcount
            totalamount = Val(totalamount) + Val(ListView1.Items(i).SubItems(2).Text)
            i = i + 1
        Loop
        TextBox3.Text = totalamount
    End Sub
End Class