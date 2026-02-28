Public Class frmAdminPilots
    Private Sub btnAddPilots_Click(sender As Object, e As EventArgs) Handles btnAddPilots.Click
        Dim frmAddPilots As New frmAddPilots

        frmAddPilots.ShowDialog()
    End Sub

    Private Sub btnDeletePilots_Click(sender As Object, e As EventArgs) Handles btnDeletePilots.Click
        Dim frmDeletePilots As New frmDeletePilots

        frmDeletePilots.ShowDialog()
    End Sub

    Private Sub btnAddPilotsToFlights_Click(sender As Object, e As EventArgs) Handles btnAddPilotsToFlights.Click
        Dim frmAddPilotsToFlight As New frmAddPilotsToFlight

        frmAddPilotsToFlight.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class