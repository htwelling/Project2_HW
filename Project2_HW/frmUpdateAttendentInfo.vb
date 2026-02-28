Imports System.Data.SqlClient

Public Class frmUpdateAttendentInfo
    Private Sub frmUpdateAttendantProfile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAttendantInfo()
    End Sub

    Private Sub LoadAttendantInfo()
        Try
            Dim strSQL As String = "
            SELECT strFirstName, strLastName, strEmployeeID,
                   dtmDateOfHire, dtmDateOfTermination
            FROM TAttendants
            WHERE intAttendantID = ?
        "

            Using cmd As New OleDb.OleDbCommand(strSQL, m_conAdministrator)
                cmd.Parameters.AddWithValue("@AttendantID", SelectedAttendentID)

                Dim rdr As OleDb.OleDbDataReader = cmd.ExecuteReader()

                If rdr.Read() Then
                    txtFirstName.Text = rdr("strFirstName").ToString()
                    txtLastName.Text = rdr("strLastName").ToString()
                    txtEmployeeID.Text = rdr("strEmployeeID").ToString()
                    txtDateofHire.Text = CDate(rdr("dtmDateOfHire"))
                    txtDateofTermination.Text = CDate(rdr("dtmDateOfTermination"))
                End If

                rdr.Close()
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading attendant information: " & ex.Message)
        End Try
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            Dim strSQL As String = "
            UPDATE TAttendants
            SET strFirstName = ?, 
                strLastName = ?, 
                strEmployeeID = ?, 
                dtmDateOfHire = ?, 
                dtmDateOfTermination = ?
            WHERE intAttendantID = ?
        "

            Using cmd As New OleDb.OleDbCommand(strSQL, m_conAdministrator)
                cmd.Parameters.AddWithValue("@First", txtFirstName.Text)
                cmd.Parameters.AddWithValue("@Last", txtLastName.Text)
                cmd.Parameters.AddWithValue("@EmpID", txtEmployeeID.Text)
                cmd.Parameters.AddWithValue("@Hire", txtDateofHire.Text)
                cmd.Parameters.AddWithValue("@Term", txtDateofTermination.Text)
                cmd.Parameters.AddWithValue("@AttendantID", SelectedAttendentID)

                cmd.ExecuteNonQuery()
            End Using

            MessageBox.Show("Attendant profile updated successfully.")
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Error saving profile: " & ex.Message)
        End Try
    End Sub
End Class