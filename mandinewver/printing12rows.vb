Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class printing12rows
    Public abcd As String
    Private Sub billprinting_report_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim rpt As New bill_printing_report12
        Dim myConnection As SqlConnection
        Dim MyCommand As New SqlCommand()
        Dim myDA As New SqlDataAdapter()
        Dim myDS As New DataSet1()
        Dim connetionstring As String
        'connetionstring = "Data Source=.\SQLEXPRESS;AttachDbFilename=|datadirectory|\kdata.mdf;Integrated Security=True;User Instance=True"
        'connetionstring = "server=guri\SQLEXPRESS;database=mandidb;Integrated Security=True"
        ''connetionstring = "Data Source=.\SQLEXPRESS;AttachDbFilename=|datadirectory|\mydata.mdf;Integrated Security=True;User Instance=True"
        connetionstring = "Data Source=.\SQLEXPRESS;AttachDbFilename=|datadirectory|\mydata.mdf;user id=sa;password=abc"
        'connetionstring = "Data Source=server\SQLEXPRESS;AttachDbFilename=d:\vega1\mydata.mdf;user id=sa;password=abc"
        myConnection = New SqlConnection(connetionstring)
        MyCommand.Connection = myConnection
        MyCommand.CommandText = "SELECT * FROM printing"
        MyCommand.CommandType = CommandType.Text
        myDA.SelectCommand = MyCommand
        myDA.Fill(myDS, "Printing")
        rpt.SetDataSource(myDS)
        Dim fName, fname1 As TextObject
        fName = rpt.ReportDefinition.Sections("Section2").ReportObjects("Text26")
        fname1 = rpt.ReportDefinition.Sections("Section2").ReportObjects("Text27")
        fName.Text = abcd
        fname1.Text = abcd
        CrystalReportViewer1.ReportSource = rpt
        CrystalReportViewer1.Refresh()
    End Sub
End Class