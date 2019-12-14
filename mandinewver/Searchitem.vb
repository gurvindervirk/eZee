Imports System.Data.SqlClient
Public Class Searchitem
    Dim a7 As New predefined
    Public companyid, bookid, itemid As Integer
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
            get_item_id()
            list_show()
            ListBox1.Visible = False
        End If
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        a7.DoConvert(TextBox2)
        Dim abc As SqlCommand
        abc = New SqlCommand("select name from vegetable where name like @vname + '%' order by name")
        abc.Parameters.AddWithValue("@vname", SqlDbType.VarChar).Value = TextBox2.Text
        ListBox1.Visible = True
        a7.Listbox_add(abc, ListBox1)
    End Sub
    Private Sub ListBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox2.Text = ListBox1.Text
            get_item_id()
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
        abc = New SqlCommand("select a.bname,b.cname,c.name,a.nag,a.weight,a.rate,a.total from bikri a, customer b,vegetable c where a.cid=b.customerid and a.vegetableid=c.vegetableid and a.sessionid= @sessionid  and a.companyid=@companyid and a.bookid=@bookid and entry_date=@date and a.vegetableid=@vid")
        abc.Parameters.Add("@date", SqlDbType.DateTime).Value = Daily_Bikri_Report.DateTimePicker1.Value.Date
        abc.Parameters.Add("@sessionid", SqlDbType.VarChar).Value = MDIParent1.sessionid
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@bookid", SqlDbType.VarChar).Value = bookid
        abc.Parameters.Add("@vid", SqlDbType.Int).Value = itemid
        a7.List_add(abc, ListView1)
    End Sub
    Private Sub Searchitem_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Sub get_item_id()
        Dim abc As SqlCommand
        abc = New SqlCommand("select vegetableid from vegetable where name=@vegname")
        abc.Parameters.AddWithValue("@vegname", SqlDbType.VarChar).Value = TextBox2.Text
        Dim xyz(1) As String
        a7.array_list(abc, xyz, 0)
        itemid = xyz(0)
    End Sub
    Private Sub Searchitem_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ListBox1.Visible = False
    End Sub
End Class