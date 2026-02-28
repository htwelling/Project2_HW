Imports System.Data.OleDb

Public Class frmShowPilotsPast
    Private Sub frmPilotPastFlights_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If SelectedPilotID <= 0 Then
            MessageBox.Show("Pilot not selected.")
            Close()
            Exit Sub
        End If

    End Sub
    Private Sub btnShowPastFlights_Click(sender As Object, e As EventArgs) Handles btnShowPastFlights.Click
        Try
            lstPastFlights.Items.Clear()

            ' Open connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            ' Call stored procedure
            Dim cmd As New OleDbCommand("usp_GetPastFlightsByPilot", m_conAdministrator)
            cmd.CommandType = CommandType.StoredProcedure

            ' Stored procedure parameter
            cmd.Parameters.AddWithValue("@PilotID", SelectedPilotID)

            Dim reader As OleDbDataReader = cmd.ExecuteReader()
            Dim totalMiles As Integer = 0

            While reader.Read()
                Dim flightInfo As String =
                    $"Date: {CDate(reader("dtmFlightDate")).ToShortDateString()} | " &
                    $"Flight #: {reader("strFlightNumber")} | " &
                    $"Departure: {CDate(reader("dtmTimeOfDeparture")).ToShortTimeString()} | " &
                    $"Landing: {CDate(reader("dtmTimeOfLanding")).ToShortTimeString()} | " &
                    $"From Airport ID: {reader("intFromAirportID")} | " &
                    $"To Airport ID: {reader("intToAirportID")} | " &
                    $"Miles Flown: {reader("intMilesFlown")}"

                lstPastFlights.Items.Add(flightInfo)

                totalMiles += CInt(reader("intMilesFlown"))
            End While

            reader.Close()
            CloseDatabaseConnection()

            lblTotalMiles.Text = totalMiles.ToString()

        Catch ex As Exception
            MessageBox.Show("Error loading past flights: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class