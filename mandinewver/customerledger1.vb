Imports System.Data.SqlClient
Public Class Customerledger1
    Dim a7 As New predefined
    Public companyid, customerid As Integer
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
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
            TextBox1.Text = Trim(ListBox1.Items(0))
            Customer_id_no()
            ListBox1.Visible = False
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        a7.DoConvert(TextBox1)
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname from customer where companyid=@companyid and cname like @cname + '%'order by cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        ListBox1.Visible = True
        a7.Listbox_add(abc, ListBox1)
    End Sub
    Private Sub ListBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox1.Text = ListBox1.Text
            Customer_id_no()
            ListBox1.Visible = False
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox1.SelectedIndex = 0 Then
            End If
        End If
    End Sub
    Public Sub Show_ledger()
        Opening_balance()
        Dim zero As Integer
        zero = 0
        Dim abc As SqlCommand
        'abc = New SqlCommand("select date,sum(amount),voucher_type,sessionid,cid from voucher where cid=@pid and date>=@dt1 and date<=@dt2 and sessionid=@session and voucher_type<>@vtype group by date,voucher_type,sessionid,cid order by date,voucher_type")
        abc = New SqlCommand("select date,sum(amount),voucher_type,sessionid,cid from voucher where cid=@pid and date>=@dt1 and date<=@dt2 and sessionid=@session and amount>@zero group by date,voucher_type,sessionid,cid order by date,voucher_type")
        abc.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@dt2", SqlDbType.DateTime).Value = DateTimePicker2.Value.Date
        abc.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
        abc.Parameters.Add("@pid", SqlDbType.Int).Value = customerid
        abc.Parameters.Add("@zero", SqlDbType.Int).Value = zero
        abc.Parameters.Add("@vtype", SqlDbType.VarChar).Value = "Dummy"
        a7.Vega_ledger_add(abc, ListView1, Val(TextBox2.Text))
    End Sub
    Public Sub Opening_balance()
        Dim abc, abc1 As SqlCommand
        Dim desc, desc1 As String
        desc = "DR"
        desc1 = "CR"
        abc = New SqlCommand("select sum(amount)from voucher where account_desc=@desc and cid=@pid and date<@dt1 and sessionid=@session")
        abc.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@pid", SqlDbType.Int).Value = customerid
        abc.Parameters.Add("@desc", SqlDbType.VarChar).Value = desc
        abc.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
        abc1 = New SqlCommand("select sum(amount)from voucher where account_desc=@desc and cid=@pid and date<@dt1 and sessionid=@session")
        abc1.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc1.Parameters.Add("@pid", SqlDbType.Int).Value = customerid
        abc1.Parameters.Add("@desc", SqlDbType.VarChar).Value = desc1
        abc1.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
        Dim xyz(0), xyz1(0) As String
        a7.array_list(abc, xyz, 0)
        a7.array_list(abc1, xyz1, 0)
        Dim dr_balance, cr_balance As Long
        dr_balance = xyz(0)
        cr_balance = xyz1(0)
        If dr_balance > cr_balance Then
            TextBox2.Text = dr_balance - cr_balance
        ElseIf cr_balance > dr_balance Then
            TextBox2.Text = cr_balance - dr_balance
        ElseIf cr_balance = dr_balance Then
            TextBox2.Text = ""
        End If
    End Sub
    Private Sub Customerledger1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Customerledger1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Company_add()
        ListBox1.Visible = False
    End Sub
    Public Sub Company_add()
        Dim abc As String
        abc = "select company from company"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.S Then
            If ListView1.SelectedItems.Item(0).SubItems(5).Text = "Bill" Then
                bill_transfer.bill_date = ListView1.SelectedItems(0).SubItems(1).Text
                bill_transfer.TextBox1.Text = ListView1.SelectedItems(0).SubItems(1).Text
                bill_transfer.TextBox2.Text = TextBox1.Text
                bill_transfer.customerid = customerid
                bill_transfer.companyid = companyid
                bill_transfer.list_show()
                bill_transfer.Show()
            End If
            If ListView1.SelectedItems.Item(0).SubItems(5).Text = "DRvoucher" Then
                transfer_advances.Label6.Text = TextBox1.Text
                transfer_advances.receipt_date = ListView1.SelectedItems(0).SubItems(1).Text
                transfer_advances.customerid = customerid
                transfer_advances.companyid = companyid
                transfer_advances.Show()
            End If
            If ListView1.SelectedItems.Item(0).SubItems(5).Text = "Receipt" Then
                billtransfer.Label6.Text = TextBox1.Text
                billtransfer.receipt_date = ListView1.SelectedItems(0).SubItems(1).Text
                billtransfer.customerid = customerid
                billtransfer.companyid = companyid
                billtransfer.ListView1.Focus()
                billtransfer.Show()
            End If
        End If
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim location As String = IO.Directory.GetParent(Application.ExecutablePath).FullName
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        'oWrite = oFile.CreateText("c:\temp\sample1.txt")
        oWrite = oFile.CreateText(location & "\" & "sample1.txt")
        Dim abc, i, lbreak As Integer
        abc = ListView1.Items.Count
        i = 0
        lbreak = 1
        Dim bspace As String
        bspace = ""
        oWrite.Write("(")
        oWrite.Write(ComboBox1.Text)
        oWrite.Write(")")
        oWrite.WriteLine()
        oWrite.Write(New String(" ", 1))
        oWrite.Write(TextBox1.Text)
        oWrite.Write(New String(" ", 2))
        oWrite.Write("Ledger from Date :- ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.Write(" to Date :- ")
        oWrite.Write(DateTimePicker2.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.Write("Opn. Bal.")
        oWrite.Write(New String(" ", 2))
        oWrite.Write(TextBox2.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.Write("{0,-10}", ("Date"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,-28}", ("Narration"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Bill")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Advances")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Wasuli")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Balance")
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        Do While i < abc
            oWrite.Write("{0,-10}", Mid(ListView1.Items(i).SubItems(1).Text, 1, 10))
            oWrite.Write(New String(" ", 1))
            If ListView1.Items(i).SubItems(7).Text = " " Then
                oWrite.Write("{0,-28}", Mid(ListView1.Items(i).SubItems(7).Text, 1, 28))
                oWrite.Write(New String(" ", 1))
            Else
                oWrite.Write("{0,-70}", Mid(ListView1.Items(i).SubItems(7).Text, 1, 70))
                oWrite.Write(New String(" ", 1))
            End If

            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(2).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(3).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(4).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(6).Text)
            oWrite.WriteLine()
            oWrite.WriteLine(New String("-", 80))
            i = i + 1
        Loop
        oWrite.Close()
        Dim p As New Process
        'info.FileName = "c:\temp\sample1.txt"
        Dim info As New ProcessStartInfo With {
            .FileName = location & "\" & "sample1.txt",
            .Verb = "print"
        }
        p.StartInfo = info
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo.RedirectStandardOutput = False
        p.Start()
        ' System.Diagnostics.Process.Start("c:\printlist.bat")
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
    End Sub
    Public Sub Customer_id_no()
        Dim itemname As String
        itemname = TextBox1.Text
        customerid = a7.get_customer_id(companyid, itemname)
    End Sub
    Private Sub DateTimePicker2_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker2.CloseUp
        Show_ledger()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim location As String = IO.Directory.GetParent(Application.ExecutablePath).FullName
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        'oWrite = oFile.CreateText("c:\temp\sample1.txt")
        oWrite = oFile.CreateText(location & "\" & "sample1.txt")
        Dim abc, i, lbreak As Integer
        abc = ListView1.Items.Count
        i = 0
        lbreak = 1
        Dim bspace As String
        bspace = ""
        oWrite.Write("(")
        oWrite.Write(ComboBox1.Text)
        oWrite.Write(")")
        oWrite.WriteLine()
        oWrite.Write(New String(" ", 1))
        oWrite.Write(TextBox1.Text)
        oWrite.Write(New String(" ", 2))
        oWrite.Write("Ledger from Date :- ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.Write(" to Date :- ")
        oWrite.Write(DateTimePicker2.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.Write("Opn. Bal.")
        oWrite.Write(New String(" ", 2))
        oWrite.Write(TextBox2.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.Write("{0,-10}", ("Date"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,-28}", ("Narration"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Bill")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Advances")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Wasuli")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Balance")
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        Do While i < abc
            oWrite.Write("{0,-10}", Mid(ListView1.Items(i).SubItems(1).Text, 1, 10))
            oWrite.Write(New String(" ", 1))
            If ListView1.Items(i).SubItems(7).Text = " " Then
                oWrite.Write("{0,-28}", Mid(ListView1.Items(i).SubItems(7).Text, 1, 28))
                oWrite.Write(New String(" ", 1))
            Else
                oWrite.Write("{0,-70}", Mid(ListView1.Items(i).SubItems(7).Text, 1, 70))
                oWrite.Write(New String(" ", 1))
            End If

            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(2).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(3).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(4).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(6).Text)
            oWrite.WriteLine()
            oWrite.WriteLine(New String("-", 80))
            i = i + 1
        Loop
        oWrite.Close()
        Shell("print file /" & location & "\" & " sample1.txt")
        'Dim p As New Process
        'Dim info As New ProcessStartInfo
        ''info.FileName = "c:\temp\sample1.txt"
        'info.FileName = location & "\" & "sample1.txt"
        'info.Verb = "print"
        'p.StartInfo = info
        'p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        'p.StartInfo.RedirectStandardOutput = False
        'p.Start()
        '' System.Diagnostics.Process.Start("c:\printlist.bat")
    End Sub
End Class