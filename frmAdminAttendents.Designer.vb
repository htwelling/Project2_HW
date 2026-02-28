<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdminAttendents
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
        Me.btnAddAttendent = New System.Windows.Forms.Button()
        Me.btnDeleteAttendent = New System.Windows.Forms.Button()
        Me.btnAddAttendentToFlight = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAddAttendent
        '
        Me.btnAddAttendent.Location = New System.Drawing.Point(288, 81)
        Me.btnAddAttendent.Name = "btnAddAttendent"
        Me.btnAddAttendent.Size = New System.Drawing.Size(139, 36)
        Me.btnAddAttendent.TabIndex = 0
        Me.btnAddAttendent.Text = "Add Attendent"
        Me.btnAddAttendent.UseVisualStyleBackColor = True
        '
        'btnDeleteAttendent
        '
        Me.btnDeleteAttendent.Location = New System.Drawing.Point(288, 144)
        Me.btnDeleteAttendent.Name = "btnDeleteAttendent"
        Me.btnDeleteAttendent.Size = New System.Drawing.Size(139, 39)
        Me.btnDeleteAttendent.TabIndex = 1
        Me.btnDeleteAttendent.Text = "Delete Attendent"
        Me.btnDeleteAttendent.UseVisualStyleBackColor = True
        '
        'btnAddAttendentToFlight
        '
        Me.btnAddAttendentToFlight.Location = New System.Drawing.Point(288, 215)
        Me.btnAddAttendentToFlight.Name = "btnAddAttendentToFlight"
        Me.btnAddAttendentToFlight.Size = New System.Drawing.Size(139, 43)
        Me.btnAddAttendentToFlight.TabIndex = 2
        Me.btnAddAttendentToFlight.Text = "Add attendent to flight"
        Me.btnAddAttendentToFlight.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(315, 369)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(97, 28)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Exit"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmAdminAttendents
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnAddAttendentToFlight)
        Me.Controls.Add(Me.btnDeleteAttendent)
        Me.Controls.Add(Me.btnAddAttendent)
        Me.Name = "frmAdminAttendents"
        Me.Text = "frmAdminAttendents"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnAddAttendent As Button
    Friend WithEvents btnDeleteAttendent As Button
    Friend WithEvents btnAddAttendentToFlight As Button
    Friend WithEvents btnClose As Button
End Class
