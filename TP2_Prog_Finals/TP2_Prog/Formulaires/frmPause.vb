Public Class frmPause

    Private Sub frmPause_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Le Form agira comme un dialog et retournera une valeur. 
        btnContinue.DialogResult = Windows.Forms.DialogResult.OK
        btnMenu.DialogResult = Windows.Forms.DialogResult.Abort
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btnContinue.Click
        Return 'Retourne Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnOptions_Click(sender As System.Object, e As System.EventArgs) Handles btnOptions.Click
        MenuOptions.ShowDialog()
    End Sub

    Private Sub btnMenu_Click(sender As System.Object, e As System.EventArgs) Handles btnMenu.Click
        Return 'Retourne Windows.Forms.DialogResult.Abort
    End Sub
End Class