<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmModifierProfil
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lblTitreModifierProfil = New System.Windows.Forms.Label()
        Me.btnDeleteProfile = New System.Windows.Forms.Button()
        Me.btnFermer = New System.Windows.Forms.Button()
        Me.btnChangerNom = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblTitreModifierProfil
        '
        Me.lblTitreModifierProfil.AutoSize = True
        Me.lblTitreModifierProfil.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitreModifierProfil.Location = New System.Drawing.Point(30, 17)
        Me.lblTitreModifierProfil.Name = "lblTitreModifierProfil"
        Me.lblTitreModifierProfil.Size = New System.Drawing.Size(263, 37)
        Me.lblTitreModifierProfil.TabIndex = 0
        Me.lblTitreModifierProfil.Text = "Modifier le Profil"
        '
        'btnDeleteProfile
        '
        Me.btnDeleteProfile.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDeleteProfile.Location = New System.Drawing.Point(63, 70)
        Me.btnDeleteProfile.Name = "btnDeleteProfile"
        Me.btnDeleteProfile.Size = New System.Drawing.Size(100, 23)
        Me.btnDeleteProfile.TabIndex = 0
        Me.btnDeleteProfile.Text = "Supprimer le profil"
        Me.btnDeleteProfile.UseVisualStyleBackColor = True
        '
        'btnFermer
        '
        Me.btnFermer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFermer.Location = New System.Drawing.Point(117, 110)
        Me.btnFermer.Name = "btnFermer"
        Me.btnFermer.Size = New System.Drawing.Size(100, 23)
        Me.btnFermer.TabIndex = 3
        Me.btnFermer.Text = "Fermer"
        Me.btnFermer.UseVisualStyleBackColor = True
        '
        'btnChangerNom
        '
        Me.btnChangerNom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnChangerNom.Location = New System.Drawing.Point(169, 70)
        Me.btnChangerNom.Name = "btnChangerNom"
        Me.btnChangerNom.Size = New System.Drawing.Size(100, 23)
        Me.btnChangerNom.TabIndex = 1
        Me.btnChangerNom.Text = "Changer le nom"
        Me.btnChangerNom.UseVisualStyleBackColor = True
        '
        'frmModifierProfil
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(322, 158)
        Me.Controls.Add(Me.btnChangerNom)
        Me.Controls.Add(Me.btnFermer)
        Me.Controls.Add(Me.btnDeleteProfile)
        Me.Controls.Add(Me.lblTitreModifierProfil)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmModifierProfil"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Nano Bot - Modifier le Profil"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitreModifierProfil As System.Windows.Forms.Label
    Friend WithEvents btnDeleteProfile As System.Windows.Forms.Button
    Friend WithEvents btnFermer As System.Windows.Forms.Button
    Friend WithEvents btnChangerNom As System.Windows.Forms.Button
End Class
