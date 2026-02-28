Imports System.Data.OleDb

Public Class frmAttendents
    Private Sub frmSelectAttendant_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            Dim sql As String =
            "SELECT intAttendantID,
                    (strFirstName + ' ' + strLastName) AS FullName
             FROM TAttendants
             ORDER BY strLastName, strFirstName"

            Dim cmd As New OleDbCommand(sql, m_conAdministrator)
            Dim reader As OleDbDataReader = cmd.ExecuteReader()

            Dim dt As New DataTable
            dt.Load(reader)

            ' Bind to combo box
            cboAttendents.ValueMember = "intAttendantID"
            cboAttendents.DisplayMember = "FullName"
            cboAttendents.DataSource = dt

            reader.Close()
            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show("Error loading attendants: " & ex.Message)
        End Try
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Ensure a selection was made
        If cboAttendents.Text = "" Then
            MessageBox.Show("Please select an attendant.")
            Exit Sub
        End If

        ' Save selected Attendant ID globally
        SelectedAttendentID = CInt(cboAttendents.SelectedValue)

        MessageBox.Show("Attendant selected successfully.")

        Dim frmAttendentsMainMenu As New frmAttendentsMainMenu

        frmAttendentsMainMenu.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class