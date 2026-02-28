Public Class frmUpdateCustomer
    Private Sub UpdatePassenger_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim strSelect As String = ""
        Dim cmdSelect As OleDb.OleDbCommand ' this will be used for our Select statement
        Dim drSourceTable As OleDb.OleDbDataReader ' this will be where our data is retrieved to
        Dim dt As DataTable = New DataTable ' this is the table we will load from our reader
        Dim dtc As DataTable = New DataTable ' this is the table we will load from our reader for City
        Dim dts As DataTable = New DataTable ' this is the table we will load from our reader for State

        Try
            ' loop through the textboxes and clear them in case they have data in them after a delete
            For Each cntrl As Control In Controls
                If TypeOf cntrl Is TextBox Then
                    cntrl.Text = String.Empty
                End If
            Next

            ' open the DB this is in module
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


            ' Build the select statement
            strSelect = "SELECT intStudentID, strFirstName + ' ' + strLastName as StudentName FROM TStudents"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader

            ' load table from data reader
            dt.Load(drSourceTable)

            ' Clean up
            drSourceTable.Close()

            ' close the database connection
            CloseDatabaseConnection()

        Catch excError As Exception

            ' Log and display error message
            MessageBox.Show(excError.Message)

        End Try
    End Sub
    Private Sub btnUpdateCustomer_Click(sender As Object, e As EventArgs) Handles btnUpdateCustomer.Click
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
        Dim intRowsAffected As Integer

        ' thie will hold our Update statement
        Dim cmdUpdate As OleDb.OleDbCommand

        Try




            ' open database this is in module
            If OpenDatabaseConnectionSQLServer() = False Then

                ' No, warn the user ...
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                        "The application will now close.",
                                        Me.Text + " Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' and close the form/application
                Me.Close()

            End If

            strFirstName = txtFirstName.Text
            strLastName = txtLastName.Text
            strAddress = txtAddress.Text
            intCity = cboCities.SelectedValue
            intState = cboStates.SelectedValue
            strZip = txtZip.Text
            strPhoneNumber = txtPhoneNumber.Text
            strEmail = txtEmailAddress.Text


            ' Build the select statement using PK from name selected
            strSelect = "Update TPassengers Set " &
                        "strFirstName = '" & strFirstName & "', " &
                        "strLastName = '" & strLastName & "', " &
                        "strAddress = '" & strAddress & "', " &
                        "intCityID = " & intCity & ", " &
                        "intStateID = " & intState & ", " &
                        "strZip = '" & strZip & "', " &
                        "strPhoneNumber = '" & strPhoneNumber & "' , " &
                        "strEmail = '" & strEmail & "' , " & "' , "

            ' uncomment out the following message box line to use as a tool to check your sql statement
            ' remember anything not a numeric value going into SQL Server must have single quotes '
            ' around it, including dates.

            MessageBox.Show(strSelect)


            ' make the connection
            cmdUpdate = New OleDb.OleDbCommand(strSelect, m_conAdministrator)

            ' IUpdate the row with execute the statement
            intRowsAffected = cmdUpdate.ExecuteNonQuery()

            ' have to let the user know what happened 
            If intRowsAffected = 1 Then
                MessageBox.Show("Update successful")
            Else
                MessageBox.Show("Update failed")
            End If

            ' close the database connection
            CloseDatabaseConnection()

            Close()



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class