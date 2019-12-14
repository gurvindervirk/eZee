Imports System.Data.SqlClient
Public Class modify_vegetable
    Dim a7 As New predefined
    Dim vid As Integer
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        e.Handled = a7.trapkey(Asc(e.KeyChar))
    End Sub
    Public Sub show_list()
        Label3.Visible = True
        Label4.Visible = True
        Label5.Visible = True
        TextBox1.Visible = True
        TextBox2.Visible = True
        TextBox3.Visible = True
        Button1.Visible = True
        Button2.Visible = True
        Dim abc As SqlCommand
        abc = New SqlCommand("select name,arhat,laga from vegetable where vegetableid=@vid")
        abc.Parameters.AddWithValue("@vid", SqlDbType.Int).Value = vid
        Dim xyz(2) As String
        a7.array_list(abc, xyz, 0)
        TextBox1.Text = xyz(0)
        TextBox2.Text = xyz(1)
        TextBox3.Text = xyz(2)
        TextBox1.Focus()
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        a7.DoConvert(TextBox1)
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Escape Then
            TextBox1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then Exit Sub
        Dim abc As SqlCommand
        abc = New SqlCommand("update vegetable set arhat=@arhat,laga=@laga,name=@vname where vegetableid=@vid")
        abc.Parameters.AddWithValue("@vid", SqlDbType.Int).Value = vid
        abc.Parameters.AddWithValue("@vname", SqlDbType.VarChar).Value = TextBox1.Text
        abc.Parameters.AddWithValue("@arhat", SqlDbType.Float).Value = Val(TextBox2.Text)
        abc.Parameters.AddWithValue("@laga", SqlDbType.Float).Value = Val(TextBox3.Text)
        a7.update_data(abc)
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False
        TextBox1.Visible = False
        TextBox2.Visible = False
        TextBox3.Visible = False
        Button1.Visible = False
        Button2.Visible = False
        list_add()
        ListView1.Focus()
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        e.Handled = a7.trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub
    Public Sub list_add()
        Dim abc As SqlCommand
        abc = New SqlCommand("select name,arhat,laga,vegetableid from vegetable")
        a7.list_add1(abc, ListView1)
    End Sub
    Private Sub modify_vegetable_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False
        TextBox1.Visible = False
        TextBox2.Visible = False
        TextBox3.Visible = False
        Button1.Visible = False
        Button2.Visible = False
        list_add()
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.S Then
            vid = Val(ListView1.SelectedItems.Item(0).SubItems(4).Text)
            show_list()
        End If
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.D Then
            Dim abc3 As SqlCommand
            vid = Val(ListView1.SelectedItems.Item(0).SubItems(4).Text)
            abc3 = New SqlCommand("delete from vegetable where vegetableid=@vid")
            abc3.Parameters.AddWithValue("@vid", SqlDbType.Int).Value = vid
            a7.delete_data(abc3)
            list_add()
        End If
    End Sub
End Class