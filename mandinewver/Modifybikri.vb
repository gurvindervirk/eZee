Imports System.Data.SqlClient
Public Class Modifybikri
    Dim a7 As New predefined
    Dim arhat, laga As Double
    Dim choice As String
    Public companyid, bookid, customerid, sessionid, vid As Integer
    Public Sub data_list_add()
        Dim abc As SqlCommand
        abc = New SqlCommand("select a.cname,b.nag,b.weight,b.rate,b.total,b.arhat,b.laga,b.gtotal,b.entry_no from bikri b, customer a where a.customerid=b.cid and b.sessionid=@sd and b.entry_date=@fd and b.companyid=@cd and b.bookid=@pd and b.bname=@bname and b.vegetableid=@veg")
        abc.Parameters.Add("@fd", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@sd", SqlDbType.Int).Value = MDIParent1.sessionid
        abc.Parameters.Add("@cd", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@pd", SqlDbType.Int).Value = bookid
        abc.Parameters.Add("@bname", SqlDbType.VarChar).Value = TextBox19.Text
        abc.Parameters.Add("@veg", SqlDbType.Int).Value = vid
        a7.List_add(abc, ListView1)
    End Sub
    Private Sub Modifybikri_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Daily_Bikri_Report.test_list()
    End Sub
    Private Sub Modifybikri_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Modifybikri_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ListBox2.Visible = False
        get_company_id()
        get_book_id()
        get_vegetable_id()
        arhat_show()
        data_list_add()
        sum_bikri()
        Dim xyz(1) As String
        Dim abc As SqlCommand
        abc = New SqlCommand("select com_option from company where companyid=@companyid")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        a7.array_list(abc, xyz, 0)
        choice = xyz(0)
        check_status()
    End Sub
    Public Sub sum_bikri()
        Dim abc As SqlCommand
        abc = New SqlCommand("select entry_date,sessionid,companyid,bookid,bname,vegetableid,sum(nag),sum(weight),sum(total),sum(arhat),sum(laga),sum(gtotal) from bikri group by entry_date,sessionid,companyid,bookid,bname,vegetableid having entry_date=@fd and sessionid=@sd and companyid=@cd  and bookid=@pd and bname=@bname and vegetableid=@veg")
        abc.Parameters.Add("@fd", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@sd", SqlDbType.Int).Value = MDIParent1.sessionid
        abc.Parameters.Add("@cd", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@pd", SqlDbType.Int).Value = bookid
        abc.Parameters.Add("@bname", SqlDbType.VarChar).Value = TextBox19.Text
        abc.Parameters.Add("@veg", SqlDbType.Int).Value = vid
        Dim textarray(6) As String
        a7.array_list(abc, textarray, 6)
        TextBox17.Text = textarray(0)
        TextBox16.Text = textarray(1)
        TextBox14.Text = textarray(2)
        TextBox13.Text = textarray(3)
        TextBox12.Text = textarray(4)
        TextBox11.Text = textarray(5)
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Down Then
            ListBox2.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            If TextBox3.Text = "" Then
                TextBox3.Text = "CASH"
                ListBox2.Visible = False
                TextBox4.Focus()
                Exit Sub
            End If
            If ListBox2.Items.Count = 0 Then
                Exit Sub
            End If
            TextBox3.Text = Trim(ListBox2.Items(0))
            customer_id_no()
            ListBox2.Visible = False
            TextBox4.Focus()
            Dim xyz(3) As String
            Dim abc1 As SqlCommand
            abc1 = New SqlCommand("select arhat,laga from cust_bill_setup where customerid=@customerid and companyid=@companyid")
            abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            abc1.Parameters.AddWithValue("@customerid", SqlDbType.VarChar).Value = customerid
            a7.array_list(abc1, xyz, 0)
            If Val(xyz(0)) > 0 Then
                arhat = xyz(0)
            Else
                arhat = TextBox15.Text
            End If
            If Val(xyz(1)) > 0 Then
                'laga = xyz(1)
                laga = 0
            Else
                laga = TextBox18.Text
            End If
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        a7.DoConvert(TextBox3)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and cname like @cname +'%' order by cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox3.Text
        ListBox2.Visible = True
        a7.Listbox_add(abc, ListBox2)
    End Sub
    Private Sub ListBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox2.KeyUp
        Dim xyz(3) As String
        Dim abc1 As SqlCommand
        If e.KeyCode = Keys.Enter Then
            TextBox3.Text = ListBox2.Text
            customer_id_no()
            abc1 = New SqlCommand("select arhat,laga from cust_bill_setup where customerid=@customerid and companyid=@companyid")
            abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            abc1.Parameters.AddWithValue("@customerid", SqlDbType.VarChar).Value = customerid
            a7.array_list(abc1, xyz, 0)
            If Val(xyz(0)) > 0 Then
                arhat = xyz(0)
            Else
                arhat = TextBox15.Text
            End If
            If Val(xyz(1)) > 0 Then
                laga = xyz(1)
            Else
                laga = TextBox18.Text
            End If
            TextBox4.Focus()
            ListBox2.Visible = False
            Exit Sub

            If ListBox2.Items.Count = 0 Then
                Exit Sub
            End If
            TextBox3.Text = Trim(ListBox2.Items(0))
            customer_id_no()
            ListBox2.Visible = False
            TextBox4.Focus()
            abc1 = New SqlCommand("select arhat,laga from cust_bill_setup where customerid=@customerid and companyid=@companyid")
            abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            abc1.Parameters.AddWithValue("@customerid", SqlDbType.VarChar).Value = customerid
            a7.array_list(abc1, xyz, 0)
            If Val(xyz(0)) > 0 Then
                arhat = xyz(0)
            Else
                arhat = TextBox15.Text
            End If
            If Val(xyz(1)) > 0 Then
                laga = xyz(1)
                laga = 0
            Else
                laga = TextBox18.Text
            End If
            ListBox2.Visible = False
            TextBox4.Focus()
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox2.SelectedIndex = 0 Then
                TextBox3.Focus()
            End If
        End If

        'Dim xyz(3) As String
        'Dim abc1 As SqlCommand
        'If e.KeyCode = Keys.Enter Then
        '    TextBox3.Text = ListBox2.Text
        '    abc1 = New SqlCommand("select arhat,laga from cust_bill_setup where customerid=@customerid and companyid=@companyid")
        '    abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        '    abc1.Parameters.AddWithValue("@customerid", SqlDbType.VarChar).Value = customerid
        '    a7.array_list(abc1, xyz, 0)
        '    If Val(xyz(0)) > 0 Then
        '        arhat = xyz(0)
        '    Else
        '        arhat = TextBox15.Text
        '    End If
        '    If Val(xyz(1)) > 0 Then
        '        laga = xyz(1)
        '    Else
        '        laga = TextBox18.Text
        '    End If
        '    TextBox4.Focus()
        '    ListBox2.Visible = False
        '    Exit Sub

        '    If ListBox2.Items.Count = 0 Then
        '        Exit Sub
        '    End If
        '    TextBox3.Text = Trim(ListBox2.Items(0))
        '    customer_id_no()
        '    ListBox2.Visible = False
        '    TextBox4.Focus()
        '    abc1 = New SqlCommand("select arhat,laga from cust_bill_setup where customerid=@customerid and companyid=@companyid")
        '    abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        '    abc1.Parameters.AddWithValue("@customerid", SqlDbType.VarChar).Value = customerid
        '    a7.array_list(abc1, xyz, 0)
        '    If Val(xyz(0)) > 0 Then
        '        arhat = xyz(0)
        '    Else
        '        arhat = TextBox15.Text
        '    End If
        '    If Val(xyz(1)) > 0 Then
        '        laga = xyz(1)
        '        laga = 0
        '    Else
        '        laga = TextBox18.Text
        '    End If
        '    ListBox2.Visible = False
        '    TextBox4.Focus()
        'End If

        'If e.KeyCode = Keys.Up Then
        '    If ListBox2.SelectedIndex = 0 Then
        '        TextBox3.Focus()
        '    End If
        'End If

    End Sub
    Private Sub TextBox4_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        If e.KeyCode = Keys.Enter Then
            If TextBox4.Text = "" Then TextBox4.Text = 1
            TextBox5.Focus()
        End If
    End Sub
    Private Sub TextBox6_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyUp
        If e.KeyCode = Keys.Enter Then
            insert_data()
        End If
    End Sub
    Private Sub TextBox5_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox6.Focus()
        End If
    End Sub
    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
        If RadioButton3.Checked = True Then
            If Val(TextBox5.Text) > 0 Then
                TextBox7.Text = Val(TextBox5.Text) * Val(TextBox6.Text)
            Else
                TextBox7.Text = Val(TextBox4.Text) * Val(TextBox6.Text)
            End If
        End If
        If RadioButton5.Checked = True Then
            TextBox7.Text = TextBox6.Text
        End If
        cal_grand()
    End Sub
    Public Sub cal_grand()
        If choice = "N" Then
            TextBox8.Text = (Val(TextBox7.Text) * arhat) / 100
            TextBox9.Text = (Val(TextBox4.Text) * laga)
            TextBox10.Text = Val(TextBox7.Text) + Val(TextBox8.Text) + Val(TextBox9.Text)
        Else
            TextBox9.Text = (Val(TextBox4.Text) * laga)
            TextBox8.Text = ((Val(TextBox7.Text) + Val(TextBox9.Text)) * arhat) / 100
            TextBox10.Text = Val(TextBox7.Text) + Val(TextBox8.Text) + Val(TextBox9.Text)
        End If
    End Sub
    Public Sub insert_data()
        If TextBox1.Text = "" Then
            MsgBox("Cann't Accept Blank Company", MsgBoxStyle.Critical, "Vega ver 3.5")
            Exit Sub
        End If
        If TextBox2.Text = "" Then
            MsgBox("Cann't Accept Blank Book", MsgBoxStyle.Critical, "Vega ver 3.5")
            Exit Sub
        End If
        If TextBox19.Text = "" Then
            MsgBox("Cann't Accept Blank Beopari Name", MsgBoxStyle.Critical, "Vega ver 3.5")
            Exit Sub
        End If
        If TextBox20.Text = "" Then
            MsgBox("Cann't Accept Blank Vegetable", MsgBoxStyle.Critical, "Vega ver 3.5")
            TextBox2.Focus()
            Exit Sub
        End If
        If TextBox3.Text = "" Then
            MsgBox("Cann't Accept Blank Customer", MsgBoxStyle.Critical, "Vega ver 3.5")
            TextBox3.Focus()
            Exit Sub
        End If
        customer_id_no()
        get_vegetable_id()
        Dim abc, abc1, abc4 As SqlCommand
        Dim cnn As New SqlConnection
        Dim narration As String
        narration = TextBox20.Text & " " & "(" & TextBox4.Text & "Nag" & "," & " " & TextBox5.Text & "Kg" & " " & "@" & TextBox6.Text & "=" & " " & TextBox7.Text & ")"
        Dim adapter As New SqlDataAdapter
        Dim tran As SqlTransaction = Nothing
        cnn = New SqlConnection(a7.connetionstring)
        Try
            cnn.Open()
            tran = cnn.BeginTransaction("Transaction1")
            abc = New SqlCommand("insert into bikri(entry_date,companyid,sessionid,bookid,bname,vegetableid,cid,nag,weight,rate,total,arhat,laga,gtotal,arhat_rate,laga_rate)values(@entry_date,@companyid,@sessionid,@bookid,@bname,@vegetableid,@cid,@nag,@weight,@rate,@total,@arhat,@laga,@gtotal,@arhat_rate,@laga_rate)select Scope_Identity()")
            abc.Parameters.Add("@entry_date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
            abc.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
            abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            abc.Parameters.Add("@bookid", SqlDbType.Int).Value = bookid
            abc.Parameters.Add("@bname", SqlDbType.VarChar).Value = TextBox19.Text
            abc.Parameters.Add("@vegetableid", SqlDbType.Int).Value = vid
            abc.Parameters.Add("@cid", SqlDbType.Int).Value = customerid
            abc.Parameters.Add("@nag", SqlDbType.Float).Value = Val(TextBox4.Text)
            abc.Parameters.Add("@weight", SqlDbType.Float).Value = Val(TextBox5.Text)
            abc.Parameters.Add("@rate", SqlDbType.Float).Value = Val(TextBox6.Text)
            abc.Parameters.Add("@total", SqlDbType.Float).Value = Val(TextBox7.Text)
            abc.Parameters.Add("@arhat", SqlDbType.Float).Value = Val(TextBox8.Text)
            abc.Parameters.Add("@laga", SqlDbType.Float).Value = Val(TextBox9.Text)
            abc.Parameters.Add("@gtotal", SqlDbType.Float).Value = Val(TextBox10.Text)
            abc.Parameters.Add("@arhat_rate", SqlDbType.Float).Value = Val(TextBox15.Text)
            abc.Parameters.Add("@laga_rate", SqlDbType.Float).Value = Val(TextBox18.Text)

            abc.Connection = cnn
            abc.Transaction = tran
            adapter.InsertCommand = abc
            Dim identity As Integer = -1
            identity = Integer.Parse(adapter.InsertCommand.ExecuteScalar().ToString())
            tran.Save("Save")
            Dim desc, type As String
            desc = "DR"
            type = "Bill"
            abc1 = New SqlCommand("insert into voucher(cid,date,amount,particular,voucher_type,account_desc,companyid,sessionid,narration)values(@cid,@date,@amount,@particular,@voucher_type,@account_desc,@companyid,@sessionid,@narration)select Scope_Identity()")
            abc1.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid
            abc1.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
            abc1.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(TextBox10.Text)
            abc1.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = type
            abc1.Parameters.AddWithValue("@voucher_type", SqlDbType.VarChar).Value = type
            abc1.Parameters.AddWithValue("@account_desc", SqlDbType.VarChar).Value = desc
            abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            abc1.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
            abc1.Parameters.AddWithValue("@narration", SqlDbType.VarChar).Value = narration
            abc1.Connection = cnn
            abc1.Transaction = tran
            adapter.InsertCommand = abc1
            Dim identity1 As Integer = -1
            identity1 = Integer.Parse(adapter.InsertCommand.ExecuteScalar().ToString())
            tran.Save("Save1")

            abc4 = New SqlCommand("insert into invoice_voucher(invoice_no,voucher_id)values(@invoiceid,@voucherid)")
            abc4.Parameters.AddWithValue("@voucherid", SqlDbType.Int).Value = identity1
            abc4.Parameters.Add("@invoiceid", SqlDbType.Float).Value = identity
            abc4.Connection = cnn
            abc4.Transaction = tran
            adapter.InsertCommand = abc4
            adapter.InsertCommand.ExecuteNonQuery()
            tran.Save("Save3")
            tran.Commit()

        Catch ex As Exception
            MsgBox(ex.Message)
            tran.Rollback()
        Finally
            cnn.Close()
        End Try
        data_list_add()
        sum_bikri()
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        If RadioButton2.Checked = True Then
            TextBox6.Text = ""
        End If
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        ListBox2.Visible = False
        TextBox3.Focus()
    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Dim abc, abc1, abc2, abc3 As SqlCommand
        Dim eno, voucherid As Integer
        Dim cnn As New SqlConnection
        Dim adapter As New SqlDataAdapter
        Dim tran As SqlTransaction = Nothing
        eno = Val(ListView1.SelectedItems.Item(0).SubItems(8).Text)
        If MsgBox("Delete Record ......?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Delete") = MsgBoxResult.Yes Then
            Try
                abc1 = New SqlCommand("select voucher_id from invoice_voucher where invoice_no=@invoiceno")
                abc1.Parameters.AddWithValue("@invoiceno", SqlDbType.Int).Value = eno
                Dim xyz(0) As String
                a7.array_list(abc1, xyz, 0)
                voucherid = xyz(0)
                abc3 = New SqlCommand("delete from invoice_voucher where invoice_no=@invoiceno")
                abc3.Parameters.AddWithValue("@invoiceno", SqlDbType.Int).Value = eno
                abc2 = New SqlCommand("delete from voucher where voucher_id=@voucherid")
                abc2.Parameters.AddWithValue("@voucherid", SqlDbType.Int).Value = voucherid
                abc = New SqlCommand("delete  from bikri where entry_no=" & eno)
                cnn = New SqlConnection(a7.connetionstring)

                cnn.Open()
                tran = cnn.BeginTransaction("Transaction1")
                abc3.Connection = cnn
                abc3.Transaction = tran
                adapter.DeleteCommand = abc3
                adapter.DeleteCommand.ExecuteNonQuery()
                tran.Save("save")

                abc2.Connection = cnn
                abc2.Transaction = tran
                adapter.DeleteCommand = abc2
                adapter.DeleteCommand.ExecuteNonQuery()
                tran.Save("Save1")

                abc.Connection = cnn
                abc.Transaction = tran
                adapter.DeleteCommand = abc
                adapter.DeleteCommand.ExecuteNonQuery()
                tran.Save("Save2")
                tran.Commit()
            Catch ex As Exception
                MsgBox(ex.Message)
                tran.Rollback()
            Finally
                cnn.Close()
            End Try
            data_list_add()
            sum_bikri()
            TextBox3.Focus()
            ListBox2.Visible = False
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Sub arhat_show()
        Dim xyz(2) As String
        Dim abc As SqlCommand
        abc = New SqlCommand("select arhat,laga from vegetable where vegetableid=@vid")
        abc.Parameters.AddWithValue("@vid", SqlDbType.Int).Value = vid
        a7.array_list(abc, xyz, 0)
        TextBox15.Text = xyz(0)
        TextBox18.Text = xyz(1)
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        customer_entry_form.DateTimePicker1.Value = DateTimePicker1.Value
        customer_entry_form.companyid = companyid
        customer_entry_form.Show()
    End Sub
    Private Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Vegetablesentry.Show()
    End Sub
    Public Sub customer_id_no()
        Dim itemname As String
        itemname = TextBox3.Text
        customerid = a7.get_customer_id(companyid, itemname)
    End Sub
    Public Sub get_vegetable_id()
        Dim abc As SqlCommand
        abc = New SqlCommand("select vegetableid from vegetable where name=@vegname")
        abc.Parameters.AddWithValue("@vegname", SqlDbType.VarChar).Value = TextBox20.Text
        Dim xyz(1) As String
        a7.array_list(abc, xyz, 0)
        vid = xyz(0)
    End Sub
    Public Sub get_book_id()
        Dim abc As SqlCommand
        abc = New SqlCommand("select bookid from book where name=@bookname")
        abc.Parameters.AddWithValue("@bookname", SqlDbType.VarChar).Value = TextBox2.Text
        Dim xyz(1) As String
        a7.array_list(abc, xyz, 0)
        bookid = xyz(0)
    End Sub
    Public Sub get_company_id()
        Dim itemname As String
        itemname = TextBox1.Text
        companyid = a7.get_company_id(itemname)
    End Sub
    Public Sub check_status()
        If MDIParent1.utype = "ADMIN" Then
            TextBox3.Enabled = True
            TextBox4.Enabled = True
            TextBox5.Enabled = True
            TextBox6.Enabled = True
            ListView1.Enabled = True
            Button1.Enabled = True
        Else
            Dim abc As SqlCommand
            abc = New SqlCommand("select currentdate from softwaredate where currentdate=@dt")
            abc.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
            Dim dstatus As Integer
            dstatus = a7.check_rec(abc)
            If dstatus = 0 Then
                TextBox3.Enabled = False
                TextBox4.Enabled = False
                TextBox5.Enabled = False
                TextBox6.Enabled = False
                ListView1.Enabled = False
                Button1.Enabled = False
            Else
                TextBox3.Enabled = True
                TextBox4.Enabled = True
                TextBox5.Enabled = True
                TextBox6.Enabled = True
                ListView1.Enabled = True
                Button1.Enabled = True
            End If
        End If
    End Sub
End Class