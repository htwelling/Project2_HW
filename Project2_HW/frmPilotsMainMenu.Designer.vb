<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPilotsMainMenu
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
        Me.btnUpdatePilot = New System.Windows.Forms.Button()
        Me.btnPastFlight = New System.Windows.Forms.Button()
        Me.btnFutureFlight = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(167, 167)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(151, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Update pilot information:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(209, 205)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Show past flights:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(203, 244)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Show future flights:"
        '
        'btnUpdatePilot
        '
        Me.btnUpdatePilot.Location = New System.Drawing.Point(325, 160)
        Me.btnUpdatePilot.Name = "btnUpdatePilot"
        Me.btnUpdatePilot.Size = New System.Drawing.Size(159, 23)
        Me.btnUpdatePilot.TabIndex = 3
        Me.btnUpdatePilot.Text = "Update information"
        Me.btnUpdatePilot.UseVisualStyleBackColor = True
        '
        'btnPastFlight
        '
        Me.btnPastFlight.Location = New System.Drawing.Point(324, 198)
        Me.btnPastFlight.Name = "btnPastFlight"
        Me.btnPastFlight.Size = New System.Drawing.Size(160, 23)
        Me.btnPastFlight.TabIndex = 4
        Me.btnPastFlight.Text = "Show past flights"
        Me.btnPastFlight.UseVisualStyleBackColor = True
        '
        'btnFutureFlight
        '
        Me.btnFutureFlight.Location = New System.Drawing.Point(325, 236)
        Me.btnFutureFlight.Name = "btnFutureFlight"
        Me.btnFutureFlight.Size = New System.Drawing.Size(159, 23)
        Me.btnFutureFlight.TabIndex = 5
        Me.btnFutureFlight.Text = "Show future flights"
        Me.btnFutureFlight.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(265, 313)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(107, 38)
        Me.btnClose.TabIndex = 6
        Me.btnClose.Text = "Exit"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmPilotsMainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnFutureFlight)
        Me.Controls.Add(Me.btnPastFlight)
        Me.Controls.Add(Me.btnUpdatePilot)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmPilotsMainMenu"
        Me.Text = "frmPilotsMainMenu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnUpdatePilot As Button
    Friend WithEvents btnPastFlight As Button
    Friend WithEvents btnFutureFlight As Button
    Friend WithEvents btnClose As Button
End Class
