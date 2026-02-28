Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class frmUpdateAttendentInfo
    Private Sub frmUpdateAttendantProfile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Ensure ID is valid
            If SelectedAttendentID <= 0 Then
                MessageBox.Show("No attendant selected.")
                Me.Close()
                Exit Sub
            End If

            ' Open DB connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            ' ===============================
            '  CALL: usp_GetAttendantByID
            ' ===============================
            Dim cmd As New OleDbCommand("usp_GetAttendantByID", m_conAdministrator)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@AttendantID", SelectedAttendentID)

            Dim dr As OleDbDataReader = cmd.ExecuteReader()

            If dr.Read() Then
                txtFirstName.Text = dr("strFirstName").ToString()
                txtLastName.Text = dr("strLastName").ToString()
                txtEmployeeID.Text = dr("strEmployeeID").ToString()
                txtAttendantID.Text = dr("strEmployeeLoginID").ToString()
                txtAttendantPassword.Text = dr("strEmployeePassword").ToString()

                If Not IsDBNull(dr("dtmDateOfHire")) Then
                    dtpHire.Value = CDate(dr("dtmDateOfHire"))
                End If

                If Not IsDBNull(dr("dtmDateOfTermination")) Then
                    dtpTerm.Value = CDate(dr("dtmDateOfTermination"))
                Else
                    dtpTerm.Checked = False
                End If

            Else
                MessageBox.Show("Attendant not found.")
                dr.Close()
                CloseDatabaseConnection()
                Me.Close()
                Exit Sub
            End If

            dr.Close()
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show("Error loading attendant info: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            ' Confirm update
            Dim result = MessageBox.Show("Save changes to attendant?",
                                 "Confirm Update",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question)

            If result = DialogResult.No Then
                Exit Sub
            End If

            ' Open DB connection
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If

            ' ===============================
            '  CALL: usp_UpdateAttendant
            ' ===============================
            Dim cmd As New OleDbCommand("usp_UpdateAttendant", m_conAdministrator)
            cmd.CommandType = CommandType.StoredProcedure

            ' Stored procedure parameters (MUST MATCH EXACT NAMES)
            cmd.Parameters.AddWithValue("@AttendantID", SelectedAttendentID)
            cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim())
            cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim())
            cmd.Parameters.AddWithValue("@EmployeeID", txtEmployeeID.Text.Trim())
            cmd.Parameters.AddWithValue("@DateOfHire", dtpHire.Value)

            ' Handle nullable termination date
            If dtpTerm.Checked Then
                cmd.Parameters.AddWithValue("@DateOfTermination", dtpTerm.Value)
            Else
                cmd.Parameters.AddWithValue("@DateOfTermination", DBNull.Value)
            End If

            ' Execute update
            Dim rows As Integer = cmd.ExecuteNonQuery()

            If rows > 0 Then
                MessageBox.Show("Attendant updated successfully!")
                Me.Close()
            Else
                MessageBox.Show("No changes saved.")
            End If

            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show("Error updating attendant: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class