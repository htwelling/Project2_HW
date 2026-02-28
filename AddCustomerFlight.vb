Imports System.Data.OleDb


Public Class AddCustomerFlight
    Private rand As New Random()


    Private FlightBaseCost As Decimal = 250D
    Private ReservedSeatAdd As Decimal = 125D

    Private Sub AddCustomerFlight_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Hide all seat-related options
            grpSeatType.Visible = False
            radReservedSeat.Visible = False
            radCheckInSeat.Visible = False
            radReservedSeat.Checked = False
            radCheckInSeat.Checked = False

            cboFlights.Items.Clear()

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            ' Load future flights
            Dim dt As New DataTable
            Dim sql As String =
                "SELECT intFlightID,
                        strFlightNumber + ' - ' +
                        CONVERT(VARCHAR, dtmFlightDate, 101) AS DisplayFlight
                 FROM TFlights
                 WHERE dtmFlightDate > GETDATE()
                 ORDER BY dtmFlightDate"

            Using cmd As New OleDbCommand(sql, m_conAdministrator)
                dt.Load(cmd.ExecuteReader())
            End Using

            cboFlights.DisplayMember = "DisplayFlight"
            cboFlights.ValueMember = "intFlightID"
            cboFlights.DataSource = dt

            cboFlights.SelectedIndex = -1
            cboFlights.Text = ""

            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show("Load Error: " & ex.Message)
        End Try
    End Sub



    '====================================================================
    ' When flight is selected → reveal seat group + calculate costs
    '====================================================================
    Private Sub cboFlights_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFlights.SelectedIndexChanged
        If cboFlights.SelectedIndex = -1 Then
            grpSeatType.Visible = False
            Exit Sub
        End If

        CalculateAndDisplayCosts()
    End Sub



    '====================================================================
    ' COST CALCULATION
    '====================================================================
    Private Sub CalculateAndDisplayCosts()
        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If

            Dim flightID As Integer = CInt(cboFlights.SelectedValue)

            '----------------------------------------------------------
            ' GET FLIGHT INFO
            '----------------------------------------------------------
            Dim sqlFlight As String =
    "SELECT F.intMilesFlown,
            PT.strPlaneType,
            A.strAirportCode
     FROM TFlights F
     INNER JOIN TPlanes P ON F.intPlaneID = P.intPlaneID
     INNER JOIN TPlaneTypes PT ON P.intPlaneTypeID = PT.intPlaneTypeID
     INNER JOIN TAirports A ON F.intToAirportID = A.intAirportID
     WHERE F.intFlightID = ?"

            Dim totalMiles As Integer = 0
            Dim planeType As String = ""
            Dim arrivalAirport As String = ""

            Using cmd As New OleDbCommand(sqlFlight, m_conAdministrator)
                cmd.Parameters.AddWithValue("?", flightID)

                Using rdr = cmd.ExecuteReader()
                    If rdr.Read() Then
                        totalMiles = CInt(rdr("intMilesFlown"))
                        planeType = rdr("strPlaneType").ToString()
                        arrivalAirport = rdr("strAirportCode").ToString()
                    End If
                End Using
            End Using


            '----------------------------------------------------------
            ' HOW MANY PASSENGERS ON THIS FLIGHT?
            '----------------------------------------------------------
            Dim passengerCount As Integer
            Dim sqlCount As String =
                "SELECT COUNT(*) FROM TFlightPassengers WHERE intFlightID = ?"

            Using cmd As New OleDbCommand(sqlCount, m_conAdministrator)
                cmd.Parameters.AddWithValue("?", flightID)
                passengerCount = CInt(cmd.ExecuteScalar())
            End Using


            '----------------------------------------------------------
            ' AGE OF SELECTED PASSENGER
            '----------------------------------------------------------
            Dim passengerAge As Integer
            Dim sqlDOB As String =
                "SELECT DATEDIFF(YEAR, dtmDateOfBirth, GETDATE())
                 FROM TPassengers
                 WHERE intPassengerID = ?"

            Using cmd As New OleDbCommand(sqlDOB, m_conAdministrator)
                cmd.Parameters.AddWithValue("?", SelectedCustomerID)
                passengerAge = CInt(cmd.ExecuteScalar())
            End Using


            '----------------------------------------------------------
            ' HOW MANY FLIGHTS THIS CUSTOMER HAS ALREADY TAKEN?
            '----------------------------------------------------------
            Dim previousFlights As Integer
            Dim sqlPrev As String =
                "SELECT COUNT(*)
                 FROM TFlightPassengers FP
                 INNER JOIN TFlights F ON FP.intFlightID = F.intFlightID
                 WHERE FP.intPassengerID = ?
                   AND F.dtmFlightDate < GETDATE()"

            Using cmd As New OleDbCommand(sqlPrev, m_conAdministrator)
                cmd.Parameters.AddWithValue("?", SelectedCustomerID)
                previousFlights = CInt(cmd.ExecuteScalar())
            End Using


            CloseDatabaseConnection()



            '==========================================================
            ' APPLY COST RULES
            '==========================================================
            Dim baseCost As Decimal = FlightBaseCost

            ' Mileage rule
            If totalMiles > 750 Then baseCost += 50D

            ' Passenger seat-count rules
            If passengerCount > 8 Then baseCost += 100D
            If passengerCount < 4 Then baseCost -= 50D

            ' Plane type rules
            If planeType = "Airbus A350" Then baseCost += 35D
            If planeType = "Boeing 747-8" Then baseCost -= 25D

            ' Destination rule
            If arrivalAirport = "MIA" Then baseCost += 15D

            ' Reserved seat version
            Dim reservedCost As Decimal = baseCost + ReservedSeatAdd

            ' Age discounts
            If passengerAge >= 65 Then
                baseCost *= 0.8D
                reservedCost *= 0.8D
            ElseIf passengerAge <= 5 Then
                baseCost *= 0.35D
                reservedCost *= 0.35D
            End If

            ' Frequent flyer discounts
            If previousFlights > 10 Then
                baseCost *= 0.8D
                reservedCost *= 0.8D
            ElseIf previousFlights > 5 Then
                baseCost *= 0.9D
                reservedCost *= 0.9D
            End If



            '==========================================================
            ' DISPLAY RESULTS
            '==========================================================
            grpSeatType.Visible = True

            radReservedSeat.Visible = True
            radReservedSeat.Text = "Reserved Seat - $" & reservedCost.ToString("F2")
            radReservedSeat.Tag = reservedCost

            radCheckInSeat.Visible = True
            radCheckInSeat.Text = "Designated Seat at Check-In - $" & baseCost.ToString("F2")
            radCheckInSeat.Tag = baseCost

        Catch ex As Exception
            MessageBox.Show("Cost Calculation Error: " & ex.Message)
        End Try
    End Sub



    '====================================================================
    ' INSERT BOOKING
    '====================================================================
    Private Sub btnAddFlight_Click(sender As Object, e As EventArgs) Handles btnAddFlight.Click

        Try
            If cboFlights.SelectedIndex = -1 Then
                MessageBox.Show("Please select a flight first.")
                Exit Sub
            End If

            If radReservedSeat.Checked = False And radCheckInSeat.Checked = False Then
                MessageBox.Show("Please select a seat option.")
                Exit Sub
            End If

            Dim cost As Decimal =
                If(radReservedSeat.Checked,
                   CDec(radReservedSeat.Tag),
                   CDec(radCheckInSeat.Tag))

            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If

            ' ----------------------------------------------------
            ' GET NEXT PilotFlights ID
            ' ----------------------------------------------------
            Dim strSelectFlightPassengers As String =
    "SELECT MAX(intFlightPassengerID) + 1 AS NextID FROM TFlightPassengers;"

            Dim cmdSelectFlightPassengers As New OleDb.OleDbCommand(strSelectFlightPassengers, m_conAdministrator)
            Dim rdr As OleDb.OleDbDataReader = cmdSelectFlightPassengers.ExecuteReader()

            Dim nextFlightPassengersID As Integer = 1
            If rdr.Read() AndAlso Not rdr.IsDBNull(0) Then
                nextFlightPassengersID = CInt(rdr("NextID"))
            End If

            rdr.Close()

            Dim sqlInsert As String =
                "INSERT INTO TFlightPassengers
                 (intFlightPassengerID, intFlightID, intPassengerID, strSeat, decFlightCost)
                 VALUES (?, ?, ?, ?, ?)"

            Using cmd As New OleDbCommand(sqlInsert, m_conAdministrator)
                cmd.Parameters.AddWithValue("?", nextFlightPassengersID)
                cmd.Parameters.AddWithValue("?", CInt(cboFlights.SelectedValue))
                cmd.Parameters.AddWithValue("?", SelectedCustomerID)
                cmd.Parameters.AddWithValue("?", txtSeat.Text)
                cmd.Parameters.AddWithValue("?", cost)

                If cmd.ExecuteNonQuery() = 1 Then
                    MessageBox.Show("Flight successfully booked!")
                Else
                    MessageBox.Show("Error: Booking failed.")
                End If
            End Using

            CloseDatabaseConnection()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Submit Error: " & ex.Message)
        End Try

    End Sub

    Private Sub radSeatCheckIn_CheckedChanged(sender As Object, e As EventArgs) Handles radCheckInSeat.CheckedChanged
        If radCheckInSeat.Checked Then

            ' Make sure flight is selected first
            If cboFlights.SelectedIndex = -1 Then
                MessageBox.Show("Please select a flight before assigning a seat.")
                radCheckInSeat.Checked = False
                Exit Sub
            End If

            ' Auto-generate a seat number
            txtSeat.Visible = False
            Label2.Visible = False

            Dim seatNumber As Integer = rand.Next(1, 31)   ' Seats 1–30
            Dim seatLetter As String = "ABCDEF"(rand.Next(0, 6)) ' Random letter A–F

            txtSeat.Text = seatNumber.ToString() & seatLetter
            txtSeat.ReadOnly = True
        Else
            If radReservedSeat.Checked Then
                txtSeat.Visible = True
                Label2.Visible = True
                txtSeat.Clear()
            End If
        End If
    End Sub


    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

End Class
