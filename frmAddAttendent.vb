Imports System.Data.OleDb

Public Class frmAddAttendent
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If ValidateData() = False Then Exit Sub

        Dim strFirst As String = txtFirstName.Text.Trim()
        Dim strLast As String = txtLastName.Text.Trim()
        Dim strEmployeeID As String = txtEmployeeID.Text.Trim()
        Dim strLogin As String = txtAttendantID.Text.Trim()
        Dim strPassword As String = txtAttendantPassword.Text.Trim()

        Dim dtHire As Date = dtmDateOfHire.Value
        Dim dtTermination As Date = dtmDateOfTerm.Value

        If OpenDatabaseConnectionSQLServer() = False Then
            MessageBox.Show("Database connection failed.")
            Exit Sub
        End If

        '----------------------------------------------------------
        ' GET NEXT PRIMARY KEY FOR ATTENDANT
        '----------------------------------------------------------
        Dim intNextID As Integer
        Dim strPKSQL As String = "SELECT ISNULL(MAX(intAttendantID),0) + 1 FROM TAttendants"
        Dim cmdPK As New OleDbCommand(strPKSQL, m_conAdministrator)
        intNextID = CInt(cmdPK.ExecuteScalar())

        '----------------------------------------------------------
        ' CALL STORED PROCEDURE usp_AddAttendant
        '----------------------------------------------------------
        Try
            Dim cmd As New OleDbCommand("usp_AddAttendant", m_conAdministrator)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@AttendantID", intNextID)
            cmd.Parameters.AddWithValue("@FirstName", strFirst)
            cmd.Parameters.AddWithValue("@LastName", strLast)
            cmd.Parameters.AddWithValue("@EmployeeID", strEmployeeID)
            cmd.Parameters.AddWithValue("@DateOfHire", dtHire)
            cmd.Parameters.AddWithValue("@DateOfTermination", dtTermination)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Error adding attendant: " & ex.Message)
            CloseDatabaseConnection()
            Exit Sub
        End Try

        '----------------------------------------------------------
        ' INSERT INTO TEmployee (NO STORED PROCEDURE)
        '----------------------------------------------------------
        Try
            Dim strInsertEmp As String =
                "INSERT INTO TEmployee (intEmployeeID, strEmployeeLoginID, strEmployeePassword, strEmployeeRole, intEmployeePersonID) " &
                "VALUES (?, ?, ?, ?, ?);"

            Dim cmdEmp As New OleDbCommand(strInsertEmp, m_conAdministrator)

            cmdEmp.Parameters.AddWithValue("@EmpID", strEmployeeID)
            cmdEmp.Parameters.AddWithValue("@LoginID", strLogin)
            cmdEmp.Parameters.AddWithValue("@Password", strPassword)
            cmdEmp.Parameters.AddWithValue("@Role", "Attendant")
            cmdEmp.Parameters.AddWithValue("@PersonID", intNextID)

            cmdEmp.ExecuteNonQuery()

            MessageBox.Show("Attendant and Employee login successfully added!")

        Catch ex As Exception
            MessageBox.Show("Error inserting into Employee table: " & ex.Message)
        Finally
            CloseDatabaseConnection()
        End Try
    End Sub

    '------------------------------------------------------------
    ' VALIDATE USER INPUT
    '------------------------------------------------------------
    Private Function ValidateData() As Boolean
        If txtFirstName.Text.Trim() = "" Then
            MessageBox.Show("First Name is required.")
            Return False
        End If

        If txtLastName.Text.Trim() = "" Then
            MessageBox.Show("Last Name is required.")
            Return False
        End If

        If txtEmployeeID.Text.Trim() = "" Then
            MessageBox.Show("Employee ID is required.")
            Return False
        End If

        If dtmDateOfTerm.Value < dtmDateOfHire.Value Then
            MessageBox.Show("Termination date cannot be earlier than hire date.")
            Return False
        End If

        If txtAttendantID.Text = "" Then
            MessageBox.Show("Must add ID to sign in later")
            Return False
        Else
            If txtAttendantPassword.Text = "" Then
                MessageBox.Show("Must add password to sign in later")
                Return False
            End If
        End If

        Return True
    End Function

    '------------------------------------------------------------
    ' EXIT BUTTON
    '------------------------------------------------------------
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
        Dim frmAttendentsMainMenu As New frmAttendentsMainMenu
        frmAttendentsMainMenu.ShowDialog()
    End Sub
End Class