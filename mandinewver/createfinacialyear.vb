Imports System.Data.SqlClient
Public Class createfinacialyear
    Dim a7 As New predefined
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Enter Then
            Dim abc As SqlCommand
            abc = New SqlCommand("insert into session_selection(session)values(@sessionname)")
            abc.Parameters.AddWithValue("@sessionname", SqlDbType.VarChar).Value = TextBox1.Text
            a7.insert_data1(abc)
            show_list()
            TextBox1.Text = ""
            TextBox1.Focus()
        End If
    End Sub
    Public Sub show_list()
        Dim abc As SqlCommand
        abc = New SqlCommand("select * from session_selection")
        a7.List_add(abc, ListView1)
    End Sub
    Private Sub createfinacialyear_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub createfinacialyear_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        show_list()
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        Dim abc As SqlCommand
        Dim eno As Integer
        eno = Val(ListView1.SelectedItems.Item(0).SubItems(1).Text)
        If e.KeyCode = Keys.D Then
            abc = New SqlCommand("delete  from session_selection where sessionid=" & eno)
            a7.Delete_data(abc)
            show_list()
            TextBox1.Focus()
        End If
    End Sub
End Class