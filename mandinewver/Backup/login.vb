Imports System.Data.SqlClient
Public Class login
    Dim a7 As New predefined
    Dim a8 As New LicenseHelper
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim abc1 As SqlCommand
        Dim rcount As Integer
        abc1 = New SqlCommand("select count(*) from security where pwd=@pwd and uname=@uname and utype=@utype")
        abc1.Parameters.Add("@pwd", SqlDbType.VarChar).Value = TextBox2.Text
        abc1.Parameters.Add("@uname", SqlDbType.VarChar).Value = TextBox1.Text
        abc1.Parameters.Add("@utype", SqlDbType.VarChar).Value = ComboBox1.Text
        Dim xyz(1) As String
        a7.array_list(abc1, xyz, 0)
        rcount = Val(xyz(0))
        If rcount = 0 Then
            MsgBox("Password Not matched..", MsgBoxStyle.Information, "Vega 3.5")
            Exit Sub
        Else
            systemsetup.utype = ComboBox1.Text
            systemsetup.Show()
        End If
    End Sub
    Public Sub add_user()
        ComboBox1.Items.Add("USER")
        ComboBox1.Items.Add("MANAGER")
        ComboBox1.Items.Add("ADMIN")
    End Sub
    Private Sub login_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        a8.Required()
        'Dim abc1 As SqlCommand
        'abc1 = New SqlCommand("delete from bikri")
        'a7.delete_data(abc1)
        'Me.Close()
        add_user()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox1.Focus()
    End Sub
End Class