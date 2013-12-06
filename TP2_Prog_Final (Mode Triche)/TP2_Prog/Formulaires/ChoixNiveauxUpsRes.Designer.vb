<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChoixNiveauxUpsRes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChoixNiveauxUpsRes))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabChoixNiveau = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblNiveaux = New System.Windows.Forms.Label()
        Me.lstLevels = New System.Windows.Forms.ListBox()
        Me.lblScoreTotNeeded = New System.Windows.Forms.Label()
        Me.lblLevelTitle = New System.Windows.Forms.Label()
        Me.picLevelPreview = New System.Windows.Forms.PictureBox()
        Me.txtLevelDescription = New System.Windows.Forms.TextBox()
        Me.tabUpgrades = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.opt3Roues = New System.Windows.Forms.RadioButton()
        Me.opt2Roues = New System.Windows.Forms.RadioButton()
        Me.lblScoreTab2 = New System.Windows.Forms.Label()
        Me.lblTitreAmeliorations = New System.Windows.Forms.Label()
        Me.btnBatterieUpgrade = New System.Windows.Forms.Button()
        Me.btnForceRotationUpgrade = New System.Windows.Forms.Button()
        Me.btnAccelerationUpgrade = New System.Windows.Forms.Button()
        Me.btnVitesseUpgrade = New System.Windows.Forms.Button()
        Me.lblBatterie = New System.Windows.Forms.Label()
        Me.lblForceRotation = New System.Windows.Forms.Label()
        Me.lblAcceleration = New System.Windows.Forms.Label()
        Me.lblVitesseMax = New System.Windows.Forms.Label()
        Me.lstTypeBatterie = New System.Windows.Forms.ListBox()
        Me.lstForceRotation = New System.Windows.Forms.ListBox()
        Me.lstAcceleration = New System.Windows.Forms.ListBox()
        Me.lstVitesseMax = New System.Windows.Forms.ListBox()
        Me.tabStatistiques = New System.Windows.Forms.TabPage()
        Me.lblTempsJeux = New System.Windows.Forms.Label()
        Me.lblTotalScore = New System.Windows.Forms.Label()
        Me.lblScore = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.btnQuitter = New System.Windows.Forms.Button()
        Me.btnPlay = New System.Windows.Forms.Button()
        Me.btnOpenOptions = New System.Windows.Forms.Button()
        Me.btnCommentJouer = New System.Windows.Forms.Button()
        Me.lblNomModifiable = New System.Windows.Forms.LinkLabel()
        Me.tmrDelayDialog = New System.Windows.Forms.Timer(Me.components)
        Me.tmrTotalTempsJeu = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.tabChoixNiveau.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.picLevelPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabUpgrades.SuspendLayout()
        Me.tabStatistiques.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabChoixNiveau)
        Me.TabControl1.Controls.Add(Me.tabUpgrades)
        Me.TabControl1.Controls.Add(Me.tabStatistiques)
        Me.TabControl1.Location = New System.Drawing.Point(9, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(600, 344)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.TabStop = False
        '
        'tabChoixNiveau
        '
        Me.tabChoixNiveau.Controls.Add(Me.SplitContainer1)
        Me.tabChoixNiveau.Location = New System.Drawing.Point(4, 22)
        Me.tabChoixNiveau.Name = "tabChoixNiveau"
        Me.tabChoixNiveau.Padding = New System.Windows.Forms.Padding(3)
        Me.tabChoixNiveau.Size = New System.Drawing.Size(592, 318)
        Me.tabChoixNiveau.TabIndex = 0
        Me.tabChoixNiveau.Text = "Choix du Niveau"
        Me.tabChoixNiveau.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblNiveaux)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstLevels)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblScoreTotNeeded)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblLevelTitle)
        Me.SplitContainer1.Panel2.Controls.Add(Me.picLevelPreview)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtLevelDescription)
        Me.SplitContainer1.Size = New System.Drawing.Size(586, 312)
        Me.SplitContainer1.SplitterDistance = 199
        Me.SplitContainer1.TabIndex = 0
        Me.SplitContainer1.TabStop = False
        '
        'lblNiveaux
        '
        Me.lblNiveaux.AutoSize = True
        Me.lblNiveaux.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNiveaux.Location = New System.Drawing.Point(28, 10)
        Me.lblNiveaux.Name = "lblNiveaux"
        Me.lblNiveaux.Size = New System.Drawing.Size(138, 37)
        Me.lblNiveaux.TabIndex = 0
        Me.lblNiveaux.Text = "Niveaux"
        '
        'lstLevels
        '
        Me.lstLevels.FormattingEnabled = True
        Me.lstLevels.Location = New System.Drawing.Point(14, 67)
        Me.lstLevels.Name = "lstLevels"
        Me.lstLevels.Size = New System.Drawing.Size(172, 186)
        Me.lstLevels.TabIndex = 1
        '
        'lblScoreTotNeeded
        '
        Me.lblScoreTotNeeded.AutoSize = True
        Me.lblScoreTotNeeded.Location = New System.Drawing.Point(22, 181)
        Me.lblScoreTotNeeded.Name = "lblScoreTotNeeded"
        Me.lblScoreTotNeeded.Size = New System.Drawing.Size(118, 13)
        Me.lblScoreTotNeeded.TabIndex = 6
        Me.lblScoreTotNeeded.Text = "Score total nécessaire: "
        '
        'lblLevelTitle
        '
        Me.lblLevelTitle.AutoSize = True
        Me.lblLevelTitle.Location = New System.Drawing.Point(22, 159)
        Me.lblLevelTitle.Name = "lblLevelTitle"
        Me.lblLevelTitle.Size = New System.Drawing.Size(24, 13)
        Me.lblLevelTitle.TabIndex = 0
        Me.lblLevelTitle.Text = "titre"
        '
        'picLevelPreview
        '
        Me.picLevelPreview.Image = CType(resources.GetObject("picLevelPreview.Image"), System.Drawing.Image)
        Me.picLevelPreview.Location = New System.Drawing.Point(25, 10)
        Me.picLevelPreview.Name = "picLevelPreview"
        Me.picLevelPreview.Size = New System.Drawing.Size(330, 146)
        Me.picLevelPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picLevelPreview.TabIndex = 1
        Me.picLevelPreview.TabStop = False
        '
        'txtLevelDescription
        '
        Me.txtLevelDescription.BackColor = System.Drawing.Color.White
        Me.txtLevelDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLevelDescription.Location = New System.Drawing.Point(25, 204)
        Me.txtLevelDescription.Multiline = True
        Me.txtLevelDescription.Name = "txtLevelDescription"
        Me.txtLevelDescription.ReadOnly = True
        Me.txtLevelDescription.Size = New System.Drawing.Size(330, 95)
        Me.txtLevelDescription.TabIndex = 0
        Me.txtLevelDescription.TabStop = False
        '
        'tabUpgrades
        '
        Me.tabUpgrades.Controls.Add(Me.Label1)
        Me.tabUpgrades.Controls.Add(Me.opt3Roues)
        Me.tabUpgrades.Controls.Add(Me.opt2Roues)
        Me.tabUpgrades.Controls.Add(Me.lblScoreTab2)
        Me.tabUpgrades.Controls.Add(Me.lblTitreAmeliorations)
        Me.tabUpgrades.Controls.Add(Me.btnBatterieUpgrade)
        Me.tabUpgrades.Controls.Add(Me.btnForceRotationUpgrade)
        Me.tabUpgrades.Controls.Add(Me.btnAccelerationUpgrade)
        Me.tabUpgrades.Controls.Add(Me.btnVitesseUpgrade)
        Me.tabUpgrades.Controls.Add(Me.lblBatterie)
        Me.tabUpgrades.Controls.Add(Me.lblForceRotation)
        Me.tabUpgrades.Controls.Add(Me.lblAcceleration)
        Me.tabUpgrades.Controls.Add(Me.lblVitesseMax)
        Me.tabUpgrades.Controls.Add(Me.lstTypeBatterie)
        Me.tabUpgrades.Controls.Add(Me.lstForceRotation)
        Me.tabUpgrades.Controls.Add(Me.lstAcceleration)
        Me.tabUpgrades.Controls.Add(Me.lstVitesseMax)
        Me.tabUpgrades.Location = New System.Drawing.Point(4, 22)
        Me.tabUpgrades.Name = "tabUpgrades"
        Me.tabUpgrades.Padding = New System.Windows.Forms.Padding(3)
        Me.tabUpgrades.Size = New System.Drawing.Size(592, 318)
        Me.tabUpgrades.TabIndex = 1
        Me.tabUpgrades.Text = "Améliorations"
        Me.tabUpgrades.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(219, 250)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Nombres de roues du véhicule:"
        '
        'opt3Roues
        '
        Me.opt3Roues.AutoSize = True
        Me.opt3Roues.Location = New System.Drawing.Point(293, 266)
        Me.opt3Roues.Name = "opt3Roues"
        Me.opt3Roues.Size = New System.Drawing.Size(65, 17)
        Me.opt3Roues.TabIndex = 30
        Me.opt3Roues.TabStop = True
        Me.opt3Roues.Text = "3 Roues"
        Me.opt3Roues.UseVisualStyleBackColor = True
        '
        'opt2Roues
        '
        Me.opt2Roues.AutoSize = True
        Me.opt2Roues.Location = New System.Drawing.Point(222, 266)
        Me.opt2Roues.Name = "opt2Roues"
        Me.opt2Roues.Size = New System.Drawing.Size(65, 17)
        Me.opt2Roues.TabIndex = 29
        Me.opt2Roues.TabStop = True
        Me.opt2Roues.Text = "2 Roues"
        Me.opt2Roues.UseVisualStyleBackColor = True
        '
        'lblScoreTab2
        '
        Me.lblScoreTab2.AutoSize = True
        Me.lblScoreTab2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScoreTab2.Location = New System.Drawing.Point(375, 26)
        Me.lblScoreTab2.Name = "lblScoreTab2"
        Me.lblScoreTab2.Size = New System.Drawing.Size(87, 25)
        Me.lblScoreTab2.TabIndex = 28
        Me.lblScoreTab2.Text = "Score: "
        '
        'lblTitreAmeliorations
        '
        Me.lblTitreAmeliorations.AutoSize = True
        Me.lblTitreAmeliorations.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitreAmeliorations.Location = New System.Drawing.Point(20, 17)
        Me.lblTitreAmeliorations.Name = "lblTitreAmeliorations"
        Me.lblTitreAmeliorations.Size = New System.Drawing.Size(225, 37)
        Me.lblTitreAmeliorations.TabIndex = 27
        Me.lblTitreAmeliorations.Text = "Améliorations"
        '
        'btnBatterieUpgrade
        '
        Me.btnBatterieUpgrade.Location = New System.Drawing.Point(445, 99)
        Me.btnBatterieUpgrade.Name = "btnBatterieUpgrade"
        Me.btnBatterieUpgrade.Size = New System.Drawing.Size(137, 23)
        Me.btnBatterieUpgrade.TabIndex = 7
        Me.btnBatterieUpgrade.Text = "Améliorer"
        Me.btnBatterieUpgrade.UseVisualStyleBackColor = True
        '
        'btnForceRotationUpgrade
        '
        Me.btnForceRotationUpgrade.Location = New System.Drawing.Point(300, 99)
        Me.btnForceRotationUpgrade.Name = "btnForceRotationUpgrade"
        Me.btnForceRotationUpgrade.Size = New System.Drawing.Size(137, 23)
        Me.btnForceRotationUpgrade.TabIndex = 5
        Me.btnForceRotationUpgrade.Text = "Améliorer"
        Me.btnForceRotationUpgrade.UseVisualStyleBackColor = True
        '
        'btnAccelerationUpgrade
        '
        Me.btnAccelerationUpgrade.Location = New System.Drawing.Point(153, 99)
        Me.btnAccelerationUpgrade.Name = "btnAccelerationUpgrade"
        Me.btnAccelerationUpgrade.Size = New System.Drawing.Size(137, 23)
        Me.btnAccelerationUpgrade.TabIndex = 3
        Me.btnAccelerationUpgrade.Text = "Améliorer"
        Me.btnAccelerationUpgrade.UseVisualStyleBackColor = True
        '
        'btnVitesseUpgrade
        '
        Me.btnVitesseUpgrade.Location = New System.Drawing.Point(10, 99)
        Me.btnVitesseUpgrade.Name = "btnVitesseUpgrade"
        Me.btnVitesseUpgrade.Size = New System.Drawing.Size(137, 23)
        Me.btnVitesseUpgrade.TabIndex = 1
        Me.btnVitesseUpgrade.Text = "Améliorer"
        Me.btnVitesseUpgrade.UseVisualStyleBackColor = True
        '
        'lblBatterie
        '
        Me.lblBatterie.AutoSize = True
        Me.lblBatterie.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBatterie.Location = New System.Drawing.Point(468, 71)
        Me.lblBatterie.Name = "lblBatterie"
        Me.lblBatterie.Size = New System.Drawing.Size(83, 13)
        Me.lblBatterie.TabIndex = 21
        Me.lblBatterie.Text = "Batterie (1/5)"
        '
        'lblForceRotation
        '
        Me.lblForceRotation.AutoSize = True
        Me.lblForceRotation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblForceRotation.Location = New System.Drawing.Point(297, 71)
        Me.lblForceRotation.Name = "lblForceRotation"
        Me.lblForceRotation.Size = New System.Drawing.Size(141, 13)
        Me.lblForceRotation.TabIndex = 20
        Me.lblForceRotation.Text = "Force de Rotation (1/5)"
        '
        'lblAcceleration
        '
        Me.lblAcceleration.AutoSize = True
        Me.lblAcceleration.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcceleration.Location = New System.Drawing.Point(166, 71)
        Me.lblAcceleration.Name = "lblAcceleration"
        Me.lblAcceleration.Size = New System.Drawing.Size(110, 13)
        Me.lblAcceleration.TabIndex = 19
        Me.lblAcceleration.Text = "Accéleration (1/5)"
        '
        'lblVitesseMax
        '
        Me.lblVitesseMax.AutoSize = True
        Me.lblVitesseMax.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVitesseMax.Location = New System.Drawing.Point(11, 71)
        Me.lblVitesseMax.Name = "lblVitesseMax"
        Me.lblVitesseMax.Size = New System.Drawing.Size(136, 13)
        Me.lblVitesseMax.TabIndex = 18
        Me.lblVitesseMax.Text = "Vitesse Maximale (1/5)"
        '
        'lstTypeBatterie
        '
        Me.lstTypeBatterie.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstTypeBatterie.FormattingEnabled = True
        Me.lstTypeBatterie.Items.AddRange(New Object() {"Batterie usagée", "Batterie de portable", "Batterie de Cellulaire", "Batterie de montre", "Réacteur Nucléaire"})
        Me.lstTypeBatterie.Location = New System.Drawing.Point(445, 128)
        Me.lstTypeBatterie.Name = "lstTypeBatterie"
        Me.lstTypeBatterie.Size = New System.Drawing.Size(137, 95)
        Me.lstTypeBatterie.TabIndex = 6
        '
        'lstForceRotation
        '
        Me.lstForceRotation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstForceRotation.FormattingEnabled = True
        Me.lstForceRotation.Items.AddRange(New Object() {"Grande Roue", "Carrousel", "Mélangeur", "J'ai mal au torse", "Perte de conscience"})
        Me.lstForceRotation.Location = New System.Drawing.Point(300, 128)
        Me.lstForceRotation.Name = "lstForceRotation"
        Me.lstForceRotation.Size = New System.Drawing.Size(137, 95)
        Me.lstForceRotation.TabIndex = 4
        '
        'lstAcceleration
        '
        Me.lstAcceleration.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstAcceleration.FormattingEnabled = True
        Me.lstAcceleration.Items.AddRange(New Object() {"Gros monsieur", "Chien", "Toyota", "Ferrari", "Instantanée"})
        Me.lstAcceleration.Location = New System.Drawing.Point(155, 128)
        Me.lstAcceleration.Name = "lstAcceleration"
        Me.lstAcceleration.Size = New System.Drawing.Size(137, 95)
        Me.lstAcceleration.TabIndex = 2
        '
        'lstVitesseMax
        '
        Me.lstVitesseMax.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.lstVitesseMax.FormattingEnabled = True
        Me.lstVitesseMax.Items.AddRange(New Object() {"Vieille Matante", "Normal", "Formule 1", "Fusée", "Trop rapide"})
        Me.lstVitesseMax.Location = New System.Drawing.Point(10, 128)
        Me.lstVitesseMax.Name = "lstVitesseMax"
        Me.lstVitesseMax.Size = New System.Drawing.Size(137, 95)
        Me.lstVitesseMax.TabIndex = 0
        '
        'tabStatistiques
        '
        Me.tabStatistiques.Controls.Add(Me.lblTempsJeux)
        Me.tabStatistiques.Controls.Add(Me.lblTotalScore)
        Me.tabStatistiques.Controls.Add(Me.lblScore)
        Me.tabStatistiques.Controls.Add(Me.lblTitle)
        Me.tabStatistiques.Location = New System.Drawing.Point(4, 22)
        Me.tabStatistiques.Name = "tabStatistiques"
        Me.tabStatistiques.Padding = New System.Windows.Forms.Padding(3)
        Me.tabStatistiques.Size = New System.Drawing.Size(592, 318)
        Me.tabStatistiques.TabIndex = 2
        Me.tabStatistiques.Text = "Statistiques"
        Me.tabStatistiques.UseVisualStyleBackColor = True
        '
        'lblTempsJeux
        '
        Me.lblTempsJeux.AutoSize = True
        Me.lblTempsJeux.Location = New System.Drawing.Point(21, 154)
        Me.lblTempsJeux.Name = "lblTempsJeux"
        Me.lblTempsJeux.Size = New System.Drawing.Size(88, 13)
        Me.lblTempsJeux.TabIndex = 4
        Me.lblTempsJeux.Text = "Temps total joué:"
        '
        'lblTotalScore
        '
        Me.lblTotalScore.AutoSize = True
        Me.lblTotalScore.Location = New System.Drawing.Point(21, 125)
        Me.lblTotalScore.Name = "lblTotalScore"
        Me.lblTotalScore.Size = New System.Drawing.Size(152, 13)
        Me.lblTotalScore.TabIndex = 3
        Me.lblTotalScore.Text = "Score amassé depuis le début:"
        '
        'lblScore
        '
        Me.lblScore.AutoSize = True
        Me.lblScore.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblScore.Location = New System.Drawing.Point(19, 85)
        Me.lblScore.Name = "lblScore"
        Me.lblScore.Size = New System.Drawing.Size(87, 25)
        Me.lblScore.TabIndex = 2
        Me.lblScore.Text = "Score: "
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(17, 12)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(223, 42)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "Statistiques"
        '
        'btnQuitter
        '
        Me.btnQuitter.Location = New System.Drawing.Point(13, 378)
        Me.btnQuitter.Name = "btnQuitter"
        Me.btnQuitter.Size = New System.Drawing.Size(96, 23)
        Me.btnQuitter.TabIndex = 5
        Me.btnQuitter.Text = "&Quitter"
        Me.btnQuitter.UseVisualStyleBackColor = True
        '
        'btnPlay
        '
        Me.btnPlay.Location = New System.Drawing.Point(530, 378)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(75, 23)
        Me.btnPlay.TabIndex = 1
        Me.btnPlay.Text = "&Jouer"
        Me.btnPlay.UseVisualStyleBackColor = True
        '
        'btnOpenOptions
        '
        Me.btnOpenOptions.Location = New System.Drawing.Point(116, 378)
        Me.btnOpenOptions.Name = "btnOpenOptions"
        Me.btnOpenOptions.Size = New System.Drawing.Size(123, 23)
        Me.btnOpenOptions.TabIndex = 4
        Me.btnOpenOptions.Text = "&Changer les options"
        Me.btnOpenOptions.UseVisualStyleBackColor = True
        '
        'btnCommentJouer
        '
        Me.btnCommentJouer.Location = New System.Drawing.Point(419, 378)
        Me.btnCommentJouer.Name = "btnCommentJouer"
        Me.btnCommentJouer.Size = New System.Drawing.Size(99, 23)
        Me.btnCommentJouer.TabIndex = 2
        Me.btnCommentJouer.Text = "&Comment Jouer?"
        Me.btnCommentJouer.UseVisualStyleBackColor = True
        '
        'lblNomModifiable
        '
        Me.lblNomModifiable.AutoSize = True
        Me.lblNomModifiable.Location = New System.Drawing.Point(289, 383)
        Me.lblNomModifiable.Name = "lblNomModifiable"
        Me.lblNomModifiable.Size = New System.Drawing.Size(0, 13)
        Me.lblNomModifiable.TabIndex = 3
        '
        'tmrDelayDialog
        '
        Me.tmrDelayDialog.Enabled = True
        Me.tmrDelayDialog.Interval = 2000
        '
        'tmrTotalTempsJeu
        '
        Me.tmrTotalTempsJeu.Enabled = True
        Me.tmrTotalTempsJeu.Interval = 1000
        '
        'ChoixNiveauxUpsRes
        '
        Me.AcceptButton = Me.btnPlay
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(619, 413)
        Me.Controls.Add(Me.lblNomModifiable)
        Me.Controls.Add(Me.btnCommentJouer)
        Me.Controls.Add(Me.btnOpenOptions)
        Me.Controls.Add(Me.btnPlay)
        Me.Controls.Add(Me.btnQuitter)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ChoixNiveauxUpsRes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Nano Bot"
        Me.TabControl1.ResumeLayout(False)
        Me.tabChoixNiveau.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.picLevelPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabUpgrades.ResumeLayout(False)
        Me.tabUpgrades.PerformLayout()
        Me.tabStatistiques.ResumeLayout(False)
        Me.tabStatistiques.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabChoixNiveau As System.Windows.Forms.TabPage
    Friend WithEvents tabUpgrades As System.Windows.Forms.TabPage
    Friend WithEvents tabStatistiques As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lstLevels As System.Windows.Forms.ListBox
    Friend WithEvents lblNiveaux As System.Windows.Forms.Label
    Friend WithEvents btnQuitter As System.Windows.Forms.Button
    Friend WithEvents picLevelPreview As System.Windows.Forms.PictureBox
    Friend WithEvents txtLevelDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnPlay As System.Windows.Forms.Button
    Friend WithEvents btnBatterieUpgrade As System.Windows.Forms.Button
    Friend WithEvents btnForceRotationUpgrade As System.Windows.Forms.Button
    Friend WithEvents btnAccelerationUpgrade As System.Windows.Forms.Button
    Friend WithEvents btnVitesseUpgrade As System.Windows.Forms.Button
    Friend WithEvents lblBatterie As System.Windows.Forms.Label
    Friend WithEvents lblForceRotation As System.Windows.Forms.Label
    Friend WithEvents lblAcceleration As System.Windows.Forms.Label
    Friend WithEvents lblVitesseMax As System.Windows.Forms.Label
    Friend WithEvents lstTypeBatterie As System.Windows.Forms.ListBox
    Friend WithEvents lstForceRotation As System.Windows.Forms.ListBox
    Friend WithEvents lstAcceleration As System.Windows.Forms.ListBox
    Friend WithEvents lstVitesseMax As System.Windows.Forms.ListBox
    Friend WithEvents btnOpenOptions As System.Windows.Forms.Button
    Friend WithEvents btnCommentJouer As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblTempsJeux As System.Windows.Forms.Label
    Friend WithEvents lblTotalScore As System.Windows.Forms.Label
    Friend WithEvents lblScore As System.Windows.Forms.Label
    Friend WithEvents lblTitreAmeliorations As System.Windows.Forms.Label
    Friend WithEvents lblScoreTab2 As System.Windows.Forms.Label
    Friend WithEvents lblNomModifiable As System.Windows.Forms.LinkLabel
    Friend WithEvents tmrDelayDialog As System.Windows.Forms.Timer
    Friend WithEvents lblLevelTitle As System.Windows.Forms.Label
    Friend WithEvents tmrTotalTempsJeu As System.Windows.Forms.Timer
    Friend WithEvents opt2Roues As System.Windows.Forms.RadioButton
    Friend WithEvents opt3Roues As System.Windows.Forms.RadioButton
    Friend WithEvents lblScoreTotNeeded As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
