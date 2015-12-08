Public Class FormFocusingRecorded

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        My.Settings.skipAuto = SkipAutoFtextbox.Text
        My.Settings.Save()
        savefocus()
        saveAutofocusarray()
        'MsgBox(My.Settings.skipAuto)
        Me.Close()
    End Sub

    Public Sub savefocus()
        For i = 1 To 144
            My.Settings.recordedfocus(i) = recordedfocus(i).ToString
        Next
        My.Settings.Save()
    End Sub

    Public Sub saveAutofocusarray()
        For i = 1 To 199
            My.Settings.Autofocusarray(i) = AutofocusArray(i).ToString
        Next

    End Sub


    Private Sub FormFocusingRecorded_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'MsgBox(My.Settings.skipAuto)

        setvaluesfromArray()
        TextBox1.Text = My.Settings.positionToAutofocus
        SkipAutoFtextbox.Text = My.Settings.skipAuto

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        For i = 1 To 144
            recordedfocus(i) = 0
        Next
        setvaluesfromArray()
    End Sub

    Sub setvaluesfromArray()
        NumericUpDown1.Value = recordedfocus(1)
        NumericUpDown2.Value = recordedfocus(2)
        NumericUpDown3.Value = recordedfocus(3)
        NumericUpDown4.Value = recordedfocus(4)
        NumericUpDown5.Value = recordedfocus(5)
        NumericUpDown6.Value = recordedfocus(6)
        NumericUpDown7.Value = recordedfocus(7)
        NumericUpDown8.Value = recordedfocus(8)
        NumericUpDown9.Value = recordedfocus(9)
        NumericUpDown10.Value = recordedfocus(10)
        NumericUpDown11.Value = recordedfocus(11)
        NumericUpDown12.Value = recordedfocus(12)
        NumericUpDown13.Value = recordedfocus(13)
        NumericUpDown14.Value = recordedfocus(14)
        NumericUpDown15.Value = recordedfocus(15)
        NumericUpDown16.Value = recordedfocus(16)
        NumericUpDown17.Value = recordedfocus(17)
        NumericUpDown18.Value = recordedfocus(18)
        NumericUpDown19.Value = recordedfocus(19)
        NumericUpDown20.Value = recordedfocus(20)
        NumericUpDown21.Value = recordedfocus(21)
        NumericUpDown22.Value = recordedfocus(22)
        NumericUpDown23.Value = recordedfocus(23)
        NumericUpDown24.Value = recordedfocus(24)
        NumericUpDown25.Value = recordedfocus(25)
        NumericUpDown26.Value = recordedfocus(26)
        NumericUpDown27.Value = recordedfocus(27)
        NumericUpDown28.Value = recordedfocus(28)
        NumericUpDown29.Value = recordedfocus(29)
        NumericUpDown30.Value = recordedfocus(30)
        NumericUpDown31.Value = recordedfocus(31)
        NumericUpDown32.Value = recordedfocus(32)
        NumericUpDown33.Value = recordedfocus(33)
        NumericUpDown34.Value = recordedfocus(34)
        NumericUpDown35.Value = recordedfocus(35)
        NumericUpDown36.Value = recordedfocus(36)
        NumericUpDown37.Value = recordedfocus(37)
        NumericUpDown38.Value = recordedfocus(38)
        NumericUpDown39.Value = recordedfocus(39)
        NumericUpDown40.Value = recordedfocus(40)
        NumericUpDown41.Value = recordedfocus(41)
        NumericUpDown42.Value = recordedfocus(42)
        NumericUpDown43.Value = recordedfocus(43)
        NumericUpDown44.Value = recordedfocus(44)
        NumericUpDown45.Value = recordedfocus(45)
        NumericUpDown46.Value = recordedfocus(46)
        NumericUpDown47.Value = recordedfocus(47)
        NumericUpDown48.Value = recordedfocus(48)
        NumericUpDown49.Value = recordedfocus(49)
        NumericUpDown50.Value = recordedfocus(50)
        NumericUpDown51.Value = recordedfocus(51)
        NumericUpDown52.Value = recordedfocus(52)
        NumericUpDown53.Value = recordedfocus(53)
        NumericUpDown54.Value = recordedfocus(54)
        NumericUpDown55.Value = recordedfocus(55)
        NumericUpDown56.Value = recordedfocus(56)
        NumericUpDown57.Value = recordedfocus(57)
        NumericUpDown58.Value = recordedfocus(58)
        NumericUpDown59.Value = recordedfocus(59)
        NumericUpDown60.Value = recordedfocus(60)
        NumericUpDown61.Value = recordedfocus(61)
        NumericUpDown62.Value = recordedfocus(62)
        NumericUpDown63.Value = recordedfocus(63)
        NumericUpDown64.Value = recordedfocus(64)
        NumericUpDown65.Value = recordedfocus(65)
        NumericUpDown66.Value = recordedfocus(66)
        NumericUpDown67.Value = recordedfocus(67)
        NumericUpDown68.Value = recordedfocus(68)
        NumericUpDown69.Value = recordedfocus(69)
        NumericUpDown70.Value = recordedfocus(70)
        NumericUpDown71.Value = recordedfocus(71)
        NumericUpDown72.Value = recordedfocus(72)
        NumericUpDown73.Value = recordedfocus(73)
        NumericUpDown74.Value = recordedfocus(74)
        NumericUpDown75.Value = recordedfocus(75)
        NumericUpDown76.Value = recordedfocus(76)
        NumericUpDown77.Value = recordedfocus(77)
        NumericUpDown78.Value = recordedfocus(78)
        NumericUpDown79.Value = recordedfocus(79)
        NumericUpDown80.Value = recordedfocus(80)
        NumericUpDown81.Value = recordedfocus(81)
        NumericUpDown82.Value = recordedfocus(82)
        NumericUpDown83.Value = recordedfocus(83)
        NumericUpDown84.Value = recordedfocus(84)
        NumericUpDown85.Value = recordedfocus(85)
        NumericUpDown86.Value = recordedfocus(86)
        NumericUpDown87.Value = recordedfocus(87)
        NumericUpDown88.Value = recordedfocus(88)
        NumericUpDown89.Value = recordedfocus(89)
        NumericUpDown90.Value = recordedfocus(90)
        NumericUpDown91.Value = recordedfocus(91)
        NumericUpDown92.Value = recordedfocus(92)
        NumericUpDown93.Value = recordedfocus(93)
        NumericUpDown94.Value = recordedfocus(94)
        NumericUpDown95.Value = recordedfocus(95)
        NumericUpDown96.Value = recordedfocus(96)
        NumericUpDown97.Value = recordedfocus(97)
        NumericUpDown98.Value = recordedfocus(98)
        NumericUpDown99.Value = recordedfocus(99)
        NumericUpDown100.Value = recordedfocus(100)
        NumericUpDown101.Value = recordedfocus(101)
        NumericUpDown102.Value = recordedfocus(102)
        NumericUpDown103.Value = recordedfocus(103)
        NumericUpDown104.Value = recordedfocus(104)
        NumericUpDown105.Value = recordedfocus(105)
        NumericUpDown106.Value = recordedfocus(106)
        NumericUpDown107.Value = recordedfocus(107)
        NumericUpDown108.Value = recordedfocus(108)
        NumericUpDown109.Value = recordedfocus(109)
        NumericUpDown110.Value = recordedfocus(110)
        NumericUpDown111.Value = recordedfocus(111)
        NumericUpDown112.Value = recordedfocus(112)
        NumericUpDown113.Value = recordedfocus(113)
        NumericUpDown114.Value = recordedfocus(114)
        NumericUpDown115.Value = recordedfocus(115)
        NumericUpDown116.Value = recordedfocus(116)
        NumericUpDown117.Value = recordedfocus(117)
        NumericUpDown118.Value = recordedfocus(118)
        NumericUpDown119.Value = recordedfocus(119)
        NumericUpDown120.Value = recordedfocus(120)
        NumericUpDown121.Value = recordedfocus(121)
        NumericUpDown122.Value = recordedfocus(122)
        NumericUpDown123.Value = recordedfocus(123)
        NumericUpDown124.Value = recordedfocus(124)
        NumericUpDown125.Value = recordedfocus(125)
        NumericUpDown126.Value = recordedfocus(126)
        NumericUpDown127.Value = recordedfocus(127)
        NumericUpDown128.Value = recordedfocus(128)
        NumericUpDown129.Value = recordedfocus(129)
        NumericUpDown130.Value = recordedfocus(130)
        NumericUpDown131.Value = recordedfocus(131)
        NumericUpDown132.Value = recordedfocus(132)
        NumericUpDown133.Value = recordedfocus(133)
        NumericUpDown134.Value = recordedfocus(134)
        NumericUpDown135.Value = recordedfocus(135)
        NumericUpDown136.Value = recordedfocus(136)
        NumericUpDown137.Value = recordedfocus(137)
        NumericUpDown138.Value = recordedfocus(138)
        NumericUpDown139.Value = recordedfocus(139)
        NumericUpDown140.Value = recordedfocus(140)
        NumericUpDown141.Value = recordedfocus(141)
        NumericUpDown142.Value = recordedfocus(142)
        NumericUpDown143.Value = recordedfocus(143)
        NumericUpDown144.Value = recordedfocus(144)

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        loadvalues()

    End Sub

    Sub loadvalues()
        For i = 1 To 144
            recordedfocus(i) = CInt(My.Settings.recordedfocus(i))
        Next
        setvaluesfromArray()


        For i = 1 To 200
            AutofocusArray(i) = CInt(My.Settings.Autofocusarray(i))
        Next


    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        calcForPos1()
        NumericUpDown1.Value = 0 - max
    End Sub
    Dim max As Integer = 0
    Public Sub calcForPos1()
        max = 0
        For i = 2 To 144
            max = max + recordedfocus(i)
        Next
        recordedfocus(1) = 0 - max
    End Sub



    Private Sub NumericUpDown1_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        recordedfocus(1) = NumericUpDown1.Value
    End Sub

    Private Sub NumericUpDown2_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        recordedfocus(2) = NumericUpDown2.Value

    End Sub

    Private Sub NumericUpDown3_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown3.ValueChanged
        recordedfocus(3) = NumericUpDown3.Value

    End Sub

    Private Sub NumericUpDown4_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown4.ValueChanged
        recordedfocus(4) = NumericUpDown4.Value

    End Sub

    Private Sub NumericUpDown5_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown5.ValueChanged
        recordedfocus(5) = NumericUpDown5.Value

    End Sub

    Private Sub NumericUpDown6_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown6.ValueChanged
        recordedfocus(6) = NumericUpDown6.Value

    End Sub

    Private Sub NumericUpDown7_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown7.ValueChanged
        recordedfocus(7) = NumericUpDown7.Value

    End Sub

    Private Sub NumericUpDown8_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown8.ValueChanged
        recordedfocus(8) = NumericUpDown8.Value

    End Sub

    Private Sub NumericUpDown9_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown9.ValueChanged
        recordedfocus(9) = NumericUpDown9.Value

    End Sub

    Private Sub NumericUpDown10_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown10.ValueChanged
        recordedfocus(10) = NumericUpDown10.Value

    End Sub

    Private Sub NumericUpDown11_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown11.ValueChanged
        recordedfocus(11) = NumericUpDown11.Value

    End Sub

    Private Sub NumericUpDown12_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown12.ValueChanged
        recordedfocus(12) = NumericUpDown12.Value

    End Sub

    Private Sub NumericUpDown13_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown13.ValueChanged
        recordedfocus(13) = NumericUpDown13.Value

    End Sub

    Private Sub NumericUpDown14_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown14.ValueChanged
        recordedfocus(14) = NumericUpDown14.Value

    End Sub

    Private Sub NumericUpDown15_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown15.ValueChanged
        recordedfocus(15) = NumericUpDown15.Value

    End Sub

    Private Sub NumericUpDown16_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown16.ValueChanged
        recordedfocus(16) = NumericUpDown16.Value

    End Sub

    Private Sub NumericUpDown17_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown17.ValueChanged
        recordedfocus(17) = NumericUpDown17.Value

    End Sub

    Private Sub NumericUpDown18_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown18.ValueChanged
        recordedfocus(18) = NumericUpDown18.Value

    End Sub

    Private Sub NumericUpDown19_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown19.ValueChanged
        recordedfocus(19) = NumericUpDown19.Value
    End Sub
    Private Sub NumericUpDown20_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown20.ValueChanged
        recordedfocus(20) = NumericUpDown20.Value
    End Sub
    Private Sub NumericUpDown21_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown21.ValueChanged
        recordedfocus(21) = NumericUpDown21.Value
    End Sub
    Private Sub NumericUpDown22_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown22.ValueChanged
        recordedfocus(22) = NumericUpDown22.Value
    End Sub
    Private Sub NumericUpDown23_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown23.ValueChanged
        recordedfocus(23) = NumericUpDown23.Value
    End Sub
    Private Sub NumericUpDown24_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown24.ValueChanged
        recordedfocus(24) = NumericUpDown24.Value
    End Sub
    Private Sub NumericUpDown25_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown25.ValueChanged
        recordedfocus(25) = NumericUpDown25.Value
    End Sub
    Private Sub NumericUpDown26_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown26.ValueChanged
        recordedfocus(26) = NumericUpDown26.Value
    End Sub
    Private Sub NumericUpDown27_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown27.ValueChanged
        recordedfocus(27) = NumericUpDown27.Value
    End Sub
    Private Sub NumericUpDown28_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown28.ValueChanged
        recordedfocus(28) = NumericUpDown28.Value
    End Sub
    Private Sub NumericUpDown29_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown29.ValueChanged
        recordedfocus(29) = NumericUpDown29.Value
    End Sub
    Private Sub NumericUpDown30_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown30.ValueChanged
        recordedfocus(30) = NumericUpDown30.Value
    End Sub
    Private Sub NumericUpDown31_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown31.ValueChanged
        recordedfocus(31) = NumericUpDown31.Value
    End Sub
    Private Sub NumericUpDown32_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown32.ValueChanged
        recordedfocus(32) = NumericUpDown32.Value
    End Sub
    Private Sub NumericUpDown33_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown33.ValueChanged
        recordedfocus(33) = NumericUpDown33.Value
    End Sub
    Private Sub NumericUpDown34_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown34.ValueChanged
        recordedfocus(34) = NumericUpDown34.Value
    End Sub
    Private Sub NumericUpDown35_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown35.ValueChanged
        recordedfocus(35) = NumericUpDown35.Value
    End Sub
    Private Sub NumericUpDown36_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown36.ValueChanged
        recordedfocus(36) = NumericUpDown36.Value
    End Sub
    Private Sub NumericUpDown37_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown37.ValueChanged
        recordedfocus(37) = NumericUpDown37.Value
    End Sub
    Private Sub NumericUpDown38_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown38.ValueChanged
        recordedfocus(38) = NumericUpDown38.Value
    End Sub
    Private Sub NumericUpDown39_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown39.ValueChanged
        recordedfocus(39) = NumericUpDown39.Value
    End Sub
    Private Sub NumericUpDown40_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown40.ValueChanged
        recordedfocus(40) = NumericUpDown40.Value
    End Sub
    Private Sub NumericUpDown41_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown41.ValueChanged
        recordedfocus(41) = NumericUpDown41.Value
    End Sub
    Private Sub NumericUpDown42_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown42.ValueChanged
        recordedfocus(42) = NumericUpDown42.Value
    End Sub
    Private Sub NumericUpDown43_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown43.ValueChanged
        recordedfocus(43) = NumericUpDown43.Value
    End Sub
    Private Sub NumericUpDown44_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown44.ValueChanged
        recordedfocus(44) = NumericUpDown44.Value
    End Sub
    Private Sub NumericUpDown45_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown45.ValueChanged
        recordedfocus(45) = NumericUpDown45.Value
    End Sub
    Private Sub NumericUpDown46_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown46.ValueChanged
        recordedfocus(46) = NumericUpDown46.Value
    End Sub
    Private Sub NumericUpDown47_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown47.ValueChanged
        recordedfocus(47) = NumericUpDown47.Value
    End Sub
    Private Sub NumericUpDown48_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown48.ValueChanged
        recordedfocus(48) = NumericUpDown48.Value
    End Sub
    Private Sub NumericUpDown49_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown49.ValueChanged
        recordedfocus(49) = NumericUpDown49.Value
    End Sub
    Private Sub NumericUpDown50_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown50.ValueChanged
        recordedfocus(50) = NumericUpDown50.Value
    End Sub
    Private Sub NumericUpDown51_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown51.ValueChanged
        recordedfocus(51) = NumericUpDown51.Value
    End Sub
    Private Sub NumericUpDown52_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown52.ValueChanged
        recordedfocus(52) = NumericUpDown52.Value
    End Sub
    Private Sub NumericUpDown53_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown53.ValueChanged
        recordedfocus(53) = NumericUpDown53.Value
    End Sub
    Private Sub NumericUpDown54_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown54.ValueChanged
        recordedfocus(54) = NumericUpDown54.Value
    End Sub
    Private Sub NumericUpDown55_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown55.ValueChanged
        recordedfocus(55) = NumericUpDown55.Value
    End Sub
    Private Sub NumericUpDown56_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown56.ValueChanged
        recordedfocus(56) = NumericUpDown56.Value
    End Sub
    Private Sub NumericUpDown57_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown57.ValueChanged
        recordedfocus(57) = NumericUpDown57.Value
    End Sub
    Private Sub NumericUpDown58_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown58.ValueChanged
        recordedfocus(58) = NumericUpDown58.Value
    End Sub
    Private Sub NumericUpDown59_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown59.ValueChanged
        recordedfocus(59) = NumericUpDown59.Value
    End Sub
    Private Sub NumericUpDown60_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown60.ValueChanged
        recordedfocus(60) = NumericUpDown60.Value
    End Sub
    Private Sub NumericUpDown61_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown61.ValueChanged
        recordedfocus(61) = NumericUpDown61.Value
    End Sub
    Private Sub NumericUpDown62_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown62.ValueChanged
        recordedfocus(62) = NumericUpDown62.Value
    End Sub
    Private Sub NumericUpDown63_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown63.ValueChanged
        recordedfocus(63) = NumericUpDown63.Value
    End Sub
    Private Sub NumericUpDown64_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown64.ValueChanged
        recordedfocus(64) = NumericUpDown64.Value
    End Sub
    Private Sub NumericUpDown65_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown65.ValueChanged
        recordedfocus(65) = NumericUpDown65.Value
    End Sub
    Private Sub NumericUpDown66_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown66.ValueChanged
        recordedfocus(66) = NumericUpDown66.Value
    End Sub
    Private Sub NumericUpDown67_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown67.ValueChanged
        recordedfocus(67) = NumericUpDown67.Value
    End Sub
    Private Sub NumericUpDown68_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown68.ValueChanged
        recordedfocus(68) = NumericUpDown68.Value
    End Sub
    Private Sub NumericUpDown69_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown69.ValueChanged
        recordedfocus(69) = NumericUpDown69.Value
    End Sub
    Private Sub NumericUpDown70_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown70.ValueChanged
        recordedfocus(70) = NumericUpDown70.Value
    End Sub
    Private Sub NumericUpDown71_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown71.ValueChanged
        recordedfocus(71) = NumericUpDown71.Value
    End Sub
    Private Sub NumericUpDown72_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NumericUpDown72.ValueChanged
        recordedfocus(72) = NumericUpDown72.Value
    End Sub
    Private Sub NumericUpDown73_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown73.ValueChanged
        recordedfocus(73) = NumericUpDown73.Value
    End Sub
    Private Sub NumericUpDown74_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown74.ValueChanged
        recordedfocus(74) = NumericUpDown74.Value
    End Sub
    Private Sub NumericUpDown75_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown75.ValueChanged
        recordedfocus(75) = NumericUpDown75.Value
    End Sub
    Private Sub NumericUpDown76_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown76.ValueChanged
        recordedfocus(76) = NumericUpDown76.Value
    End Sub
    Private Sub NumericUpDown77_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown77.ValueChanged
        recordedfocus(77) = NumericUpDown77.Value
    End Sub
    Private Sub NumericUpDown78_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown78.ValueChanged
        recordedfocus(78) = NumericUpDown78.Value
    End Sub
    Private Sub NumericUpDown79_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown79.ValueChanged
        recordedfocus(79) = NumericUpDown79.Value
    End Sub
    Private Sub NumericUpDown80_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown80.ValueChanged
        recordedfocus(80) = NumericUpDown80.Value
    End Sub
    Private Sub NumericUpDown81_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown81.ValueChanged
        recordedfocus(81) = NumericUpDown81.Value
    End Sub
    Private Sub NumericUpDown82_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown82.ValueChanged
        recordedfocus(82) = NumericUpDown82.Value
    End Sub
    Private Sub NumericUpDown83_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown83.ValueChanged
        recordedfocus(83) = NumericUpDown83.Value
    End Sub
    Private Sub NumericUpDown84_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown84.ValueChanged
        recordedfocus(84) = NumericUpDown84.Value
    End Sub
    Private Sub NumericUpDown85_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown85.ValueChanged
        recordedfocus(85) = NumericUpDown85.Value
    End Sub
    Private Sub NumericUpDown86_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown86.ValueChanged
        recordedfocus(86) = NumericUpDown86.Value
    End Sub
    Private Sub NumericUpDown87_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown87.ValueChanged
        recordedfocus(87) = NumericUpDown87.Value
    End Sub
    Private Sub NumericUpDown88_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown88.ValueChanged
        recordedfocus(88) = NumericUpDown88.Value
    End Sub
    Private Sub NumericUpDown89_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown89.ValueChanged
        recordedfocus(89) = NumericUpDown89.Value
    End Sub
    Private Sub NumericUpDown90_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown90.ValueChanged
        recordedfocus(90) = NumericUpDown90.Value
    End Sub
    Private Sub NumericUpDown91_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown91.ValueChanged
        recordedfocus(91) = NumericUpDown91.Value
    End Sub
    Private Sub NumericUpDown92_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown92.ValueChanged
        recordedfocus(92) = NumericUpDown92.Value
    End Sub
    Private Sub NumericUpDown93_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown93.ValueChanged
        recordedfocus(93) = NumericUpDown93.Value
    End Sub
    Private Sub NumericUpDown94_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown94.ValueChanged
        recordedfocus(94) = NumericUpDown94.Value
    End Sub
    Private Sub NumericUpDown95_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown95.ValueChanged
        recordedfocus(95) = NumericUpDown95.Value
    End Sub
    Private Sub NumericUpDown96_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown96.ValueChanged
        recordedfocus(96) = NumericUpDown96.Value
    End Sub
    Private Sub NumericUpDown97_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown97.ValueChanged
        recordedfocus(97) = NumericUpDown97.Value
    End Sub
    Private Sub NumericUpDown98_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown98.ValueChanged
        recordedfocus(98) = NumericUpDown98.Value
    End Sub
    Private Sub NumericUpDown99_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown99.ValueChanged
        recordedfocus(99) = NumericUpDown99.Value
    End Sub
    Private Sub NumericUpDown100_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown100.ValueChanged
        recordedfocus(100) = NumericUpDown100.Value
    End Sub
    Private Sub NumericUpDown101_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown101.ValueChanged
        recordedfocus(101) = NumericUpDown101.Value
    End Sub
    Private Sub NumericUpDown102_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown102.ValueChanged
        recordedfocus(102) = NumericUpDown102.Value
    End Sub
    Private Sub NumericUpDown103_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown103.ValueChanged
        recordedfocus(103) = NumericUpDown103.Value
    End Sub
    Private Sub NumericUpDown104_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown104.ValueChanged
        recordedfocus(104) = NumericUpDown104.Value
    End Sub
    Private Sub NumericUpDown105_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown105.ValueChanged
        recordedfocus(105) = NumericUpDown105.Value
    End Sub
    Private Sub NumericUpDown106_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown106.ValueChanged
        recordedfocus(106) = NumericUpDown106.Value
    End Sub
    Private Sub NumericUpDown107_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown107.ValueChanged
        recordedfocus(107) = NumericUpDown107.Value
    End Sub
    Private Sub NumericUpDown108_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown108.ValueChanged
        recordedfocus(108) = NumericUpDown108.Value
    End Sub
    Private Sub NumericUpDown109_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown109.ValueChanged
        recordedfocus(109) = NumericUpDown109.Value
    End Sub
    Private Sub NumericUpDown110_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown110.ValueChanged
        recordedfocus(110) = NumericUpDown110.Value
    End Sub
    Private Sub NumericUpDown111_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown111.ValueChanged
        recordedfocus(111) = NumericUpDown111.Value
    End Sub
    Private Sub NumericUpDown112_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown112.ValueChanged
        recordedfocus(112) = NumericUpDown112.Value
    End Sub
    Private Sub NumericUpDown113_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown113.ValueChanged
        recordedfocus(113) = NumericUpDown113.Value
    End Sub
    Private Sub NumericUpDown114_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown114.ValueChanged
        recordedfocus(114) = NumericUpDown114.Value
    End Sub
    Private Sub NumericUpDown115_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown115.ValueChanged
        recordedfocus(115) = NumericUpDown115.Value
    End Sub
    Private Sub NumericUpDown116_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown116.ValueChanged
        recordedfocus(116) = NumericUpDown116.Value
    End Sub
    Private Sub NumericUpDown117_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown117.ValueChanged
        recordedfocus(117) = NumericUpDown117.Value
    End Sub
    Private Sub NumericUpDown118_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown118.ValueChanged
        recordedfocus(118) = NumericUpDown118.Value
    End Sub
    Private Sub NumericUpDown119_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown119.ValueChanged
        recordedfocus(119) = NumericUpDown119.Value
    End Sub
    Private Sub NumericUpDown120_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown120.ValueChanged
        recordedfocus(120) = NumericUpDown120.Value
    End Sub
    Private Sub NumericUpDown121_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown121.ValueChanged
        recordedfocus(121) = NumericUpDown121.Value
    End Sub
    Private Sub NumericUpDown122_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown122.ValueChanged
        recordedfocus(122) = NumericUpDown122.Value
    End Sub
    Private Sub NumericUpDown123_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown123.ValueChanged
        recordedfocus(123) = NumericUpDown123.Value
    End Sub
    Private Sub NumericUpDown124_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown124.ValueChanged
        recordedfocus(124) = NumericUpDown124.Value
    End Sub
    Private Sub NumericUpDown125_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown125.ValueChanged
        recordedfocus(125) = NumericUpDown125.Value
    End Sub
    Private Sub NumericUpDown126_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown126.ValueChanged
        recordedfocus(126) = NumericUpDown126.Value
    End Sub
    Private Sub NumericUpDown127_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown127.ValueChanged
        recordedfocus(127) = NumericUpDown127.Value
    End Sub
    Private Sub NumericUpDown128_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown128.ValueChanged
        recordedfocus(128) = NumericUpDown128.Value
    End Sub
    Private Sub NumericUpDown129_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown129.ValueChanged
        recordedfocus(129) = NumericUpDown129.Value
    End Sub
    Private Sub NumericUpDown130_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown130.ValueChanged
        recordedfocus(130) = NumericUpDown130.Value
    End Sub
    Private Sub NumericUpDown131_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown131.ValueChanged
        recordedfocus(131) = NumericUpDown131.Value
    End Sub
    Private Sub NumericUpDown132_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown132.ValueChanged
        recordedfocus(132) = NumericUpDown132.Value
    End Sub
    Private Sub NumericUpDown133_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown133.ValueChanged
        recordedfocus(133) = NumericUpDown133.Value
    End Sub
    Private Sub NumericUpDown134_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown134.ValueChanged
        recordedfocus(134) = NumericUpDown134.Value
    End Sub
    Private Sub NumericUpDown135_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown135.ValueChanged
        recordedfocus(135) = NumericUpDown135.Value
    End Sub
    Private Sub NumericUpDown136_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown136.ValueChanged
        recordedfocus(136) = NumericUpDown136.Value
    End Sub
    Private Sub NumericUpDown137_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown137.ValueChanged
        recordedfocus(137) = NumericUpDown137.Value
    End Sub
    Private Sub NumericUpDown138_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown138.ValueChanged
        recordedfocus(138) = NumericUpDown138.Value
    End Sub
    Private Sub NumericUpDown139_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown139.ValueChanged
        recordedfocus(139) = NumericUpDown139.Value
    End Sub
    Private Sub NumericUpDown140_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown140.ValueChanged
        recordedfocus(140) = NumericUpDown140.Value
    End Sub
    Private Sub NumericUpDown141_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown141.ValueChanged
        recordedfocus(141) = NumericUpDown141.Value
    End Sub
    Private Sub NumericUpDown142_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown142.ValueChanged
        recordedfocus(142) = NumericUpDown142.Value
    End Sub
    Private Sub NumericUpDown143_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown143.ValueChanged
        recordedfocus(143) = NumericUpDown143.Value
    End Sub
    Private Sub NumericUpDown144_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown144.ValueChanged
        recordedfocus(144) = NumericUpDown144.Value
    End Sub





    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Calc1to36()
        NumericUpDown1.Value = 0 - max
    End Sub
    Public Sub Calc1to36()
        max = 0
        For i = 2 To 36
            max = max + recordedfocus(i)
        Next
        recordedfocus(1) = 0 - max
    End Sub

    Dim intervalSplit As String()
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        updateAutoF()
    End Sub

    Dim astring As String = ""

    Private Sub updateAutoF()
        If TextBox1.Text.EndsWith("-") Or TextBox1.Text.EndsWith(",") Or TextBox1.Text.StartsWith(",") Or TextBox1.Text.StartsWith("-") Then
            Exit Sub
        End If
        'MsgBox("clard")
        For i = 1 To 97
            AutofocusArray(i) = 0
        Next
        

        If TextBox1.Text = "" Then
            Exit Sub
        End If

        For Each j As String In TextBox1.Text.Split(New Char() {","c})
            If j.Contains("-") Then
                intervalSplit = j.Split(New Char() {"-"c})
                For p = Convert.ToInt16(intervalSplit(0)) To Convert.ToInt16(intervalSplit(1)) Step Convert.ToInt16(SkipAutoFtextbox.Text)
                    'MsgBox(p)
                    AutofocusArray(p) = 1
                Next
            Else
                'MsgBox(j)
                AutofocusArray(Convert.ToInt32(j)) = 1
                'AutofocusArray(j) = 1
            End If

        Next


        For Each h As String In AutofocusArray
            astring = astring + h + ","
        Next

        My.Settings.positionToAutofocus = TextBox1.Text
        My.Settings.Save()
    End Sub

    Private Sub SkipAutoFtextbox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SkipAutoFtextbox.TextChanged
        
        updateAutoF()
        
        'MsgBox(My.Settings.skipAuto)
        If SkipAutoFtextbox.Text = "" Then
            SkipAutoFtextbox.Text = "1"
        End If
        'MsgBox(My.Settings.skipAuto)
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        astring = ""
        For Each h As String In AutofocusArray
            astring = astring + h + ","
        Next
        MsgBox(astring.Remove(0, 2))
    End Sub

   
    Private Sub Button6_Click(sender As Object, e As EventArgs)
        MsgBox(My.Settings.recordedfocus(1))
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxEnableAF.CheckedChanged
        If CheckBoxEnableAF.Checked Then
            TextBox1.Enabled = True
        Else
            TextBox1.Text = 199
            TextBox1.Enabled = False
        End If
    End Sub
End Class