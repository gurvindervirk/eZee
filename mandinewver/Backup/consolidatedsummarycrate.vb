Imports System.Data.SqlClient
Public Class consolidatedsummarycrate
    Dim a7 As New predefined
    Public choice As Integer
    Dim totalamount As Integer
    Dim companyid, crateid As Integer
    Public Sub customer_outstanding()
        Dim abc As SqlCommand
        abc = New SqlCommand("select crateid,cname,(select sum(dr) from dbo.crateledger where companyid=@companyid and crateid=dbo.crate.crateid and date<=@dt) , (select sum(cr) from dbo.crateledger where companyid=@companyid and crateid=dbo.crate.crateid and date<=@dt) from dbo.crate order by cname")
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        a7.cust_outstanding(abc, ListView1)
    End Sub
    Private Sub Customeroutstanding_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        print_report()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
    End Sub
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox1)
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
    Public Sub print_report()
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        oWrite = oFile.CreateText("c:\temp\sample1.txt")
        Dim abc, i, lbreak As Integer
        abc = ListView1.Items.Count
        i = 0
        lbreak = 1
        Dim bspace As String
        bspace = ""
        oWrite.Write("(")
        oWrite.Write(ComboBox1.Text)
        oWrite.Write(")")
        oWrite.Write("Crate Outstanding Till Date :-")
        oWrite.Write(" ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 20))
        Do While i < abc
            oWrite.Write("{0,-12}", Mid(ListView1.Items(i).SubItems(2).Text, 1, 12))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,6}", ListView1.Items(i).SubItems(3).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.WriteLine()
            i = i + 1
        Loop
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 20))
        oWrite.Write("Total ")
        oWrite.Write(New String(" ", 6))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,6}", TextBox1.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 20))
        oWrite.Close()
        System.Diagnostics.Process.Start("c:\printlist.bat")
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.S Then
            crateoutstanding.companyid = companyid
            crateoutstanding.crateid = Val(ListView1.SelectedItems.Item(0).SubItems(1).Text)
            crateoutstanding.TextBox2.Text = ComboBox1.Text
            crateoutstanding.TextBox3.Text = ListView1.SelectedItems.Item(0).SubItems(2).Text
            crateoutstanding.DateTimePicker1.Text = DateTimePicker1.Text
            crateoutstanding.customer_outstanding()
            crateoutstanding.Show()
        End If
    End Sub
End Class