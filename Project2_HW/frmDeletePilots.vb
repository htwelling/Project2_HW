Public Class frmDeletePilots
    Private Sub frmDeletePilot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            Dim strSQL As String =
            "SELECT intPilotID,
                    (strLastName + ', ' + strFirstName) AS FullName
             FROM TPilots
             ORDER BY strLastName, strFirstName;"

            Dim cmd As New OleDb.OleDbCommand(strSQL, m_conAdministrator)
            Dim rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()

            Dim dt As New DataTable()
            dt.Load(rdr)

            cboDeletePilot.DisplayMember = "FullName"
            cboDeletePilot.ValueMember = "intPilotID"
            cboDeletePilot.DataSource = dt

            rdr.Close()
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show("Error loading pilots: " & ex.Message)
        End Try
    End Sub
    Private Sub btnDeletePilot_Click(sender As Object, e As EventArgs) Handles btnDeletePilot.Click
        Try
            If cboDeletePilot.Text = "" Then
                MessageBox.Show("Please select a pilot to delete.")
                Exit Sub
            End If

            Dim pilotID As Integer = CInt(cboDeletePilot.SelectedValue)
            Dim pilotName As String = cboDeletePilot.Text

            Dim result As DialogResult =
                MessageBox.Show("Are you sure you want to delete pilot: " & pilotName & "?",
                                "Confirm Delete",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning)

            If result = DialogResult.No Then Exit Sub

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If

            Dim strDelete As String =
                "DELETE FROM TPilots WHERE intPilotID = ?;"

            Dim cmd As New OleDb.OleDbCommand(strDelete, m_conAdministrator)
            cmd.Parameters.AddWithValue("@ID", pilotID)

            Dim rows As Integer = cmd.ExecuteNonQuery()

            If rows > 0 Then
                MessageBox.Show("Pilot deleted successfully.")
            Else
                MessageBox.Show("Delete failed.")
            End If

            CloseDatabaseConnection()
            Me.Close()   ' Return to Pilot Main Menu

        Catch ex As Exception
            MessageBox.Show("Error deleting pilot: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()

        Dim frmPilotsMainMenu As New frmPilotsMainMenu

        frmPilotsMainMenu.ShowDialog()
    End Sub
End Class