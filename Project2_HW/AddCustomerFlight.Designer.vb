<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddCustomerFlight
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboFlights = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboSeats = New System.Windows.Forms.ComboBox()
        Me.btnAddFlight = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(243, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select Flight Date:"
        '
        'cboFlights
        '
        Me.cboFlights.FormattingEnabled = True
        Me.cboFlights.Items.AddRange(New Object() {"11/15/25", "11/30/25", "12/05/25", "12/20/25"})
        Me.cboFlights.Location = New System.Drawing.Point(226, 112)
        Me.cboFlights.Name = "cboFlights"
        Me.cboFlights.Size = New System.Drawing.Size(145, 24)
        Me.cboFlights.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(257, 186)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 16)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Select Seat:"
        '
        'cboSeats
        '
        Me.cboSeats.FormattingEnabled = True
        Me.cboSeats.Items.AddRange(New Object() {"13A", "15B", "17C", "23D", "25F"})
        Me.cboSeats.Location = New System.Drawing.Point(237, 205)
        Me.cboSeats.Name = "cboSeats"
        Me.cboSeats.Size = New System.Drawing.Size(121, 24)
        Me.cboSeats.TabIndex = 3
        '
        'btnAddFlight
        '
        Me.btnAddFlight.Location = New System.Drawing.Point(237, 277)
        Me.btnAddFlight.Name = "btnAddFlight"
        Me.btnAddFlight.Size = New System.Drawing.Size(118, 33)
        Me.btnAddFlight.TabIndex = 4
        Me.btnAddFlight.Text = "Add Flight"
        Me.btnAddFlight.UseVisualStyleBackColor = True
        '
        'AddCustomerFlight
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(712, 475)
        Me.Controls.Add(Me.btnAddFlight)
        Me.Controls.Add(Me.cboSeats)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboFlights)
        Me.Controls.Add(Me.Label1)
        Me.Name = "AddCustomerFlight"
        Me.Text = "AddCustomerFlight"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cboFlights As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboSeats As ComboBox
    Friend WithEvents btnAddFlight As Button
End Class
