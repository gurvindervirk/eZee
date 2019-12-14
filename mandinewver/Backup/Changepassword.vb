Imports System.Data.SqlClient
Public Class Changepassword
    Dim a7 As New predefined
    Public Sub adduser()
        Dim abc As String
        abc = "select uname from security"
        a7.abc_add(abc, ComboBox1)
    End Sub
    Private Sub Changepassword_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        adduser()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim abc As SqlCommand
        abc = New SqlCommand("select pwd from security where uname=@uname")
        abc.Parameters.AddWithValue("@uname", SqlDbType.VarChar).Value = ComboBox1.Text
        Dim xyz(0) As String
        a7.array_list(abc, xyz, 0)
        If xyz(0) = TextBox1.Text Then
        Else
            MsgBox("Incorect password .....try again", MsgBoxStyle.Information, "Password Verification")
            Exit Sub
        End If
        If TextBox2.Text = TextBox3.Text Then
            Dim abc1 As SqlCommand
            abc1 = New SqlCommand("update security set pwd=@pwd where uname=@uname")
            abc1.Parameters.AddWithValue("@uname", SqlDbType.VarChar).Value = ComboBox1.Text
            abc1.Parameters.AddWithValue("@pwd", SqlDbType.VarChar).Value = TextBox3.Text
            a7.update_data(abc1)
        Else
            MsgBox("New password doesn't match ....!!", MsgBoxStyle.Exclamation, "Password Verification")
            Exit Sub
        End If
        ComboBox1.Items.Clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        adduser()
        ComboBox1.Focus()
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            ComboBox1.Focus()
        End If
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            TextBox1.Focus()
        End If
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1.Focus()
        End If
        If e.KeyCode = Keys.Escape Then
            TextBox2.Focus()
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim abc As SqlCommand
        abc = New SqlCommand("delete from security where uname=@uname")
        abc.Parameters.AddWithValue("@uname", SqlDbType.VarChar).Value = ComboBox1.Text
        a7.delete_data(abc)
        ComboBox1.Items.Clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        adduser()
        ComboBox1.Focus()
    End Sub
End Class