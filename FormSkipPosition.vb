Public Class FormSkipPosition

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        'FIRST MAKE SURE ALL ELEMENTS OF VECTOR 'SKIP' ARE SET TO FALSE:
        'For i As Integer = 0 To 39
        '    skip(i) = False
        'Next

        For i = 0 To 40
            skip(i) = False
            skipOnlyLight(i) = False
        Next


        'THEN SET THE VALUES INPUT BY THE USER TO TRUE:
        If TextBox1.Text <> "" Then
            skip(Convert.ToInt16(TextBox1.Text)) = True
            skipOnlyLight(Convert.ToInt16(TextBox1.Text)) = CheckBox1.Checked
        End If
        My.Settings.SkipSettings(0) = TextBox1.Text
        My.Settings.SkipLightIOnly(0) = CheckBox1.Checked.ToString

        If TextBox2.Text <> "" Then
            skip(Convert.ToInt16(TextBox2.Text)) = True
            skipOnlyLight(Convert.ToInt16(TextBox2.Text)) = CheckBox2.Checked

        End If
        My.Settings.SkipSettings(1) = TextBox2.Text
        My.Settings.SkipLightIOnly(1) = CheckBox2.Checked.ToString


        If TextBox3.Text <> "" Then
            skip(Convert.ToInt16(TextBox3.Text)) = True
            skipOnlyLight(Convert.ToInt16(TextBox3.Text)) = CheckBox3.Checked

        End If
        My.Settings.SkipSettings(2) = TextBox3.Text
        My.Settings.SkipLightIOnly(2) = CheckBox3.Checked.ToString


        If TextBox4.Text <> "" Then
            skip(Convert.ToInt16(TextBox4.Text)) = True
            skipOnlyLight(Convert.ToInt16(TextBox4.Text)) = CheckBox4.Checked
        End If
        My.Settings.SkipSettings(3) = TextBox4.Text
        My.Settings.SkipLightIOnly(3) = CheckBox4.Checked.ToString

        If TextBox5.Text <> "" Then
            skip(Convert.ToInt16(TextBox5.Text)) = True
            skipOnlyLight(Convert.ToInt16(TextBox5.Text)) = CheckBox5.Checked

        End If
        My.Settings.SkipSettings(4) = TextBox5.Text
        My.Settings.SkipLightIOnly(4) = CheckBox5.Checked.ToString


        If TextBox6.Text <> "" Then
            skip(Convert.ToInt16(TextBox6.Text)) = True
            skipOnlyLight(Convert.ToInt16(TextBox6.Text)) = CheckBox6.Checked
        End If
        My.Settings.SkipSettings(5) = TextBox6.Text
        My.Settings.SkipLightIOnly(5) = CheckBox6.Checked.ToString


        If TextBox7.Text <> "" Then
            skip(Convert.ToInt16(TextBox7.Text)) = True
            skipOnlyLight(Convert.ToInt16(TextBox7.Text)) = CheckBox7.Checked
        End If
        My.Settings.SkipSettings(6) = TextBox7.Text
        My.Settings.SkipLightIOnly(6) = CheckBox7.Checked.ToString




        'MessageBox.Show(skip(1).ToString + " " + skip(2).ToString + " " + skip(3).ToString)
        My.Settings.Save()

        Me.Close()

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub FormSkipPosition_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If My.Settings.SkipSettings(0) <> "" Then
            TextBox1.Text = My.Settings.SkipSettings(0)
        End If
        If My.Settings.SkipSettings(1) <> "" Then
            TextBox2.Text = My.Settings.SkipSettings(1)
        End If
        If My.Settings.SkipSettings(2) <> "" Then
            TextBox3.Text = My.Settings.SkipSettings(2)
        End If
        If My.Settings.SkipSettings(3) <> "" Then
            TextBox4.Text = My.Settings.SkipSettings(3)
        End If
        If My.Settings.SkipSettings(4) <> "" Then
            TextBox5.Text = My.Settings.SkipSettings(4)
        End If
        If My.Settings.SkipSettings(5) <> "" Then
            TextBox6.Text = My.Settings.SkipSettings(5)
        End If
        If My.Settings.SkipSettings(6) <> "" Then
            TextBox7.Text = My.Settings.SkipSettings(6)
        End If




        If My.Settings.SkipLightIOnly(0) = "True" Then
            CheckBox1.Checked = True
        End If
        If My.Settings.SkipLightIOnly(1) = "True" Then
            CheckBox2.Checked = True
        End If
        If My.Settings.SkipLightIOnly(2) = "True" Then
            CheckBox3.Checked = True
        End If
        If My.Settings.SkipLightIOnly(3) = "True" Then
            CheckBox4.Checked = True
        End If
        If My.Settings.SkipLightIOnly(4) = "True" Then
            CheckBox5.Checked = True
        End If
        If My.Settings.SkipLightIOnly(5) = "True" Then
            CheckBox6.Checked = True
        End If
        If My.Settings.SkipLightIOnly(6) = "True" Then
            CheckBox7.Checked = True
        End If


    End Sub
End Class