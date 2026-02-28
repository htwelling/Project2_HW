Public Class frmAttendentsMainMenu
    Private Sub btnUpdateAttendent_Click(sender As Object, e As EventArgs) Handles btnUpdateAttendent.Click
        Dim frmUpdateAttendentInfo As New frmUpdateAttendentInfo

        frmUpdateAttendentInfo.ShowDialog()
    End Sub

    Private Sub btnPastFlight_Click(sender As Object, e As EventArgs) Handles btnPastFlight.Click
        Dim frmShowAttendentPast As New frmShowAttendentPast

        frmShowAttendentPast.ShowDialog()
    End Sub

    Private Sub btnFutureFlight_Click(sender As Object, e As EventArgs) Handles btnFutureFlight.Click
        Dim frmShowAttendentFuture As New frmShowAttendentFuture

        frmShowAttendentFuture.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class