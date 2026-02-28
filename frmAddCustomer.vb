Imports System.Data.OleDb

Public Class frmAddCustomer
    Private Sub frmAddCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim strSelect As String
            Dim cmdSelect As OleDb.OleDbCommand
            Dim drSourceTable As OleDb.OleDbDataReader
            Dim dtCities As New DataTable
            Dim dtStates As New DataTable

            ' Open DB
            If OpenDatabaseConnectionSQLServer() = False Then

                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                "The application will now close.",
                                Me.Text + " Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)

                Me.Close()
            End If


            ' ----------------------------------------------------
            ' Load Cities
            ' ----------------------------------------------------
            strSelect = "SELECT strCity FROM TPassengers ORDER BY strCity"

            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader()
            dtCities.Load(drSourceTable)

            cboCities.ValueMember = "intPassengerID"
            cboCities.DisplayMember = "strCity"
            cboCities.DataSource = dtCities


            ' ----------------------------------------------------
            ' Load States
            ' ----------------------------------------------------
            strSelect = "SELECT intStateID, strState FROM TStates ORDER BY strState"

            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader()
            dtStates.Load(drSourceTable)

            cboStates.ValueMember = "intStateID"
            cboStates.DisplayMember = "strState"
            cboStates.DataSource = dtStates


            ' Cleanup
            drSourceTable.Close()

            ' Close DB
            CloseDatabaseConnection()

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try


    End Sub

    Private Sub btnAddCustomer_Click(sender As Object, e As EventArgs) Handles btnAddCustomer.Click
        If ValidateDataInput() = False Then Exit Sub

        Try
            If OpenDatabaseConnectionSQLServer() = False Then
                MessageBox.Show("Database connection error.")
                Exit Sub
            End If

            ' ----------------------------------------------------
            ' GET NEXT Passenger ID
            ' ----------------------------------------------------
            Dim nextCustomerID As Integer = 1
            Dim sqlCust As String = "SELECT MAX(intPassengerID) + 1 FROM TPassengers;"

            Using cmd As New OleDbCommand(sqlCust, m_conAdministrator)
                Dim result = cmd.ExecuteScalar()
                If Not IsDBNull(result) Then nextCustomerID = CInt(result)
            End Using

            ' ----------------------------------------------------
            ' INSERT INTO TCustomers
            ' ----------------------------------------------------
            Dim sqlInsertCustomer As String =
                "INSERT INTO TPassengers
                (intPassengerID, strFirstName, strLastName, strAddress,
                 strCity, intStateID, strZip, strPhoneNumber, strEmail, strPassengerLoginID,
                 strPassengerPassword, dtmDateOfBirth)
                 VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);"

            Using cmd As New OleDbCommand(sqlInsertCustomer, m_conAdministrator)
                cmd.Parameters.AddWithValue("@PassengerID", nextCustomerID)
                cmd.Parameters.AddWithValue("@FName", txtFirstName.Text.Trim())
                cmd.Parameters.AddWithValue("@LName", txtLastName.Text.Trim())
                cmd.Parameters.AddWithValue("@Street", txtAddress.Text.Trim())
                cmd.Parameters.AddWithValue("@City", cboCities.Text)
                cmd.Parameters.AddWithValue("@State", cboStates.SelectedValue)
                cmd.Parameters.AddWithValue("@Zip", txtZip.Text.Trim())
                cmd.Parameters.AddWithValue("@Phone", txtPhoneNumber.Text.Trim())
                cmd.Parameters.AddWithValue("@Email", txtEmailAddress.Text.Trim())
                cmd.Parameters.AddWithValue("@LoginID", txtCustomerID.Text.Trim())
                cmd.Parameters.AddWithValue("Password", txtCustomerPassword.Text.Trim())
                cmd.Parameters.AddWithValue("DOB", dtpDOB.Value)

                cmd.ExecuteNonQuery()
            End Using

            ' ----------------------------------------------------
            ' SUCCESS
            ' ----------------------------------------------------
            MessageBox.Show("Customer successfully added!")

            CloseDatabaseConnection()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Insert Error: " & ex.Message)
        End Try

    End Sub
    Private Function ValidateDataInput() As Boolean
        If txtFirstName.Text = "" Then
            MessageBox.Show("Must add First name")
            Return False
        Else
            If txtLastName.Text = "" Then
                MessageBox.Show("Must add Last name")
                Return False
            Else
                If txtAddress.Text = "" Then
                    MessageBox.Show("Must add address")
                    Return False
                End If
            End If
        End If

        If cboCities.Text = "" Then
            MessageBox.Show("Must put which city passenger lives in")
            Return False
        Else
            If cboStates.Text = "" Then
                MessageBox.Show("Must add which state passenger lives in")
                Return False
            End If
        End If

        If txtZip.Text = "" Then
            MessageBox.Show("Must add passengers zip code")
            Return False
        Else
            If txtPhoneNumber.Text = "" Then
                MessageBox.Show("Must add passengers phone number")
                Return False
            Else
                If txtEmailAddress.Text = "" Then
                    MessageBox.Show("Must add email address")
                    Return False
                End If
            End If
        End If

        If txtCustomerID.Text = "" Then
            MessageBox.Show("Must put in ID name")
            Return False
        Else
            If txtCustomerPassword.Text = "" Then
                MessageBox.Show("Must put in password")
                Return False
            End If
        End If

        Return True

    End Function

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class