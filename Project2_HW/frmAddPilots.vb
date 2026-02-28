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

        ValidatePilotData()

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

        Return True
    End Function
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If ValidatePilotData() = False Then
            Exit Sub
        End If

        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            ' Get next primary key
            Dim strSelect As String =
                "SELECT MAX(intPilotID) + 1 AS NextID FROM TPilots;"

            Dim cmdSelect As New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            Dim rdr As OleDb.OleDbDataReader = cmdSelect.ExecuteReader()

            Dim nextID As Integer = 1
            If rdr.Read() AndAlso Not rdr.IsDBNull(0) Then
                nextID = CInt(rdr("NextID"))
            End If

            rdr.Close()

            ' Insert Query
            Dim strInsert As String =
                "INSERT INTO TPilots (intPilotID, strFirstName, strLastName, strEmployeeID, " &
                "dtmDateOfHire, dtmDateOfTermination, dtmDateOfLicense, intPilotRoleID) " &
                "VALUES (?, ?, ?, ?, ?, ?, ?, ?);"

            Dim cmdInsert As New OleDb.OleDbCommand(strInsert, m_conAdministrator)

            cmdInsert.Parameters.AddWithValue("@ID", nextID)
            cmdInsert.Parameters.AddWithValue("@FName", txtFirstName.Text.Trim())
            cmdInsert.Parameters.AddWithValue("@LName", txtLastName.Text.Trim())
            cmdInsert.Parameters.AddWithValue("@EmpID", txtEmployeeID.Text.Trim())
            cmdInsert.Parameters.AddWithValue("@Hire", dtmDateOfHire.Value)
            cmdInsert.Parameters.AddWithValue("@Term", dtmDateOfTerm.Value)
            cmdInsert.Parameters.AddWithValue("@Lic", dtmDateOfLicense.Value)
            cmdInsert.Parameters.AddWithValue("@Role", cboRoles.SelectedValue)

            Dim rows As Integer = cmdInsert.ExecuteNonQuery()

            If rows > 0 Then
                MessageBox.Show("Pilot successfully added!")
            Else
                MessageBox.Show("Insert failed.")
            End If

            CloseDatabaseConnection()
            Me.Close() ' return to Pilot Main Menu

        Catch ex As Exception
            MessageBox.Show("Error adding pilot: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()

        Dim frmPilotsMainMenu As New frmPilotsMainMenu

        frmPilotsMainMenu.ShowDialog()
    End Sub
End Class