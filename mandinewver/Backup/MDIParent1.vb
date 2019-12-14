Imports System.Windows.Forms
Public Class MDIParent1
    Public sessionid As Integer
    Public sessionname As String
    Public utype As String
    Public entry_date As Date
    Private Sub MDIParent1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        login.Close()
        systemsetup.Close()
    End Sub
    Private Sub MDIParent1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        login.Visible = False
        systemsetup.Visible = False
        Label6.Text = Date.Today
        Label7.Text = entry_date.Date
        If utype = "USER" Then
            LockDatemenu.Visible = False
        End If
        If utype = "ADMIN" Then
            AdminPanelToolStripMenuItem.Visible = True
            'ToolStripMenuItem3.Visible = True
        Else
            AdminPanelToolStripMenuItem.Visible = False
            'ToolStripMenuItem3.Visible = False
        End If

    End Sub
    Private Sub companycreation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles companycreation.Click
        company.Show()
    End Sub
    Private Sub sessioncreationmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sessioncreationmenu.Click
        createfinacialyear.Show()
    End Sub
    Private Sub bookcreationmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bookcreationmenu.Click
        Bookentry.Show()
    End Sub
    Private Sub Vegetablecreationmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Vegetablecreationmenu.Click
        Vegetablesentry.Show()
    End Sub
    Private Sub Customercreationmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Customercreationmenu.Click
        customer_opening_balance.Show()
        'customer_entry_form.Show()
    End Sub
    Private Sub Bikrientrymenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Bikrientrymenu.Click
        bikribook.Show()
    End Sub
    Private Sub receiptentrymenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles receiptentrymenu.Click
        cashrecipt.Show()
    End Sub
    Private Sub adventrymenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles adventrymenu.Click
        advances_entry.Show()
    End Sub
    Private Sub bikrilistmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bikrilistmenu.Click
        Daily_Bikri_Report.Show()
    End Sub
    Private Sub receiptlistmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles receiptlistmenu.Click
        Daily_Cash_Recipt.Show()
    End Sub
    Private Sub Advanceslistmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Advanceslistmenu.Click
        'Advancelist.Show()
    End Sub
    Private Sub bills_show_menu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bills_show_menu.Click
        Bills_report.Show()
    End Sub
    Private Sub Outstandingsmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Outstandingsmenu.Click
        Outstanding.Show()
    End Sub
    Private Sub Ledgermenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ledgermenu.Click
        customerledger1.Show()
    End Sub
    Private Sub modifycompanymenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles modifycompanymenu.Click
        modify_company.Show()
    End Sub
    Private Sub Modifyvegetablesmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Modifyvegetablesmenu.Click
        modify_vegetable.Show()
    End Sub
    Private Sub Modifycustomermenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Modifycustomermenu.Click
        change_cname.Show()
    End Sub
    Private Sub MonthlyBikrimenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MonthlyBikrimenu.Click
        Monthlyconsolidatedreport.Show()
    End Sub
    Private Sub BooKWiseDetailToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BooKWiseDetailToolStripMenuItem.Click
        bookwiselist.Show()
    End Sub
    Private Sub ItemTransfersDetailsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ItemTransfersDetailsToolStripMenuItem.Click
        bill_transfer_detail.Show()
    End Sub
    Private Sub CashTransferDetailsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CashTransferDetailsToolStripMenuItem.Click
        cash_transfer_detail.Show()
    End Sub
    Private Sub AdvanceTransferDetailsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AdvanceTransferDetailsToolStripMenuItem.Click
        advance_transfer_detail.Show()
    End Sub
    Private Sub YearlyBikriToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Yearlyconsolidatedreport.Show()
    End Sub
    Private Sub custbillsetupentry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles custbillsetupentry.Click
        cust_bill_setup.Show()
    End Sub
    Private Sub custbillsetupmodify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles custbillsetupmodify.Click
        list_cust_bill_setup.Show()
    End Sub
    Private Sub cratebillprinting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cratebillprinting.Click
        cratebills.Show()
    End Sub
    Private Sub crateoutstanding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crateoutstanding.Click
        consolidatedsummarycrate.Show()
    End Sub
    Private Sub crateledgerprinting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crateledgerprinting.Click
        crateledger.Show()
    End Sub
    Private Sub ChangeSessionToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeSessionToolStripMenuItem.Click
        Change_session.Show()
    End Sub
    Private Sub LockDatemenu_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LockDatemenu.Click
        date_locking.Show()
    End Sub
    Private Sub openingbalancecrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles openingbalancecrate.Click
        openingbalanceadd_crate.Show()
    End Sub
    Private Sub crateissueentry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crateissueentry.Click
        Crate_issue.Show()
    End Sub
    Private Sub CrateRecepitentry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrateRecepitentry.Click
        crate_receipt.Show()
    End Sub
    Private Sub cratecreation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cratecreationentry.Click
        Crate_creation.Show()
    End Sub
    Private Sub modifycrateentry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles modifycrateentry.Click
        Crate_list.Show()
    End Sub
    Private Sub MergebikriToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MergebikriToolStripMenuItem.Click
        merge_customer.Show()
    End Sub
    Private Sub MergeListToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MergeListToolStripMenuItem.Click
        Mergebikrilist.Show()
    End Sub
    Private Sub modifybooksmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles modifybooksmenu.Click
        booklist.Show()
    End Sub
    Private Sub AlterPasswordToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AlterPasswordToolStripMenuItem.Click
        Changepassword.Show()
    End Sub
    Private Sub CreatePasswordToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CreatePasswordToolStripMenuItem.Click
        createpassword.Show()
    End Sub
    Private Sub LedgerCrateMarkaWiseToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LedgerCrateMarkaWiseToolStripMenuItem.Click
        crateledgermarkawise.Show()
    End Sub

    
    Private Sub UnlockDateToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UnlockDateToolStripMenuItem.Click
        unlockdate.Show()
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click

    End Sub
End Class
