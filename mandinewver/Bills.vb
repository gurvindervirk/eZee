Imports System.Data.SqlClient
Public Class Bills_report
    Dim a7 As New predefined
    Dim companyid As Integer
    Public Sub Company_add()
        Dim abc As String
        abc = "select company from company"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Private Sub Bills_report_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Bills_report_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
    End Sub
    Public Sub List_add()
        Dim abc As SqlCommand
        Dim vtype As String
        vtype = "Bill"
        abc = New SqlCommand("select c.cid,b.cname,sum(c.total),sum(c.arhat),sum(c.laga),sum(c.gtotal) from customer b,bikri c where  c.cid=b.customerid and c.entry_date=@date and c.companyid=@companyid and c.sessionid=@sessionid group by c.cid,b.cname order by b.cname")
        abc.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        abc.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        a7.List_add(abc, ListView1)
        Outstanding_list()
        Show_total()
    End Sub
    Public Sub Dotmatrix_printer()
        'Dim ApplicationDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
        'Dim LogfilePath = System.IO.Path.Combine(ApplicationDir, "sample.txt")
        Dim location As String = IO.Directory.GetParent(Application.ExecutablePath).FullName
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        'oWrite = oFile.CreateText("c:\temp\sample.txt")
        'oWrite = oFile.CreateText()
        oWrite = oFile.CreateText(location & "\" & "sample.txt")
        'oWrite = oFile.CreateText()
        'MsgBox(location)
        Dim abc, i As Integer
        abc = ListView1.Items.Count
        i = 0
        Do While i < abc
            If ListView1.Items(i).Checked = True Then
                oWrite.WriteLine("{0,20}{1,-20}", New String(" ", 20), ComboBox1.Text)
                oWrite.WriteLine("{0,-30}{1,10}", ListView1.Items(i).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"))
                oWrite.WriteLine(New String("-", 40))
                oWrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}", "Item", "Nag", "Weight", "Rate", "Amount")
                oWrite.WriteLine(New String("-", 40))
                a7.print_bill(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite)
                oWrite.WriteLine(New String("-", 40))
                oWrite.WriteLine("{0,30}{1,10}", "Safi", Format(Val(ListView1.Items(i).SubItems(2).Text), "0.00"))
                oWrite.WriteLine("{0,30}{1,10}", "Arhat", Format(Val(ListView1.Items(i).SubItems(3).Text), "0.00"))
                oWrite.WriteLine("{0,30}{1,10}", "Laga", Format(Val(ListView1.Items(i).SubItems(4).Text), "0.00"))
                oWrite.WriteLine("{0,40}", New String("-", 40))
                oWrite.WriteLine("{0,30}{1,10}", "Total", Format(Val(ListView1.Items(i).SubItems(5).Text), "0.00"))
                oWrite.WriteLine("{0,40}", New String("-", 40))
                If Val(ListView1.Items(i).SubItems(6).Text) > 0 Then
                    oWrite.WriteLine("{0,30}{1,10}", "Prv.Balance", Format(Val(ListView1.Items(i).SubItems(6).Text), "0.00"))
                End If
                If Val(ListView1.Items(i).SubItems(7).Text) > 0 Then
                    oWrite.WriteLine("{0,30}{1,10}", "Advances -", Format(Val(ListView1.Items(i).SubItems(7).Text), "0.00"))
                End If
                oWrite.WriteLine("{0,30}{1,10}", "Grand Total", Format(Val(ListView1.Items(i).SubItems(8).Text), "0.00"))
                oWrite.WriteLine("{0,40}", New String("-", 40))
                Dim add_rec As SqlCommand
                Dim voucher_type As String
                voucher_type = "Receipt"
                add_rec = New SqlCommand("select amount,date from voucher where cid=@cid and companyid=@companyid and sessionid=@sessionid and voucher_type='" & voucher_type & "' and date<=@dt1 order by voucher_id desc ")
                add_rec.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                add_rec.Parameters.Add("@cid", SqlDbType.Int).Value = ListView1.Items(i).Text
                add_rec.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                add_rec.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
                Dim xyz3(2) As String
                a7.array_list(add_rec, xyz3, 0)
                'MsgBox(xyz3(0))
                oWrite.WriteLine("{0,-12}{1,9}{2,8}{3,10}", "Received Rs.", Format(Val(xyz3(0)), "0.00"), " On Date ", xyz3(1))
                oWrite.WriteLine()
                oWrite.WriteLine()
            End If
            i = i + 1
        Loop
        oWrite.Close()
        'System.Diagnostics.Process.Start("c:\printfile.bat")
        'System.Diagnostics.Process.Start("type/c" & " " & location & "\" & "sample.txt>\lpt1")
        Shell("print file /" & location & "\" & " sample.txt")
        'System.Diagnostics.Process.Start("type" & " " & "\" & "printfile.bat")
        'type c:\temp\sample.txt>\\g\g\lpt1 

    End Sub
    'Public Sub add_array_values()
    '    Dim abc As SqlCommand
    '    abc = New SqlCommand("select sum(total),sum(arhat),sum(laga),sum(gtotal) from bikri where entry_date=@date and session=@session and company=@company")
    '    abc.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
    '    abc.Parameters.Add("@session", SqlDbType.VarChar).Value = MDIParent1.sessionid
    '    abc.Parameters.Add("@company", SqlDbType.VarChar).Value = ComboBox1.Text
    '    Dim xyz(4) As String
    '    a7.array_list(abc, xyz, 0)
    '    TextBox1.Text = xyz(0)
    '    TextBox2.Text = xyz(1)
    '    TextBox3.Text = xyz(2)
    '    TextBox4.Text = xyz(3)
    'End Sub
    Public Sub Print_bill()
        'Dim location As String = IO.Directory.GetParent(Application.ExecutablePath).FullName
        'Dim oFile As System.IO.File
        'Dim oWrite As System.IO.StreamWriter
        ''oWrite = oFile.CreateText("c:\temp\sample.txt")
        'oWrite = oFile.CreateText(location & "\" & "sample.txt")
        Dim abc, i, dd1, dd2 As Integer
        Dim cnn As New SqlConnection
        Dim adapter As New SqlDataAdapter
        Dim tran As SqlTransaction = Nothing
        Dim abc_add, abcd, rec_c As SqlCommand
        Dim row_c As Integer
        cnn = New SqlConnection(a7.connetionstring)
        abc = ListView1.Items.Count
        i = 0
        dd1 = 0
        dd2 = abc Mod 2
        Try
            cnn.Open()
            tran = cnn.BeginTransaction("Transaction1")
            If dd2 = 1 Then
                abc = abc - 1
            End If
            'a7.delete_data(abc)
            'dotmatrix_printer()
            abcd = New SqlCommand("delete from printing") With {
                .Connection = cnn,
                .Transaction = tran
            }
            adapter.DeleteCommand = abcd
            adapter.DeleteCommand.ExecuteNonQuery()
            tran.Save("DeleteData")
            Do While i < abc
                row_c = 0
                If ListView1.Items(i).Checked = True Then
                    rec_c = New SqlCommand("select * from bikri where entry_date=@edate and companyid=@companyid and cid=@cid and sessionid=@sessionid")
                    rec_c.Parameters.Add("@edate", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                    rec_c.Parameters.Add("@cid", SqlDbType.Int).Value = ListView1.Items(i).Text
                    rec_c.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                    rec_c.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid

                    row_c = a7.check_rec(rec_c)
                    If row_c <= 10 Then
                        TextBox8.Text = ""
                        TextBox9.Text = ""
                        TextBox10.Text = ""
                        TextBox11.Text = ""
                        TextBox12.Text = ""
                        TextBox13.Text = ""
                        TextBox14.Text = ""
                        TextBox15.Text = ""
                        TextBox16.Text = ""
                        TextBox17.Text = ""
                        a7.billstring = dd1
                        'oWrite.WriteLine("{0,20}{1,-20}{2,-5}{3,-20}{4,-20}", New String(" ", 20), ComboBox1.Text, New String(" ", 5), New String(" ", 20), ComboBox1.Text)
                        'oWrite.WriteLine("{0,-30}{1,10}{2,-5}{3,-30}{4,10}", ListView1.Items(i).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"), New String(" ", 5), ListView1.Items(i + 1).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"))
                        'oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                        'oWrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", "Item", "Nag", "Weight", "Rate", "Amount", New String(" ", 5), "Item", "Nag", "Weight", "Rate", "Amount")
                        'oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                        ''a7.print_bill(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite)
                        a7.print_bill_bookwise(ListView1.Items(i).Text, DateTimePicker1, companyid, ListView1.Items(i + 1).Text)
                        'oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Safi", Format(Val(ListView1.Items(i).SubItems(2).Text), "0.00"), New String(" ", 5), "Safi", Format(Val(ListView1.Items(i + 1).SubItems(2).Text), "0.00"))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Arhat", Format(Val(ListView1.Items(i).SubItems(3).Text), "0.00"), New String(" ", 5), "Arhat", Format(Val(ListView1.Items(i + 1).SubItems(3).Text), "0.00"))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Laga", Format(Val(ListView1.Items(i).SubItems(4).Text), "0.00"), New String(" ", 5), "Laga", Format(Val(ListView1.Items(i + 1).SubItems(4).Text), "0.00"))
                        'oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Total", Format(Val(ListView1.Items(i).SubItems(5).Text), "0.00"), New String(" ", 5), "Total", Format(Val(ListView1.Items(i + 1).SubItems(5).Text), "0.00"))
                        'oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Prv.Balance", Format(Val(ListView1.Items(i).SubItems(6).Text), "0.00"), New String(" ", 5), "Prv.Balance", Format(Val(ListView1.Items(i + 1).SubItems(6).Text), "0.00"))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Advances -", Format(Val(ListView1.Items(i).SubItems(7).Text), "0.00"), New String(" ", 5), "Advances -", Format(Val(ListView1.Items(i + 1).SubItems(7).Text), "0.00"))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Grand Total", Format(Val(ListView1.Items(i).SubItems(8).Text), "0.00"), New String(" ", 5), "Grand Total", Format(Val(ListView1.Items(i + 1).SubItems(8).Text), "0.00"))
                        'oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                        Dim add_rec, add_rec1 As SqlCommand
                        Dim voucher_type As String
                        voucher_type = "Receipt"
                        add_rec = New SqlCommand("select amount,date from voucher where cid=@cid and companyid=@companyid and sessionid=@sessionid and voucher_type='" & voucher_type & "' and date<=@dt1 order by voucher_id desc ")
                        add_rec.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                        add_rec.Parameters.Add("@cid", SqlDbType.Int).Value = ListView1.Items(i).Text
                        add_rec.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                        add_rec.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
                        Dim xyz3(2) As String
                        a7.array_list(add_rec, xyz3, 0)

                        add_rec1 = New SqlCommand("select amount,date from voucher where cid=@cid and companyid=@companyid and sessionid=@sessionid and voucher_type='" & voucher_type & "' and date<=@dt1 order by voucher_id desc ")
                        add_rec1.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                        add_rec1.Parameters.Add("@cid", SqlDbType.Int).Value = ListView1.Items(i + 1).Text
                        add_rec1.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                        add_rec1.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
                        Dim xyz4(2) As String
                        a7.array_list(add_rec1, xyz4, 0)
                        'oWrite.WriteLine("{0,-12}{1,9}{2,8}{3,10}{4,5}{5,-12}{6,8}{7,8}{8,10}", "Received Rs.", Format(Val(xyz3(0)), "0.00"), " On Date ", xyz3(1), New String(" ", 5), "Received Rs.", Format(Val(xyz4(0)), "0.00"), " On Date ", xyz4(1))
                        'oWrite.WriteLine()
                        'oWrite.WriteLine()

                        abc_add = New SqlCommand("insert into printing(t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15,t16,t17,t18,t19,t20,t21,t22,t23,t24,t25,t26,t27,t28,t29)values(@t1,@t2,@t3,@t4,@t5,@t6,@t7,@t8,@t9,@t10,@t11,@t12,@t13,@t14,@t15,@t16,@t17,@t18,@t19,@t20,@t21,@t22,@t23,@t24,@t25,@t26,@t27,@t28,@t29)")
                        abc_add.Parameters.Add("@t1", SqlDbType.VarChar).Value = DateTimePicker1.Value.ToShortDateString
                        abc_add.Parameters.Add("@t2", SqlDbType.VarChar).Value = ListView1.Items(i).SubItems(1).Text
                        abc_add.Parameters.Add("@t3", SqlDbType.VarChar).Value = TextBox8.Text
                        abc_add.Parameters.Add("@t4", SqlDbType.VarChar).Value = TextBox10.Text
                        abc_add.Parameters.Add("@t5", SqlDbType.VarChar).Value = TextBox11.Text
                        abc_add.Parameters.Add("@t6", SqlDbType.VarChar).Value = TextBox12.Text
                        abc_add.Parameters.Add("@t7", SqlDbType.VarChar).Value = TextBox13.Text
                        abc_add.Parameters.Add("@t8", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(2).Text), "0.00")
                        abc_add.Parameters.Add("@t9", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(3).Text), "0.00")
                        abc_add.Parameters.Add("@t10", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(4).Text), "0.00")
                        abc_add.Parameters.Add("@t11", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(5).Text), "0.00")
                        abc_add.Parameters.Add("@t12", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(6).Text), "0.00")
                        abc_add.Parameters.Add("@t13", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(7).Text), "0.00")
                        abc_add.Parameters.Add("@t14", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(8).Text), "0.00")

                        abc_add.Parameters.Add("@t15", SqlDbType.VarChar).Value = DateTimePicker1.Value.ToShortDateString
                        abc_add.Parameters.Add("@t16", SqlDbType.VarChar).Value = ListView1.Items(i + 1).SubItems(1).Text
                        abc_add.Parameters.Add("@t17", SqlDbType.VarChar).Value = TextBox9.Text
                        abc_add.Parameters.Add("@t18", SqlDbType.VarChar).Value = TextBox14.Text
                        abc_add.Parameters.Add("@t19", SqlDbType.VarChar).Value = TextBox15.Text
                        abc_add.Parameters.Add("@t20", SqlDbType.VarChar).Value = TextBox16.Text
                        abc_add.Parameters.Add("@t21", SqlDbType.VarChar).Value = TextBox17.Text
                        abc_add.Parameters.Add("@t22", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(2).Text), "0.00")
                        abc_add.Parameters.Add("@t23", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(3).Text), "0.00")
                        abc_add.Parameters.Add("@t24", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(4).Text), "0.00")
                        abc_add.Parameters.Add("@t25", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(5).Text), "0.00")
                        abc_add.Parameters.Add("@t26", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(6).Text), "0.00")
                        abc_add.Parameters.Add("@t27", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(7).Text), "0.00")
                        abc_add.Parameters.Add("@t28", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(8).Text), "0.00")
                        abc_add.Parameters.Add("@t29", SqlDbType.VarChar).Value = i
                        abc_add.Connection = cnn
                        abc_add.Transaction = tran
                        adapter.InsertCommand = abc_add
                        adapter.InsertCommand.ExecuteNonQuery()
                        tran.Save("Save3")
                        'a7.insert_data1(abc_add)
                    End If
                End If
                i = i + 2
                'MsgBox(TextBox8.Text)
            Loop
            If dd2 = 1 Then
                If row_c <= 10 Then

                    TextBox8.Text = ""
                    TextBox9.Text = ""
                    TextBox10.Text = ""
                    TextBox11.Text = ""
                    TextBox12.Text = ""
                    TextBox13.Text = ""
                    TextBox14.Text = ""
                    TextBox15.Text = ""
                    TextBox16.Text = ""
                    TextBox17.Text = ""
                    'oWrite.WriteLine("{0,20}{1,-20}", New String(" ", 20), ComboBox1.Text)
                    'oWrite.WriteLine("{0,-30}{1,10}", ListView1.Items(abc).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"))
                    'oWrite.WriteLine(New String("-", 40))
                    'oWrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}", "Item", "Nag", "Weight", "Rate", "Amount")
                    'oWrite.WriteLine(New String("-", 40))
                    ''a7.print_bill(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite)
                    a7.print_bill_laser(ListView1.Items(abc).Text, DateTimePicker1, companyid)
                    'oWrite.WriteLine(New String("-", 40))
                    'oWrite.WriteLine("{0,30}{1,10}", "Safi", Format(Val(ListView1.Items(abc).SubItems(2).Text), "0.00"))
                    'oWrite.WriteLine("{0,30}{1,10}", "Arhat", Format(Val(ListView1.Items(abc).SubItems(3).Text), "0.00"))
                    'oWrite.WriteLine("{0,30}{1,10}", "Laga", Format(Val(ListView1.Items(abc).SubItems(4).Text), "0.00"))
                    'oWrite.WriteLine("{0,40}", New String("-", 40))
                    'oWrite.WriteLine("{0,30}{1,10}", "Total", Format(Val(ListView1.Items(abc).SubItems(5).Text), "0.00"))
                    'oWrite.WriteLine("{0,40}", New String("-", 40))
                    'oWrite.WriteLine("{0,30}{1,10}", "Prv.Balance", Format(Val(ListView1.Items(abc).SubItems(6).Text), "0.00"))
                    'oWrite.WriteLine("{0,30}{1,10}", "Advances -", Format(Val(ListView1.Items(abc).SubItems(7).Text), "0.00"))
                    'oWrite.WriteLine("{0,30}{1,10}", "Grand Total", Format(Val(ListView1.Items(abc).SubItems(8).Text), "0.00"))
                    'oWrite.WriteLine("{0,40}", New String("-", 40))
                    Dim add_rec2 As SqlCommand
                    Dim voucher_type As String
                    voucher_type = "Receipt"
                    add_rec2 = New SqlCommand("select amount,date from voucher where cid=@cid and companyid=@companyid and sessionid=@sessionid and voucher_type='" & voucher_type & "' and date<=@dt1 order by voucher_id desc ")
                    add_rec2.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                    add_rec2.Parameters.Add("@cid", SqlDbType.Int).Value = ListView1.Items(abc).Text
                    add_rec2.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                    add_rec2.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
                    Dim xyz5(2) As String
                    a7.array_list(add_rec2, xyz5, 0)
                    'oWrite.WriteLine("{0,-12}{1,9}{2,8}{3,10}", "Received Rs.", Format(Val(xyz5(0)), "0.00"), " On Date ", xyz5(1))
                    'oWrite.WriteLine()
                    'oWrite.WriteLine()

                    abc_add = New SqlCommand("insert into printing(t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t29)values(@t1,@t2,@t3,@t4,@t5,@t6,@t7,@t8,@t9,@t10,@t11,@t12,@t13,@t14,@t29)")
                    abc_add.Parameters.Add("@t1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                    abc_add.Parameters.Add("@t2", SqlDbType.VarChar).Value = ListView1.Items(i).SubItems(1).Text
                    abc_add.Parameters.Add("@t3", SqlDbType.VarChar).Value = TextBox8.Text
                    abc_add.Parameters.Add("@t4", SqlDbType.VarChar).Value = TextBox10.Text
                    abc_add.Parameters.Add("@t5", SqlDbType.VarChar).Value = TextBox11.Text
                    abc_add.Parameters.Add("@t6", SqlDbType.VarChar).Value = TextBox12.Text
                    abc_add.Parameters.Add("@t7", SqlDbType.VarChar).Value = TextBox13.Text
                    abc_add.Parameters.Add("@t8", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(2).Text
                    abc_add.Parameters.Add("@t9", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(3).Text
                    abc_add.Parameters.Add("@t10", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(4).Text
                    abc_add.Parameters.Add("@t11", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(5).Text
                    abc_add.Parameters.Add("@t12", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(6).Text
                    abc_add.Parameters.Add("@t13", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(7).Text
                    abc_add.Parameters.Add("@t14", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(8).Text
                    abc_add.Parameters.Add("@t29", SqlDbType.VarChar).Value = abc
                    'a7.insert_data1(abc_add)
                    'MsgBox(TextBox8.Text)
                    abc_add.Connection = cnn
                    abc_add.Transaction = tran
                    adapter.InsertCommand = abc_add
                    adapter.InsertCommand.ExecuteNonQuery()
                    tran.Save("Save3")
                    'tran.Commit()
                End If
            End If
            'abc_add.Connection = cnn
            'abc_add.Transaction = tran
            'adapter.InsertCommand = abc_add
            'adapter.InsertCommand.ExecuteNonQuery()
            'tran.Save("Save3")
            tran.Commit()
            ' oWrite.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            tran.Rollback()
        Finally
            cnn.Close()
        End Try
        billprinting_report.abcd = ComboBox1.Text
        billprinting_report.bill_6()
        billprinting_report.Show()
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
    Public Sub Print_bill_12()
        'Dim location As String = IO.Directory.GetParent(Application.ExecutablePath).FullName
        'Dim oFile As System.IO.File
        'Dim oWrite As System.IO.StreamWriter
        'oWrite = oFile.CreateText("c:\temp\sample.txt")
        'oWrite = oFile.CreateText(location & "\" & "sample.txt")
        Dim abc, i, dd1, dd2 As Integer
        Dim row_c As Integer
        Dim cnn As New SqlConnection
        Dim adapter As New SqlDataAdapter
        Dim tran As SqlTransaction = Nothing
        Dim abc_add, abcd, rec_c As SqlCommand
        Dim ds As New DataSet
        cnn = New SqlConnection(a7.connetionstring)
        abc = ListView1.Items.Count
        i = 0
        dd1 = 0
        dd2 = abc Mod 2
        Try
            cnn.Open()
            tran = cnn.BeginTransaction("Transaction1")
            If dd2 = 1 Then
                abc = abc - 1
            End If
            'a7.delete_data(abc)
            'dotmatrix_printer()
            abcd = New SqlCommand("delete from printing") With {
                .Connection = cnn,
                .Transaction = tran
            }
            adapter.DeleteCommand = abcd
            adapter.DeleteCommand.ExecuteNonQuery()
            tran.Save("DeleteData")
            Do While i < abc
                row_c = 0
                If ListView1.Items(i).Checked = True Then

                    rec_c = New SqlCommand("select * from bikri where entry_date=@edate and companyid=@companyid and cid=@cid and sessionid=@sessionid")
                    rec_c.Parameters.Add("@edate", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                    rec_c.Parameters.Add("@cid", SqlDbType.Int).Value = ListView1.Items(i).Text
                    rec_c.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                    rec_c.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
                    row_c = a7.check_rec(rec_c)
                    If row_c > 10 Then
                        TextBox8.Text = ""
                        TextBox9.Text = ""
                        TextBox10.Text = ""
                        TextBox11.Text = ""
                        TextBox12.Text = ""
                        TextBox13.Text = ""
                        TextBox14.Text = ""
                        TextBox15.Text = ""
                        TextBox16.Text = ""
                        TextBox17.Text = ""
                        'a7.billstring = dd1
                        'oWrite.WriteLine("{0,20}{1,-20}{2,-5}{3,-20}{4,-20}", New String(" ", 20), ComboBox1.Text, New String(" ", 5), New String(" ", 20), ComboBox1.Text)
                        'oWrite.WriteLine("{0,-30}{1,10}{2,-5}{3,-30}{4,10}", ListView1.Items(i).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"), New String(" ", 5), ListView1.Items(i + 1).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"))
                        'oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                        'oWrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", "Item", "Nag", "Weight", "Rate", "Amount", New String(" ", 5), "Item", "Nag", "Weight", "Rate", "Amount")
                        'oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                        ''a7.print_bill(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite)
                        a7.print_bill_bookwise12(ListView1.Items(i).Text, DateTimePicker1, companyid, ListView1.Items(i + 1).Text)
                        'oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Safi", Format(Val(ListView1.Items(i).SubItems(2).Text), "0.00"), New String(" ", 5), "Safi", Format(Val(ListView1.Items(i + 1).SubItems(2).Text), "0.00"))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Arhat", Format(Val(ListView1.Items(i).SubItems(3).Text), "0.00"), New String(" ", 5), "Arhat", Format(Val(ListView1.Items(i + 1).SubItems(3).Text), "0.00"))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Laga", Format(Val(ListView1.Items(i).SubItems(4).Text), "0.00"), New String(" ", 5), "Laga", Format(Val(ListView1.Items(i + 1).SubItems(4).Text), "0.00"))
                        'oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Total", Format(Val(ListView1.Items(i).SubItems(5).Text), "0.00"), New String(" ", 5), "Total", Format(Val(ListView1.Items(i + 1).SubItems(5).Text), "0.00"))
                        'oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Prv.Balance", Format(Val(ListView1.Items(i).SubItems(6).Text), "0.00"), New String(" ", 5), "Prv.Balance", Format(Val(ListView1.Items(i + 1).SubItems(6).Text), "0.00"))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Advances -", Format(Val(ListView1.Items(i).SubItems(7).Text), "0.00"), New String(" ", 5), "Advances -", Format(Val(ListView1.Items(i + 1).SubItems(7).Text), "0.00"))
                        'oWrite.WriteLine("{0,30}{1,10}{2,5}{3,30}{4,10}", "Grand Total", Format(Val(ListView1.Items(i).SubItems(8).Text), "0.00"), New String(" ", 5), "Grand Total", Format(Val(ListView1.Items(i + 1).SubItems(8).Text), "0.00"))
                        'oWrite.WriteLine("{0,40}{1,5}{2,40}", New String("-", 40), New String(" ", 5), New String("-", 40))
                        Dim add_rec, add_rec1 As SqlCommand
                        Dim voucher_type As String
                        voucher_type = "Receipt"
                        add_rec = New SqlCommand("select amount,date from voucher where cid=@cid and companyid=@companyid and sessionid=@sessionid and voucher_type='" & voucher_type & "' and date<=@dt1 order by voucher_id desc ")
                        add_rec.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                        add_rec.Parameters.Add("@cid", SqlDbType.Int).Value = ListView1.Items(i).Text
                        add_rec.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                        add_rec.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
                        Dim xyz3(2) As String
                        a7.array_list(add_rec, xyz3, 0)

                        add_rec1 = New SqlCommand("select amount,date from voucher where cid=@cid and companyid=@companyid and sessionid=@sessionid and voucher_type='" & voucher_type & "' and date<=@dt1 order by voucher_id desc ")
                        add_rec1.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                        add_rec1.Parameters.Add("@cid", SqlDbType.Int).Value = ListView1.Items(i + 1).Text
                        add_rec1.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                        add_rec1.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
                        Dim xyz4(2) As String
                        a7.array_list(add_rec1, xyz4, 0)
                        'oWrite.WriteLine("{0,-12}{1,9}{2,8}{3,10}{4,5}{5,-12}{6,8}{7,8}{8,10}", "Received Rs.", Format(Val(xyz3(0)), "0.00"), " On Date ", xyz3(1), New String(" ", 5), "Received Rs.", Format(Val(xyz4(0)), "0.00"), " On Date ", xyz4(1))
                        'oWrite.WriteLine()
                        'oWrite.WriteLine()

                        abc_add = New SqlCommand("insert into printing(t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15,t16,t17,t18,t19,t20,t21,t22,t23,t24,t25,t26,t27,t28,t29)values(@t1,@t2,@t3,@t4,@t5,@t6,@t7,@t8,@t9,@t10,@t11,@t12,@t13,@t14,@t15,@t16,@t17,@t18,@t19,@t20,@t21,@t22,@t23,@t24,@t25,@t26,@t27,@t28,@t29)")
                        abc_add.Parameters.Add("@t1", SqlDbType.VarChar).Value = DateTimePicker1.Value.ToShortDateString
                        abc_add.Parameters.Add("@t2", SqlDbType.VarChar).Value = ListView1.Items(i).SubItems(1).Text
                        abc_add.Parameters.Add("@t3", SqlDbType.VarChar).Value = TextBox8.Text
                        abc_add.Parameters.Add("@t4", SqlDbType.VarChar).Value = TextBox10.Text
                        abc_add.Parameters.Add("@t5", SqlDbType.VarChar).Value = TextBox11.Text
                        abc_add.Parameters.Add("@t6", SqlDbType.VarChar).Value = TextBox12.Text
                        abc_add.Parameters.Add("@t7", SqlDbType.VarChar).Value = TextBox13.Text
                        abc_add.Parameters.Add("@t8", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(2).Text), "0.00")
                        abc_add.Parameters.Add("@t9", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(3).Text), "0.00")
                        abc_add.Parameters.Add("@t10", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(4).Text), "0.00")
                        abc_add.Parameters.Add("@t11", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(5).Text), "0.00")
                        abc_add.Parameters.Add("@t12", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(6).Text), "0.00")
                        abc_add.Parameters.Add("@t13", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(7).Text), "0.00")
                        abc_add.Parameters.Add("@t14", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i).SubItems(8).Text), "0.00")

                        abc_add.Parameters.Add("@t15", SqlDbType.VarChar).Value = DateTimePicker1.Value.ToShortDateString
                        abc_add.Parameters.Add("@t16", SqlDbType.VarChar).Value = ListView1.Items(i + 1).SubItems(1).Text
                        abc_add.Parameters.Add("@t17", SqlDbType.VarChar).Value = TextBox9.Text
                        abc_add.Parameters.Add("@t18", SqlDbType.VarChar).Value = TextBox14.Text
                        abc_add.Parameters.Add("@t19", SqlDbType.VarChar).Value = TextBox15.Text
                        abc_add.Parameters.Add("@t20", SqlDbType.VarChar).Value = TextBox16.Text
                        abc_add.Parameters.Add("@t21", SqlDbType.VarChar).Value = TextBox17.Text
                        abc_add.Parameters.Add("@t22", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(2).Text), "0.00")
                        abc_add.Parameters.Add("@t23", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(3).Text), "0.00")
                        abc_add.Parameters.Add("@t24", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(4).Text), "0.00")
                        abc_add.Parameters.Add("@t25", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(5).Text), "0.00")
                        abc_add.Parameters.Add("@t26", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(6).Text), "0.00")
                        abc_add.Parameters.Add("@t27", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(7).Text), "0.00")
                        abc_add.Parameters.Add("@t28", SqlDbType.VarChar).Value = Format(Val(ListView1.Items(i + 1).SubItems(8).Text), "0.00")
                        abc_add.Parameters.Add("@t29", SqlDbType.VarChar).Value = i
                        abc_add.Connection = cnn
                        abc_add.Transaction = tran
                        adapter.InsertCommand = abc_add
                        adapter.InsertCommand.ExecuteNonQuery()
                        tran.Save("Save3")
                        'a7.insert_data1(abc_add)
                    End If
                End If
                i = i + 2
                'MsgBox(TextBox8.Text)
            Loop
            If dd2 = 1 Then
                If row_c > 10 Then
                    TextBox8.Text = ""
                    TextBox9.Text = ""
                    TextBox10.Text = ""
                    TextBox11.Text = ""
                    TextBox12.Text = ""
                    TextBox13.Text = ""
                    TextBox14.Text = ""
                    TextBox15.Text = ""
                    TextBox16.Text = ""
                    TextBox17.Text = ""
                    'oWrite.WriteLine("{0,20}{1,-20}", New String(" ", 20), ComboBox1.Text)
                    'oWrite.WriteLine("{0,-30}{1,10}", ListView1.Items(abc).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"))
                    'oWrite.WriteLine(New String("-", 40))
                    'oWrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}", "Item", "Nag", "Weight", "Rate", "Amount")
                    'oWrite.WriteLine(New String("-", 40))
                    ''a7.print_bill(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite)
                    a7.print_bill_laser(ListView1.Items(abc).Text, DateTimePicker1, companyid)
                    'oWrite.WriteLine(New String("-", 40))
                    'oWrite.WriteLine("{0,30}{1,10}", "Safi", Format(Val(ListView1.Items(abc).SubItems(2).Text), "0.00"))
                    'oWrite.WriteLine("{0,30}{1,10}", "Arhat", Format(Val(ListView1.Items(abc).SubItems(3).Text), "0.00"))
                    'oWrite.WriteLine("{0,30}{1,10}", "Laga", Format(Val(ListView1.Items(abc).SubItems(4).Text), "0.00"))
                    'oWrite.WriteLine("{0,40}", New String("-", 40))
                    'oWrite.WriteLine("{0,30}{1,10}", "Total", Format(Val(ListView1.Items(abc).SubItems(5).Text), "0.00"))
                    'oWrite.WriteLine("{0,40}", New String("-", 40))
                    'oWrite.WriteLine("{0,30}{1,10}", "Prv.Balance", Format(Val(ListView1.Items(abc).SubItems(6).Text), "0.00"))
                    'oWrite.WriteLine("{0,30}{1,10}", "Advances -", Format(Val(ListView1.Items(abc).SubItems(7).Text), "0.00"))
                    'oWrite.WriteLine("{0,30}{1,10}", "Grand Total", Format(Val(ListView1.Items(abc).SubItems(8).Text), "0.00"))
                    'oWrite.WriteLine("{0,40}", New String("-", 40))
                    Dim add_rec2 As SqlCommand
                    Dim voucher_type As String
                    voucher_type = "Receipt"
                    add_rec2 = New SqlCommand("select amount,date from voucher where cid=@cid and companyid=@companyid and sessionid=@sessionid and voucher_type='" & voucher_type & "' and date<=@dt1 order by voucher_id desc ")
                    add_rec2.Parameters.Add("@dt1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                    add_rec2.Parameters.Add("@cid", SqlDbType.Int).Value = ListView1.Items(abc).Text
                    add_rec2.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
                    add_rec2.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
                    Dim xyz5(2) As String
                    a7.array_list(add_rec2, xyz5, 0)
                    'oWrite.WriteLine("{0,-12}{1,9}{2,8}{3,10}", "Received Rs.", Format(Val(xyz5(0)), "0.00"), " On Date ", xyz5(1))
                    'oWrite.WriteLine()
                    'oWrite.WriteLine()
                    abc_add = New SqlCommand("insert into printing(t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t29)values(@t1,@t2,@t3,@t4,@t5,@t6,@t7,@t8,@t9,@t10,@t11,@t12,@t13,@t14,@t29)")
                    abc_add.Parameters.Add("@t1", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
                    abc_add.Parameters.Add("@t2", SqlDbType.VarChar).Value = ListView1.Items(i).SubItems(1).Text
                    abc_add.Parameters.Add("@t3", SqlDbType.VarChar).Value = TextBox8.Text
                    abc_add.Parameters.Add("@t4", SqlDbType.VarChar).Value = TextBox10.Text
                    abc_add.Parameters.Add("@t5", SqlDbType.VarChar).Value = TextBox11.Text
                    abc_add.Parameters.Add("@t6", SqlDbType.VarChar).Value = TextBox12.Text
                    abc_add.Parameters.Add("@t7", SqlDbType.VarChar).Value = TextBox13.Text
                    abc_add.Parameters.Add("@t8", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(2).Text
                    abc_add.Parameters.Add("@t9", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(3).Text
                    abc_add.Parameters.Add("@t10", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(4).Text
                    abc_add.Parameters.Add("@t11", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(5).Text
                    abc_add.Parameters.Add("@t12", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(6).Text
                    abc_add.Parameters.Add("@t13", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(7).Text
                    abc_add.Parameters.Add("@t14", SqlDbType.VarChar).Value = ListView1.Items(abc).SubItems(8).Text
                    abc_add.Parameters.Add("@t29", SqlDbType.VarChar).Value = abc
                    'a7.insert_data1(abc_add)
                    'MsgBox(TextBox8.Text)
                    abc_add.Connection = cnn
                    abc_add.Transaction = tran
                    adapter.InsertCommand = abc_add
                    adapter.InsertCommand.ExecuteNonQuery()
                    tran.Save("Save3")
                    'tran.Commit()
                End If
            End If
            'abc_add.Connection = cnn
            'abc_add.Transaction = tran
            'adapter.InsertCommand = abc_add
            'adapter.InsertCommand.ExecuteNonQuery()
            'tran.Save("Save3")
            tran.Commit()
            ' oWrite.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            tran.Rollback()
        Finally
            cnn.Close()
        End Try
        billprinting_report.abcd = ComboBox1.Text
        billprinting_report.Show()
        billprinting_report.bill_12()
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
    Public Sub Print_bill_laserprinter()
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        oWrite = oFile.CreateText("c:\temp\sample.txt")
        Dim abc, i, balance, balance1, dd1, dd2 As Integer
        abc = ListView1.Items.Count
        i = 0
        dd1 = 0
        Do While i < abc
            If ListView1.Items(i).Checked = True Then
                a7.billstring = dd1
                If dd1 = 0 Then
                    oWrite.WriteLine("{0,20}{1,-20}", New String(" ", 20), ComboBox1.Text)
                    oWrite.WriteLine("{0,-30}{1,10}", ListView1.Items(i).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"))
                    oWrite.WriteLine(New String("-", 40))
                    oWrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}", "Item", "Nag", "Weight", "Rate", "Amount")
                    oWrite.WriteLine(New String("-", 40))
                    'a7.print_bill(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite)
                    'a7.print_bill_bookwise(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite)
                    oWrite.WriteLine(New String("-", 40))
                    oWrite.WriteLine("{0,30}{1,10}", "Safi", Format(Val(ListView1.Items(i).SubItems(2).Text), "0.00"))
                    oWrite.WriteLine("{0,30}{1,10}", "Arhat", Format(Val(ListView1.Items(i).SubItems(3).Text), "0.00"))
                    oWrite.WriteLine("{0,30}{1,10}", "Laga", Format(Val(ListView1.Items(i).SubItems(4).Text), "0.00"))
                    oWrite.WriteLine("{0,40}", New String("-", 40))
                    oWrite.WriteLine("{0,30}{1,10}", "Total", Format(Val(ListView1.Items(i).SubItems(5).Text), "0.00"))
                    oWrite.WriteLine("{0,40}", New String("-", 40))
                    If Val(ListView1.Items(i).SubItems(6).Text) > 0 Then
                        oWrite.WriteLine("{0,30}{1,10}", "Prv.Balance", Format(Val(ListView1.Items(i).SubItems(6).Text), "0.00"))
                    End If
                    If Val(ListView1.Items(i).SubItems(7).Text) > 0 Then
                        oWrite.WriteLine("{0,30}{1,10}", "Advances -", Format(Val(ListView1.Items(i).SubItems(7).Text), "0.00"))
                    End If
                    oWrite.WriteLine("{0,30}{1,10}", "Grand Total", Format(Val(ListView1.Items(i).SubItems(8).Text), "0.00"))
                    oWrite.WriteLine("{0,40}", New String("-", 40))
                    oWrite.WriteLine()
                    oWrite.WriteLine()
                    dd1 = dd1 + 1
                Else
                    oWrite.WriteLine("{0,40}{1,20}{2,-20}", New String(" ", 40), New String(" ", 20), ComboBox1.Text)
                    oWrite.WriteLine("{0,40}{1,-30}{2,10}", New String(" ", 40), ListView1.Items(i).SubItems(1).Text, DateTimePicker1.Value.ToString("dd/MM/yyyy"))
                    oWrite.WriteLine("{0,40}{1,40}", New String(" ", 40), New String("-", 40))
                    oWrite.WriteLine("{0,40}{1,-10}{2,4}{3,8}{4,8}{5,10}", New String(" ", 40), "Item", "Nag", "Weight", "Rate", "Amount")
                    oWrite.WriteLine("{0,40}{1,40}", New String(" ", 40), New String("-", 40))
                    'a7.print_bill(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite)
                    'a7.print_bill_bookwise(ListView1.Items(i).Text, DateTimePicker1, companyid, oWrite)
                    oWrite.WriteLine("{0,40}{1,20}", New String(" ", 40), New String("-", 40))
                    oWrite.WriteLine("{0,40}{1,30}{2,10}", New String(" ", 40), "Safi", Format(Val(ListView1.Items(i).SubItems(2).Text), "0.00"))
                    oWrite.WriteLine("{0,40}{1,30}{2,10}", New String(" ", 40), "Arhat", Format(Val(ListView1.Items(i).SubItems(3).Text), "0.00"))
                    oWrite.WriteLine("{0,40}{1,30}{2,10}", New String(" ", 40), "Laga", Format(Val(ListView1.Items(i).SubItems(4).Text), "0.00"))
                    oWrite.WriteLine("{0,40}{1,40}", New String(" ", 40), New String("-", 40))
                    oWrite.WriteLine("{0,40}{1,30}{2,10}", New String(" ", 40), "Total", Format(Val(ListView1.Items(i).SubItems(5).Text), "0.00"))
                    oWrite.WriteLine("{0,40}{1,40}", New String(" ", 40), New String("-", 40))
                    If Val(ListView1.Items(i).SubItems(6).Text) > 0 Then
                        oWrite.WriteLine("{0,40}{1,30}{2,10}", New String(" ", 40), "Prv.Balance", Format(Val(ListView1.Items(i).SubItems(6).Text), "0.00"))
                    End If
                    If Val(ListView1.Items(i).SubItems(7).Text) > 0 Then
                        oWrite.WriteLine("{0,40}{1,30}{2,10}", New String(" ", 40), "Advances -", Format(Val(ListView1.Items(i).SubItems(7).Text), "0.00"))
                    End If
                    oWrite.WriteLine("{0,40}{1,30}{2,10}", New String(" ", 40), "Grand Total", Format(Val(ListView1.Items(i).SubItems(8).Text), "0.00"))
                    oWrite.WriteLine("{0,40}{1,40}", New String(" ", 40), New String("-", 40))
                    oWrite.WriteLine()
                    oWrite.WriteLine()

                    dd1 = 0
                End If

            End If
            i = i + 1
        Loop
        oWrite.Close()
        Dim p As New Process
        Dim info As New ProcessStartInfo With {
            .FileName = "c:\temp\sample.txt"
        }
        'info.Verb = "print"
        p.StartInfo = info
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo.RedirectStandardOutput = False
        p.Start()
        'System.Diagnostics.Process.Start("c:\printfile.bat")
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Dim cnn1 As New SqlConnection
        'Dim adapter As New SqlDataAdapter
        'Dim tran1 As SqlTransaction = Nothing
        'Dim abcd As SqlCommand
        'cnn1 = New SqlConnection(a7.connetionstring)
        'Try
        '    cnn1.Open()
        '    abcd = New SqlCommand("delete from printing")
        '    'a7.delete_data(abc)
        '    'dotmatrix_printer()
        '    tran1 = cnn1.BeginTransaction("Transaction1")
        '    abcd.Connection = cnn1
        '    abcd.Transaction = tran1
        '    adapter.DeleteCommand = abcd
        '    adapter.DeleteCommand.ExecuteNonQuery()
        '    tran1.Save("save")
        Print_bill()
        'tran1.Commit()
        '' oWrite.Close()
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        '    tran1.Rollback()
        'Finally
        '    cnn1.Close()
        'End Try
    End Sub
    Public Sub Outstanding_list()
        Dim abc As SqlCommand
        Dim rcount, i As Integer
        Dim total, balance, prv_balance, total_balance As Long
        prv_balance = 0
        total_balance = 0
        rcount = ListView1.Items.Count
        i = 0
        Do While i <= rcount - 1
            'abc = New SqlCommand("select customerid,cname,(select sum(amount) from dbo.voucher where account_desc=@dr and sessionid=@sessionid and cid=dbo.customer.customerid and companyid=@companyid and date<=@date) - (select sum(amount) from dbo.voucher where account_desc=@cr and sessionid=@sessionid and cid=dbo.customer.customerid and companyid=@companyid and date<=@date) from dbo.customer where dbo.customer.customerid=@customerid")
            abc = New SqlCommand("select customerid,cname,(select sum(amount) from dbo.voucher where account_desc=@dr and sessionid=@sessionid and cid=dbo.customer.customerid and companyid=@companyid and date<=@date) , (select sum(amount) from dbo.voucher where account_desc=@cr and sessionid=@sessionid and cid=dbo.customer.customerid and companyid=@companyid and date<=@date) from dbo.customer where dbo.customer.customerid=@customerid")
            abc.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
            abc.Parameters.Add("@dr", SqlDbType.VarChar).Value = "DR"
            abc.Parameters.Add("@cr", SqlDbType.VarChar).Value = "CR"
            abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            abc.Parameters.Add("@customerid", SqlDbType.Int).Value = Val(ListView1.Items(i).SubItems(0).Text)
            abc.Parameters.Add("@date", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
            Dim xyz(3) As String
            a7.array_list(abc, xyz, 0)
            total = Val(xyz(2)) - Val(xyz(3))
            If Val(total) >= Val(ListView1.Items(i).SubItems(5).Text) Then
                balance = total - Val(ListView1.Items(i).SubItems(5).Text)
                ListView1.Items(i).SubItems.Add(balance)
                ListView1.Items(i).SubItems.Add("")
                ListView1.Items(i).SubItems.Add(total)
            Else
                balance = Val(ListView1.Items(i).SubItems(5).Text) - total
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
        List_add()
        ListView1.Focus()
    End Sub
    Private Sub Show_total()
        Dim abc, i As Integer
        abc = ListView1.Items.Count
        i = 0
        Dim safi, arhat, laga, total, adv, pbal, gtotal As Integer
        safi = 0
        arhat = 0
        laga = 0
        total = 0
        adv = 0
        pbal = 0
        gtotal = 0
        Do While i < abc
            safi = safi + Val(ListView1.Items(i).SubItems(2).Text)
            arhat = arhat + Val(ListView1.Items(i).SubItems(3).Text)
            laga = laga + Val(ListView1.Items(i).SubItems(4).Text)
            total = total + Val(ListView1.Items(i).SubItems(5).Text)
            adv = adv + Val(ListView1.Items(i).SubItems(6).Text)
            pbal = pbal + Val(ListView1.Items(i).SubItems(7).Text)
            gtotal = gtotal + Val(ListView1.Items(i).SubItems(8).Text)
            i = i + 1
        Loop
        TextBox1.Text = safi
        TextBox2.Text = arhat
        TextBox3.Text = laga
        TextBox4.Text = total
        TextBox5.Text = adv
        TextBox6.Text = pbal
        TextBox7.Text = gtotal

    End Sub
    Private Sub DateTimePicker1_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.CloseUp
        list_add()
        ListView1.Focus()
    End Sub
    Public Sub Printing_data_add()
        Dim abc, i As Integer
        Dim abc1 As SqlCommand
        abc = ListView1.Items.Count
        i = 0
        Do While i < abc
            abc1 = New SqlCommand("select a.name,b.nag,b.weight,b.rate,b.total,c.name from bikri b,vegetable a,book c where b.entry_date=@edate and b.companyid=@companyid and b.cid=@cid and b.sessionid=@sessionid and b.vegetableid=a.vegetableid and b.bookid=c.bookid")
            abc1.Parameters.Add("@edate", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
            abc1.Parameters.Add("@cid", SqlDbType.Int).Value = ListView1.Items(i).Text
            abc1.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            abc1.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid

            i = i + 1
        Loop
    End Sub

    Private Sub Button1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        billprinting_report.Show()
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
        oWrite.Write(ComboBox1.Text)
        oWrite.Write(")")
        oWrite.WriteLine()
        oWrite.Write(New String(" ", 1))
        oWrite.Write(New String(" ", 2))
        oWrite.Write("Consolidated Sale Report:- ")
        oWrite.Write(DateTimePicker1.Text)
        'oWrite.Write(" To :- ")
        'oWrite.Write(DateTimePicker1.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.Write("{0,-16}", ("Cust. Name"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", ("Safi"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Arhat")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Laga")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Total")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Prv.Bal")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", ("Adv."))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", ("G.total"))
        oWrite.Write(New String(" ", 1))
        'oWrite.Write("{0,8}", ("Cls. Bal"))
        'oWrite.Write(New String(" ", 1))
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        Do While i < abc
            oWrite.Write("{0,-16}", Mid(ListView1.Items(i).SubItems(1).Text, 1, 10))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(2).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(3).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(4).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(5).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(6).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(7).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(8).Text, 1, 8))
            'oWrite.Write(New String(" ", 1))
            'oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(8).Text, 1, 8))
            oWrite.WriteLine()
            oWrite.WriteLine(New String("-", 80))
            i = i + 1
        Loop
        oWrite.Write("{0,-5}", ("Total"))
        oWrite.Write(New String(" ", 12))
        oWrite.Write("{0,8}", TextBox1.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", TextBox2.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", TextBox3.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", TextBox4.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", TextBox5.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", TextBox6.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", TextBox7.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.Close()
        Shell("print file /" & location & "\" & " sample1.txt")
        'System.Diagnostics.Process.Start("c:\printlist.bat")
        'Dim p As New Process
        'Dim info As New ProcessStartInfo
        'info.FileName = location & "\" & "sample1.txt"
        'info.Verb = "print"
        'p.StartInfo = info
        'p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        'p.StartInfo.RedirectStandardOutput = False
        'p.Start()
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
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
        oWrite.Write(ComboBox1.Text)
        oWrite.Write(")")
        oWrite.WriteLine()
        oWrite.Write(New String(" ", 1))
        oWrite.Write(New String(" ", 2))
        oWrite.Write("Consolidated Sale Report:- ")
        oWrite.Write(DateTimePicker1.Text)
        'oWrite.Write(" To :- ")
        'oWrite.Write(DateTimePicker1.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.Write("{0,-16}", ("Cust. Name"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", ("Safi"))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Arhat")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Laga")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Total")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", "Prv.Bal")
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", ("Adv."))
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", ("G.total"))
        oWrite.Write(New String(" ", 1))
        'oWrite.Write("{0,8}", ("Cls. Bal"))
        'oWrite.Write(New String(" ", 1))
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        Do While i < abc
            oWrite.Write("{0,-16}", Mid(ListView1.Items(i).SubItems(1).Text, 1, 10))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(2).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(3).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(4).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(5).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(6).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(7).Text, 1, 8))
            oWrite.Write(New String(" ", 1))
            oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(8).Text, 1, 8))
            'oWrite.Write(New String(" ", 1))
            'oWrite.Write("{0,8}", Mid(ListView1.Items(i).SubItems(8).Text, 1, 8))
            oWrite.WriteLine()
            oWrite.WriteLine(New String("-", 80))
            i = i + 1
        Loop
        oWrite.Write("{0,-5}", ("Total"))
        oWrite.Write(New String(" ", 12))
        oWrite.Write("{0,8}", TextBox1.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", TextBox2.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", TextBox3.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", TextBox4.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", TextBox5.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", TextBox6.Text)
        oWrite.Write(New String(" ", 1))
        oWrite.Write("{0,8}", TextBox7.Text)
        oWrite.WriteLine()
        oWrite.WriteLine(New String("-", 80))
        oWrite.Close()
        'System.Diagnostics.Process.Start("c:\printlist.bat")
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
    End Sub
    Private Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        dotmatrix_printer()
    End Sub

    Private Sub Button7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button7.Click
        
        print_bill_12()
    End Sub
End Class