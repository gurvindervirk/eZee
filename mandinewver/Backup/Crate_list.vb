Imports System.Data.SqlClient
Public Class Crate_list
    Dim a7 As New predefined
    Public companyid As Integer
    Public Sub company_add()
        ComboBox1.Items.Clear()
        Dim abc As String
        abc = "select company from company"
        a7.abc_add(abc, ComboBox1)
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Down Then
            ListView1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            ListView1.Focus()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
        list_add()
    End Sub
    Public Sub list_add()
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname,crateid from crate where companyid=@companyid")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        ListView1.Items.Clear()
        a7.list_add1(abc, ListView1)
    End Sub
    Private Sub Crate_list_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.S Then
            Modifycrate.companyid = companyid
            Modifycrate.crateid = Val(ListView1.SelectedItems.Item(0).SubItems(2).Text)
            Modifycrate.TextBox2.Text = ComboBox1.Text
            Modifycrate.TextBox3.Text = ListView1.SelectedItems.Item(0).SubItems(1).Text
            Modifycrate.Show()
        End If
    End Sub
End Class