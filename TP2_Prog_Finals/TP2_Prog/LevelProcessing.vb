Imports System.Threading
Imports System.Threading.Tasks

Class LevelProcessing

#Region "Variables et constantes (public et private) et constructeur"

    ''''Quelques constantes et variables
    'Vitesse de rendu
    Public Const _FPS As Integer = 30 'Images par seconde
    Public Const _FRAME_TIME_MS As Single = 1000 / _FPS 'Le temps d'une image, en millisecondes. 
    Private Const _PHYSICS_FRAME_PRECISION As Integer = 6 'En une image de "_FPS", la physique sera calculée "_PHYSICS_FRAME_PRECISION" fois en un seul "frame" (image), donc avant d'être rendu. Ceci augmente énormément la précision de la physique, en subdivisant le nombre de calculs par seconde. 
    'La forme du véhicule
    Private Const _PHYSICS_SPEED As Single = _FPS * _PHYSICS_FRAME_PRECISION 'Représente directement la vitesse à laquelle les calculs de physique seront faits par seconde. 

    Public Const _DIST_BETWEEN_WHEELS As Integer = 180 'Le double de la largeur entre une roue et le centre du véhicule
    Public Const _WHEEL_RADIUS As Integer = 40 'Rayon des roues
    Public Const _WHEEL_DIAMETER As Integer = 2 * _WHEEL_RADIUS 'Diamètre des roues

    'les variables contenant la physique
    Private _vehicule As New Dictionary(Of String, Double) 'Le corps du véhicule
    Private _wheels As New List(Of Dictionary(Of String, Double)) 'Une liste des varaibles de ses 2 roues.
    Public _numberOfWheels_Index As Integer = My.Settings.nbRoues 'La roue 0 existe aussi, donc il y aura une roue de plus que ce chiffre.
    Private _vehiculeMass As Double = (_numberOfWheels_Index + 2) / 3 'Le facteur de poids du véhicule. 
    Private _isCollisionState As Boolean = False

    'Variables contenant les données du niveau
    Private _level As Integer 'Cette variable définit le niveau joué actuel.
    Private _levelBaseDatas As New List(Of Integer) 'Array contenant la hauteur des points du sol pour tous les pixel de l'image. 
    Private _levelDatas As New List(Of Integer) 'Array contenant la hauteur des points du sol du niveau pour tout le terrain de la carte. 
    Private _levelDatas_SyncObj As New Object 'Utilisé par le thread qui génère le niveau pour synchroniser l'accès à _levelDatas. 
    Private _levelTerrainObj As LevelGeneration

    'Variables concernant le score et le niveau d'énergie du véhicule
    Private _scoreAtStart As Double = My.Settings.score 'Le score avant de commencer le niveau
    Private _totalScoreAtStart As Double = My.Settings.totalScore 'Le score total depuis le début avant de commencer le niveau
    Private _scoreToAdd As Integer 'Le score aquis durant le niveau
    Private _scoreMaxDistYet As Integer 'La distance maximum parcourue dans cette instance. Utile pour ne pas donner du score si le véhicule recule.
    Private _maxBatteryLevel As Integer = 100 + Math.Abs(My.Settings.oldLvlBatteriePower) * 25
    Private _batteryLevel As Double = _maxBatteryLevel 'L'énergie restante avant de en plus en avoir. 
    Private Const _TICKS_BEFORE_DEATH As Integer = 5 * _FPS * _PHYSICS_FRAME_PRECISION
    Private _ticksLeftBeforeDead As Integer = _TICKS_BEFORE_DEATH '450 ticks, donc 5 secondes. Temps avant de mourir si l'on a plus d'énergie


    'Variable décidant si le niveau doit continuer de fonctionner ou pas
    Private _continueLoop As Boolean = True 'Si False, la boucle principale arrète.
    Private _restarted As Boolean = False 'Si cette varaible est True, le joueur recommence le niveau. La variable est remise à False instantanément dans la boucle principale (Main_Loop) pour ne pas recommencer en tout moment. 




    Sub New(ByVal level As Integer)
        _level = level
        'On génère le niveau (ici le terrain est généré selon l'image) :
        _levelTerrainObj = New LevelGeneration(level)
    End Sub
#End Region

#Region "Gestion Du niveau"
    Public Sub Start()
        _restarted = True
        Main_Loop()
        '_continueLoop = True
    End Sub

    'Public Sub Restart()
    '    _restarted = True
    '    Main_Loop()
    '    '_continueLoop = True
    'End Sub

    Public Sub Pause()
        _continueLoop = False
    End Sub
    Public Sub Unpause()
        _continueLoop = True
        Main_Loop()
    End Sub
    Public Function IsPaused() As Boolean
        Return Not _continueLoop
    End Function

    Public Sub EndLevel()
        _continueLoop = False
    End Sub
#End Region

#Region "Fonction des touches de clavier"
    ''Les fonctions et variables suivantes définissent et obtiennent l'état des touches pesées afin que l'engin physique en prenne compte. 
    'La touche W est pour avancer. 
    Private _key_W_IsPressedVar As Boolean = False
    Private _key_W_IsPressed_Lock As New Object
    Public Sub SetKeyState_W(ByVal IsPressed As Boolean)
        SyncLock _key_W_IsPressed_Lock
            _key_W_IsPressedVar = IsPressed
        End SyncLock
    End Sub
    Private Function Key_W_IsPressed() As Boolean
        Static returnState As Boolean
        SyncLock _key_W_IsPressed_Lock
            returnState = _key_W_IsPressedVar
        End SyncLock
        Return returnState
    End Function

    'La touche A est pour faire une rotation vers la gauche. 
    Private _key_A_IsPressedVar As Boolean = False
    Private _key_A_IsPressed_Lock As New Object
    Public Sub SetKeyState_A(ByVal IsPressed As Boolean)
        SyncLock _key_A_IsPressed_Lock
            _key_A_IsPressedVar = IsPressed
        End SyncLock
    End Sub
    Private Function Key_A_IsPressed() As Boolean
        Static returnState As Boolean
        SyncLock _key_A_IsPressed_Lock
            returnState = _key_A_IsPressedVar
        End SyncLock
        Return returnState
    End Function

    'La touche S est pour freiner/reculer
    Private _key_S_IsPressedVar As Boolean = False
    Private _key_S_IsPressed_Lock As New Object
    Public Sub SetKeyState_S(ByVal IsPressed As Boolean)
        SyncLock _key_S_IsPressed_Lock
            _key_S_IsPressedVar = IsPressed
        End SyncLock
    End Sub
    Private Function Key_S_IsPressed() As Boolean
        Static returnState As Boolean
        SyncLock _key_S_IsPressed_Lock
            returnState = _key_S_IsPressedVar
        End SyncLock
        Return returnState
    End Function

    'La touche D est pour faire une rotation vers la droite. 
    Private _key_D_IsPressedVar As Boolean = False
    Private _key_D_IsPressed_Lock As New Object
    Public Sub SetKeyState_D(ByVal IsPressed As Boolean)
        SyncLock _key_D_IsPressed_Lock
            _key_D_IsPressedVar = IsPressed
        End SyncLock
    End Sub
    Private Function Key_D_IsPressed() As Boolean
        Static returnState As Boolean
        SyncLock _key_D_IsPressed_Lock
            returnState = _key_D_IsPressedVar
        End SyncLock
        Return returnState
    End Function

    ''Fin des fonctions de touches de clavier. 
#End Region



#Region "Boucle de Jeu"
    'La boucle qui s'effectue constamment à la vitesse des _FPS pour calculer la physique et les graphiques. 
    Private Sub Main_Loop()
        Dim stp As Stopwatch = New Stopwatch
        Dim remainingToNextFrame As Single


        ''''''''''Initialisation des variables physiques du niveau au comencement :
        If _restarted Then
            REM Il est plus commun d'utiliser des _underscores_ dans les constantes, mais
            REM ici ils sont utilisés pour améliorer la lisibilité des nombreuses variables.

            '''''Véhicule :
            _vehicule.Clear()

            'Accélération 
            _vehicule.Add("veh_Acc_X", 0)
            _vehicule.Add("veh_Acc_Y", 0)
            'Vitesse (velocity)
            _vehicule.Add("veh_Vel_X", 0)
            _vehicule.Add("veh_Vel_Y", 0)
            'Position
            _vehicule.Add("veh_Pos_X", CInt(_DIST_BETWEEN_WHEELS * 20))
            _vehicule.Add("veh_Pos_Y", _DIST_BETWEEN_WHEELS)

            'Accélération angulaire
            _vehicule.Add("veh_Rot_Acc", 0)
            'Vitesse angulaire
            _vehicule.Add("veh_Rot_Vel", 0)
            'Angle (inclinaison) :
            _vehicule.Add("veh_Rot", 0)


            '''''Roues :
            _wheels.Clear()

            For wheelNum As Integer = 0 To _NumberOfWheels_Index
                _wheels.Add(New Dictionary(Of String, Double))
                'Accélération angulaire :
                _wheels.Item(wheelNum).Add("wheel_AngAcc", 0)
                'Vitesse angulaire :
                _wheels.Item(wheelNum).Add("wheel_AngVel", 0)
                'Rotation (inclinaison) :
                _wheels.Item(wheelNum).Add("wheel_AngPos", 0)
                'Position
                _wheels.Item(wheelNum).Add("wheel_Pos_X", (wheelNum + 1) * _DIST_BETWEEN_WHEELS)
                _wheels.Item(wheelNum).Add("wheel_Pos_Y", 400)

                'La force de la dernière collision survenue pour cette roue :
                _wheels.Item(wheelNum).Add("previous_CollisionForce", 0)
            Next

            _restarted = False 'Le niveau a été correctement initialisé. 
        End If


        stp.Start()


        While True
            'Ici, on calcule le temps qu'à pris la dernière boucle, puis on "Thread.Sleep" jusqu'à ce que la prochaine image (frame) soit atteinte (stabiliser le _FPS). 
            stp.Stop()
            remainingToNextFrame = _FRAME_TIME_MS - stp.ElapsedMilliseconds
            stp.Restart()
            remainingToNextFrame = CSng(Precomputations.UnderZeroEqualsZero(remainingToNextFrame))
            Thread.Sleep(CInt(remainingToNextFrame))

            'Obtenons le niveau 
            _levelDatas = _levelTerrainObj.GetFullLevel(CInt(_vehicule.Item("veh_Pos_X")) + 5000) 'Ajoutons 5000 dans le cas où l'utilisateur aurait un très grand écran. On approche du bug de l'an 3000!



            '//////////////////////////////
            'Nous sommes dans un frame, faisons les calculs physiques et envoyons ceci par la suite à l'engin graphique.


            'Calculs physiques pour ce frame
            For i As Integer = 0 To _PHYSICS_FRAME_PRECISION - 1
                'Enlevons de l'énergie dépendemment des touches sont enfoncées
                If (Key_W_IsPressed() Or Key_S_IsPressed()) And _isCollisionState Then _batteryLevel -= 3 / _FPS / _PHYSICS_FRAME_PRECISION
                If Key_A_IsPressed() Or Key_D_IsPressed() Then _batteryLevel -= 3 / _FPS / _PHYSICS_FRAME_PRECISION
                'Enlevons aussi de l'énergie avec le temps
                _batteryLevel -= 0.5 / _FPS / _PHYSICS_FRAME_PRECISION

                'Si il ne reste plus de batterie, réduisons le compte vers la mort et annulons le keypress W et S...
                If _batteryLevel <= 0 Then
                    _ticksLeftBeforeDead -= 1
                    SetKeyState_W(False)
                    SetKeyState_S(False)
                End If
                'Si le compte vers la mort a déjà été enclenché mais qu'une batterie a été récupérée
                If _batteryLevel > 0 Then _ticksLeftBeforeDead = _TICKS_BEFORE_DEATH
                If _ticksLeftBeforeDead = 0 Then
                    MessageBox.Show("Vous n'avez plus d'énergie. Votre score pour cette ballade fût de :" & vbNewLine & _
                                    Format(My.Settings.score, "###,###") & vbNewLine & _
                                    "Vous serez rammené au menu. Profitez-en pour acheter des améliorations !", "Fin du niveau", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    frmJeu.Close()
                End If





                'Les collisions et leur résolution est calculée. 
                ProcessPhysics()



            Next
            _isCollisionState = False 'Remettons l'état de collision à neuf pour la prochaine itération.


            'Mettons à jour le score :
            Dim thisFrameDist As Integer = CInt(_vehicule.Item("veh_Pos_X")) \ 1000
            If _scoreMaxDistYet < thisFrameDist Then
                _scoreMaxDistYet = thisFrameDist
                'Ici quelques calculs sont effectués pour ajuster le score. Il augmente selon une fonction de degré 3 (ax^3+bx^2+cx+d) dépendant du niveau actuel et de la dsitance parcourue :
                _scoreToAdd = CInt((Math.Pow((_level + 1) / 2 * 5 * _scoreMaxDistYet, 2) - 250) / 200)
                My.Settings.score = _scoreAtStart + _scoreToAdd + 1
                My.Settings.totalScore = _totalScoreAtStart + _scoreToAdd + 1
                My.Settings.Save()
            End If
            
            

            'On laisse le temps aux autres subs de s'effectuer :
            Application.DoEvents()



            'On envoie l'image au DrawLevel, ou on termine la boucle si indiqué :
            If _continueLoop Then

                'Convertissons quelques variables de coordonnées absolues en coordonnées relatives pour le "dessinage" dans le Sub DrawLevel (en fait, positionner l'écran sur le centre du véhicule et générer le bloc de terrain à envoyer) :
                Dim vehiculeCenter As New Point(CInt(_vehicule.Item("veh_Pos_X")), CInt(_vehicule.Item("veh_Pos_Y")))

                Dim map As New List(Of Integer)


                'Le véhicule est alligné en largeur à une certaine position relative.
                Dim leftSpacing As Integer = CInt(frmJeu.PictureBox1.Width * 0.2)
                'Le véhicule est alligné en hauteur à une certaine position relative.
                Dim topSpacing As Integer = CInt(frmJeu.PictureBox1.Height * 0.6 - _DIST_BETWEEN_WHEELS / 2)
                Dim VerticalOffset As Integer = vehiculeCenter.Y - topSpacing
                Dim batteryPos As Integer = -1

                SyncLock _levelDatas_SyncObj 'Synchronisation des données du terrain du niveau (car en parrallèle avec le producteur de ces données...)
                    'Décalage du terrain relatif à la position du véhicule
                    For width_x As Integer = vehiculeCenter.X - leftSpacing To vehiculeCenter.X + frmJeu.PictureBox1.Width - leftSpacing
                        If width_x > 0 Then
                            map.Add(_levelDatas.Item(width_x) - VerticalOffset)

                            'On trouve la position de la batterie la plus proche pour en trouver la position relative... 
                            If _levelTerrainObj._LevelBatteries.IndexOf(width_x) <> -1 And batteryPos = -1 Then
                                batteryPos = width_x - (vehiculeCenter.X - leftSpacing)
                            End If
                        Else
                            map.Add(-10000)
                        End If
                    Next
                End SyncLock

                Dim wheels As New List(Of Point)
                For wheelNum = 0 To _numberOfWheels_Index
                    wheels.Add(New Point(CInt(_wheels.Item(wheelNum).Item("wheel_Pos_X")) - vehiculeCenter.X + leftSpacing, CInt(_wheels.Item(wheelNum).Item("wheel_Pos_Y")) - VerticalOffset))
                Next

                'On remplace le centre absolu par le centre relatif
                vehiculeCenter = New Point(leftSpacing, topSpacing)

                '//////////////////////////////
                'Envoyer les données de calcul à notre "engin graphique"
                frmJeu.DrawLevel(wheels, vehiculeCenter, map, Precomputations.Pythagorean_C(_vehicule.Item("veh_Vel_X"), _vehicule.Item("veh_Vel_Y")) * _PHYSICS_FRAME_PRECISION, batteryPos, _batteryLevel, _maxBatteryLevel)
                '//////////////////////////////

            Else
                Exit While
            End If


            'On laisse le temps aux autres subs de s'effectuer :
            'Application.DoEvents()
        End While
    End Sub
#End Region



#Region "Physique"

#Region "Fonctions de physiques"

    'Gère les collisions pour l'engin graphique
    Private _coll As New List(Of Point)
    Public Function coll(ByVal wheelNum As Integer) As Point
        Return _coll.Item(wheelNum)
    End Function

    'Projette un vecteur sur un autre pour en trouver la composante parralèle. La valeur de retour est la longeur du vecteur projeté. 
    Private Function ProjectVector(ByVal baseVector_X As Double, ByVal baseVector_Y As Double, ByVal vectorToProject_X As Double, ByVal vectorToProject_Y As Double, ByVal rotateBaseOf_90CCW_ForProjection As Boolean) As Double
        '*Les lignes suivantes sont à titre de compréhension. Ces lignes ont étés réduites en une seule àprès ce commentaire. 
        Dim projectedVector_Lenght As Double
        'Trouvons l'angle du vecteur à projeter : 
        Dim vectorToProject_Angle As Double = 180 / Math.PI * Math.Atan2(vectorToProject_Y, vectorToProject_X)
        'Trouvons l'angle du vecteur de base : 
        Dim baseVector_Angle As Double = 180 / Math.PI * Math.Atan2(baseVector_Y, baseVector_X)
        If rotateBaseOf_90CCW_ForProjection Then baseVector_Angle += 90
        'Trouvons la différence d'angle entre le vecteur à projeter et le vecteur de base : 
        Dim deltaAngle As Double = vectorToProject_Angle - baseVector_Angle

        'Trouvons la longeur du vecteur à projeter : 
        Dim vectorToProjectLenght As Double = Precomputations.Pythagorean_C(vectorToProject_X, vectorToProject_Y)
        'Trouvons la longeur de la projection : 
        projectedVector_Lenght = vectorToProjectLenght * Precomputations.Cos(deltaAngle)

        'Retour de la réponse : 
        Return projectedVector_Lenght


        'Toutes les lignes commentées plus haut ont étés réduites en économisant des variables et des lignes :
        'Dim baseVector_Angle As Double = Precomputations.ArcSin(baseVector_Y, baseVector_X) '+ Convert.ToInt32(rotateBaseOf_90CCW_ForProjection) * 90
        'Return Precomputations.Pythagorean_C(vectorToProject_X, vectorToProject_Y) * Precomputations.Cos(Precomputations.ArcSin(vectorToProject_Y, vectorToProject_X) - baseVector_Angle)
    End Function


    'Cette fonction est très utilisée et servira à receuillir la position des roues selon le centre et l'angle du véhicule. Le paramètre "absolutePosition" décrit si la position à retourner est celle absolue sur la carte, ou celle relative au centre du véhicule. 
    Private Function GetWheelsPos(ByVal absolutePosition As Boolean) As List(Of List(Of Double))
        Dim wheelPos As New List(Of List(Of Double))
        'Dimension 1 de cette liste : 
        ''Numéro de la roue (wheelNum) tel que 0, 1, ou si trois roues, 2
        'Dimension 2 :
        ''wheelPos.Item(wheelNum).Item(0) = Coordonnée en X
        ''wheelPos.Item(wheelNum).Item(1) = Coordonnée en Y

        'On trouve l'angle entre chaque roues selon le nombre de roues
        Dim angleBetweenTwoWheels As Double = 360 / (_numberOfWheels_Index + 1)

        For wheelNum As Integer = 0 To _numberOfWheels_Index
            wheelPos.Add(New List(Of Double)) 'On ajoute l'index de la roue à la liste retournée

            Dim x As Double
            Dim y As Double

            Dim absoluteAngleOfThisWheel As Double = AngleOfWheel(wheelNum, True)
            x = Precomputations.Cos(absoluteAngleOfThisWheel) * _DIST_BETWEEN_WHEELS / 2
            y = Precomputations.Sin(absoluteAngleOfThisWheel) * _DIST_BETWEEN_WHEELS / 2

            If absolutePosition Then
                'Si on veut la position absolue, ajoutons le centre du véhicule au résultat.
                x += _vehicule.Item("veh_Pos_X")
                y += _vehicule.Item("veh_Pos_Y")
            End If

            wheelPos.Item(wheelNum).Add(x) 'X Position of Wheel
            wheelPos.Item(wheelNum).Add(y) 'Y Position of Wheel
        Next

        Return wheelPos
    End Function

    'Retourne l'angle de cette roue, angle absolu ou pas. 
    'L'angle absolu d'une roue signifie {l'angle relatif de la roue sur le véhicule} ajouté à {l'angle du véhicule}
    Private Function AngleOfWheel(ByVal wheelNum As Integer, ByVal absolute As Boolean) As Double
        Dim angle As Double = (wheelNum * (360 / (_numberOfWheels_Index + 1)))
        If absolute Then angle += _vehicule.Item("veh_Rot")
        angle = angle Mod 360
        Return angle
    End Function

    'Le sub suivant est appelé à l'intérieur de la boucle du Sub Main_Loop(). Donc, ce sub est appelé à chaque fraction de seconde. 
    'Celui qui s'aventure à essayer de comprendre ce code sans de notions de base en physique mécanique devra affronter un long périple de torture.
#End Region

#Region "Coeur de la physique"
    Private Sub ProcessPhysics()
        'Le sub est séparé en 2 parties : La détection de la collision, puis la gestion des collisions. 

        'Pour la détection de collision, il s'agit de détecter, pour chaque pixel vertical de la roue, si le sol rentre dans la roue et enregistrer la distance entre el sol et le sens de la roue pour en faire une somme, puis calculer la moyenne de l'angle duquel proviens la collision pour la positionner
        'Du moment que le sol rentre dans la roue, il y aura plusieurs données générées pour chaque pixel où il y a collision. Il s'agira ensuite de regrouper ces données afin de faire comme s'il y avait eu une seule collision, étant égale à la somme de la profondeur des collisions, et de la moyenne de leur angle. Tout cela produira un vecteur moyen par roue, de direction opposée au sol et avec une certaine force (grandeur)

        'Pour la résolution de la collision, selon les vecteurs trouvés au point précédent, il faudra trouver leur effet sur le centre de masse du véhicule (un déplacement), puis ensuite trouver leur effet sur la rotation du véhicule. Ces vecteurs n'agiront pas sur la vitesse ni la vitesse angulaire du véhicule, mais sur son accélération et accélération angulaire (car ce sont des forces). 

        'S'ajoute à tout ceci l'accélération des roues que l'utilisateur contrôle, ainsi que l'accélération angulaire et la force de gravitée.

        '|||||||||| 
        'Il est à noter que le pneu est vu comme un peu comme élastique ou avec un effet de ressort, donc il peut pénetrer légèrement dans le sol, et selon la pénetration, une certaine force de repoussement sera générée.
        '|||||||||| 


        '########################
        '######################## Détection de collision
        '########################

        Dim wheelsCollisionForce As New List(Of List(Of Double)) 'Force de la collision, utile pour la résoudre
        Dim wheelsCollisionPos As New List(Of List(Of Double)) 'Position de la collision par rapport à la roue
        Dim excessDippedDist As Integer 'Si plus grand que 0, la roue est enfoncée trop creux dans le sol et un calcul spécial devra la ressortir de la distance spécifiée. 
        'Dimension1 : Numero (index) de la roue
        'Dimension2 : Force en X (index 0) et en Y (index 1) de la collision


        Dim isCollisionState As Boolean = False 'Définit si nous sommes en état de collision ou pas. ATTENTION, DIFFÉRENT DE _isCollisionState. Celle déclarée ici contient les informations seulement pour cette itération de physique, et non pour un frame. 
        Dim wheelCollisionState As New List(Of Boolean) 'Définit l'état de collision pour chacune des roues. Ressemble à _isCollisionState mais pour chaque roues..


        Static wheelBorders As List(Of Integer) = Precomputations.WheelBorders(_WHEEL_RADIUS) 'On obtient les points de la roue en Y où détecter les collisions. 
        For wheelNum As Integer = 0 To _NumberOfWheels_Index 'Boucle les multiples roues

            Dim delta_x As Integer 'Cette différence en X nous servira de variable d'increment.
            delta_x = 0

            Dim x1 As Integer = CInt(_wheels.Item(wheelNum).Item("wheel_Pos_X"))
            Dim y1 As Integer = CInt(_wheels.Item(wheelNum).Item("wheel_Pos_Y"))
            Dim ThisWheelPos As New Point(x1, y1) 'On récupère l'information de la position de la roue à partir de la variable "Private _wheels"

            Dim wheelCollisions As New Dictionary(Of Integer, Integer) 'Les informations récoltées par l'algo de collision seront dans cette variable. 
            SyncLock _levelDatas_SyncObj 'On se servira de cette variable, il faut la synchroniser à cause du multi-threading.

                'Détecte s'il y a collision en chaque point de la roue actuelle et ajoute la valeur à un array (ici, dictionnaire WheelCollisions).
                If ThisWheelPos.X - _WHEEL_RADIUS > 0 Then 'Si le véhicule est dans la carte
                    For x As Integer = CInt(ThisWheelPos.X) - _WHEEL_RADIUS To CInt(ThisWheelPos.X) + _WHEEL_RADIUS - 1
                        If 0 > ThisWheelPos.Y - wheelBorders.Item(delta_x) - _levelDatas.Item(x) Then
                            'La condition satisfaite, une collision a eu lieu
                            'On ajoute à une liste contenant les points de collision et leur profondeur. 
                            Dim xDistFromCenter As Integer = _WHEEL_RADIUS - delta_x
                            Dim yDistFromCenter As Integer = ThisWheelPos.Y - _levelDatas.Item(x)
                            Dim radiusFromCenter As Integer = CInt(Precomputations.Pythagorean_C(xDistFromCenter, yDistFromCenter))


                            'Calculons l'état d'enfoncement pour cette roue pour savoir s'il est excessif:
                            Static minRadius As Integer = CInt((0.75 * _WHEEL_RADIUS))
                            Dim dippedDifference As Integer = CInt(Precomputations.UnderZeroEqualsZero(minRadius - radiusFromCenter))
                            If dippedDifference > excessDippedDist Then
                                excessDippedDist = dippedDifference
                            End If

                            'Si la collision dépasse la hauteur de la roue, on la simplifie à un distance recevable... 
                            If radiusFromCenter > _WHEEL_RADIUS Then
                                yDistFromCenter = Math.Sign(yDistFromCenter) * (wheelBorders.Item(delta_x) + 1)
                            End If

                            'Dans le cas où la collision est supérieure à la roue, on la met égale au centre de la roue en hauteur.
                            If yDistFromCenter < 0 Then yDistFromCenter = 0

                            wheelCollisions.Add(xDistFromCenter, yDistFromCenter) 'Ajoutons le point de collision pour plus tard trouver la force
                        End If
                        delta_x += 1
                    Next
                Else
                    'Aucune collision...
                End If

            End SyncLock


            'Collision avec une batterie : 
            Dim collisionRadiusFromWheelCenter As Integer = _WHEEL_RADIUS + 50 'représente la distance entre le cetre d'une batterie et de la roue s'ils sont collés. Ceci servira à tester la collision avec la batterie. 
            For Each batteryX As Integer In _levelTerrainObj._LevelBatteries
                'On vérifie la distance entre la batterie et la roue avec le théorème de pythagore. On trouve la distance entre les deux (l'hypothénuse C d'un triangle entre la différence en X et Y de la roue et de la batterie) et on la compare à la distance de collision : 
                If Math.Abs(ThisWheelPos.X - batteryX) < 50 Then 'Si nous sommes proche de la batterie en X
                    If collisionRadiusFromWheelCenter > Precomputations.Pythagorean_C(ThisWheelPos.X - batteryX, ThisWheelPos.Y - (_levelDatas.Item(batteryX) + 25)) Then
                        'On restaure la jauge d'énergie
                        _batteryLevel += _maxBatteryLevel
                        If _batteryLevel > _maxBatteryLevel Then _batteryLevel = _maxBatteryLevel
                    End If
                End If
            Next



            'Maintenant la collision détectée en chaque point de la roue, simplifions ces collisions en une seule qui sera une "collision moyenne" de cette roue
            'Commencons par trouver la somme de toutes les collisions
            Dim depthSum As Double
            depthSum = 0
            Dim distanceMoySumX As Double
            distanceMoySumX = 0
            Dim distanceMoySumY As Double
            distanceMoySumY = 0

            'Calculer les informations relative à la moyenne des collisions, pour chaque roues : 
            For Each collisionPoint As KeyValuePair(Of Integer, Integer) In wheelCollisions
                Dim distanceX As Integer = collisionPoint.Key
                Dim distanceY As Integer = Math.Abs(collisionPoint.Value)
                Dim distance As Double = _WHEEL_RADIUS - Precomputations.Pythagorean_C(distanceX, distanceY)
                If distance = 0 Then distance = 0.0000000001

                depthSum += distance

                distanceMoySumX += distanceX * distance
                distanceMoySumY += Math.Abs(distanceY * distance)
            Next

            'Calculons enfin la moyenne de la position des collisions selon la somme trouvée :
            Dim distanceMoyX As Integer
            distanceMoyX = 0
            Dim distanceMoyY As Integer
            distanceMoyY = 0

            If depthSum = 0 Then depthSum = 0.0000000001
            If depthSum <> 0 Then 'Pas de division par 0 !
                distanceMoyX = CInt(-distanceMoySumX / depthSum)
                distanceMoyY = CInt(distanceMoySumY / depthSum)
            End If

            'Ajouter la collision à la variable coll pour l'engin graphique...
            _coll.Add(New Point)
            _coll.Item(wheelNum) = New Point(distanceMoyX, distanceMoyY)


            'Si les pneus sont en train de ressortir du sol plutôt que d'en rentrer, nous réduisons l'accélération afin de simuler des amortisseurs, et pour faire en sorte que le véhicule rebondisse moins haut que la hauteur qu'il avait avant.
            Dim thisCollisionForce As Double = Precomputations.Pythagorean_C(distanceMoySumX, distanceMoySumY)

            If thisCollisionForce < _wheels.Item(wheelNum).Item("previous_CollisionForce") Then
                Static tireBouncyness As Double = 1 '0 to 1.
                distanceMoySumX = distanceMoySumX * tireBouncyness - 1
                distanceMoySumY = distanceMoySumY * tireBouncyness - 1
            End If

            _wheels.Item(wheelNum).Item("previous_CollisionForce") = thisCollisionForce


            'Mettons en mémoire la force des collisions des roues :
            wheelsCollisionForce.Add(New List(Of Double))
            wheelsCollisionForce.Item(wheelNum).Add(distanceMoySumX) 'Vecteur force de la collision en X
            wheelsCollisionForce.Item(wheelNum).Add(distanceMoySumY) 'Vecteur force de la collision en Y

            'Mettons en mémoire la position des collisions sur les roues :
            wheelsCollisionPos.Add(New List(Of Double))
            wheelsCollisionPos.Item(wheelNum).Add(distanceMoyX) 'Vecteur de position de la collision en X
            wheelsCollisionPos.Item(wheelNum).Add(distanceMoyY) 'Vecteur de position de la collision en Y


            'Définissons si on est dans un état de collision ou pas pour chaque roue et globalement: 
            wheelCollisionState.Add(False)
            If wheelsCollisionForce.Item(wheelNum).Item(0) <> 0 Or wheelsCollisionForce.Item(wheelNum).Item(1) <> 0 Then
                wheelCollisionState.Item(wheelNum) = True
                isCollisionState = True
            End If

            wheelCollisions.Clear()

            If isCollisionState Then _isCollisionState = True 'On assigne l'état de collision d'une roue d'une itération à celle globale par frame, utilisée globalement pour enlever de l'énergie. 
        Next

        '########################
        '######################## Fin détection de collision
        '########################



        '%%%%%%%%%%%%%%%%%%%%%%%%
        '%%%%%%%%%%%%%%%%%%%%%%%% Résolution de collision (coeur de la physique)
        '%%%%%%%%%%%%%%%%%%%%%%%%

        'mouvement, vitesse, et accélération du véhicule :

        'Trouver la force totale de la collision sur le centre de masse du véhicule. 
        Dim totalColisionForce_X As Double 'Définit la force totale de la colision en X
        Dim totalColisionForce_Y As Double 'Définit la force totale de la colision en Y
        For wheelNum As Integer = 0 To _numberOfWheels_Index
            totalColisionForce_X += wheelsCollisionForce.Item(wheelNum).Item(0)
            totalColisionForce_Y += wheelsCollisionForce.Item(wheelNum).Item(1)
        Next
        totalColisionForce_Y = Math.Sign(totalColisionForce_Y) * totalColisionForce_Y 'La collision fera toujours remonter vers le haut. 


        'Établissons la force totale de collision pour chaque roue et pour le centre de masse, ainsi que la force totale (de cette force totale de friction et de la force totale de collision)

        'Variables pour la friction
        Dim totalWheelFrictionForce_X As New List(Of Double) 'Définit la force de friction en X pour chaque roue
        Dim totalWheelFrictionForce_Y As New List(Of Double) 'Définit la force de friction en Y pour chaque roue
        Dim totalWheelsFrictionForce_X As Double 'Définit la force totale de friction en X sur le centre de masse du véhicule
        Dim totalWheelsFrictionForce_Y As Double 'Définit la force totale de friction en X sur le centre de masse du véhicule

        'Variables pour la force totale de "la force totale de collision" ET de "la force totale de friction".
        Dim totalWheelForce_X As New List(Of Double) 'Définit la force totale (colision + friction) en X pour chaque roue
        Dim totalWheelForce_Y As New List(Of Double) 'Définit la force totale (colision + friction) en Y pour chaque roue
        Dim totalWheelsForce_X As Double 'Définit la force totale (colision + friction) en X pour chaque roue
        Dim totalWheelsForce_Y As Double 'Définit la force totale (colision + friction) en Y pour chaque roue

        For wheelNum As Integer = 0 To _numberOfWheels_Index
            ''On définit les forces de friction (Si entrées par l'utilisateur avec les touches W et S.)
            totalWheelFrictionForce_X.Add(0)
            totalWheelFrictionForce_Y.Add(0)

            'Le multiplicateur (coefficient) de la force de friction. Ici, le calcul est aussi fait selon la vitesse pour simplifier les calculs.
            Dim frictionForce As Double = 0.85 + Math.Abs(My.Settings.oldLvlAcceleration) * 0.15 'Accélération
            Dim speedModifier As Double = 8.5 + Math.Abs(My.Settings.oldLvlVitesse) * 1.5 / _vehiculeMass 'Vitesse max
            'La ligne suivante agit comme une fonction représentant l'accélération selon le module de la vitesse. Plus la vitesse est grande, plus l'accélération est petite. Ceci sert à limiter la vitesse maximale et à appliquer l'accélération selon celle-ci. 
            Dim accFactor As Double = Precomputations.UnderZeroEqualsZero(frictionForce - Precomputations.Pythagorean_C(_vehicule.Item("veh_Vel_X"), _vehicule.Item("veh_Vel_Y")) / (speedModifier))

            'Si la touche avancer ou reculer est pesée
            If Key_W_IsPressed() And wheelCollisionState.Item(wheelNum) = True Then
                totalWheelFrictionForce_X.Item(wheelNum) = wheelsCollisionForce.Item(wheelNum).Item(1) * accFactor
                totalWheelFrictionForce_Y.Item(wheelNum) = -wheelsCollisionForce.Item(wheelNum).Item(0) * accFactor
            ElseIf Key_S_IsPressed() And wheelCollisionState.Item(wheelNum) = True Then
                totalWheelFrictionForce_X.Item(wheelNum) = -wheelsCollisionForce.Item(wheelNum).Item(1) * accFactor
                totalWheelFrictionForce_Y.Item(wheelNum) = wheelsCollisionForce.Item(wheelNum).Item(0) * accFactor
            End If

            'Ajoutons ce total par roue au total global (du centre de masse).
            totalWheelsFrictionForce_X += totalWheelFrictionForce_X.Item(wheelNum)
            totalWheelsFrictionForce_Y += totalWheelFrictionForce_Y.Item(wheelNum)

            ''Ajoutons ici directement les différents éléments composant la force totale à la variable destinée à cette fin.
            totalWheelForce_X.Add(totalWheelFrictionForce_X.Item(wheelNum) + wheelsCollisionForce.Item(wheelNum).Item(0))
            totalWheelForce_Y.Add(totalWheelFrictionForce_Y.Item(wheelNum) + wheelsCollisionForce.Item(wheelNum).Item(1))
            'Ajoutons ce total par roue au total global (du centre de masse).
            totalWheelsForce_X += totalWheelForce_X.Item(wheelNum)
            totalWheelsForce_Y += totalWheelForce_Y.Item(wheelNum)
        Next

        'Définit le total de la "force totale" en X (toutes les roues et force de : colision + friction) 
        Dim totalForce_X As Double = totalColisionForce_X + totalWheelsForce_X
        'Définit le total de la "force totale" en Y (toutes les roues et force de : colision + friction) 
        Dim totalForce_Y As Double = totalColisionForce_Y + totalWheelsForce_Y



        ''Position du véhicule : 


        'L'accélération pour cette image (frame) est définie
        _vehicule.Item("veh_Acc_X") = (0.000205 * totalForce_X / _PHYSICS_FRAME_PRECISION) ' + 0.05
        _vehicule.Item("veh_Acc_Y") = (0.000205 * totalForce_Y - 0.25) / _PHYSICS_FRAME_PRECISION 'La gravitée s'effectue ici, en Y ! Elle est de -1.5px/s^2

        'Dans le cas où la roue rentre trop dans le sol : 
        If excessDippedDist > 0 Then
            Dim c As Double = Pythagorean_C(totalForce_X, totalForce_Y)
            If c = 0 Then c = 0.0000000001
            'Ressortons-la un peu...
            _vehicule.Item("veh_Pos_X") += 0.25 * excessDippedDist * totalForce_X / c
            _vehicule.Item("veh_Pos_Y") += 0.25 * excessDippedDist * totalForce_Y / c
            _vehicule.Item("veh_Acc_X") += 0.05 * excessDippedDist * totalForce_X / c
            _vehicule.Item("veh_Acc_Y") += 0.05 * excessDippedDist * totalForce_Y / c
        End If

        'À la vitesse est ajouté (transmis) l'accélération
        _vehicule.Item("veh_Vel_X") += _vehicule.Item("veh_Acc_X")
        _vehicule.Item("veh_Vel_Y") += _vehicule.Item("veh_Acc_Y")
        'Mettons un maximum à la vitesse... :
        If _vehicule.Item("veh_Vel_X") > 40 Then _vehicule.Item("veh_Vel_X") = 40
        If _vehicule.Item("veh_Vel_X") > 40 Then _vehicule.Item("veh_Vel_X") = 40

        'À la position est ajouté (transmis) la vitesse.
        'Le rôle que joue ici l'accélération dans le calcul de la position est de nuire au mouvement, de prendre de l'énergie. 
        _vehicule.Item("veh_Pos_X") += _vehicule.Item("veh_Vel_X") + 0.0002 * totalForce_X
        _vehicule.Item("veh_Pos_Y") += _vehicule.Item("veh_Vel_Y") + 0.0002 * totalForce_Y



        ''Mouvement, vitesse, et accélération ANGULAIRE du véhicule :


        'Gestion de la rotation des roues selon les collisions et la friction.
        Dim relativeWheelPos As List(Of List(Of Double)) = GetWheelsPos(False)
        Dim forcePos As New List(Of List(Of Double))
        Dim totalAngularForce As Double = 0
        For wheelNum As Integer = 0 To _numberOfWheels_Index
            'Retrouvons l'angle absolu de cette roue
            Dim angleOfThisWheel As Double = AngleOfWheel(wheelNum, True)

            forcePos.Add(New List(Of Double))
            'Rayon en Y
            forcePos.Item(wheelNum).Add(relativeWheelPos.Item(wheelNum).Item(0) + wheelsCollisionPos.Item(wheelNum).Item(0))
            'Rayon en Y
            forcePos.Item(wheelNum).Add(relativeWheelPos.Item(wheelNum).Item(1) + wheelsCollisionPos.Item(wheelNum).Item(1))

            'Le vecteur force projetté sur la perpendiculaire du rayon est ajouté à la force angulaire totale. 
            totalAngularForce += ProjectVector(forcePos.Item(wheelNum).Item(0), forcePos.Item(wheelNum).Item(1), totalWheelForce_X.Item(wheelNum), totalWheelForce_Y.Item(wheelNum), True)

        Next
        _vehicule.Item("veh_Rot_Acc") = 0

        'Ajoutons l'accélération angulaire par les touches A ou D :
        If _batteryLevel > 0 And (Key_A_IsPressed() Or Key_D_IsPressed()) Then
            'Déterminons le sens de rotation voulu. 
            Dim rotDirection As Integer = Math.Sign(_vehicule.Item("veh_Rot_Vel"))
            Dim keyRotDirection As Integer = 1

            If Key_D_IsPressed() Then
                keyRotDirection *= -1
            End If

            'Selon le sens de rotation actuel et le sens voulu, influencons la force de rotation. Elle sera limitée (rotAccFactor) si la vitesse de rotation maximale est déjà atteinte dans le sens de rotation étant le même que le sens voulu, sinon la force (accélération...) sera maximale. 
            'Limitons l'accélération angulaire maximale par l'utilisateur :
            Dim rotateSpeedModifier As Double = (0.2 + Math.Abs(My.Settings.oldLvlForceRotation) * 0.06) / _vehiculeMass 'Vitesse max de rotation.
            'La ligne suivante agit comme une fonction représentant l'accélération selon le module de la vitesse. Plus la vitesse est grande, plus l'accélération est petite. Ceci sert à limiter la vitesse maximale et à appliquer l'accélération selon celle-ci. 
            Dim functionSlope As Double = (_vehicule.Item("veh_Rot_Vel")) / (5 * rotateSpeedModifier)
            Dim rotAccMultiplier As Double = 0.04 + Math.Abs(My.Settings.oldLvlForceRotation) * 0.02 'Accélération max de la rotation par les touches A ou D. 
            Dim rotAccFactor As Double = rotAccMultiplier * Precomputations.UnderZeroEqualsZero(1 - Math.Abs(functionSlope))

            If rotDirection * keyRotDirection > 0 Then
                _vehicule.Item("veh_Rot_Acc") += keyRotDirection * rotAccFactor / _vehiculeMass
            Else
                _vehicule.Item("veh_Rot_Acc") += keyRotDirection * rotAccMultiplier / _vehiculeMass
            End If
        End If


        'Limitons l'accélération angulaire maximale, maintenant, faite par le terrain (et l'accélération apr les roues...) :
        Dim groundRotAccFactor = 1 * Precomputations.UnderZeroEqualsZero(1 - Math.Abs(_vehicule.Item("veh_Rot_Vel")) / (10 / _vehiculeMass))
        _vehicule.Item("veh_Rot_Acc") += 0.00005 * totalAngularForce * groundRotAccFactor / (1.2 * _vehiculeMass)


        _vehicule.Item("veh_Rot_Vel") += _vehicule.Item("veh_Rot_Acc")
        _vehicule.Item("veh_Rot_Vel") *= 0.999 'Imaginons des forces de frottement ralentissant la rotation...

        _vehicule.Item("veh_Rot") = (_vehicule.Item("veh_Rot") + _vehicule.Item("veh_Rot_Vel") + 0.000006 * totalAngularForce) Mod 360


        'On replace les roues au bon endroit après le mouvement et la rotaiton du véhicule :
        Dim wheelPos As List(Of List(Of Double)) = GetWheelsPos(True)
        For wheelNum As Integer = 0 To _numberOfWheels_Index
            _wheels.Item(wheelNum).Item("wheel_Pos_X") = wheelPos.Item(wheelNum).Item(0)
            _wheels.Item(wheelNum).Item("wheel_Pos_Y") = wheelPos.Item(wheelNum).Item(1)
        Next


        '%%%%%%%%%%%%%%%%%%%%%%%%
        '%%%%%%%%%%%%%%%%%%%%%%%% Fin Résolution de collision (physique)
        '%%%%%%%%%%%%%%%%%%%%%%%%

    End Sub
#End Region
#End Region



End Class
