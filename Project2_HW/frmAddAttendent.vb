Imports System.Data.OleDb

Public Class frmAddAttendent
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If ValidateData() = False Then Exit Sub

        Dim strFirst As String = txtFirstName.Text.Trim()
        Dim strLast As String = txtLastName.Text.Trim()
        Dim strEmployeeID As String = txtEmployeeID.Text.Trim()
        Dim dtHire As Date = dtmDateOfHire.Value
        Dim dtTermination As Date = dtmDateOfTerm.Value

        If OpenDatabaseConnectionSQLServer() = False Then
            MessageBox.Show("Database connection failed.")
            Exit Sub
        End If

        '----------------------------------------------------------
        ' GET NEXT PRIMARY KEY
        '----------------------------------------------------------
        Dim intNextID As Integer
        Dim strPKSQL As String = "SELECT MAX(intAttendantID) + 1 FROM TAttendants"
        Dim cmdPK As New OleDbCommand(strPKSQL, m_conAdministrator)
        Dim result = cmdPK.ExecuteScalar()

        If IsDBNull(result) Then
            intNextID = 1
        Else
            intNextID = CInt(result)
        End If

        '----------------------------------------------------------
        ' INSERT STATEMENT
        '----------------------------------------------------------
        Dim strInsert As String =
            "INSERT INTO TAttendants " &
            "(intAttendantID, strFirstName, strLastName, strEmployeeID, dtmDateOfHire, dtmDateOfTermination) " &
            "VALUES (?, ?, ?, ?, ?, ?)"

        Dim cmdInsert As New OleDbCommand(strInsert, m_conAdministrator)
        cmdInsert.Parameters.AddWithValue("@p1", intNextID)
        cmdInsert.Parameters.AddWithValue("@p2", strFirst)
        cmdInsert.Parameters.AddWithValue("@p3", strLast)
        cmdInsert.Parameters.AddWithValue("@p4", strEmployeeID)
        cmdInsert.Parameters.AddWithValue("@p5", dtHire)
        cmdInsert.Parameters.AddWithValue("@p6", dtTermination)

        Try
            cmdInsert.ExecuteNonQuery()
            MessageBox.Show("Attendant successfully added!")
        Catch ex As Exception
            MessageBox.Show("Error adding attendant: " & ex.Message)
        End Try

        CloseDatabaseConnection()
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