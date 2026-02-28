<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStats
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblTotalCustomers = New System.Windows.Forms.Label()
        Me.lblTotalFlightsTaken = New System.Windows.Forms.Label()
        Me.lblAverageMilesFlown = New System.Windows.Forms.Label()
        Me.lstPilotsMiles = New System.Windows.Forms.ListBox()
        Me.lstAttendantMiles = New System.Windows.Forms.ListBox()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(88, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(168, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Total number of customers:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(88, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(214, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Total flights taken by all customers:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(88, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(211, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Average miles flown per customer:"
        '
        'lblTotalCustomers
        '
        Me.lblTotalCustomers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalCustomers.Location = New System.Drawing.Point(321, 9)
        Me.lblTotalCustomers.Name = "lblTotalCustomers"
        Me.lblTotalCustomers.Size = New System.Drawing.Size(126, 23)
        Me.lblTotalCustomers.TabIndex = 3
        '
        'lblTotalFlightsTaken
        '
        Me.lblTotalFlightsTaken.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalFlightsTaken.Location = New System.Drawing.Point(321, 46)
        Me.lblTotalFlightsTaken.Name = "lblTotalFlightsTaken"
        Me.lblTotalFlightsTaken.Size = New System.Drawing.Size(126, 23)
        Me.lblTotalFlightsTaken.TabIndex = 4
        '
        'lblAverageMilesFlown
        '
        Me.lblAverageMilesFlown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAverageMilesFlown.Location = New System.Drawing.Point(321, 86)
        Me.lblAverageMilesFlown.Name = "lblAverageMilesFlown"
        Me.lblAverageMilesFlown.Size = New System.Drawing.Size(126, 23)
        Me.lblAverageMilesFlown.TabIndex = 5
        '
        'lstPilotsMiles
        '
        Me.lstPilotsMiles.FormattingEnabled = True
        Me.lstPilotsMiles.ItemHeight = 16
        Me.lstPilotsMiles.Location = New System.Drawing.Point(91, 134)
        Me.lstPilotsMiles.Name = "lstPilotsMiles"
        Me.lstPilotsMiles.Size = New System.Drawing.Size(190, 180)
        Me.lstPilotsMiles.TabIndex = 6
        '
        'lstAttendantMiles
        '
        Me.lstAttendantMiles.FormattingEnabled = True
        Me.lstAttendantMiles.ItemHeight = 16
        Me.lstAttendantMiles.Location = New System.Drawing.Point(321, 134)
        Me.lstAttendantMiles.Name = "lstAttendantMiles"
        Me.lstAttendantMiles.Size = New System.Drawing.Size(190, 180)
        Me.lstAttendantMiles.TabIndex = 7
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(148, 343)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(94, 49)
        Me.btnSubmit.TabIndex = 8
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(353, 343)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(94, 50)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmStats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(673, 496)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.lstAttendantMiles)
        Me.Controls.Add(Me.lstPilotsMiles)
        Me.Controls.Add(Me.lblAverageMilesFlown)
        Me.Controls.Add(Me.lblTotalFlightsTaken)
        Me.Controls.Add(Me.lblTotalCustomers)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmStats"
        Me.Text = "frmStats"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblTotalCustomers As Label
    Friend WithEvents lblTotalFlightsTaken As Label
    Friend WithEvents lblAverageMilesFlown As Label
    Friend WithEvents lstPilotsMiles As ListBox
    Friend WithEvents lstAttendantMiles As ListBox
    Friend WithEvents btnSubmit As Button
    Friend WithEvents btnExit As Button
End Class
