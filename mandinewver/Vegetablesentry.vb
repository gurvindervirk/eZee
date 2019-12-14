Imports System.Data.SqlClient
Public Class Vegetablesentry
    Dim a7 As New predefined
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If TextBox1.Text = "" Then Exit Sub
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub
    Public Sub show_list()
        Dim abc As SqlCommand
        abc = New SqlCommand("select name from vegetable ")
        a7.List_add(abc, ListView1)
    End Sub
    Private Sub Vegetablesentry_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If bikribook.Visible = True Then
            bikribook.TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        a7.DoConvert(TextBox1)
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
   
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim abc As SqlCommand
        abc = New SqlCommand("insert into vegetable(name,arhat,laga)values(@vname,@arhat,@laga)")
        abc.Parameters.AddWithValue("@vname", SqlDbType.VarChar).Value = TextBox1.Text
        abc.Parameters.AddWithValue("@arhat", SqlDbType.Float).Value = Val(TextBox2.Text)
        abc.Parameters.AddWithValue("@laga", SqlDbType.Float).Value = Val(TextBox3.Text)
        a7.insert_data1(abc)
        show_list()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox1.Focus()
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Vegetablesentry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        show_list()
    End Sub
End Class