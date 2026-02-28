Imports System.Data.OleDb

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

            ' -----------------------------------------------
            ' DELETE USING STORED PROCEDURE
            ' -----------------------------------------------
            Try
                Dim cmd As New OleDbCommand("usp_DeletePilot", m_conAdministrator)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@intAttendantID", pilotID)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Pilot successfully deleted!")
            Catch ex As Exception
                MessageBox.Show("Error deleting pilot: " & ex.Message)
            End Try

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