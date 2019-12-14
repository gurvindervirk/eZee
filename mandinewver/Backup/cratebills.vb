Imports System.Data.SqlClient
Public Class cratebills
    Dim a7 As New predefined
    Dim companyid As Integer
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox1)
    End Sub
    Private Sub Bills_report_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Bills_report_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
    End Sub
    Public Sub list_add()
        Dim abc As SqlCommand
        Dim particular As String
        particular = "ISSUE"
        abc = New SqlCommand("select c.customerid,b.cname,sum(c.dr) from customer b,crateledger c where  c.customerid=b.customerid and c.date=@date and c.companyid=@companyid and c.particular=@particular group by c.customerid,b.cname order by b.cname")
        abc.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@particular", SqlDbType.VarChar).Value = particular
        a7.list_add(abc, ListView1)
        outstanding_list()
        show_total()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Public Sub print_bill()
        Dim location As String = IO.Directory.GetParent(Application.ExecutablePath).FullName
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        'oWrite = oFile.CreateText("c:\temp\sample.txt")
        oWrite = oFile.CreateText(location & "\" & "sample1.txt")
        Dim abc, i, balance, balance1 As Integer
        abc = ListView1.Items.Count
        i = 0
        Do While i < abc
            If ListView1.Items(i).Checked = True Then
                oWrite.WriteLine("{0,20}{1,-20}", New String(" ", 20), ComboBox1.Text)
                oWrite.WriteLine("{0,-30}{1,10}", ListView1.Items(i).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"))
                oWrite.WriteLine(New String("-", 40))
                oWrite.WriteLine("{0,-10}{1,30}", "Marka", "Crate ")
                oWrite.WriteLine(New String("-", 40))
                a7.print_crate(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite)
                oWrite.WriteLine(New String("-", 40))
                oWrite.WriteLine("{0,10}{1,30}", "Total     ", Val(ListView1.Items(i).SubItems(2).Text))
                oWrite.WriteLine("{0,40}", New String("-", 40))
                If Val(ListView1.Items(i).SubItems(3).Text) > 0 Then
                    oWrite.WriteLine("{0,10}{1,30}", "Prv.Balance", Val(ListView1.Items(i).SubItems(3).Text))
                End If
                If Val(ListView1.Items(i).SubItems(4).Text) > 0 Then
                    oWrite.WriteLine("{0,10}{1,30}", "Advances -", Val(ListView1.Items(i).SubItems(4).Text))
                End If
                oWrite.WriteLine("{0,10}{1,29}", "Grand Total", Val(ListView1.Items(i).SubItems(5).Text))
                oWrite.WriteLine("{0,13}{1,15}{2,12}", "-------------", "Marka Wise Bal.", "------------")
                a7.print_crate_bal(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite)
                oWrite.WriteLine("{0,40}", New String("-", 40))
                oWrite.WriteLine()
                oWrite.WriteLine()
            End If
            i = i + 1
        Loop
        oWrite.Close()
        Shell("print file /" & location & "\" & " sample1.txt")
        'Dim p As New Process
        'Dim info As New ProcessStartInfo
        'info.FileName = "c:\temp\sample.txt"
        'info.Verb = "print"
        'p.StartInfo = info
        'p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        'p.StartInfo.RedirectStandardOutput = False
        'p.Start()
        'System.Diagnostics.Process.Start("c:\printfile.bat")
    End Sub
    Public Sub print_bill_laser()
        Dim location As String = IO.Directory.GetParent(Application.ExecutablePath).FullName
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        'oWrite = oFile.CreateText("c:\temp\sample.txt")
        oWrite = oFile.CreateText(location & "\" & "sample1.txt")
        Dim abc, i, balance, balance1, dd1, dd2 As Integer
        abc = ListView1.Items.Count
        i = 0
        dd1 = 0
        dd2 = abc Mod 2

        If dd2 = 1 Then
            abc = abc - 1
        End If
        Do While i < abc
            If ListView1.Items(i).Checked = True Then
                oWrite.WriteLine("{0,20}{1,-20}{2,5},{3,20}{4,-20}", New String(" ", 20), ComboBox1.Text, New String(" ", 5), New String(" ", 20), ComboBox1.Text)
                oWrite.WriteLine("{0,-30}{1,10}{2,5}{3,-30}{4,10}", ListView1.Items(i).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"), New String(" ", 5), ListView1.Items(i + 1).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"))
                oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                oWrite.WriteLine("{0,-10}{1,30}{2,5}{3,-10}{4,30}", "Marka", "Crate ", New String(" ", 5), "Marka", "Crate ")
                oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                a7.print_crate_laser(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite, ListView1.Items(i + 1).Text)
                oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                oWrite.WriteLine("{0,10}{1,30}{2,5}{3,10}{4,30}", "Total     ", Val(ListView1.Items(i).SubItems(2).Text), New String(" ", 5), "Total     ", Val(ListView1.Items(i + 1).SubItems(2).Text))
                oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                oWrite.WriteLine("{0,10}{1,30}{2,5}{3,10}{4,30}", "Prv.Balc. ", Val(ListView1.Items(i).SubItems(3).Text), New String(" ", 5), "Prv.Balc. ", Val(ListView1.Items(i + 1).SubItems(3).Text))
                oWrite.WriteLine("{0,10}{1,30}{2,5}{3,10}{4,30}", "Advances -", Val(ListView1.Items(i).SubItems(4).Text), New String(" ", 5), "Advances -", Val(ListView1.Items(i + 1).SubItems(4).Text))
                oWrite.WriteLine("{0,10}{1,29}{2,5}{3,10}{4,29}", "Grand Total", Val(ListView1.Items(i).SubItems(5).Text), New String(" ", 5), "Grand Total", Val(ListView1.Items(i + 1).SubItems(5).Text))
                'oWrite.WriteLine("{0,13}{1,15}{2,12}", "-------------", "Marka Wise Bal.", "------------")
                'a7.print_crate_bal(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite)
                oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                Dim abc3 As SqlCommand
                Dim crb As Integer
                crb = 0
                abc3 = New SqlCommand("select cr,date from crateledger  where customerid=@customerid and companyid=@companyid and cr>@cr and date<=@date and particular=@particular order by voucherid desc")
                abc3.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
                abc3.Parameters.AddWithValue("@customerid", SqlDbType.Int).Value = ListView1.Items(i).Text
                abc3.Parameters.AddWithValue("@cr", SqlDbType.Int).Value = crb
                abc3.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                abc3.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = "RECEIPT"
                Dim xyz6(2) As String
                a7.array_list(abc3, xyz6, 0)

                Dim abc4 As SqlCommand
                Dim crb1 As Integer
                crb = 0
                abc4 = New SqlCommand("select cr,date from crateledger  where customerid=@customerid and companyid=@companyid and cr>@cr and date<=@date and particular=@particular order by voucherid desc ")
                abc4.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
                abc4.Parameters.AddWithValue("@customerid", SqlDbType.Int).Value = ListView1.Items(i + 1).Text
                abc4.Parameters.AddWithValue("@cr", SqlDbType.Int).Value = crb1
                abc4.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                abc4.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = "RECEIPT"
                Dim xyz7(2) As String
                a7.array_list(abc4, xyz7, 0)
                oWrite.WriteLine("{0,-12}{1,9}{2,8}{3,10}{4,5}{5,-12}{6,8}{7,8}{8,10}", "Received CR.", Format(Val(xyz6(0)), "0.00"), " On Date ", xyz6(1), New String(" ", 5), "Received CR.", Format(Val(xyz7(0)), "0.00"), " On Date ", xyz7(1))
                'oWrite.WriteLine()
                oWrite.WriteLine()
                oWrite.WriteLine()
                oWrite.WriteLine()
                oWrite.WriteLine()
                oWrite.WriteLine()
            End If
            i = i + 2
        Loop
        If dd2 = 1 Then
            oWrite.WriteLine("{0,20}{1,-20}", New String(" ", 20), ComboBox1.Text)
            oWrite.WriteLine("{0,-30}{1,10}", ListView1.Items(abc).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"))
            oWrite.WriteLine(New String("-", 40))
            oWrite.WriteLine("{0,-10}{1,30}", "Marka", "Crate ")
            oWrite.WriteLine(New String("-", 40))
            a7.print_crate(ListView1.Items(abc).Text, DateTimePicker1, companyid, oWrite)
            oWrite.WriteLine(New String("-", 40))
            oWrite.WriteLine("{0,10}{1,30}", "Total     ", Val(ListView1.Items(i).SubItems(2).Text))
            oWrite.WriteLine("{0,40}", New String("-", 40))
            If Val(ListView1.Items(abc).SubItems(3).Text) > 0 Then
                oWrite.WriteLine("{0,10}{1,30}", "Prv.Balc.", Val(ListView1.Items(i).SubItems(3).Text))
            End If
            If Val(ListView1.Items(abc).SubItems(4).Text) > 0 Then
                oWrite.WriteLine("{0,10}{1,30}", "Advances -", Val(ListView1.Items(abc).SubItems(4).Text))
            End If
            oWrite.WriteLine("{0,10}{1,29}", "Grand Total", Val(ListView1.Items(abc).SubItems(5).Text))
            'oWrite.WriteLine("{0,13}{1,15}{2,12}", "-------------", "Marka Wise Bal.", "------------")
            'a7.print_crate_bal(ListView1.Items(abc).Text, DateTimePicker1, companyid, oWrite)
            oWrite.WriteLine("{0,40}", New String("-", 40))
            Dim abc2 As SqlCommand
            Dim crb As Integer
            crb = 0
            abc2 = New SqlCommand("select cr,date from crateledger  where customerid=@customerid and companyid=@companyid and cr>@cr and date<=@date and particular=@particular order by voucherid desc")
            abc2.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            abc2.Parameters.AddWithValue("@customerid", SqlDbType.Int).Value = ListView1.Items(abc).Text
            abc2.Parameters.AddWithValue("@cr", SqlDbType.Int).Value = crb
            abc2.Parameters.AddWithValue("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
            abc2.Parameters.AddWithValue("@particular", SqlDbType.VarChar).Value = "RECEIPT"
            Dim xyz5(2) As String
            a7.array_list(abc2, xyz5, 0)
            oWrite.WriteLine("{0,-12}{1,9}{2,8}{3,10}", "Received CR.", Format(Val(xyz5(0)), "0.00"), " On Date ", xyz5(1))
            'oWrite.WriteLine()
            oWrite.WriteLine()
            oWrite.WriteLine()
            oWrite.WriteLine()
            oWrite.WriteLine()
            oWrite.WriteLine()
        End If
        oWrite.Close()
        Dim p As New Process
        Dim info As New ProcessStartInfo
        info.FileName = location & "\" & "sample1.txt"
        'info.FileName = "c:\temp\sample.txt"
        info.Verb = "print"
        p.StartInfo = info
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo.RedirectStandardOutput = False
        p.Start()
        'System.Diagnostics.Process.Start("c:\printfile.bat")
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        print_bill_laser()
    End Sub
    Public Sub outstanding_list()
        Dim abc As SqlCommand
        Dim rcount, i As Integer
        Dim total, balance, prv_balance, total_balance As Long
        prv_balance = 0
        total_balance = 0
        rcount = ListView1.Items.Count
        i = 0
        Do While i <= rcount - 1
            abc = New SqlCommand("select customerid,cname,(select sum(dr) from dbo.crateledger where customerid=dbo.customer.customerid and companyid=@companyid and date<=@date) , (select sum(cr) from dbo.crateledger where customerid=dbo.customer.customerid and companyid=@companyid and date<=@date) from dbo.customer where dbo.customer.customerid=@customerid")
            abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            abc.Parameters.Add("@customerid", SqlDbType.Int).Value = Val(ListView1.Items(i).SubItems(0).Text)
            abc.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
            Dim xyz(3) As String
            a7.array_list(abc, xyz, 0)
            total = Val(xyz(2)) - Val(xyz(3))
            If Val(total) >= Val(ListView1.Items(i).SubItems(2).Text) Then
                balance = total - Val(ListView1.Items(i).SubItems(2).Text)
                ListView1.Items(i).SubItems.Add(balance)
                ListView1.Items(i).SubItems.Add("")
                ListView1.Items(i).SubItems.Add(total)
            Else
                balance = Val(ListView1.Items(i).SubItems(2).Text) - total
                ListView1.Items(i).SubItems.Add("")
                ListView1.Items(i).SubItems.Add(balance)
                ListView1.Items(i).SubItems.Add(total)
            End If
            i = i + 1
        Loop
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim abc, i As Integer
        abc = ListView1.Items.Count
        i = 0
        Do While i < abc
            ListView1.Items(i).Checked = True
            i = i + 1
        Loop
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
        list_add()
        ListView1.Focus()
    End Sub
    Private Sub show_total()
        Dim abc, i As Integer
        abc = ListView1.Items.Count
        i = 0
        Dim safi, arhat, laga, total As Integer
        safi = 0
        arhat = 0
        laga = 0
        total = 0
        Do While i < abc
            total = total + Val(ListView1.Items(i).SubItems(3).Text)
            i = i + 1
        Loop
        TextBox4.Text = total
    End Sub
    Private Sub DateTimePicker1_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.CloseUp
        list_add()
        ListView1.Focus()
    End Sub
    Private Sub Button1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        print_bill()
    End Sub
End Class