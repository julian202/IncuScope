Imports System
Imports System.IO

Public Class FormNote
    Dim FILE_NAME As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("You must select a computer and a folder!")
            Exit Sub
        End If
        If CheckBox1.Checked = False And CheckBox2.Checked = False Then
            MsgBox("You must select a camera!")
            Exit Sub
        End If
        My.Settings.thisPC = thisPC
        My.Settings.Save()

        imagefolder = TextBox3.Text
        imageSubfolder = TextBox2.Text


        sessionfolder = imagefolder & "\" & Format(Date.Now.Month, "00") & Format(Date.Now.Day, "00") & Date.Now.Year.ToString.Substring(2) & "-" & imageSubfolder
        sessionfolderOriginal = sessionfolder
        My.Settings.imageSubFolder = imageSubfolder
        My.Settings.Save()


        If (Not System.IO.Directory.Exists(sessionfolder)) Then
            Try
                System.IO.Directory.CreateDirectory(sessionfolder)
            Catch ex As Exception
                MsgBox("That directory doesn't exist, you probably got the drive letter wrong")
            End Try

        End If

        If My.Settings.checkb21 = True Then  'save to vanadium checkb
            If (Not System.IO.Directory.Exists(vanadiumfolder)) Then
                Try
                    System.IO.Directory.CreateDirectory(vanadiumfolder)
                Catch ex As Exception
                End Try
            End If
        End If


        infotext = TextBox1.Text
        'FILE_NAME = imagefolder & "\Now\info.txt"  'creates a text file
        FILE_NAME = sessionFolder & "\info.txt"  'creates a text file

        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, False)
        objWriter.WriteLine(infotext)
        objWriter.Close()


        Try
            FileCopy(directoryInfo.FullName & "\ConfigAndErrorFiles" & "\" & Label6.Text, sessionfolder & "\" & Label6.Text)
            'MsgBox(directoryInfo.FullName & "\ConfigAndErrorFiles" & "\" & Label6.Text)
        Catch ex As Exception
            MsgBox("No scale bar image will be saved! " & ex.Message)
        End Try
        If My.Settings.checkb21 = True Then  'save to vanadium checkb
            Try
                FileCopy(directoryInfo.FullName & "\ConfigAndErrorFiles" & "\" & Label6.Text, vanadiumfolder & "\" & Label6.Text)
            Catch ex As Exception
                MsgBox("No scale bar image will be saved to Vanadium! " & ex.Message)
            End Try
        End If

        Me.Close()
    End Sub

    Private Sub FormNote_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

       


        If My.Settings.imagewithCam1 = True Then
            CheckBox1.Checked = True
            usingCam1 = True
        Else
            CheckBox1.Checked = False
            usingCam1 = False
        End If
        If My.Settings.imagewithCam2 = True Then
            CheckBox2.Checked = True
            usingCam2 = True
        Else
            CheckBox2.Checked = False
            usingCam2 = False
        End If

        imagefolder = drive & "\Images"
        imageSubfolder = My.Settings.imageSubFolder
        TextBox3.Text = imagefolder
        changeLabel5text()

        TextBox2.Text = imageSubfolder
        'If CheckBox1.Checked Then
        '    TextBox2.Text = Format(Date.Now.Month, "00") & Format(Date.Now.Day, "00") & Date.Now.Year.ToString.Substring(2) & "-" & imageSubfolder
        'Else
        '    TextBox2.Text = imageSubfolder
        'End If

        thisPC = My.Settings.thisPC
        If thisPC = "1" Then
            ComboBox1.SelectedIndex = 0
        End If
        If thisPC = "2" Then
            ComboBox1.SelectedIndex = 1
        End If
        If thisPC = "3" Then
            ComboBox1.SelectedIndex = 2
        End If
        If thisPC = "4" Then
            ComboBox1.SelectedIndex = 3
        End If
        If thisPC = "5" Then
            ComboBox1.SelectedIndex = 4
        End If
        If thisPC = "6" Then
            ComboBox1.SelectedIndex = 5
        End If
        'Try
        '    Using sr As New StreamReader(sessionfolder & "\info.txt")
        '        TextBox1.Text = sr.ReadToEnd()
        '    End Using
        'Catch
        '    ' MsgBox("The file could not be read:")
        'End Try


        TextBox1.Text = "Computer: PC-" & thisPC
        TextBox1.Text += Environment.NewLine
        TextBox1.Text += "Shutters Cam1: BF=" & shutterBF.absValue.ToString & " Fl=" & shutterfluo.absValue.ToString
        TextBox1.Text += Environment.NewLine
        TextBox1.Text += "Shutters Cam2: BF=" & shutterBFb.absValue.ToString & " Fl=" & shutterfluob.absValue.ToString
        TextBox1.Text += Environment.NewLine
        TextBox1.Text += "Interval=" & interval & " Secs"
        TextBox1.Text += Environment.NewLine
        TextBox1.Text += "Gain: Cam1=" & My.Settings.gainCam1 & " Cam2=" & My.Settings.gainCam2

        Label5.Text = "\\" & vanadiumName & "\D\Images\PC-" & thisPC & "\" & TextBox2.Text
        Label6.Text = My.Settings.ScaleBarImage

        If My.Settings.checkb21 = False Then  'save to vanadium checkb
            Label5.Text = "You have selected to not save to Vanadium"
            Label4.Enabled = False
            Label5.Enabled = False
        Else
            Label4.Enabled = True
            Label5.Enabled = True
        End If

        If Incuscope.My.Settings.AutoStartCheckBox = True Then
            ''MsgBox("Autostart checkbox has been checked...  Autostarting")
            ''StartSleepingThread()
            ''WaitEvent.WaitOne()
            SendKeys.Send("{ENTER}")
        End If


        If My.Settings.ScaleBarImage10XChecked = True Then
            CheckBox10X.Checked = True
            Try
                Label6.Text = My.Settings.ScaleBarImage10X
                'MsgBox("1")
            Catch ex As Exception
            End Try
        Else
            CheckBox10X.Checked = False
        End If
        If My.Settings.ScaleBarImage4XChecked = True Then
            CheckBox4X.Checked = True
            Try
                Label6.Text = My.Settings.ScaleBarImage4X
                'MsgBox("2")
            Catch ex As Exception
            End Try
        Else
            CheckBox4X.Checked = False
        End If
    End Sub






    Private Sub RadioButton1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            TextBox3.Text = "C:\Images"
            Refresh()
            thisPC = "Xeon"
            changeLabel5text()

        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            TextBox3.Text = "S:\Images"
            Refresh()
            thisPC = "ZipfelLab"
            changeLabel5text()

        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked Then
            TextBox3.Text = "C:\Images"
            Refresh()
            thisPC = "Dell"
            changeLabel5text()

        End If
    End Sub


    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged
        changeLabel5text()
    End Sub

    Sub changeLabel5text()
        Label5.Text = "\\" & vanadiumName & "\D\Images\PC-" & thisPC & "\" & Format(Date.Now.Month, "00") & Format(Date.Now.Day, "00") & Date.Now.Year.ToString.Substring(2) & "-" & TextBox2.Text
        vanadiumfolder = Label5.Text
        vanadiumfolderOriginal = vanadiumfolder

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        cancelSession = True
        Me.Close()
    End Sub




    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            usingCam1 = True
            My.Settings.imagewithCam1 = True
            My.Settings.Save()
        Else
            usingCam1 = False
            My.Settings.imagewithCam1 = False
            My.Settings.Save()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            usingCam2 = True
            My.Settings.imagewithCam2 = True
            My.Settings.Save()
        Else
            usingCam2 = False
            My.Settings.imagewithCam2 = False
            My.Settings.Save()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

        thisPC = ComboBox1.SelectedItem.ToString
        'MsgBox("this pc is " & thisPC)
        changeLabel5text()
    End Sub


    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Label6.Text = InputBox("Enter the correct scale bar file name (not path) in the folder " & directoryInfo.FullName & "\ConfigAndErrorFiles" & "\", "Change Scale Bar File", Label6.Text)

        If CheckBox10X.Checked Then
            My.Settings.ScaleBarImage10X = Label6.Text
        End If
        If CheckBox4X.Checked Then
            My.Settings.ScaleBarImage4X = Label6.Text
        End If
        If CheckBoxOther.Checked Then
            My.Settings.ScaleBarImage = Label6.Text
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox10X_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox10X.CheckedChanged
        If CheckBox10X.Checked Then
            My.Settings.ScaleBarImage10XChecked = True
            Try
                Label6.Text = My.Settings.ScaleBarImage10X
            Catch ex As Exception
            End Try
        Else
            My.Settings.ScaleBarImage10XChecked = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox4X_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4X.CheckedChanged
        If CheckBox4X.Checked Then
            My.Settings.ScaleBarImage4XChecked = True
            Try
                Label6.Text = My.Settings.ScaleBarImage4X
            Catch ex As Exception
            End Try
        Else
            My.Settings.ScaleBarImage4XChecked = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBoxOther_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxOther.CheckedChanged

    End Sub
End Class