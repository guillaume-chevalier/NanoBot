<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MenuOptions
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
        Me.components = New System.ComponentModel.Container()
        Me.lblResolution = New System.Windows.Forms.Label()
        Me.btnFermerOptions = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.cboResolution = New System.Windows.Forms.ComboBox()
        Me.chkFullScreen = New System.Windows.Forms.CheckBox()
        Me.lblInfoResolution = New System.Windows.Forms.Label()
        Me.grbAffichage = New System.Windows.Forms.GroupBox()
        Me.grbQualiteGraphiques = New System.Windows.Forms.GroupBox()
        Me.optGraphicsNormal = New System.Windows.Forms.RadioButton()
        Me.optGraphicsFast = New System.Windows.Forms.RadioButton()
        Me.tipResetDebug = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkMontrerLesCollisions = New System.Windows.Forms.CheckBox()
        Me.grbAffichage.SuspendLayout()
        Me.grbQualiteGraphiques.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblResolution
        '
        Me.lblResolution.AutoSize = True
        Me.lblResolution.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResolution.Location = New System.Drawing.Point(8, 21)
        Me.lblResolution.Name = "lblResolution"
        Me.lblResolution.Size = New System.Drawing.Size(72, 16)
        Me.lblResolution.TabIndex = 1
        Me.lblResolution.Text = "Résolution"
        '
        'btnFermerOptions
        '
        Me.btnFermerOptions.Location = New System.Drawing.Point(271, 114)
        Me.btnFermerOptions.Name = "btnFermerOptions"
        Me.btnFermerOptions.Size = New System.Drawing.Size(127, 43)
        Me.btnFermerOptions.TabIndex = 4
        Me.btnFermerOptions.Text = "Fermer"
        Me.btnFermerOptions.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(171, 131)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(94, 27)
        Me.btnReset.TabIndex = 1
        Me.btnReset.Text = "Défaut"
        Me.tipResetDebug.SetToolTip(Me.btnReset, "Remet les paramètres par défaut")
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'cboResolution
        '
        Me.cboResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboResolution.Items.AddRange(New Object() {"640x480", "900x600", "1024x768", "1280x1024", "1366x768", "1600x900", "1680x1050", "1920x1080"})
        Me.cboResolution.Location = New System.Drawing.Point(86, 16)
        Me.cboResolution.Name = "cboResolution"
        Me.cboResolution.Size = New System.Drawing.Size(139, 21)
        Me.cboResolution.TabIndex = 0
        '
        'chkFullScreen
        '
        Me.chkFullScreen.AutoSize = True
        Me.chkFullScreen.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFullScreen.Location = New System.Drawing.Point(11, 81)
        Me.chkFullScreen.Name = "chkFullScreen"
        Me.chkFullScreen.Size = New System.Drawing.Size(95, 20)
        Me.chkFullScreen.TabIndex = 1
        Me.chkFullScreen.Text = "Plein Écran"
        Me.chkFullScreen.UseVisualStyleBackColor = True
        '
        'lblInfoResolution
        '
        Me.lblInfoResolution.AutoSize = True
        Me.lblInfoResolution.Location = New System.Drawing.Point(8, 40)
        Me.lblInfoResolution.Name = "lblInfoResolution"
        Me.lblInfoResolution.Size = New System.Drawing.Size(217, 13)
        Me.lblInfoResolution.TabIndex = 0
        Me.lblInfoResolution.Text = "(La résolution n'affecte que la fenêtre de jeu)"
        '
        'grbAffichage
        '
        Me.grbAffichage.Controls.Add(Me.lblResolution)
        Me.grbAffichage.Controls.Add(Me.lblInfoResolution)
        Me.grbAffichage.Controls.Add(Me.cboResolution)
        Me.grbAffichage.Controls.Add(Me.chkFullScreen)
        Me.grbAffichage.Location = New System.Drawing.Point(12, 7)
        Me.grbAffichage.Name = "grbAffichage"
        Me.grbAffichage.Size = New System.Drawing.Size(241, 118)
        Me.grbAffichage.TabIndex = 0
        Me.grbAffichage.TabStop = False
        Me.grbAffichage.Text = "Affichage"
        '
        'grbQualiteGraphiques
        '
        Me.grbQualiteGraphiques.Controls.Add(Me.optGraphicsNormal)
        Me.grbQualiteGraphiques.Controls.Add(Me.optGraphicsFast)
        Me.grbQualiteGraphiques.Location = New System.Drawing.Point(260, 13)
        Me.grbQualiteGraphiques.Name = "grbQualiteGraphiques"
        Me.grbQualiteGraphiques.Size = New System.Drawing.Size(138, 95)
        Me.grbQualiteGraphiques.TabIndex = 1
        Me.grbQualiteGraphiques.TabStop = False
        Me.grbQualiteGraphiques.Text = "Qualité des graphiques"
        '
        'optGraphicsNormal
        '
        Me.optGraphicsNormal.AutoSize = True
        Me.optGraphicsNormal.Location = New System.Drawing.Point(7, 52)
        Me.optGraphicsNormal.Name = "optGraphicsNormal"
        Me.optGraphicsNormal.Size = New System.Drawing.Size(58, 17)
        Me.optGraphicsNormal.TabIndex = 1
        Me.optGraphicsNormal.Text = "Normal"
        Me.optGraphicsNormal.UseVisualStyleBackColor = True
        '
        'optGraphicsFast
        '
        Me.optGraphicsFast.AutoSize = True
        Me.optGraphicsFast.Checked = True
        Me.optGraphicsFast.Location = New System.Drawing.Point(7, 26)
        Me.optGraphicsFast.Name = "optGraphicsFast"
        Me.optGraphicsFast.Size = New System.Drawing.Size(59, 17)
        Me.optGraphicsFast.TabIndex = 0
        Me.optGraphicsFast.TabStop = True
        Me.optGraphicsFast.Text = "Rapide"
        Me.optGraphicsFast.UseVisualStyleBackColor = True
        '
        'tipResetDebug
        '
        Me.tipResetDebug.AutomaticDelay = 100
        Me.tipResetDebug.AutoPopDelay = 10000
        Me.tipResetDebug.InitialDelay = 100
        Me.tipResetDebug.ReshowDelay = 20
        '
        'chkMontrerLesCollisions
        '
        Me.chkMontrerLesCollisions.AutoSize = True
        Me.chkMontrerLesCollisions.Location = New System.Drawing.Point(12, 137)
        Me.chkMontrerLesCollisions.Name = "chkMontrerLesCollisions"
        Me.chkMontrerLesCollisions.Size = New System.Drawing.Size(123, 17)
        Me.chkMontrerLesCollisions.TabIndex = 0
        Me.chkMontrerLesCollisions.Text = "Montrer les collisions"
        Me.tipResetDebug.SetToolTip(Me.chkMontrerLesCollisions, "1) Permet la visualisation des vecteurs de collisions dans le jeu." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2) Et donne l" & _
        "a possibilité de débloquer tous les niveaux et" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "améliorations (dans le menu prof" & _
        "il) afin de tester le jeu.")
        Me.chkMontrerLesCollisions.UseVisualStyleBackColor = True
        '
        'MenuOptions
        '
        Me.AcceptButton = Me.btnFermerOptions
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 168)
        Me.Controls.Add(Me.chkMontrerLesCollisions)
        Me.Controls.Add(Me.grbQualiteGraphiques)
        Me.Controls.Add(Me.grbAffichage)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.btnFermerOptions)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "MenuOptions"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Nano Bot - Options"
        Me.TopMost = True
        Me.grbAffichage.ResumeLayout(False)
        Me.grbAffichage.PerformLayout()
        Me.grbQualiteGraphiques.ResumeLayout(False)
        Me.grbQualiteGraphiques.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblResolution As System.Windows.Forms.Label
    Friend WithEvents btnFermerOptions As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents cboResolution As System.Windows.Forms.ComboBox
    Friend WithEvents chkFullScreen As System.Windows.Forms.CheckBox
    Friend WithEvents lblInfoResolution As System.Windows.Forms.Label
    Friend WithEvents grbAffichage As System.Windows.Forms.GroupBox
    Friend WithEvents grbQualiteGraphiques As System.Windows.Forms.GroupBox
    Friend WithEvents optGraphicsNormal As System.Windows.Forms.RadioButton
    Friend WithEvents optGraphicsFast As System.Windows.Forms.RadioButton
    Friend WithEvents tipResetDebug As System.Windows.Forms.ToolTip
    Friend WithEvents chkMontrerLesCollisions As System.Windows.Forms.CheckBox
End Class
