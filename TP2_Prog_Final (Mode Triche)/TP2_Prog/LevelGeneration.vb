Imports System.Threading
Imports System.Threading.Tasks

Public Class LevelGeneration
    Private _level As Integer

    Private _levelGenerationNotCompleted As Boolean = True 'Indique à la classe LevelProcessing si le niveau est prêt à être joué et qu'il y a déjà assez de terrain de généré. 
    Private _levelGenerationNotCompleted_Lock As New Object 'Utilisé par plusieurs threads

    Private _levelBaseDatas As New List(Of Integer) 'Array contenant la hauteur des points du sol pour tous les pixel de l'image. 
    Private _levelDatas As New List(Of Integer) 'Array contenant la hauteur des points du sol du niveau pour tout le terrain de la carte. 
    Private _levelDimensionDifferenceDatas As New List(Of Integer) 'Array contenant la hauteur des points du sol du niveau pour tout le terrain de la carte. 
    Private _levelDatas_Lock As New Object 'Utilisé par le thread qui génère le niveau pour synchroniser l'accès à _levelDatas.

    Public _LevelBatteries As New List(Of Integer) 'Dictionnaire qui contient la position des batteries dans le niveau.

    Private Const _maxLevelWidthCount As Integer = 1000000 'Représente la longeur du niveau (presque infini).


    Sub New(ByVal level As Integer)
        _level = level
        'On génère le terrain du niveau et la position des batteries : 
        GenerateLevelBase(_level)
        GenBatteries()
    End Sub

    'Fonction utilisée par LevelProcessing afin de lire le niveau
    Public Function GetFullLevel(ByVal minimumNeededLenght As Integer) As List(Of Integer)
        Dim levelPart As List(Of Integer)

        'Attendons que le niveau se génère assez avant de retourner une valeur : 
        Dim levelGenerationNotCompleted As Boolean
        SyncLock _levelGenerationNotCompleted_Lock
            levelGenerationNotCompleted = _levelGenerationNotCompleted
        End SyncLock

        If levelGenerationNotCompleted Then
            'Le niveau n'a pas fini de générer... attendons au moins que la partie qui nous intéresse existe
            Dim whileContinueBool As Boolean = True
            While whileContinueBool
                SyncLock _levelDatas_Lock
                    If _levelDatas.Count > minimumNeededLenght + 1 Then whileContinueBool = False
                End SyncLock
                Thread.Sleep(100) 'On attends patiemment la génération du nvieau...
            End While
            SyncLock _levelDatas_Lock
                levelPart = _levelDatas
            End SyncLock
            Return levelPart
        Else 'Si le niveau a fini d'être généré, on le retourne entièrement sans même faire un synclock.
            Return _levelDatas
        End If
    End Function

    





    Private Sub GenerateLevelBase(ByVal level As Integer)
        'On récupère l'image de ce niveau 
        Static levelBMP As Bitmap = New Bitmap(Application.StartupPath & "\levels\level" & level & ".png")

        'On boucle dans l'image du début à la fin.
        Dim i As Integer
        For y = 0 To levelBMP.Width - 1
            For x = 0 To levelBMP.Height - 1
                'Nous obtenons la luminosité (de 0 à 255) de ce pixel et l'ajoutons à la carte des pixels de base.
                'Cette carte "de base" ne sera pas en boucle (infinie) et servira comme base pour faire celle infinie. 
                'Ici, le "last" joeu le rôle d'une statique et le niveau est "arrondi" et plus doux selon le dernier pixel généré. On peut jouer avec les chiffres 0.95 et 0.5 pour que leur somme soit égale à 1.
                'Dim last As Integer = CInt(last * 0.95 + 0.05 * (255 * levelBMP.GetPixel(x, y).GetBrightness()))
                '_levelBaseDatas.Add(last)
                _levelBaseDatas.Add(CInt(255 * levelBMP.GetPixel(x, y).GetBrightness()))
                i += 1
            Next
        Next

        'ce petit for est utilisé pour corriger le début niveau où on peut voir les pixels avoir une séparation brusque. Ce for pourrait être plus poussé et répéter en mirroir les pixels de al fin du levelBase et les mettre en fondu progressif avec ceux du début, mais ce n'est pas énormément important. 
        For x As Integer = 0 To 500
            _levelBaseDatas.Item(x) = _levelBaseDatas.Item(x + 500)
        Next

        'On génère le terrain avec la base de niveau qu'on viens de générer :
        GenerateLevelTerrain(_maxLevelWidthCount) 'Bon ok, le terrain ne sera pas TOTALEMENT infini, mais de la longeur du paramètre spécifié ici... 

    End Sub

    Private Sub GenerateLevelTerrain(ByVal levelWidth As Integer)

        'Créons un thread en parrallèle pour calculer le terrain du niveau :
        Dim levelGenerationThread As New Thread( _
            Sub()
                'On calcule la longeur de la carte dans un thread roulant en parralèle au jeu. 
                Dim baseLevelLoopingWidth As Integer = _levelBaseDatas.Count - 1

                Dim levelBase_X As Integer = 0 'Représente la position en X selon la base du niveau actuel, mais n'est pas remis à 0 lors de la prochaine "séquence".
                For level_X As Integer = 0 To levelWidth 'La variable level_X représente où on en est rendus à générer en X dans le niveau. 
                    'La "séquence" est l'index du nombre de fois qu'on à répétés l'image de 10 000 pixels (niveau 100x100 px). 
                    Dim actualSequence As Integer = levelBase_X \ baseLevelLoopingWidth + 1

                    'Cette variable représente où on en est dans la séquence actuelle. Elle prends donc valeur de 0 à 10 000.
                    Dim levelBase_i As Integer = levelBase_X Mod baseLevelLoopingWidth

                    'Cette variable est comme la précécente, mais contient un écart de plus de 50% avec la position actuelle dans la séquence. Utile pour avoir une 2e valeur aléatoire. 
                    Dim levelBase_i_Plus As Integer = CInt((levelBase_X + 1.1 * baseLevelLoopingWidth / 2) Mod baseLevelLoopingWidth)


                    'Retrouvons le dernier point créé par la boucle. 
                    Dim lastPointY As Integer
                    If _levelDatas.Count - 1 < 0 Then
                        lastPointY = 0
                    Else
                        SyncLock _levelDatas_Lock
                            lastPointY = _levelDatas.Item(_levelDatas.Count - 1)
                        End SyncLock
                    End If

                    'Définissons la hauteur du point actuel 
                    Dim pointY As Integer
                    Select Case _level 'Beaucoup de chiffres ici ont étés choisis de façon arbitraires, afin de personnaliser les 4 niveaux (Case 0 à 3)
                        Case 0
                            'pointY = CInt((_levelBaseDatas.Item(levelBase_i) - 128) * (level_X / 10000))
                            pointY = CInt((_levelBaseDatas.Item(levelBase_i) - 128) * (level_X / 10000))
                            If pointY - lastPointY > CInt(level_X / 450) Then
                                pointY = lastPointY + CInt(level_X / 450)
                            End If

                            Dim waveLenght As Integer = CInt(Math.Abs((_levelBaseDatas.Item(levelBase_i_Plus) + _levelBaseDatas.Item(levelBase_i)) / 2) + 200) 'waveLenght est la distance pour laquelle chaque "pallier" se répète. Ici on répète dans le case plutôt que dans la boucle extérieure.
                            For i As Integer = 0 To waveLenght
                                'On créé la "vague" sinusoidale
                                Dim tempPointY As Integer = CInt(lastPointY + (pointY - lastPointY) * SmoothSine(i / waveLenght))

                                SyncLock _levelDatas_Lock 'On prends possession de _levelDatas pour l'éditer sans conflits avec les autres threads.
                                    _levelDatas.Add(tempPointY)
                                End SyncLock
                            Next
                            levelBase_X += 1 'Avancons dans la carte
                            level_X += waveLenght
                        Case 1
                            pointY = CInt((_levelBaseDatas.Item(levelBase_i) - 128) * (level_X / 11000))
                            'Création d'une pente plus douce lorsque la différence de hauteur entre ce point et le précédent est trop grande :
                            If pointY - lastPointY > (10 + CInt(level_X / 4000)) Then
                                pointY = lastPointY + CInt(Math.Round(((level_X Mod (2.7 + level_X / 800000)) + 0) / 2 - 0.5))
                            End If
                            '50 est la distance pour laquelle chaque "pallier" se répète.
                            levelBase_X = level_X \ 50

                            SyncLock _levelDatas_Lock 'On prends possession de _levelDatas pour l'éditer sans conflits avec les autres threads.
                                _levelDatas.Add(pointY)
                            End SyncLock
                        Case 2
                            pointY = CInt((_levelBaseDatas.Item(levelBase_i) - 128) * (level_X / 20000))
                            If pointY - lastPointY > CInt(level_X / 450) Then
                                pointY = lastPointY + CInt(level_X / 450)
                            End If

                            Dim waveLenght As Integer = CInt(Math.Abs((_levelBaseDatas.Item(levelBase_i_Plus) + _levelBaseDatas.Item(levelBase_i)) / 2) + 200) 'waveLenght est la distance pour laquelle chaque "pallier" se répète. Ici on répète dans le case plutôt que dans la boucle extérieure.
                            For i As Integer = 0 To waveLenght
                                'On créé la "vague" sinusoidale
                                Dim tempPointY As Integer = CInt(lastPointY + (pointY - lastPointY) * SmoothSine(i / waveLenght))

                                SyncLock _levelDatas_Lock 'On prends possession de _levelDatas pour l'éditer sans conflits avec les autres threads.
                                    _levelDatas.Add(tempPointY)
                                    _levelDimensionDifferenceDatas.Add(tempPointY)
                                End SyncLock
                            Next
                            levelBase_X += 1 'Avancons dans la carte
                            level_X += waveLenght
                        Case 3
                            pointY = CInt((_levelBaseDatas.Item(levelBase_i) - 128) * (level_X / 30000))
                            'Création d'une pente plus douce lorsque la différence de hauteur entre ce point et le précédent est trop grande :
                            If pointY - lastPointY > (10 + CInt(level_X / 4000)) Then
                                pointY = lastPointY + CInt(Math.Round(((level_X Mod (2.7 + level_X / 80000)) + 0) / 2 - 0.5))
                            End If
                            '50 est la distance pour laquelle chaque "pallier" se répète.
                            levelBase_X = level_X \ 50

                            SyncLock _levelDatas_Lock 'On prends possession de _levelDatas pour l'éditer sans conflits avec les autres threads.
                                _levelDatas.Add(pointY)
                                _levelDimensionDifferenceDatas.Add(pointY)
                            End SyncLock
                    End Select
                Next
                SyncLock _levelGenerationNotCompleted_Lock
                    _levelGenerationNotCompleted = False
                End SyncLock
            End Sub)

        levelGenerationThread.Start() 'On exécute en parrallèle (sur un autre core) le thread qu'on viens de créer.
        Thread.Sleep(100) 'Laisson un peu de temps au thread commencé précédemment pour qu'il ait au moins un début de généré. 

    End Sub

    Private Function SmoothSine(ByVal intervalZeroToOne As Double) As Double
        'Renvoie une fonction où est-ce que le point 0 est égal à 0, où le point 0.5 est égal à 0.5 et où le point 1 égal à 1. La fonction croit selon une vague sinusoidale, donc la partie de la fonction de 0 à 0.5 renvera une valeur plus petite en Y comparé au X (non-linéaire), et la partie de la fonction de 0.5 à 1 renverra une valeur un peu plus grande en Y qu'en X. Seuls les 3 premiers points mentionnés prouvent la croissance de la fonction et portent à croire qu'elle est linéaire, mais elle se déforme en vague, adoucissant le taux de variation (pente) au point 0 et 1 à un taux de 0 (donc plat). 
        Return (Math.Sin((2 * intervalZeroToOne - 1) * (Math.PI / 2)) + 1) / 2
    End Function




    ''Sub qui s'occupe de faire apparaître les batteries dans le niveau. On prends en paramètre où la voiture est rendu dans le niveau afin qui puisse générer les batteries dynamiquement, au besoin. 
    Private Sub GenBatteries()

        Dim nextBatteryPos As Integer = 7000
        Dim nextBatteryInterval As Integer = 7000
        Dim nextBatteryIntervalToAdd As Integer = 350

        For x = 0 To _maxLevelWidthCount Step 50
            If nextBatteryPos = x Then
                _LevelBatteries.Add(x)
                nextBatteryInterval += nextBatteryIntervalToAdd
                nextBatteryPos += nextBatteryInterval
            End If
        Next

    End Sub

End Class
