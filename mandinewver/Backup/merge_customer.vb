Imports System.Data.SqlClient
Public Class merge_customer
    Dim a7 As New predefined
    Dim companyid As Integer
    Dim customerid, customerid1 As Integer
    Public Sub show_customer()
        Dim abc As SqlCommand
        ListBox1.Items.Clear()
        abc = New SqlCommand("Select cname from customer where companyid=@companyid")
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        a7.listbox_add(abc, ListBox1)
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        Dim abc1, abc5, abc6, abc7, abc8 As SqlCommand
        Dim cratedesc As String
        If CheckBox2.Checked = True Then
            cratedesc = "YES"
        Else
            cratedesc = "NO"
        End If
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Enter Then
            If TextBox2.Text = "" Then Exit Sub
            TextBox2.Text = Trim(ListBox2.Items(0))
            customer_id_no1()
            ListBox2.Visible = False
            Dim cnn As New SqlConnection
            Dim adapter As New SqlDataAdapter
            Dim tran As SqlTransaction = Nothing
            cnn = New SqlConnection(a7.connetionstring)
            Try
                cnn.Open()
                If CheckBox1.Checked = True Then
                    tran = cnn.BeginTransaction("Transaction1")
                    abc1 = New SqlCommand("update bikri set cid=@customerid1 where cid=@customerid and companyid=@companyid")
                    abc1.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                    abc1.Parameters.Add("@customerid", SqlDbType.Int).Value = customerid
                    abc1.Parameters.Add("@customerid1", SqlDbType.Int).Value = customerid1
                    abc1.Connection = cnn
                    abc1.Transaction = tran
                    adapter.UpdateCommand = abc1
                    adapter.UpdateCommand.ExecuteNonQuery()
                    tran.Save("Save")

                    abc5 = New SqlCommand("update voucher set cid=@customerid1 where cid=@customerid and companyid=@companyid")
                    abc5.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                    abc5.Parameters.Add("@customerid", SqlDbType.Int).Value = customerid
                    abc5.Parameters.Add("@customerid1", SqlDbType.Int).Value = customerid1
                    abc5.Connection = cnn
                    abc5.Transaction = tran
                    adapter.UpdateCommand = abc5
                    adapter.UpdateCommand.ExecuteNonQuery()
                    tran.Save("Save1")
                End If
                If CheckBox2.Checked = True Then
                    abc6 = New SqlCommand("update crateledger set customerid=@customerid1 where customerid=@customerid and companyid=@companyid")
                    abc6.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                    abc6.Parameters.Add("@customerid", SqlDbType.Int).Value = customerid
                    abc6.Parameters.Add("@customerid1", SqlDbType.Int).Value = customerid1
                    abc6.Connection = cnn
                    abc6.Transaction = tran
                    adapter.UpdateCommand = abc6
                    adapter.UpdateCommand.ExecuteNonQuery()
                    tran.Save("Save2")
                End If
                If CheckBox3.Checked = True Then
                    abc7 = New SqlCommand("delete from customer where customerid=@customerid and companyid=@companyid")
                    abc7.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                    abc7.Parameters.Add("@customerid", SqlDbType.Int).Value = customerid
                    abc7.Connection = cnn
                    abc7.Transaction = tran
                    adapter.DeleteCommand = abc7
                    adapter.DeleteCommand.ExecuteNonQuery()
                    tran.Save("Save3")
                End If
                abc8 = New SqlCommand("insert into mergebikri(companyid,fromcust,tocust,date,cratedesc)values(@companyid,@customerid,@customerid1,@date,@cratedesc)")
                abc8.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                abc8.Parameters.Add("@customerid", SqlDbType.VarChar).Value = TextBox1.Text
                abc8.Parameters.Add("@customerid1", SqlDbType.VarChar).Value = TextBox2.Text
                abc8.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = Date.Today
                abc8.Parameters.AddWithValue("@cratedesc", SqlDbType.VarChar).Value = cratedesc
                abc8.Connection = cnn
                abc8.Transaction = tran
                adapter.InsertCommand = abc8
                adapter.InsertCommand.ExecuteNonQuery()
                tran.Save("Save4")
                tran.Commit()
            Catch ex As Exception
                MsgBox(ex.Message)
                tran.Rollback()
            Finally
                cnn.Close()
            End Try
            TextBox1.Text = ""
            TextBox2.Text = ""
            ListBox1.Visible = False
            ListBox2.Visible = False
            CheckBox1.Checked = True
            CheckBox2.Checked = True
            CheckBox3.Checked = True
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
    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        a7.DoConvert(TextBox2)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and  cname like @cname + '%' order by cname")
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@cname", SqlDbType.VarChar).Value = TextBox2.Text
        ListBox2.Visible = True
        ListBox2.Items.Clear()
        a7.listbox_add(abc, ListBox2)
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
    Private Sub change_cname_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ListBox1.Visible = False
        ListBox2.Visible = False
        company_add()
        CheckBox1.Checked = True
        CheckBox2.Checked = True
        CheckBox3.Checked = True
    End Sub
    Public Sub customer_id_no()
        Dim itemname As String
        itemname = TextBox1.Text
        customerid = a7.get_customer_id(companyid, itemname)
    End Sub
    Public Sub customer_id_no1()
        Dim itemname As String
        itemname = TextBox2.Text
        customerid1 = a7.get_customer_id(companyid, itemname)
    End Sub
    Private Sub ListBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox2.Text = ListBox2.Text
            customer_id_no()
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox2.SelectedIndex = 0 Then
                TextBox2.Focus()
            End If
        End If
    End Sub
    Private Sub ListBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox1.Text = ListBox1.Text
            customer_id_no()
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox1.SelectedIndex = 0 Then
                TextBox1.Focus()
            End If
        End If
    End Sub
End Class