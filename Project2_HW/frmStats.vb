Imports System.Data.OleDb

Public Class frmStats
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            LoadCustomerStats()
            LoadPilotMiles()
            LoadAttendantMiles()
            MessageBox.Show("Statistics Loaded Successfully.")
        Catch ex As Exception
            MessageBox.Show("Error loading statistics: " & ex.Message)
        End Try
    End Sub
    Private Sub LoadCustomerStats()
        Dim cmd As New OleDbCommand()
        cmd.Connection = m_conAdministrator

        ' Total number of customers
        cmd.CommandText = "SELECT COUNT(*) FROM TCustomers"
        Dim totalCustomers As Integer = CInt(cmd.ExecuteScalar())
        lblTotalCustomers.Text = "Total Customers: " & totalCustomers

        ' Total flights taken by customers
        cmd.CommandText = "SELECT COUNT(*) FROM TCustomerFlights"
        Dim customerFlights As Integer = CInt(cmd.ExecuteScalar())
        lblTotalFlightsTaken.Text = "Total Flights Taken: " & customerFlights

        ' Average miles flown
        cmd.CommandText =
            "SELECT AVG(TFlights.intMiles) " &
            "FROM TFlights INNER JOIN TCustomerFlights " &
            "ON TFlights.intFlightID = TCustomerFlights.intFlightID"

        Dim avgMilesObj As Object = cmd.ExecuteScalar()
        Dim avgMiles As Integer = If(IsDBNull(avgMilesObj), 0, CInt(avgMilesObj))

        lblAverageMilesFlown.Text = "Average Miles Flown: " & avgMiles
    End Sub
    Private Sub LoadPilotMiles()
        lstPilotsMiles.Items.Clear()

        Dim cmd As New OleDbCommand()
        cmd.Connection = m_conAdministrator

        cmd.CommandText =
            "SELECT P.strFirstName & ' ' & P.strLastName AS PilotName, " &
            "       SUM(F.intMiles) AS TotalMiles " &
            "FROM TPilots AS P " &
            "LEFT JOIN TPilotFlights AS PF ON P.intPilotID = PF.intPilotID " &
            "LEFT JOIN TFlights AS F ON PF.intFlightID = F.intFlightID " &
            "GROUP BY P.strFirstName, P.strLastName " &
            "ORDER BY PilotName"

        Dim reader As OleDbDataReader = cmd.ExecuteReader()

        While reader.Read()
            Dim name As String = reader("PilotName").ToString()
            Dim miles As Integer = If(IsDBNull(reader("TotalMiles")), 0, CInt(reader("TotalMiles")))

            lstPilotsMiles.Items.Add(name & " — Miles: " & miles)
        End While

        reader.Close()
    End Sub
    Private Sub LoadAttendantMiles()
        lstAttendantMiles.Items.Clear()

        Dim cmd As New OleDbCommand()
        cmd.Connection = m_conAdministrator

        cmd.CommandText =
            "SELECT A.strFirstName & ' ' & A.strLastName AS AttendantName, " &
            "       SUM(F.intMiles) AS TotalMiles " &
            "FROM TAttendants AS A " &
            "LEFT JOIN TAttendantFlights AS AF ON A.intAttendantID = AF.intAttendantID " &
            "LEFT JOIN TFlights AS F ON AF.intFlightID = F.intFlightID " &
            "GROUP BY A.strFirstName, A.strLastName " &
            "ORDER BY AttendantName"

        Dim reader As OleDbDataReader = cmd.ExecuteReader()

        While reader.Read()
            Dim name As String = reader("AttendantName").ToString()
            Dim miles As Integer = If(IsDBNull(reader("TotalMiles")), 0, CInt(reader("TotalMiles")))

            lstAttendantMiles.Items.Add(name & " — Miles: " & miles)
        End While

        reader.Close()
    End Sub
End Class