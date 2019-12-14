Imports System.Data.SqlClient
Public Class systemsetup
    Dim a7 As New predefined
    Public sessionid As Integer
    Public utype As String
    Public Sub combo_add()
        Dim abc As String
        abc = "select session from session_selection"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim abc As SqlCommand
        abc = New SqlCommand("select sessionid from session_selection where session=@session")
        abc.Parameters.AddWithValue("@session", SqlDbType.VarChar).Value = ComboBox1.Text
        Dim xyz(1) As String
        a7.array_list(abc, xyz, 0)
        sessionid = xyz(0)
    End Sub
    Private Sub systemsetup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        combo_add()
        If utype = "USER" Then

        End If
        get_locked_date()
        get_current_date()
    End Sub
    Public Sub get_locked_date()
        Dim abc As SqlCommand
        abc = New SqlCommand("select locked_date from locked order by locked_date desc")
        Dim xyz(0) As String
        a7.array_list(abc, xyz, 0)
        If xyz(0) = "0" Then
        Else
            DateTimePicker1.Text = xyz(0)
            DateTimePicker1.Enabled = False
        End If
    End Sub
    Public Sub get_current_date()
        Dim abc As SqlCommand
        abc = New SqlCommand("select currentdate from softwaredate order by currentdate desc")
        Dim xyz(0) As String
        a7.array_list(abc, xyz, 0)
        If xyz(0) = "0" Then
            If utype = "USER" Then
                Label4.Visible = True
                Label2.Visible = False
                Label3.Visible = False
                DateTimePicker1.Visible = False
                DateTimePicker2.Visible = False
                Label4.Text = "No Date Selected...!!"
            Else
                Label4.Visible = False
            End If

        Else
            DateTimePicker2.Text = xyz(0)
            DateTimePicker2.Enabled = False
            If utype = "USER" Then
                Label4.Visible = True
                Label2.Visible = False
                Label3.Visible = False
                DateTimePicker1.Visible = False
                DateTimePicker2.Visible = False
                Label4.Text = DateTimePicker2.Text
            Else
                Label4.Visible = False
            End If
        End If
    End Sub
    Public Sub check_date_status()
        Dim abc, abc2 As SqlCommand
        abc = New SqlCommand("select locked_date from locked where locked_date=@locked_date")
        abc.Parameters.Add("@locked_date", SqlDbType.DateTime).Value = DateTimePicker2.Value.Date
        Dim date_status, date_status_curr As Integer
        Dim date_type As DateTime
        Dim xyz(0) As String
        date_status = a7.check_rec(abc)
        If date_status = 0 Then
            abc2 = New SqlCommand("select currentdate from softwaredate")
            date_status_curr = a7.check_rec(abc2)
            If date_status_curr > 0 Then
                a7.array_list(abc2, xyz, 0)
                date_type = DateTime.Parse(xyz(0))
                If DateTimePicker2.Value = date_type Then
                    MDIParent1.sessionid = sessionid
                    MDIParent1.utype = utype
                    MDIParent1.entry_date = DateTimePicker2.Value.Date
                    MDIParent1.Label1.Text = ComboBox1.Text
                    MDIParent1.Show()
                Else
                    Dim abc1 As SqlCommand
                    abc1 = New SqlCommand("insert into softwaredate(currentdate)values(@dt)")
                    abc1.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTimePicker2.Value.Date
                    a7.insert_data1(abc1)
                    If ComboBox1.Text = "" Then
                        MsgBox("Select Session.....!!", MsgBoxStyle.Information, "Vega ver 3.5")
                        ComboBox1.Focus()
                        Exit Sub
                    End If
                    MDIParent1.sessionid = sessionid
                    MDIParent1.utype = utype
                    MDIParent1.entry_date = DateTimePicker2.Value.Date
                    MDIParent1.Label1.Text = ComboBox1.Text
                    MDIParent1.Show()
                End If
            Else
                Dim abc1 As SqlCommand
                abc1 = New SqlCommand("insert into softwaredate(currentdate)values(@dt)")
                abc1.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTimePicker2.Value.Date
                a7.insert_data1(abc1)
                If ComboBox1.Text = "" Then
                    MsgBox("Select Session.....!!", MsgBoxStyle.Information, "Vega ver 3.5")
                    ComboBox1.Focus()
                    Exit Sub
                End If
                MDIParent1.sessionid = sessionid
                MDIParent1.utype = utype
                MDIParent1.entry_date = DateTimePicker2.Value.Date
                MDIParent1.Label1.Text = ComboBox1.Text
                MDIParent1.Show()
            End If
        Else

            MsgBox("Sorry!! Date Is Locked Can't work in back date", MsgBoxStyle.Critical, "Vega 3.5")
            Exit Sub
        End If
        'End If
        '    'Dim date_type As DateTime
        '    Dim date_type As Nullable(Of DateTime)
        '    Dim xyz(0) As String
        '    a7.array_list(abc2, xyz, 0)
        '    date_type = DateTime.Parse(xyz(0))
        '    If date_type.HasValue Then
        '        'If Not.date_type= Then
        '        'If xyz(0) > 0 Then
        '        'date_type = xyz(0)
        '        If DateTimePicker2.Value = date_type Then
        '            MDIParent1.sessionid = sessionid
        '            MDIParent1.utype = utype
        '            MDIParent1.entry_date = DateTimePicker2.Value.Date
        '            MDIParent1.Label1.Text = ComboBox1.Text
        '            MDIParent1.Show()
        '        Else
        '            Dim abc1 As SqlCommand
        '            abc1 = New SqlCommand("insert into softwaredate(currentdate)values(@dt)")
        '            abc1.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTimePicker2.Value.Date
        '            a7.insert_data1(abc1)
        '            If ComboBox1.Text = "" Then
        '                MsgBox("Select Session.....!!", MsgBoxStyle.Information, "Vega ver 3.5")
        '                ComboBox1.Focus()
        '                Exit Sub
        '            End If
        '            MDIParent1.sessionid = sessionid
        '            MDIParent1.utype = utype
        '            MDIParent1.entry_date = DateTimePicker2.Value.Date
        '            MDIParent1.Label1.Text = ComboBox1.Text
        '            MDIParent1.Show()
        '        End If

        '    Else
        '        Dim abc1 As SqlCommand
        '        abc1 = New SqlCommand("insert into softwaredate(currentdate)values(@dt)")
        '        abc1.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTimePicker2.Value.Date
        '        a7.insert_data1(abc1)
        '        If ComboBox1.Text = "" Then
        '            MsgBox("Select Session.....!!", MsgBoxStyle.Information, "Vega ver 3.5")
        '            ComboBox1.Focus()
        '            Exit Sub
        '        End If
        '        MDIParent1.sessionid = sessionid
        '        MDIParent1.utype = utype
        '        MDIParent1.entry_date = DateTimePicker2.Value.Date
        '        MDIParent1.Label1.Text = ComboBox1.Text
        '        MDIParent1.Show()
        '    End If
        'Else

        '    MsgBox("Sorry!! Date Is Locked Can't work in back date", MsgBoxStyle.Critical, "Vega 3.5")
        '    Exit Sub
        'End If
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        check_date_status()
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim abc, i, voucher_chk, crate_chk As Integer
        Dim abc1, abc2, abc3, abc4 As SqlCommand
        Dim date_type As DateTime
        i = 0
        voucher_chk = 0
        crate_chk = 0
        abc4 = New SqlCommand("select currentdate from softwaredate")
        Dim xyz(0) As String
        a7.array_list(abc4, xyz, 0)
        If xyz(0).Length = 0 Then
            MsgBox("No Current date Is Alloted.!!")
            Exit Sub
        Else
            date_type = xyz(0)
            abc2 = New SqlCommand("select * from voucher where date=@lockeddate")
            abc2.Parameters.Add("@lockeddate", SqlDbType.DateTime).Value = date_type
            voucher_chk = a7.check_rec(abc2)
            abc3 = New SqlCommand("select * from crateledger where date=@lockeddate")
            abc3.Parameters.Add("@lockeddate", SqlDbType.DateTime).Value = date_type
            crate_chk = a7.check_rec(abc3)
            If voucher_chk = 0 And crate_chk = 0 Then
                abc1 = New SqlCommand("delete from softwaredate")
                a7.Delete_data(abc1)
            End If
        End If
        Me.Close()

    End Sub
End Class
