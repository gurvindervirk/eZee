Imports System.Data.SqlClient
Public Class Cashrecipt
    Dim a7 As New Predefined
    Public companyid, customerid As Integer
    Private Sub TextBox4_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        If e.KeyCode = Keys.Enter Then
            If Val(TextBox5.Text) >= 0 Then
                Dim abc1 As SqlCommand
                Dim part, type, desc As String
                part = "Receipt"
                desc = "CR"
                type = "Receipt"
                abc1 = New SqlCommand("insert into voucher(cid,date,amount,particular,voucher_type,account_desc,companyid,sessionid)values(@cid,@date,@amount,@particular,@voucher_type,@account_desc,@companyid,@sessionid)")
                abc1.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = customerid
                abc1.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                abc1.Parameters.AddWithValue("@amount", SqlDbType.Int).Value = Val(TextBox4.Text)
                abc1.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = part
                abc1.Parameters.AddWithValue("@voucher_type", SqlDbType.VarChar).Value = type
                abc1.Parameters.AddWithValue("@account_desc", SqlDbType.VarChar).Value = desc
                abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
                abc1.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
                a7.Insert_data1(abc1)
            Else
                MsgBox("Amount Should be Less then or equal to total balance")
                Exit Sub
            End If
            show_list()
            TextBox5.Text = ""
            TextBox4.Text = ""
            TextBox3.Text = ""
            TextBox2.Text = ""
            TextBox2.Focus()
            ListBox1.Visible = False
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        ListBox1.Visible = True
        a7.DoConvert(TextBox2)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and  cname like @cname+ '%' order by cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox2.Text
        ListBox1.Items.Clear()
        a7.Listbox_add(abc, ListBox1)
    End Sub
    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Down Then
            ListBox1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            If TextBox2.Text = "" Then Exit Sub
            If ListBox1.Items.Count = 0 Then
                Exit Sub
            End If
            TextBox2.Text = Trim(ListBox1.Items(0))
            ListBox1.Visible = False
            customer_id_no()
            get_balance()
            TextBox4.Focus()
        End If
    End Sub
    Private Sub ListBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox2.Text = ListBox1.Text
            customer_id_no()
            get_balance()
            ListBox1.Visible = False
            TextBox4.Focus()
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox1.SelectedIndex = 0 Then
            End If
        End If
    End Sub
    Private Sub Cashrecipt_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Cashrecipt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DateTimePicker1.Value = MDIParent1.entry_date
        If MDIParent1.utype = "ADMIN" Then
            DateTimePicker1.Enabled = True
        Else
            DateTimePicker1.Enabled = False
        End If
        company_add()
        'show_list()
        ListBox1.Visible = False
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        TextBox5.Text = Val(TextBox3.Text) - Val(TextBox4.Text)
    End Sub
    Public Sub Company_add()
        Dim abc As String
        abc = "select company from company"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.Get_company_id(itemname)
        TextBox2.Focus()
        show_list()
    End Sub
    Public Sub Get_balance()
        Dim abc, abc1 As SqlCommand
        Dim account_desc, account_desc1 As String
        Dim sum_dr, sum_cr, balance As Integer
        account_desc = "DR"
        account_desc1 = "CR"
        sum_dr = 0
        sum_cr = 0
        abc = New SqlCommand("select sum(amount) from voucher where companyid=" & companyid & " and sessionid=" & MDIParent1.sessionid & " and cid=" & customerid & " and account_desc='" & account_desc & "'")
        Dim xyz(0), xyz1(0) As String
        a7.Array_list(abc, xyz, 0)
        sum_dr = xyz(0)
        abc1 = New SqlCommand("select sum(amount) from voucher where companyid=" & companyid & " and sessionid=" & MDIParent1.sessionid & " and cid=" & customerid & " and account_desc='" & account_desc1 & "'")
        a7.Array_list(abc1, xyz1, 0)
        sum_cr = xyz1(0)
        balance = sum_dr - sum_cr
        TextBox3.Text = balance
    End Sub
    Public Sub Customer_id_no()
        Dim itemname As String
        itemname = TextBox2.Text
        customerid = a7.Get_customer_id(companyid, itemname)
    End Sub
    Public Sub Show_list()
        Dim abc As SqlCommand
        Dim voucher_type As String
        Dim totalamount As Integer
        totalamount = 0
        voucher_type = "Receipt"
        abc = New SqlCommand("select b.cname,a.amount,a.voucher_id from voucher a,customer b where a.cid=b.customerid and a.companyid=@companyid and a.sessionid=@sessionid and a.voucher_type='" & voucher_type & "' and a.date=@dt1 order by voucher_id ")
        abc.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        a7.List_add1(abc, ListView1)
        Dim rcount, i As Integer
        rcount = ListView1.Items.Count
        i = 0
        Do While i < rcount
            totalamount = Val(totalamount) + Val(ListView1.Items(i).SubItems(2).Text)
            i = i + 1
        Loop
        TextBox1.Text = totalamount
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        Dim abc As SqlCommand
        Dim eno As Integer
        eno = Val(ListView1.SelectedItems.Item(0).SubItems(3).Text)
        If e.KeyCode = Keys.D Then
            abc = New SqlCommand("delete  from voucher where voucher_id=" & eno)
            a7.Delete_data(abc)
            show_list()
            TextBox2.Focus()
        End If
        If e.KeyValue = Keys.S Then
            modify_cash_receipt.Label9.Text = ListView1.SelectedItems.Item(0).SubItems(3).Text
            modify_cash_receipt.companyid = companyid
            modify_cash_receipt.TextBox1.Text = ComboBox1.Text
            modify_cash_receipt.Show()
        End If
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim oFile As System.IO.File
        'Dim oWrite As System.IO.StreamWriter
        'oWrite = oFile.CreateText("c:\temp\sample1.txt")
        'Dim abc, i, lbreak As Integer
        'abc = ListView1.Items.Count
        'i = 0
        'lbreak = 1
        'Dim bspace As String
        'bspace = ""
        'oWrite.Write("(")
        'oWrite.Write(ComboBox1.Text)
        'oWrite.Write(")")
        'oWrite.WriteLine()
        'oWrite.Write(New String(" ", 1))
        'oWrite.Write(New String(" ", 2))
        'oWrite.Write("Cash Receipt:- ")
        'oWrite.Write(DateTimePicker1.Text)
        ''oWrite.Write(" To :- ")
        ''oWrite.Write(DateTimePicker1.Text)
        'oWrite.WriteLine()
        'oWrite.WriteLine(New String("-", 30))
        'oWrite.Write("{0,-16}", ("Cust. Name"))
        'oWrite.Write(New String(" ", 1))
        'oWrite.Write("{0,8}", ("Amount"))
        ''oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", "Arhat")
        ''oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", "Laga")
        ''oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", "Total")
        ''oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", "Prv.Bal")
        ''oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", ("Adv."))
        ''oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", ("G.total"))
        ''oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", ("Cls. Bal"))
        ''oWrite.Write(New String(" ", 1))
        'oWrite.WriteLine()
        'oWrite.WriteLine(New String("-", 30))
        'Do While i < abc
        '    oWrite.Write("{0,-16}", Mid(ListView1.Items(i).SubItems(1).Text, 1, 10))
        '    oWrite.Write(New String(" ", 1))
        '    oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(2).Text, 1, 8))
        '    oWrite.Write(New String(" ", 1))
        '    'oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(3).Text, 1, 8))
        '    'oWrite.Write(New String(" ", 1))
        '    'oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(4).Text, 1, 8))
        '    'oWrite.Write(New String(" ", 1))
        '    'oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(5).Text, 1, 8))
        '    'oWrite.Write(New String(" ", 1))
        '    'oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(6).Text, 1, 8))
        '    'oWrite.Write(New String(" ", 1))
        '    'oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(7).Text, 1, 8))
        '    'oWrite.Write(New String(" ", 1))
        '    'oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(8).Text, 1, 8))
        '    ''oWrite.Write(New String(" ", 1))
        '    ''oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(8).Text, 1, 8))
        '    oWrite.WriteLine()
        '    oWrite.WriteLine(New String("-", 30))
        '    i = i + 1
        'Loop
        'oWrite.Write("{0,-5}", ("Total"))
        'oWrite.Write(New String(" ", 12))
        'oWrite.Write("{0,8}", TextBox1.Text)
        'oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", TextBox2.Text)
        ''oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", TextBox3.Text)
        ''oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", TextBox4.Text)
        ''oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", TextBox5.Text)
        ''oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", TextBox6.Text)
        ''oWrite.Write(New String(" ", 1))
        ''oWrite.Write("{0,8}", TextBox7.Text)
        'oWrite.WriteLine()
        'oWrite.WriteLine(New String("-", 30))
        'oWrite.Close()
        ''System.Diagnostics.Process.Start("c:\printlist.bat")
        'Dim p As New Process
        'Dim info As New ProcessStartInfo
        'info.FileName = "c:\temp\sample1.txt"
        'info.Verb = "print"
        'p.StartInfo = info
        'p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        'p.StartInfo.RedirectStandardOutput = False
        'p.Start()
        cons_receipt()
    End Sub
    Public Sub Cons_receipt()
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        oWrite = oFile.CreateText("c:\temp\sample1.txt")
        oWrite.Write("(")
        oWrite.Write(ComboBox1.Text)
        oWrite.Write(")")
        oWrite.WriteLine()
        oWrite.Write(New String(" ", 1))
        oWrite.Write("Cash Receipt:- ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        Dim abc, i, lbreak As Integer
        abc = ListView1.Items.Count
        i = 0
        lbreak = 1
        Dim bspace As String
        bspace = ""
        i = 0
        lbreak = 1
        Do While i < abc
            oWrite.Write("{0,-12}", Mid(ListView1.Items(i).SubItems(1).Text, 1, 12))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,6}", ListView1.Items(i).SubItems(2).Text)
            oWrite.Write(New String(" ", 1))
            If lbreak = 4 Then
                oWrite.WriteLine()
            End If
            If lbreak = 4 Then
                lbreak = 0
            End If
            lbreak = lbreak + 1
            i = i + 1
        Loop
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.Write("Total Receipts")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,7}", TextBox1.Text)
        oWrite.Close()
        'System.Diagnostics.Process.Start("c:\printlist.bat")
        Dim p As New Process
        Dim info As New ProcessStartInfo With {
            .FileName = "c:\temp\sample1.txt",
            .Verb = "print"
        }
        p.StartInfo = info
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo.RedirectStandardOutput = False
        p.Start()
    End Sub
End Class