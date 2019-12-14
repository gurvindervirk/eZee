Imports System.Data.SqlClient
Public Class Customerwise
    Dim a7 As New predefined
    Public companyid, bookid, cid As Integer
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Escape Then
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
            list_show()
            ListBox1.Visible = False
        End If
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        a7.DoConvert(TextBox2)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and cname like @cname +'%'order by cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox2.Text
        ListBox1.Visible = True
        a7.Listbox_add(abc, ListBox1)
    End Sub
    Private Sub ListBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox2.Text = ListBox1.Text
            customer_id_no()
            list_show()
            ListBox1.Visible = False
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox1.SelectedIndex = 0 Then
            End If
        End If
    End Sub
    Public Sub list_show()
        Dim abc As SqlCommand
        abc = New SqlCommand("select a.bname,b.cname,c.name,a.nag,a.weight,a.rate,a.total from bikri a, customer b,vegetable c where a.cid=b.customerid and a.vegetableid=c.vegetableid and a.sessionid= @sessionid  and a.companyid=@companyid and a.bookid=@bookid and entry_date=@date and a.cid=@cid")
        abc.Parameters.Add("@date", SqlDbType.DateTime).Value = Daily_Bikri_Report.DateTimePicker1.Value.Date
        abc.Parameters.Add("@sessionid", SqlDbType.VarChar).Value = MDIParent1.sessionid
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@bookid", SqlDbType.VarChar).Value = bookid
        abc.Parameters.Add("@cid", SqlDbType.Int).Value = cid
        a7.List_add(abc, ListView1)
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Sub customer_id_no()
        Dim itemname As String
        itemname = TextBox2.Text
        cid = a7.get_customer_id(companyid, itemname)
    End Sub
    Private Sub Customerwise_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ListBox1.Visible = False
    End Sub
End Class