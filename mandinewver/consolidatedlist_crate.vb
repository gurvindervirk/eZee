Imports System.Data.SqlClient
Public Class consolidatedlist_crate
    Dim a7 As New predefined
    Public companyid, crateid, drtotal, crtotal As Integer
    Public Sub New_record()
        Dim abc As SqlCommand
        abc = New SqlCommand("select cid,cname,amount from dummy_outstanding where status=@status order by cname")
        abc.Parameters.Add("@status", SqlDbType.VarChar).Value = "ISSUE"
        a7.List_add1(abc, ListView1)
    End Sub
    Public Sub OLD_record()
        Dim abc As SqlCommand
        abc = New SqlCommand("select cid,cname,amount from dummy_outstanding where status=@status order by cname")
        abc.Parameters.Add("@status", SqlDbType.VarChar).Value = "OLD"
        a7.list_add1(abc, ListView2)
    End Sub
    Private Sub Consolidatedlist_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        customer_outstanding()
        New_record()
        OLD_record()
        outstanding_total()
        show_list()
        show_list_advances()
        show_mandi()
    End Sub
    Public Sub Outstanding_total()
        Dim abc As SqlCommand
        abc = New SqlCommand("select sum(amount) from dummy_outstanding")
        Dim xyz(0) As String
        a7.Array_list(abc, xyz, 0)
        TextBox2.Text = xyz(0)
    End Sub
    Public Sub Print_report()
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
        oWrite.WriteLine()
        oWrite.Write("(")
        oWrite.Write(TextBox1.Text)
        oWrite.Write(")")
        oWrite.Write(" ")
        oWrite.Write("Marka :-")
        oWrite.Write(" ")
        oWrite.Write("(")
        oWrite.Write(TextBox5.Text)
        oWrite.Write(")")
        oWrite.Write(" ")
        oWrite.Write("Crate Outstanding Till Date :-")
        oWrite.Write(" ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        If abc > 0 Then
            Do While i < abc
                oWrite.Write("{0,-12}", Mid(ListView1.Items(i).SubItems(2).Text, 1, 12))
                oWrite.Write(New String(" ", 1))
                oWrite.Write("{0,6}", ListView1.Items(i).SubItems(3).Text)
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
        End If
        abc = ListView2.Items.Count
        i = 0
        lbreak = 1
        If abc > 0 Then
            Do While i < abc
                oWrite.Write("{0,-12}", Mid(ListView2.Items(i).SubItems(2).Text, 1, 12))
                oWrite.Write(New String(" ", 1))
                oWrite.Write("{0,6}", ListView2.Items(i).SubItems(3).Text)
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
            oWrite.Write("Total Outstandings ")
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,7}", TextBox2.Text)

            oWrite.WriteLine()
        End If
        abc = ListView3.Items.Count
        If abc > 0 Then
            oWrite.Write(New String("-", 36))
            oWrite.Write("Receipts")
            oWrite.Write(New String("-", 36))
            oWrite.WriteLine()

            i = 0
            lbreak = 1
            Do While i < abc
                oWrite.Write("{0,-12}", Mid(ListView3.Items(i).SubItems(2).Text, 1, 12))
                oWrite.Write(New String(" ", 1))
                oWrite.Write("{0,6}", ListView3.Items(i).SubItems(3).Text)
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
            oWrite.Write("{0,7}", TextBox3.Text)
        End If
        abc = ListView4.Items.Count
        If abc > 0 Then
            oWrite.WriteLine()
            oWrite.Write(New String("-", 36))
            oWrite.Write("Advances")
            oWrite.Write(New String("-", 36))
            oWrite.WriteLine()
            i = 0
            lbreak = 1
            Do While i < abc
                oWrite.Write("{0,-12}", Mid(ListView4.Items(i).SubItems(2).Text, 1, 12))
                oWrite.Write(New String(" ", 1))
                oWrite.Write("{0,6}", ListView4.Items(i).SubItems(3).Text)
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
            oWrite.Write("Total Advances")
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,7}", TextBox4.Text)
        End If
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.WriteLine()
        oWrite.Write("{0,10}", "O/Bal(+)")
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", "Issue(+)")
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", "Advn(-)")
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", "Recpt(=)")
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", "Cls. Bal.")
        oWrite.WriteLine()
        oWrite.Write("{0,10}", TextBox9.Text)
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", drtotal)
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", TextBox4.Text)
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", crtotal)
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", TextBox2.Text)
        oWrite.WriteLine()
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
        'System.Diagnostics.Process.Start("c:\printlist.bat")
    End Sub
    Public Sub Print_report_dotmatrix()
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
        oWrite.WriteLine()
        oWrite.Write("(")
        oWrite.Write(TextBox1.Text)
        oWrite.Write(")")
        oWrite.Write(" ")
        oWrite.Write("Marka :-")
        oWrite.Write(" ")
        oWrite.Write("(")
        oWrite.Write(TextBox5.Text)
        oWrite.Write(")")
        oWrite.Write(" ")
        oWrite.Write("Crate Outstanding Till Date :-")
        oWrite.Write(" ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        If abc > 0 Then
            Do While i < abc
                oWrite.Write("{0,-12}", Mid(ListView1.Items(i).SubItems(2).Text, 1, 12))
                oWrite.Write(New String(" ", 1))
                oWrite.Write("{0,6}", ListView1.Items(i).SubItems(3).Text)
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
        End If
        abc = ListView2.Items.Count
        i = 0
        lbreak = 1
        If abc > 0 Then
            Do While i < abc
                oWrite.Write("{0,-12}", Mid(ListView2.Items(i).SubItems(2).Text, 1, 12))
                oWrite.Write(New String(" ", 1))
                oWrite.Write("{0,6}", ListView2.Items(i).SubItems(3).Text)
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
            oWrite.Write("Total Outstandings ")
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,7}", TextBox2.Text)

            oWrite.WriteLine()
        End If
        abc = ListView3.Items.Count
        If abc > 0 Then
            oWrite.Write(New String("-", 36))
            oWrite.Write("Receipts")
            oWrite.Write(New String("-", 36))
            oWrite.WriteLine()

            i = 0
            lbreak = 1
            Do While i < abc
                oWrite.Write("{0,-12}", Mid(ListView3.Items(i).SubItems(2).Text, 1, 12))
                oWrite.Write(New String(" ", 1))
                oWrite.Write("{0,6}", ListView3.Items(i).SubItems(3).Text)
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
            oWrite.Write("{0,7}", TextBox3.Text)
        End If
        abc = ListView4.Items.Count
        If abc > 0 Then
            oWrite.WriteLine()
            oWrite.Write(New String("-", 36))
            oWrite.Write("Advances")
            oWrite.Write(New String("-", 36))
            oWrite.WriteLine()
            i = 0
            lbreak = 1
            Do While i < abc
                oWrite.Write("{0,-12}", Mid(ListView4.Items(i).SubItems(2).Text, 1, 12))
                oWrite.Write(New String(" ", 1))
                oWrite.Write("{0,6}", ListView4.Items(i).SubItems(3).Text)
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
            oWrite.Write("Total Advances")
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,7}", TextBox4.Text)
        End If
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.WriteLine()
        oWrite.Write("{0,10}", "O/Bal(+)")
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", "Issue(+)")
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", "Advn(-)")
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", "Recpt(=)")
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", "Cls. Bal.")
        oWrite.WriteLine()
        oWrite.Write("{0,10}", TextBox9.Text)
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", drtotal)
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", TextBox4.Text)
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", crtotal)
        oWrite.Write(New String(" ", 2))
        oWrite.Write("{0,8}", TextBox2.Text)
        oWrite.WriteLine()
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
        'System.Diagnostics.Process.Start("c:\printlist.bat")
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        print_report()
    End Sub
    Public Sub Show_list()
        Dim abc As SqlCommand
        Dim totalamount As Integer
        Dim voucher_type As String
        voucher_type = "Receipt"
        abc = New SqlCommand("select a.customerid,b.cname,a.cr from crateledger a,customer b where a.customerid=b.customerid and a.particular=@voucher_type and a.companyid=@companyid and a.crateid=@crateid and a.date=@dt1 order by b.cname")
        abc.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@crateid", SqlDbType.Int).Value = crateid
        abc.Parameters.Add("@voucher_type", SqlDbType.VarChar).Value = voucher_type
        a7.List_add1(abc, ListView3)
        Dim rcount, i As Integer
        rcount = ListView3.Items.Count
        i = 0
        Do While i < rcount
            totalamount = Val(totalamount) + Val(ListView3.Items(i).SubItems(3).Text)
            i = i + 1
        Loop
        TextBox3.Text = totalamount
    End Sub
    Public Sub Show_list_advances()
        Dim abc As SqlCommand
        Dim totalamount As Integer
        Dim voucher_type As String
        voucher_type = "OPENINGBALANCE"
        abc = New SqlCommand("select a.customerid,b.cname,a.dr from crateledger a,customer b where a.customerid=b.customerid and a.particular=@voucher_type and a.companyid=@companyid and a.crateid=@crateid and a.date=@dt1 order by b.cname")
        abc.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@crateid", SqlDbType.Int).Value = crateid
        abc.Parameters.Add("@voucher_type", SqlDbType.VarChar).Value = voucher_type
        a7.List_add1(abc, ListView4)
        Dim rcount, i As Integer
        rcount = ListView4.Items.Count
        i = 0
        Do While i < rcount
            totalamount = Val(totalamount) + Val(ListView4.Items(i).SubItems(3).Text)
            i = i + 1
        Loop
        TextBox4.Text = totalamount
    End Sub
    Public Sub Show_mandi()
        Dim abc As SqlCommand
        abc = New SqlCommand("select sum(dr),sum(cr) from crateledger where date=@date and companyid=@companyid and crateid=@crateid")
        abc.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@crateid", SqlDbType.Int).Value = crateid
        Dim xyz(4) As String
        a7.Array_list(abc, xyz, 0)
        drtotal = xyz(0)
        crtotal = xyz(1)
    End Sub
    Public Sub Customer_outstanding()
        Dim abc As SqlCommand
        abc = New SqlCommand("select customerid,cname,(select sum(dr) from dbo.crateledger where companyid=@companyid and crateid=@crateid and date<@dt) - (select sum(cr) from dbo.crateledger where companyid=@companyid and crateid=@crateid and date<@dt) from dbo.customer")
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@crateid", SqlDbType.Int).Value = crateid
        abc.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        Dim xyz(3) As String
        a7.Array_list(abc, xyz, 0)
        TextBox9.Text = xyz(2)
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            crateoutstanding.ListView1.Focus()
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        print_report_dotmatrix()
    End Sub
End Class