Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class frmAddPilotsToFlight
    Private Sub frmAssignPilotToFlight_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            ' ===============================
            '   LOAD PILOTS
            ' ===============================
            Dim strSQLPilots As String =
                "SELECT intPilotID, strLastName + ', ' + strFirstName AS PilotName 
                 FROM TPilots
                 ORDER BY strLastName"

            Dim cmdPilots As New OleDb.OleDbCommand(strSQLPilots, m_conAdministrator)
            Dim rdrPilots As OleDb.OleDbDataReader = cmdPilots.ExecuteReader()

            cboPilots.Items.Clear()
            While rdrPilots.Read()
                cboPilots.Items.Add(New With {
                    .ID = rdrPilots("intPilotID"),
                    .Name = rdrPilots("PilotName").ToString()
                })
            End While
            rdrPilots.Close()

            ' Display name in combo box
            cboPilots.DisplayMember = "Name"
            cboPilots.ValueMember = "ID"

            ' ===============================
            '   LOAD FLIGHTS
            ' ===============================
            Dim strSQLFlights As String =
                "SELECT intFlightID,
                        strFlightNumber + '   (' + CONVERT(varchar, dtmFlightDate, 101) + ')' AS FlightDisplay
                 FROM TFlights
                 WHERE dtmFlightDate > GETDATE()
                 ORDER BY dtmFlightDate"

            Dim cmdFlights As New OleDb.OleDbCommand(strSQLFlights, m_conAdministrator)
            Dim rdrFlights As OleDb.OleDbDataReader = cmdFlights.ExecuteReader()

            cboFlights.Items.Clear()
            While rdrFlights.Read()
                cboFlights.Items.Add(New With {
                    .ID = rdrFlights("intFlightID"),
                    .Name = rdrFlights("FlightDisplay").ToString()
                })
            End While
            rdrFlights.Close()

            cboFlights.DisplayMember = "Name"
            cboFlights.ValueMember = "ID"

            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show("Error loading form: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            If cboPilots.SelectedItem Is Nothing Then
                MessageBox.Show("Please select a pilot.")
                Exit Sub
            End If

            If cboFlights.SelectedItem Is Nothing Then
                MessageBox.Show("Please select a flight.")
                Exit Sub
            End If

            Dim selectedPilot = cboPilots.SelectedItem
            Dim selectedFlight = cboFlights.SelectedItem

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If


            ' ----------------------------------------------------
            ' GET NEXT PilotFlights ID
            ' ----------------------------------------------------
            Dim strSelectPilotFlights As String =
                "SELECT MAX(intPilotFlightID) + 1 AS NextID FROM TPilotFlights;"

            Dim cmdSelectPilotFlights As New OleDb.OleDbCommand(strSelectPilotFlights, m_conAdministrator)
            Dim rdr As OleDb.OleDbDataReader = cmdSelectPilotFlights.ExecuteReader()

            Dim nextPilotFlightsID As Integer = 1
            If rdr.Read() AndAlso Not rdr.IsDBNull(0) Then
                nextPilotFlightsID = CInt(rdr("NextID"))
            End If

            rdr.Close()

            Dim confirm As DialogResult =
                MessageBox.Show("Are you sure you want to assign this pilot to this flight?",
                                "Confirm Assignment",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question)

            If confirm = DialogResult.No Then Exit Sub

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If

            ' =================================================
            '   INSERT INTO TPilotFlights
            ' =================================================
            Dim strInsert As String =
                "INSERT INTO TPilotFlights (intPilotFlightID,intPilotID, intFlightID)
                 VALUES (?, ?, ?)"

            Dim cmd As New OleDb.OleDbCommand(strInsert, m_conAdministrator)
            cmd.Parameters.AddWithValue("@PilotFlightsID", nextPilotFlightsID)
            cmd.Parameters.AddWithValue("@PilotID", selectedPilot.ID)
            cmd.Parameters.AddWithValue("@FlightID", selectedFlight.ID)

            Dim rows As Integer = cmd.ExecuteNonQuery()

            CloseDatabaseConnection()

            If rows > 0 Then
                MessageBox.Show("Pilot successfully assigned to flight!")
            Else
                MessageBox.Show("Assignment failed.")
            End If

        Catch ex As Exception
            MessageBox.Show("Error assigning pilot: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()

        Dim frmPilotsMainMenu As New frmPilotsMainMenu

        frmPilotsMainMenu.ShowDialog()
    End Sub
End Class