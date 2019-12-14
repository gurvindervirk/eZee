Imports System.Data.SqlClient
Public Class Outstanding
    Dim a7 As New predefined
    Public choice As Integer
    Dim totalamount As Integer
    Dim companyid As Integer
    Public Sub Customer_outstanding()

        Dim abc As SqlCommand
        'abc = New SqlCommand("select customerid,cname,(select sum(amount) from dbo.voucher where account_desc=@dr and sessionid=@sessionid and companyid=@companyid and cid=dbo.customer.customerid and date<=@dt) - (select sum(amount) from dbo.voucher where account_desc=@cr and sessionid=@sessionid and companyid=@companyid and cid=dbo.customer.customerid and date<=@dt) from dbo.customer order by cname")
        abc = New SqlCommand("select customerid,cname,(select sum(amount) from dbo.voucher where account_desc=@dr and sessionid=@sessionid and companyid=@companyid and cid=dbo.customer.customerid and date<=@dt) , (select sum(amount) from dbo.voucher where account_desc=@cr and sessionid=@sessionid and companyid=@companyid and cid=dbo.customer.customerid and date<=@dt) from dbo.customer order by cname")
        abc.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        abc.Parameters.Add("@dr", SqlDbType.VarChar).Value = "DR"
        abc.Parameters.Add("@cr", SqlDbType.VarChar).Value = "CR"
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        a7.cust_outstanding(abc, ListView1)
    End Sub
    Private Sub Customeroutstanding_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim abc As SqlCommand
        abc = New SqlCommand("delete from dummy_outstanding")
        a7.Delete_data(abc)
        Dim abc3 As String
        Dim rcount, i As Integer
        rcount = ListView1.Items.Count
        i = 0
        Do While i < rcount
            abc3 = "insert into dummy_outstanding(cid,cname,amount)values(" & Val(ListView1.Items(i).SubItems(1).Text) & ",'" & ListView1.Items(i).SubItems(2).Text & "'," & Val(ListView1.Items(i).SubItems(3).Text) & ")"
            a7.Insert_data(abc3)
            i = i + 1
        Loop
        update_record()
        consolidatedlist.TextBox1.Text = ComboBox1.Text
        consolidatedlist.DateTimePicker1.Text = DateTimePicker1.Text
        consolidatedlist.companyid = companyid
        consolidatedlist.Show()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
    End Sub
    Public Sub Company_add()

        Dim abc As String
        abc = "select company from company"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Private Sub DateTimePicker1_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.CloseUp
        customer_outstanding()
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
    Public Sub Print_report()

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
        oWrite.Write(ComboBox1.Text)
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
        Dim info As New ProcessStartInfo With {
            .FileName = "c:\temp\sample.txt",
            .Verb = "print"
        }
        p.StartInfo = info
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo.RedirectStandardOutput = False
        p.Start()
        'System.Diagnostics.Process.Start("c:\temp\printbill.bat")
    End Sub
    Public Sub Update_record()

        Dim abc As SqlCommand
        abc = New SqlCommand("update dummy_outstanding set status=@status from dummy_outstanding a,bikri b where a.cid=b.cid and b.entry_date=@date and b.sessionid=@sessionid")
        abc.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@status", SqlDbType.VarChar).Value = "NEW"
        abc.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        a7.Update_data(abc)
    End Sub
End Class