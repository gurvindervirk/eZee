Imports System.Data.SqlClient
Public Class unlockdate
    Dim a7 As New predefined
    Public Sub list_add()
        Dim abc As SqlCommand
        abc = New SqlCommand("select id,locked_date from locked order by locked_date desc")
        a7.List_add(abc, ListView1)
    End Sub
    Private Sub unlockdate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        list_add()
    End Sub
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim abc, i, voucher_chk, crate_chk As Integer
        Dim abc1, abc2, abc3, abc4 As SqlCommand
        Dim date_type As DateTime
        abc = ListView1.Items.Count
        i = 0
        voucher_chk = 0
        crate_chk = 0
        Do While i < abc
            If ListView1.Items(i).Checked = True Then
                abc4 = New SqlCommand("select locked_date from locked where id=@locked_id")
                abc4.Parameters.Add("@locked_id", SqlDbType.Int).Value = ListView1.Items(i).Text
                Dim xyz(0) As String
                a7.array_list(abc4, xyz, 0)
                date_type = xyz(0)
                abc2 = New SqlCommand("select * from voucher where date=@lockeddate")
                abc2.Parameters.Add("@lockeddate", SqlDbType.DateTime).Value = date_type
                voucher_chk = a7.check_rec(abc2)
                abc3 = New SqlCommand("select * from crateledger where date=@lockeddate")
                abc3.Parameters.Add("@lockeddate", SqlDbType.DateTime).Value = date_type
                crate_chk = a7.check_rec(abc3)
                If voucher_chk = 0 And crate_chk = 0 Then
                    abc1 = New SqlCommand("delete from locked where id=@locked_id")
                    abc1.Parameters.Add("@locked_id", SqlDbType.Int).Value = ListView1.Items(i).Text
                    a7.Delete_data(abc1)
                End If
            End If
            i = i + 1
        Loop
        list_add()
    End Sub
End Class