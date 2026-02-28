Public Class frmCustomer
    Private Sub frmCustomer_Load()
        Try

            Dim strSelect As String = ""
            Dim cmdSelect As OleDb.OleDbCommand ' this will be used for our Select statement
            Dim drSourceTable As OleDb.OleDbDataReader ' this will be where our data is retrieved to
            Dim dtc As DataTable = New DataTable ' this is the table we will load from our reader for City
            Dim dts As DataTable = New DataTable ' this is the table we will load from our reader for State
            Dim dtr As DataTable = New DataTable ' this is the table we will load from our reader for Race

            ' open the DB
            If OpenDatabaseConnectionSQLServer() = False Then

                ' No, warn the user ...
                MessageBox.Show(Me, "Database connection error." & vbNewLine &
                                    "The application will now close.",
                                    Me.Text + " Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)

                ' and close the form/application
                Close()

            End If

            ' Build the select statement to obtain Cities
            strSelect = "SELECT intPassengerID, strLastName FROM TPassengers"

            ' Retrieve all the records 
            cmdSelect = New OleDb.OleDbCommand(strSelect, m_conAdministrator)
            drSourceTable = cmdSelect.ExecuteReader
            dtc.Load(drSourceTable)

            'load the Cities result set into the combobox.  For VB, we do this by binding the data to the combobox

            cboCustomers.ValueMember = "intPassengerID"
            cboCustomers.DisplayMember = "strLastName"
            cboCustomers.DataSource = dtc


            ' Clean up
            drSourceTable.Close()

            ' close the database connection
            CloseDatabaseConnection()

        Catch ex As Exception

            ' Log and display error message
            MessageBox.Show(ex.Message)

        End Try
    End Sub
    Private Sub btnAddCustomer_Click(sender As Object, e As EventArgs) Handles btnAddCustomer.Click

        Dim frmAddCustomer As New frmAddCustomer

        frmAddCustomer.ShowDialog()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If cboCustomers.Text = "" Then
            MessageBox.Show("Must pick customer name.")
            Exit Sub
        End If
        ' Save selected customer ID into the global variable
        SelectedCustomerID = CInt(cboCustomers.SelectedValue)

        Dim frmCustomerMainMenu As New frmCustomerMainMenu

        frmCustomerMainMenu.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Close()
    End Sub
End Class