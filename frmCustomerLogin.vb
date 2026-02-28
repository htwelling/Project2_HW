Imports System.Data.OleDb

Public Class frmCustomerLogin
    Private Sub btnNewCustomer_Click(sender As Object, e As EventArgs) Handles btnNewCustomer.Click
        Dim frmAddCustomer As New frmAddCustomer

        frmAddCustomer.ShowDialog()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If txtCustomerID.Text.Trim() = "" Then
            MessageBox.Show("Please enter Login ID.")
            Exit Sub
        End If

        If txtCustomerPassword.Text.Trim() = "" Then
            MessageBox.Show("Please enter Password.")
            Exit Sub
        End If

        '==============================
        ' OPEN DATABASE
        '==============================
        If OpenDatabaseConnectionSQLServer() = False Then
            MessageBox.Show("Database connection error.")
            Exit Sub
        End If

        '==============================
        ' LOOK FOR MATCH IN DATABASE
        '==============================
        Dim strSQL As String =
            "SELECT intPassengerID
             FROM TPassengers
             WHERE strPassengerLoginID = ?
               AND strPassengerPassword = ?"

        Dim cmd As New OleDbCommand(strSQL, m_conAdministrator)
        cmd.Parameters.AddWithValue("@p1", txtCustomerID.Text.Trim())
        cmd.Parameters.AddWithValue("@p2", txtCustomerPassword.Text.Trim())

        Dim rdr As OleDbDataReader = cmd.ExecuteReader()

        '==============================
        ' CHECK IF LOGIN MATCHES
        '==============================
        If rdr.HasRows Then
            rdr.Read()

            ' Store passenger ID in global variable
            SelectedCustomerID = CInt(rdr("intPassengerID"))

            rdr.Close()
            CloseDatabaseConnection()

            MessageBox.Show("Login successful!")

            ' Take user to Passenger Main Menu
            Dim frmCustomerMainMenu As New frmCustomerMainMenu

            frmCustomerMainMenu.ShowDialog()

        Else
            rdr.Close()
            CloseDatabaseConnection()

            MessageBox.Show("ID and/or Password are not Valid.")
        End If

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class