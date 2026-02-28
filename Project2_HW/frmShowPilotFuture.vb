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

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            Dim sql As String =
                "SELECT TFlights.strFlightNumber,
                    TFlights.strOrigin,
                    TFlights.strDestination,
                    TFlights.dtmDepartureDate,
                    TFlights.intMiles
             FROM TPilotFlights
             INNER JOIN TFlights
                    ON TPilotFlights.intFlightID = TFlights.intFlightID
             WHERE TPilotFlights.intPilotID = @PilotID
               AND TFlights.dtmDepartureDate > GETDATE()
             ORDER BY TFlights.dtmDepartureDate ASC"

            Dim cmd As New OleDbCommand(sql, m_conAdministrator)
            cmd.Parameters.AddWithValue("@PilotID", SelectedPilotID)

            Dim reader As OleDbDataReader = cmd.ExecuteReader()

            Dim totalMiles As Integer = 0

            While reader.Read()
                Dim flightInfo As String =
                    $"{reader("strFlightNumber")} | " &
                    $"{reader("strOrigin")} -> {reader("strDestination")} | " &
                    $"{CDate(reader("dtmDepartureDate")).ToShortDateString()} | " &
                    $"{reader("intMiles")} miles"

                lstFutureFlights.Items.Add(flightInfo)

                totalMiles += CInt(reader("intMiles"))
            End While

            reader.Close()
            CloseDatabaseConnection()

            lblTotalMiles.Text = totalMiles.ToString()

        Catch ex As Exception
            MessageBox.Show("Error loading future flights: " & ex.Message)
        End Try
    End Sub
End Class