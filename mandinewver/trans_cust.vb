Imports System.Data.SqlClient
Public Class trans_cust
    Dim a7 As New predefined
    Public companyid, customerid, voucherid, entryno As Integer
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Down Then
            ListBox2.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            If ListBox2.Items.Count = 0 Then
                Exit Sub
            End If
            TextBox3.Text = Trim(ListBox2.Items(0))
            customer_id_no()
            ListBox2.Visible = False
            Button1.Focus()
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        a7.DoConvert(TextBox3)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and cname like @cname +'%'order by cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox3.Text
        ListBox2.Visible = True
        a7.Listbox_add(abc, ListBox2)
    End Sub
    Private Sub ListBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox3.Text = ListBox2.Text
            customer_id_no()
            ListBox2.Visible = False
            Button1.Focus()
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox2.SelectedIndex = 0 Then
                TextBox3.Focus()
            End If
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim row_count, i As Integer
        Dim abc1, abc2 As SqlCommand
        row_count = bill_transfer.ListView1.Items.Count
        i = 0
        Dim eno As Integer
        Do While i <= row_count - 1
            If bill_transfer.ListView1.Items(i).Checked = True Then
                eno = Val(bill_transfer.ListView1.Items(i).Text)
                entryno = eno
                get_entry_no()
                Dim cnn As New SqlConnection
                Dim adapter As New SqlDataAdapter
                Dim tran As SqlTransaction = Nothing
                cnn = New SqlConnection(a7.connetionstring)
                Try
                    cnn.Open()
                    tran = cnn.BeginTransaction("Transaction1")
                    abc1 = New SqlCommand("update bikri set cid=@customerid where entry_no=@entryno")
                    abc1.Parameters.AddWithValue("@customerid", SqlDbType.Int).Value = customerid
                    abc1.Parameters.AddWithValue("@entryno", SqlDbType.Int).Value = entryno
                    abc1.Connection = cnn
                    abc1.Transaction = tran
                    adapter.UpdateCommand = abc1
                    adapter.UpdateCommand.ExecuteNonQuery()
                    tran.Save("Save")

                    abc2 = New SqlCommand("update voucher set cid=@customerid where voucher_id=@voucherid")
                    abc2.Parameters.AddWithValue("@customerid", SqlDbType.Int).Value = customerid
                    abc2.Parameters.AddWithValue("@voucherid", SqlDbType.Int).Value = voucherid

                    abc2.Connection = cnn
                    abc2.Transaction = tran
                    adapter.UpdateCommand = abc2
                    adapter.UpdateCommand.ExecuteNonQuery()
                    tran.Save("Save1")
                    Dim abc3 As SqlCommand
                    abc3 = New SqlCommand("insert into transfer_bills(companyid,sessionid,fromcustid,tocustid,transferdate,entryno,voucherid)values(@companyid,@sessionid,@fromcustid,@tocust,@transferdate,@entryno,@voucherid)")
                    abc3.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
                    abc3.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
                    abc3.Parameters.AddWithValue("@fromcustid", SqlDbType.VarChar).Value = Customerledger1.TextBox1.Text
                    abc3.Parameters.AddWithValue("@tocust", SqlDbType.VarChar).Value = TextBox3.Text
                    abc3.Parameters.AddWithValue("@transferdate", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                    abc3.Parameters.AddWithValue("@entryno", SqlDbType.Int).Value = entryno
                    abc3.Parameters.AddWithValue("@voucherid", SqlDbType.Int).Value = voucherid
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
            End If
            i = i + 1
        Loop
        Me.Close()
    End Sub
    Private Sub trans_cust_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        bill_transfer.Close()
        Customerledger1.Show_ledger()
    End Sub
    Private Sub trans_cust_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Sub customer_id_no()
        Dim itemname As String
        itemname = TextBox3.Text
        customerid = a7.get_customer_id(companyid, itemname)
    End Sub
    Public Sub get_entry_no()
        Dim abc As SqlCommand
        Dim xyz(0) As String
        abc = New SqlCommand("select voucher_id from invoice_voucher where invoice_no=@invoiceno")
        abc.Parameters.AddWithValue("@invoiceno", SqlDbType.Int).Value = entryno
        a7.array_list(abc, xyz, 0)
        voucherid = xyz(0)
    End Sub
    Private Sub trans_cust_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        companyid = Customerledger1.companyid
    End Sub
End Class
