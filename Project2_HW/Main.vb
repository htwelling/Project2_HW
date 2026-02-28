Public Class Main
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If radCustomer.Checked = True Then
            Dim frmCustomer As New frmCustomer

            frmCustomer.ShowDialog()
        End If

        If radAttendents.Checked = True Then
            Dim frmAttendents As New frmAttendents

            frmAttendents.ShowDialog()
        End If

        If radPilots.Checked = True Then
            Dim frmPilots As New frmPilots

            frmPilots.ShowDialog()
        End If

        If radAdministration.Checked = True Then
            Dim frmAdministration As New frmAdministration

            frmAdministration.ShowDialog()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class
