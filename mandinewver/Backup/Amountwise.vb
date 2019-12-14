Imports System.Data.SqlClient
Public Class amountwise
    Dim a7 As New predefined
    Public companyid, bookid As Integer
    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        e.Handled = a7.trapkey(Asc(e.KeyChar))
    End Sub
    Public Sub list_show()
        Dim abc As SqlCommand
        abc = New SqlCommand("select a.bname,b.cname,c.name,a.nag,a.weight,a.rate,a.total from bikri a, customer b,vegetable c where a.cid=b.customerid and a.vegetableid=c.vegetableid and a.sessionid= @sessionid  and a.companyid=@companyid and a.bookid=@bookid and entry_date=@date and a.total=@total")
        abc.Parameters.Add("@date", SqlDbType.DateTime).Value = Daily_Bikri_Report.DateTimePicker1.Value.Date
        abc.Parameters.Add("@sessionid", SqlDbType.VarChar).Value = MDIParent1.sessionid
        abc.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid
        abc.Parameters.Add("@bookid", SqlDbType.VarChar).Value = bookid
        abc.Parameters.Add("@total", SqlDbType.Int).Value = TextBox2.Text
        a7.list_add(abc, ListView1)
    End Sub
    Private Sub amountwise_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub ListView1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView1.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub TextBox2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyUp
        If e.KeyValue = Keys.Escape Then
            Me.Close()
        End If
        If e.KeyValue = Keys.Enter Then
            If TextBox2.Text = "" Then Exit Sub
            list_show()
        End If
    End Sub
End Class