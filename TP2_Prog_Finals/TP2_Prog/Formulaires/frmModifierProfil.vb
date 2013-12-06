Public Class frmModifierProfil

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnDeleteProfile.Click
        Dim dialogAnswer As DialogResult = MessageBox.Show("Ceci va supprimer le profil courant (niveaux et mise à niveaux débloqués, score, etc.), souhaitez-vous tout de même poursuivre?", "Écrasement du profil", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)
        If dialogAnswer = Windows.Forms.DialogResult.Yes Then
            My.Settings.calledFrom = "Supprimer Profil"
            frmDemandeNom.ShowDialog()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnFermer.Click
        Me.Close()
    End Sub

    'Private Sub frmModifierProfil_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    If My.Settings.debugOn = True Then
    '        btnDebug.Visible = True
    '    Else
    '        btnDebug.Visible = False
    '    End If
    'End Sub

    Private Sub btnChangerNom_Click(sender As Object, e As EventArgs) Handles btnChangerNom.Click
        My.Settings.calledFrom = "Changer Nom"
        frmDemandeNom.ShowDialog()
    End Sub
End Class