Imports System.Data.OleDb

Public Class frmUpdatePilotInfo
    Private Sub frmUpdatePilotProfile_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If SelectedPilotID <= 0 Then
            MessageBox.Show("No pilot selected.")
            Me.Close()
            Exit Sub
        End If

        If OpenDatabaseConnectionSQLServer() = False Then
            MessageBox.Show("Database connection error.")
            Me.Close()
            Exit Sub
        End If

        LoadPilotRoles()
        LoadPilotInfo()
        LoadEmployeeLoginInfo()

        CloseDatabaseConnection()

    End Sub


    ' -------------------------------------------------------------
    ' LOAD PILOT DATA
    ' -------------------------------------------------------------
    Private Sub LoadPilotInfo()

        Dim sql As String =
            "SELECT strFirstName, strLastName, strEmployeeID,
                    dtmDateOfHire, dtmDateOfTermination, dtmDateOfLicense,
                    intPilotRoleID
             FROM TPilots
             WHERE intPilotID = ?"

        Dim cmd As New OleDbCommand(sql, m_conAdministrator)
        cmd.Parameters.AddWithValue("?", SelectedPilotID)

        Dim reader As OleDbDataReader = cmd.ExecuteReader()

        If reader.Read() Then

            txtFirstName.Text = reader("strFirstName").ToString()
            txtLastName.Text = reader("strLastName").ToString()
            txtEmployeeID.Text = reader("strEmployeeID").ToString()

            If Not IsDBNull(reader("dtmDateOfHire")) Then
                dtpHire.Value = CDate(reader("dtmDateOfHire"))
            End If

            If Not IsDBNull(reader("dtmDateOfTermination")) Then
                dtpDateOfTerm.Value = CDate(reader("dtmDateOfTermination"))
            End If

            If Not IsDBNull(reader("dtmDateOfLicense")) Then
                dtpLicnese.Value = CDate(reader("dtmDateOfLicense"))
            End If

            If Not IsDBNull(reader("intPilotRoleID")) Then
                cboPilotRole.SelectedValue = CInt(reader("intPilotRoleID"))
            End If

        Else
            MessageBox.Show("Pilot not found.")
            Me.Close()
        End If

        reader.Close()

    End Sub


    ' -------------------------------------------------------------
    ' LOAD EMPLOYEE LOGIN/PASSWORD FOR THIS PILOT
    ' -------------------------------------------------------------
    Private Sub LoadEmployeeLoginInfo()

        Dim sql As String =
            "SELECT strEmployeeLoginID, strEmployeePassword 
             FROM TEmployee
             WHERE intEmployeePersonID = ?
               AND strEmployeeRole = 'Pilot'"

        Dim cmd As New OleDbCommand(sql, m_conAdministrator)
        cmd.Parameters.AddWithValue("?", SelectedPilotID)

        Dim rdr As OleDbDataReader = cmd.ExecuteReader()

        If rdr.Read() Then
            txtPilotID.Text = rdr("strEmployeeLoginID").ToString()
            txtPilotPassword.Text = rdr("strEmployeePassword").ToString()
        End If

        rdr.Close()

    End Sub


    ' -------------------------------------------------------------
    ' LOAD ROLES
    ' -------------------------------------------------------------
    Private Sub LoadPilotRoles()

        Dim sql As String = "SELECT intPilotRoleID, strPilotRole FROM TPilotRoles"

        Dim cmd As New OleDbCommand(sql, m_conAdministrator)
        Dim da As New OleDbDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)

        cboPilotRole.DataSource = dt
        cboPilotRole.DisplayMember = "strPilotRole"
        cboPilotRole.ValueMember = "intPilotRoleID"

    End Sub


    ' -------------------------------------------------------------
    ' UPDATE BUTTON
    ' -------------------------------------------------------------
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        If OpenDatabaseConnectionSQLServer() = False Then
            MessageBox.Show("Database connection error.")
            Exit Sub
        End If

        Try
            ' -----------------------------------------------------
            ' FIRST: UPDATE PILOT TABLE
            ' -----------------------------------------------------
            Dim sqlPilot As String =
                "UPDATE TPilots SET
                    strFirstName = ?,
                    strLastName = ?,
                    strEmployeeID = ?,
                    dtmDateOfHire = ?,
                    dtmDateOfTermination = ?,
                    dtmDateOfLicense = ?,
                    intPilotRoleID = ?
                 WHERE intPilotID = ?"

            Dim cmdPilot As New OleDbCommand(sqlPilot, m_conAdministrator)

            cmdPilot.Parameters.AddWithValue("?", txtFirstName.Text.Trim())
            cmdPilot.Parameters.AddWithValue("?", txtLastName.Text.Trim())
            cmdPilot.Parameters.AddWithValue("?", txtEmployeeID.Text.Trim())
            cmdPilot.Parameters.AddWithValue("?", dtpHire.Value)
            cmdPilot.Parameters.AddWithValue("?", dtpDateOfTerm.Value)
            cmdPilot.Parameters.AddWithValue("?", dtpLicnese.Value)
            cmdPilot.Parameters.AddWithValue("?", CInt(cboPilotRole.SelectedValue))
            cmdPilot.Parameters.AddWithValue("?", SelectedPilotID)

            cmdPilot.ExecuteNonQuery()


            ' -----------------------------------------------------
            ' SECOND: UPDATE EMPLOYEE LOGIN TABLE
            ' -----------------------------------------------------
            Dim sqlEmp As String =
                "UPDATE TEmployee SET
                    strEmployeeLoginID = ?,
                    strEmployeePassword = ?
                 WHERE intEmployeePersonID = ?
                   AND strEmployeeRole = 'Pilot'"

            Dim cmdEmp As New OleDbCommand(sqlEmp, m_conAdministrator)

            cmdEmp.Parameters.AddWithValue("?", txtPilotID.Text.Trim())
            cmdEmp.Parameters.AddWithValue("?", txtPilotPassword.Text.Trim())
            cmdEmp.Parameters.AddWithValue("?", SelectedPilotID)

            cmdEmp.ExecuteNonQuery()


            MessageBox.Show("Pilot profile updated successfully!")

        Catch ex As Exception
            MessageBox.Show("Error updating pilot: " & ex.Message)
        End Try

        CloseDatabaseConnection()

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class