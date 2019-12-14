Imports System.Data.SqlClient
Public Class bookwiselist
    Dim a7 As New predefined
    Dim companyid, bookid, customerid As Integer
    Public Sub Company_add()
        Dim abc As String
        abc = "select company from company"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Private Sub Bookwiselist_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Company_add()
    End Sub
    Public Sub Test_list()
        Dim abc As SqlCommand
        abc = New SqlCommand("select b.name,sum(total) from bikri a,book b where a.entry_date=@date and a.sessionid=@session and a.companyid=@company and a.bookid=b.bookid group by b.name")
        abc.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
        abc.Parameters.Add("@company", SqlDbType.Int).Value = companyid
        a7.List_add(abc, ListView1)
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
        test_list()
    End Sub
    Private Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim location As String = IO.Directory.GetParent(Application.ExecutablePath).FullName
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        'oWrite = oFile.CreateText("c:\temp\sample.txt")
        oWrite = oFile.CreateText(location & "\" & "sample1.txt")
        Dim abc, i As Integer
        abc = ListView1.Items.Count
        i = 0
        Dim bspace As String
        bspace = ""
        oWrite.Write("(")
        oWrite.Write(ComboBox1.Text)
        oWrite.Write(")")
        oWrite.WriteLine()
        oWrite.Write("Bikri Of Date :- ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 25))
        oWrite.Write("{0,-15}", "Book")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Total")
        oWrite.Write(New String(" ", 1))
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 25))
        Do While i < abc
            oWrite.Write("{0,-15}", Mid(ListView1.Items(i).SubItems(0).Text, 1, 15))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(1).Text)
            oWrite.WriteLine()
            i = i + 1
        Loop
        oWrite.WriteLine(New String("-", 25))
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
        'System.Diagnostics.Process.Start("c:\temp\printbill.bat")
        'System.Diagnostics.Process.Start("c:\printfile.bat")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

    End Sub
End Class