Imports System.Data.SqlClient
Public Class date_locking
    Dim a7 As New predefined
    Public Sub get_current_date()
        Dim abc As SqlCommand
        abc = New SqlCommand("select currentdate from softwaredate order by currentdate desc")
        Dim xyz(0) As String
        a7.array_list(abc, xyz, 0)
        If xyz(0) = "0" Then
        Else
            DateTimePicker1.Text = xyz(0)
        End If
    End Sub
    Private Sub date_locking_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        check_date_status()
    End Sub
    Public Sub save_locked_date()
        Dim abc As SqlCommand
        abc = New SqlCommand("insert into locked(locked_date)values(@dt)")
        abc.Parameters.Add("@dt", SqlDbType.DateTime).Value = DateTimePicker1.Value.Date
        a7.insert_data1(abc)
        Dim abc1 As SqlCommand
        abc1 = New SqlCommand("delete from softwaredate")
        a7.Delete_data(abc1)
        Me.Close()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        save_locked_date()
    End Sub
    Public Sub check_date_status()
        Dim abc As SqlCommand
        abc = New SqlCommand("select currentdate from softwaredate")
        Dim ch_rec As Integer
        ch_rec = a7.check_rec(abc)
        If ch_rec > 0 Then
            Label2.Visible = True
            DateTimePicker1.Visible = True
            Label1.Visible = True
            Button1.Visible = True
            Label3.Visible = False
            get_current_date()
        Else
            Label2.Visible = False
            DateTimePicker1.Visible = False
            Label1.Visible = False
            Button1.Visible = False
            Label3.Visible = True
        End If
    End Sub
End Class