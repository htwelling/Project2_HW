<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.radCustomer = New System.Windows.Forms.RadioButton()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.radPilots = New System.Windows.Forms.RadioButton()
        Me.radAttendents = New System.Windows.Forms.RadioButton()
        Me.radAdministration = New System.Windows.Forms.RadioButton()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'radCustomer
        '
        Me.radCustomer.AutoSize = True
        Me.radCustomer.Location = New System.Drawing.Point(310, 75)
        Me.radCustomer.Name = "radCustomer"
        Me.radCustomer.Size = New System.Drawing.Size(85, 20)
        Me.radCustomer.TabIndex = 0
        Me.radCustomer.TabStop = True
        Me.radCustomer.Text = "Customer"
        Me.radCustomer.UseVisualStyleBackColor = True
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(305, 199)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(114, 41)
        Me.btnSubmit.TabIndex = 1
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'radPilots
        '
        Me.radPilots.AutoSize = True
        Me.radPilots.Location = New System.Drawing.Point(310, 101)
        Me.radPilots.Name = "radPilots"
        Me.radPilots.Size = New System.Drawing.Size(61, 20)
        Me.radPilots.TabIndex = 2
        Me.radPilots.TabStop = True
        Me.radPilots.Text = "Pilots"
        Me.radPilots.UseVisualStyleBackColor = True
        '
        'radAttendents
        '
        Me.radAttendents.AutoSize = True
        Me.radAttendents.Location = New System.Drawing.Point(310, 128)
        Me.radAttendents.Name = "radAttendents"
        Me.radAttendents.Size = New System.Drawing.Size(91, 20)
        Me.radAttendents.TabIndex = 3
        Me.radAttendents.TabStop = True
        Me.radAttendents.Text = "Attendents"
        Me.radAttendents.UseVisualStyleBackColor = True
        '
        'radAdministration
        '
        Me.radAdministration.AutoSize = True
        Me.radAdministration.Location = New System.Drawing.Point(310, 155)
        Me.radAdministration.Name = "radAdministration"
        Me.radAdministration.Size = New System.Drawing.Size(112, 20)
        Me.radAdministration.TabIndex = 4
        Me.radAdministration.TabStop = True
        Me.radAdministration.Text = "Administration"
        Me.radAdministration.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(305, 260)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(114, 43)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.radAdministration)
        Me.Controls.Add(Me.radAttendents)
        Me.Controls.Add(Me.radPilots)
        Me.Controls.Add(Me.btnSubmit)
        Me.Controls.Add(Me.radCustomer)
        Me.Name = "Main"
        Me.Text = "Fly me 2 the Moon"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents radCustomer As RadioButton
    Friend WithEvents btnSubmit As Button
    Friend WithEvents radPilots As RadioButton
    Friend WithEvents radAttendents As RadioButton
    Friend WithEvents radAdministration As RadioButton
    Friend WithEvents btnExit As Button
End Class
