Imports System.Data.SqlClient
Public Class Daily_Bikri_Report
    Dim a7 As New predefined
    Dim companyid, bookid, customerid As Integer
    Public Sub company_add()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox1)
    End Sub
    Public Sub book_add()
        Dim abc As String
        abc = "select name from book"
        a7.abc_add(abc, ComboBox2)
    End Sub
    Private Sub Daily_Bikri_Report_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Daily_Bikri_Report_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
        book_add()
    End Sub
    Private Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        test_list()
        ListView1.Focus()
    End Sub
    Public Sub test_list()
        Dim abc As SqlCommand
        abc = New SqlCommand("select a.bname,b.name,sum(nag),sum(weight),sum(total) from bikri a,vegetable b where a.vegetableid=b.vegetableid and a.entry_date=@date and a.sessionid=@session and a.companyid=@company and a.bookid=@book group by a.bname,b.name")
        abc.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
        abc.Parameters.Add("@company", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@book", SqlDbType.Int).Value = bookid
        a7.list_add(abc, ListView1)
        Dim abc1, abc2, abc3 As SqlCommand
        abc1 = New SqlCommand("Select sum(total) from bikri where entry_date= @date and sessionid= @session  and companyid= @company  and bookid= @book  and cid= @cid")
        abc1.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc1.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
        abc1.Parameters.Add("@company", SqlDbType.Int).Value = companyid
        abc1.Parameters.Add("@book", SqlDbType.Int).Value = bookid
        abc1.Parameters.Add("@cid", SqlDbType.Int).Value = customerid
        Dim xyz(0) As String
        a7.array_list(abc1, xyz, 0)
        TextBox1.Text = xyz(0)
        abc2 = New SqlCommand("Select sum(total) from bikri where entry_date= @date and sessionid= @session  and companyid= @company  and bookid= @book  and cid<> @cid")
        abc2.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc2.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
        abc2.Parameters.Add("@company", SqlDbType.Int).Value = companyid
        abc2.Parameters.Add("@book", SqlDbType.Int).Value = bookid
        abc2.Parameters.Add("@cid", SqlDbType.Int).Value = customerid
        a7.array_list(abc2, xyz, 0)
        TextBox2.Text = xyz(0)
        abc3 = New SqlCommand("Select sum(total) from bikri where entry_date= @date and sessionid= @session  and companyid= @company  and bookid= @book")
        abc3.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc3.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
        abc3.Parameters.Add("@company", SqlDbType.Int).Value = companyid
        abc3.Parameters.Add("@book", SqlDbType.Int).Value = bookid
        a7.array_list(abc3, xyz, 0)
        TextBox3.Text = xyz(0)
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Enter Then
            Modifybikri.DateTimePicker1.Value = DateTimePicker1.Value
            Modifybikri.TextBox1.Text = ComboBox1.Text
            Modifybikri.TextBox2.Text = ComboBox2.Text
            Modifybikri.TextBox19.Text = ListView1.SelectedItems.Item(0).SubItems(0).Text
            Modifybikri.TextBox20.Text = ListView1.SelectedItems.Item(0).SubItems(1).Text
            Modifybikri.TextBox3.Focus()
            Modifybikri.Show()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        amountwise.TextBox1.Text = DateTimePicker1.Value
        amountwise.bookid = bookid
        amountwise.companyid = companyid
        amountwise.Show()
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Customerwise.companyid = companyid
        Customerwise.bookid = bookid
        Customerwise.TextBox1.Text = DateTimePicker1.Value
        Customerwise.Show()
    End Sub
    Private Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Searchitem.companyid = companyid
        Searchitem.bookid = bookid
        Searchitem.TextBox1.Text = DateTimePicker1.Value
        Searchitem.Show()
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub ComboBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox2.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub DateTimePicker1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DateTimePicker1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        oWrite = oFile.CreateText("c:\temp\sample.txt")
        Dim abc, i As Integer
        abc = ListView1.Items.Count
        i = 0
        Dim bspace As String
        bspace = ""
        oWrite.Write("(")
        oWrite.Write(ComboBox1.Text)
        oWrite.Write(")")
        oWrite.WriteLine()
        oWrite.Write(New String(" ", 1))
        oWrite.Write("Bikri Of Date :- ")
        oWrite.Write(DateTimePicker1.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("Book :- ")
        oWrite.Write(ComboBox2.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 58))
        oWrite.Write("{0,-15}", "Beopari Name")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,-15}", "Vegetable")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Nag")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Weight")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Total")
        oWrite.Write(New String(" ", 1))
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 58))
        Do While i < abc
            oWrite.Write("{0,-15}", Mid(ListView1.Items(i).SubItems(4).Text, 1, 15))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,-15}", ListView1.Items(i).SubItems(5).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(6).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(7).Text)
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", ListView1.Items(i).SubItems(8).Text)
            oWrite.WriteLine()
            i = i + 1
        Loop
        oWrite.WriteLine(New String("-", 58))
        oWrite.Write("Nagdi ")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,7}", TextBox1.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("Udhar ")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,7}", TextBox2.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("Total ")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,7}", TextBox3.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 58))
        oWrite.Close()
        'System.Diagnostics.Process.Start("c:\temp\printbill.bat")
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim abc, abc1 As SqlCommand
        abc = New SqlCommand("select bookid from book where name=@bookname")
        abc.Parameters.AddWithValue("@bookname", SqlDbType.VarChar).Value = ComboBox2.Text
        Dim xyz(1) As String
        a7.array_list(abc, xyz, 0)
        bookid = xyz(0)
        Dim dname As String
        dname = "CASH"
        abc1 = New SqlCommand("select customerid from customer where companyid=@companyid and  cname=@dname")
        abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc1.Parameters.Add("@dname", SqlDbType.VarChar).Value = dname
        Dim xyz1(1) As String
        a7.array_list(abc1, xyz1, 0)
        customerid = xyz1(0)
        test_list()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class