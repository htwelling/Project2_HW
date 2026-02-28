Public Class AddCustomerFlight
    Private Sub frmBookFlight_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim strSelect As String
            Dim cmdSelect As OleDb.OleDbCommand
            Dim drSourceTable As OleDb.OleDbDataReader
            Dim dtFlights As New DataTable

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
            End If

            'Select future flights only
            strSelect = "SELECT intFlightID, strFlightInfo + ' (' + CONVERT(varchar, dtFlightDate) + ')' AS FlightDisplay 
                     FROM TFlights
                     WHERE dtFlightDate >= GETDATE()
                     ORDER BY dtFlightDate"

            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dtFlights.Load(drSourceTable)

            'Bind results to combo box
            cboFlights.DisplayMember = "FlightDisplay"
            cboFlights.ValueMember = "intFlightID"
            cboFlights.DataSource = dtFlights

            drSourceTable.Close()
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnAddFlight_Click(sender As Object, e As EventArgs) Handles btnAddFlight.Click
        Try
            'Confirm booking
            Dim result = MessageBox.Show("Are you sure you want to book this flight?",
                                         "Confirm Booking",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question)

            If result = DialogResult.No Then
                Exit Sub
            End If

            'Get selected flight
            Dim intFlightID As Integer = CInt(cboFlights.SelectedValue)

            'Fake seat number for now
            Dim strSeat As String = "15A"

            Dim strSelect As String
            Dim strInsert As String
            Dim cmdSelect As OleDb.OleDbCommand
            Dim cmdInsert As OleDb.OleDbCommand
            Dim drSourceTable As OleDb.OleDbDataReader
            Dim intNextPK As Integer
            Dim intRows As Integer

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
            End If

            'Get the next primary key
            strSelect = "SELECT MAX(intCustomerFlightID) + 1 AS NextPK FROM TCustomerFlights"
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            drSourceTable.Read()

            If drSourceTable.IsDBNull(0) Then
                intNextPK = 1
            Else
                intNextPK = CInt(drSourceTable("NextPK"))
            End If

            drSourceTable.Close()

            'Insert booking row
            strInsert = "INSERT INTO TCustomerFlights (intCustomerFlightID, intCustomerID, intFlightID, strSeatNumber) " &
                        "VALUES (" & intNextPK & ", " & SelectedCustomerID & ", " & intFlightID & ", '" & strSeat & "')"

            cmdInsert = New OleDb.OleDbCommand(strInsert, m_conAdministrator)
            intRows = cmdInsert.ExecuteNonQuery()

            If intRows > 0 Then
                MessageBox.Show("Flight successfully booked!")
            Else
                MessageBox.Show("Flight booking failed.")
            End If

            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class