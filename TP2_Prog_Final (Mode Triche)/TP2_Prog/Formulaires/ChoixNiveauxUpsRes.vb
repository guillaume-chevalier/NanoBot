Imports System.IO

Public Class ChoixNiveauxUpsRes

    Private Sub ChoixNiveauxUpsRes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'On met dans une variable la cible du dossier où le programme réside
        Dim path As String = Directory.GetCurrentDirectory()
        'On va chercher tous les noms de fichiers .lnfo (Level Information) dans le dossier levels et on met ça dans un tableau
        Dim filelist() As String = Directory.GetFiles(Directory.GetCurrentDirectory & "\levels\", "*.lnfo")

        'On boucle dans les noms de fichiers...
        For Each element As String In filelist
            '...On vient lire le contenu  du fichier
            Dim fileContent() As String = File.ReadAllLines(element)
            '...et on ajoute les noms dans la liste lstLevels. La première du fichier c'est le nom du niveau.
            lstLevels.Items.Add(fileContent(0))
        Next 'On fait ça pour tous les niveaux.

        'Si il n'y a pas de nom de joueur d'enregistré, on commence le timer. Le timer s'occupera d'afficher frmDemandeNom avec un léger délais, afin que le formulaire ait le focus lorsqu'il apparait.
        If My.Settings.nomJoueur = "" Then tmrDelayDialog.Start()

        lblNomModifiable.Text = My.Settings.nomJoueur

        'Permet d'éviter l'erreur d'execution où l'utilisateur clique sur la liste sans sélectionner d'élément
        lstLevels.SelectedIndex = My.Settings.selectedLevel

        lblScoreTab2.Text = Format(My.Settings.score, "###,###")

        Dim tempsJeu As TimeSpan = TimeSpan.FromSeconds(My.Settings.totalTempsJeu)

        If My.Settings.nbRoues = 1 Then
            opt2Roues.Select()
        ElseIf My.Settings.nbRoues = 2 Then
            opt3Roues.Enabled = True
        End If

        lblTempsJeux.Text = "Temps total joué: " & _
            tempsJeu.Hours.ToString.PadLeft(2, "0"c) & ":" & _
            tempsJeu.Minutes.ToString.PadLeft(2, "0"c) & ":" & _
            tempsJeu.Seconds.ToString.PadLeft(2, "0"c)

        reloadInfo()

        lstLevels.Select()
    End Sub

#Region "Comportement des contrôles"


    Private Sub btnQuitter_Click(sender As Object, e As EventArgs) Handles btnQuitter.Click
        Me.Close()
    End Sub

    Private Sub ChoixNiveauxUpsRes_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub lstLevels_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstLevels.SelectedIndexChanged
        Dim path As String = Directory.GetCurrentDirectory()
        Dim levelFileName() As String = Directory.GetFiles(Directory.GetCurrentDirectory & "\levels\", "level" & lstLevels.SelectedIndex & ".lnfo")
        Dim levelImagePath() As String = Directory.GetFiles(Directory.GetCurrentDirectory & "\levels\", "level" & lstLevels.SelectedIndex & ".jpg")
        Dim levelInfoContent() As String = File.ReadAllLines(levelFileName(0))
        viewLevelInfo(levelInfoContent, levelImagePath(0))
    End Sub

    Private Sub btnOpenOptions_Click(sender As Object, e As EventArgs) Handles btnOpenOptions.Click
        MenuOptions.ShowDialog()
    End Sub

    Private Sub btnPlay_Click(sender As Object, e As EventArgs) Handles btnPlay.Click
        If lstLevels.SelectedIndex = -1 Then
            MessageBox.Show("Vous devez choisir un niveau", "patate", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            My.Settings.selectedLevel = lstLevels.SelectedIndex
            My.Settings.Save()
            Me.ShowInTaskbar = False 'On cache le formulaire ChoixNiveauxUpsRes de la barre des tâches lorsqu'on appelle le formJeu
            tmrTotalTempsJeu.Start()
            frmJeu.ShowDialog()
            tmrTotalTempsJeu.Stop()
            reloadInfo()
            My.Settings.Save()

            Me.ShowInTaskbar = True 'On refais apparaître le formulaire ChoixNiveauxUpsRes dans la barre des tâches lorsqu'on y revient.
        End If
    End Sub

    Private Sub btnCommentJouer_Click(sender As Object, e As EventArgs) Handles btnCommentJouer.Click
        CommentJouer.ShowDialog()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNomModifiable.LinkClicked
        frmModifierProfil.ShowDialog()
    End Sub

    Private Sub opt2Roues_CheckedChanged(sender As Object, e As EventArgs) Handles opt2Roues.CheckedChanged, opt3Roues.CheckedChanged
        If opt2Roues.Checked = True Then
            My.Settings.nbRoues = 1
        ElseIf opt3Roues.Checked = True Then
            My.Settings.nbRoues = 2
        End If
    End Sub

    Private Sub lstUpgrades_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAcceleration.SelectedIndexChanged, lstVitesseMax.SelectedIndexChanged, lstForceRotation.SelectedIndexChanged, lstTypeBatterie.SelectedIndexChanged
        'Dans ce sub on regarde si l'élément que l'utilisateur sélectionne dans une liste a été débloqué, s'il ne l'est pas on lui en empêche.
        'On s'occupe aussi dans ce sub de permettre au joueur de choisir une ancienne valeure d'upgrade (s'il n'est pas content avec un de ses choix il peut choisir l'upgrade précédent.

        'Vitesse
        If lstVitesseMax.SelectedIndex > My.Settings.lvlVitesseVoiture Then
            lstVitesseMax.SelectedIndex -= 1
        ElseIf lstVitesseMax.SelectedIndex <> My.Settings.lvlVitesseVoiture Then
            My.Settings.oldLvlVitesse = lstVitesseMax.SelectedIndex
        End If


        'Accélération
        If lstAcceleration.SelectedIndex > My.Settings.lvlAccelerationVoiture Then
            lstAcceleration.SelectedIndex -= 1
        ElseIf lstAcceleration.SelectedIndex <> My.Settings.lvlAccelerationVoiture Then
            My.Settings.oldLvlAcceleration = lstAcceleration.SelectedIndex
        End If
        

        'Force de Rotation
        If lstForceRotation.SelectedIndex > My.Settings.lvlForceRotation Then
            lstForceRotation.SelectedIndex -= 1
        ElseIf lstForceRotation.SelectedIndex <> My.Settings.lvlForceRotation Then
            My.Settings.oldLvlForceRotation = lstForceRotation.SelectedIndex
        End If
        
        'Type de batterie
        If lstTypeBatterie.SelectedIndex > My.Settings.lvlBatteriePower Then
            lstTypeBatterie.SelectedIndex -= 1
        ElseIf lstTypeBatterie.SelectedIndex <> My.Settings.lvlBatteriePower Then
            My.Settings.oldLvlBatteriePower = lstTypeBatterie.SelectedIndex
        End If
        My.Settings.oldLvlVitesse = lstVitesseMax.SelectedIndex
        My.Settings.oldLvlAcceleration = lstAcceleration.SelectedIndex
        My.Settings.oldLvlForceRotation = lstForceRotation.SelectedIndex
        My.Settings.oldLvlBatteriePower = lstTypeBatterie.SelectedIndex

    End Sub

#End Region

#Region "Timers"

    Private Sub tmrDelayDialog_Tick(sender As Object, e As EventArgs) Handles tmrDelayDialog.Tick
        If My.Settings.nomJoueur = "" Then frmDemandeNom.Show()
        tmrDelayDialog.Stop()
    End Sub

    Private Sub tmrTotalTempsJeu_Tick(sender As Object, e As EventArgs) Handles tmrTotalTempsJeu.Tick
        My.Settings.totalTempsJeu += 1 'Total number of seconds
    End Sub
#End Region

#Region "Les Améliorations"
'Les boutons upgrade s'occupent d'augmenter le niveau de certains aspects de la voiture.
'On ne peut acheter une amélioration si on a pas un score assez élevé.
    Private Sub btnVitesseUpgrade_Click(sender As Object, e As EventArgs) Handles btnVitesseUpgrade.Click
        If lstVitesseMax.SelectedIndex < 4 Then
            If My.Settings.score >= itemPrice(lstVitesseMax.SelectedIndex + 1) Then
                My.Settings.score -= itemPrice(lstVitesseMax.SelectedIndex + 1)
                My.Settings.lvlVitesseVoiture += 1
                lstVitesseMax.SelectedIndex += 1
                reloadInfo()
                lblScoreTab2.Text = Format(My.Settings.score, "###,###")
            End If
        End If
    End Sub

    Private Sub btnAccelerationUpgrade_Click(sender As Object, e As EventArgs) Handles btnAccelerationUpgrade.Click

        If lstAcceleration.SelectedIndex < 4 Then
            If My.Settings.score >= itemPrice(lstAcceleration.SelectedIndex + 1) Then
                My.Settings.score -= itemPrice(lstAcceleration.SelectedIndex + 1)
                My.Settings.lvlAccelerationVoiture += 1
                lstAcceleration.SelectedIndex += 1
                reloadInfo()
                lblScoreTab2.Text = Format(My.Settings.score, "###,###")
            End If
        End If
    End Sub

    Private Sub btnForceRotationUpgrade_Click(sender As Object, e As EventArgs) Handles btnForceRotationUpgrade.Click
        If lstForceRotation.SelectedIndex < 4 Then
            If My.Settings.score >= itemPrice(lstForceRotation.SelectedIndex + 1) Then
                My.Settings.score -= itemPrice(lstForceRotation.SelectedIndex + 1)
                My.Settings.lvlForceRotation += 1
                lstForceRotation.SelectedIndex += 1
                reloadInfo()
                lblScoreTab2.Text = Format(My.Settings.score, "###,###")
            End If
        End If
    End Sub

    Private Sub btnBatterieUpgrade_Click(sender As Object, e As EventArgs) Handles btnBatterieUpgrade.Click
        If lstTypeBatterie.SelectedIndex < 4 Then
            If My.Settings.score >= itemPrice(lstTypeBatterie.SelectedIndex + 1) Then
                My.Settings.score -= itemPrice(lstTypeBatterie.SelectedIndex + 1)
                My.Settings.lvlBatteriePower += 1
                lstTypeBatterie.SelectedIndex += 1
                reloadInfo()
                lblScoreTab2.Text = Format(My.Settings.score, "###,###")
            End If
        End If
    End Sub

    Private Function itemPrice(ByVal level As Integer) As Double
        Dim price As Double
        price = 1000 * Math.Pow(1 * level, 2)
        Return price
    End Function

#End Region

#Region "Tout ce qui concerne le Draw des éléments des listes"

    Private Sub lstVitesse_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstVitesseMax.DrawItem
        Dim drawGray As Boolean = False
        If e.Index > My.Settings.lvlVitesseVoiture Then drawGray = True

        DrawAllLst(sender, e, drawGray, "lstVitesseMax")
    End Sub

    Private Sub lstAcceleration_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstAcceleration.DrawItem
        Dim drawGray As Boolean = False
        If e.Index > My.Settings.lvlAccelerationVoiture Then drawGray = True

        DrawAllLst(sender, e, drawGray, "lstAcceleration")
    End Sub

    Private Sub lstForceRotation_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstForceRotation.DrawItem
        Dim drawGray As Boolean = False
        If e.Index > My.Settings.lvlForceRotation Then drawGray = True

        DrawAllLst(sender, e, drawGray, "lstForceRotation")
    End Sub

    Private Sub lstTypeBatterie_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstTypeBatterie.DrawItem
        Dim drawGray As Boolean = False
        If e.Index > My.Settings.lvlBatteriePower Then drawGray = True

        DrawAllLst(sender, e, drawGray, "lstTypeBatterie")
    End Sub
	
	'Ici on prend en charge le dessinage des listes, puisque ça nous permet plus de flexibilité, tel que mettre en gris les améliorations pas encore
    'débloqués.
    Private Sub DrawAllLst(sender As Object, e As DrawItemEventArgs, ByVal drawGray As Boolean, ByVal liste As String)
   
        ' On dessine l'arrière plan du listbox pour chaque item du listbox
        e.DrawBackground()

        'Ce qui va permettre de jouer avec les couleurs
        Dim brush As Brush

        'Si l'item est de plus haut niveau que celui du joueur... 
        If drawGray Then
            brush = Brushes.Gray '... L'item devient gris.
        Else
            brush = Brushes.Black '...Sinon on le draw normalement
        End If
		
		'Selon la liste qui appelle le sub lDrawAllLst (donc qui est en train de se faire dessiner), on transfères les paramètes du sub à la bonne liste.
        Select Case liste
            Case "lstVitesseMax"
                e.Graphics.DrawString(lstVitesseMax.Items(e.Index).ToString, e.Font, brush, New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))

            Case "lstAcceleration"
                e.Graphics.DrawString(lstAcceleration.Items(e.Index).ToString, e.Font, brush, New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))

            Case "lstForceRotation"
                e.Graphics.DrawString(lstForceRotation.Items(e.Index).ToString, e.Font, brush, New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))

            Case "lstTypeBatterie"
                e.Graphics.DrawString(lstTypeBatterie.Items(e.Index).ToString, e.Font, brush, New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))

        End Select
		
		'Puisqu'on dessine manuellement les listes, il faut aussi manuellement indiquer qu'est-ce qu'il faut faire lorsque le joueur sélectionne un upgrade dans la liste.
        e.DrawFocusRectangle() 'On dessine le rectangle bleu

        My.Settings.oldLvlVitesse = lstVitesseMax.SelectedIndex
        My.Settings.oldLvlAcceleration = lstAcceleration.SelectedIndex
        My.Settings.oldLvlForceRotation = lstForceRotation.SelectedIndex
        My.Settings.oldLvlBatteriePower = lstTypeBatterie.SelectedIndex


    End Sub

#End Region

#Region "Subs non reliés à des contrôles"
	
	'Sub qui est appelé à chaque fois qu'on change le SelectedIndex de la liste lstLevels. On s'occupe à prendre les informations d'un niveau à partir d'un fichier text .lnfo (level info) et on les affiches le formulaire.
    Private Sub viewLevelInfo(ByVal selectedLevelText() As String, ByVal selectedLevelImage As String)
        Dim title As String = selectedLevelText(0)
        Dim description As String = selectedLevelText(1)

        lblLevelTitle.Text = title
        txtLevelDescription.Text = description

        picLevelPreview.ImageLocation = selectedLevelImage

        If My.Settings.totalScore >= 2 * itemPrice(lstLevels.SelectedIndex) Then 'If itemPrice(lstLevels.SelectedIndex) >= My.Settings.totalScore Then
            btnPlay.Enabled = True
            lblScoreTotNeeded.Text = ""
        Else
            btnPlay.Enabled = False
            lblScoreTotNeeded.Text = "Score total nécessaire: " & Format(2 * itemPrice(lstLevels.SelectedIndex), "###,###")
        End If

    End Sub

	'À chaque fois qu'on change un setting, ou du texte, bref presque n'importe quoi, on appelle le sub reloadInfo afin de faire une mise à jour des différents éléments du formulaire.
    Public Sub reloadInfo()
        'On sélectionne les items dans les listbox
        lstVitesseMax.SelectedIndex = My.Settings.lvlVitesseVoiture
        lstAcceleration.SelectedIndex = My.Settings.lvlAccelerationVoiture
        lstForceRotation.SelectedIndex = My.Settings.lvlForceRotation
        lstTypeBatterie.SelectedIndex = My.Settings.lvlBatteriePower


        'On modifie les labels
        lblVitesseMax.Text = "Vitesse Maximale (" & My.Settings.lvlVitesseVoiture & "/5)"
        lblAcceleration.Text = "Accéleration (" & My.Settings.lvlAccelerationVoiture & "/5)"
        lblForceRotation.Text = "Force de Rotation (" & My.Settings.lvlForceRotation & "/5)"
        lblBatterie.Text = "Batterie (" & My.Settings.lvlBatteriePower & "/5)"
        lblScoreTab2.Text = Format(My.Settings.score, "###,###")
        lblScore.Text = "Score: " & Format(My.Settings.score, "###,###")
        lblTotalScore.Text = "Score total depuis le début: " & Format(My.Settings.totalScore, "###,###")

        Dim tempsJeu As TimeSpan = TimeSpan.FromSeconds(My.Settings.totalTempsJeu)
	
		'On format le temps du jeu
        lblTempsJeux.Text = "Temps total joué: " & _
            tempsJeu.Hours.ToString.PadLeft(2, "0"c) & ":" & _
            tempsJeu.Minutes.ToString.PadLeft(2, "0"c) & ":" & _
            tempsJeu.Seconds.ToString.PadLeft(2, "0"c)

		'On s'occupe de rendre les boutons upgrades désactivés et leur texte changé lorsqu'on atteint le level maximal de cette amélioration
        If My.Settings.lvlVitesseVoiture = 4 Then
            btnVitesseUpgrade.Text = "Améliorer (MAX)"
            btnVitesseUpgrade.Enabled = False
        Else
            btnVitesseUpgrade.Text = "Améliorer (" & Format(itemPrice(lstVitesseMax.SelectedIndex + 1), "###,###") & ")"
        End If

        If My.Settings.lvlAccelerationVoiture = 4 Then
            btnAccelerationUpgrade.Text = "Améliorer (MAX)"
            btnAccelerationUpgrade.Enabled = False
        Else
            btnAccelerationUpgrade.Text = "Améliorer (" & Format(itemPrice(lstAcceleration.SelectedIndex + 1), "###,###") & ")"
        End If

        If My.Settings.lvlForceRotation = 4 Then
            btnForceRotationUpgrade.Text = "Améliorer (MAX)"
            btnForceRotationUpgrade.Enabled = False
        Else
            btnForceRotationUpgrade.Text = "Améliorer (" & Format(itemPrice(lstForceRotation.SelectedIndex + 1), "###,###") & ")"
        End If

        If My.Settings.lvlBatteriePower = 4 Then
            btnBatterieUpgrade.Text = "Améliorer (MAX)"
            btnBatterieUpgrade.Enabled = False
        Else
            btnBatterieUpgrade.Text = "Améliorer (" & Format(itemPrice(lstTypeBatterie.SelectedIndex + 1), "###,###") & ")"
        End If



    End Sub

#End Region


End Class