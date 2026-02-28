Public Class frmCustomerMainMenu
    Private Sub btnUpdateInformation_Click(sender As Object, e As EventArgs) Handles btnUpdateInformation.Click
        Dim frmUpdateCustomer As New frmUpdateCustomer

        frmUpdateCustomer.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub

    Private Sub btnAddFlight_Click(sender As Object, e As EventArgs) Handles btnAddFlight.Click
        Dim AddCustomerFlight As New AddCustomerFlight

        AddCustomerFlight.ShowDialog()
    End Sub

    Private Sub btnShowPastFlights_Click(sender As Object, e As EventArgs) Handles btnShowPastFlights.Click
        Dim frmShowPastFlights As New frmShowPastFlights

        frmShowPastFlights.ShowDialog()
    End Sub

    Private Sub btnFutureFlights_Click(sender As Object, e As EventArgs) Handles btnFutureFlights.Click
        Dim frmFutureFlights As New frmFutureFlights

        frmFutureFlights.ShowDialog()
    End Sub
End Class