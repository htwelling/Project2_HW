Public Class frmShowAttendentFuture
    Private Sub btnShowFutureFlights_Click(sender As Object, e As EventArgs) Handles btnShowFutureFlights.Click
        Try
            ' Open the DB connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            ' ===============================
            '   CALL STORED PROCEDURE
            ' ===============================
            Dim cmd As New OleDb.OleDbCommand("usp_GetFutureFlightsByAttendant", m_conAdministrator)
            cmd.CommandType = CommandType.StoredProcedure

            ' Add parameter (OleDb uses positional order)
            cmd.Parameters.AddWithValue("@AttendantID", SelectedAttendentID)

            Dim rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()

            lstFutureFlights.Items.Clear()
            Dim totalMiles As Integer = 0

            ' ===============================
            '   READ RESULTS
            ' ===============================
            While rdr.Read()
                Dim flightInfo As String =
                    "Flight #: " & rdr("strFlightNumber").ToString() & vbCrLf &
                    "Departure: " &
                        CDate(rdr("dtmTimeOfDeparture")).ToString("MM/dd/yyyy hh:mm tt") & vbCrLf &
                    "Miles: " & rdr("intMilesFlown").ToString() & vbCrLf &
                    "-------------------------------------------"

                lstFutureFlights.Items.Add(flightInfo)

                totalMiles += CInt(rdr("intMilesFlown"))
            End While

            rdr.Close()
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