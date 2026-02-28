Public Class frmAdminMainMenu
    Private Sub btnManagePilots_Click(sender As Object, e As EventArgs) Handles btnManagePilots.Click
        Dim frmAdminPilots As New frmAdminPilots

        frmAdminPilots.ShowDialog()
    End Sub

    Private Sub btnManageAttendents_Click(sender As Object, e As EventArgs) Handles btnManageAttendents.Click
        Dim frmAdminAttendents As New frmAdminAttendents

        frmAdminAttendents.ShowDialog()
    End Sub

    Private Sub btnStats_Click(sender As Object, e As EventArgs) Handles btnStats.Click
        Dim frmStats As New frmStats

        frmStats.ShowDialog()
    End Sub
End Class