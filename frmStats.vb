Imports System.Data.OleDb

Public Class frmStats
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            ' Open database connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If

            ' ===============================
            '   Total Number of Customers
            ' ===============================
            Dim cmdTotalCustomers As New OleDbCommand("SELECT COUNT(*) FROM TPassengers", m_conAdministrator)
            lblTotalCustomers.Text = cmdTotalCustomers.ExecuteScalar().ToString()

            ' ===============================
            '   Total Flights Taken by All Customers
            ' ===============================
            Dim cmdTotalFlights As New OleDbCommand("SELECT COUNT(*) FROM TFlightPassengers", m_conAdministrator)
            lblTotalFlightsTaken.Text = cmdTotalFlights.ExecuteScalar().ToString()

            ' ===============================
            '   Average Miles Flown for All Customers
            ' ===============================
            Dim cmdAverageMiles As New OleDbCommand("
                SELECT AVG(F.intMilesFlown) 
                FROM TFlights AS F
                INNER JOIN TFlightPassengers AS FP ON F.intFlightID = FP.intFlightID", m_conAdministrator)

            Dim avgMilesObj = cmdAverageMiles.ExecuteScalar()
            lblAverageMiles.Text = If(IsDBNull(avgMilesObj), "0", CInt(avgMilesObj).ToString())

            ' ===============================
            '   Pilot Total Miles
            ' ===============================
            lstPilotsMiles.Items.Clear()
            Dim cmdPilotMiles As New OleDbCommand("
                SELECT P.strLastName + ', ' + P.strFirstName AS PilotName,
                       ISNULL(SUM(F.intMilesFlown), 0) AS TotalMiles
                FROM TPilots AS P
                LEFT JOIN TPilotFlights AS PF ON P.intPilotID = PF.intPilotID
                LEFT JOIN TFlights AS F ON PF.intFlightID = F.intFlightID
                GROUP BY P.strFirstName, P.strLastName
                ORDER BY P.strLastName", m_conAdministrator)

            Dim rdrPilot As OleDbDataReader = cmdPilotMiles.ExecuteReader()
            lstPilotsMiles.Items.Add("All pilots and their miles flown")
            lstPilotsMiles.Items.Add("================================")
            While rdrPilot.Read()
                lstPilotsMiles.Items.Add($"{rdrPilot("PilotName")} - Total Miles: {rdrPilot("TotalMiles")}")
            End While
            rdrPilot.Close()

            ' ===============================
            '   Attendant Total Miles
            ' ===============================
            lstAttendantMiles.Items.Clear()
            Dim cmdAttendantMiles As New OleDbCommand("
                SELECT A.strLastName + ', ' + A.strFirstName AS AttendantName,
                       ISNULL(SUM(F.intMilesFlown), 0) AS TotalMiles
                FROM TAttendants AS A
                LEFT JOIN TAttendantFlights AS AF ON A.intAttendantID = AF.intAttendantID
                LEFT JOIN TFlights AS F ON AF.intFlightID = F.intFlightID
                GROUP BY A.strFirstName, A.strLastName
                ORDER BY A.strLastName", m_conAdministrator)

            Dim rdrAttendant As OleDbDataReader = cmdAttendantMiles.ExecuteReader()
            lstAttendantMiles.Items.Add("All attendants and their miles flown")
            lstAttendantMiles.Items.Add("====================================")
            While rdrAttendant.Read()
                lstAttendantMiles.Items.Add($"{rdrAttendant("AttendantName")} - Total Miles: {rdrAttendant("TotalMiles")}")
            End While
            rdrAttendant.Close()

            ' Close connection
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show("Error loading statistics: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class