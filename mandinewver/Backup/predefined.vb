Imports System.Data.SqlClient
Public Class predefined
    Public connetionstring As String
    Public cnn, cnn1, cnn2, cnn3, cnn4, cnn5, cnn6, cnn7 As SqlConnection
    Public billstring As Integer
    Dim command, command1 As SqlCommand
    Dim reader, reader1, reader2, reader3, reader4, reader5, reader6, reader7, reader8 As SqlDataReader
    Public Sub New()
        ''connetionstring = "Data Source=.\SQLEXPRESS;AttachDbFilename=|datadirectory|\mydata.mdf;Integrated Security=True;User Instance=True"
        'connetionstring = "Data Source=server\SQLEXPRESS;AttachDbFilename=|datadirectory|\mydata.mdf;Integrated Security=True"
        ''connetionstring = "Data Source=server\SQLEXPRESS;AttachDbFilename=d:\mandidata\mydata.mdf;user id=sa;password=abc"
        connetionstring = "Data Source=.\SQLEXPRESS;AttachDbFilename=|datadirectory|\mydata.mdf;user id=sa;password=abc"
        'connetionstring = "Data Source=server\SQLEXPRESS;AttachDbFilename=d:\vega1\mydata.mdf;user id=sa;password=abc"
        'connetionstring = "server=SERVER\SQLEXPRESS;database=d:\mandidata\mydata.mdf;user id=sa;password=abc"
        'Dim path As String
        'path = Application.StartupPath & "\mydata.mdf"
        'If My.Computer.FileSystem.FileExists(path) Then

        'MessageBox.Show("File is available " & path)
        'Else
        'MessageBox.Show("File is Not available " & path)
        'End If
        ''connetionstring = "Data Source=server\SQLEXPRESS;AttachDbFilename=d:\vega1\mydata.mdf;user id=sa;password=abc"
        cnn = New SqlConnection(connetionstring)
        cnn1 = New SqlConnection(connetionstring)
        cnn2 = New SqlConnection(connetionstring)
        cnn3 = New SqlConnection(connetionstring)
        cnn4 = New SqlConnection(connetionstring)
        cnn5 = New SqlConnection(connetionstring)
        cnn6 = New SqlConnection(connetionstring)
        cnn7 = New SqlConnection(connetionstring)
    End Sub
    Public Sub refresh_all(ByVal dform As GroupBox)
        Dim c As Control
        For Each c In dform.Controls
            If TypeOf c Is TextBox Then
                c.Text = ""
            End If
        Next
    End Sub
    Public Sub refresh_form(ByVal dform As Form)
        Dim c As Control
        For Each c In dform.Controls
            If TypeOf c Is TextBox Then
                c.Text = ""
            End If
        Next
    End Sub
    Public Function trapkey(ByVal kcode As String) As Boolean
        If (kcode >= 48 And kcode <= 57) Or kcode = 8 Or kcode = 46 Then
            trapkey = False
        Else
            trapkey = True
        End If
    End Function
    Public Sub DoConvert(ByVal dtext As TextBox)
        Dim a As Integer = dtext.SelectionStart
        dtext.Text = UCase(dtext.Text)
        dtext.SelectionStart = a
    End Sub
    Public Sub insert_data(ByVal istring As String)
        Dim adapter As New SqlDataAdapter
        Try
            cnn.Open()
            adapter.InsertCommand = New SqlCommand(istring, cnn)
            adapter.InsertCommand.ExecuteNonQuery()
            cnn.Close()
        Catch ex As SqlException
            If ex.Number = 2601 Or ex.Number = 2627 Then
                MsgBox("Can't Insert Duplicate Value")
            Else
                MsgBox(ex.Message)
            End If
            cnn.Close()
        End Try
    End Sub
    Public Sub list_add(ByVal command As SqlCommand, ByVal lv As ListView)
        lv.Items.Clear()
        Dim row As Integer
        Try
            cnn.Open()
            command.Connection = cnn
            reader = command.ExecuteReader
            row = 0
            Dim column As Integer
            While reader.Read
                column = 1
                lv.Items.Add(reader.Item(0))
                Do While column <= reader.FieldCount - 1
                    If IsDBNull(reader.Item(column)) Then
                        lv.Items(row).SubItems.Add(" ")
                    Else
                        lv.Items(row).SubItems.Add(reader.Item(column))
                    End If
                    column = column + 1
                Loop
                row = row + 1
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            cnn.Close()
        End Try
    End Sub
    Public Sub abc_add(ByVal astring As String, ByVal dcombo As ComboBox)
        Try
            cnn.Open()
            command = New SqlCommand(astring, cnn)
            reader = command.ExecuteReader
            While reader.Read
                dcombo.Items.Add(reader.Item(0))
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub listbox_add(ByVal command As SqlCommand, ByVal list1 As ListBox)
        list1.Items.Clear()
        Try
            cnn.Open()
            command.Connection = cnn
            reader = command.ExecuteReader
            While reader.Read
                list1.Items.Add(reader.Item(0))
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub delete_data(ByVal command As SqlCommand)
        Dim adapter As New SqlDataAdapter
        Try
            cnn.Open()
            command.Connection = cnn
            adapter.DeleteCommand = command
            adapter.DeleteCommand.ExecuteNonQuery()
            cnn.Close()
        Catch ex As SqlException
            If ex.Number = 547 Then
                MsgBox("Can't be deleted..!!...it contain's records")
            Else
                MsgBox(ex.Message)
            End If
            cnn.Close()
        End Try
    End Sub
    Public Sub update_data(ByVal command As SqlCommand)
        Dim adapter1 As New SqlDataAdapter
        Try
            cnn.Open()
            command.Connection = cnn
            adapter1.UpdateCommand = command
            adapter1.UpdateCommand.ExecuteNonQuery()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub ledger_add(ByVal command As SqlCommand, ByVal lv As ListView)
        lv.Items.Clear()
        Dim row As Integer
        Try
            cnn.Open()
            command.Connection = cnn
            reader = command.ExecuteReader
            row = 0
            Dim column As Integer
            Dim balance1 As Long
            balance1 = 0
            While reader.Read
                column = 1
                lv.Items.Add(reader.Item(0))
                Do While column <= reader.FieldCount - 1
                    lv.Items(row).SubItems.Add(reader.Item(column))
                    column = column + 1
                Loop
                Dim balance As Long
                balance = Val(lv.Items(row).SubItems(5).Text) - Val(lv.Items(row).SubItems(6).Text)
                balance1 = balance1 + balance
                lv.Items(row).SubItems.Add(balance1)
                row = row + 1
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub outstanding_add(ByVal astring As String, ByVal lv As ListView)
        lv.Items.Clear()
        Dim row As Integer
        Try
            cnn.Open()
            command = New SqlCommand(astring, cnn)
            reader = command.ExecuteReader
            row = 0
            Dim balance1 As Long
            While reader.Read
                balance1 = reader.Item(3) - reader.Item(4)
                If balance1 > 0 Then
                    lv.Items.Add(reader.Item(0))
                    lv.Items(row).SubItems.Add(balance1)
                    row = row + 1
                End If
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub GetTransactionData(ByVal command As SqlCommand, ByVal lv As ListView)
        lv.Items.Clear()
        Dim row As Integer
        Try
            cnn.Open()
            command.Connection = cnn
            reader = command.ExecuteReader()
            Dim balance1 As Long
            row = 0
            While reader.Read
                balance1 = reader.Item(1) - reader.Item(2)
                If balance1 > 0 Then
                    lv.Items.Add(reader.Item(0))
                    lv.Items(row).SubItems.Add(balance1)
                    row = row + 1
                End If
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub array_list(ByVal command As SqlCommand, ByVal alist() As String, ByVal fstart As Integer)
        Try
            cnn.Open()
            command.Connection = cnn
            reader = command.ExecuteReader()
            Dim row, rindex, rcount As Integer
            row = 0
            rindex = fstart
            rcount = reader.FieldCount - 1
            If reader.HasRows Then
                reader.Read()
                While rindex <= rcount
                    If IsDBNull(reader.Item(rindex)) Then
                        alist(row) = 0
                    Else
                        alist(row) = reader.Item(rindex)
                    End If
                    row = row + 1
                    rindex = rindex + 1
                End While
            Else
                alist(row) = 0
            End If
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub array_row_add(ByVal command As SqlCommand, ByVal alist() As String)
        Try
            cnn.Open()
            command.Connection = cnn
            reader = command.ExecuteReader()
            Dim row As Integer
            row = 0
            While reader.Read
                alist(row) = reader.Item(0)
                row = row + 1
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub insert_data1(ByVal command As SqlCommand)
        Dim adapter As New SqlDataAdapter
        Try
            cnn.Open()
            command.Connection = cnn
            adapter.InsertCommand = command
            adapter.InsertCommand.ExecuteNonQuery()
            cnn.Close()
        Catch ex As SqlException
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub print_bill(ByVal customerid As Integer, ByVal pdate As DateTimePicker, ByVal companyid As Integer, ByVal pwrite As System.IO.StreamWriter)
        Try
            cnn.Open()
            command = New SqlCommand("select a.name,b.nag,b.weight,b.rate,b.total from bikri b,vegetable a where b.entry_date=@edate and b.companyid=@companyid and b.cid=@cid and b.sessionid=@sessionid and b.vegetableid=a.vegetableid")
            command.Parameters.Add("@edate", SqlDbType.DateTime).Value = pdate.Value.Date
            command.Parameters.Add("@cid", SqlDbType.Int).Value = customerid
            command.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            command.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
            command.Connection = cnn
            reader = command.ExecuteReader
            While reader.Read
                pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}", reader.Item(0), reader.Item(1), Format(Val(reader.Item(2)), "0.00"), Format(Val(reader.Item(3)), "0.00"), Format(Val(reader.Item(4)), "0.00"))
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub print_bill_bookwise(ByVal customerid As Integer, ByVal pdate As DateTimePicker, ByVal companyid As Integer, ByVal customerid1 As Integer)
        Try
            cnn.Open()
            cnn1.Open()
            Dim name_v(11), nag_v(11), weight_v(11), rate_v(11), total_v(11), name1_v(11) As String
            Dim name_v1(11), nag_v1(11), weight_v1(11), rate_v1(11), total_v1(11), name1_v1(11) As String
            command = New SqlCommand("select a.name,b.nag,b.weight,b.rate,b.total,c.name from bikri b,vegetable a,book c where b.entry_date=@edate and b.companyid=@companyid and b.cid=@cid and b.sessionid=@sessionid and b.vegetableid=a.vegetableid and b.bookid=c.bookid")
            command.Parameters.Add("@edate", SqlDbType.DateTime).Value = pdate.Value.Date
            command.Parameters.Add("@cid", SqlDbType.Int).Value = customerid
            command.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            command.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
            command.Connection = cnn
            reader = command.ExecuteReader

            command1 = New SqlCommand("select a.name,b.nag,b.weight,b.rate,b.total,c.name from bikri b,vegetable a,book c where b.entry_date=@edate and b.companyid=@companyid and b.cid=@cid and b.sessionid=@sessionid and b.vegetableid=a.vegetableid and b.bookid=c.bookid")
            command1.Parameters.Add("@edate", SqlDbType.DateTime).Value = pdate.Value.Date
            command1.Parameters.Add("@cid", SqlDbType.Int).Value = customerid1
            command1.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            command1.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
            command1.Connection = cnn1

            reader1 = command1.ExecuteReader
            Dim rec_count, rec_count1, rec_total As Integer
            rec_count = 0
            rec_count1 = 0
            rec_total = 10

            While reader.Read
                If rec_count = 11 Then
                    Exit While
                End If
                name_v(rec_count) = reader.Item(0)
                nag_v(rec_count) = reader.Item(1)
                weight_v(rec_count) = reader.Item(2)
                rate_v(rec_count) = reader.Item(3)
                total_v(rec_count) = reader.Item(4)
                name1_v(rec_count) = reader.Item(5)

                If name_v(rec_count) = "" Then
                Else
                    weight_v(rec_count) = Format(Val(weight_v(rec_count)), "0.00")
                    rate_v(rec_count) = Format(Val(rate_v(rec_count)), "0.00")
                    total_v(rec_count) = Format(Val(total_v(rec_count)), "0.00")
                End If
                rec_count = rec_count + 1

            End While

            While reader1.Read
                If rec_count1 = 11 Then
                    Exit While
                End If
                name_v1(rec_count1) = reader1.Item(0)
                nag_v1(rec_count1) = reader1.Item(1)
                weight_v1(rec_count1) = reader1.Item(2)
                rate_v1(rec_count1) = reader1.Item(3)
                total_v1(rec_count1) = reader1.Item(4)
                name1_v1(rec_count1) = reader1.Item(5)

                If name_v1(rec_count1) = "" Then
                Else
                    weight_v1(rec_count1) = Format(Val(weight_v1(rec_count1)), "0.00")
                    rate_v1(rec_count1) = Format(Val(rate_v1(rec_count1)), "0.00")
                    total_v1(rec_count1) = Format(Val(total_v1(rec_count1)), "0.00")
                End If

                rec_count1 = rec_count1 + 1
            End While
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(0), nag_v(0), weight_v(0), rate_v(0), total_v(0), New String(" ", 5), name_v1(0), nag_v1(0), weight_v1(0), rate_v1(0), total_v1(0))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(1), nag_v(1), weight_v(1), rate_v(1), total_v(1), New String(" ", 5), name_v1(1), nag_v1(1), weight_v1(1), rate_v1(1), total_v1(1))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(2), nag_v(2), weight_v(2), rate_v(2), total_v(2), New String(" ", 5), name_v1(2), nag_v1(2), weight_v1(2), rate_v1(2), total_v1(2))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(3), nag_v(3), weight_v(3), rate_v(3), total_v(3), New String(" ", 5), name_v1(3), nag_v1(3), weight_v1(3), rate_v1(3), total_v1(3))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(4), nag_v(4), weight_v(4), rate_v(4), total_v(4), New String(" ", 5), name_v1(4), nag_v1(4), weight_v1(4), rate_v1(4), total_v1(4))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(5), nag_v(5), weight_v(5), rate_v(5), total_v(5), New String(" ", 5), name_v1(5), nag_v1(5), weight_v1(5), rate_v1(5), total_v1(5))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(6), nag_v(6), weight_v(6), rate_v(6), total_v(6), New String(" ", 5), name_v1(6), nag_v1(6), weight_v1(6), rate_v1(6), total_v1(6))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(7), nag_v(7), weight_v(7), rate_v(7), total_v(7), New String(" ", 5), name_v1(7), nag_v1(7), weight_v1(7), rate_v1(7), total_v1(7))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(8), nag_v(8), weight_v(8), rate_v(8), total_v(8), New String(" ", 5), name_v1(8), nag_v1(8), weight_v1(8), rate_v1(8), total_v1(8))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(9), nag_v(9), weight_v(9), rate_v(9), total_v(9), New String(" ", 5), name_v1(9), nag_v1(9), weight_v1(9), rate_v1(9), total_v1(9))
            Dim abcd, abcd1, nag_abcd, nag_abcd1, abcd_weight, abcd_weight1, abcd_rate, abcd_rate1, abcd_total, abcd_total1 As String
            abcd = name_v(0) & Environment.NewLine & name_v(1) & Environment.NewLine & name_v(2) & Environment.NewLine & name_v(3) & Environment.NewLine & name_v(4) & Environment.NewLine & name_v(5) & Environment.NewLine & name_v(6) & Environment.NewLine & name_v(7) & Environment.NewLine & name_v(8) & Environment.NewLine & name_v(9)
            abcd1 = name_v1(0) & Environment.NewLine & name_v1(1) & Environment.NewLine & name_v1(2) & Environment.NewLine & name_v1(3) & Environment.NewLine & name_v1(4) & Environment.NewLine & name_v1(5) & Environment.NewLine & name_v1(6) & Environment.NewLine & name_v1(7) & Environment.NewLine & name_v1(8) & Environment.NewLine & name_v1(9)
            nag_abcd = nag_v(0) & Environment.NewLine & nag_v(1) & Environment.NewLine & nag_v(2) & Environment.NewLine & nag_v(3) & Environment.NewLine & nag_v(4) & Environment.NewLine & nag_v(5) & Environment.NewLine & nag_v(6) & Environment.NewLine & nag_v(7) & Environment.NewLine & nag_v(8) & Environment.NewLine & nag_v(9)
            nag_abcd1 = nag_v1(0) & Environment.NewLine & nag_v1(1) & Environment.NewLine & nag_v1(2) & Environment.NewLine & nag_v1(3) & Environment.NewLine & nag_v1(4) & Environment.NewLine & nag_v1(5) & Environment.NewLine & nag_v1(6) & Environment.NewLine & nag_v1(7) & Environment.NewLine & nag_v1(8) & Environment.NewLine & nag_v1(9)
            abcd_weight = weight_v(0) & Environment.NewLine & weight_v(1) & Environment.NewLine & weight_v(2) & Environment.NewLine & weight_v(3) & Environment.NewLine & weight_v(4) & Environment.NewLine & weight_v(5) & Environment.NewLine & weight_v(6) & Environment.NewLine & weight_v(7) & Environment.NewLine & weight_v(8) & Environment.NewLine & weight_v(9)
            abcd_weight1 = weight_v1(0) & Environment.NewLine & weight_v1(1) & Environment.NewLine & weight_v1(2) & Environment.NewLine & weight_v1(3) & Environment.NewLine & weight_v1(4) & Environment.NewLine & weight_v1(5) & Environment.NewLine & weight_v1(6) & Environment.NewLine & weight_v1(7) & Environment.NewLine & weight_v1(8) & Environment.NewLine & weight_v1(9)
            abcd_rate = rate_v(0) & Environment.NewLine & rate_v(1) & Environment.NewLine & rate_v(2) & Environment.NewLine & rate_v(3) & Environment.NewLine & rate_v(4) & Environment.NewLine & rate_v(5) & Environment.NewLine & rate_v(6) & Environment.NewLine & rate_v(7) & Environment.NewLine & rate_v(8) & Environment.NewLine & rate_v(9)
            abcd_rate1 = rate_v1(0) & Environment.NewLine & rate_v1(1) & Environment.NewLine & rate_v1(2) & Environment.NewLine & rate_v1(3) & Environment.NewLine & rate_v1(4) & Environment.NewLine & rate_v1(5) & Environment.NewLine & rate_v1(6) & Environment.NewLine & rate_v1(7) & Environment.NewLine & rate_v1(8) & Environment.NewLine & rate_v1(9)
            abcd_total = total_v(0) & Environment.NewLine & total_v(1) & Environment.NewLine & total_v(2) & Environment.NewLine & total_v(3) & Environment.NewLine & total_v(4) & Environment.NewLine & total_v(5) & Environment.NewLine & total_v(6) & Environment.NewLine & total_v(7) & Environment.NewLine & total_v(8) & Environment.NewLine & total_v(9)
            abcd_total1 = total_v1(0) & Environment.NewLine & total_v1(1) & Environment.NewLine & total_v1(2) & Environment.NewLine & total_v1(3) & Environment.NewLine & total_v1(4) & Environment.NewLine & total_v1(5) & Environment.NewLine & total_v1(6) & Environment.NewLine & total_v1(7) & Environment.NewLine & total_v1(8) & Environment.NewLine & total_v1(9)
            'abcd =  Environment.NewLine & name_v(1) & nag_v(1) & weight_v(1) & rate_v(1) & total_v(1) & Environment.NewLine & name_v(2) & nag_v(2) & weight_v(2) & rate_v(2) & total_v(2)
            Bills_report.TextBox8.Text = abcd
            Bills_report.TextBox10.Text = nag_abcd
            Bills_report.TextBox11.Text = abcd_weight
            Bills_report.TextBox12.Text = abcd_rate
            Bills_report.TextBox13.Text = abcd_total

            Bills_report.TextBox9.Text = abcd1
            Bills_report.TextBox14.Text = nag_abcd1
            Bills_report.TextBox15.Text = abcd_weight1
            Bills_report.TextBox16.Text = abcd_rate1
            Bills_report.TextBox17.Text = abcd_total1
            command.Dispose()
            command1.Dispose()
            reader.Close()
            reader1.Close()
            cnn.Close()
            cnn1.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub print_bill_bookwise12(ByVal customerid As Integer, ByVal pdate As DateTimePicker, ByVal companyid As Integer, ByVal customerid1 As Integer)
        Try
            cnn.Open()
            cnn1.Open()
            Dim name_v(21), nag_v(21), weight_v(21), rate_v(21), total_v(21), name1_v(21) As String
            Dim name_v1(21), nag_v1(21), weight_v1(21), rate_v1(21), total_v1(21), name1_v1(21) As String
            command = New SqlCommand("select a.name,b.nag,b.weight,b.rate,b.total,c.name from bikri b,vegetable a,book c where b.entry_date=@edate and b.companyid=@companyid and b.cid=@cid and b.sessionid=@sessionid and b.vegetableid=a.vegetableid and b.bookid=c.bookid")
            command.Parameters.Add("@edate", SqlDbType.DateTime).Value = pdate.Value.Date
            command.Parameters.Add("@cid", SqlDbType.Int).Value = customerid
            command.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            command.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
            command.Connection = cnn
            reader = command.ExecuteReader

            command1 = New SqlCommand("select a.name,b.nag,b.weight,b.rate,b.total,c.name from bikri b,vegetable a,book c where b.entry_date=@edate and b.companyid=@companyid and b.cid=@cid and b.sessionid=@sessionid and b.vegetableid=a.vegetableid and b.bookid=c.bookid")
            command1.Parameters.Add("@edate", SqlDbType.DateTime).Value = pdate.Value.Date
            command1.Parameters.Add("@cid", SqlDbType.Int).Value = customerid1
            command1.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            command1.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
            command1.Connection = cnn1

            reader1 = command1.ExecuteReader
            Dim rec_count, rec_count1, rec_total As Integer
            rec_count = 0
            rec_count1 = 0
            rec_total = 20

            While reader.Read
                If rec_count = 21 Then
                    Exit While
                End If
                name_v(rec_count) = reader.Item(0)
                nag_v(rec_count) = reader.Item(1)
                weight_v(rec_count) = reader.Item(2)
                rate_v(rec_count) = reader.Item(3)
                total_v(rec_count) = reader.Item(4)
                name1_v(rec_count) = reader.Item(5)

                If name_v(rec_count) = "" Then
                Else
                    weight_v(rec_count) = Format(Val(weight_v(rec_count)), "0.00")
                    rate_v(rec_count) = Format(Val(rate_v(rec_count)), "0.00")
                    total_v(rec_count) = Format(Val(total_v(rec_count)), "0.00")
                End If
                rec_count = rec_count + 1

            End While

            While reader1.Read
                If rec_count1 = 21 Then
                    Exit While
                End If
                name_v1(rec_count1) = reader1.Item(0)
                nag_v1(rec_count1) = reader1.Item(1)
                weight_v1(rec_count1) = reader1.Item(2)
                rate_v1(rec_count1) = reader1.Item(3)
                total_v1(rec_count1) = reader1.Item(4)
                name1_v1(rec_count1) = reader1.Item(5)

                If name_v1(rec_count1) = "" Then
                Else
                    weight_v1(rec_count1) = Format(Val(weight_v1(rec_count1)), "0.00")
                    rate_v1(rec_count1) = Format(Val(rate_v1(rec_count1)), "0.00")
                    total_v1(rec_count1) = Format(Val(total_v1(rec_count1)), "0.00")
                End If

                rec_count1 = rec_count1 + 1
            End While
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(0), nag_v(0), weight_v(0), rate_v(0), total_v(0), New String(" ", 5), name_v1(0), nag_v1(0), weight_v1(0), rate_v1(0), total_v1(0))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(1), nag_v(1), weight_v(1), rate_v(1), total_v(1), New String(" ", 5), name_v1(1), nag_v1(1), weight_v1(1), rate_v1(1), total_v1(1))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(2), nag_v(2), weight_v(2), rate_v(2), total_v(2), New String(" ", 5), name_v1(2), nag_v1(2), weight_v1(2), rate_v1(2), total_v1(2))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(3), nag_v(3), weight_v(3), rate_v(3), total_v(3), New String(" ", 5), name_v1(3), nag_v1(3), weight_v1(3), rate_v1(3), total_v1(3))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(4), nag_v(4), weight_v(4), rate_v(4), total_v(4), New String(" ", 5), name_v1(4), nag_v1(4), weight_v1(4), rate_v1(4), total_v1(4))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(5), nag_v(5), weight_v(5), rate_v(5), total_v(5), New String(" ", 5), name_v1(5), nag_v1(5), weight_v1(5), rate_v1(5), total_v1(5))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(6), nag_v(6), weight_v(6), rate_v(6), total_v(6), New String(" ", 5), name_v1(6), nag_v1(6), weight_v1(6), rate_v1(6), total_v1(6))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(7), nag_v(7), weight_v(7), rate_v(7), total_v(7), New String(" ", 5), name_v1(7), nag_v1(7), weight_v1(7), rate_v1(7), total_v1(7))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(8), nag_v(8), weight_v(8), rate_v(8), total_v(8), New String(" ", 5), name_v1(8), nag_v1(8), weight_v1(8), rate_v1(8), total_v1(8))
            'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}{5,5}{6,-10}{7,4}{8,8}{9,8}{10,10}", name_v(9), nag_v(9), weight_v(9), rate_v(9), total_v(9), New String(" ", 5), name_v1(9), nag_v1(9), weight_v1(9), rate_v1(9), total_v1(9))
            Dim abcd, abcd1, nag_abcd, nag_abcd1, abcd_weight, abcd_weight1, abcd_rate, abcd_rate1, abcd_total, abcd_total1 As String
            abcd = name_v(0) & Environment.NewLine & name_v(1) & Environment.NewLine & name_v(2) & Environment.NewLine & name_v(3) & Environment.NewLine & name_v(4) & Environment.NewLine & name_v(5) & Environment.NewLine & name_v(6) & Environment.NewLine & name_v(7) & Environment.NewLine & name_v(8) & Environment.NewLine & name_v(9) & Environment.NewLine & name_v(10) & Environment.NewLine & name_v(11) & Environment.NewLine & name_v(12) & Environment.NewLine & name_v(13) & Environment.NewLine & name_v(14) & Environment.NewLine & name_v(15) & Environment.NewLine & name_v(16) & Environment.NewLine & name_v(17) & Environment.NewLine & name_v(18) & Environment.NewLine & name_v(19) & Environment.NewLine & name_v(20)
            abcd1 = name_v1(0) & Environment.NewLine & name_v1(1) & Environment.NewLine & name_v1(2) & Environment.NewLine & name_v1(3) & Environment.NewLine & name_v1(4) & Environment.NewLine & name_v1(5) & Environment.NewLine & name_v1(6) & Environment.NewLine & name_v1(7) & Environment.NewLine & name_v1(8) & Environment.NewLine & name_v1(9) & Environment.NewLine & name_v1(10) & Environment.NewLine & name_v1(11) & Environment.NewLine & name_v1(12) & Environment.NewLine & name_v1(13) & Environment.NewLine & name_v1(14) & Environment.NewLine & name_v1(15) & Environment.NewLine & name_v1(16) & Environment.NewLine & name_v1(17) & Environment.NewLine & name_v1(18) & Environment.NewLine & name_v1(19) & Environment.NewLine & name_v1(20)
            nag_abcd = nag_v(0) & Environment.NewLine & nag_v(1) & Environment.NewLine & nag_v(2) & Environment.NewLine & nag_v(3) & Environment.NewLine & nag_v(4) & Environment.NewLine & nag_v(5) & Environment.NewLine & nag_v(6) & Environment.NewLine & nag_v(7) & Environment.NewLine & nag_v(8) & Environment.NewLine & nag_v(9) & Environment.NewLine & nag_v(10) & Environment.NewLine & nag_v(11) & Environment.NewLine & nag_v(12) & Environment.NewLine & nag_v(13) & Environment.NewLine & nag_v(14) & Environment.NewLine & nag_v(15) & Environment.NewLine & nag_v(16) & Environment.NewLine & nag_v(17) & Environment.NewLine & nag_v(18) & Environment.NewLine & nag_v(19) & Environment.NewLine & nag_v(20)
            nag_abcd1 = nag_v1(0) & Environment.NewLine & nag_v1(1) & Environment.NewLine & nag_v1(2) & Environment.NewLine & nag_v1(3) & Environment.NewLine & nag_v1(4) & Environment.NewLine & nag_v1(5) & Environment.NewLine & nag_v1(6) & Environment.NewLine & nag_v1(7) & Environment.NewLine & nag_v1(8) & Environment.NewLine & nag_v1(9) & Environment.NewLine & nag_v1(10) & Environment.NewLine & nag_v1(11) & Environment.NewLine & nag_v1(12) & Environment.NewLine & nag_v1(13) & Environment.NewLine & nag_v1(14) & Environment.NewLine & nag_v1(15) & Environment.NewLine & nag_v1(16) & Environment.NewLine & nag_v1(17) & Environment.NewLine & nag_v1(18) & Environment.NewLine & nag_v1(19) & Environment.NewLine & nag_v1(20)
            abcd_weight = weight_v(0) & Environment.NewLine & weight_v(1) & Environment.NewLine & weight_v(2) & Environment.NewLine & weight_v(3) & Environment.NewLine & weight_v(4) & Environment.NewLine & weight_v(5) & Environment.NewLine & weight_v(6) & Environment.NewLine & weight_v(7) & Environment.NewLine & weight_v(8) & Environment.NewLine & weight_v(9) & Environment.NewLine & weight_v(10) & Environment.NewLine & weight_v(11) & Environment.NewLine & weight_v(12) & Environment.NewLine & weight_v(13) & Environment.NewLine & weight_v(14) & Environment.NewLine & weight_v(15) & Environment.NewLine & weight_v(16) & Environment.NewLine & weight_v(17) & Environment.NewLine & weight_v(18) & Environment.NewLine & weight_v(19) & Environment.NewLine & weight_v(20)
            abcd_weight1 = weight_v1(0) & Environment.NewLine & weight_v1(1) & Environment.NewLine & weight_v1(2) & Environment.NewLine & weight_v1(3) & Environment.NewLine & weight_v1(4) & Environment.NewLine & weight_v1(5) & Environment.NewLine & weight_v1(6) & Environment.NewLine & weight_v1(7) & Environment.NewLine & weight_v1(8) & Environment.NewLine & weight_v1(9) & weight_v1(10) & Environment.NewLine & weight_v1(11) & Environment.NewLine & weight_v1(12) & Environment.NewLine & weight_v1(13) & Environment.NewLine & weight_v1(14) & Environment.NewLine & weight_v1(15) & Environment.NewLine & weight_v1(16) & Environment.NewLine & weight_v1(17) & Environment.NewLine & weight_v1(18) & Environment.NewLine & weight_v1(19) & weight_v1(20)
            abcd_rate = rate_v(0) & Environment.NewLine & rate_v(1) & Environment.NewLine & rate_v(2) & Environment.NewLine & rate_v(3) & Environment.NewLine & rate_v(4) & Environment.NewLine & rate_v(5) & Environment.NewLine & rate_v(6) & Environment.NewLine & rate_v(7) & Environment.NewLine & rate_v(8) & Environment.NewLine & rate_v(9) & rate_v(10) & Environment.NewLine & rate_v(11) & Environment.NewLine & rate_v(12) & Environment.NewLine & rate_v(13) & Environment.NewLine & rate_v(14) & Environment.NewLine & rate_v(15) & Environment.NewLine & rate_v(16) & Environment.NewLine & rate_v(17) & Environment.NewLine & rate_v(18) & Environment.NewLine & rate_v(19)
            abcd_rate1 = rate_v1(0) & Environment.NewLine & rate_v1(1) & Environment.NewLine & rate_v1(2) & Environment.NewLine & rate_v1(3) & Environment.NewLine & rate_v1(4) & Environment.NewLine & rate_v1(5) & Environment.NewLine & rate_v1(6) & Environment.NewLine & rate_v1(7) & Environment.NewLine & rate_v1(8) & Environment.NewLine & rate_v1(9) & Environment.NewLine & rate_v1(10) & Environment.NewLine & rate_v1(11) & Environment.NewLine & rate_v1(12) & Environment.NewLine & rate_v1(13) & Environment.NewLine & rate_v1(14) & Environment.NewLine & rate_v1(15) & Environment.NewLine & rate_v1(16) & Environment.NewLine & rate_v1(17) & Environment.NewLine & rate_v1(18) & Environment.NewLine & rate_v1(19) & Environment.NewLine & rate_v1(20)
            abcd_total = total_v(0) & Environment.NewLine & total_v(1) & Environment.NewLine & total_v(2) & Environment.NewLine & total_v(3) & Environment.NewLine & total_v(4) & Environment.NewLine & total_v(5) & Environment.NewLine & total_v(6) & Environment.NewLine & total_v(7) & Environment.NewLine & total_v(8) & Environment.NewLine & total_v(9) & Environment.NewLine & total_v(10) & Environment.NewLine & total_v(11) & Environment.NewLine & total_v(12) & Environment.NewLine & total_v(13) & Environment.NewLine & total_v(14) & Environment.NewLine & total_v(15) & Environment.NewLine & total_v(16) & Environment.NewLine & total_v(17) & Environment.NewLine & total_v(18) & Environment.NewLine & total_v(19) & Environment.NewLine & total_v(20)
            abcd_total1 = total_v1(0) & Environment.NewLine & total_v1(1) & Environment.NewLine & total_v1(2) & Environment.NewLine & total_v1(3) & Environment.NewLine & total_v1(4) & Environment.NewLine & total_v1(5) & Environment.NewLine & total_v1(6) & Environment.NewLine & total_v1(7) & Environment.NewLine & total_v1(8) & Environment.NewLine & total_v1(9) & Environment.NewLine & total_v1(10) & Environment.NewLine & total_v1(11) & Environment.NewLine & total_v1(12) & Environment.NewLine & total_v1(13) & Environment.NewLine & total_v1(14) & Environment.NewLine & total_v1(15) & Environment.NewLine & total_v1(16) & Environment.NewLine & total_v1(17) & Environment.NewLine & total_v1(18) & Environment.NewLine & total_v1(19) & Environment.NewLine & total_v1(20)
            'abcd =  Environment.NewLine & name_v(1) & nag_v(1) & weight_v(1) & rate_v(1) & total_v(1) & Environment.NewLine & name_v(2) & nag_v(2) & weight_v(2) & rate_v(2) & total_v(2)
            Bills_report.TextBox8.Text = abcd
            Bills_report.TextBox10.Text = nag_abcd
            Bills_report.TextBox11.Text = abcd_weight
            Bills_report.TextBox12.Text = abcd_rate
            Bills_report.TextBox13.Text = abcd_total

            Bills_report.TextBox9.Text = abcd1
            Bills_report.TextBox14.Text = nag_abcd1
            Bills_report.TextBox15.Text = abcd_weight1
            Bills_report.TextBox16.Text = abcd_rate1
            Bills_report.TextBox17.Text = abcd_total1
            command.Dispose()
            command1.Dispose()
            reader.Close()
            reader1.Close()
            cnn.Close()
            cnn1.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub print_bill_laser(ByVal customerid As Integer, ByVal pdate As DateTimePicker, ByVal companyid As Integer)
        Try
            cnn.Open()
            command = New SqlCommand("select a.name,b.nag,b.weight,b.rate,b.total from bikri b,vegetable a where b.entry_date=@edate and b.companyid=@companyid and b.cid=@cid and b.sessionid=@sessionid and b.vegetableid=a.vegetableid")
            command.Parameters.Add("@edate", SqlDbType.DateTime).Value = pdate.Value.Date
            command.Parameters.Add("@cid", SqlDbType.Int).Value = customerid
            command.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            command.Parameters.Add("@sessionid", SqlDbType.Int).Value = MDIParent1.sessionid
            command.Connection = cnn
            reader = command.ExecuteReader
            Dim rec_count, rec_total As Integer
            Dim abcd, abcd_nag, abcd_weight, abcd_rate, abcd_total As String
            rec_count = 0
            rec_total = 6
            While reader.Read
                If rec_count = 7 Then
                    Exit While
                End If
                'pwrite.WriteLine("{0,-10}{1,4}{2,8}{3,8}{4,10}", reader.Item(0), reader.Item(1), Format(Val(reader.Item(2)), "0.00"), Format(Val(reader.Item(3)), "0.00"), Format(Val(reader.Item(4)), "0.00"))
                abcd = abcd & reader.Item(0) & Environment.NewLine
                abcd_nag = abcd_nag & reader.Item(1) & Environment.NewLine
                abcd_weight = abcd_weight & reader.Item(2) & Environment.NewLine
                abcd_rate = abcd_rate & reader.Item(3) & Environment.NewLine
                abcd_total = abcd_total & reader.Item(4) & Environment.NewLine
                rec_count = rec_count + 1
            End While
            Do While rec_count <= rec_total
                'pwrite.WriteLine("{0,40}", New String(" ", 40))
                rec_count = rec_count + 1
            Loop
            Bills_report.TextBox8.Text = abcd
            Bills_report.TextBox10.Text = abcd_nag
            Bills_report.TextBox11.Text = abcd_weight
            Bills_report.TextBox12.Text = abcd_rate
            Bills_report.TextBox13.Text = abcd_total
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub print_crate(ByVal customerid As Integer, ByVal pdate As DateTimePicker, ByVal companyid As Integer, ByVal pwrite As System.IO.StreamWriter)
        Try
            cnn.Open()
            command = New SqlCommand("select a.cname,b.dr from crateledger b,crate a where b.date=@edate and b.companyid=@companyid and b.crateid=a.crateid and b.customerid=@cid and b.dr>@dr")
            command.Parameters.Add("@edate", SqlDbType.DateTime).Value = pdate.Value.Date
            command.Parameters.Add("@cid", SqlDbType.Int).Value = customerid
            command.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            command.Parameters.Add("@dr", SqlDbType.Int).Value = 0
            command.Connection = cnn
            reader = command.ExecuteReader
            Dim rec_count, rec_total As Integer
            rec_count = 0
            rec_total = 6
            While reader.Read
                If rec_count = 7 Then
                    Exit While
                End If
                pwrite.WriteLine("{0,-10}{1,30}", reader.Item(0), reader.Item(1))
                rec_count = rec_count + 1
            End While
            Do While rec_count <= rec_total
                pwrite.WriteLine("{0,40}", New String(" ", 40))
                rec_count = rec_count + 1
            Loop
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub print_crate_laser(ByVal customerid As Integer, ByVal pdate As DateTimePicker, ByVal companyid As Integer, ByVal pwrite As System.IO.StreamWriter, ByVal customerid1 As Integer)
        Try
            cnn.Open()
            cnn1.Open()
            Dim name_v(6), nag_v(6) As String
            Dim name_v1(6), nag_v1(6) As String
            command = New SqlCommand("select a.cname,b.dr from crateledger b,crate a where b.date=@edate and b.companyid=@companyid and b.crateid=a.crateid and b.customerid=@cid and b.dr>@dr")
            command.Parameters.Add("@edate", SqlDbType.DateTime).Value = pdate.Value.Date
            command.Parameters.Add("@cid", SqlDbType.Int).Value = customerid
            command.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            command.Parameters.Add("@dr", SqlDbType.Int).Value = 0
            command.Connection = cnn
            reader = command.ExecuteReader

            command1 = New SqlCommand("select a.cname,b.dr from crateledger b,crate a where b.date=@edate and b.companyid=@companyid and b.crateid=a.crateid and b.customerid=@cid and b.dr>@dr")
            command1.Parameters.Add("@edate", SqlDbType.DateTime).Value = pdate.Value.Date
            command1.Parameters.Add("@cid", SqlDbType.Int).Value = customerid1
            command1.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
            command1.Parameters.Add("@dr", SqlDbType.Int).Value = 0
            command1.Connection = cnn1
            reader1 = command1.ExecuteReader

            Dim rec_count, rec_count1, rec_total As Integer
            rec_count = 0
            rec_count1 = 0
            rec_total = 6

            While reader.Read
                If rec_count = 7 Then
                    Exit While
                End If
                name_v(rec_count) = reader.Item(0)
                nag_v(rec_count) = reader.Item(1)
                rec_count = rec_count + 1
            End While

            While reader1.Read
                If rec_count1 = 7 Then
                    Exit While
                End If
                name_v1(rec_count1) = reader1.Item(0)
                nag_v1(rec_count1) = reader1.Item(1)
                rec_count1 = rec_count1 + 1
            End While
            pwrite.WriteLine("{0,-10}{1,30}{2,5}{3,-10}{4,30}", name_v(0), nag_v(0), New String(" ", 5), name_v1(0), nag_v1(0))
            pwrite.WriteLine("{0,-10}{1,30}{2,5}{3,-10}{4,30}", name_v(1), nag_v(1), New String(" ", 5), name_v1(1), nag_v1(1))
            pwrite.WriteLine("{0,-10}{1,30}{2,5}{3,-10}{4,30}", name_v(2), nag_v(2), New String(" ", 5), name_v1(2), nag_v1(2))
            pwrite.WriteLine("{0,-10}{1,30}{2,5}{3,-10}{4,30}", name_v(3), nag_v(3), New String(" ", 5), name_v1(3), nag_v1(3))
            pwrite.WriteLine("{0,-10}{1,30}{2,5}{3,-10}{4,30}", name_v(4), nag_v(4), New String(" ", 5), name_v1(4), nag_v1(4))
            pwrite.WriteLine("{0,-10}{1,30}{2,5}{3,-10}{4,30}", name_v(5), nag_v(5), New String(" ", 5), name_v1(5), nag_v1(5))
            pwrite.WriteLine("{0,-10}{1,30}{2,5}{3,-10}{4,30}", name_v(6), nag_v(6), New String(" ", 5), name_v1(6), nag_v1(6))


            command.Dispose()
            command1.Dispose()
            reader.Close()
            reader1.Close()
            cnn.Close()
            cnn1.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Sub print_crate_bal(ByVal customerid As Integer, ByVal pdate As DateTimePicker, ByVal companyid As Integer, ByVal pwrite As System.IO.StreamWriter)
        Try
            cnn.Open()
            command = New SqlCommand("select a.cname,sum(b.dr),sum(b.cr) from crateledger b ,crate a  where a.crateid=b.crateid and b.companyid=@compid and b.customerid=@customerid and date<=@date group by a.cname")
            command.Parameters.Add("@date", SqlDbType.DateTime).Value = pdate.Value.Date
            command.Parameters.Add("@customerid", SqlDbType.Int).Value = customerid
            command.Parameters.Add("@compid", SqlDbType.Int).Value = companyid
            command.Connection = cnn
            reader = command.ExecuteReader
            While reader.Read
                pwrite.WriteLine("{0,-10}{1,30}", reader.Item(0), reader.Item(1) - reader.Item(2))
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            command.Dispose()
            reader.Close()
            cnn.Close()
        End Try
    End Sub
    Public Function get_company_id(ByVal companyname As String) As Integer
        Dim abc As SqlCommand
        abc = New SqlCommand("select companyid from company where company=@company")
        abc.Parameters.AddWithValue("@company", SqlDbType.VarChar).Value = companyname
        Dim xyz(1) As String
        array_list(abc, xyz, 0)
        get_company_id = xyz(0)
    End Function
    Public Function get_customer_id(ByVal companyid As Integer, ByVal customername As String) As Integer
        Dim abc As SqlCommand
        abc = New SqlCommand("select customerid from customer where companyid=@companyid and cname=@cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = customername
        Dim xyz(1) As String
        array_list(abc, xyz, 0)
        get_customer_id = xyz(0)
    End Function
    Public Function get_crate_id(ByVal companyid As Integer, ByVal customername As String) As Integer
        Dim abc As SqlCommand
        abc = New SqlCommand("select crateid from crate where companyid=@companyid and cname=@cname")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = customername
        Dim xyz(1) As String
        array_list(abc, xyz, 0)
        get_crate_id = xyz(0)
    End Function
    Public Function get_voucher_id() As Integer
        Dim abc As String
        abc = "select voucher_id from voucher order by voucher_id desc"
        get_voucher_id = auto_id(abc)
    End Function
    Public Function get_receipt_no() As Integer
        Dim abc As String
        abc = "select receipt_no from voucher order by receipt_no desc"
        get_receipt_no = auto_id(abc)
    End Function
    Public Function get_customer_id() As Integer
        Dim abc As String
        abc = "select customerid from customer order by customerid desc"
        get_customer_id = auto_id(abc)
    End Function
    Public Function auto_id(ByVal astring As String) As Integer
        Dim rn1 As Integer
        Try
            cnn.Open()
            command = New SqlCommand(astring, cnn)
            reader = command.ExecuteReader
            If reader.HasRows Then
                reader.Read()
                rn1 = reader.Item(0)
                auto_id = rn1 + 1

            Else
                auto_id = 1
            End If
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            cnn.Close()
        End Try
    End Function
    'Public Sub cust_outstanding(ByVal command As SqlCommand, ByVal lv As ListView)
    '    lv.Items.Clear()
    '    Dim row As Integer
    '    Try
    '        cnn.Open()
    '        command.Connection = cnn
    '        reader = command.ExecuteReader
    '        row = 0
    '        Dim column As Integer
    '        While reader.Read
    '            column = 0
    '            If IsDBNull(reader.Item(2)) Then
    '            Else
    '                If Val(reader.Item(2)) > 0 Then
    '                    lv.Items.Add(row + 1)
    '                    lv.Items(row).SubItems.Add(reader.Item(0))
    '                    lv.Items(row).SubItems.Add(reader.Item(1))
    '                    lv.Items(row).SubItems.Add(reader.Item(2))
    '                    row = row + 1
    '                End If
    '            End If
    '        End While
    '        command.Dispose()
    '        reader.Close()
    '        cnn.Close()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        cnn.Close()
    '    End Try
    'End Sub
    Public Sub cust_outstanding(ByVal command As SqlCommand, ByVal lv As ListView)
        lv.Items.Clear()
        Dim row As Integer
        Try
            cnn.Open()
            command.Connection = cnn
            reader = command.ExecuteReader
            row = 0
            Dim column As Integer
            While reader.Read
                column = 0
                If IsDBNull(reader.Item(2)) Then

                ElseIf IsDBNull(reader.Item(3)) Then
                    If Val(reader.Item(2)) > 0 Then
                        lv.Items.Add(row + 1)
                        lv.Items(row).SubItems.Add(reader.Item(0))
                        lv.Items(row).SubItems.Add(reader.Item(1))
                        lv.Items(row).SubItems.Add(reader.Item(2))
                        row = row + 1
                    End If
                Else
                    If Val(reader.Item(2)) - Val(reader.Item(3)) > 0 Then
                        lv.Items.Add(row + 1)
                        lv.Items(row).SubItems.Add(reader.Item(0))
                        lv.Items(row).SubItems.Add(reader.Item(1))
                        lv.Items(row).SubItems.Add(reader.Item(2) - reader.Item(3))
                        row = row + 1
                    End If
                End If
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            cnn.Close()
        End Try
    End Sub
    Public Sub list_add1(ByVal command As SqlCommand, ByVal lv As ListView)
        lv.Items.Clear()
        Dim row As Integer
        Try
            cnn.Open()
            command.Connection = cnn
            reader = command.ExecuteReader
            row = 0
            Dim column As Integer
            While reader.Read
                column = 0
                'lv.Items.Add(reader.Item(0))
                lv.Items.Add(row + 1)
                Do While column <= reader.FieldCount - 1
                    lv.Items(row).SubItems.Add(reader.Item(column))
                    column = column + 1
                Loop
                row = row + 1
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub vega_ledger_add(ByVal command As SqlCommand, ByVal lv As ListView, ByVal ob As Integer)
        lv.Items.Clear()
        Dim row As Integer
        Dim abc As SqlCommand
        Try
            cnn.Open()
            command.Connection = cnn
            reader = command.ExecuteReader
            row = 0
            Dim column As Integer
            While reader.Read
                column = 0
                lv.Items.Add(row + 1)
                lv.Items(row).SubItems.Add(reader.Item(0))
                If reader.Item(2) = "Bill" Then
                    lv.Items(row).SubItems.Add(reader.Item(1))
                    lv.Items(row).SubItems.Add(" ")
                    lv.Items(row).SubItems.Add(" ")
                    ob = ob + Val(reader.Item(1))
                    lv.Items(row).SubItems.Add(reader.Item(2))
                    lv.Items(row).SubItems.Add(ob)
                    lv.Items(row).SubItems.Add(" ")
                    row = row + 1
                    abc = New SqlCommand("select narration from voucher where sessionid=@sessionid and cid=@cid and voucher_type=@voucher_type and date=@date")
                    abc.Parameters.Add("@date", SqlDbType.DateTime).Value = reader.Item(0)
                    abc.Parameters.Add("@sessionid", SqlDbType.Int).Value = reader.Item(3)
                    abc.Parameters.Add("@cid", SqlDbType.Int).Value = reader.Item(4)
                    abc.Parameters.Add("@voucher_type", SqlDbType.VarChar).Value = "Bill"
                    cnn2.Open()
                    abc.Connection = cnn2
                    reader2 = abc.ExecuteReader
                    If reader2.HasRows Then
                        While reader2.Read
                            If IsDBNull(reader2.Item(0)) Then
                            Else
                                lv.Items.Add(row + 1)
                                lv.Items(row).SubItems.Add(" ")
                                lv.Items(row).SubItems.Add(" ")
                                lv.Items(row).SubItems.Add(" ")
                                lv.Items(row).SubItems.Add(" ")
                                lv.Items(row).SubItems.Add(" ")
                                lv.Items(row).SubItems.Add(" ")
                                lv.Items(row).SubItems.Add(reader2.Item(0))
                                row = row + 1
                            End If
                        End While
                    End If
                    cnn2.Close()
                ElseIf reader.Item(2) = "DRvoucher" Or reader.Item(2) = "OB" Then
                    lv.Items(row).SubItems.Add(" ")
                    lv.Items(row).SubItems.Add(reader.Item(1))
                    lv.Items(row).SubItems.Add(" ")
                    ob = ob + Val(reader.Item(1))
                    lv.Items(row).SubItems.Add(reader.Item(2))
                    lv.Items(row).SubItems.Add(ob)
                    lv.Items(row).SubItems.Add(" ")
                    row = row + 1
                Else
                    lv.Items(row).SubItems.Add(" ")
                    lv.Items(row).SubItems.Add(" ")
                    lv.Items(row).SubItems.Add(reader.Item(1))
                    ob = ob - Val(reader.Item(1))
                    lv.Items(row).SubItems.Add(reader.Item(2))
                    lv.Items(row).SubItems.Add(ob)
                    lv.Items(row).SubItems.Add(" ")
                    row = row + 1
                End If


            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            cnn.Close()
            cnn2.Close()
        End Try
    End Sub
    Public Sub consolidated_bikri_report(ByVal command As SqlCommand, ByVal lv As ListView, ByVal ob As Integer)
        lv.Items.Clear()
        Dim row As Integer
        Dim abc, abc1, abc2, abc3, abc4, abc5, abc6 As SqlCommand
        Dim op_bal_dr, op_bal_cr, cl_bal_dr, cl_bal_cr, op_bal_total, cl_bal_total
        op_bal_dr = 0
        op_bal_cr = 0
        cl_bal_cr = 0
        cl_bal_dr = 0
        Try
            cnn.Open()
            command.Connection = cnn
            reader = command.ExecuteReader
            row = 0
            Dim column As Integer
            While reader.Read
                column = 0
                lv.Items.Add(reader.Item(0))
                Dim desc, desc1 As String


                abc = New SqlCommand("select sum(total),sum(arhat),sum(laga),sum(gtotal)from bikri where companyid=@companyid and sessionid=@sessionid and entry_date=@date group by entry_date")
                abc.Parameters.Add("@date", SqlDbType.DateTime).Value = reader.Item(0)
                abc.Parameters.Add("@sessionid", SqlDbType.Int).Value = reader.Item(2)
                abc.Parameters.Add("@companyid", SqlDbType.Int).Value = reader.Item(1)
                cnn2.Open()
                abc.Connection = cnn2
                reader2 = abc.ExecuteReader
                If reader2.HasRows Then
                    reader2.Read()

                    If IsDBNull(reader2.Item(0)) Then
                        lv.Items(row).SubItems.Add(" ")
                    Else
                        lv.Items(row).SubItems.Add(reader2.Item(0))
                    End If
                    If IsDBNull(reader2.Item(1)) Then
                        lv.Items(row).SubItems.Add(" ")
                    Else
                        lv.Items(row).SubItems.Add(reader2.Item(1))
                    End If
                    If IsDBNull(reader2.Item(2)) Then
                        lv.Items(row).SubItems.Add(" ")
                    Else
                        lv.Items(row).SubItems.Add(reader2.Item(2))
                    End If
                    If IsDBNull(reader2.Item(3)) Then
                        lv.Items(row).SubItems.Add(" ")
                    Else
                        lv.Items(row).SubItems.Add(reader2.Item(3))
                    End If
                Else
                    lv.Items(row).SubItems.Add(" ")
                    lv.Items(row).SubItems.Add(" ")
                    lv.Items(row).SubItems.Add(" ")
                    lv.Items(row).SubItems.Add(" ")
                End If
                cnn2.Close()

                desc = "DR"
                desc1 = "CR"
                abc2 = New SqlCommand("select sum(amount)from voucher where account_desc=@desc and companyid=@companyid and date<@dt1 and sessionid=@session")
                abc2.Parameters.Add("@dt1", SqlDbType.DateTime).Value = reader.Item(0)
                abc2.Parameters.Add("@desc", SqlDbType.VarChar).Value = desc
                abc2.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
                abc2.Parameters.Add("@companyid", SqlDbType.Int).Value = reader.Item(1)


                cnn1.Open()
                abc2.Connection = cnn1
                reader1 = abc2.ExecuteReader
                If reader1.HasRows Then
                    reader1.Read()
                    If IsDBNull(reader1.Item(0)) Then
                        op_bal_dr = 0
                    Else
                        op_bal_dr = reader1.Item(0)
                    End If
                End If

                cnn1.Close()

                abc1 = New SqlCommand("select sum(amount)from voucher where account_desc=@desc and companyid=@companyid and date<@dt1 and sessionid=@session")
                abc1.Parameters.Add("@dt1", SqlDbType.DateTime).Value = reader.Item(0)
                abc1.Parameters.Add("@desc", SqlDbType.VarChar).Value = desc1
                abc1.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
                abc1.Parameters.Add("@companyid", SqlDbType.Int).Value = reader.Item(1)
                cnn3.Open()
                abc1.Connection = cnn3
                reader3 = abc1.ExecuteReader
                If reader3.HasRows Then
                    reader3.Read()
                    If IsDBNull(reader3.Item(0)) Then
                        op_bal_cr = 0
                    Else
                        op_bal_cr = reader3.Item(0)
                    End If

                End If

                op_bal_total = op_bal_dr - op_bal_cr
                cnn3.Close()

                lv.Items(row).SubItems.Add(op_bal_total)

                abc3 = New SqlCommand("select sum(amount)from voucher where voucher_type=@desc and companyid=@companyid and date=@dt1 and sessionid=@session")
                abc3.Parameters.Add("@dt1", SqlDbType.DateTime).Value = reader.Item(0)
                abc3.Parameters.Add("@desc", SqlDbType.VarChar).Value = "Receipt"
                abc3.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
                abc3.Parameters.Add("@companyid", SqlDbType.Int).Value = reader.Item(1)
                cnn4.Open()
                abc3.Connection = cnn4
                reader4 = abc3.ExecuteReader
                reader4.Read()
                If IsDBNull(reader4.Item(0)) Then
                    lv.Items(row).SubItems.Add(" ")
                Else
                    lv.Items(row).SubItems.Add(reader4.Item(0))
                End If
                cnn4.Close()

                abc4 = New SqlCommand("select sum(amount)from voucher where voucher_type=@desc and companyid=@companyid and date=@dt1 and sessionid=@session")
                abc4.Parameters.Add("@dt1", SqlDbType.DateTime).Value = reader.Item(0)
                abc4.Parameters.Add("@desc", SqlDbType.VarChar).Value = "DRvoucher"
                abc4.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
                abc4.Parameters.Add("@companyid", SqlDbType.Int).Value = reader.Item(1)
                cnn5.Open()
                abc4.Connection = cnn5
                reader5 = abc4.ExecuteReader
                reader5.Read()
                If IsDBNull(reader5.Item(0)) Then
                    lv.Items(row).SubItems.Add(" ")
                Else
                    lv.Items(row).SubItems.Add(reader5.Item(0))
                End If
                cnn5.Close()

                desc = "DR"
                desc1 = "CR"
                abc5 = New SqlCommand("select sum(amount)from voucher where account_desc=@desc and companyid=@companyid and date<=@dt1 and sessionid=@session")
                abc5.Parameters.Add("@dt1", SqlDbType.DateTime).Value = reader.Item(0)
                abc5.Parameters.Add("@desc", SqlDbType.VarChar).Value = desc
                abc5.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
                abc5.Parameters.Add("@companyid", SqlDbType.Int).Value = reader.Item(1)


                cnn6.Open()
                abc5.Connection = cnn6
                reader6 = abc5.ExecuteReader
                If reader6.HasRows Then
                    reader6.Read()
                    If IsDBNull(reader6.Item(0)) Then
                        cl_bal_dr = 0
                    Else
                        cl_bal_dr = reader6.Item(0)
                    End If

                End If
                cnn6.Close()

                abc6 = New SqlCommand("select sum(amount)from voucher where account_desc=@desc and companyid=@companyid and date<=@dt1 and sessionid=@session")
                abc6.Parameters.Add("@dt1", SqlDbType.DateTime).Value = reader.Item(0)
                abc6.Parameters.Add("@desc", SqlDbType.VarChar).Value = desc1
                abc6.Parameters.Add("@session", SqlDbType.Int).Value = MDIParent1.sessionid
                abc6.Parameters.Add("@companyid", SqlDbType.Int).Value = reader.Item(1)
                cnn7.Open()
                abc6.Connection = cnn7
                reader7 = abc6.ExecuteReader
                If reader7.HasRows Then
                    reader7.Read()
                    If IsDBNull(reader7.Item(0)) Then
                        cl_bal_cr = 0
                    Else
                        cl_bal_cr = reader7.Item(0)
                    End If

                End If
                cl_bal_total = cl_bal_dr - cl_bal_cr
                cnn7.Close()
                lv.Items(row).SubItems.Add(cl_bal_total)
                row = row + 1

            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            cnn.Close()
            cnn1.Close()
            cnn2.Close()
            cnn3.Close()
            cnn4.Close()
            cnn5.Close()
            cnn6.Close()
            cnn7.Close()
        End Try
    End Sub
    Public Sub crate_ledger_add(ByVal command As SqlCommand, ByVal lv As ListView, ByVal ob As Integer)
        lv.Items.Clear()
        Dim row As Integer
        Try
            cnn.Open()
            command.Connection = cnn
            reader = command.ExecuteReader
            row = 0
            Dim column As Integer
            While reader.Read
                column = 0
                lv.Items.Add(row + 1)
                lv.Items(row).SubItems.Add(reader.Item(0))
                lv.Items(row).SubItems.Add(reader.Item(1))
                lv.Items(row).SubItems.Add(reader.Item(2))
                lv.Items(row).SubItems.Add(reader.Item(3))
                lv.Items(row).SubItems.Add(reader.Item(4))
                If reader.Item(4) = "ISSUE" Or reader.Item(4) = "B/OPENING" Then
                    ob = ob + Val(reader.Item(2))
                Else
                    ob = ob - Val(reader.Item(3))
                End If
                lv.Items(row).SubItems.Add(ob)
                row = row + 1
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            cnn.Close()
        End Try
    End Sub
    Public Function check_rec(ByVal command As SqlCommand) As Integer
        check_rec = 0
        Try
            cnn.Open()
            command.Connection = cnn
            reader = command.ExecuteReader
            While reader.Read()
                check_rec = check_rec + 1
            End While
            command.Dispose()
            reader.Close()
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
End Class

