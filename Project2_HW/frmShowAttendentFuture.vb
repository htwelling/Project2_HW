Public Class frmShowAttendentFuture
    Private Sub btnShowFutureFlights_Click(sender As Object, e As EventArgs) Handles btnShowFutureFlights.Click
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
               AND F.dtmDeparture > GETDATE()
             ORDER BY F.dtmDeparture ASC"

            Dim cmd As New OleDb.OleDbCommand(strSQL, m_conAdministrator)
            cmd.Parameters.AddWithValue("@AttID", SelectedAttendentID)

            Dim rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()

            lstFutureFlights.Items.Clear()
            Dim totalMiles As Integer = 0

            While rdr.Read()
                Dim flightInfo As String =
                "Flight #: " & rdr("strFlightNumber").ToString() & vbCrLf &
                "From: " & rdr("strOrigin").ToString() & " → " &
                rdr("strDestination").ToString() & vbCrLf &
                "Departure: " &
                    CDate(rdr("dtmDeparture")).ToString("MM/dd/yyyy hh:mm tt") & vbCrLf &
                "Miles: " & rdr("intMiles").ToString() & vbCrLf &
                "-------------------------------------------"

                lstFutureFlights.Items.Add(flightInfo)

                totalMiles += CInt(rdr("intMiles"))
            End While

            rdr.Close()
            CloseDatabaseConnection()

            lblTotalMiles.Text = totalMiles.ToString()

        Catch ex As Exception
            MessageBox.Show("Error loading future flights: " & ex.Message)
        End Try

    End Sub
End Class