Imports System.Data.SqlClient
Imports System.IO
Public Class Daily_Cash_Recipt
    Dim a7 As New predefined
    Dim companyid As Integer
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox1)
    End Sub
    Private Sub Daily_Cash_Recipt_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Daily_Cash_Recipt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub DateTimePicker1_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.CloseUp
        show_list()
    End Sub
    Private Sub DateTimePicker1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        oWrite = oFile.CreateText("c:\temp\sample.txt")
        oWrite.WriteLine(New String(" ", 10) + "Daily Cash Report :- " + DateTimePicker1.Value.ToString("dd/MM/yyyy"))
        oWrite.WriteLine(New String("-", 50))
        oWrite.WriteLine("{0,-7}{1,-32}{2,10}", "R.No", "Customer Name", "Amount")
        oWrite.WriteLine(New String("-", 50))
        Dim abc, i As Integer
        abc = ListView1.Items.Count
        i = 0
        Do While i < abc
            oWrite.WriteLine("{0,-7}{1,-32}{2,10}", ListView1.Items(i).Text, ListView1.Items(i).SubItems(1).Text, ListView1.Items(i).SubItems(2).Text)
            i = i + 1
        Loop
        oWrite.WriteLine(New String("-", 50))
        oWrite.WriteLine("{0,39}{1,10}", "Total", TextBox1.Text)
        oWrite.Close()
    End Sub
    Public Sub show_list()
        Dim abc As SqlCommand
        Dim voucher_type As String
        Dim totalamount As Integer
        totalamount = 0
        voucher_type = "Receipt"
        abc = New SqlCommand("select b.cname,a.amount,a.voucher_id from voucher a,customer b where a.cid=b.customerid and a.companyid=@companyid and a.voucher_type='" & voucher_type & "' and a.date=@dt1 order by voucher_id ")
        abc.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        a7.list_add1(abc, ListView1)
        Dim rcount, i As Integer
        rcount = ListView1.Items.Count
        i = 0
        Do While i < rcount
            totalamount = Val(totalamount) + Val(ListView1.Items(i).SubItems(2).Text)
            i = i + 1
        Loop
        TextBox1.Text = totalamount
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class