Imports System.Data.SqlClient
Public Class Modify_cust_bill_setup
    Dim a7 As New predefined
    Public companyid, customerid As Integer
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        a7.DoConvert(TextBox2)
    End Sub
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        e.Handled = a7.trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Escape Then
            TextBox1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        e.Handled = a7.trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Escape Then
            TextBox2.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim abc1 As SqlCommand
        abc1 = New SqlCommand("update cust_bill_setup set arhat=@arhat,laga=@laga where companyid=@companyid and customerid=@customerid")
        abc1.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc1.Parameters.Add("@customerid", SqlDbType.Int).Value = customerid
        abc1.Parameters.Add("@laga", SqlDbType.Int).Value = Val(TextBox3.Text)
        abc1.Parameters.Add("@arhat", SqlDbType.Int).Value = Val(TextBox2.Text)
        a7.update_data(abc1)
        Me.Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Modify_cust_bill_setup_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If list_cust_bill_setup.Visible = True Then
            list_cust_bill_setup.list_show()
        End If
    End Sub
    Private Sub cust_bill_setup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        show_data()
    End Sub
    Public Sub show_data()
        Dim abc As SqlCommand
        abc = New SqlCommand("select arhat,laga from cust_bill_setup where customerid=@customerid and companyid=@companyid")
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@customerid", SqlDbType.Int).Value = customerid
        Dim xyz(2) As String
        a7.array_list(abc, xyz, 0)
        TextBox2.Text = xyz(0)
        TextBox3.Text = xyz(1)
    End Sub
End Class