Imports System.Data.SqlClient
Public Class modifybook
    Dim a7 As New predefined
    Public bookid As Integer
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            Dim abc As SqlCommand
            abc = New SqlCommand("update book set name=@bname where bookid=@bookid")
            abc.Parameters.AddWithValue("@bname", SqlDbType.VarChar).Value = TextBox2.Text
            abc.Parameters.AddWithValue("@bookid", SqlDbType.Int).Value = bookid
            a7.update_data(abc)
            booklist.show_list()
            Me.Close()
        End If
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        a7.DoConvert(TextBox2)
    End Sub
End Class