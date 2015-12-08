Public Class FormFocusing


    Dim mybool As Boolean
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        'transfer form variables to the global variables in the module
        If RadioButton1.Checked Then
            FchangeUp(1) = True
        End If
        If RadioButton2.Checked Then
            FchangeUp(1) = False
        End If

        'For i As Integer = 1 To 22


        '    If radio(i).Checked Then
        '        If i Mod 2 = 0 Then  'even
        '            mybool = True
        '        Else
        '            mybool = False
        '        End If
        '        FchangeUp(2) = mybool
        '    End If

        'Next

        If RadioButton3.Checked Then
            FchangeUp(2) = False
        End If
        If RadioButton4.Checked Then
            FchangeUp(2) = True
        End If
        If RadioButton5.Checked Then
            FchangeUp(3) = False
        End If
        If RadioButton6.Checked Then
            FchangeUp(3) = True
        End If
        If RadioButton7.Checked Then
            FchangeUp(4) = False
        End If
        If RadioButton8.Checked Then
            FchangeUp(4) = True
        End If
        If RadioButton9.Checked Then
            FchangeUp(5) = False
        End If
        If RadioButton10.Checked Then
            FchangeUp(5) = True
        End If
        If RadioButton11.Checked Then
            FchangeUp(6) = False
        End If
        If RadioButton12.Checked Then
            FchangeUp(6) = True
        End If
        If RadioButton13.Checked Then
            FchangeUp(7) = False
        End If
        If RadioButton14.Checked Then
            FchangeUp(7) = True
        End If
        If RadioButton15.Checked Then
            FchangeUp(8) = False
        End If
        If RadioButton16.Checked Then
            FchangeUp(8) = True
        End If
        If RadioButton17.Checked Then
            FchangeUp(9) = False
        End If
        If RadioButton18.Checked Then
            FchangeUp(9) = True
        End If
        If RadioButton19.Checked Then
            FchangeUp(10) = False
        End If
        If RadioButton20.Checked Then
            FchangeUp(10) = True
        End If
        If RadioButton21.Checked Then
            FchangeUp(11) = False
        End If
        If RadioButton22.Checked Then
            FchangeUp(11) = True
        End If



        'positionFchange(1) = NumericUpDown1.Value
        'positionFchange(2) = NumericUpDown3.Value
        For i As Integer = 1 To 11
            positionFchange(i) = numeric((2 * i) - 2).Value   '-2 because numeric() is an array that starts at 0
        Next


        'Ftimes1 = NumericUpDown2.Value
        'Ftimes2 = NumericUpDown4.Value
        'Ftimes3 = NumericUpDown6.Value
        'Ftimes4 = NumericUpDown8.Value
        For i As Integer = 1 To 11
            Ftimes(i) = numeric((2 * i) - 1).Value
        Next





        'transfer variables to settings file

        'My.Settings.Fchange1 = FchangeUp1
        'My.Settings.Fchange2 = FchangeUp2
        'My.Settings.Fchange3 = FchangeUp3
        'My.Settings.Fchange4 = FchangeUp4
        For i As Integer = 1 To 11

            My.Settings.FchangeSArray(i - 1) = FchangeUp(i)
        Next



        'My.Settings.positionsF(0) = positionFchange1
        'My.Settings.positionsF(1) = positionFchange2
        'My.Settings.positionsF(2) = positionFchange3
        'My.Settings.positionsF(3) = positionFchange4

        For i As Integer = 1 To 11
            My.Settings.positionsF(i - 1) = positionFchange(i)
        Next


        'My.Settings.Ftimes(0) = Ftimes1
        'My.Settings.Ftimes(1) = Ftimes2
        'My.Settings.Ftimes(2) = Ftimes3
        'My.Settings.Ftimes(3) = Ftimes4
        For i As Integer = 1 To 11
            My.Settings.Ftimes(i - 1) = Ftimes(i)
        Next


        My.Settings.Save()











        Me.Close()

    End Sub


    'Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
    '    positionFchange(1) = NumericUpDown1.Value
    'End Sub
    'Private Sub NumericUpDown3_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown3.ValueChanged
    '    positionFchange(2) = NumericUpDown3.Value
    'End Sub
    'Private Sub NumericUpDown5_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown5.ValueChanged
    '    positionFchange(3) = NumericUpDown5.Value
    'End Sub
    'Private Sub NumericUpDown7_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown7.ValueChanged
    '    positionFchange(4) = NumericUpDown7.Value
    'End Sub
    'Private Sub NumericUpDown9_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown9.ValueChanged
    '    positionFchange(5) = NumericUpDown7.Value
    'End Sub
    'Private Sub NumericUpDown11_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown11.ValueChanged
    '    positionFchange(6) = NumericUpDown7.Value
    'End Sub
    'Private Sub NumericUpDown13_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown13.ValueChanged
    '    positionFchange(7) = NumericUpDown7.Value
    'End Sub
    'Private Sub NumericUpDown15_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown15.ValueChanged
    '    positionFchange(8) = NumericUpDown7.Value
    'End Sub
    'Private Sub NumericUpDown17_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown17.ValueChanged
    '    positionFchange(9) = NumericUpDown7.Value
    'End Sub
    'Private Sub NumericUpDown19_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown19.ValueChanged
    '    positionFchange(10) = NumericUpDown7.Value
    'End Sub
    'Private Sub NumericUpDown21_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown21.ValueChanged
    '    positionFchange(11) = NumericUpDown7.Value
    'End Sub







    Dim numeric() As System.Windows.Forms.NumericUpDown
    Dim radio() As System.Windows.Forms.RadioButton
    Private Sub FormFocusing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

   
        numeric = {NumericUpDown1, NumericUpDown2, NumericUpDown3, NumericUpDown4, NumericUpDown5, NumericUpDown6, NumericUpDown7, NumericUpDown8, NumericUpDown9, NumericUpDown10, NumericUpDown11, NumericUpDown12, NumericUpDown13, NumericUpDown14, NumericUpDown15, NumericUpDown16, NumericUpDown17, NumericUpDown18, NumericUpDown19, NumericUpDown20, NumericUpDown21, NumericUpDown22}
        radio = {RadioButton1, RadioButton2, RadioButton3, RadioButton4, RadioButton5, RadioButton6, RadioButton7, RadioButton8, RadioButton9, RadioButton10, RadioButton11, RadioButton12, RadioButton13, RadioButton14, RadioButton15, RadioButton16, RadioButton17, RadioButton18, RadioButton19, RadioButton20, RadioButton21, RadioButton22}



        'refresh global variables with stored variables
        For i As Integer = 1 To 11
            positionFchange(i) = My.Settings.positionsF(i - 1)
        Next
        'positionFchange1 = My.Settings.positionsF(0)
        'positionFchange2 = My.Settings.positionsF(1)
        'positionFchange3 = My.Settings.positionsF(2)
        'positionFchange4 = My.Settings.positionsF(3)

        For i As Integer = 1 To 11
            Ftimes(i) = My.Settings.Ftimes(i - 1)
        Next
        'Ftimes1 = My.Settings.Ftimes(0)
        'Ftimes2 = My.Settings.Ftimes(1)
        'Ftimes3 = My.Settings.Ftimes(2)
        'Ftimes4 = My.Settings.Ftimes(3)


        For i As Integer = 1 To 11
            FchangeUp(i) = My.Settings.FchangeSArray(i - 1)
        Next
        'FchangeUp1 = My.Settings.Fchange1
        'FchangeUp2 = My.Settings.Fchange2
        'FchangeUp3 = My.Settings.Fchange3
        'FchangeUp4 = My.Settings.Fchange4

        'transfer global variables to form


        
        If FchangeUp(1) = True Then
            RadioButton1.Checked = True
        Else
            RadioButton2.Checked = True
        End If


        For i As Integer = 2 To 11

            If FchangeUp(i) = True Then
                radio((2 * i) - 1).Checked = True
            Else
                radio((2 * i) - 2).Checked = True
            End If

        Next
        'If FchangeUp2 = True Then
        '    RadioButton4.Checked = True
        'Else
        '    RadioButton3.Checked = True
        'End If
        'If FchangeUp3 = True Then
        '    RadioButton6.Checked = True
        'Else
        '    RadioButton5.Checked = True
        'End If
        'If FchangeUp4 = True Then
        '    RadioButton8.Checked = True
        'Else
        '    RadioButton7.Checked = True
        'End If



        For i As Integer = 1 To 11
            numeric((2 * i) - 2).Value = positionFchange(i)
        Next
        'NumericUpDown1.Value = positionFchange1
        'NumericUpDown3.Value = positionFchange2
        'NumericUpDown5.Value = positionFchange3
        'NumericUpDown7.Value = positionFchange4


        For i As Integer = 1 To 11
            numeric((2 * i - 1)).Value = Ftimes(i)
        Next
        'NumericUpDown2.Value = Ftimes1
        'NumericUpDown4.Value = Ftimes2
        'NumericUpDown6.Value = Ftimes3
        'NumericUpDown8.Value = Ftimes4





    End Sub

    'Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
    '    Ftimes1 = NumericUpDown2.Value
    'End Sub

    'Private Sub NumericUpDown4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown4.ValueChanged
    '    Ftimes2 = NumericUpDown4.Value
    'End Sub

    'Private Sub NumericUpDown6_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown6.ValueChanged
    '    Ftimes3 = NumericUpDown6.Value
    'End Sub

    'Private Sub NumericUpDown8_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown8.ValueChanged
    '    Ftimes4 = NumericUpDown8.Value
    'End Sub






    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        For i As Integer = 1 To 22
            numeric(i - 1).Value = 0
        Next
        'NumericUpDown1.Value = 0
        'NumericUpDown3.Value = 0
        'NumericUpDown5.Value = 0
        'NumericUpDown7.Value = 0
        'NumericUpDown2.Value = 0
        'NumericUpDown4.Value = 0
        'NumericUpDown6.Value = 0
        'NumericUpDown8.Value = 0

        For i As Integer = 2 To 11
            radio((2 * i) - 2).Checked = True
        Next
        RadioButton2.Checked = True
        'RadioButton2.Checked = True
        'RadioButton3.Checked = True
        'RadioButton5.Checked = True
        'RadioButton7.Checked = True
    End Sub

   
  
End Class