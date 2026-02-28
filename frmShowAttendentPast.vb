Public Class frmShowAttendentPast
    Private Sub btnShowPastFlights_Click(sender As Object, e As EventArgs) Handles btnShowPastFlights.Click
        Try
            If SelectedAttendentID <= 0 Then
                MessageBox.Show("No attendant selected.")
                Exit Sub
            End If

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If

            ' ===============================
            '   CALL STOORED PROCEDURE
            ' ===============================
            Dim cmd As New OleDb.OleDbCommand("usp_GetPastFlightsByAttendant", m_conAdministrator)
            cmd.CommandType = CommandType.StoredProcedure

            ' OleDb uses positional parameters
            cmd.Parameters.AddWithValue("@AttendantID", SelectedAttendentID)

            Dim rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()

            lstPastFlights.Items.Clear()
            Dim totalMiles As Integer = 0

            While rdr.Read()
                Dim flightInfo As String =
                    "Flight #: " & rdr("strFlightNumber").ToString() & vbCrLf &
                    "Date: " & CDate(rdr("dtmTimeOfDeparture")).ToShortDateString() & vbCrLf &
                    "Miles: " & rdr("intMilesFlown").ToString() & vbCrLf &
                    "-------------------------------------------"

                lstPastFlights.Items.Add(flightInfo)
                totalMiles += CInt(rdr("intMilesFlown"))
            End While

            rdr.Close()
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
