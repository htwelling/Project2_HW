Imports System.Data.OleDb

Public Class frmUpdatePilotInfo
    Private Sub frmUpdatePilotProfile_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If SelectedPilotID <= 0 Then
            MessageBox.Show("No pilot selected.")
            Me.Close()
            Exit Sub
        End If

        ' Load pilot roles into combo box
        LoadPilotRoles()

        Dim sql As String =
        "SELECT strFirstName, strLastName, strEmployeeID,
                dtmDateOfHire, dtmDateOfTermination, dtmDateOfLicense,
                intPilotRoleID
         FROM TPilots
         WHERE intPilotID = @PilotID"

        Dim cmd As New OleDbCommand(sql, m_conAdministrator)
        cmd.Parameters.AddWithValue("@PilotID", SelectedPilotID)

        Dim reader As OleDbDataReader = cmd.ExecuteReader()

        If reader.Read() Then
            txtFirstName.Text = reader("strFirstName").ToString()
            txtLastName.Text = reader("strLastName").ToString()
            txtEmployeeID.Text = reader("strEmployeeID").ToString()

            cboPilotRole.SelectedValue = CInt(reader("intPilotRoleID"))
        Else
            MessageBox.Show("Pilot not found.")
            Close()
        End If

        reader.Close()

    End Sub
    Private Sub LoadPilotRoles()
        Dim sql As String = "SELECT intPilotRoleID, strPilotRoleName FROM TPilotRoles"

        Dim cmd As New OleDbCommand(sql, m_conAdministrator)
        Dim dt As New DataTable
        Dim da As New OleDbDataAdapter(cmd)

        da.Fill(dt)

        cboPilotRole.DataSource = dt
        cboPilotRole.DisplayMember = "strPilotRoleName"
        cboPilotRole.ValueMember = "intPilotRoleID"
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim sql As String =
        "UPDATE TPilots SET
            strFirstName = @FirstName,
            strLastName = @LastName,
            strEmployeeID = @EmployeeID,
            dtmDateOfHire = @HireDate,
            dtmDateOfTermination = @TerminationDate,
            dtmDateOfLicense = @LicenseDate,
            intPilotRoleID = @RoleID
         WHERE intPilotID = @PilotID"

        Dim cmd As New OleDbCommand(sql, m_conAdministrator)

        cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim())
        cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim())
        cmd.Parameters.AddWithValue("@EmployeeID", txtEmployeeID.Text.Trim())

        cmd.Parameters.AddWithValue("@RoleID", CInt(cboPilotRole.SelectedValue))

        cmd.Parameters.AddWithValue("@PilotID", SelectedPilotID)

        Try
            Dim rows As Integer = cmd.ExecuteNonQuery()
            If rows > 0 Then
                MessageBox.Show("Pilot profile updated successfully!")
                Me.Close()
            Else
                MessageBox.Show("No changes saved.")
            End If

        Catch ex As Exception
            MessageBox.Show("Error updating pilot: " & ex.Message)
        End Try

    End Sub
End Class