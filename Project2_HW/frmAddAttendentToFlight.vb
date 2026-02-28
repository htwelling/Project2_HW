Imports System.Data.OleDb

Public Class frmAddAttendentToFlight
    Private Sub frmAssignAttendantToFlight_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If OpenDatabaseConnectionSQLServer() = False Then
            MessageBox.Show("Database connection failed.")
            Exit Sub
        End If

        LoadAttendants()
        LoadFutureFlights()

        CloseDatabaseConnection()
    End Sub

    Private Sub LoadAttendants()

        cboAttendants.Items.Clear()

        Dim strSQL As String =
            "SELECT intAttendantID, strFirstName + ' ' + strLastName AS FullName " &
            "FROM TAttendants ORDER BY strLastName, strFirstName"

        Dim cmd As New OleDbCommand(strSQL, m_conAdministrator)
        Dim rdr As OleDbDataReader = cmd.ExecuteReader()

        While rdr.Read()
            cboAttendants.Items.Add(New With {
                .ID = rdr("intAttendantID"),
                .Name = rdr("FullName")
            })
        End While

        rdr.Close()
    End Sub
    Private Sub LoadFutureFlights()

        cboFlights.Items.Clear()

        Dim strSQL As String =
            "SELECT intFlightID, strFlightNumber, dtmDepartureDate " &
            "FROM TFlights " &
            "WHERE dtmDepartureDate > GETDATE() " &
            "ORDER BY dtmDepartureDate"

        Dim cmd As New OleDbCommand(strSQL, m_conAdministrator)
        Dim rdr As OleDbDataReader = cmd.ExecuteReader()

        While rdr.Read()
            cboFlights.Items.Add(New With {
                .ID = rdr("intFlightID"),
                .Name = rdr("strFlightNumber") & " - " &
                        CDate(rdr("dtmDepartureDate")).ToShortDateString()
            })
        End While

        rdr.Close()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If cboAttendants.SelectedItem Is Nothing Then
            MessageBox.Show("Please select an attendant.")
            Exit Sub
        End If

        If cboFlights.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a flight.")
            Exit Sub
        End If

        Dim selectedAttendant = cboAttendants.SelectedItem
        Dim selectedFlight = cboFlights.SelectedItem

        Dim attendantID As Integer = selectedAttendant.ID
        Dim flightID As Integer = selectedFlight.ID

        ' Confirm
        Dim response = MessageBox.Show(
            "Are you sure you want to assign this attendant to this flight?",
            "Confirm Assignment",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If response = DialogResult.No Then Exit Sub


        If OpenDatabaseConnectionSQLServer() = False Then
            MessageBox.Show("Database connection failed.")
            Exit Sub
        End If

        ' INSERT ASSIGNMENT INTO TAttendantFlights
        Dim strSQL As String =
            "INSERT INTO TAttendantFlights (intAttendantID, intFlightID) " &
            "VALUES (?, ?)"

        Dim cmd As New OleDbCommand(strSQL, m_conAdministrator)
        cmd.Parameters.AddWithValue("@p1", attendantID)
        cmd.Parameters.AddWithValue("@p2", flightID)

        Try
            cmd.ExecuteNonQuery()
            MessageBox.Show("Attendant successfully assigned to the flight!")
        Catch ex As Exception
            MessageBox.Show("Error assigning attendant: " & ex.Message)
        End Try

        CloseDatabaseConnection()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()

        Dim frmAttendentsMainMenu As New frmAttendentsMainMenu

        frmAttendentsMainMenu.ShowDialog()
    End Sub
End Class