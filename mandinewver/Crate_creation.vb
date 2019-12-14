Imports System.Data.SqlClient
Public Class Crate_creation
    Dim a7 As New predefined
    Public companyid As Integer
    Public Sub company_add()
        ComboBox1.Items.Clear()
        Dim abc As String
        abc = "select company from company"
        a7.Abc_add(abc, ComboBox1)
    End Sub
    Private Sub ComboBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ComboBox1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyCode = Keys.Down Then
            TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyCode = Keys.Escape Then
            TextBox1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            TextBox3.Focus()
        End If
    End Sub
    Private Sub TextBox3_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyUp
        If e.KeyCode = Keys.Escape Then
            TextBox2.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            Button1.Focus()
        End If
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim itemname As String
        itemname = ComboBox1.Text
        companyid = a7.get_company_id(itemname)
        list_add()
        TextBox1.Focus()
    End Sub
    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        If e.KeyCode = Keys.Escape Then
            ComboBox1.Focus()
        End If
        If e.KeyCode = Keys.Enter Then
            Dim abc1 As SqlCommand
            Dim check_rec As Integer
            abc1 = New SqlCommand("select cname from crate where cname=vname and companyid=companyid")
            abc1.Parameters.AddWithValue("@vname", SqlDbType.VarChar).Value = TextBox1.Text
            abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
            check_rec = a7.check_rec(abc1)
            If check_rec = 0 Then
                TextBox2.Focus()
            Else
                MsgBox("Duplicate crate Name...!!", MsgBoxStyle.Critical, "Vega ver. 3.5")
                TextBox1.Focus()
                Exit Sub
            End If
            TextBox2.Focus()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        a7.DoConvert(TextBox1)
    End Sub
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        e.Handled = a7.Trapkey(Asc(e.KeyChar))
    End Sub
    Private Sub Crate_creation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        company_add()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim abc1 As SqlCommand
        Dim check_rec As Integer
        abc1 = New SqlCommand("select cname from crate where cname=@vname and companyid=@companyid")
        abc1.Parameters.AddWithValue("@vname", SqlDbType.VarChar).Value = TextBox1.Text
        abc1.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        check_rec = a7.check_rec(abc1)
        If check_rec = 0 Then
            TextBox2.Focus()
        Else
            MsgBox("Duplicate crate Name...!!", MsgBoxStyle.Critical, "Vega ver. 3.5")
            TextBox1.Focus()
            Exit Sub
        End If
        Dim abc As SqlCommand
        abc = New SqlCommand("insert into crate(cname,companyid,cost,opn_bal)values(@cname,@companyid,@cost,@opn_bal)")
        abc.Parameters.AddWithValue("@cname", SqlDbType.VarChar).Value = TextBox1.Text
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.AddWithValue("@cost", SqlDbType.Float).Value = TextBox2.Text
        abc.Parameters.AddWithValue("@opn_bal", SqlDbType.Int).Value = TextBox3.Text
        a7.insert_data1(abc)
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        company_add()
        list_add()
        ComboBox1.Focus()
    End Sub
    Public Sub list_add()
        Dim abc As SqlCommand
        abc = New SqlCommand("select cname,cost,opn_bal from crate where companyid=@companyid")
        abc.Parameters.AddWithValue("@companyid", SqlDbType.Int).Value = companyid
        a7.List_add(abc, ListView1)
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class