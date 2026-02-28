Imports System.Data.OleDb

Public Class frmDeleteAttendent
    Private Sub frmDeleteAttendant_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If OpenDatabaseConnectionSQLServer() = False Then
            MessageBox.Show("Database connection failed.")
            Me.Close()
            Exit Sub
        End If

        LoadAttendants()
        CloseDatabaseConnection()
    End Sub

    Private Sub LoadAttendants()
        If OpenDatabaseConnectionSQLServer() = False Then Exit Sub

        cboAttendants.Items.Clear()

        Dim strSQL As String =
            "SELECT intAttendantID, strFirstName + ' ' + strLastName AS FullName " &
            "FROM TAttendants " &
            "ORDER BY strLastName, strFirstName"

        Dim cmd As New OleDbCommand(strSQL, m_conAdministrator)
        Dim rdr As OleDbDataReader = cmd.ExecuteReader()

        Dim dt As New DataTable()
        dt.Load(rdr)

        cboAttendants.DisplayMember = "FullName"
        cboAttendants.ValueMember = "intAttendantID"
        cboAttendants.DataSource = dt

        rdr.Close()
        CloseDatabaseConnection()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If cboAttendants.SelectedItem Is Nothing Then
            MessageBox.Show("Please select an attendant to delete.")
            Exit Sub
        End If

        Dim selectedAttendant = cboAttendants.SelectedItem
        Dim attendantID As Integer = selectedAttendant.ID
        Dim attendantName As String = selectedAttendant.Name

        ' confirmation
        Dim response = MessageBox.Show(
            "Are you sure you want to delete Attendant: " & attendantName & "?",
            "Confirm Deletion",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning)

        If response = DialogResult.No Then Exit Sub

        If OpenDatabaseConnectionSQLServer() = False Then
            MessageBox.Show("Database connection failed.")
            Exit Sub
        End If

        ' -----------------------------------------------
        ' DELETE USING STORED PROCEDURE
        ' -----------------------------------------------
        Try
            Dim cmd As New OleDbCommand("usp_DeleteAttendant", m_conAdministrator)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@intAttendantID", attendantID)

            cmd.ExecuteNonQuery()
            MessageBox.Show("Attendant successfully deleted!")
        Catch ex As Exception
            MessageBox.Show("Error deleting attendant: " & ex.Message)
        End Try

        CloseDatabaseConnection()

        ' Reload list after deletion
        LoadAttendants()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
        Dim frmAttendentsMainMenu As New frmAttendentsMainMenu
        frmAttendentsMainMenu.ShowDialog()
    End Sub
End Class