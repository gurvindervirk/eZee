Imports System.Data.SqlClient
Public Class Modifycrate
    Dim a7 As New predefined
    Public companyid, crateid As Integer
    Public Sub company_add()
        ComboBox1.Items.Clear()
        Dim abc As String
        abc = "select company from company"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
        TextBox1.Focus()
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        a7.DoConvert(TextBox1)
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Enter Then
            If ComboBox1.Text <> "" And TextBox1.Text <> "" Then
                Dim abc As SqlCommand
                abc = New SqlCommand("update crate set cname=@cname,companyid=@companyid where crateid=@crateid")
                abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
                abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
                abc.Parameters.AddWithValue("@crateid", SqlDbType.Int).Value = crateid
                a7.insert_data1(abc)
                Me.Close()
            End If
        End If
    End Sub

    Private Sub Modifycrate_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Crate_list.list_add()
    End Sub
    Private Sub Modifycrate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.Text <> "" And TextBox1.Text <> "" Then
            Dim abc As SqlCommand
            abc = New SqlCommand("update crate set cname=@cname,companyid=@companyid where crateid=@crateid")
            abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
            abc.Parameters.AddWithValue("@crateid", SqlDbType.Int).Value = crateid
            a7.insert_data1(abc)
            Me.Close()
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class