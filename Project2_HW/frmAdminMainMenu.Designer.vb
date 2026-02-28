<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdminMainMenu
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
        Me.btnManagePilots = New System.Windows.Forms.Button()
        Me.btnManageAttendents = New System.Windows.Forms.Button()
        Me.btnStats = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnManagePilots
        '
        Me.btnManagePilots.Location = New System.Drawing.Point(271, 103)
        Me.btnManagePilots.Name = "btnManagePilots"
        Me.btnManagePilots.Size = New System.Drawing.Size(174, 48)
        Me.btnManagePilots.TabIndex = 0
        Me.btnManagePilots.Text = "Manage Pilots"
        Me.btnManagePilots.UseVisualStyleBackColor = True
        '
        'btnManageAttendents
        '
        Me.btnManageAttendents.Location = New System.Drawing.Point(271, 178)
        Me.btnManageAttendents.Name = "btnManageAttendents"
        Me.btnManageAttendents.Size = New System.Drawing.Size(174, 49)
        Me.btnManageAttendents.TabIndex = 1
        Me.btnManageAttendents.Text = "Manage Attendents"
        Me.btnManageAttendents.UseVisualStyleBackColor = True
        '
        'btnStats
        '
        Me.btnStats.Location = New System.Drawing.Point(271, 253)
        Me.btnStats.Name = "btnStats"
        Me.btnStats.Size = New System.Drawing.Size(174, 48)
        Me.btnStats.TabIndex = 2
        Me.btnStats.Text = "FlyMe2TheMoon Stats"
        Me.btnStats.UseVisualStyleBackColor = True
        '
        'frmAdminMainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnStats)
        Me.Controls.Add(Me.btnManageAttendents)
        Me.Controls.Add(Me.btnManagePilots)
        Me.Name = "frmAdminMainMenu"
        Me.Text = "frmAdminMainMenu"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnManagePilots As Button
    Friend WithEvents btnManageAttendents As Button
    Friend WithEvents btnStats As Button
End Class
