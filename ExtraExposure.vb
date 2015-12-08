Public Class ExtraExposure

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        'THEN SET THE VALUES INPUT BY THE USER TO TRUE:
        If TextBox1.Text <> "" Then
            exposureArray(0) = Convert.ToDouble(TextBox1.Text)
            My.Settings.ExposureSettings(0) = TextBox1.Text
        End If

        If TextBox2.Text <> "" Then
            exposureArray(1) = Convert.ToDouble(TextBox2.Text)
            My.Settings.ExposureSettings(1) = TextBox2.Text
        End If

        If TextBox3.Text <> "" Then
            exposureArray(2) = Convert.ToDouble(TextBox3.Text)
            My.Settings.ExposureSettings(2) = TextBox3.Text
        End If

        If TextBox4.Text <> "" Then
            exposureArray(3) = Convert.ToDouble(TextBox4.Text)
            My.Settings.ExposureSettings(3) = TextBox4.Text
        End If

        If TextBox5.Text <> "" Then
            exposureArray(4) = Convert.ToDouble(TextBox5.Text)
            My.Settings.ExposureSettings(4) = TextBox5.Text
        End If

        If TextBox6.Text <> "" Then
            exposureArray(5) = Convert.ToDouble(TextBox6.Text)
            My.Settings.ExposureSettings(5) = TextBox6.Text
        End If

        If TextBox7.Text <> "" Then
            exposureArray(6) = Convert.ToDouble(TextBox7.Text)
            My.Settings.ExposureSettings(6) = TextBox7.Text
        End If

        If TextBox8.Text <> "" Then
            exposureArray(7) = Convert.ToDouble(TextBox8.Text)
            My.Settings.ExposureSettings(7) = TextBox8.Text
        End If

        If TextBox9.Text <> "" Then
            exposureArray(8) = Convert.ToDouble(TextBox9.Text)
            My.Settings.ExposureSettings(8) = TextBox9.Text
        End If

        If TextBox10.Text <> "" Then
            exposureArray(9) = Convert.ToDouble(TextBox10.Text)
            My.Settings.ExposureSettings(9) = TextBox10.Text
        End If

        If TextBox11.Text <> "" Then
            exposureArray(10) = Convert.ToDouble(TextBox11.Text)
            My.Settings.ExposureSettings(10) = TextBox11.Text
        End If

        If TextBox12.Text <> "" Then
            exposureArray(11) = Convert.ToDouble(TextBox12.Text)
            My.Settings.ExposureSettings(11) = TextBox12.Text
        End If

        If TextBox13.Text <> "" Then
            exposureArray(12) = Convert.ToDouble(TextBox13.Text)
            My.Settings.ExposureSettings(12) = TextBox13.Text
        End If





        If TextBox14.Text <> "" Then
            exposureArray(13) = Convert.ToDouble(TextBox14.Text)
            My.Settings.ExposureSettings(13) = TextBox14.Text
        End If

        If TextBox15.Text <> "" Then
            exposureArray(14) = Convert.ToDouble(TextBox15.Text)
            My.Settings.ExposureSettings(14) = TextBox15.Text
        End If

        If TextBox16.Text <> "" Then
            exposureArray(15) = Convert.ToDouble(TextBox16.Text)
            My.Settings.ExposureSettings(15) = TextBox16.Text
        End If

        If TextBox17.Text <> "" Then
            exposureArray(16) = Convert.ToDouble(TextBox17.Text)
            My.Settings.ExposureSettings(16) = TextBox17.Text
        End If

        If TextBox18.Text <> "" Then
            exposureArray(17) = Convert.ToDouble(TextBox18.Text)
            My.Settings.ExposureSettings(17) = TextBox18.Text
        End If

        If TextBox19.Text <> "" Then
            exposureArray(18) = Convert.ToDouble(TextBox19.Text)
            My.Settings.ExposureSettings(18) = TextBox19.Text
        End If

        If TextBox20.Text <> "" Then
            exposureArray(19) = Convert.ToDouble(TextBox20.Text)
            My.Settings.ExposureSettings(19) = TextBox20.Text
        End If

        If TextBox21.Text <> "" Then
            exposureArray(20) = Convert.ToDouble(TextBox21.Text)
            My.Settings.ExposureSettings(20) = TextBox21.Text
        End If

        If TextBox22.Text <> "" Then
            exposureArray(21) = Convert.ToDouble(TextBox22.Text)
            My.Settings.ExposureSettings(21) = TextBox22.Text
        End If

        If TextBox23.Text <> "" Then
            exposureArray(22) = Convert.ToDouble(TextBox23.Text)
            My.Settings.ExposureSettings(22) = TextBox23.Text
        End If

        If TextBox24.Text <> "" Then
            exposureArray(23) = Convert.ToDouble(TextBox24.Text)
            My.Settings.ExposureSettings(23) = TextBox24.Text
        End If

        If TextBox25.Text <> "" Then
            exposureArray(24) = Convert.ToDouble(TextBox25.Text)
            My.Settings.ExposureSettings(24) = TextBox25.Text
        End If

        If TextBox26.Text <> "" Then
            exposureArray(25) = Convert.ToDouble(TextBox26.Text)
            My.Settings.ExposureSettings(25) = TextBox26.Text
        End If
















        'MessageBox.Show(skip(1).ToString + " " + skip(2).ToString + " " + skip(3).ToString)
        My.Settings.Save()

        Me.Close()
    End Sub

    Private Sub ExtraExposure_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        TextBox1.Text = My.Settings.ExposureSettings(0)
        TextBox2.Text = My.Settings.ExposureSettings(1)
        TextBox3.Text = My.Settings.ExposureSettings(2)
        TextBox4.Text = My.Settings.ExposureSettings(3)
        TextBox5.Text = My.Settings.ExposureSettings(4)
        TextBox6.Text = My.Settings.ExposureSettings(5)
        TextBox7.Text = My.Settings.ExposureSettings(6)
        TextBox8.Text = My.Settings.ExposureSettings(7)
        TextBox9.Text = My.Settings.ExposureSettings(8)
        TextBox10.Text = My.Settings.ExposureSettings(9)
        TextBox11.Text = My.Settings.ExposureSettings(10)
        TextBox12.Text = My.Settings.ExposureSettings(11)
        TextBox13.Text = My.Settings.ExposureSettings(12)
        TextBox14.Text = My.Settings.ExposureSettings(13)
        TextBox15.Text = My.Settings.ExposureSettings(14)
        TextBox16.Text = My.Settings.ExposureSettings(15)
        TextBox17.Text = My.Settings.ExposureSettings(16)
        TextBox18.Text = My.Settings.ExposureSettings(17)
        TextBox19.Text = My.Settings.ExposureSettings(18)
        TextBox20.Text = My.Settings.ExposureSettings(19)
        TextBox21.Text = My.Settings.ExposureSettings(20)
        TextBox22.Text = My.Settings.ExposureSettings(21)
        TextBox23.Text = My.Settings.ExposureSettings(22)
        TextBox24.Text = My.Settings.ExposureSettings(23)
        TextBox25.Text = My.Settings.ExposureSettings(24)
        TextBox26.Text = My.Settings.ExposureSettings(25)

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'THEN SET THE VALUES INPUT BY THE USER TO TRUE:
        If TextBox1.Text <> "" Then
            exposureArray(0) = Convert.ToDouble(TextBox1.Text)
            My.Settings.ExposureSettings2(0) = TextBox1.Text
        End If

        If TextBox2.Text <> "" Then
            exposureArray(1) = Convert.ToDouble(TextBox2.Text)
            My.Settings.ExposureSettings2(1) = TextBox2.Text
        End If

        If TextBox3.Text <> "" Then
            exposureArray(2) = Convert.ToDouble(TextBox3.Text)
            My.Settings.ExposureSettings2(2) = TextBox3.Text
        End If

        If TextBox4.Text <> "" Then
            exposureArray(3) = Convert.ToDouble(TextBox4.Text)
            My.Settings.ExposureSettings2(3) = TextBox4.Text
        End If

        If TextBox5.Text <> "" Then
            exposureArray(4) = Convert.ToDouble(TextBox5.Text)
            My.Settings.ExposureSettings2(4) = TextBox5.Text
        End If

        If TextBox6.Text <> "" Then
            exposureArray(5) = Convert.ToDouble(TextBox6.Text)
            My.Settings.ExposureSettings2(5) = TextBox6.Text
        End If

        If TextBox7.Text <> "" Then
            exposureArray(6) = Convert.ToDouble(TextBox7.Text)
            My.Settings.ExposureSettings2(6) = TextBox7.Text
        End If

        If TextBox8.Text <> "" Then
            exposureArray(7) = Convert.ToDouble(TextBox8.Text)
            My.Settings.ExposureSettings2(7) = TextBox8.Text
        End If

        If TextBox9.Text <> "" Then
            exposureArray(8) = Convert.ToDouble(TextBox9.Text)
            My.Settings.ExposureSettings2(8) = TextBox9.Text
        End If

        If TextBox10.Text <> "" Then
            exposureArray(9) = Convert.ToDouble(TextBox10.Text)
            My.Settings.ExposureSettings2(9) = TextBox10.Text
        End If

        If TextBox11.Text <> "" Then
            exposureArray(10) = Convert.ToDouble(TextBox11.Text)
            My.Settings.ExposureSettings2(10) = TextBox11.Text
        End If

        If TextBox12.Text <> "" Then
            exposureArray(11) = Convert.ToDouble(TextBox12.Text)
            My.Settings.ExposureSettings2(11) = TextBox12.Text
        End If

        If TextBox13.Text <> "" Then
            exposureArray(12) = Convert.ToDouble(TextBox13.Text)
            My.Settings.ExposureSettings2(12) = TextBox13.Text
        End If





        If TextBox14.Text <> "" Then
            exposureArray(13) = Convert.ToDouble(TextBox14.Text)
            My.Settings.ExposureSettings2(13) = TextBox14.Text
        End If

        If TextBox15.Text <> "" Then
            exposureArray(14) = Convert.ToDouble(TextBox15.Text)
            My.Settings.ExposureSettings2(14) = TextBox15.Text
        End If

        If TextBox16.Text <> "" Then
            exposureArray(15) = Convert.ToDouble(TextBox16.Text)
            My.Settings.ExposureSettings2(15) = TextBox16.Text
        End If

        If TextBox17.Text <> "" Then
            exposureArray(16) = Convert.ToDouble(TextBox17.Text)
            My.Settings.ExposureSettings2(16) = TextBox17.Text
        End If

        If TextBox18.Text <> "" Then
            exposureArray(17) = Convert.ToDouble(TextBox18.Text)
            My.Settings.ExposureSettings2(17) = TextBox18.Text
        End If

        If TextBox19.Text <> "" Then
            exposureArray(18) = Convert.ToDouble(TextBox19.Text)
            My.Settings.ExposureSettings2(18) = TextBox19.Text
        End If

        If TextBox20.Text <> "" Then
            exposureArray(19) = Convert.ToDouble(TextBox20.Text)
            My.Settings.ExposureSettings2(19) = TextBox20.Text
        End If

        If TextBox21.Text <> "" Then
            exposureArray(20) = Convert.ToDouble(TextBox21.Text)
            My.Settings.ExposureSettings2(20) = TextBox21.Text
        End If

        If TextBox22.Text <> "" Then
            exposureArray(21) = Convert.ToDouble(TextBox22.Text)
            My.Settings.ExposureSettings2(21) = TextBox22.Text
        End If

        If TextBox23.Text <> "" Then
            exposureArray(22) = Convert.ToDouble(TextBox23.Text)
            My.Settings.ExposureSettings2(22) = TextBox23.Text
        End If

        If TextBox24.Text <> "" Then
            exposureArray(23) = Convert.ToDouble(TextBox24.Text)
            My.Settings.ExposureSettings2(23) = TextBox24.Text
        End If

        If TextBox25.Text <> "" Then
            exposureArray(24) = Convert.ToDouble(TextBox25.Text)
            My.Settings.ExposureSettings2(24) = TextBox25.Text
        End If

        If TextBox26.Text <> "" Then
            exposureArray(25) = Convert.ToDouble(TextBox26.Text)
            My.Settings.ExposureSettings2(25) = TextBox26.Text
        End If





        'MessageBox.Show(skip(1).ToString + " " + skip(2).ToString + " " + skip(3).ToString)
        My.Settings.Save()


    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click

        TextBox1.Text = My.Settings.ExposureSettings2(0)
        TextBox2.Text = My.Settings.ExposureSettings2(1)
        TextBox3.Text = My.Settings.ExposureSettings2(2)
        TextBox4.Text = My.Settings.ExposureSettings2(3)
        TextBox5.Text = My.Settings.ExposureSettings2(4)
        TextBox6.Text = My.Settings.ExposureSettings2(5)
        TextBox7.Text = My.Settings.ExposureSettings2(6)
        TextBox8.Text = My.Settings.ExposureSettings2(7)
        TextBox9.Text = My.Settings.ExposureSettings2(8)
        TextBox10.Text = My.Settings.ExposureSettings2(9)
        TextBox11.Text = My.Settings.ExposureSettings2(10)
        TextBox12.Text = My.Settings.ExposureSettings2(11)
        TextBox13.Text = My.Settings.ExposureSettings2(12)
        TextBox14.Text = My.Settings.ExposureSettings2(13)
        TextBox15.Text = My.Settings.ExposureSettings2(14)
        TextBox16.Text = My.Settings.ExposureSettings2(15)
        TextBox17.Text = My.Settings.ExposureSettings2(16)
        TextBox18.Text = My.Settings.ExposureSettings2(17)
        TextBox19.Text = My.Settings.ExposureSettings2(18)
        TextBox20.Text = My.Settings.ExposureSettings2(19)
        TextBox21.Text = My.Settings.ExposureSettings2(20)
        TextBox22.Text = My.Settings.ExposureSettings2(21)
        TextBox23.Text = My.Settings.ExposureSettings2(22)
        TextBox24.Text = My.Settings.ExposureSettings2(23)
        TextBox25.Text = My.Settings.ExposureSettings2(24)
        TextBox26.Text = My.Settings.ExposureSettings2(25)
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        TextBox1.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
        TextBox5.Text = 0
        TextBox6.Text = 0
        TextBox7.Text = 0
        TextBox8.Text = 0
        TextBox9.Text = 0
        TextBox10.Text = 0
        TextBox11.Text = 0
        TextBox12.Text = 0
        TextBox13.Text = 0
        TextBox14.Text = 0
        TextBox15.Text = 0
        TextBox16.Text = 0
        TextBox17.Text = 0
        TextBox18.Text = 0
        TextBox19.Text = 0
        TextBox20.Text = 0
        TextBox21.Text = 0
        TextBox22.Text = 0
        TextBox23.Text = 0
        TextBox24.Text = 0
        TextBox25.Text = 0
        TextBox26.Text = 0
    End Sub
End Class