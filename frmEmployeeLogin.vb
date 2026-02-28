Imports System.Data.OleDb

Public Class frmEmployeeLogin
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim loginID As String = txtEmployeeLogin.Text.Trim()
        Dim password As String = txtEmployeePassword.Text.Trim()

        If loginID = "" Or password = "" Then
            MessageBox.Show("Please enter Login ID and Password.")
            Exit Sub
        End If

        ' Open DB connection
        If OpenDatabaseConnectionSQLServer() = False Then
            MessageBox.Show("Database connection failed.")
            Exit Sub
        End If

        ' ============================================
        '   SQL QUERY TO VALIDATE EMPLOYEE LOGIN
        ' ============================================
        Dim strSQL As String =
            "SELECT intEmployeeID, strEmployeeRole, intEmployeePersonID
             FROM TEmployee
             WHERE strEmployeeLoginID = ?
               AND strEmployeePassword = ?"

        Dim cmd As New OleDbCommand(strSQL, m_conAdministrator)
        cmd.Parameters.AddWithValue("@p1", loginID)
        cmd.Parameters.AddWithValue("@p2", password)

        Dim rdr As OleDbDataReader = cmd.ExecuteReader()

        If rdr.Read() Then
            ' ============================
            ' Login successful
            ' ============================
            Dim employeeRole As String = rdr("strEmployeeRole").ToString()
            Dim employeeIDNumber As Integer = CInt(rdr("intEmployeePersonID"))
            Dim systemID As Integer = CInt(rdr("intEmployeePersonID"))

            rdr.Close()
            CloseDatabaseConnection()

            ' ================================================
            ' ROLE ROUTING
            ' ================================================

            If employeeRole = "Admin" Then
                MessageBox.Show("Login Successful!")

                ' Admin doesn't need a global ID
                Dim frmAdminMainMenu As New frmAdminMainMenu

                frmAdminMainMenu.ShowDialog()
                Exit Sub

            ElseIf employeeRole = "Pilot" Then
                MessageBox.Show("Login Successful!")


                ' Save GLOBAL PilotID
                SelectedPilotID = employeeIDNumber   'Foreign key to TPilots

                Dim frmPilotsMainMenu As New frmPilotsMainMenu

                frmPilotsMainMenu.ShowDialog()
                Exit Sub

            ElseIf employeeRole = "Attendant" Then
                MessageBox.Show("Login Successful!")


                ' Save GLOBAL AttendantID
                SelectedAttendentID = employeeIDNumber   'Foreign key to TAttendants

                Dim frmAttendentsMainMenu As New frmAttendentsMainMenu

                frmAttendentsMainMenu.ShowDialog()
                Exit Sub

            Else
                MessageBox.Show("Employee role is unknown or invalid.")
                Exit Sub
            End If

        Else
            ' ================================================
            ' NO MATCH FOUND
            ' ================================================
            rdr.Close()
            CloseDatabaseConnection()

            MessageBox.Show("ID and/or Password are not Valid")
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class