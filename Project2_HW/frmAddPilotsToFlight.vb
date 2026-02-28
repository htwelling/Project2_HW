Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class frmAddPilotsToFlight
    Private Sub frmAssignPilotToFlight_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadPilots()
        LoadFutureFlights()
    End Sub
    Private Sub LoadPilots()
        Dim strSQL As String =
            "SELECT intPilotID, strFirstName + ' ' + strLastName AS FullName " &
            "FROM TPilots ORDER BY strLastName, strFirstName"

        Dim cmd As New OleDbCommand(strSQL, m_conAdministrator)
        Dim rdr As OleDbDataReader = cmd.ExecuteReader()

        While rdr.Read()
            cboPilots.Items.Add(New With {
                .ID = rdr("intPilotID"),
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
                .Desc = rdr("strFlightNumber") & " - " &
                        CDate(rdr("dtmDepartureDate")).ToShortDateString()
            })
        End While

        rdr.Close()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If cboPilots.Text = "" Then
            MessageBox.Show("Please select a Pilot.")
            Return
        End If

        If cboFlights.Text = "" Then
            MessageBox.Show("Please select a Flight.")
            Return
        End If

        Dim selectedPilot = cboPilots.SelectedItem
        Dim selectedFlight = cboFlights.SelectedItem

        Dim pilotID As Integer = selectedPilot.ID
        Dim flightID As Integer = selectedFlight.ID

        Dim response = MessageBox.Show("Are you sure you want to assign this pilot to this flight?",
                                      "Confirm Assignment",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question)

        If response = DialogResult.No Then Exit Sub

        '------------------------------------------------------------
        ' INSERT ASSIGNMENT INTO DATABASE
        '------------------------------------------------------------
        Dim strSQL As String =
            "INSERT INTO TPilotFlights (intPilotID, intFlightID) VALUES (?, ?)"

        Dim cmd As New OleDbCommand(strSQL, m_conAdministrator)
        cmd.Parameters.AddWithValue("@p1", pilotID)
        cmd.Parameters.AddWithValue("@p2", flightID)

        Try
            cmd.ExecuteNonQuery()
            MessageBox.Show("Pilot successfully assigned to the flight!")
        Catch ex As Exception
            MessageBox.Show("Error assigning pilot: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()

        Dim frmPilotsMainMenu As New frmPilotsMainMenu

        frmPilotsMainMenu.ShowDialog()
    End Sub
End Class