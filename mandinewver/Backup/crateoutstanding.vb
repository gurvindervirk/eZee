Imports System.Data.SqlClient
Public Class crateoutstanding
    Dim a7 As New predefined
    Public choice As Integer
    Dim totalamount As Integer
    Public companyid, crateid As Integer
    Public Sub customer_outstanding()
        Dim abc As SqlCommand
        abc = New SqlCommand("select customerid,cname,(select sum(dr) from dbo.crateledger where companyid=@companyid and customerid=dbo.customer.customerid and date<=@dt and crateid=@crateid) , (select sum(cr) from dbo.crateledger where companyid=@companyid and customerid=dbo.customer.customerid and date<=@dt and crateid=@crateid) from dbo.customer order by cname")
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@crateid", SqlDbType.Int).Value = crateid
        abc.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        a7.cust_outstanding(abc, ListView1)
        Dim rcount, i As Integer
        rcount = ListView1.Items.Count
        i = 0
        totalamount = 0
        Do While i < rcount
            totalamount = Val(totalamount) + Val(ListView1.Items(i).SubItems(3).Text)
            i = i + 1
        Loop
        TextBox1.Text = totalamount
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim abc As SqlCommand
        abc = New SqlCommand("delete from dummy_outstanding")
        a7.delete_data(abc)
        Dim abc3 As String
        Dim rcount, i As Integer
        rcount = ListView1.Items.Count
        i = 0
        Do While i < rcount
            abc3 = "insert into dummy_outstanding(cid,cname,amount)values(" & Val(ListView1.Items(i).SubItems(1).Text) & ",'" & ListView1.Items(i).SubItems(2).Text & "'," & Val(ListView1.Items(i).SubItems(3).Text) & ")"
            a7.insert_data(abc3)
            i = i + 1
        Loop
        update_record()
        consolidatedlist_crate.TextBox1.Text = TextBox2.Text
        consolidatedlist_crate.TextBox5.Text = TextBox3.Text
        consolidatedlist_crate.DateTimePicker1.Text = DateTimePicker1.Text
        consolidatedlist_crate.companyid = companyid
        consolidatedlist_crate.crateid = crateid
        consolidatedlist_crate.ListView1.Focus()
        consolidatedlist_crate.Show()
    End Sub

    Public Sub print_report()
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        oWrite = oFile.CreateText("c:\temp\sample.txt")
        Dim abc, i, lbreak As Integer
        abc = ListView1.Items.Count
        i = 0
        lbreak = 1
        Dim bspace As String
        bspace = ""
        oWrite.Write("(")
        oWrite.Write(TextBox2.Text)
        oWrite.Write(")")
        oWrite.Write("Customer Outstanding Till Date :-")
        oWrite.Write(" ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
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
        oWrite.Write("Total ")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,7}", TextBox1.Text)
        oWrite.Close()
        Dim p As New Process
        Dim info As New ProcessStartInfo
        info.FileName = "c:\temp\sample.txt"
        info.Verb = "print"
        p.StartInfo = info
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo.RedirectStandardOutput = False
        p.Start()
        'System.Diagnostics.Process.Start("c:\printlist.bat")
        'System.Diagnostics.Process.Start("c:\temp\printbill.bat")
    End Sub
    Public Sub update_record()
        Dim abc As SqlCommand
        abc = New SqlCommand("update dummy_outstanding set status=@status  from dummy_outstanding a,crateledger b where a.cid=b.customerid and b.date=@date and b.particular=@status")
        abc.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@status", SqlDbType.VarChar).Value = "ISSUE"
        a7.update_data(abc)
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class