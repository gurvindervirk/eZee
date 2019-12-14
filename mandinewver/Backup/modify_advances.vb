Imports System.Data.SqlClient
Public Class modify_advances
    Dim a7 As New predefined
    Public companyid, customerid As Integer
    Public voucherid, entryid As Integer
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            If ListBox1.Items.Count = 0 Then
                Exit Sub
            End If
            TextBox1.Text = Trim(ListBox1.Items(0))
            customer_id_no()
            ListBox1.Visible = False
            TextBox3.Focus()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        ListBox1.Visible = True
        a7.DoConvert(TextBox1)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and  cname like @cname +'%'order by cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        ListBox1.Items.Clear()
        a7.listbox_add(abc, ListBox1)
    End Sub
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        e.Handled = a7.trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then Exit Sub
        If MsgBox("Update Record.....?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Vega 3.5") = MsgBoxResult.Yes Then
            Dim abc1 As SqlCommand
            abc1 = New SqlCommand("update voucher set cid=@cid,amount=@amount,particular=@part where voucher_id=@voucherid")
            abc1.Parameters.AddWithValue("@voucherid", SqlDbType.Int).Value = Val(Label7.Text)
            abc1.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid
            abc1.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(TextBox2.Text)
            abc1.Parameters.AddWithValue("@part", SqlDbType.VarChar).Value = TextBox3.Text
            a7.update_data(abc1)
            Me.Close()
        End If
    End Sub
    Private Sub Customer_Entry_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If advances_entry.Visible = True Then
            advances_entry.show_list()
        End If
        If Customer_Entry.Visible = True Then
            Customer_Entry.show_list()
        End If
    End Sub
    Private Sub Customer_Entry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        show_data()
        ListBox1.Visible = False
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        TextBox1.Focus()
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Public Sub show_data()
        Dim abc As SqlCommand
        abc = New SqlCommand("select a.cname,b.date,b.amount,b.cid from customer a ,voucher b where a.customerid=b.cid and b.voucher_id=@voucherid")
        abc.Parameters.AddWithValue("@voucherid", SqlDbType.Int).Value = Val(voucherid)
        Dim xyz(5) As String
        a7.array_list(abc, xyz, 0)
        TextBox1.Text = xyz(0)
        DateTimePicker1.Value = xyz(1)
        customerid = xyz(3)
        TextBox2.Text = xyz(2)
        TextBox1.Focus()
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        a7.DoConvert(TextBox1)
    End Sub
    Public Sub customer_id_no()
        Dim itemname As String
        itemname = TextBox1.Text
        customerid = a7.get_customer_id(companyid, itemname)
    End Sub
End Class