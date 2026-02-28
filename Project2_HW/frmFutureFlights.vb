Public Class frmFutureFlights
    Private Sub btnShowFutureFlights_Click(sender As Object, e As EventArgs) Handles btnShowFutureFlights.Click
        Try
            Dim strSelect As String
            Dim cmdSelect As OleDb.OleDbCommand
            Dim dr As OleDb.OleDbDataReader
            Dim intTotalMiles As Integer = 0

            lstFutureFlights.Items.Clear()

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
            End If

            'Select FUTURE flights for selected customer
            strSelect =
                "SELECT F.strFlightInfo, F.dtFlightDate, F.intMiles, CF.strSeatNumber " &
                "FROM TCustomerFlights AS CF " &
                "INNER JOIN TFlights AS F ON CF.intFlightID = F.intFlightID " &
                "WHERE CF.intCustomerID = " & SelectedCustomerID & " " &
                "AND F.dtFlightDate >= GETDATE() " &
                "ORDER BY F.dtFlightDate ASC"

            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            dr = cmdSelect.ExecuteReader

            'Load all future flights into list box
            While dr.Read()
                Dim flightInfo As String =
                    "Flight: " & dr("strFlightInfo") & vbCrLf &
                    "Date: " & CDate(dr("dtFlightDate")).ToShortDateString() & vbCrLf &
                    "Seat: " & dr("strSeatNumber") & vbCrLf &
                    "Miles: " & dr("intMiles") & vbCrLf &
                    "------------------------------"

                lstFutureFlights.Items.Add(flightInfo)

                'Add up miles
                intTotalMiles += CInt(dr("intMiles"))
            End While

            dr.Close()
            CloseDatabaseConnection()

            'Show total future miles
            lblTotalMiles.Text = "Total Future Miles: " & intTotalMiles.ToString()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class