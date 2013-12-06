<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmJeu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmJeu))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnPause = New System.Windows.Forms.Button()
        Me.tmrStartLevel = New System.Windows.Forms.Timer(Me.components)
        Me.lblVit = New System.Windows.Forms.Label()
        Me.lblScore = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Default
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(284, 158)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'btnPause
        '
        Me.btnPause.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnPause.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnPause.Location = New System.Drawing.Point(12, 12)
        Me.btnPause.Name = "btnPause"
        Me.btnPause.Size = New System.Drawing.Size(75, 23)
        Me.btnPause.TabIndex = 1
        Me.btnPause.Text = "&Pause"
        Me.btnPause.UseVisualStyleBackColor = True
        '
        'tmrStartLevel
        '
        Me.tmrStartLevel.Enabled = True
        Me.tmrStartLevel.Interval = 1
        '
        'lblVit
        '
        Me.lblVit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVit.AutoSize = True
        Me.lblVit.ForeColor = System.Drawing.Color.White
        Me.lblVit.Location = New System.Drawing.Point(613, 412)
        Me.lblVit.Name = "lblVit"
        Me.lblVit.Size = New System.Drawing.Size(98, 13)
        Me.lblVit.TabIndex = 7
        Me.lblVit.Text = "Vitesse :  0 nm/sec"
        '
        'lblScore
        '
        Me.lblScore.AutoSize = True
        Me.lblScore.ForeColor = System.Drawing.Color.White
        Me.lblScore.Location = New System.Drawing.Point(12, 38)
        Me.lblScore.Name = "lblScore"
        Me.lblScore.Size = New System.Drawing.Size(44, 13)
        Me.lblScore.TabIndex = 8
        Me.lblScore.Text = "Score : "
        '
        'frmJeu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(724, 434)
        Me.Controls.Add(Me.lblScore)
        Me.Controls.Add(Me.lblVit)
        Me.Controls.Add(Me.btnPause)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmJeu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Niveau 1"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents btnPause As System.Windows.Forms.Button
    Friend WithEvents tmrStartLevel As System.Windows.Forms.Timer
    Friend WithEvents lblVit As System.Windows.Forms.Label
    Friend WithEvents lblScore As System.Windows.Forms.Label

End Class
