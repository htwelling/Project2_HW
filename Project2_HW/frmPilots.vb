Imports System.Data.OleDb

Public Class frmPilots
    Private Sub frmSelectPilot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If OpenDatabaseConnectionSQLServer() = False Then
            MessageBox.Show("Failed to connect to database")
            Me.Close()
            Exit Sub
        End If

        Dim sql As String =
        "SELECT intPilotID, (strFirstName & ' ' & strLastName) AS PilotName " &
        "FROM TPilots " &
        "ORDER BY strLastName, strFirstName"

        Dim cmd As New OleDbCommand(sql, m_conAdministrator)
        Dim reader As OleDbDataReader = cmd.ExecuteReader()

        Dim dt As New DataTable
        dt.Load(reader)

        cboPilot.DataSource = dt
        cboPilot.DisplayMember = "PilotName"
        cboPilot.ValueMember = "intPilotID"

        cboPilot.SelectedIndex = -1   ' No selection at startup
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Ensure a pilot was selected
        If cboPilot.SelectedIndex = -1 Then
            MessageBox.Show("You must select a pilot from the list.", "Pilot Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Store selected PilotID in the global variable
        SelectedPilotID = CInt(cboPilot.SelectedValue)

        MessageBox.Show("Pilot Selected: " & cboPilot.Text &
                    vbCrLf & "Pilot ID Saved: " & SelectedPilotID,
                    "Pilot Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Dim frmPilotsMainMenu As New frmPilotsMainMenu

        frmPilotsMainMenu.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class