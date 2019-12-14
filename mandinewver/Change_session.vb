Imports System.Data.SqlClient
Public Class Change_session
    Dim a7 As New predefined
    Public sessionid As Integer
    Public Sub combo_add()
        Dim abc As String
        abc = "select session from session_selection"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim abc As SqlCommand
        abc = New SqlCommand("select sessionid from session_selection where session=@session")
        abc.Parameters.AddWithValue("@session", SqlDbType.VarChar).Value = ComboBox1.Text
        Dim xyz(1) As String
        a7.array_list(abc, xyz, 0)
        sessionid = xyz(0)
        MDIParent1.sessionid = sessionid
    End Sub
    Private Sub Change_session_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MDIParent1.Label1.Text = ComboBox1.Text
    End Sub
    Private Sub Change_session_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        combo_add()
    End Sub
End Class