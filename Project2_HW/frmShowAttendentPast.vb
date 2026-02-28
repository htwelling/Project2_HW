Public Class frmShowAttendentPast
    Private Sub btnShowPastFlights_Click(sender As Object, e As EventArgs) Handles btnShowPastFlights.Click
        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            Dim strSQL As String =
                "SELECT F.intFlightID,
                    F.strFlightNumber,
                    F.strOrigin,
                    F.strDestination,
                    F.dtmDeparture,
                    F.intMiles
             FROM TFlights AS F
             INNER JOIN TFlightAttendants AS FA
                    ON F.intFlightID = FA.intFlightID
             WHERE FA.intAttendantID = ?
               AND F.dtmDeparture < GETDATE()
             ORDER BY F.dtmDeparture DESC"

            Dim cmd As New OleDb.OleDbCommand(strSQL, m_conAdministrator)
            cmd.Parameters.AddWithValue("@AttID", SelectedAttendentID)

            Dim rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()

            Dim totalMiles As Integer = 0
            lstPastFlights.Items.Clear()

            While rdr.Read()
                Dim flightInfo As String =
                    "Flight #: " & rdr("strFlightNumber").ToString() & vbCrLf &
                    "From: " & rdr("strOrigin").ToString() & " → " &
                    rdr("strDestination").ToString() & vbCrLf &
                    "Date: " & CDate(rdr("dtmDeparture")).ToShortDateString() & vbCrLf &
                    "Miles: " & rdr("intMiles").ToString() & vbCrLf &
                    "-------------------------------------------"

                lstPastFlights.Items.Add(flightInfo)

                totalMiles += CInt(rdr("intMiles"))
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