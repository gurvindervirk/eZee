Imports System.Data.SqlClient
Public Class Crate_issue
    Dim a7 As New predefined
    Public companyid, customerid, crateid As Integer
    Public Sub company_add()
        ComboBox1.Items.Clear()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox1)
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyValue = Keys.Escape Then
            DateTimePicker1.Focus()
        End If
        If e.KeyCode = Keys.Down Then
            ListBox1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ListView1.Items.Clear()
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox2.Focus()
        ListBox1.Visible = False
        ListBox2.Visible = False
        TextBox1.Focus()
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
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
            TextBox2.Text = Trim(ListBox2.Items(0))
            customer_id_no()
            ListBox2.Visible = False
            TextBox3.Focus()
        End If
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Down Then
            ListBox1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then Exit Sub
            If ListBox1.Items.Count = 0 Then
                Exit Sub
            End If
            TextBox1.Text = Trim(ListBox1.Items(0))
            get_crate_id()
            list_add()
            ListBox1.Visible = False
            TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        a7.DoConvert(TextBox2)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and cname like @cname +'%'")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox2.Text
        ListBox2.Visible = True
        a7.listbox_add(abc, ListBox2)
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        a7.DoConvert(TextBox1)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from crate where companyid=@companyid and cname like @vname+ '%'order by cname")
        abc.Parameters.AddWithValue("@vname", SqlDbType.VarChar).Value = TextBox1.Text
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        ListBox1.Visible = True
        a7.listbox_add(abc, ListBox1)
    End Sub
    Public Sub customer_id_no()
        Dim itemname As String
        itemname = TextBox2.Text
        customerid = a7.get_customer_id(companyid, itemname)
    End Sub
    Public Sub get_crate_id()
        Dim abc As SqlCommand
        abc = New SqlCommand("select crateid from crate where companyid=@companyid and cname=@cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        Dim xyz(1) As String
        a7.array_list(abc, xyz, 0)
        crateid = xyz(0)
    End Sub
    Private Sub ListBox1_KeyUp1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox1.Text = ListBox1.Text
            get_crate_id()
            list_add()
            ListBox1.Visible = False
            TextBox2.Focus()
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox1.SelectedIndex = 0 Then
                TextBox1.Focus()
            End If
        End If
    End Sub
    Private Sub ListBox2_KeyUp1(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox2.Text = ListBox2.Text
            customer_id_no()
            ListBox2.Visible = False
            TextBox3.Focus()
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox2.SelectedIndex = 0 Then
                TextBox2.Focus()
            End If
        End If
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        e.Handled = a7.trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Enter Then
            Dim abc1 As SqlCommand
            Dim cr As Integer
            cr = 0
            Dim part As String
            part = "ISSUE"
            abc1 = New SqlCommand("insert into crateledger(date,companyid,crateid,customerid,dr,cr,particular)values(@date,@companyid,@crateid,@customerid,@dr,@cr,@particular)")
            abc1.Parameters.AddWithValue("@customerid", SqlDbType.Int).Value = customerid
            abc1.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
            abc1.Parameters.AddWithValue("@dr", SqlDbType.Int).Value = Val(TextBox3.Text)
            abc1.Parameters.AddWithValue("@cr", SqlDbType.Int).Value = cr
            abc1.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = part
            abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            abc1.Parameters.AddWithValue("@crateid", SqlDbType.Int).Value = crateid
            a7.insert_data1(abc1)
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox2.Focus()
            ListBox1.Visible = False
            ListBox2.Visible = False
            list_add()
        End If
        If e.KeyCode = Keys.Escape Then
            TextBox2.Focus()
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Crate_issue_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DateTimePicker1.Value = MDIParent1.entry_date
        If MDIParent1.utype = "ADMIN" Then
            DateTimePicker1.Enabled = True
        Else
            DateTimePicker1.Enabled = False
        End If
        company_add()
        ListBox1.Visible = False
        ListBox2.Visible = False
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        ListView1.Items.Clear()
        TextBox1.Focus()
        ListBox1.Visible = False
        ListBox2.Visible = False
    End Sub
    Public Sub list_add()
        Dim abc As SqlCommand
        Dim crb As Integer
        crb = 0
        abc = New SqlCommand("select a.cname,b.dr,b.voucherid from customer a,crateledger b where a.customerid=b.customerid and b.crateid=@crateid and b.companyid=@companyid and b.dr>@dr and b.particular=@particular and b.date=@date")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@crateid", SqlDbType.Int).Value = crateid
        abc.Parameters.AddWithValue("@dr", SqlDbType.Int).Value = crb
        abc.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = "ISSUE"
        abc.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        a7.list_add1(abc, ListView1)
        Dim totalamount As Integer
        totalamount = 0
        TextBox4.Text = ""
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
            abc = New SqlCommand("delete  from crateledger where voucherid=" & eno)
            a7.delete_data(abc)
            list_add()
            TextBox2.Focus()
        End If
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ComboBox1.Text <> "" Then
            customer_entry_form.DateTimePicker1.Value = DateTimePicker1.Value
            customer_entry_form.companyid = companyid
            customer_entry_form.Show()
        Else
            MsgBox("Select Company Before Customer Entry")
            ComboBox1.Focus()
        End If
    End Sub
End Class