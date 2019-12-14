Imports System.Data.SqlClient
Public Class Bookentry
    Dim a7 As New predefined
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Enter Then
            Dim abc As SqlCommand
            abc = New SqlCommand("insert into book(name)values(@bookname)")
            abc.Parameters.AddWithValue("@bookname", SqlDbType.VarChar).Value = TextBox1.Text
            a7.insert_data1(abc)
            show_list()
            TextBox1.Text = ""
            TextBox1.Focus()
        End If
    End Sub
    Public Sub show_list()
        Dim abc As SqlCommand
        abc = New SqlCommand("select * from book")
        a7.List_add(abc, ListView1)
    End Sub
    Private Sub createfinacialyear_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        show_list()
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        a7.DoConvert(TextBox1)
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        Dim bookid As Integer
        If e.KeyCode = Keys.D Then
            Dim abc3 As SqlCommand
            bookid = Val(ListView1.SelectedItems.Item(0).SubItems(1).Text)
            abc3 = New SqlCommand("delete from book where bookid=@bookid")
            abc3.Parameters.AddWithValue("@bookid", SqlDbType.Int).Value = bookid
            a7.Delete_data(abc3)
            show_list()
        End If
    End Sub
End Class