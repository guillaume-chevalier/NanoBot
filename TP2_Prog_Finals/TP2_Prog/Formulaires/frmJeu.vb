'Imports System.Threading
'Imports System.Threading.Thread
'Imports System.Threading.Tasks

Imports System.Drawing
Imports System.Threading

Public Class frmJeu

    Private Const _FPS As Single = 30
    Private Const _FRAME_TIME_MS As Single = 1000 / _FPS

    Private level As LevelProcessing 'Le niveau
    Private started As Boolean = False 'Si le niveau est déjà instancié 

    Public ResolutionX As Integer = Screen.PrimaryScreen.Bounds.Width 'Variable du menu option
    Public ResolutionY As Integer = Screen.PrimaryScreen.Bounds.Height 'Variable du menu option

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Niveau " & My.Settings.selectedLevel + 1
        lblScore.Text = Format(My.Settings.score, "###,###")

        'Pour debbuging et testing, mettre tout au max :
        'Dim level As Integer = 4
        'My.Settings.oldLvlAcceleration = level
        'My.Settings.oldLvlBatteriePower = level
        'My.Settings.oldLvlForceRotation = level
        'My.Settings.oldLvlVitesse = level

        'Faisons jouer la musique :
        My.Computer.Audio.Play(Application.StartupPath & "\Resources\Other Echoes - Sunburn.wav", AudioPlayMode.BackgroundLoop)

        'Ajustons la résolution et tout
        GraphicSettingsChanged()

        'Le timer est déjà "Enabled". À titre de démonstration :
        'tmrStartLevel.Start() '(Voir évènement tick... le timer est de 1 seul tick de 1 ms.)
    End Sub
    'Nous devons recourir à un timer, car la boucle infinie de l'objet "level" empêche le Form_Load de fonctionner comme il faut. En fait, si le "level" est instancié au "Load", le focus sur le form actuel est étrangement perdu et il est impossible que l'événement KeyPress soit appelé, malgré que le jeu roule. Vive les boucles infinies. 
    Private Sub tmrStartLevel_Tick(sender As System.Object, e As System.EventArgs) Handles tmrStartLevel.Tick
        tmrStartLevel.Stop()
        started = True
        level = New LevelProcessing(My.Settings.selectedLevel) 'On génère le niveau dans son objet
        level.Start() 'On commence le niveau et la boucle infinie générant les images 30 fois par secondes

        'Cette ligne et le reste du sub n'est lue qu'à la fermeture du jeu et de sa boucle.
        '...

    End Sub



    'Capte les touches pesées et les envoient au niveau. 
    Private Sub MyBase_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, btnPause.KeyDown

        If started Then
            Select Case Chr(e.KeyCode)
                Case "W"c
                    level.SetKeyState_W(True)
                Case "A"c
                    level.SetKeyState_A(True)
                Case "S"c
                    level.SetKeyState_S(True)
                Case "D"c
                    level.SetKeyState_D(True)
                Case ChrW(Keys.Escape)
                    'Escape met le jeu en pause. Aussi, la touche "p" fonctionne, car le texte du btnPause est "&Pause".
                    btnPause_Click(New Object, System.EventArgs.Empty)
            End Select
        End If

    End Sub
    Private Sub MyBase_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp, btnPause.KeyUp

        If started Then
            Select Case Chr(e.KeyCode)
                Case "W"c
                    level.SetKeyState_W(False)
                Case "A"c
                    level.SetKeyState_A(False)
                Case "S"c
                    level.SetKeyState_S(False)
                Case "D"c
                    level.SetKeyState_D(False)
            End Select
        End If

    End Sub








    Private Sub GraphicSettingsChanged()
        If My.Settings.fullscreen Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            Me.WindowState = FormWindowState.Maximized
            PictureBox1.Width = Screen.PrimaryScreen.Bounds.Width
            PictureBox1.Height = Screen.PrimaryScreen.Bounds.Height
        Else
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D
            Me.WindowState = FormWindowState.Normal
            Me.Width = My.Settings.screenWidth
            Me.Height = My.Settings.screenHeight
            PictureBox1.Width = My.Settings.screenWidth - 10
            PictureBox1.Height = My.Settings.screenHeight - 31

            'Replaçons le form au milieu : 
            Me.Location = New Point(CInt((Screen.PrimaryScreen.Bounds.Width - Me.Width) / 2), CInt((Screen.PrimaryScreen.Bounds.Height - Me.Height) / 2))
        End If
        'Le changement à été effectué :
        My.Settings.graphicSettingsChanged = False
    End Sub


    'Ce sub aura pour tâche de dessiner le niveau à chaque instant, selon le "FPS" (images par secondes). Ce sub est appelé par LevelProcessing.Main_Loop() dans la variable "level" qui est private au form actuel et qui est une instance de LevelProcessing. Ouf !

    Public Sub DrawLevel(ByVal wheels As List(Of Point), ByVal vehiculeCenter As Point, ByVal map As List(Of Integer), ByVal speed As Double, ByVal batteryPos As Integer, ByVal batteryLevel As Double, ByVal maxBatteryLevel As Integer)
        'Changer la résolution du form dans le cas où le menu option a été ouvert
        If My.Settings.graphicSettingsChanged Then GraphicSettingsChanged()


        'Les dimensions de la PictureBox actuelle. Stocké dans une variable pour optimiser les performances, le contrôle ne devra pas être toujours utilisé pour avoir les dimensions.
        Dim dimensions As New Point(PictureBox1.Width, PictureBox1.Height)

        'Convertir la position des roueset du centre selon le système de coordonnées de GDI+ en inversant la hauteur en Y
        For wheelNum = 0 To wheels.Count - 1
            wheels.Item(wheelNum) = New Point(wheels.Item(wheelNum).X, dimensions.Y - wheels.Item(wheelNum).Y)
        Next
        vehiculeCenter = New Point(vehiculeCenter.X, dimensions.Y - vehiculeCenter.Y)



        'Création des variables images pour dessiner cette image (frame) :
        Dim bmpOutput As New Bitmap(PictureBox1.Width, PictureBox1.Height)
        Dim gfx As Graphics = Graphics.FromImage(bmpOutput)
        'Régler la qualitée graphique : 
        If My.Settings.graphicQuality > 0 Then
            gfx.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Else
            gfx.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed
        End If

        'Couleurs :
        Static Brush_DarkGray As Brush = New SolidBrush(Color.FromArgb(40, 36, 36))
        Static Pen_LightGray As Pen = New Pen(Color.FromArgb(243, 243, 243))
        Static Pen_DarkBlue As Pen = New Pen(Color.FromArgb(43, 145, 161))
        Static Pen_LightBlue As Pen = New Pen(Color.FromArgb(44, 123, 221))
        Static Pen_MintGreen As Pen = New Pen(Color.FromArgb(117, 255, 162))


        'Arrière-plan noir
        gfx.FillRectangle(Brush_DarkGray, New Rectangle(0, 0, dimensions.X, dimensions.Y))


        'Selon la qualitée graphique, on dessine le véhicule et le terrain. 
        If My.Settings.graphicQuality > 0 Then 'Qualitée graphique haute.
            'On dessine chaque roue
            For wheelNum As Integer = 0 To wheels.Count - 1

                'Dessin d'une roue :
                gfx.DrawEllipse(Pen_LightBlue, New Rectangle(wheels.Item(wheelNum).X - LevelProcessing._WHEEL_RADIUS, wheels.Item(wheelNum).Y - LevelProcessing._WHEEL_RADIUS, LevelProcessing._WHEEL_DIAMETER, LevelProcessing._WHEEL_DIAMETER))

                'Dessin de la tige de la roue 
                gfx.DrawLine(Pen_DarkBlue, wheels.Item(wheelNum), vehiculeCenter)


                'Vecteurs collision:
                If My.Settings.showCollisions Then
                    gfx.DrawLine(Pen_MintGreen, wheels.Item(wheelNum), New Point(wheels.Item(wheelNum).X + level.coll(wheelNum).X, wheels.Item(wheelNum).Y + level.coll(wheelNum).Y))
                End If
            Next

            'Terrain du niveau
            For x_i As Integer = 0 To map.Count - 6 'Ici, le décalage est de 6 (0 à 5) afin de créer un effet de largeur. 
                'Dessin d'un point du terrain : 
                gfx.DrawLine(Pen_MintGreen, x_i, dimensions.Y - map.Item(x_i), x_i + 5, dimensions.Y - map.Item(x_i + 5))

                'Dessin d'une batterie : 
                If x_i = batteryPos Then
                    gfx.DrawEllipse(Pen_DarkBlue, New Rectangle(x_i - 25, dimensions.Y - map.Item(x_i) - 75, 50, 50))
                End If
            Next

        Else 'Qualitée graphique basse. 

            'On dessine chaque roue
            For wheelNum As Integer = 0 To wheels.Count - 1

                'Dessin de la roue
                Dim pixel_x As Integer
                pixel_x = 0
                For Each pixel_y As Integer In Precomputations.WheelBorders(LevelProcessing._WHEEL_DIAMETER) 'Utilisation du demi-cercle de détection de collision mis en cache pour dessiner les roues. C'est plus rapide d'utiliser ça que d'utiliser la fonction par défaut (.DrawEllipse ou .DrawArc), et ça donne un style. 
                    'Définition du point à dessiner :
                    Dim wheelPixel As New Point(pixel_x + wheels.Item(wheelNum).X - LevelProcessing._WHEEL_RADIUS, pixel_y + wheels.Item(wheelNum).Y)
                    'Vérifions au moins si le pixel fait partie de l'écran, puis dessinons le :
                    If wheelPixel.X > 0 AndAlso wheelPixel.X < dimensions.X AndAlso wheelPixel.Y > 0 AndAlso wheelPixel.Y < dimensions.Y Then bmpOutput.SetPixel(wheelPixel.X, wheelPixel.Y, Pen_LightBlue.Color)
                    pixel_x += 1
                Next

                'Dessin de la tige de la roue 
                gfx.DrawLine(Pen_DarkBlue, wheels.Item(wheelNum), vehiculeCenter)

                'Vecteurs collision:
                If My.Settings.showCollisions Then
                    gfx.DrawLine(Pen_MintGreen, wheels.Item(wheelNum), New Point(wheels.Item(wheelNum).X + level.coll(wheelNum).X, wheels.Item(wheelNum).Y + level.coll(wheelNum).Y))
                End If
            Next

            'Terrain du niveau
            For x_i As Integer = 0 To map.Count - 2
                'Dessin d'un point du terrain : 
                gfx.DrawLine(Pen_MintGreen, x_i, dimensions.Y - map.Item(x_i), x_i + 1, dimensions.Y - map.Item(x_i + 1))

                'Dessin d'une batterie : 
                If x_i = batteryPos Then
                    gfx.DrawEllipse(Pen_DarkBlue, New Rectangle(x_i - 25, dimensions.Y - map.Item(x_i) - 75, 50, 50))
                End If
            Next
        End If


        'Dessin de la barre d'énergie (batteryLevel) :
        Dim spacingX As Integer = 100
        Dim spacingY As Integer = 12
        'Dessiner la ligne en haut :
        gfx.DrawLine(Pen_LightBlue, spacingX, spacingY, spacingX + maxBatteryLevel, spacingY)
        'Dessiner la barre :
        Dim batteryPercent As Integer = CInt(Precomputations.UnderZeroEqualsZero(batteryLevel))
        For y As Integer = spacingY To spacingY + 22
            gfx.DrawLine(Pen_LightBlue, spacingX, y, batteryPercent + spacingX, y)
        Next


        '//////////
        'Envoi de la nouvelle image créée au PictureBox de jeu : 
        PictureBox1.Image = bmpOutput
        '//////////



        'Compteur d'images par secondes (FPS counter)
        Static frameElapsedTime As New Stopwatch

        Static startedStopwatch As Boolean = False
        If Not startedStopwatch Then frameElapsedTime.Start()

        'Ceci a été retiré, mais représente le FPS dans le cas où il ne serait pas limité à 30. Il est donc possible que ça affiche 70, cela signifierait que le FPS pourrait atteindre 70 si pas limité, mais il n'est toutefois pas de 70, mais de 30 max. 
        'Static lastTime1 As Long
        'lblFPS.Text = "FPS : " & (1000 / (frameElapsedTime.ElapsedMilliseconds)).ToString("##.#") & "/30"
        'lastTime1 = frameElapsedTime.ElapsedMilliseconds

        Static remainingToNextFrame As Double
        remainingToNextFrame = LevelProcessing._FRAME_TIME_MS - frameElapsedTime.ElapsedMilliseconds
        remainingToNextFrame = CSng(Precomputations.UnderZeroEqualsZero(remainingToNextFrame))
        Thread.Sleep(CInt(remainingToNextFrame))

        frameElapsedTime.Restart()



        'Affichage de la vitesse : 
        lblVit.Text = "Vitesse : " & CInt(speed) & " nm/sec"

        'Afficher le score : 
        lblScore.Text = "Score : " & Format(My.Settings.score, "###,###")


        'Laisser le temps à l'application de "respirer" (faire ses interruptions et autres événements) au travers de tout ceci :
        'À noter que cette ligne est peut-être superflue.
        Application.DoEvents()

    End Sub




    Private Sub btnPause_Click(sender As System.Object, e As System.EventArgs) Handles btnPause.Click

        level.Pause()

        'Le frmPause agit comme un "Dialog" et retourne une valeur.
        Dim pauseDialogResult As DialogResult = frmPause.ShowDialog()

        If pauseDialogResult = Windows.Forms.DialogResult.Abort Then
            'On ferme le jeu et retourne au menu 
            Me.Close()
        Else ' ElseIf pauseDialogResult = Windows.Forms.DialogResult.OK Then
            'On continue
            level.Unpause()
        End If

    End Sub

    Private Sub frmJeu_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'On évite une erreur causée si le jeu est fermé lorsqu'en fullscreen. 
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D
        Me.WindowState = FormWindowState.Normal

        'On ferme le niveau en terminant la boucle infinie du LevelProcessing, sans cela, il y aurait une erreur lors de l'appelation (à partir de la boucle infinie du LevelProcessing) du DrawLevel de ce form qui viens pourtant de se fermer.
        If started Then level.EndLevel() 'Évitons les erreurs... Le jeu prends 1 tick de timer à ouvrir xD. 

        My.Computer.Audio.Stop() 'Arrêtons la musique. 
        Me.Dispose() 'Tue toutes les variables existantes, évitant les bugs.
    End Sub

End Class
