Imports System.Data.SqlClient
Public Class crateledger
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
            customer_id_no()
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
        a7.listbox_add(abc, ListBox1)
    End Sub
    Private Sub ListBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox1.Text = ListBox1.Text
            customer_id_no()
            ListBox1.Visible = False
        End If
        If e.KeyCode = Keys.Up Then
            If ListBox1.SelectedIndex = 0 Then
            End If
        End If
    End Sub
    Public Sub show_ledger()
        opening_balance()
        Dim abc As SqlCommand
        abc = New SqlCommand("select a.date,b.cname,a.dr,a.cr,a.particular from crateledger a,crate b where a.customerid=@pid and date>=@dt1 and date<=@dt2 and a.crateid=b.crateid order by a.date,a.particular")
        abc.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@dt2", SqlDbType.DateTime).Value = DateTimePicker2.Value.Date
        abc.Parameters.Add("@pid", SqlDbType.Int).Value = customerid
        a7.crate_ledger_add(abc, ListView1, Val(TextBox2.Text))
    End Sub
    Public Sub opening_balance()
        Dim abc As SqlCommand
        abc = New SqlCommand("select sum(dr),sum(cr)from crateledger where customerid=@pid and date<@dt1")
        abc.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@pid", SqlDbType.Int).Value = customerid
        Dim xyz(1) As String
        a7.array_list(abc, xyz, 0)
        Dim dr_balance, cr_balance As Long
        dr_balance = xyz(0)
        cr_balance = xyz(1)
        If dr_balance > cr_balance Then
            TextBox2.Text = dr_balance - cr_balance
        ElseIf cr_balance > dr_balance Then
            TextBox2.Text = cr_balance - dr_balance
        ElseIf cr_balance = dr_balance Then
            TextBox2.Text = ""
        End If
    End Sub
    Private Sub customerledger1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub customerledger1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
        ListBox1.Visible = False
    End Sub
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox1)
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
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
        oWrite.WriteLine(New String("-", 62))
        oWrite.Write("Opn. Bal.")
        oWrite.Write(New String(" ", 2))
        oWrite.Write(TextBox2.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 62))
        oWrite.Write("{0,-10}", ("Date"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,10}", "Marka")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Issue")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Receipt")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,10}", "Particular")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,10}", "Balance")
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 62))
        oWrite.WriteLine()
        Do While i < abc
            oWrite.Write("{0,-10}", Mid(ListView1.Items(i).SubItems(1).Text, 1, 10))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,10}", ListView1.Items(i).SubItems(2).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(3).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,10}", ListView1.Items(i).SubItems(4).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(5).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,10}", ListView1.Items(i).SubItems(6).Text)
            oWrite.WriteLine()
            i = i + 1
        Loop
        oWrite.WriteLine(New String("-", 62))
        oWrite.Close()
        Dim p As New Process
        Dim info As New ProcessStartInfo
        'info.FileName = "c:\temp\sample1.txt"
        info.FileName = location & "\" & "sample1.txt"
        info.Verb = "print"
        p.StartInfo = info
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo.RedirectStandardOutput = False
        p.Start()
        'System.Diagnostics.Process.Start("c:\printlist.bat")
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
    End Sub
    Public Sub customer_id_no()
        Dim itemname As String
        itemname = TextBox1.Text
        customerid = a7.get_customer_id(companyid, itemname)
    End Sub
    Private Sub DateTimePicker2_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker2.CloseUp
        show_ledger()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
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
        oWrite.WriteLine(New String("-", 62))
        oWrite.Write("Opn. Bal.")
        oWrite.Write(New String(" ", 2))
        oWrite.Write(TextBox2.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 62))
        oWrite.Write("{0,-10}", ("Date"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,10}", "Marka")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Issue")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Receipt")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,10}", "Particular")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,10}", "Balance")
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 62))
        oWrite.WriteLine()
        Do While i < abc
            oWrite.Write("{0,-10}", Mid(ListView1.Items(i).SubItems(1).Text, 1, 10))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,10}", ListView1.Items(i).SubItems(2).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(3).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,10}", ListView1.Items(i).SubItems(4).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(5).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,10}", ListView1.Items(i).SubItems(6).Text)
            oWrite.WriteLine()
            i = i + 1
        Loop
        oWrite.WriteLine(New String("-", 62))
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
        ''System.Diagnostics.Process.Start("c:\printlist.bat")
    End Sub
End Class