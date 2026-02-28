Public Class frmAdministration
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If txtPassword.Text = "Password" Then
            Dim frmAdminMainMenu As New frmAdminMainMenu

            frmAdminMainMenu.ShowDialog()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class