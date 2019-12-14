Imports System.Data.SqlClient
Public Class modify_company
    Dim a7 As New predefined
    Dim cid As Integer
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Enter Then
            If TextBox1.Text = "" Then Exit Sub
            TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        a7.DoConvert(TextBox1)
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub
    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        a7.DoConvert(TextBox2)
    End Sub
    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox4.Focus()
        End If
    End Sub
    Private Sub TextBox4_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox4.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox5.Focus()
        End If
    End Sub
    Private Sub TextBox5_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox5.KeyUp
        If e.KeyCode = Keys.Enter Then
            TextBox6.Focus()
        End If
    End Sub
    Private Sub TextBox6_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyUp
        If e.KeyCode = Keys.Enter Then
            ComboBox1.Focus()
        End If
    End Sub
    Private Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        a7.DoConvert(TextBox3)
    End Sub
    Private Sub TextBox4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        a7.DoConvert(TextBox4)
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then Exit Sub
        Dim lagachoice As String
        If ComboBox1.Text = "" Then
            lagachoice = "N"
        Else
            lagachoice = ComboBox1.Text
        End If
        Dim abc As SqlCommand
        If MsgBox("Update Record.....?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Vega 3.5") = MsgBoxResult.Yes Then
            abc = New SqlCommand("update company set company=@company,pname=@pname,address=@address,city=@city,phone=@phone,email=@email,com_option=@com_option where companyid=@cid")
            abc.Parameters.AddWithValue("@company", SqlDbType.VarChar).Value = TextBox1.Text
            abc.Parameters.AddWithValue("@pname", SqlDbType.VarChar).Value = TextBox2.Text
            abc.Parameters.AddWithValue("@address", SqlDbType.VarChar).Value = TextBox3.Text
            abc.Parameters.AddWithValue("@city", SqlDbType.VarChar).Value = TextBox4.Text
            abc.Parameters.AddWithValue("@phone", SqlDbType.VarChar).Value = TextBox5.Text
            abc.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = TextBox6.Text
            abc.Parameters.AddWithValue("@com_option", SqlDbType.VarChar).Value = lagachoice
            abc.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = cid
            a7.Update_data(abc)
        End If
        a7.Refresh_all(GroupBox1)
        company_add()
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Y")
        ComboBox1.Items.Add("N")
        TextBox1.Focus()
    End Sub
    Private Sub company_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        Dim abc As SqlCommand
        If MsgBox("Delete Record ......?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Delete") = MsgBoxResult.Yes Then
            cid = Val(ListView1.SelectedItems.Item(0).SubItems(7).Text)
            abc = New SqlCommand("delete  from company where companyid=@cid")
            abc.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = cid
            a7.Delete_data(abc)
            a7.Refresh_all(GroupBox1)
            TextBox1.Focus()
            company_add()
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Y")
            ComboBox1.Items.Add("N")
        End If
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Enter Then
            cid = Val(ListView1.SelectedItems.Item(0).SubItems(7).Text)
            Dim xyz(7) As String
            Dim abc As SqlCommand
            abc = New SqlCommand("select * from company where companyid=@cid")
            abc.Parameters.AddWithValue("@cid", SqlDbType.Int).Value = cid
            a7.array_list(abc, xyz, 0)
            TextBox1.Text = xyz(0)
            TextBox2.Text = xyz(1)
            TextBox3.Text = xyz(2)
            TextBox4.Text = xyz(3)
            TextBox5.Text = xyz(4)
            TextBox6.Text = xyz(5)
            ComboBox1.Text = xyz(6)
            TextBox1.Focus()
        End If
    End Sub
    Public Sub company_add()
        Dim abc As SqlCommand
        abc = New SqlCommand("select company,pname,address,city,phone,email,com_option,companyid from company")
        a7.List_add(abc, ListView1)
    End Sub
    Private Sub modify_company_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
        ComboBox1.Items.Add("Y")
        ComboBox1.Items.Add("N")
    End Sub
End Class