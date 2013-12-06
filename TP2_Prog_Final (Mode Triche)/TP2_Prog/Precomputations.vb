Module Precomputations

    'Une fonction comme Math.Sin() personnalisée où les calculs sont mis en cache dans un dictionnary (un genre d'array...). L'avantage du dictionnary comparé à une list ou un array est que son "Big O' notation" est presque linéaire et n'augmente presque pas avec le nomnre d'éléments ajoutés. 
    Private _sin_Lock As New Object 'Pour synchroniser la fonction en case de multi-threading.
    Private _precomputedSins As New Dictionary(Of Double, Double)

    Public Function Sin(ByVal angle As Double) As Double
        Dim answer As Double
        SyncLock _sin_Lock
            If _precomputedSins.ContainsKey(angle) Then 'Figure dans le cache
                answer = _precomputedSins.Item(angle)
            Else 'Ne figure pas déjà dans le cache... Calcul et ajout au cache
                answer = Math.Sin(angle / 180 * Math.PI)
                _precomputedSins.Item(angle) = answer
            End If
        End SyncLock
        Return answer
    End Function


    'Une fonction comme Math.Sin() personnalisée où les calculs sont mis en cache dans un dictionnary (un genre d'array...). L'avantage du dictionnary comparé à une list ou un array est que son "Big O' notation" est presque linéaire et n'augmente presque pas avec le nomnre d'éléments ajoutés. 
    Private _cos_Lock As New Object 'Pour synchroniser la fonction en case de multi-threading.
    Private _precomputedCos As New Dictionary(Of Double, Double)

    Public Function Cos(ByVal angle As Double) As Double
        Dim answer As Double
        SyncLock _cos_Lock
            If _precomputedCos.ContainsKey(angle) Then 'Figure dans le cache
                answer = _precomputedCos.Item(angle)
            Else 'Ne figure pas déjà dans le cache... Calcul et ajout au cache
                answer = Math.Cos(angle / 180 * Math.PI)
                _precomputedCos.Item(angle) = answer
            End If
        End SyncLock
        Return answer
    End Function


    'Une fonction comme Math.Asin(opp/adj) personnalisée où les calculs sont mis en cache dans un dictionnary.
    Private _arcSin_Lock As New Object 'Pour synchroniser la fonction en case de multi-threading.
    Private _precomputedArcSins As New Dictionary(Of Double, Dictionary(Of Double, Double))

    Public Function ArcSin(ByVal oppositeSide As Double, ByVal adjacentSide As Double) As Double
        Dim answer As Double
        'SyncLock _arcSin_Lock
        '    If _precomputedArcSins.ContainsKey(oppositeSide) AndAlso _precomputedArcSins.Item(oppositeSide).ContainsKey(adjacentSide) Then 'Figure déjà dans le cache.
        '        answer = _precomputedArcSins.Item(oppositeSide).Item(adjacentSide)
        '    Else 'Calcul et ajout au cache
        '        If adjacentSide <> 0 Then answer = Math.Asin(oppositeSide / adjacentSide)

        '        'Ajout au cache :
        '        If _precomputedArcSins.ContainsKey(oppositeSide) Then
        '            _precomputedArcSins.Item(oppositeSide).Add(adjacentSide, answer) 'Ajouté.
        '        Else
        '            Dim tmpDict As New Dictionary(Of Double, Double)
        '            tmpDict.Add(adjacentSide, answer) 'Est ajouté dans la ligne suivante.
        '            _precomputedArcSins.Add(adjacentSide, tmpDict)
        '        End If
        '    End If
        'End SyncLock
        If adjacentSide <> 0 Then answer = Math.Asin(oppositeSide / adjacentSide)
        Return answer
    End Function


    'Le théorème de pythagore mis en cache pour un calcul plus rapide. Ici, trouver le côté long (c) des trois côtés. Théorème : c=sqrt(a^2+b^2).
    Private _precomputedPythagorean_C_Lock As New Object 'Pour synchroniser la fonction en case de multi-threading.
    Private _precomputedPythagorean_C As New Dictionary(Of Double, Dictionary(Of Double, Double))
    
    Public Function Pythagorean_C(ByVal a As Double, ByVal b As Double) As Double
        Dim answer As Double
        SyncLock _precomputedPythagorean_C_Lock
            If _precomputedPythagorean_C.ContainsKey(a) AndAlso _precomputedPythagorean_C.Item(a).ContainsKey(b) Then 'Figure déjà dans le cache.
                answer = _precomputedPythagorean_C.Item(a).Item(b)
            Else 'Calcul et ajout au cache
                answer = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)) 'La racine carrée (Sqrt) et les Pow() coûtent cher au processeur. 

                'Ajout au cache :
                If _precomputedPythagorean_C.ContainsKey(a) Then
                    _precomputedPythagorean_C.Item(a).Add(b, answer) 'Ajouté.
                Else
                    Dim tmpDict As New Dictionary(Of Double, Double)
                    tmpDict.Add(b, answer) 'Est ajouté dans la ligne suivante.
                    _precomputedPythagorean_C.Add(a, tmpDict)
                End If
            End If
        End SyncLock
        Return answer
    End Function
    

    'Le théorème de pythagore mis en cache pour un calcul plus rapide. Ici, trouver un côté court (a ou b) des trois côtés. Théorème : a=sqrt(c^2-b^2). "a" et "b" sont interchangeables.
    Private _precomputedPythagorean_A_Lock As New Object 'Pour synchroniser la fonction en case de multi-threading.
    Private _precomputedPythagorean_A As New Dictionary(Of Double, Dictionary(Of Double, Double))
    
    Public Function Pythagorean_A(ByVal c As Double, ByVal b As Double) As Double
        Dim answer As Double
        SyncLock _precomputedPythagorean_A_Lock
            If (_precomputedPythagorean_A.ContainsKey(c) AndAlso _precomputedPythagorean_A.Item(c).ContainsKey(b)) OrElse (_precomputedPythagorean_A.ContainsKey(b) AndAlso _precomputedPythagorean_A.Item(b).ContainsKey(c)) Then 'Figure déjà dans le cache.
                answer = _precomputedPythagorean_A.Item(c).Item(b)
            Else 'Calcul et ajout au cache
                answer = Math.Sqrt(Math.Pow(c, 2) - Math.Pow(b, 2)) 'La racine carrée (Sqrt) et les Pow() coûtent cher au processeur. 

                'Ajout au cache :
                If _precomputedPythagorean_A.ContainsKey(c) Then
                    _precomputedPythagorean_A.Item(c).Add(b, answer) 'Ajouté.
                Else
                    Dim tmpDict_b As New Dictionary(Of Double, Double)
                    tmpDict_b.Add(b, answer) 'Est ajouté dans la ligne suivante.
                    _precomputedPythagorean_A.Add(c, tmpDict_b)
                End If
            End If
        End SyncLock
        Return answer
    End Function




    'Cette fonction calcule la hauteur en y des points d'un demi-cercle de roue, indexé -rayon jusqu'à rayon, le 0 étant la valeur centrale. Elle possède un cache. 
    Private _wheelBorders_Lock As New Object
    Private _wheelBorders As New List(Of Integer)
    
    Public Function WheelBorders(ByVal radius As Integer) As List(Of Integer)
        SyncLock _wheelBorders_lock
            If _wheelBorders.Count = 0 Then
                For x As Integer = 0 To radius * 2
                    'On ajoute à la liste la hauteur du point trouvé par le biais du théorème de pythagore. 
                    Dim borderHeight_Y As Integer = CInt(Precomputations.pythagorean_A(radius, x - radius))
                    _wheelBorders.Add(borderHeight_Y)

                    'La boucle suivante préparera une bonne partie du cache de nos fonctions personnalisées.
                    Dim c As Double
                    Dim a As Double
                    For y As Integer = -borderHeight_Y To borderHeight_Y
                        c = Precomputations.pythagorean_C(x, y)
                        a = Precomputations.pythagorean_A(c, x)
                        a = Precomputations.pythagorean_A(c, y)
                    Next
                Next
            End If
        End SyncLock
        Return _wheelBorders
    End Function




    'Cette fonction, si un chiffre est sous 0 (négatif), retourne 0. Ce calcul est beaucoup utilisé, donc je crée une fonction.
    Public Function UnderZeroEqualsZero(ByVal chiffre As Double) As Double
        If chiffre < 0 Then chiffre = 0
        Return chiffre
    End Function



End Module
