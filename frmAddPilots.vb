Public Class frmAddPilots
    Private Sub frmAddPilot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            Dim strSQL As String =
            "SELECT intPilotRoleID, strPilotRole " &
            "FROM TPilotRoles ORDER BY strPilotRole;"

            Dim cmd As New OleDb.OleDbCommand(strSQL, m_conAdministrator)
            Dim rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()
            Dim dt As New DataTable()
            dt.Load(rdr)

            cboRoles.DisplayMember = "strPilotRole"
            cboRoles.ValueMember = "intPilotRoleID"
            cboRoles.DataSource = dt

            rdr.Close()
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show("Error loading roles: " & ex.Message)
        End Try

    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ValidatePilotData()

        If ValidatePilotData() = False Then
            Exit Sub
        End If

        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If

            ' ----------------------------------------------------
            ' GET NEXT PILOT ID
            ' ----------------------------------------------------
            Dim strSelectPilot As String =
                "SELECT MAX(intPilotID) + 1 AS NextID FROM TPilots;"

            Dim cmdSelectPilot As New OleDb.OleDbCommand(strSelectPilot, m_conAdministrator)
            Dim rdr As OleDb.OleDbDataReader = cmdSelectPilot.ExecuteReader()

            Dim nextPilotID As Integer = 1
            If rdr.Read() AndAlso Not rdr.IsDBNull(0) Then
                nextPilotID = CInt(rdr("NextID"))
            End If

            rdr.Close()


            ' ----------------------------------------------------
            ' GET NEXT EMPLOYEE ID
            ' ----------------------------------------------------
            Dim strSelectEmp As String =
                "SELECT MAX(intEmployeeID) + 1 AS NextEmpID FROM TEmployee;"

            Dim cmdSelectEmp As New OleDb.OleDbCommand(strSelectEmp, m_conAdministrator)
            Dim rdrEmp As OleDb.OleDbDataReader = cmdSelectEmp.ExecuteReader()

            Dim nextEmployeeID As Integer = 1
            If rdrEmp.Read() AndAlso Not rdrEmp.IsDBNull(0) Then
                nextEmployeeID = CInt(rdrEmp("NextEmpID"))
            End If

            rdrEmp.Close()


            ' ----------------------------------------------------
            ' INSERT PILOT
            ' ----------------------------------------------------
            Dim strInsertPilot As String =
                "INSERT INTO TPilots 
                (intPilotID, strFirstName, strLastName, strEmployeeID, 
                 dtmDateOfHire, dtmDateOfTermination, dtmDateOfLicense, intPilotRoleID)
                 VALUES (?, ?, ?, ?, ?, ?, ?, ?);"

            Dim cmdInsertPilot As New OleDb.OleDbCommand(strInsertPilot, m_conAdministrator)

            cmdInsertPilot.Parameters.AddWithValue("@ID", nextPilotID)
            cmdInsertPilot.Parameters.AddWithValue("@FName", txtFirstName.Text.Trim())
            cmdInsertPilot.Parameters.AddWithValue("@LName", txtLastName.Text.Trim())
            cmdInsertPilot.Parameters.AddWithValue("@EmpID", txtEmployeeID.Text.Trim())
            cmdInsertPilot.Parameters.AddWithValue("@Hire", dtmDateOfHire.Value)
            cmdInsertPilot.Parameters.AddWithValue("@Term", dtmDateOfTerm.Value)
            cmdInsertPilot.Parameters.AddWithValue("@Lic", dtmDateOfLicense.Value)
            cmdInsertPilot.Parameters.AddWithValue("@Role", cboRoles.SelectedValue)

            Dim rowsPilot As Integer = cmdInsertPilot.ExecuteNonQuery()


            ' ----------------------------------------------------
            ' INSERT EMPLOYEE LOGIN
            ' ----------------------------------------------------
            Dim strInsertEmp As String =
                "INSERT INTO TEmployee 
                (intEmployeeID, strEmployeeLoginID, strEmployeePassword, strEmployeeRole, intEmployeePersonID)
                 VALUES (?, ?, ?, ?, ?);"

            Dim cmdInsertEmp As New OleDb.OleDbCommand(strInsertEmp, m_conAdministrator)

            cmdInsertEmp.Parameters.AddWithValue("@EmpID", nextEmployeeID)
            cmdInsertEmp.Parameters.AddWithValue("@LoginID", txtPilotID.Text.Trim())
            cmdInsertEmp.Parameters.AddWithValue("@Pass", txtPilotPassword.Text.Trim())
            cmdInsertEmp.Parameters.AddWithValue("@Role", "Pilot")
            cmdInsertEmp.Parameters.AddWithValue("@PilotPersonID", nextPilotID)

            Dim rowsEmp As Integer = cmdInsertEmp.ExecuteNonQuery()


            ' ----------------------------------------------------
            ' CONFIRM RESULTS
            ' ----------------------------------------------------
            If rowsPilot > 0 And rowsEmp > 0 Then
                MessageBox.Show("Pilot successfully added!")
            Else
                MessageBox.Show("Insert failed.")
            End If

            CloseDatabaseConnection()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error adding pilot: " & ex.Message)
        End Try
    End Sub

    Private Function ValidatePilotData() As Boolean

        If txtFirstName.Text = "" Then
            MessageBox.Show("First Name is required.")
            Return False
        End If

        If txtLastName.Text = "" Then
            MessageBox.Show("Last Name is required.")
            Return False
        End If

        If txtEmployeeID.Text = "" Then
            MessageBox.Show("Employee ID is required.")
            Return False
        End If

        If cboRoles.Text = "" Then
            MessageBox.Show("Pilot Role is required.")
            Return False
        End If

        If dtmDateOfHire.Value > Today Then
            MessageBox.Show("Hire date cannot be in the future.")
            Return False
        End If

        If dtmDateOfTerm.Value < dtmDateOfHire.Value Then
            MessageBox.Show("Termination date cannot be before hire date.")
            Return False
        End If

        If dtmDateOfLicense.Value > Today Then
            MessageBox.Show("License date cannot be in the future.")
            Return False
        End If

        If txtPilotID.Text = "" Then
            MessageBox.Show("Must add your ID sign in")
            Return False
        Else
            If txtPilotPassword.Text = "" Then
                MessageBox.Show("Must add password")
                Return False
            End If
        End If

        Return True
    End Function
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()

        Dim frmPilotsMainMenu As New frmPilotsMainMenu

        frmPilotsMainMenu.ShowDialog()
    End Sub
End Class