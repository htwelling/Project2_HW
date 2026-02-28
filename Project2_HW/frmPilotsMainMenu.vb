Public Class frmPilotsMainMenu
    Private Sub btnUpdatePilot_Click(sender As Object, e As EventArgs) Handles btnUpdatePilot.Click
        Dim frmUpdatePilotInfo As New frmUpdatePilotInfo

        frmUpdatePilotInfo.ShowDialog()
    End Sub

    Private Sub btnPastFlight_Click(sender As Object, e As EventArgs) Handles btnPastFlight.Click
        Dim frmShowPilotsPast As New frmShowPilotsPast

        frmShowPilotsPast.ShowDialog()
    End Sub

    Private Sub btnFutureFlight_Click(sender As Object, e As EventArgs) Handles btnFutureFlight.Click
        Dim frmShowPilotFuture As New frmShowPilotFuture

        frmShowPilotFuture.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class