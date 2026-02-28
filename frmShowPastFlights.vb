Public Class frmShowPastFlights
    Private Sub btnShowPastFlights_Click(sender As Object, e As EventArgs) Handles btnShowPastFlights.Click
        Try
            lstPastFlights.Items.Clear()
            lblTotalMiles.Text = ""

            ' Open connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            Dim strSelect As String =
            "SELECT F.strFlightNumber, F.dtmFlightDate, F.intMilesFlown, FP.strSeat " &
            "FROM TFlightPassengers AS FP " &
            "INNER JOIN TFlights AS F ON FP.intFlightID = F.intFlightID " &
            "WHERE FP.intPassengerID = ? " &
            "AND F.dtmFlightDate < GETDATE() " &
            "ORDER BY F.dtmFlightDate DESC;"

            Dim cmdSelect As New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            ' OleDb uses positional parameters: add in same order as the ? placeholders
            cmdSelect.Parameters.AddWithValue("@p1", SelectedCustomerID)

            Dim dr As OleDb.OleDbDataReader = cmdSelect.ExecuteReader()

            Dim intTotalMiles As Integer = 0

            While dr.Read()
                Dim miles As Integer = If(IsDBNull(dr("intMilesFlown")), 0, CInt(dr("intMilesFlown")))

                Dim flightInfo As String =
                "Flight: " & dr("strFlightNumber").ToString() & vbCrLf &
                "Date: " & CDate(dr("dtmFlightDate")).ToShortDateString() & vbCrLf &
                "Seat: " & dr("strSeat").ToString() & vbCrLf &
                "Miles: " & miles.ToString() & vbCrLf &
                "------------------------------"

                lstPastFlights.Items.Add(flightInfo)
                intTotalMiles += miles
            End While

            dr.Close()
            CloseDatabaseConnection()

            lblTotalMiles.Text = intTotalMiles.ToString()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class