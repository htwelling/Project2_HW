<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AddCustomerFlight
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboFlights = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnAddFlight = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtSeat = New System.Windows.Forms.TextBox()
        Me.grpSeatType = New System.Windows.Forms.GroupBox()
        Me.radCheckInSeat = New System.Windows.Forms.RadioButton()
        Me.radReservedSeat = New System.Windows.Forms.RadioButton()
        Me.grpSeatType.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(257, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select Flight Date:"
        '
        'cboFlights
        '
        Me.cboFlights.FormattingEnabled = True
        Me.cboFlights.Items.AddRange(New Object() {"11/15/25", "11/30/25", "12/05/25", "12/20/25"})
        Me.cboFlights.Location = New System.Drawing.Point(222, 74)
        Me.cboFlights.Name = "cboFlights"
        Me.cboFlights.Size = New System.Drawing.Size(203, 24)
        Me.cboFlights.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 111)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Select Seat:"
        '
        'btnAddFlight
        '
        Me.btnAddFlight.Location = New System.Drawing.Point(140, 371)
        Me.btnAddFlight.Name = "btnAddFlight"
        Me.btnAddFlight.Size = New System.Drawing.Size(118, 36)
        Me.btnAddFlight.TabIndex = 4
        Me.btnAddFlight.Text = "Add Flight"
        Me.btnAddFlight.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(395, 371)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(118, 28)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Exit"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtSeat
        '
        Me.txtSeat.Location = New System.Drawing.Point(91, 111)
        Me.txtSeat.Name = "txtSeat"
        Me.txtSeat.Size = New System.Drawing.Size(100, 22)
        Me.txtSeat.TabIndex = 6
        '
        'grpSeatType
        '
        Me.grpSeatType.Controls.Add(Me.radCheckInSeat)
        Me.grpSeatType.Controls.Add(Me.txtSeat)
        Me.grpSeatType.Controls.Add(Me.radReservedSeat)
        Me.grpSeatType.Controls.Add(Me.Label2)
        Me.grpSeatType.Location = New System.Drawing.Point(131, 104)
        Me.grpSeatType.Name = "grpSeatType"
        Me.grpSeatType.Size = New System.Drawing.Size(388, 261)
        Me.grpSeatType.TabIndex = 7
        Me.grpSeatType.TabStop = False
        Me.grpSeatType.Text = "Seat Options"
        '
        'radCheckInSeat
        '
        Me.radCheckInSeat.AutoSize = True
        Me.radCheckInSeat.Location = New System.Drawing.Point(6, 66)
        Me.radCheckInSeat.Name = "radCheckInSeat"
        Me.radCheckInSeat.Size = New System.Drawing.Size(185, 20)
        Me.radCheckInSeat.TabIndex = 1
        Me.radCheckInSeat.TabStop = True
        Me.radCheckInSeat.Text = "Seat Assigned at Check-In"
        Me.radCheckInSeat.UseVisualStyleBackColor = True
        '
        'radReservedSeat
        '
        Me.radReservedSeat.AutoSize = True
        Me.radReservedSeat.Location = New System.Drawing.Point(6, 21)
        Me.radReservedSeat.Name = "radReservedSeat"
        Me.radReservedSeat.Size = New System.Drawing.Size(119, 20)
        Me.radReservedSeat.TabIndex = 0
        Me.radReservedSeat.TabStop = True
        Me.radReservedSeat.Text = "Reserved Seat"
        Me.radReservedSeat.UseVisualStyleBackColor = True
        '
        'AddCustomerFlight
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(712, 553)
        Me.Controls.Add(Me.grpSeatType)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnAddFlight)
        Me.Controls.Add(Me.cboFlights)
        Me.Controls.Add(Me.Label1)
        Me.Name = "AddCustomerFlight"
        Me.Text = "AddCustomerFlight"
        Me.grpSeatType.ResumeLayout(False)
        Me.grpSeatType.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cboFlights As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnAddFlight As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents txtSeat As TextBox
    Friend WithEvents grpSeatType As GroupBox
    Friend WithEvents radCheckInSeat As RadioButton
    Friend WithEvents radReservedSeat As RadioButton
End Class
