<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdminPilots
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
        Me.btnAddPilots = New System.Windows.Forms.Button()
        Me.btnDeletePilots = New System.Windows.Forms.Button()
        Me.btnAddPilotsToFlights = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAddPilots
        '
        Me.btnAddPilots.Location = New System.Drawing.Point(291, 76)
        Me.btnAddPilots.Name = "btnAddPilots"
        Me.btnAddPilots.Size = New System.Drawing.Size(154, 40)
        Me.btnAddPilots.TabIndex = 0
        Me.btnAddPilots.Text = "Add Pilots"
        Me.btnAddPilots.UseVisualStyleBackColor = True
        '
        'btnDeletePilots
        '
        Me.btnDeletePilots.Location = New System.Drawing.Point(291, 143)
        Me.btnDeletePilots.Name = "btnDeletePilots"
        Me.btnDeletePilots.Size = New System.Drawing.Size(154, 45)
        Me.btnDeletePilots.TabIndex = 1
        Me.btnDeletePilots.Text = "Delete Pilots"
        Me.btnDeletePilots.UseVisualStyleBackColor = True
        '
        'btnAddPilotsToFlights
        '
        Me.btnAddPilotsToFlights.Location = New System.Drawing.Point(291, 217)
        Me.btnAddPilotsToFlights.Name = "btnAddPilotsToFlights"
        Me.btnAddPilotsToFlights.Size = New System.Drawing.Size(154, 47)
        Me.btnAddPilotsToFlights.TabIndex = 2
        Me.btnAddPilotsToFlights.Text = "Add pilots to future flights"
        Me.btnAddPilotsToFlights.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(324, 350)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(93, 30)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmAdminPilots
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnAddPilotsToFlights)
        Me.Controls.Add(Me.btnDeletePilots)
        Me.Controls.Add(Me.btnAddPilots)
        Me.Name = "frmAdminPilots"
        Me.Text = "frmAdminPilots"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnAddPilots As Button
    Friend WithEvents btnDeletePilots As Button
    Friend WithEvents btnAddPilotsToFlights As Button
    Friend WithEvents btnExit As Button
End Class
