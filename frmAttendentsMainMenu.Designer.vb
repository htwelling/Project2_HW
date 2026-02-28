<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttendentsMainMenu
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnFutureFlight = New System.Windows.Forms.Button()
        Me.btnPastFlight = New System.Windows.Forms.Button()
        Me.btnUpdateAttendent = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(340, 283)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(107, 38)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "Exit"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnFutureFlight
        '
        Me.btnFutureFlight.Location = New System.Drawing.Point(400, 206)
        Me.btnFutureFlight.Name = "btnFutureFlight"
        Me.btnFutureFlight.Size = New System.Drawing.Size(159, 23)
        Me.btnFutureFlight.TabIndex = 12
        Me.btnFutureFlight.Text = "Show future flights"
        Me.btnFutureFlight.UseVisualStyleBackColor = True
        '
        'btnPastFlight
        '
        Me.btnPastFlight.Location = New System.Drawing.Point(399, 168)
        Me.btnPastFlight.Name = "btnPastFlight"
        Me.btnPastFlight.Size = New System.Drawing.Size(160, 23)
        Me.btnPastFlight.TabIndex = 11
        Me.btnPastFlight.Text = "Show past flights"
        Me.btnPastFlight.UseVisualStyleBackColor = True
        '
        'btnUpdateAttendent
        '
        Me.btnUpdateAttendent.Location = New System.Drawing.Point(400, 130)
        Me.btnUpdateAttendent.Name = "btnUpdateAttendent"
        Me.btnUpdateAttendent.Size = New System.Drawing.Size(159, 23)
        Me.btnUpdateAttendent.TabIndex = 10
        Me.btnUpdateAttendent.Text = "Update information"
        Me.btnUpdateAttendent.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(278, 214)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 16)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Show future flights:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(284, 175)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Show past flights:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(212, 133)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(181, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Update attendent information:"
        '
        'frmAttendentsMainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnFutureFlight)
        Me.Controls.Add(Me.btnPastFlight)
        Me.Controls.Add(Me.btnUpdateAttendent)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmAttendentsMainMenu"
        Me.Text = "frmAttendentsMainMenu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnClose As Button
    Friend WithEvents btnFutureFlight As Button
    Friend WithEvents btnPastFlight As Button
    Friend WithEvents btnUpdateAttendent As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
End Class
