Public Class frmAdminAttendents
    Private Sub btnAddAttendent_Click(sender As Object, e As EventArgs) Handles btnAddAttendent.Click
        Dim frmAddAttendent As New frmAddAttendent

        frmAddAttendent.ShowDialog()
    End Sub

    Private Sub btnDeleteAttendent_Click(sender As Object, e As EventArgs) Handles btnDeleteAttendent.Click
        Dim frmDeleteAttendent As New frmDeleteAttendent

        frmDeleteAttendent.ShowDialog()
    End Sub

    Private Sub btnAddAttendentToFlight_Click(sender As Object, e As EventArgs) Handles btnAddAttendentToFlight.Click
        Dim frmAddAttendentToFlight As New frmAddAttendentToFlight

        frmAddAttendentToFlight.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class