Imports System.Data.OleDb

Public Class frmAddAttendentToFlight
    Private Sub frmAssignAttendantToFlight_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            ' ===============================
            '   LOAD ATTENDANTS
            ' ===============================
            Dim strSQLAttendants As String =
                "SELECT intAttendantID, strLastName + ', ' + strFirstName AS AttendantName 
                 FROM TAttendants
                 ORDER BY strLastName"

            Dim cmdAttendants As New OleDbCommand(strSQLAttendants, m_conAdministrator)
            Dim rdrAttendants As OleDbDataReader = cmdAttendants.ExecuteReader()

            cboAttendants.Items.Clear()
            While rdrAttendants.Read()
                cboAttendants.Items.Add(New With {
                    .ID = rdrAttendants("intAttendantID"),
                    .Name = rdrAttendants("AttendantName").ToString()
                })
            End While
            rdrAttendants.Close()

            cboAttendants.DisplayMember = "Name"
            cboAttendants.ValueMember = "ID"

            ' ===============================
            '   LOAD FLIGHTS
            ' ===============================
            Dim strSQLFlights As String =
                "SELECT intFlightID,
                        strFlightNumber + '   (' + CONVERT(varchar, dtmFlightDate, 101) + ')' AS FlightDisplay
                 FROM TFlights
                 WHERE dtmFlightDate > GETDATE()
                 ORDER BY dtmFlightDate"

            Dim cmdFlights As New OleDbCommand(strSQLFlights, m_conAdministrator)
            Dim rdrFlights As OleDbDataReader = cmdFlights.ExecuteReader()

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
            If cboAttendants.SelectedItem Is Nothing Then
                MessageBox.Show("Please select an attendant.")
                Exit Sub
            End If

            If cboFlights.SelectedItem Is Nothing Then
                MessageBox.Show("Please select a flight.")
                Exit Sub
            End If

            Dim selectedAttendant = cboAttendants.SelectedItem
            Dim selectedFlight = cboFlights.SelectedItem

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If

            ' ----------------------------------------------------
            ' GET NEXT AttendantFlights ID
            ' ----------------------------------------------------
            Dim strSelectAttendantFlights As String =
                "SELECT MAX(intAttendantFlightID) + 1 AS NextID FROM TAttendantFlights;"

            Dim cmdSelectAttendantFlights As New OleDb.OleDbCommand(strSelectAttendantFlights, m_conAdministrator)
            Dim rdr As OleDb.OleDbDataReader = cmdSelectAttendantFlights.ExecuteReader()

            Dim nextAttendantFlightsID As Integer = 1
            If rdr.Read() AndAlso Not rdr.IsDBNull(0) Then
                nextAttendantFlightsID = CInt(rdr("NextID"))
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
            '   INSERT INTO TAttendantFlights
            ' =================================================
            Dim strInsert As String =
                "INSERT INTO TAttendantFlights (intAttendantFlightID, intAttendantID, intFlightID)
                 VALUES (?, ?, ?)"

            Dim cmd As New OleDbCommand(strInsert, m_conAdministrator)
            cmd.Parameters.AddWithValue("@AttendantFlightsID", nextAttendantFlightsID)
            cmd.Parameters.AddWithValue("@AttendantID", selectedAttendant.ID)
            cmd.Parameters.AddWithValue("@FlightID", selectedFlight.ID)

            Dim rows As Integer = cmd.ExecuteNonQuery()

            CloseDatabaseConnection()

            If rows > 0 Then
                MessageBox.Show("Attendant successfully assigned to flight!")
            Else
                MessageBox.Show("Assignment failed.")
            End If

        Catch ex As Exception
            MessageBox.Show("Error assigning attendant: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()

        Dim frmAttendentsMainMenu As New frmAttendentsMainMenu

        frmAttendentsMainMenu.ShowDialog()
    End Sub
End Class