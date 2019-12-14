Imports System.Data.SqlClient
Public Class bill_transfer
    Dim a7 As New predefined
    Public bill_date As Date
    Public cname As String
    Public customerid, companyid As Integer
    Dim grand_total As Long
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Sub list_show()
        Dim abc As SqlCommand
        abc = New SqlCommand("select a.entry_no,b.name,a.nag,a.weight,a.rate,a.total,a.arhat,a.laga,a.gtotal from bikri a,vegetable b where a.sessionid= @sessionid  and a.companyid=@companyid  and a.entry_date=@date and a.cid=@cid and a.vegetableid=b.vegetableid")
        abc.Parameters.Add("@date", SqlDbType.DateTime).Value = bill_date
        abc.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@cid", SqlDbType.Int).Value = customerid
        a7.List_add(abc, ListView1)
        list_sum()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        list_show()
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Button1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim row_count, i, num As Integer
        row_count = ListView1.Items.Count
        i = 0
        num = 0
        Do While i <= row_count - 1
            If ListView1.Items(i).Checked = True Then
                num = num + 1
                TextBox11.Text = Val(TextBox11.Text) + Val(ListView1.Items(i).SubItems(8).Text)
            End If
            i = i + 1
        Loop
        TextBox12.Text = Val(TextBox10.Text) - Val(TextBox11.Text)
        If num = 0 Then Exit Sub
        If TextBox12.Text >= 0 Then
            trans_cust.companyid = companyid
            trans_cust.Show()
        Else
            MsgBox("Can't transfer,Balance will be Negative...!!", MsgBoxStyle.Information, "Vega 3.5")
            Exit Sub
        End If
    End Sub
    Private Sub bill_transfer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Customerledger1.Visible = True Then
            Customerledger1.Show_ledger()
        End If
    End Sub
    Public Sub list_sum()
        Dim row_count, i As Integer
        row_count = ListView1.Items.Count
        i = 0
        Do While i <= row_count - 1
            TextBox3.Text = Val(TextBox3.Text) + Val(ListView1.Items(i).SubItems(2).Text)
            TextBox4.Text = Val(TextBox4.Text) + Val(ListView1.Items(i).SubItems(3).Text)
            TextBox5.Text = Val(TextBox5.Text) + Val(ListView1.Items(i).SubItems(4).Text)
            TextBox6.Text = Val(TextBox6.Text) + Val(ListView1.Items(i).SubItems(5).Text)
            TextBox7.Text = Val(TextBox7.Text) + Val(ListView1.Items(i).SubItems(6).Text)
            TextBox8.Text = Val(TextBox8.Text) + Val(ListView1.Items(i).SubItems(7).Text)
            TextBox9.Text = Val(TextBox9.Text) + Val(ListView1.Items(i).SubItems(8).Text)
            i = i + 1
        Loop
    End Sub
    Private Sub bill_transfer_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Sub prv_balance()
        Dim abc, abc1 As SqlCommand
        Dim account_desc, account_desc1 As String
        Dim sum_dr, sum_cr, balance As Integer
        account_desc = "DR"
        account_desc1 = "CR"
        sum_dr = 0
        sum_cr = 0
        abc = New SqlCommand("select sum(amount) from voucher where companyid=" & companyid & " and sessionid=" & MDIParent1.sessionid & " and cid=" & customerid & " and account_desc='" & account_desc & "'")
        Dim xyz(0), xyz1(0) As String
        a7.array_list(abc, xyz, 0)
        sum_dr = xyz(0)
        abc1 = New SqlCommand("select sum(amount) from voucher where companyid=" & companyid & " and sessionid=" & MDIParent1.sessionid & " and cid=" & customerid & " and account_desc='" & account_desc1 & "'")
        a7.array_list(abc1, xyz1, 0)
        sum_cr = xyz1(0)
        balance = sum_dr - sum_cr
        TextBox10.Text = balance
    End Sub
    Private Sub bill_transfer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DateTimePicker1.Text = bill_date
        prv_balance()
        If MDIParent1.utype = "USER" Then
            TextBox10.Visible = False
            TextBox11.Visible = False
            TextBox12.Visible = False
            Button1.Visible = False
            Label3.Visible = False
            Label4.Visible = False
            Label5.Visible = False
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        print_bill()
    End Sub
    Public Sub print_bill()
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        oWrite = oFile.CreateText("c:\temp\sample.txt")
        Dim abc As Integer
        abc = ListView1.Items.Count
        oWrite.WriteLine("{0,20}{1,-20}", New String(" ", 20), Customerledger1.ComboBox1.Text)
        oWrite.WriteLine("{0,-30}{1,10}", TextBox2.Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"))
        oWrite.WriteLine(New String("-", 40))
        oWrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}", "Item", "Nag", "Weight", "Rate", "Amount")
        oWrite.WriteLine(New String("-", 40))
        'a7.print_bill(customerid, DateTimePicker1, companyid, oWrite)
        'a7.print_bill_bookwise(customerid, DateTimePicker1, companyid, oWrite)
        oWrite.WriteLine(New String("-", 40))
        oWrite.WriteLine("{0,30}{1,10}", "Safi", Format(Val(TextBox6.Text), "0.00"))
        oWrite.WriteLine("{0,30}{1,10}", "Arhat", Format(Val(TextBox7.Text), "0.00"))
        oWrite.WriteLine("{0,30}{1,10}", "Laga", Format(Val(TextBox8.Text), "0.00"))
        oWrite.WriteLine("{0,40}", New String("-", 40))
        oWrite.WriteLine("{0,30}{1,10}", "Total", Format(Val(TextBox9.Text), "0.00"))
        oWrite.WriteLine("{0,40}", New String("-", 40))
        outstanding_list()
        Dim bal, adv As Long
        If Val(grand_total) > Val(TextBox9.Text) Then
            bal = Val(grand_total) - Val(TextBox9.Text)
            oWrite.WriteLine("{0,30}{1,10}", "Prv.Balance", Format(Val(bal), "0.00"))
        End If
        If Val(TextBox9.Text) > Val(grand_total) Then
            adv = Val(TextBox9.Text) - Val(grand_total)
            oWrite.WriteLine("{0,30}{1,10}", "Advances -", Format(Val(adv), "0.00"))
        End If
        oWrite.WriteLine("{0,30}{1,10}", "Grand Total", Format(Val(grand_total), "0.00"))
        oWrite.WriteLine("{0,40}", New String("-", 40))
        oWrite.WriteLine()
        oWrite.WriteLine()
        oWrite.Close()
        System.Diagnostics.Process.Start("c:\printfile.bat")
    End Sub
    Public Sub outstanding_list()
        Dim abc As SqlCommand
        Dim rcount, i As Integer
        rcount = ListView1.Items.Count
        i = 0
        Do While i <= rcount - 1
            abc = New SqlCommand("select customerid,cname,(select sum(amount) from dbo.voucher where account_desc=@dr and sessionid=@sessionid and cid=dbo.customer.customerid and companyid=@companyid and date<=@date) - (select sum(amount) from dbo.voucher where account_desc=@cr and sessionid=@sessionid and cid=dbo.customer.customerid and companyid=@companyid and date<=@date) from dbo.customer where dbo.customer.customerid=@customerid")
            abc.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
            abc.Parameters.Add("@dr", SqlDbType.VarChar).Value = "DR"
            abc.Parameters.Add("@cr", SqlDbType.VarChar).Value = "CR"
            abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            abc.Parameters.Add("@customerid", SqlDbType.Int).Value = customerid
            abc.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
            Dim xyz(3) As String
            a7.array_list(abc, xyz, 0)
            grand_total = Val(xyz(2))
            i = i + 1
        Loop
    End Sub
End Class
