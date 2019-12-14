Imports System.Data.SqlClient
Public Class company
    Dim a7 As New predefined
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Enter Then
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
        e.Handled = a7.trapkey(Asc(e.KeyChar))
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
            Button1.Focus()
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
        Dim abc As SqlCommand
        Dim lagachoice As String
        If ComboBox1.Text = "" Then
            lagachoice = "N"
        Else
            lagachoice = ComboBox1.Text
        End If
        If MsgBox("Save Record.....?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Vega 3.5") = MsgBoxResult.Yes Then
            abc = New SqlCommand("insert into company(company,pname,address,city,phone,email,com_option)values(@company,@pname,@address,@city,@phone,@email,@com_option)")
            abc.Parameters.AddWithValue("@company", SqlDbType.VarChar).Value = TextBox1.Text
            abc.Parameters.AddWithValue("@pname", SqlDbType.VarChar).Value = TextBox2.Text
            abc.Parameters.AddWithValue("@address", SqlDbType.VarChar).Value = TextBox3.Text
            abc.Parameters.AddWithValue("@city", SqlDbType.VarChar).Value = TextBox4.Text
            abc.Parameters.AddWithValue("@phone", SqlDbType.VarChar).Value = TextBox5.Text
            abc.Parameters.AddWithValue("@email", SqlDbType.VarChar).Value = TextBox6.Text
            abc.Parameters.AddWithValue("@com_option", SqlDbType.VarChar).Value = lagachoice
            a7.insert_data1(abc)
        End If
        a7.refresh_all(GroupBox1)
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
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Sub company_add()
        Dim abc As SqlCommand
        abc = New SqlCommand("select company,pname,address,city,phone,email,com_option from company")
        a7.list_add(abc, ListView1)
    End Sub
    Private Sub company_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ComboBox1.Items.Add("Y")
        ComboBox1.Items.Add("N")
        company_add()
    End Sub
End Class



