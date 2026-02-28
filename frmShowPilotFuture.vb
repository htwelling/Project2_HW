Imports System.Data.OleDb

Public Class frmShowPilotFuture
    Private Sub frmPilotFutureFlights_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If SelectedPilotID <= 0 Then
            MessageBox.Show("Pilot not selected.")
            Me.Close()
            Exit Sub
        End If
    End Sub
    Private Sub btnShowFutureFlights_Click(sender As Object, e As EventArgs) Handles btnShowFutureFlights.Click
        Try
            lstFutureFlights.Items.Clear()
            lblTotalMiles.Text = ""

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If

            ' ---------------------------------------------------------
            ' USE STORED PROCEDURE: usp_GetFutureFlightsByPilot
            ' ---------------------------------------------------------
            Dim cmd As New OleDbCommand("usp_GetFutureFlightsByPilot", m_conAdministrator)
            cmd.CommandType = CommandType.StoredProcedure

            ' Pass global PilotID
            cmd.Parameters.AddWithValue("@PilotID", SelectedPilotID)

            Dim reader As OleDbDataReader = cmd.ExecuteReader()

            Dim totalMiles As Integer = 0

            ' ------------------------------
            ' Read each future flight record
            ' ------------------------------
            While reader.Read()

                Dim flightInfo As String =
                    "Flight #: " & reader("strFlightNumber").ToString() & vbCrLf &
                    "Date: " & CDate(reader("dtmFlightDate")).ToShortDateString() & vbCrLf &
                    "Departs: " & reader("dtmTimeOfDeparture").ToString() & vbCrLf &
                    "Lands: " & reader("dtmTimeOfLanding").ToString() & vbCrLf &
                    "From Airport ID: " & reader("intFromAirportID").ToString() & vbCrLf &
                    "To Airport ID: " & reader("intToAirportID").ToString() & vbCrLf &
                    "Miles: " & reader("intMilesFlown").ToString() & vbCrLf &
                    "-----------------------------------------"

                lstFutureFlights.Items.Add(flightInfo)

                totalMiles += CInt(reader("intMilesFlown"))
            End While

            reader.Close()
            CloseDatabaseConnection()

            lblTotalMiles.Text = totalMiles.ToString()

        Catch ex As Exception
            MessageBox.Show("Error loading future flights: " & ex.Message)
        End Try

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class