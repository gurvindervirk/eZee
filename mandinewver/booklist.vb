Imports System.Data.SqlClient
Public Class booklist
    Dim a7 As New predefined
    Public Sub show_list()
        Dim abc As SqlCommand
        abc = New SqlCommand("select * from book")
        a7.List_add(abc, ListView1)
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
        If e.KeyCode = Keys.S Then
            modifybook.bookid = Val(ListView1.SelectedItems.Item(0).SubItems(1).Text)
            modifybook.TextBox1.Text = ListView1.SelectedItems.Item(0).Text
            modifybook.Show()
        End If
    End Sub
    Private Sub booklist_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        show_list()
    End Sub
End Class