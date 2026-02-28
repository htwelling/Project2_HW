Imports System.Data.OleDb

Public Class frmUpdateCustomer
    Private Sub UpdatePassenger_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Me.Close()
                Exit Sub
            End If

            ' ============================
            ' LOAD DISTINCT CITIES
            ' ============================
            Dim dtCities As New DataTable
            Dim sqlCities As String =
                "SELECT DISTINCT strCity 
                 FROM TPassengers 
                 ORDER BY strCity"

            Using cmd As New OleDbCommand(sqlCities, m_conAdministrator)
                dtCities.Load(cmd.ExecuteReader())
            End Using

            cboCities.DisplayMember = "strCity"
            cboCities.ValueMember = "strCity"
            cboCities.DataSource = dtCities


            ' ============================
            ' LOAD STATES
            ' ============================
            Dim dtStates As New DataTable
            Dim sqlStates As String =
                "SELECT intStateID, strState 
                 FROM TStates 
                 ORDER BY strState"

            Using cmd As New OleDbCommand(sqlStates, m_conAdministrator)
                dtStates.Load(cmd.ExecuteReader())
            End Using

            cboStates.DisplayMember = "strState"
            cboStates.ValueMember = "intStateID"
            cboStates.DataSource = dtStates


            ' ============================
            ' LOAD SELECTED CUSTOMER DATA
            ' ============================
            Dim sqlCustomer As String =
                "SELECT strFirstName, strLastName, strAddress, strCity, 
                        intStateID, strZip, strPhoneNumber, strEmail,
                        strPassengerLoginID, strPassengerPassword, dtmDateOfBirth
                 FROM TPassengers
                 WHERE intPassengerID = ?"

            Using cmd As New OleDbCommand(sqlCustomer, m_conAdministrator)
                cmd.Parameters.AddWithValue("?", SelectedCustomerID)

                Dim dr As OleDbDataReader = cmd.ExecuteReader()

                If dr.Read() Then
                    txtFirstName.Text = dr("strFirstName").ToString()
                    txtLastName.Text = dr("strLastName").ToString()
                    txtAddress.Text = dr("strAddress").ToString()
                    cboCities.SelectedValue = dr("strCity").ToString()
                    cboStates.SelectedValue = CInt(dr("intStateID"))
                    txtZip.Text = dr("strZip").ToString()
                    txtPhoneNumber.Text = dr("strPhoneNumber").ToString()
                    txtEmailAddress.Text = dr("strEmail").ToString()
                    txtCustomerID.Text = dr("strPassengerLoginID").ToString()
                    txtCustomerPassword.Text = dr("strPassengerPassword").ToString()

                    If Not IsDBNull(dr("dtmDateOfBirth")) Then
                        dtpDOB.Value = CDate(dr("dtmDateOfBirth"))
                    End If
                Else
                    MessageBox.Show("Customer not found.")
                    Me.Close()
                End If

                dr.Close()
            End Using

            CloseDatabaseConnection()

        Catch ex As Exception
            MessageBox.Show("Load Error: " & ex.Message)
        End Try
    End Sub
    Private Sub btnUpdateCustomer_Click(sender As Object, e As EventArgs) Handles btnUpdateCustomer.Click
        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If

            Dim sqlUpdate As String =
                "UPDATE TPassengers SET
                    strFirstName = ?,
                    strLastName = ?,
                    strAddress = ?,
                    strCity = ?,
                    intStateID = ?,
                    strZip = ?,
                    strPhoneNumber = ?,
                    strEmail = ?,
                    strPassengerLoginID = ?,
                    strPassengerPassword = ?,
                    dtmDateOfBirth = ?
                 WHERE intPassengerID = ?"

            Using cmd As New OleDbCommand(sqlUpdate, m_conAdministrator)

                cmd.Parameters.AddWithValue("?", txtFirstName.Text)
                cmd.Parameters.AddWithValue("?", txtLastName.Text)
                cmd.Parameters.AddWithValue("?", txtAddress.Text)
                cmd.Parameters.AddWithValue("?", cboCities.SelectedValue)   ' FIXED
                cmd.Parameters.AddWithValue("?", cboStates.SelectedValue)
                cmd.Parameters.AddWithValue("?", txtZip.Text)
                cmd.Parameters.AddWithValue("?", txtPhoneNumber.Text)
                cmd.Parameters.AddWithValue("?", txtEmailAddress.Text)
                cmd.Parameters.AddWithValue("?", txtCustomerID.Text)
                cmd.Parameters.AddWithValue("?", txtCustomerPassword.Text)
                cmd.Parameters.AddWithValue("?", dtpDOB.Value)
                cmd.Parameters.AddWithValue("?", SelectedCustomerID)

                Dim rows As Integer = cmd.ExecuteNonQuery()

                If rows = 1 Then
                    MessageBox.Show("Customer updated successfully.")
                Else
                    MessageBox.Show("Update failed.")
                End If
            End Using

            CloseDatabaseConnection()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Update Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class