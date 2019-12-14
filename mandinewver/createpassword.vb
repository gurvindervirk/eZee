Imports System.Data.SqlClient
Public Class createpassword
    Dim a7 As New predefined
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
    Public Sub add_user()
        ComboBox1.Items.Add("USER")
        ComboBox1.Items.Add("MANAGER")
        ComboBox1.Items.Add("ADMIN")
    End Sub
    Private Sub createpassword_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        add_user()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox1.Focus()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim abc As SqlCommand
        abc = New SqlCommand("select uname from security where uname=@uname")
        abc.Parameters.AddWithValue("@uname", SqlDbType.VarChar).Value = TextBox1.Text
        Dim check As Integer
        check = a7.check_rec(abc)
        If check > 0 Then
            MsgBox("User Name already exists..!!try new user name", MsgBoxStyle.Information, "Invalid User Name")
            Exit Sub
        End If
        If TextBox2.Text = TextBox3.Text Then
            Dim abc1 As SqlCommand
            abc1 = New SqlCommand("insert into security(uname,pwd,utype)values(@uname,@pwd,@utype)")
            abc1.Parameters.AddWithValue("@uname", SqlDbType.VarChar).Value = TextBox1.Text
            abc1.Parameters.AddWithValue("@pwd", SqlDbType.VarChar).Value = TextBox3.Text
            abc1.Parameters.AddWithValue("@utype", SqlDbType.VarChar).Value = ComboBox1.Focus
            a7.insert_data1(abc1)
        Else
            MsgBox("New password doesn't match ....!!", MsgBoxStyle.Exclamation, "Password Verification")
            Exit Sub
        End If
        ComboBox1.Items.Clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        add_user()
        ComboBox1.Focus()
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class