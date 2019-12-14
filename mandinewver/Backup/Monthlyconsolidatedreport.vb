Imports System.Data.SqlClient
Public Class Monthlyconsolidatedreport
    Dim a7 As New predefined
    Dim abc1 As Integer
    Public sessionid As Integer
    Public companyid As Integer
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox2.Text
        companyid = a7.get_company_id(itemname)
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Monthlyconsolidatedreport_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        show_data_consolidated()
    End Sub
    Public Sub show_data()
        Dim abc As SqlCommand
        abc = New SqlCommand("select b.entry_date,sum(b.total),sum(b.arhat),sum(b.laga),sum(b.gtotal)from bikri b where companyid=@companyid and entry_date>=@startdate and entry_date<=@enddate group by entry_date")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        abc.Parameters.Add("@startdate", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@enddate", SqlDbType.DateTime).Value = DateTimePicker2.Value.Date
        a7.list_add(abc, ListView1)
        show_total()
    End Sub
    Public Sub show_data_consolidated()
        Dim abc As SqlCommand
        abc = New SqlCommand("select distinct(date),companyid,sessionid from voucher where companyid=@companyid and date>=@startdate and date<=@enddate and sessionid=@sessionid  order by date")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        abc.Parameters.Add("@startdate", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@enddate", SqlDbType.DateTime).Value = DateTimePicker2.Value.Date
        a7.consolidated_bikri_report(abc, ListView1, 0)
        show_total()
    End Sub
    Private Sub show_total()
        Dim abc, i As Integer
        abc = ListView1.Items.Count
        i = 0
        Dim safi, arhat, laga, total, wasuli, advance, opening_balance, closing_balance As Decimal
        safi = 0
        arhat = 0
        laga = 0
        total = 0
        wasuli = 0
        advance = 0
        opening_balance = 0
        closing_balance = 0
        ListView1.Items.Add(" ")
        ListView1.Items(abc).SubItems.Add("")
        ListView1.Items(abc).SubItems.Add("")
        ListView1.Items(abc).SubItems.Add("")
        ListView1.Items(abc).SubItems.Add("")
        ListView1.Items(abc).SubItems.Add("")
        ListView1.Items(abc).SubItems.Add("")
        ListView1.Items(abc).SubItems.Add("")
        ListView1.Items(abc).SubItems.Add("")
        Do While i < abc
            safi = safi + Val(ListView1.Items(i).SubItems(1).Text)
            arhat = arhat + Val(ListView1.Items(i).SubItems(2).Text)
            laga = laga + Val(ListView1.Items(i).SubItems(3).Text)
            total = total + Val(ListView1.Items(i).SubItems(4).Text)
            wasuli = wasuli + Val(ListView1.Items(i).SubItems(6).Text)
            advance = advance + Val(ListView1.Items(i).SubItems(7).Text)
            i = i + 1
        Loop
        abc = abc + 1
        ListView1.Items.Add("Total")
        ListView1.Items(abc).SubItems.Add(safi)
        ListView1.Items(abc).SubItems.Add(arhat)
        ListView1.Items(abc).SubItems.Add(laga)
        ListView1.Items(abc).SubItems.Add(total)
        ListView1.Items(abc).SubItems.Add(" ")
        ListView1.Items(abc).SubItems.Add(wasuli)
        ListView1.Items(abc).SubItems.Add(advance)
        ListView1.Items(abc).SubItems.Add(" ")
    End Sub
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox2)
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
        oWrite.Write(ComboBox2.Text)
        oWrite.Write(")")
        oWrite.WriteLine()
        oWrite.Write(New String(" ", 1))
        oWrite.Write(New String(" ", 2))
        oWrite.Write("Consolidated Sale Report From :- ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.Write(" To :- ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.Write("{0,-10}", ("Date"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", ("Opn.Bal"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Safi")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Arhat")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,6}", "laga")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Total")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", ("Receipt"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,6}", ("Advances"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", ("Cls. Bal"))
        oWrite.Write(New String(" ", 1))
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        Do While i < abc
            oWrite.Write("{0,-10}", Mid(ListView1.Items(i).SubItems(0).Text, 1, 10))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(5).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(1).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(2).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,6}", Mid(ListView1.Items(i).SubItems(3).Text, 1, 6))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(4).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(6).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,6}", Mid(ListView1.Items(i).SubItems(7).Text, 1, 6))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(8).Text, 1, 8))
            oWrite.WriteLine()
            oWrite.WriteLine(New String("-", 80))
            i = i + 1
        Loop
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
    Private Sub Monthlyconsolidatedreport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
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
        oWrite.Write(ComboBox2.Text)
        oWrite.Write(")")
        oWrite.WriteLine()
        oWrite.Write(New String(" ", 1))
        oWrite.Write(New String(" ", 2))
        oWrite.Write("Consolidated Sale Report From :- ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.Write(" To :- ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.Write("{0,-10}", ("Date"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", ("Opn.Bal"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Safi")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Arhat")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,6}", "laga")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Total")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", ("Receipt"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,6}", ("Advances"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", ("Cls. Bal"))
        oWrite.Write(New String(" ", 1))
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        Do While i < abc
            oWrite.Write("{0,-10}", Mid(ListView1.Items(i).SubItems(0).Text, 1, 10))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(5).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(1).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(2).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,6}", Mid(ListView1.Items(i).SubItems(3).Text, 1, 6))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(4).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(6).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,6}", Mid(ListView1.Items(i).SubItems(7).Text, 1, 6))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(8).Text, 1, 8))
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
        'System.Diagnostics.Process.Start("c:\printlist.bat")
    End Sub
End Class