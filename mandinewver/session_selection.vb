Imports System.Data.SqlClient
Public Class session_selection
    Dim a7 As New predefined
    Public sessionid As Integer
    Public Sub combo_add()
        Dim abc As String
        abc = "select session from session_selection"
        a7.abc_add(abc, ComboBox1)
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub session_selection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        combo_add()
    End Sub
    Private Sub Button1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        MDIParent1.Show()
        MDIParent1.sessionid = sessionid
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim abc As SqlCommand
        abc = New SqlCommand("select sessionid from session_selection where session=@session")
        abc.Parameters.AddWithValue("@session", SqlDbType.VarChar).Value = ComboBox1.Text
        Dim xyz(1) As String
        a7.array_list(abc, xyz, 0)
        sessionid = xyz(0)
    End Sub
End Class