Public Class frmAddCustomer
    Private Sub frmAddCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            Dim strSelect As String = ""
            Dim cmdSelect As OleDb.OleDbCommand ' this will be used for our Select statement
            Dim drSourceTable As OleDb.OleDbDataReader ' this will be where our data is retrieved to
            Dim dtc As DataTable = New DataTable ' this is the table we will load from our reader for City
            Dim dts As DataTable = New DataTable ' this is the table we will load from our reader for State

            ' open the DB
            If OpenDatabaseConnectionSQLServer() = False Then

                ' No, warn the user ...
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' and close the form/application
                Me.Close()

            End If

            ' Build the select statement to obtain Cities
            strSelect = "SELECT intCityID, strCity FROM TCities"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dtc.Load(drSourceTable)

            'load the Cities result set into the combobox.  For VB, we do this by binding the data to the combobox

            cboCities.ValueMember = "intCityID"
            cboCities.DisplayMember = "strCity"
            cboCities.DataSource = dtc


            ' Build the select statement
            strSelect = "SELECT intStateID, strState FROM TStates"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dts.Load(drSourceTable)


            'load the State result set into the combobox.  For VB, we do this by binding the data to the combobox


            cboStates.ValueMember = "intStateID"
            cboStates.DisplayMember = "strState"
            cboStates.DataSource = dts

            ' Clean up
            drSourceTable.Close()

            ' close the database connection
            CloseDatabaseConnection()

        Catch excError As Exception

            ' Log and display error message
            MessageBox.Show(excError.Message)

        End Try
    End Sub

    Private Sub btnAddCustomer_Click(sender As Object, e As EventArgs) Handles btnAddCustomer.Click
        ' variables for new player data and select and insert statements
        Dim strSelect As String
        Dim strInsert As String
        Dim strFirstName As String
        Dim strLastName As String
        Dim strAddress As String
        Dim intCity As Integer
        Dim intState As Integer
        Dim strZip As String
        Dim strPhoneNumber As String
        Dim strEmail As String

        Dim cmdSelect As OleDb.OleDbCommand ' select command object
        Dim cmdInsert As OleDb.OleDbCommand ' insert command object
        Dim drSourceTable As OleDb.OleDbDataReader ' data reader for pulling info
        Dim intNextPrimaryKey As Integer ' holds next highest PK value
        Dim intRowsAffected As Integer  ' how many rows were affected when sql executed

        Try


            ' validate data is entered
            ValidateDataInput()

            ' put values into strings
            strFirstName = txtFirstName.Text
            strLastName = txtLastName.Text
            strAddress = txtAddress.Text
            intCity = cboCities.SelectedValue
            intState = cboStates.SelectedValue
            strZip = txtZip.Text
            strPhoneNumber = txtPhoneNumber.Text
            strEmail = txtEmailAddress.Text

            If OpenDatabaseConnectionSQLServer() = False Then

                ' No, warn the user ...
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' and close the form/application
                Me.Close()

            End If

            strSelect = "SELECT MAX(intPassengerID) + 1 AS intNextPrimaryKey " &
                            " FROM TPassengers"

            ' Execute command
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            ' Read result( highest ID )
            drSourceTable.Read()

            ' Null? (empty table)
            If drSourceTable.IsDBNull(0) = True Then

                ' Yes, start numbering at 1
                intNextPrimaryKey = 1

            Else

                ' No, get the next highest ID
                intNextPrimaryKey = CInt(drSourceTable("intNextPrimaryKey"))

            End If
            ' build insert statement (columns must match DB columns in name and the # of columns)

            strInsert = "INSERT INTO TPassengers (intPassengerID, strFirstName, strLastName, strAddress, intCityID, intStateID, strZip, strPhoneNumber, strEmail)" &
                   "VALUES (" & intNextPrimaryKey & ", '" & strFirstName & "', '" & strLastName & "', '" & strAddress & "', " & intCity & ", " & intState & ", '" & strZip & "', '" & strPhoneNumber & "', '" & strEmail & "')"
            MessageBox.Show(strInsert)

            ' use insert command with sql string and connection object
            cmdInsert = New OleDb.OleDbCommand(strInsert, m_conAdministrator)

            ' execute query to insert data
            intRowsAffected = cmdInsert.ExecuteNonQuery()

            ' If not 0 insert successful
            If intRowsAffected > 0 Then
                MessageBox.Show("Student has been added")    ' let user know success
                ' close new player form
            End If


            CloseDatabaseConnection()       ' close connection if insert didn't work
            Close()



        Catch ex As Exception
            MessageBox.Show(ex.Message)
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
        Return True
    End Function
End Class