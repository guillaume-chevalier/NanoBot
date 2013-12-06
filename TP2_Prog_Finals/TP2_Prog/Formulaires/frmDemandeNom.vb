Public Class frmDemandeNom

    Private Sub frmDemandeNom_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Selon le contexte d'appelle du formulaire on change le nom du label et du titre du formulaire.
        Select Case My.Settings.calledFrom
            Case "Supprimer Profil"
                lblNom.Text = "Nouveau Profil:"
                Me.Text = "Nano Bot - Nouveau Profil"
            Case "Changer Nom"
                lblNom.Text = "Nouveau Nom:"
                Me.Text = "Nano Bot - Changement de Nom"
                txtNom.Text = My.Settings.nomJoueur
                txtNom.Select()
            Case Else
                lblNom.Text = "Quel est votre nom?"
                Me.Text = "Nano Bot - Votre Nom"
        End Select
    End Sub

    Private Sub frmDemandeNom_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        My.Settings.calledFrom = "" 'Lorsque le formulaire est fermé on remet à 0 la variable calledFrom, afin qu'on puisse la réutiliser dans le future.
        If lblErreur.Visible = True Then
            lblErreur.Visible = False
        End If
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If txtNom.Text.Trim(" "c).Length < 3 Or txtNom.Text.Trim(" "c).Length > 10 Then
            lblErreur.Visible = True 'On affiche un erreur si l'utilisateur entre un nom trop petits ou trop long.
        ElseIf My.Settings.calledFrom = "Supprimer Profil" Then 'Dans le cas où on veut supprimer le profil, certaines choses particulières doivent être faites.
            My.Settings.Reset()
            ChoixNiveauxUpsRes.lblScoreTab2.Text = My.Settings.score.ToString
            My.Settings.nomJoueur = txtNom.Text.Trim(" "c)
            ChoixNiveauxUpsRes.lblNomModifiable.Text = My.Settings.nomJoueur
            MessageBox.Show("L'ancient profil a été écrasé et le profil " & My.Settings.nomJoueur & " vient d'être créé.", "Écrasement du profil", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
            ChoixNiveauxUpsRes.reloadInfo()
            Me.Close()
        Else
            My.Settings.nomJoueur = txtNom.Text.Trim(" "c)
            ChoixNiveauxUpsRes.lblNomModifiable.Text = My.Settings.nomJoueur
            Me.Close()
        End If
    End Sub

End Class