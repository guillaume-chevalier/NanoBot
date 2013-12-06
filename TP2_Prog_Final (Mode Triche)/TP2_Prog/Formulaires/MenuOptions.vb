Public Class MenuOptions
    Private resolutionChange As Boolean

    '#########################

    Private Sub MenuOption_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.TopMost = True 'Le menu option sera toujours au premier plan
        My.Settings.graphicSettingsChanged = True
        chkFullScreen.Checked = My.Settings.fullscreen
        resolutionChange = False
        cboResolution.SelectedIndex = cboResolution.FindString(My.Settings.screenWidth & "x" & My.Settings.screenHeight)

        Select Case My.Settings.graphicQuality
            Case 0
                optGraphicsFast.Checked = True
            Case 1
                optGraphicsNormal.Checked = True
        End Select

        chkMontrerLesCollisions.Checked = My.Settings.showCollisions

    End Sub

    Private Sub MenuOptions_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim screenResolution As New List(Of String)

        'Puisque le dropdown retourne une chaine de charactère, on doit splitter la résolution et la mettre dans une liste.
        screenResolution = cboResolution.SelectedItem.ToString.Split(CChar("x")).ToList

        'Si la résolution a changé, resolutionChange = True
        If My.Settings.screenWidth = CInt(screenResolution(0)) Then 'Or chkFullScreen.Checked <> My.Settings.fullscreen
            resolutionChange = False
        Else
            resolutionChange = True
        End If

        If chkFullScreen.Checked <> My.Settings.fullscreen Then
            resolutionChange = True
        End If

        'On garde en mémoire les nouvelles valeures.
        Dim newWidth As Integer = CInt(screenResolution(0))
        Dim newHeight As Integer = CInt(screenResolution(1))

        If resolutionChange Then
            If chkFullScreen.Checked = True Then
                My.Settings.fullscreen = True
            Else
                My.Settings.fullscreen = False
            End If
        End If

        If resolutionChange = True Then 'And cboResolution.Enabled = True
            MessageBox.Show("Les changements d'affichages ont été affectés.", "Changements Affectés", MessageBoxButtons.OK, MessageBoxIcon.Information)
            My.Settings.screenWidth = newWidth
            My.Settings.screenHeight = newHeight
        End If

        'On sauvegarde l'option coché
        If optGraphicsFast.Checked Then
            My.Settings.graphicQuality = 0
        End If

        If optGraphicsNormal.Checked = True Then
            My.Settings.graphicQuality = 1
        End If

        My.Settings.Save()

    End Sub

    '#########################

    '#########################
    Private Sub btnRetour_Click(sender As Object, e As EventArgs) Handles btnFermerOptions.Click
        Me.Dispose()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        optGraphicsFast.Checked = True
        chkFullScreen.Checked = False
        cboResolution.SelectedIndex = 1
    End Sub

    '#########################

    Private Sub chkFullScreen_CheckedChanged(sender As Object, e As EventArgs) Handles chkFullScreen.CheckedChanged
        If chkFullScreen.Checked = True Then
            cboResolution.Enabled = False
        Else
            cboResolution.Enabled = True
        End If

        If chkFullScreen.Checked <> My.Settings.fullscreen Then
            resolutionChange = True
        End If
    End Sub

    '#########################

    Private Sub chkMontrerLesCollisions_CheckedChanged(sender As Object, e As EventArgs) Handles chkMontrerLesCollisions.CheckedChanged
        My.Settings.showCollisions = chkMontrerLesCollisions.Checked
    End Sub
End Class