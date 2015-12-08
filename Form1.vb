Imports System
Imports System.Text
Imports System.IO
Imports System.Drawing
Imports FlyCapture2Managed
Imports System.Threading
Imports System.Net.Mail


Public Class Form1
    ''''''''for drag n drop'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Inherits System.Windows.Forms.Form
    Implements IMessageFilter

    Public Sub New()

        MyBase.New()
        InitializeComponent()
        Application.AddMessageFilter(Me)
        DragAcceptFiles(Me.Handle, True)

    End Sub

    Public Function PreFilterMessage(ByRef m As System.Windows.Forms.Message) As Boolean Implements IMessageFilter.PreFilterMessage

        If m.Msg = WM_DROPFILES Then

            'this code to handle multiple dropped files.. 
            'not really neccesary for this example
            Dim nfiles As Integer = DragQueryFile(m.WParam, -1, Nothing, 0)

            Dim i As Integer
            For i = 0 To nfiles
                Dim sb As StringBuilder = New StringBuilder(256)
                DragQueryFile(m.WParam, i, sb, 256)
                HandleDroppedFiles(sb.ToString())
            Next
            DragFinish(m.WParam)
            Return True
        End If
        Return False
    End Function

    Public Sub HandleDroppedFiles(ByVal file As String)
        If Len(file) > 0 Then
            LoadPicture(file)
        End If
    End Sub

    Public Function LoadPicture(ByVal File As String) As Boolean
        If Len(File) > 0 Then
            Dim b As Bitmap = New Bitmap(File)
            'PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox1.Image = b
            doautofhere() 'calculates and displays the aufocosing algorith for this dropped image.
            Return True
        End If
        Return False
    End Function

    Private Declare Function DragAcceptFiles Lib "shell32.dll" (ByVal hwnd As IntPtr, ByVal accept As Boolean) As Long

    Private Declare Function DragQueryFile Lib "shell32.dll" (ByVal hdrop As IntPtr, ByVal ifile As Integer,
                  ByVal fname As StringBuilder,
         ByVal fnsize As Integer) As Integer

    Private Declare Sub DragFinish Lib "Shell32.dll" (ByVal hdrop As IntPtr)

    Public Const WM_DROPFILES As Integer = 563
    ''''''''''''''''''''''''''''''''''
    '''''''''''''''''''''''''''''''''



    'Dim widthMS2000 As Integer = 72300 '960 '925 '900 '950 '-1000 '8182
    'Dim heigthMS2000 As Integer = 54250 '710  '-750  '6124
    'Dim widthMS4 As Integer = 490 '621
    'Dim heigthMS4 As Integer = -392 '-470
    Dim wellwidth As String '= My.Settings.wellwidth
    Dim wellheight As String ' = My.Settings.wellheight
    Dim mywidth As String ' = My.Settings.mywidth
    Dim myheigth As String ' = My.Settings.myheigth
    Dim configName As String

    Dim specialSesssion As Boolean
    Dim specialSesssionCount As Integer
    Dim versionMS2000 As Boolean '= My.Settings.versionMS2000 'versionMS2000 is MS-2000 stage.
    'Dim imagefolder As String = My.Settings.imagefolder '"S:\Images"
    Dim LaptopVersion As Boolean
    Dim speedRight As String = "speed x=2 y=2"
    Dim speedLeftHigh As String = "minspeed 1000"
    ' Dim speedLeftLow As String = "minspeed 30000"
    Dim speedLeft As String = "minspeed 10000"
    Dim accelRight As String = "AC x=4000 y=4000"
    Dim accelLeft As String = "rampslope 255"
    Dim grabThread As System.Threading.Thread
    Dim nextWellThread As System.Threading.Thread
    'Dim QueryFinished As System.Threading.Thread
    Dim version As FC2Version
    Dim newStr As StringBuilder
    Dim busMgr As ManagedBusManager
    Dim numCameras As UInt32
    Dim guid As ManagedPGRGuid
    Dim cam As ManagedCamera
    '  Dim cam2 As ManagedCamera
    Dim camInfo As CameraInfo
    Dim camInfo2 As CameraInfo
    Dim embeddedInfo As EmbeddedImageInfo
    Dim embeddedInfo2 As EmbeddedImageInfo
    Dim rawImage As ManagedImage = New ManagedImage()
    Dim convertedImage As ManagedImage = New ManagedImage()
    Dim bitmap As Bitmap
    Dim myControlDialog As FlyCapture2Managed.Gui.CameraControlDialog
    Dim myControlDialog2 As FlyCapture2Managed.Gui.CameraControlDialog

    Dim motorisOn As Boolean
    Dim motorUpon As Boolean
    Dim motorDownon As Boolean
    Dim Str As String
    Dim finefocusinterval As Integer
    Dim coarsefocusinterval As Integer
    Dim mychar() As Char
    Dim mytext As String
    Dim s1 As String
    Dim s2 As String
    Dim m1 As String
    Dim m2 As String
    Dim difs As Integer
    Dim difm As Integer
    Dim shutterTime As String
    Dim text1 As String
    Dim item As Integer
    Dim item2 As Integer
    Dim str2 As String
    Dim str3 As String
    Dim str4 As String
    Dim Xpos As String
    Dim Ypos As String
    Dim currentwellX As Integer
    Dim currentwellY As Integer
    Dim t1 As String
    Dim currentListItem As Integer = 0
    Dim coarse As Boolean
    Dim times As Integer
    Dim lastmovementwasUp As Boolean
    Dim firstmovement As Boolean
    Dim approximation As Boolean
    Dim endmovement As Boolean
    Dim doThirdRound As Boolean
    Dim callerisTest As Boolean
    Dim autofocusing As Boolean
    Dim autofocusSub As Integer
    Dim LightNum As Integer
    Dim rect As Rectangle
    Dim newImageData As System.Drawing.Imaging.BitmapData
    Dim ptr As IntPtr
    Dim bytes As Integer
    Dim rgbValues(2) As Byte  'we will then resize this array
    Dim va As Integer
    Dim vb As Integer
    Dim vb2 As Integer
    Dim vb3 As Integer
    Dim fsleep As Integer
    Dim h As Integer
    Dim w As Integer
    Dim offset As Integer
    Dim sum As Integer
    Dim sumb As Integer
    Dim a As Integer
    Dim b As Integer
    Dim c As Integer
    Dim valueA As Integer
    Dim valueB As Integer
    'Dim i As Integer
    Dim yvalue As Integer
    Dim xvalue As Integer
    Dim ulValue As Integer
    Dim newimage As Bitmap
    Dim numberOfErrors As Integer = 1
    Dim lightisOn As Boolean = False
    Dim mypos As Integer
    Dim mydate As String
    Dim dailyfolder As String
    Dim typeoflight As String
    Dim camtype As String
    Dim myFont = New Font(FontFamily.GenericSerif, 10, FontStyle.Bold)
    Dim autofocusCounter As Integer
    Dim length As String
    Dim lengthlow As String
    Dim lengthhi As String
    Dim bm As New Bitmap(165, 162)
    Dim bm2 As New Bitmap(615, 615)
    Dim gr As Graphics
    Dim gr2 As Graphics
    Dim nitems As Integer
    Dim loaded As Boolean = False
    Dim testmode As Boolean
    Dim mcl As Boolean
    Dim pololumode As Boolean
    Dim pololusteps As Integer 'steps in x direction
    Dim pololustepsY As Integer  'steps in y direction
    Dim pololurepetitions As Integer
    Dim pololuPosY As Integer = 0
    Dim pololuPosX As Integer = 0
    Dim repeats As Integer
    Dim FILE_NAME As String

    'Public imagefolder As String 'its declared in mymodule.vb
    ' Public Inculeft As Boolean   'its declared in mymodule.vb

    Dim mycamconfig As New FlyCapture2Managed.FC2Config
    Dim shutterBF2 As New FlyCapture2Managed.CameraProperty

    Dim shutterDarkF As New FlyCapture2Managed.CameraProperty

    Dim mycamconfigb As New FlyCapture2Managed.FC2Config
    Dim shutterBF2b As New FlyCapture2Managed.CameraProperty

    Dim shutterDarkFb As New FlyCapture2Managed.CameraProperty
    Dim numbackuperrors As String = 0
    Dim cam1selected As Boolean
    Dim cam2selected As Boolean
    Dim totalSteps As Integer


    Private Sub Form1_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        stopThread = True
        lightsOFF()

    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        addtomyConsole("Form1_Loading()")


        For Each po As String In System.IO.Ports.SerialPort.GetPortNames()
            ComboBox2.Items.Add(po)
            ComboBox3.Items.Add(po)
        Next


        If My.Settings.ArduinoMode Then
            CheckBox28.Checked = True
        Else
            CheckBox28.Checked = False
        End If

        If My.Settings.AutoStartCheckBox Then
            AutoStartCheckBox.Checked = True
        Else
            AutoStartCheckBox.Checked = False
        End If

        If My.Settings.UseRecorded Then
            UseRecorded.Checked = True
        Else
            UseRecorded.Checked = False
        End If



        Label68.Text = My.Settings.thisPC

        directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory)
        directoryInfo = Directory.GetParent(directoryInfo.FullName)
        FILE_NAME = directoryInfo.FullName & "\IncomingSerial.txt"
        objWriter3 = New System.IO.StreamWriter(FILE_NAME, False)
        objWriter3.Write("")
        objWriter3.Close()

        gainCam1.type = PropertyType.Gain
        gainCam1.absControl = True
        gainCam1.absValue = My.Settings.gainCam1
        gainCam2.type = PropertyType.Gain
        gainCam2.absControl = True
        gainCam2.absValue = My.Settings.gainCam2

        shutterBF.type = PropertyType.Shutter
        shutterBF.absControl = True
        shutterBF.absValue = My.Settings.shutterBF
        shutterBF2.type = PropertyType.Shutter
        shutterBF2.absControl = True
        shutterBF2.absValue = My.Settings.shutterBF2
        shutterfluo.type = PropertyType.Shutter
        shutterfluo.absControl = True
        shutterfluo.absValue = My.Settings.shutterfluo
        shutterDarkF.type = PropertyType.Shutter
        shutterDarkF.absControl = True
        shutterDarkF.absValue = My.Settings.shutterDarkF

        shutterBFb.type = PropertyType.Shutter
        shutterBFb.absControl = True
        shutterBFb.absValue = My.Settings.shutterBFb
        shutterBF2b.type = PropertyType.Shutter
        shutterBF2b.absControl = True
        shutterBF2b.absValue = My.Settings.shutterBF2b
        shutterfluob.type = PropertyType.Shutter
        shutterfluob.absControl = True
        shutterfluob.absValue = My.Settings.shutterfluob
        shutterDarkFb.type = PropertyType.Shutter
        shutterDarkFb.absControl = True
        shutterDarkFb.absValue = My.Settings.shutterDarkFb




        loadconfigcount = My.Settings.confignumber 'sets stage configuration save number 
        loadConfigfromRelease(loadconfigcount)

        Try
            TextBox1.Text = My.Settings.interval 'sets interval between imaging rounds.
        Catch ex As Exception
            MsgBox("Couldn't load interval from my settings")
            TextBox1.Text = 3
        End Try

        Label32.Text = TextBox3.Text  'sets interval for fluo images.

        drive = My.Settings.drive   '"C:"
        If drive = "C:" Then
            RadioButton26.Checked = True
        ElseIf drive = "S:" Then
            RadioButton27.Checked = True
        End If

        imagefolder = drive & "\Images"
        imageSubfolder = "CurrentImagingSession"
        sessionfolder = imagefolder & "\" & imageSubfolder
        vanadiumName = "En-bm-awrz2-i"
        vanadiumfolder = "\\" & vanadiumName & "\D\Images\CurrentImagingSession"


        TextBox31.Text = My.Settings.MotorSteps.ToString
        TextBox32.Text = My.Settings.MotorStepsB.ToString

        If My.Settings.SwitchxCheckbox Then
            SwitchxCheckbox.Checked = True
        Else
            SwitchxCheckbox.Checked = False
        End If

        If My.Settings.SwitchyCheckbox Then
            SwitchyCheckbox.Checked = True
        Else
            SwitchyCheckbox.Checked = False
        End If

        If My.Settings.SwitchY Then
            SwitchY.Checked = True
        Else
            SwitchY.Checked = False
        End If


        If My.Settings.SwitchX Then
            SwitchX.Checked = True
        Else
            SwitchX.Checked = False
        End If


        If My.Settings.FlipYCam1 Then
            CheckBox16.Checked = True
        Else
            CheckBox16.Checked = False
        End If
        If My.Settings.FlipXCam1 Then
            CheckBox27.Checked = True
        Else
            CheckBox27.Checked = False
        End If


        If My.Settings.Cam1RED = True Then
            CheckBox32.Checked = True
        Else
            CheckBox32.Checked = False
        End If

        If My.Settings.Fl_A4988 = True Then 'Fl_A4988
            CheckBox34.Checked = True
        Else
            CheckBox34.Checked = False
        End If

        If My.Settings.ArduinoIncuMode Then
            CheckBox31.Checked = True
        Else
            CheckBox31.Checked = False
        End If


        If My.Settings.sendEmails Then
            CheckBox29.Checked = True
        Else
            CheckBox29.Checked = False
        End If

        If My.Settings.PololuBacklash Then
            CheckBox18.Checked = True
        Else
            CheckBox18.Checked = False
        End If

        If My.Settings.InvertMotor Then
            CheckBox6.Checked = True
        Else
            CheckBox6.Checked = False
        End If
        If My.Settings.testmode Then
            CheckBox23.Checked = True
            'MsgBox("checked")
        Else
            ' MsgBox("unchecked")
            CheckBox23.Checked = False
        End If

        'CheckBox6.Checked = True 'autofocusing checkbox

        Try
            ComboBox2.SelectedItem = My.Settings.myserialport
        Catch ex As Exception
        End Try
        Try
            ComboBox3.SelectedItem = My.Settings.arduinoComPort
        Catch ex As Exception
        End Try






        'If My.Settings.pololumode = True Then
        '    'MsgBox("1")
        '    CheckBox10.Checked = True
        '    RadioButton22.Checked = True
        'Else
        '    'MsgBox("2")
        '    CheckBox10.Checked = False
        '    RadioButton22.Checked = False
        '    pololumode = False
        'End If


        pololurepetitions = My.Settings.pololurepetitions
        pololusteps = My.Settings.pololusteps
        pololustepsY = My.Settings.pololustepsY
        TextBox8.Text = pololusteps
        TextBox10.Text = pololustepsY
        TextBox9.Text = pololurepetitions

        lengthlow = "15"
        lengthhi = "200"
        stopThread = True
        ' Cross Thread errors occur when debugging through Visual Studio.
        ' This is the recommended fix from Visual Studio.
        Control.CheckForIllegalCrossThreadCalls = False

        autofocusCounter = TextBox4.Text

        Me.Label5.Parent = PictureBox1
        Me.Label7.Parent = PictureBox1
        Me.Label9.Parent = PictureBox1
        Me.Label6.Parent = PictureBox1
        Me.Label11.Parent = PictureBox1
        finefocusinterval = 2
        coarsefocusinterval = 20
        RadioButton1.Checked = True


        'If versionMS2000 Then
        '    'mywidth = widthMS2000
        '    ' myheigth = heigthMS2000
        '    Label10.Text = "Using Stage MS-2000..."
        'Else
        '    ' mywidth = widthMS4
        '    ' myheigth = heigthMS4
        '    Label10.Text = "Using Stage MS-4..."
        'End If


        ConnectSerial() 'arduino serialport



        If pololumode = False And testmode = False And DCmotors.Checked = False Then
            Try
                SerialPort1.Open()  'COM3'
            Catch ex As Exception
                MessageBox.Show("Error: Please connect the Stage Controller to the PC or change the Com port. ")
            End Try
        End If

        setupCheckboxes()

        'SETTINGS FOR AUTOFOCUS:
        For i As Integer = 1 To 11
            positionFchange(i) = My.Settings.positionsF(i - 1)
        Next
        For i As Integer = 1 To 11
            Ftimes(i) = My.Settings.Ftimes(i - 1)
        Next
        For i As Integer = 1 To 11
            FchangeUp(i) = My.Settings.FchangeSArray(i - 1)
        Next

        'SET TO NOT SKIP ANY:
        For i As Integer = 0 To 999
            skip(i) = False
        Next
        For i As Integer = 0 To 999
            skipOnlyLight(i) = False
        Next

        'LOAD EXTRA EXPOSURES
        For i As Integer = 0 To 19
            exposureArray(i) = Convert.ToDouble(My.Settings.ExposureSettings(i))
        Next


        '''''PrintBuildInfo....
        version = ManagedUtilities.libraryVersion
        newStr = New StringBuilder()
        newStr.AppendFormat("FlyCapture2 library version: {0}.{1}.{2}.{3}" & vbNewLine, _
                            version.major, version.minor, version.type, version.build)
        Console.WriteLine(newStr)
        '''''

        ''''''Connect to Cameras:
        busMgr = New ManagedBusManager()
        Try
            numCameras = busMgr.GetNumOfCameras()
        Catch ex As Exception

        End Try

        Console.WriteLine("Number of cameras detected: {0}", numCameras)
        numCamerasDetected = numCameras

        Try
            serial1 = busMgr.GetCameraSerialNumberFromIndex(0)
            serial2 = busMgr.GetCameraSerialNumberFromIndex(1)

        Catch ex As Exception
        End Try

        'FormCamSelection.ShowDialog()
        'or just select the one on the right/left:
        'selectedCamSerial = serial1
        If testmode Then
        Else


            Try
                'guid = busMgr.GetCameraFromIndex(0)

                cam = New ManagedCamera()
                myControlDialog = New FlyCapture2Managed.Gui.CameraControlDialog
                myConnectToCam(serial1)
                myControlDialog.Connect(cam)

                'If numCamerasDetected = 2 Then  'now connect to 2nd camera:
                '    guid = busMgr.GetCameraFromSerialNumber(serial2)

                '    cam2 = New ManagedCamera()
                '    cam2.Connect(guid)
                '    camInfo2 = cam2.GetCameraInfo()
                '    PrintCameraInfo(camInfo2)

                '    ' Get embedded image info from camera
                '    embeddedInfo2 = cam2.GetEmbeddedImageInfo()

                '    '' Enable timestamp collection	
                '    'If (embeddedInfo.timestamp.available = True) Then
                '    '    embeddedInfo.timestamp.onOff = True
                '    'End If

                '    ' Set embedded image info to camera
                '    cam2.SetEmbeddedImageInfo(embeddedInfo2)
                '    '  myControlDialog2 = New FlyCapture2Managed.Gui.CameraControlDialog
                '    ' myControlDialog2.Connect(cam2)
                'End If

                Me.Refresh()
                cam.SetProperty(shutterBF)
                addtomyConsole("Grabbing Image..")
                cam1selected = True
                Cam1Label.Font = New Font(Cam1Label.Font, FontStyle.Bold)
                cam2selected = False
                GrabImage()
            Catch ex As Exception
                If testmode = False Then
                    MessageBox.Show("You haven't connected the camera to the PC!")
                End If

            End Try

        End If





        loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")

        Timer4.Start()  'this timer is just to draw the positions.
        loaded = True


        WaitEvent = New AutoResetEvent(False)


        'getStageParameters()



        ComboBox1.SelectedIndex = My.Settings.FocusingLightType


        TextBox34.Text = My.Settings.testPos1
        TextBox35.Text = My.Settings.testPos2

        TextBox37.Text = My.Settings.FlCam1Pin
        TextBox38.Text = My.Settings.FlCam2Pin
        TextBox41.Text = My.Settings.DfCam1Pin


        If My.Settings.Panel2Shown = True Then
            Panel2.Show()
        Else
            Panel2.Hide()
        End If

        If My.Settings.Panel3Shown = True Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If

        TextBox32.Text = My.Settings.MotorStepsB.ToString
        ''FocusEvery.Text = My.Settings.FocusEvery

        For i = 1 To 199
            recordedfocus(i) = 0

        Next
        FormFocusingRecorded.loadvalues()



        addtomyConsole("Done Form1_Load()")

        If AutoStartCheckBox.Checked Then
            GotoPos1AndWaitForStage()

            'timedMessage = "Autostart Imaging checkbox is selected. AutoStarting.."
            'time = 4
            'FormTimedDialog.Show()
            ''StartSleepingThread()
            ''WaitEvent.WaitOne()

            'StartSleepAndPressEnterThread()
            TimerPressEnter.Start()
            Dim result1 As DialogResult = MessageBox.Show("Autostart Imaging checkbox is selected. AutoStarting..", _
                              "Starting", _
                              MessageBoxButtons.YesNo)
            If result1 = DialogResult.No Then
                My.Settings.AutoStartCheckBox = False
            End If
            If My.Settings.AutoStartCheckBox = True Then
                buttonStartSession()
            End If

            
        End If

    End Sub

    'Sub StartSleepAndPressEnterThread()
    '    thread1 = New Thread(AddressOf SleepAndPressEnterThread)
    '    thread1.Start()
    'End Sub

    'Sub SleepAndPressEnterThread(ByVal delay As Object)

    '    Threading.Thread.Sleep(4000)
    '    'SendKeys.Send("{ENTER}")
    '    ''WaitEvent.Set()
    'End Sub


    Sub setupCheckboxes()

        If My.Settings.DCmotors = True Then
            DCmotors.Checked = True
        End If



        If My.Settings.checkb15 = True Then
            CheckBox15.Checked = True
        Else
            CheckBox15.Checked = False
        End If

        If My.Settings.checkb17 = True Then
            CheckBox17.Checked = True
        Else
            CheckBox17.Checked = False
        End If

        If My.Settings.checkb14 = True Then
            CheckBox14.Checked = True
        Else
            CheckBox14.Checked = False
        End If


        If My.Settings.checkb5 = True Then
            CheckBox5.Checked = True
        Else
            CheckBox5.Checked = False
        End If

        If My.Settings.checkb1 = True Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If

        If My.Settings.checkb4 = True Then
            CheckBox4.Checked = True
        Else
            CheckBox4.Checked = False
        End If

        If My.Settings.checkb19 = True Then
            CheckBox19.Checked = True
        Else
            CheckBox19.Checked = False
        End If


        If My.Settings.checkb8 = True Then
            CheckBox8.Checked = True
        Else
            CheckBox8.Checked = False
        End If

        If My.Settings.checkb20 = True Then
            CheckBox20.Checked = True
        Else
            CheckBox20.Checked = False
        End If

        If My.Settings.checkb21 = True Then
            CheckBox21.Checked = True
        Else
            CheckBox21.Checked = False
        End If

        If My.Settings.radiob20 = True Then
            RadioButton20.Checked = True
        End If

        'MsgBox(My.Settings.pololumode)
        'MsgBox(My.Settings.radiob22)
        'If My.Settings.radiob22 = True Then
        '    RadioButton22.Checked = True
        'End If
        If My.Settings.radiob25 = True Then
            RadioButton25.Checked = True
        End If
        If My.Settings.radiob23 = True Then
            RadioButton23.Checked = True
            versionMS2000 = True
        End If



        'If My.Settings.radiob24 = True Then
        '    RadioButton24.Checked = True
        'End If
        If My.Settings.mcl = True Then
            RadioButton24.Checked = True  'this is the mcl button
        End If

        If My.Settings.StageHiSpeed = True Then
            CheckBox3.Checked = True 'This sets stage speed to high for Ms-4 new and Ms-4 old stages
        Else
            TextBox19.Text = My.Settings.minspeedLOW
            TextBox19.Refresh()
            TextBox20.Text = My.Settings.maxspeedLOW
            TextBox20.Refresh()
            TextBox21.Text = My.Settings.rampslopeLOW
            TextBox21.Refresh()
            TextBox22.Text = My.Settings.UNITS
            setallStageValues()
        End If

        TextBox27.Text = shutterBF.absValue
        GainTextBox.Text = gainCam1.absValue

        If My.Settings.FlipYCam1 = True Then
            CheckBox16.Checked = True
        Else
            CheckBox16.Checked = False
        End If
        If My.Settings.FlipXCam1 = True Then
            CheckBox27.Checked = True
        Else
            CheckBox27.Checked = False
        End If


    End Sub

  

    Dim currentstageconfig As Integer

    Sub myConnectToCam(ByVal myserialbyval)
        Try

            guid = busMgr.GetCameraFromSerialNumber(myserialbyval)

            cam.Connect(guid)
            camInfo = cam.GetCameraInfo()
            PrintCameraInfo(camInfo)

            ' Get embedded image info from camera
            embeddedInfo = cam.GetEmbeddedImageInfo()

            '' Enable timestamp collection	
            'If (embeddedInfo.timestamp.available = True) Then
            '    embeddedInfo.timestamp.onOff = True
            'End If

            ' Set embedded image info to camera
            cam.SetEmbeddedImageInfo(embeddedInfo)
            'myControlDialog.Disconnect()
            ' myControlDialog.Dispose()
            'Threading.Thread.Sleep(1000)

        Catch ex As Exception
            If testmode Then
                Label60.Text = "error connecting to camera on myConnectToCam " & DateAndTime.Now
                Label60.Show()
            Else
                MsgBox("error connecting to camera")
            End If
        End Try



    End Sub


    Shared Sub PrintCameraInfo(ByVal camInfo As CameraInfo)
        Dim newStr As StringBuilder = New StringBuilder()
        newStr.Append(vbNewLine & "*** CAMERA INFORMATION ***" & vbNewLine)
        newStr.AppendFormat("Serial number - {0}" & vbNewLine, camInfo.serialNumber)
        newStr.AppendFormat("Camera model - {0}" & vbNewLine, camInfo.modelName)
        newStr.AppendFormat("Camera vendor - {0}" & vbNewLine, camInfo.vendorName)
        newStr.AppendFormat("Sensor - {0}" & vbNewLine, camInfo.sensorInfo)
        newStr.AppendFormat("Resolution - {0}" & vbNewLine, camInfo.sensorResolution)
        Console.WriteLine(newStr)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click 'Start
        liveONbutton()
    End Sub

    Sub liveONbutton()
        If stopThread = True And grabbingimage = False Then
            lightsOFF()
            stopThread = False
            grabThread = New System.Threading.Thread(AddressOf GrabLoop)
            grabThread.Start() 'GrabLoop()
        Else
            MessageBox.Show("Wait a second.. I'm still grabbing the last image")
        End If
    End Sub

    Private Sub GrabLoop()
        ' Power on the camera
        Const k_cameraPower As UInteger = &H610
        Const k_powerVal As UInt32 = &H80000000UI
        cam.WriteRegister(k_cameraPower, k_powerVal)

        ' Wait for camera to complete power-up
        Const k_millisecondsToSleep = 100
        Dim regVal As UInteger = 0

        Do While ((regVal And k_powerVal) = 0)
            System.Threading.Thread.Sleep(k_millisecondsToSleep)
            Try
                regVal = cam.ReadRegister(k_cameraPower)
            Catch ex As Exception
            End Try

        Loop
        '
        lightON()
        '  If RadioButton13.Checked Then 'this is camera1
        cam.StartCapture()
        '  Else
        '  cam2.StartCapture()
        '  End If


        Do While True 'stopThread = False
            GrabImageForLoop()
            'Refresh()
            If stopThread = True Then
                ' If RadioButton13.Checked Then 'this is camera1
                cam.StopCapture()

                ' Power off the camera
                Const k_powerValoff2 As UInt32 = &H0UI
                cam.WriteRegister(k_cameraPower, k_powerValoff2)

                '
                'Else
                '    cam2.StopCapture()
                '  End If

                lightsOFF()
                grabThread.Abort()
            End If
        Loop
    End Sub

    Dim arduinoErrors As Integer = 0
    Sub ArduinoPin(ByVal PinNumber)
        's.Close()   
        Try
            's.Open()
            'SerialPort.Write(Chr(PinNumber))
            'MsgBox("now writing " & PinNumber & " to " & SerialPort.PortName.ToString)
            SerialPort.Close()
            SerialPort.Open()
            Thread.Sleep(100)
            SerialPort.Write(PinNumber)
            If PinNumber.ToString().Substring(0, 2) = "mb" Then
                totalSteps -= Convert.ToInt32(PinNumber.ToString().Substring(2))
                Label88.Text = totalSteps
            ElseIf PinNumber.ToString().Substring(0, 2) = "mf" Then
                totalSteps += Convert.ToInt32(PinNumber.ToString().Substring(2))
                Label88.Text = totalSteps
            End If
            'MsgBox("writing")
            'SerialPort.Write("hello")

            's.Close()
        Catch ex As Exception
            If testmode Then
            Else
                arduinoErrors = arduinoErrors + 1
                'addtomyConsoleErrorMessages("Arduino not connected, (or set up the right COM port)- ArduinoPin(" & PinNumber & ")")
                addtomyConsoleErrorMessages("Arduino not connected, (or set up the right COM port)- ArduinoPin(" & PinNumber & ")")
                addtomyConsoleErrorMessages("at " & DateTime.Now)
                addtomyConsoleErrorMessages("num " & arduinoErrors)
                addtomyConsoleMain("Arduino not connected, (or set up the right COM port)- ArduinoPin(" & PinNumber & ")")
                addtomyConsoleMain("at " & DateTime.Now)
                addtomyConsoleMain("num " & arduinoErrors)
            End If
        End Try

    End Sub


    Sub lightON()
        lightisOn = True
        Try
            Button1.BackColor = Color.Yellow
            Label5.Show()
            Label5.Refresh()
            Button1.Refresh()
            If RadioButton6.Checked Then 'checkbox BF
                If DCmotors.Checked Then
                    If CheckBox32.Checked Then
                        'MsgBox("red")
                        ArduinoPin("lr") 'turn on red light
                    Else
                        ' MsgBox("green")
                        ArduinoPin("lg") 'turn on green light
                    End If


                ElseIf CheckBox28.Checked Or CheckBox31.Checked Then 'ArduinoMode Checked.

                    If cam1selected Then            'If RadioButton13.Checked Then 'Cam1 Checked.
                        If CheckBox32.Checked Then
                            ' MsgBox("red")
                            ArduinoPin("lr") 'turn on red light
                        Else
                            ' MsgBox("green")
                            ArduinoPin("lg") 'turn on green light
                        End If

                        'If CheckBox32.Checked Then ' "Cam1 is Red" checkbox
                        '    ArduinoPin(4) 'Turn on pin 12 on Arduino.
                        'Else
                        '    ArduinoPin(2) 'Turn on pin 7 on Arduino.
                        'End If

                    ElseIf cam2selected Then    'ElseIf RadioButton14.Checked Then 'Cam2 Checked.
                        If CheckBox32.Checked Then
                            'MsgBox("red cam2selected")
                            ArduinoPin("lg") 'turn on red light
                        Else
                            ' MsgBox("green cam2selected")
                            ArduinoPin("lr") 'turn on green light
                        End If
                        'If CheckBox32.Checked Then ' "Cam1 is Red" checkbox
                        '    ArduinoPin(2) 'Turn on pin 7 on Arduino.
                        'Else
                        '    ArduinoPin(4) 'Turn on pin 12 on Arduino.
                        'End If
                    End If
                Else
                    light1ON()
                End If


            ElseIf RadioButton9.Checked Then 'checkbox DF
                'cam.RestoreFromMemoryChannel(2)
                If My.Settings.Fl_A4988 Then   'Fl_A4988
                    ArduinoPin(TextBox41.Text)   'Fl_A4988 light on
                Else
                    light2ON()
                End If

            ElseIf RadioButton8.Checked Then 'checkbox Fluo
                ' cam.RestoreFromMemoryChannel(2)
                If cam1selected Then 'selected radiobutton cam1
                    If My.Settings.Fl_A4988 Then   'Fl_A4988
                        ArduinoPin(TextBox37.Text)   'Fl_A4988 light on
                    Else
                        light2ON()
                    End If

                ElseIf cam2selected Then 'selected radiobutton cam2
                    If My.Settings.Fl_A4988 Then   'Fl_A4988
                        ArduinoPin(TextBox38.Text)   'Fl_A4988 light on
                    Else
                        lightFlON()
                    End If
                End If

                ElseIf RadioButton9.Checked Then
                    'cam.RestoreFromMemoryChannel(2)
                    lightDfON()
                End If
        Catch ex As Exception
            MsgBox("Camera is not connected to PC")
        End Try
    End Sub

    Sub lightTestON()
        If RadioButton6.Checked Then
            'cam.RestoreFromMemoryChannel(1)
            light1ON()
        ElseIf RadioButton7.Checked Then
            'cam.RestoreFromMemoryChannel(2)
            light2ON()
        ElseIf RadioButton8.Checked Then
            'cam.RestoreFromMemoryChannel(2)
            lightFlON()
        ElseIf RadioButton9.Checked Then
            'cam.RestoreFromMemoryChannel(2)
            lightDfON()
        End If
    End Sub

    Sub light1ON()
        lightisOn = True
        sendtoINCUSCOPE("1")
        Button1.BackColor = Color.Yellow
        Label5.Show()
        Label5.Refresh()
        Button1.Refresh()
    End Sub
    Sub light2ON()
        lightisOn = True
        sendtoINCUSCOPE(TextBox37.Text)
        Button1.BackColor = Color.Yellow
        Label5.Show()
        Label5.Refresh()
        Button1.Refresh()
    End Sub
    Sub lightFlON()
        lightisOn = True
        sendtoINCUSCOPE(TextBox38.Text) 'sendtoINCUSCOPE("w")
        Button1.BackColor = Color.Yellow
        Label5.Show()
        Label5.Refresh()
        Button1.Refresh()
    End Sub
    Sub lightDfON()
        lightisOn = True
        sendtoINCUSCOPE(TextBox41.Text)  'sendtoINCUSCOPE("d")
        Button1.BackColor = Color.Yellow
        Label5.Show()
        Label5.Refresh()
        Button1.Refresh()
    End Sub

    Sub lightsOFF()
        'stopThread = True

        lightisOn = False

        Button1.UseVisualStyleBackColor = True
        Button1.Refresh()
        Label5.Hide()
        Label5.Refresh()

        'turn off arduino lights
        If DCmotors.Checked Then
            ArduinoPin("f")
            If CheckBox31.Checked Then 'ArduinoIncuMode Checked.
                sendtoINCUSCOPE("a")
            End If

        ElseIf CheckBox28.Checked Or CheckBox31.Checked Then 'ArduinoMode or  'ArduinoIncuMode Checked.
            ArduinoPin("f")
            If CheckBox31.Checked Then 'ArduinoIncuMode Checked.
                sendtoINCUSCOPE("a")
            End If
        ElseIf CheckBox31.Checked Then 'ArduinoIncuMode Checked.
            sendtoINCUSCOPE("a")
        Else
        sendtoINCUSCOPE("a")
        End If

    End Sub

    Sub setCameraShutters()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click 'Grab one
        GrabImage()
    End Sub
    Sub GrabImage()
    
        GrabImageSub()

    End Sub

    Sub GrabImageSub()


        'rawImage = New ManagedImage()
        'convertedImage = New ManagedImage()
        s1 = DateTime.Now.Second.ToString()
        m1 = DateTime.Now.Millisecond.ToString()

        lightON()





        'Threading.Thread.Sleep(100) '20 is not enough
        'Dim my2 As New FC2Config
        'mycamconfig.grabMode = GrabMode.BufferFrames
        'mycamconfig.numBuffers = 1
        'cam.SetConfiguration(mycamconfig)

        'Dim b As New FlyCapture2Managed.TriggerMode
        'b.onOff = True
        'cam.SetTriggerMode(b)
        'cam.FireSoftwareTrigger(False)

        ' Power on the camera
        Const k_cameraPower As UInteger = &H610
        Const k_powerVal As UInt32 = &H80000000UI
        Try
            cam.WriteRegister(k_cameraPower, k_powerVal)

        Catch ex As Exception
            'MsgBox("cam not connected, reconnect it")
            'Exit Sub
            'serial1 = busMgr.GetCameraSerialNumberFromIndex(0)
            ' cam = New ManagedCamera()
            ' myControlDialog = New FlyCapture2Managed.Gui.CameraControlDialog
            myConnectToCam(serial1)
            myControlDialog.Connect(cam)
            cam.WriteRegister(k_cameraPower, k_powerVal)
            addtomyConsoleErrorMessages("Attempted reconnecting to camera")

        End Try

        ' Wait for camera to complete power-up
        Const k_millisecondsToSleep = 100
        Dim regVal As UInteger = 0

        Do While ((regVal And k_powerVal) = 0)
            System.Threading.Thread.Sleep(k_millisecondsToSleep)
            Try
                regVal = cam.ReadRegister(k_cameraPower)
            Catch ex As Exception

            End Try
        Loop

        Try
            ' If RadioButton13.Checked Then 'this is camera1
            cam.StartCapture()
            ' Else
            ' cam2.StartCapture()
            'End If

        Catch ex As Exception
            stopThread = True
            lightsOFF()
            sendemailCameraDisconnected()
            addtomyConsoleErrorMessages("cam not connected while starting capture " & DateAndTime.Now)
            Label60.Text = "cam not connected while starting capture " & DateAndTime.Now
            Label60.Show()
            'MessageBox.Show("turning live off")
            Exit Sub
        End Try


        Try
            '   If RadioButton13.Checked Then 'this is camera1
            cam.RetrieveBuffer(rawImage)
            cam.RetrieveBuffer(rawImage)
            '  Else
            '  cam2.RetrieveBuffer(rawImage)
            '  cam2.RetrieveBuffer(rawImage)
            ' End If


        Catch ex As Exception
            addtomyConsoleErrorMessages(DateAndTime.Now & "Error retrieveing buffer: Image Consistency Error! ")
            'sendemailCameraDisconnected()

            Try
                FILE_NAME = drive & "\Programs\IncuScope\MYERRORS.txt"  'creates a text file with the errrors.
                Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter.WriteLine("Image Consistency Error! " & DateAndTime.Now)
                objWriter.Close()
            Catch ex3 As Exception
                addtomyConsoleErrorMessages("Could not find \Programs\IncuScope\MYERRORS.txt " & ex3.Message)
            End Try
            numberOfErrors = numberOfErrors + 1

            '  If RadioButton13.Checked Then 'this is camera1
            cam.StopCapture()
            ' Else
            ' cam2.StopCapture()
            ' End If

            lightsOFF()
            Threading.Thread.Sleep(5000)
            lightON()
            Try
                '   If RadioButton13.Checked Then 'this is camera1
                cam.StartCapture()
                '   Else
                '  cam2.StartCapture()
                '  End If
                ' Threading.Thread.Sleep(500)

                '  If RadioButton13.Checked Then 'this is camera1
                cam.RetrieveBuffer(rawImage)
                cam.RetrieveBuffer(rawImage)
                ' Else
                'cam2.RetrieveBuffer(rawImage)
                ' cam2.RetrieveBuffer(rawImage)
                ' End If
            Catch ex2 As Exception

                addtomyConsoleErrorMessages("cam not connected or error retrieveing buffer " & DateAndTime.Now)
                Label60.Text = "cam not connected or error retrieveing buffer " & DateAndTime.Now
                Label60.Show()
            End Try

        End Try
        Try
            ' If RadioButton13.Checked Then 'this is camera1
            cam.StopCapture()
            '  Else
            '  cam2.StopCapture()
            ' End If
        Catch ex As Exception
            addtomyConsoleErrorMessages("Error camera stopping capture, probably disconnected. " & ex.Message)
            sendemailCameraDisconnected()
            addtomyConsoleErrorMessages("cam not connected  while stopping capture " & DateAndTime.Now)
            Label60.Text = "cam not connected while stopping capture " & DateAndTime.Now
            Label60.Show()
            ' Dim FILE_NAME As String = "C:\Programs\IncuScope\MYERRORS.txt"  'creates a text file with the errrors.
            ' Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            ' objWriter.WriteLine("Image Consistency Error2! " & DateAndTime.Now)
            ' objWriter.Close()
            'numberOfErrors = numberOfErrors + 1
        End Try


        lightsOFF()
        s2 = DateTime.Now.Second.ToString()
        m2 = DateTime.Now.Millisecond.ToString()


        ' Power off the camera

        Const k_powerValoff2 As UInt32 = &H0UI
        cam.WriteRegister(k_cameraPower, k_powerValoff2)
        '



        rawImage.Convert(PixelFormat.PixelFormatBgr, convertedImage)

        bitmap = convertedImage.bitmap   ' Gets the Bitmap object. Bitmaps are only valid if the pixel format of the ManagedImage is RGB or RGBU.


        myRotateBitmap()

        PictureBox1.Image = bitmap
        ' PictureBox1.Image = Image.FromFile("C:\1.bmp")
        ' bitmap = PictureBox1.Image

        'Console.WriteLine("Light On for:")
        difs = s2 - s1
        difm = m2 - m1
        If difs < 0 Then
            difs = difs + 60
        End If
        If difm < 0 Then
            difm = difm + 1000
            difs = difs - 1
        End If
        difs = difs.ToString("00")
        difm = difm.ToString("000")
        shutterTime = difs & "." & difm
        Console.WriteLine("light on for.. " & shutterTime)
        Label8.Text = shutterTime
        Label29.Text = shutterTime
        Me.Label29.Refresh()


        'turn extra light on for light exposure experiment (fluo) 
        If exposureArray(currentListItem) <> 0 Then
            lightFlON()
            'EXTRA EXPOSURE CODE:
            'MessageBox.Show("now starting wait at exposureArray(" & currentListItem & ") which is " & exposureArray(currentListItem) & "secs")
            Threading.Thread.Sleep(1000 * exposureArray(currentListItem))
            '
            lightsOFF()
        End If

    End Sub

    Sub GrabimageNO_GUI()




        Try
            cam.StartCapture()
        Catch ex As Exception
            stopThread = True
            ''''''''''''''''  lightsOFF()
            'MessageBox.Show("turning live off")
            Exit Sub
        End Try


        Try
            cam.RetrieveBuffer(rawImage)
            cam.RetrieveBuffer(rawImage)
        Catch ex As Exception
            'Dim FILE_NAME As String = "C:\Programs\IncuScope\MYERRORS.txt"  'creates a text file with the errrors.
            'Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            'objWriter.WriteLine("Image Consistency Error! " & DateAndTime.Now)
            'objWriter.Close()
            'numberOfErrors = numberOfErrors + 1
        End Try
        Try
            cam.StopCapture()
        Catch ex As Exception
            'Dim FILE_NAME As String = "C:\Programs\IncuScope\MYERRORS.txt"  'creates a text file with the errrors.
            'Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            'objWriter.WriteLine("Image Consistency Error2! " & DateAndTime.Now)
            'objWriter.Close()
            'numberOfErrors = numberOfErrors + 1
        End Try

        '''''''''' lightsOFF()

        rawImage.Convert(PixelFormat.PixelFormatBgr, convertedImage)
        bitmap = convertedImage.bitmap   ' Gets the Bitmap object. Bitmaps are only valid if the pixel format of the ManagedImage is RGB or RGBU.
        myRotateBitmap()

        ' PictureBox1.Image = bitmap

        'turn extra light on for light exposure experiment (fluo) 
        'If exposureArray(currentListItem) <> 0 Then
        '    lightFlON()
        '    'EXTRA EXPOSURE CODE:
        '    'MessageBox.Show("now starting wait at exposureArray(" & currentListItem & ") which is " & exposureArray(currentListItem) & "secs")
        '    Threading.Thread.Sleep(1000 * exposureArray(currentListItem))
        '    '
        '    lightsOFF()
        'End If
    End Sub


    Dim grabbingimage As Boolean
    Sub GrabImageForLoop()
        grabbingimage = True
        Try

            '  If RadioButton13.Checked Then 'this is camera1
            cam.RetrieveBuffer(rawImage)
            '  Else
            ' cam2.RetrieveBuffer(rawImage)
            ' End If
        Catch ex As Exception
            'Dim FILE_NAME As String = "C:\Programs\IncuScope\MYERRORS-GrabImageForLoop.txt"  'creates a text file with the errrors.
            'Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            'objWriter.WriteLine("Image Consistency Error! " & DateAndTime.Now)
            'objWriter.Close()
            numberOfErrors = numberOfErrors + 1
        End Try
        rawImage.Convert(PixelFormat.PixelFormatBgr, convertedImage)
        bitmap = convertedImage.bitmap   ' Gets the Bitmap object. Bitmaps are only valid if the pixel format of the ManagedImage is RGB or RGBU.

        myRotateBitmap()
        Try
            PictureBox1.Image = bitmap
        Catch ex As Exception
            Threading.Thread.Sleep(2000)
            PictureBox1.Image = bitmap
        End Try

        a = a + 1
        If a > 99 Then
            a = 0
        End If
        Label1.Text = a
        grabbingimage = False

    End Sub


    Sub myRotateBitmap()
        If mcl Then
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX)
            'ElseIf versionMS2000 Then
            '    bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY) 'bitmap.RotateFlip(RotateFlipType.Rotate180FlipX)

        ElseIf pololumode Then
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX) 'RotateFlipType.RotateNoneFlipX
        Else
            'bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX) 'RotateFlipType.RotateNoneFlipX
        End If

        If CheckBox16.Checked Then 'This is the Flip XY checkbox   'My.Settings.myinculeft
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY) 'RotateFlipType.RotateNoneFlipX
        End If
        If CheckBox27.Checked Then 'This is the Flip X checkbox   'My.Settings.myinculeft
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX) 'RotateFlipType.RotateNoneFlipX 
        End If

    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click  'Stop
        stopThread = True
        lightsOFF()

    End Sub


    Private Sub LinkLabel4_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        myControlDialog.Show()
    End Sub

    Dim dontsendemail As Integer


 
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click 'Timer Start
        buttonStartSession()
    End Sub

    Sub buttonStartSession()
        totalSteps = 0
        Label88.Text = 0
        currentListItem = 0
        specialSesssion = False
        startSession()
        startImaging()
    End Sub


    Sub startSession()
        LabelRound.Text = 0
        If ThreeImages.Checked Then
            TextBox31.Text = TextBox39.Text
        End If

        RecordFocus.Checked = False
        numbackuperrors = 0
        FormNote.ShowDialog(Me)  'Info session text file
        'Save PictureBox3 which is the relative positions!
        If cancelSession = True Then
            cancelSession = False
            Exit Sub
        End If
        Label68.Text = thisPC
        My.Settings.thisPC = thisPC

        savePositionsAndConfig()
        

        'moveConfigstoprevioustxtfiles()\
        Label67.Text = imageSubfolder
        'Label68.Text = objective

        'saveConfigtxt(directoryInfo.FullName) 'save Config.txt to Release folder
    End Sub

    Sub savePositionsAndConfig()
        Try
            PictureBox3.Image.Save(sessionfolder & "\Positions.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        Catch ex As Exception
            'MsgBox("Couldn't save positions to Vanadium.. " & ex.Message)
            AddZero()  'must add at least one position to fix the error.
            PictureBox3.Image.Save(sessionfolder & "\Positions.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        End Try


        If CheckBox21.Checked Then 'this is the save to vanadium checkbox
            Try
                PictureBox3.Image.Save(vanadiumfolder & "\Positions.jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
                savePositionstxt(vanadiumfolder)
                saveConfigtxt(vanadiumfolder)
                FileCopy(sessionfolder & "\info.txt", vanadiumfolder & "\info.txt") 'copies info.txt to vanadium folder.

            Catch ex As Exception
                'MsgBox("Couldn't save positions to Vanadium.. " & ex.Message)
            End Try
        End If


        savePositionstxt(sessionfolder) 'saves SAVEDX.txt to current session folder.
        movepositionstoprevioustxtfiles() 'moves SAVEDX.txt to SAVED2X.txt etc in Release folder.
        savePositionstxt(directoryInfo.FullName & "\ConfigAndErrorFiles") 'saves SAVEDX.txt to Release folder.
        loadposcount = 1 'sets count for loading previously saved positions.
        loadconfigcount = 1
        saveConfigtxt(sessionfolder) 'save Config.txt to current imaging folder
    End Sub

    Sub startImaging()
        

        If CheckBox3.Checked Then
            addtomyConsoleMain("Changing stage speed to low")
            'MsgBox("First uncheck 'Set High speed' ")
            'Exit Sub
            CheckBox3.Checked = False
        End If



        If My.Settings.AutoStartCheckBox = False Then
            If CheckBox21.Checked Then  'save to vanadium checkbox.
                Dim result = MessageBox.Show("Make sure you have cleared Vanadium", "caption", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
            Else
                Dim result = MessageBox.Show("You are not saving to Vanadium", "caption", MessageBoxButtons.OKCancel)
                If result = DialogResult.Cancel Then
                    Exit Sub
                End If
            End If
        End If


        'CLEAR FILE MYDATA  which contains centering data
        Using writer As StreamWriter = New StreamWriter("MYDATA.txt")
            writer.Write("")
            ' writer.WriteLine("two 2")
        End Using
        Using writer As StreamWriter = New StreamWriter("MYDATA_X.txt")
            writer.Write("")
        End Using
        Using writer As StreamWriter = New StreamWriter("MYDATA_Y.txt")
            writer.Write("")
        End Using

        Console.WriteLine("*****START****")





        dontsendemail = 0
        For i As Integer = 0 To 99
            If skip(i) = True Then
                MessageBox.Show("Some positions will be skipped. If you don't want this then go to More Option>Skip Position...")
                Exit For
            End If
        Next

        Button5.FlatStyle = FlatStyle.Flat
        Button5.BackColor = Color.Red

        If mcl Then

        ElseIf pololumode = True Then
            'GotoPos1Pololu()
        Else
            If RadioButton25.Checked Then   ' radiobutton no-stage selected, so don't send command to stage.
            Else
                ' GotoPos1()

            End If

        End If


        Label42.Text = currentListItem + 1

        timerStopped = False
        startCountDown = False
        'gotoNextwell = Tr


        drawPostions()

        If RadioButton25.Checked Then  'no stage radiobutton
            Dim result = MessageBox.Show(" No stage radiobutton is checked!", "caption", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Exit Sub
            End If
        End If


        'Do focusing
        If CheckBox7.Checked And currentListItem = positiontoautofocus - 1 Then  'remember this is repeated at Timer1_Tick!!!!
            Label7.Text = "Refocusing at Pos " & currentListItem + 1
            Label7.Show()
            'autofocusingButtonAlias()
            callerisTest = False
            'dofocus()
            newfocus()

        End If

        'Do alignment
        If CheckBox19.Checked Then
            Label7.Text = "Auto-aligning..."
            Label7.Show()
            addtomyConsole(Label7.Text)

            alignX()
            If CheckBox8.Checked Then  'don't do alignY if checkbox8 is checked. this is for stages where only x axis works.
            Else
                alignY()
            End If


            Thread.Sleep(300)
            Zero() 'it's important to zero after aligning!
            Thread.Sleep(300)

            'save this darkfield image
            RadioButton9.Checked = True
            GrabandSave()



            ' calibrateMS2000()
        End If

        Label7.Text = "Grabbing Image at Pos " & currentListItem + 1
        Console.WriteLine(Label7.Text)
        Label7.Show()
        Label7.Refresh()
        justStarted = True
        continuation()

    End Sub

    Dim justStarted As Boolean = True
    Sub continuation()
        
        LabelRound.Text = LabelRound.Text + 1
        'If LabelRound.Text = 2 Then
        '    loaddatafromRelease("SAVEDXA.txt", "SAVEDYA.txt")
        '    FormFocusingRecorded.Calc1to36()

        'End If

        lastpositionX = "0"
        lastpositionY = "0"
        thispositionX = "0"
        thispositionY = "0"

        If pololumode And CheckBox18.Checked Then  'backlash correction. (CheckBox18)
            backlashcorrectionleftup()
        End If

        ' backlashcorrectionMs4Y()
        'backlashcorrectionMs4X()



        scheduled()



        GrabandSaveAllSelected()
        justStarted = False

        'this may not be necessary
        Label7.Hide()
        PictureBox1.Refresh()
        '
        If RadioButton25.Checked Then  'no stage radiobutton
            startCountDown = True
            interval = TextBox1.Text
            t1 = interval
            Label7.Text = "Next Image in... " & t1
            Label7.Show()
            Timer1.Start()
        Else
            NextWell()
        End If


    End Sub

    Sub backlashcorrectionleftup()
        'move up 1
        movePololu(0, pololusteps, 1, 1)
        'move left 1
        movePololu(1, pololustepsY, 1, 0)
        'move down 1
        movePololu(1, pololusteps, 1, 1)
        'move right 1
        movePololu(0, pololustepsY, 1, 0)
        Threading.Thread.Sleep(2000)
    End Sub

    Sub backlashcorrectionMs4Y()
        addtomyConsoleMain("NOW BACKLASH CORRECTION Y......")
        Label7.Text = "Correcting backlash in Y"
        Label7.Show()
        Label7.Refresh()
        'now move it  a field width (to remove backlash)'''''''''''''''''''''''''''''''''''
        addtomyConsoleMain("Moving object down")
        Str = "RM y=" 'object moves right in ms-4 new.
        Str += mywidth.ToString
        Str += ControlChars.Cr
        SerialPort1.DiscardInBuffer()
        SerialPort1.Write(Str)
        addtomyConsole(Str)
        QueryFinishedMovementThread() 'this starts a new thread that waits for the stage to respond when it has finished moving.
        WaitEvent.WaitOne()  ' this waits for the previous thread to end.
        addtomyConsole("DONE 1ST HALF OF BACKLASH CORRECTION Y.....")
        ' GrabImage()
        addtomyConsoleMain("Moving object up")
        Str = "RM y=-"  'object moves left in ms-4 new.
        Str += mywidth.ToString
        Str += ControlChars.Cr
        SerialPort1.DiscardInBuffer()
        SerialPort1.Write(Str)
        addtomyConsole(Str)
        QueryFinishedMovementThread() 'this starts a new thread that waits for the stage to respond when it has finished moving.
        WaitEvent.WaitOne()  ' this waits for the previous thread to end.
        'GrabImage()
        addtomyConsoleMain("DONE BACKLASH CORRECTION Y.")
        Label7.Hide()
    End Sub

    Sub backlashcorrectionMs4X()
        addtomyConsoleMain("NOW BACKLASH CORRECTION X")
        Label7.Text = "Correcting backlash in X"
        Label7.Show()
        Label7.Refresh()
        'now move it  a field width (to remove backlash)'''''''''''''''''''''''''''''''''''
        addtomyConsoleMain("Moving object right")
        Str = "RM X=" 'object moves right in ms-4 new.
        Str += mywidth.ToString
        Str += ControlChars.Cr
        SerialPort1.DiscardInBuffer()
        SerialPort1.Write(Str)
        addtomyConsole(Str)
        QueryFinishedMovementThread() 'this starts a new thread that waits for the stage to respond when it has finished moving.
        WaitEvent.WaitOne()  ' this waits for the previous thread to end.
        addtomyConsole("DONE 1ST HALF OF BACKLASH CORRECTION X.....")
        'GrabImage()
        addtomyConsoleMain("Moving object left")
        Str = "RM X=-"  'object moves left in ms-4 new.
        Str += mywidth.ToString
        Str += ControlChars.Cr
        SerialPort1.DiscardInBuffer()
        SerialPort1.Write(Str)
        addtomyConsole(Str)
        QueryFinishedMovementThread() 'this starts a new thread that waits for the stage to respond when it has finished moving.
        WaitEvent.WaitOne()  ' this waits for the previous thread to end.
        ' GrabImage()
        addtomyConsoleMain("DONE BACKLASH CORRECTION X")
        Label7.Hide()
    End Sub




    Sub GrabandSaveAllSelected()

        Threading.Thread.Sleep(2500)

        If CheckBox24.Checked Or UseRecorded.Checked Then  'autofocusing checkbox
            If justStarted = False Then
                changeFocus()
            Else
                mypos = currentListItem + 1  'should do this even if justStarted
            End If

        End If
        'MsgBox("mypos: " & mypos & "currentListItem: " & currentListItem)

        'If CheckBox12.Checked Then  ' CheckBox12 is "Focus all" checkbox
        '    If Label86.Text = "1" Then
        '        If My.Settings.imagewithCam2 = True Then  'if using 2 camera focus with cam2.
        '            connectToCam2()  'RadioButton14.Checked = True 'selects radiobutton cam2
        '        End If
        '        startFocusing()
        '        Label86.Text = FocusEvery.Text
        '    Else
        '        Label86.Text = (CInt(Label86.Text) - 1).ToString
        '    End If
        'End If

        If AutofocusArray(mypos) = 1 Then
            If My.Settings.imagewithCam2 = True Then  'if using 2 camera focus with cam2.
                connectToCam2()  'RadioButton14.Checked = True 'selects radiobutton cam2
            End If
            startFocusing()
            'MsgBox("Start focusing.  Mypos=" & mypos)
        End If

        If ThreeImages.Checked Then ' take three images per position at 3 different focuses
            enableMotor()
            Thread.Sleep(200)
            motorSub_DOWN()
            Thread.Sleep(1500)
            grabImagesSub()
            motorSub_UP()
            Thread.Sleep(1500)
            grabImagesSub()
            motorSub_UP()
            Thread.Sleep(1500)
            grabImagesSub()
            motorSub_DOWN()
            Thread.Sleep(1500)
            disableMotor()

        Else
            grabImagesSub()
        End If


    End Sub

    Sub grabImagesSub()

        If Label32.Text = 1 Then  ' fluo on only every certain number of images, according to textbox3.
            If CheckBox2.Checked Then 'BF2
                RadioButton7.Checked = True
                GrabandSave()
            End If

            If CheckBox4.Checked Then 'Fluo
                RadioButton8.Checked = True
                GrabandSave()
            End If
            Label32.Text = TextBox3.Text

        Else
            Label32.Text = Label32.Text - 1
        End If

        If CheckBox5.Checked Then 'DarkField
            RadioButton9.Checked = True
            GrabandSave()
        End If

        Threading.Thread.Sleep(500)

        If CheckBox1.Checked Then 'BF
            'MsgBox(My.Settings.UseBFCam1.ToString & " " & cam1selected.ToString)
            RadioButton6.Checked = True
            If My.Settings.UseBFCam1 = False And cam1selected Then ' checkbox to not use BF in Cam1 
                addtomyConsoleErrorMessages("not using BF in Cam1")
            Else
                GrabandSave()
            End If


        End If
    End Sub

    Sub changeFocus()
        If UseRecorded.Checked Then
            changeFocusNew()
        Else
            changeFocusClassic()
        End If

    End Sub

    Sub changeFocusNew()

        mypos = currentListItem + 1


        If recordedfocus(mypos) > 0 Then

            Label9.Text = "Changing Focus for Pos" & mypos
            Label9.Show()
            Label9.Refresh()
            Label7.Text = "Objective is moving up..."
            Label6.Show()
            Label7.Show()
            Refresh()
            enableMotor()
            Threading.Thread.Sleep(500)

            If CheckBox6.Checked Then
                ArduinoPin("mb" & (recordedfocus(mypos)).ToString)
            Else
                ArduinoPin("mf" & (recordedfocus(mypos)).ToString)
            End If

            Threading.Thread.Sleep(6000)
            disableMotor()
            Label9.Hide()
            Label6.Hide()
            Label11.Hide()
        ElseIf recordedfocus(mypos) < 0 Then
            Label9.Text = "Changing Focus for Pos" & mypos
            Label9.Show()
            Label9.Refresh()
            Label7.Text = "Objective is moving down..."
            Label11.Show()
            Label7.Show()
            Refresh()
            enableMotor()
            Threading.Thread.Sleep(500)


            If CheckBox6.Checked Then
                ArduinoPin("mf" & (recordedfocus(mypos) * -1).ToString)
            Else
                ArduinoPin("mb" & (recordedfocus(mypos) * -1).ToString)
            End If

            Threading.Thread.Sleep(6000)
            disableMotor()
            Label9.Hide()
            Label6.Hide()
            Label11.Hide()
        End If

    End Sub

    Sub changeFocusClassic()



        mypos = currentListItem + 1
        For i As Integer = 1 To 11
            If mypos = positionFchange(i) Then
                If FchangeUp(i) Then
                    Label9.Text = "Changing Focus for Pos" & mypos
                    Label9.Show()
                    Label9.Refresh()
                    Label7.Text = "Objective is moving up..."
                    Label6.Show()
                    Label7.Show()
                    Refresh()
                    enableMotor()
                    Threading.Thread.Sleep(1000)


                    If DCmotors.Checked Then
                        ArduinoPin("mf" & (Ftimes(i) * 10).ToString)
                    Else

                        If CheckBox6.Checked Then
                            moveStepper(1, Ftimes(i), 10)
                        Else
                            moveStepper(0, Ftimes(i), 10)
                        End If

                    End If



                    'For j As Integer = 0 To (Ftimes(i) - 1)
                    '    'PinMotorUpCoarse()
                    '    'sendtoINCUMOTOR("i")
                    '    RadioButton1.Checked = True
                    '    motorSub_UP()
                    '    Threading.Thread.Sleep(300)
                    'Next


                    Threading.Thread.Sleep(7000)
                    disableMotor()
                Else
                    Label9.Text = "Changing Focus for Pos" & mypos
                    Label9.Show()
                    Label9.Refresh()
                    Label7.Text = "Objective is moving down..."
                    Label11.Show()
                    Label7.Show()
                    Refresh()
                    enableMotor()
                    Threading.Thread.Sleep(1000)


                    If DCmotors.Checked Then
                        ArduinoPin("mb" & (Ftimes(i) * 10).ToString)
                    Else

                        If CheckBox6.Checked Then
                            moveStepper(0, Ftimes(i), 10)
                        Else
                            moveStepper(1, Ftimes(i), 10)
                        End If

                    End If




                    'For j As Integer = 0 To (Ftimes(i) - 1)
                    '    'PinMotorDownCoarse()
                    '    'sendtoINCUMOTOR("d")
                    '    RadioButton1.Checked = True
                    '    motorSub_DOWN()
                    '    Threading.Thread.Sleep(300)
                    'Next


                    Threading.Thread.Sleep(7000)
                    disableMotor()
                End If
            End If
        Next
        'Label7.Text = "Grabbing Image at Pos " & currentListItem + 1 & "..."
        Label7.Hide()
        Label9.Hide()
        Label6.Hide()
        Label11.Hide()
        Refresh()
        Threading.Thread.Sleep(200)

    End Sub

    Sub GrabandSave()
        If skip(currentListItem + 1) = True And skipOnlyLight(currentListItem + 1) = True Then

        Else
            If usingCam1 And usingCam2 Then
                connectToCam1()  'RadioButton13.Checked = True 'selects radiobutton cam1
                GrabImageSub()
                PictureBox1.Refresh()

                SaveImage()
                'MsgBox("saved 1")
                Thread.Sleep(1000) 'waits for image to be saved
                connectToCam2() 'RadioButton14.Checked = True 'selects radiobutton cam2
                GrabImageSub()
                PictureBox1.Refresh()

                SaveImage()
                ' MsgBox("saved 2")
                usingCam1 = True 'we must set them back to true becuase checking rb13 and rb14 uncheckes them!  and also do it after savimage()!
                usingCam2 = True 'we must set them back to true becuase checking rb13 and rb14 uncheckes them!  and also do it after savimage()!

            End If
            If usingCam1 = True And usingCam2 = False Then
                connectToCam1()  'RadioButton13.Checked = True  'selects radiobutton cam1
                GrabImageSub()
                PictureBox1.Refresh()
                SaveImage()
            End If
            If usingCam1 = False And usingCam2 = True Then
                connectToCam2()  'RadioButton14.Checked = True  'selects radiobutton cam2
                GrabImageSub()
                PictureBox1.Refresh()
                SaveImage()
            End If


        End If



    End Sub

    Dim SessionNotFinished As Boolean
    Dim effectiveTotalSteps As Integer
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        effectiveTotalSteps = totalSteps + Convert.ToInt32(My.Settings.recordedfocus(1))
        If effectiveTotalSteps > 500 Or effectiveTotalSteps < -500 Then
            stopSession()
            MsgBox("It looks like the sample is out of focus. The imaging session has been stopped. Refocus and restart the session.")
        End If
        
        t1 = t1 - 1


        If specialSesssion Then

            specialSesssionCount += 1
            If specialSesssionCount < 5 Then
                Thread.Sleep(2000)
            End If
            If specialSesssionCount = 1 Then
                t1 = 0
                moveTo(-4620, 0)
                sessionfolder = sessionfolderOriginal.Substring(0, sessionfolderOriginal.Length - 1) + "B"
                vanadiumfolder = vanadiumfolderOriginal.Substring(0, vanadiumfolderOriginal.Length - 1) + "B"
                Label67.Text = imageSubfolder.Substring(0, imageSubfolder.Length - 1) + "B"
                If RadioButtonX2.Checked Then
                    specialSesssionCount = 3
                End If
            ElseIf specialSesssionCount = 2 Then
                t1 = 0
                moveTo(4620, -3540)
                sessionfolder = sessionfolderOriginal.Substring(0, sessionfolderOriginal.Length - 1) + "C"
                vanadiumfolder = vanadiumfolderOriginal.Substring(0, vanadiumfolderOriginal.Length - 1) + "C"
                Label67.Text = imageSubfolder.Substring(0, imageSubfolder.Length - 1) + "C"
                
            ElseIf specialSesssionCount = 3 Then
                t1 = 0
                moveTo(-4620, 0)
                sessionfolder = sessionfolderOriginal.Substring(0, sessionfolderOriginal.Length - 1) + "D"
                vanadiumfolder = vanadiumfolderOriginal.Substring(0, vanadiumfolderOriginal.Length - 1) + "D"
                Label67.Text = imageSubfolder.Substring(0, imageSubfolder.Length - 1) + "D"
            ElseIf specialSesssionCount = 4 Then
                If RadioButtonX2.Checked Then
                    moveTo(4620, 0)
                Else
                    moveTo(4620, 3540)
                End If

                sessionfolder = sessionfolderOriginal.Substring(0, sessionfolderOriginal.Length - 1) + "A"
                vanadiumfolder = vanadiumfolderOriginal.Substring(0, vanadiumfolderOriginal.Length - 1) + "A"
                Label67.Text = imageSubfolder.Substring(0, imageSubfolder.Length - 1) + "A"
                specialSesssionCount = 5
                t1 = interval
                Thread.Sleep(6000)
            End If


            If (Not System.IO.Directory.Exists(sessionfolder)) Then
                Try
                    System.IO.Directory.CreateDirectory(sessionfolder)
                Catch ex As Exception
                    MsgBox("That directory doesn't exist, you probably got the drive letter wrong")
                End Try
            End If
            If specialSesssionCount < 5 Then
                Thread.Sleep(7000)
            End If

            Zero()
            Thread.Sleep(100)
        End If



        Label7.Text = "Next Image in... " & t1
        Label7.Show()


        TextBox2.Text = t1
        TextBox2.Refresh()

        If t1 = 0 Then
            If specialSesssionCount > 5 Then
                specialSesssionCount = 0
            End If
            dontsendemail = dontsendemail + 1
            Label7.Hide()
            Timer1.Stop()
            'changeFocus()

            'do alignment
            If CheckBox19.Checked Then
                Label7.Text = "Auto-aligning..."
                Label7.Show()
                addtomyConsole(Label7.Text)

                alignX()
                If CheckBox8.Checked Then  'don't do alignY if checkbox8 is checked. this is for stages where only x axis works.
                Else
                    alignY()
                End If

                Thread.Sleep(300)
                Zero() 'it's important to zero after aligning!
                Thread.Sleep(300)

                'save this darkfield image
                RadioButton9.Checked = True
                GrabandSave()
            End If


            'do autofocusing
            If CheckBox7.Checked And currentListItem = positiontoautofocus - 1 Then
                autofocusCounter = autofocusCounter - 1
                If autofocusCounter = 0 Then
                    'autofocusingButtonAlias()
                    autofocusCounter = TextBox4.Text
                    callerisTest = False
                    Label7.Text = "Refocusing at Pos " & currentListItem + 1
                    Label7.Show()
                    'dofocus()
                    newfocus()
                End If
            End If
            Label7.Text = "Grabbing Image at Pos " & currentListItem + 1
            Label7.Show()
            Label7.Refresh()
            Console.WriteLine(Label7.Text)
            continuation()



            'The following code is now in a sub called continuation()
            'If CheckBox3.Checked Then

            '    calibrateMS2000()


            'End If

            'GrabandSaveAllSelected()
            ''t1 = interval
            'NextWell()


        End If






    End Sub





    Private Sub NextWell()

        While skip(currentListItem + 2) = True And skipOnlyLight(currentListItem + 2) = False
            currentListItem = currentListItem + 1
            Label42.Text = currentListItem + 1
            ' MessageBox.Show("SKIP position" + currentListItem + 2)
        End While

        If ListBox2.Items.Count - 1 > currentListItem Then 'if it's not the last item.

            Label7.Text = "Moving to Pos " & currentListItem + 2 & "..."
            Console.WriteLine(Label7.Text)
            Label7.Show()
            PictureBox1.Refresh()
            Label7.Refresh()
            

            If pololumode Then
                currentListItem = currentListItem + 1
                Label42.Text = currentListItem + 1
                item = CInt(ListBox2.Items(currentListItem).ToString)
                item2 = CInt(ListBox3.Items(currentListItem).ToString)
                'MsgBox("next well is " & item & ", " & item2)
                'x pos
                If (item - pololuPosX) > 0 Then
                    'move up
                    movePololu(0, pololusteps, item - pololuPosX, 1)
                ElseIf (item - pololuPosX) < 0 Then
                    'move down
                    movePololu(1, pololusteps, pololuPosX - item, 1)
                ElseIf (item - pololuPosX) = 0 Then
                End If
                'y pos
                ' MsgBox("starting mov")
                If (item2 - pololuPosY) > 0 Then
                    'move right
                    'Console.WriteLine("pololusteps " & pololustepsY & "pololurepetitions " & (item2 - pololuPosY).ToString)
                    movePololu(0, pololustepsY, item2 - pololuPosY, 0)
                    '  MsgBox("mov1")
                ElseIf (item2 - pololuPosY) < 0 Then
                    'move left
                    'Console.WriteLine("pololusteps " & pololustepsY & "pololurepetitions " & (pololuPosY - item2).ToString)
                    movePololu(1, pololustepsY, pololuPosY - item2, 0)
                    ' MsgBox("mov2")
                ElseIf (item2 - pololuPosY) = 0 Then
                    ' MsgBox("mov3")
                End If
                'pololu sleep, pololu wait time
                Console.WriteLine("waiting for for movement to end  " & (pololusteps * (Math.Abs(pololuPosY - item2) + Math.Abs(pololuPosX - item)) * 6 / 1000).ToString & " seconds....")
                Threading.Thread.Sleep(pololusteps * (Math.Abs(pololuPosY - item2) + Math.Abs(pololuPosX - item)) * 6)
                Console.WriteLine(".")
                'backlash correction:
                'If (currentListItem Mod nbyn) = 0 Then
                If CheckBox18.Checked Then
                    backlashcorrectionleftup()
                End If
                'End If
                Threading.Thread.Sleep(2000)
                Console.WriteLine("finished waiting!")
                finishedMoving = True
                'Console.WriteLine("finishedMoving True")
                pololuPosX = pololuPosX + item - pololuPosX
                Label36.Text = pololuPosX
                pololuPosY = pololuPosY + item2 - pololuPosY
                Label37.Text = pololuPosY

            ElseIf mcl Then

                item = CInt(ListBox2.Items(currentListItem + 1).ToString)
                item2 = CInt(ListBox3.Items(currentListItem + 1).ToString)
                Str = "U" & Chr(7) & "r" & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(0) & item & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(1) & item2 & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(80) & Chr(13)
                SerialPort1.Write(Str)
                currentListItem = currentListItem + 1
                Label42.Text = currentListItem + 1
                'Threading.Thread.Sleep(4000)
                'QueryFinishedMovementThreadMCL()
                'WaitEvent.WaitOne()
                'finishedMoving = True

            ElseIf DCmotors.Checked Then

                ArduinoPin("x" & ListBox2.Items(currentListItem + 1).ToString & "y" & ListBox3.Items(currentListItem + 1).ToString)
                currentListItem = currentListItem + 1
                Label42.Text = currentListItem + 1

            Else  'if not pololu or mcl *********************************************************************************************


                Str = "MOVE X=" 'this is press of right arrow (movement in X !!!)
                'str += distance * FormPos.ListBox1.Items(currentListItem).ToString
                'Dim dist As Integer = CInt(distance)
                item = CInt(ListBox2.Items(currentListItem + 1).ToString)
                str2 = item
                Str += str2
                str3 = " Y="  'this is press of up arrow (movement in Y !!!)
                Str += str3
                item2 = CInt(ListBox3.Items(currentListItem + 1).ToString)
                str4 = item2
                Str += str4
                Str += ControlChars.Cr
                SerialPort1.Close()
                SerialPort1.Open()
                SerialPort1.Write(Str) 'no movement 
                currentListItem = currentListItem + 1
                Label42.Text = currentListItem + 1
                thispositionX = item
                thispositionY = item2
            End If


            gotoNextwell = True

            If pololumode Then

                timer5tick()
            ElseIf mcl Then
                ' addtomyConsole("timer5tick()")
                '  timer5tick() 'grabs image and calls nextwell (this function) again.

                QueryFinishedMovement() 'this calls timer5

            Else

                QueryFinishedMovement()
            End If


        Else

            Console.WriteLine("this is last pos, now going to 1")

            gotoNextwell = False
            Label7.Hide()
            currentListItem = 0
            Label42.Text = currentListItem + 1

            'If CheckBox24.Checked Or UseRecorded.Checked Then
            '    changeFocus()
            'End If


            startCountDown = True
            If pololumode Then
                finishedMoving = True
                GotoPos1Pololu()
            ElseIf mcl Then
                GotoPos1Mcl()
            ElseIf DCmotors.Checked Then
                DcgotoZero()
                QueryFinishedMovement()
            Else
                GotoPos1() ' no movement
            End If


        End If



        ' nextWellThread.Abort()

    End Sub

    Dim finishedMoving As Boolean = False
    Dim gotoNextwell As Boolean = False
    Dim startCountDown As Boolean = False
    Private Sub Timer5_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer5.Tick  'this is queryfinishedmovement
        timer5tick()
    End Sub

    Dim lastpositionX As String
    Dim lastpositionY As String
    Dim thispositionX As String
    Dim thispositionY As String

    
    Sub QueryFinishedMovement()
        If DCmotors.Checked Then
        Else
            mytext = SerialPort1.ReadExisting
        End If
        Timer5.Start()
    End Sub

    Sub timer5tick() 'this is queryfinishedmovement
        If finishedMoving Then

            Console.WriteLine("finishedmoving true")
            Timer5.Stop()
            finishedMoving = False

            If timerStopped Then
                Label7.Text = "Stopped at Pos " & currentListItem + 1
                timerStopped = False
            Else
                If gotoNextwell Then  'this grabs image and calls nextwell


                    If CheckBox20.Checked Then  'do ms-4 backlash correction checkbox.
                        If CInt(lastpositionY) <> CInt(thispositionY) Then   'do backlash correction in both directions if Y changes, becuase we are imaging line by line, so a change in y is a new line
                            backlashcorrectionMs4Y()
                            'End If
                            'If CInt(lastpositionX) <> CInt(thispositionX) Then
                            backlashcorrectionMs4X()
                        End If
                    End If

                    lastpositionX = thispositionX
                    lastpositionY = thispositionY


                    Console.WriteLine("gotonextwell true")
                    'If CheckBox24.Checked Or UseRecorded.Checked Then  'autofocusing checkbox
                    '    changeFocus()
                    'End If

                    'do alignment
                    If CheckBox19.Checked And CheckBox25.Checked And currentListItem = positiontoautoalign - 1 Then
                        Label7.Text = "Auto-aligning..."
                        Label7.Show()
                        addtomyConsole(Label7.Text)
                        gettempXandY()
                        oldXpos = tempXpos
                        oldYpos = tempYpos
                        alignX()
                        If CheckBox8.Checked Then  'don't do alignY if checkbox8 is checked. this is for stages where only x axis works.
                        Else
                            alignY()
                        End If
                        Thread.Sleep(300)
                        gettempXandY()
                        changeXpos = tempXpos - oldXpos
                        changeYpos = tempYpos - oldYpos
                        changeAllfollowingPositionsby(changeXpos, changeYpos)
                        'Zero() 'it's important to zero after aligning!
                        Thread.Sleep(300)

                        'save this darkfield image
                        RadioButton9.Checked = True
                        GrabandSave()
                    End If

                    'do autofocusing
                    If CheckBox7.Checked And (currentListItem = positiontoautofocus - 1 Or (CheckBox26.Checked And currentListItem = positiontoautofocus2 - 1)) Then  'if autofocus one step at this position.
                        autofocusCounter = autofocusCounter - 1
                        If autofocusCounter = 0 Then
                            'autofocusingButtonAlias()
                            autofocusCounter = TextBox4.Text
                            callerisTest = False
                            Label7.Text = "Refocusing at Pos " & currentListItem + 1
                            Label7.Show()
                            'dofocus()
                            newfocus()
                        End If
                    End If




                    Label7.Text = "Grabbing Image at Pos " & currentListItem + 1 & "..."
                    Console.WriteLine(Label7.Text)
                    Label7.Show()
                    Label7.Refresh()
                    GrabandSaveAllSelected() '**********************************************************GRABS IMAGE
                    Timer2.Start() 'Waits 1sec and calls Next well.

                Else 'else go home

                    Label7.Hide()

                    If startCountDown Then
                        Console.WriteLine("startcountdown true")

                        'QueryFinishedMovementThread() 'so that it waits until the movement has finished before starting countdown
                        'WaitEvent.WaitOne()

                        interval = TextBox1.Text
                        t1 = interval
                        Label7.Text = "Next Image in... " & t1
                        Label7.Show()
                        Timer1.Start()
                        startCountDown = False
                    Else
                        Console.WriteLine("startcountdown false")
                        If lightisOn = False Then
                            'MsgBox("g")
                            Thread.Sleep(700)
                            GrabImage()
                        End If
                        'highSpeed()
                    End If

                End If
            End If

        ElseIf finishedMoving = False Then

            If pololumode Then

            ElseIf mcl Then
                addtomyConsole("QueryFinishedMovementThreadMCL()")
                QueryFinishedMovementThreadMCL()
                WaitEvent.WaitOne()
                finishedMoving = True
                addtomyConsole("QueryFinishedMovementThreadMCL() finished")

            ElseIf DCmotors.Checked Then
                mytext = TextBox33.Text 'SerialPort.ReadExisting
                Console.WriteLine(mytext)
                addtomyConsole(mytext)
                If mytext.Contains("S") Then
                    Console.WriteLine("Stage has stopped")
                    addtomyConsole("Stage has stopped")
                    finishedMoving = True
                End If


            Else
                Console.WriteLine("finishedmoving false")
                Label7.Show()
                Str = "STATUS"
                Str += ControlChars.Cr
                SerialPort1.Write(Str)
                mytext = SerialPort1.ReadExisting
                Console.WriteLine(mytext)
                addtomyConsole(mytext)
                If mytext.Contains("N") Then
                    Console.WriteLine("Stage has stopped")
                    addtomyConsole("Stage has stopped")
                    finishedMoving = True
                End If
            End If
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click  'Save this
        SaveImage()
    End Sub

    Sub SaveImage()
        mydate = DateTime.Now.ToString("MM-dd-yyyy_(HH-mm-ss-tt)")

        If RadioButton6.Checked Then
            typeoflight = "_bf"
        End If
        If RadioButton7.Checked Then
            typeoflight = "_bf2"
        End If
        If RadioButton8.Checked Then
            typeoflight = "_fl"
        End If
        If RadioButton9.Checked Then
            typeoflight = "_df"
        End If


        If cam1selected Then
            camtype = "_Cam1"
        ElseIf cam2selected Then
            camtype = "_Cam2"
        End If
        

        If (Not System.IO.Directory.Exists(sessionfolder)) Then
            System.IO.Directory.CreateDirectory(sessionfolder)
        End If
        If (Not System.IO.Directory.Exists(sessionfolder & "\Pos" & currentListItem + 1 & typeoflight & camtype)) Then
            System.IO.Directory.CreateDirectory(sessionfolder & "\Pos" & currentListItem + 1 & typeoflight & camtype)
        End If
        'PictureBox1.Image.Save("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        'PictureBox1.Image.Save("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & Label8.Text & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        'PictureBox1.Image.Save("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        Try
            'PictureBox1.Image.Save("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".bmp", System.Drawing.Imaging.ImageFormat.Bmp)
            PictureBox1.Image.Save(sessionfolder & "\Pos" & currentListItem + 1 & typeoflight & camtype & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg) 'ImageFormat.Jpeg
            'MsgBox(imagefolder)
        Catch ex As Exception
            sendemailMemory()  'SENDS AN EMAIL STATING THAT THERE IS PROBABLY NO MORE MEMORY IN C:
            Label57.Visible = True  '"Probably not enough memory"
        End Try

        'NOW SAVE TO VANADIUM
        Try
            If CheckBox21.Checked Then  'save to vanadium checkbox
                If (Not System.IO.Directory.Exists(vanadiumfolder)) Then
                    System.IO.Directory.CreateDirectory(vanadiumfolder)
                End If
                If (Not System.IO.Directory.Exists(vanadiumfolder & "\Pos" & currentListItem + 1 & typeoflight & camtype)) Then
                    System.IO.Directory.CreateDirectory(vanadiumfolder & "\Pos" & currentListItem + 1 & typeoflight & camtype)
                End If
                PictureBox1.Image.Save(vanadiumfolder & "\Pos" & currentListItem + 1 & typeoflight & camtype & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
            End If
        Catch ex As Exception
            Thread.Sleep(1000) 'wait a litlle bit before trying again
            'Try catch within a try catch!
            Try
                'Try one more time:
                If CheckBox21.Checked Then  'save to vanadium checkbox
                    If (Not System.IO.Directory.Exists(vanadiumfolder)) Then
                        System.IO.Directory.CreateDirectory(vanadiumfolder)
                    End If
                    If (Not System.IO.Directory.Exists(vanadiumfolder & "\Pos" & currentListItem + 1 & typeoflight & camtype)) Then
                        System.IO.Directory.CreateDirectory(vanadiumfolder & "\Pos" & currentListItem + 1 & typeoflight & camtype)
                    End If
                    PictureBox1.Image.Save(vanadiumfolder & "\Pos" & currentListItem + 1 & typeoflight & camtype & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
                End If
            Catch ex2 As Exception
                numbackuperrors = numbackuperrors + 1
                'MessageBox.Show(ex.Message)
                FILE_NAME = "Backup_errors.txt"  'creates a text file with the errrors in the .exe folder.
                Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter.WriteLine("Backup error " & numbackuperrors & " at " & DateAndTime.Now)
                objWriter.Close()

                'now save it to this session's folder:
                FILE_NAME = sessionfolder & "\Backup_errors.txt"  'creates a text file with the errrors in the .exe folder.
                Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME, True)
                objWriter2.WriteLine("Backup error " & numbackuperrors & " at " & DateAndTime.Now)
                objWriter2.Close()
                'MsgBox("vanadiumfolder: " & vanadiumfolder & " exception: " & ex.Message)
                Label27.Text = "Failed Vanadium Backup " & numbackuperrors & " at " & DateAndTime.Now
                addtomyConsoleErrorMessages(Label27.Text)
                Label27.Visible = True
                'sendemailBackupError()
            End Try
        End Try
    End Sub

    Sub saveImageWithName(ByVal vb As Integer, ByVal name As String)
        ' mydate = DateTime.Now.ToString("MM-dd-yyyy_(HH-mm-ss-tt)")


        'If RadioButton6.Checked Then
        '    typeoflight = "_bf"
        'End If
        'If RadioButton7.Checked Then
        '    typeoflight = "_bf2"
        'End If
        'If RadioButton8.Checked Then
        '    typeoflight = "_fl"
        'End If
        'If RadioButton9.Checked Then
        '    typeoflight = "_df"
        'End If


        'If (Not System.IO.Directory.Exists("C:\Images\Now")) Then
        '    System.IO.Directory.CreateDirectory("C:\Images\Now")
        'End If
        'If (Not System.IO.Directory.Exists("C:\Images\Now\Pos" & currentListItem + 1)) Then
        '    System.IO.Directory.CreateDirectory("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight)
        'End If
        Dim path As String = imagefolder & "\Now\Pos" & currentListItem + 1 & "_bf\" & ntimerfortest & "\"

        If (Not System.IO.Directory.Exists(path)) Then
            System.IO.Directory.CreateDirectory(path)
        End If


        'PictureBox1.Image.Save("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        'PictureBox1.Image.Save("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & Label8.Text & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        vb = vb + 1
        'PictureBox1.Image.Save("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & "_" & vb.ToString & "_" & name & ".bmp", System.Drawing.Imaging.ImageFormat.Bmp)
        bitmap.Save(path & name & "_" & vb.ToString & ".bmp", System.Drawing.Imaging.ImageFormat.Bmp)


    End Sub

    Dim timerStopped As Boolean = False
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click 'Stop Timer
        stopSession()
    End Sub

    Sub stopSession()
        Button5.BackColor = SystemColors.ControlDark
        Button5.FlatStyle = FlatStyle.System

        timerStopped = True
        Timer1.Stop()
        Label7.Hide()
        startCountDown = False
        gotoNextwell = False
        'highSpeed()
        Timer5.Stop()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If TextBox31.Text <> "" Then
            Try
                interval = TextBox1.Text
                TextBox2.Text = TextBox1.Text
                My.Settings.interval = interval.ToString
                My.Settings.Save()
            Catch ex As Exception
            End Try
        End If




    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            If (Not System.IO.Directory.Exists(sessionfolder)) Then
                System.IO.Directory.CreateDirectory(sessionfolder)
            End If


            'Process.Start(imagefolder & "\Now")
            Process.Start(sessionfolder)

        Catch ex As Exception

            MessageBox.Show("There is no folder")

        End Try
    End Sub


    '*******************************

    'NOW HID CODE!

    '***********************************
    ' vendor and product IDs
    Private Const VendorID As Short = 1121    'Replace with your device's
    Private Const ProductID As Short = 4      'product and vendor IDs
    ' read and write buffers
    Private Const BufferInSize As Short = 65 '55 'Size of the data buffer coming IN to the PC
    Private Const BufferOutSize As Short = 65 '55    'Size of the data buffer going OUT from the PC
    Dim BufferIn(BufferInSize) As Byte          'Received data will be stored here - the first byte in the array is unused
    Dim BufferOut(BufferOutSize) As Byte    'Transmitted data is stored here - the first item in the array must be 0
    Dim Status(55) As Char  'Used for PMT's Off and Resume button

    Dim NameStr As String = "incuMOtor"
    Dim pHandle As Integer
    Dim pHandleIncuscope As Integer
    Dim pHandleIncumotor As Integer
    Dim pHandlePololu As Integer
    Dim NameLength As Integer = 9
    Dim pName As Integer


    Sub sendtoINCUSCOPE(ByVal str As String)
        ConnectToHID(Me)
        nitems = hidGetItemCount()
        For i = 0 To (nitems - 1)
            pHandleIncuscope = hidGetItem(i)
            pName = hidGetProductName(pHandleIncuscope, NameStr, NameLength)
            If NameStr = "INCUSCOPE" Then
                Exit For
            End If
        Next
        If NameStr <> "INCUSCOPE" Then
            If testmode = False Then
                timedMessage = NameStr & "  You did not connect the PIC with device name INCUSCOPE"
                time = 4
                FormTimedDialog.Show()
                addtomyConsoleErrorMessages(timedMessage)
                'MsgBox(NameStr & "  You did not connect the PIC with device name INCUSCOPE")
            End If
        Else
            Write2Buffer(str) 'PIC looks for "#" as delimiter
            hidWrite(pHandleIncuscope, BufferOut(0))
            Dim mybyte() As Byte = {50, 10}
            'Dim hexo As String = BitConverter.ToString(mybyte)

            'MsgBox(hexo)

        End If
        DisconnectFromHID()
    End Sub

    Sub sendtoINCUMOTOR(ByVal str As String)
        ConnectToHID(Me)
        nitems = hidGetItemCount()
        For i = 0 To (nitems - 1)
            pHandleIncumotor = hidGetItem(i)
            pName = hidGetProductName(pHandleIncumotor, NameStr, NameLength)
            If NameStr = "INCUMOTOR" Then
                Exit For
            End If
        Next
        If NameStr <> "INCUMOTOR" Then
            If testmode = False Then
                'MsgBox(NameStr & "  You did not connect the PIC with device name INCUMOTOR")
                timedMessage = NameStr & "  You did not connect the PIC with device name INCUMOTOR"
                addtomyConsoleErrorMessages(timedMessage)
                time = 4
                FormTimedDialog.Show()
            End If
        Else
            Write2Buffer(str) 'PIC looks for "#" as delimiter
            hidWrite(pHandleIncumotor, BufferOut(0))
        End If
        DisconnectFromHID()
    End Sub

    Sub sendtoINCUMOTOR2(ByVal str As String)
        ConnectToHID(Me)
        nitems = hidGetItemCount()
        For i = 0 To (nitems - 1)
            pHandleIncumotor = hidGetItem(i)
            pName = hidGetProductName(pHandleIncumotor, NameStr, NameLength)
            If NameStr = "INCUMOTOR" Then
                Exit For
            End If
        Next
        If NameStr <> "INCUMOTOR" Then
            If testmode = False Then
                ' MsgBox(NameStr & "  You did not connect the PIC with device name INCUMOTOR")
                timedMessage = NameStr & "  You did not connect the PIC with device name INCUMOTOR"
                addtomyConsoleErrorMessages(timedMessage)
                time = 4
                FormTimedDialog.Show()
            End If
        Else
            Write2Buffer(str) 'PIC looks for "#" as delimiter
            hidWrite(pHandleIncumotor, BufferOut(0))
        End If
        DisconnectFromHID()
    End Sub

    Public Sub Write2Buffer(ByVal Command As String)
        Dim i As Short
        For i = 1 To Command.Length
            BufferOut(i) = Asc(Command.Substring(i - 1, 1)) 'Substring index starts at 0
            'BufferOut(i) = (Command.Substring(i - 1, 1)) 'Substring index starts at 0
            'MsgBox(i & " - " & BufferOut(i))

        Next
    End Sub

    'Public Sub MyWrite2Buffer(ByVal Command As Byte)
    '    'Dim i As Short
    '    'For i = 1 To Command.Length
    '    '    BufferOut(i) = Asc(Command.Substring(i - 1, 1)) 'Substring index starts at 0
    '    '    'BufferOut(i) = (Command.Substring(i - 1, 1)) 'Substring index starts at 0
    '    '    MsgBox(i & " - " & BufferOut(i))
    '    'Next
    '    BufferOut(1) = Command

    'End Sub



    '*****************************************************************
    ' a HID device has been plugged in...
    '*****************************************************************
    Public Sub OnPlugged(ByVal pHandle As Integer)
        If hidGetVendorID(pHandle) = VendorID And hidGetProductID(pHandle) = ProductID Then
            ' ** YOUR CODE HERE **

        End If
    End Sub

    '*****************************************************************
    ' a HID device has been unplugged...
    '*****************************************************************
    Public Sub OnUnplugged(ByVal pHandle As Integer)
        If hidGetVendorID(pHandle) = VendorID And hidGetProductID(pHandle) = ProductID Then
            hidSetReadNotify(hidGetHandle(VendorID, ProductID), False)
            ' ** YOUR CODE HERE **
            Dim i As Short
            For i = 1 To UBound(BufferIn)
                BufferIn(i) = Asc("0")
            Next
        End If
    End Sub

    '*****************************************************************
    ' controller changed notification - called
    ' after ALL HID devices are plugged or unplugged
    '*****************************************************************
    Public Sub OnChanged()
        ' get the handle of the device we are interested in, then set
        ' its read notify flag to true - this ensures you get a read
        ' notification message when there is some data to read...
        Dim pHandle As Integer
        pHandle = hidGetHandle(VendorID, ProductID)
        hidSetReadNotify(hidGetHandle(VendorID, ProductID), True)
        'Clear Input Buffer
        Dim i As Short
        For i = 1 To UBound(BufferIn)
            BufferIn(i) = Asc("0")
        Next
    End Sub

    '*****************************************************************
    ' on read event...
    '*****************************************************************
    Public Sub OnRead(ByVal pHandle As Integer)
        ' read the data (don't forget, pass the whole array)...
        If hidRead(pHandle, BufferIn(0)) Then
            ' ** YOUR CODE HERE **
            ' first byte is the report ID, e.g. BufferIn(0)
            ' the other bytes are the data from the microcontroller...
        End If
    End Sub

    Sub PinMotorDown()
        Str = "r" & Chr(1)
        sendtoINCUSCOPE(Str)
    End Sub
    Sub PinMotorUp()
        Str = "f" & Chr(1)
        sendtoINCUSCOPE(Str)
    End Sub
    Sub PinMotorUpStop()
        Str = "f" & Chr(0)
        sendtoINCUSCOPE(Str)
    End Sub
    Sub PinMotorDownStop()
        Str = "r" & Chr(0)
        sendtoINCUSCOPE(Str)
    End Sub
    Sub PinMotorDownFine()
        Str = "r" & Chr(finefocusinterval)
        sendtoINCUSCOPE(Str)
    End Sub
    Sub PinMotorDownCoarse()
        Str = "r" & Chr(coarsefocusinterval)
        sendtoINCUSCOPE(Str)
    End Sub
    Sub PinMotorUpFine()
        Str = "f" & Chr(finefocusinterval)
        sendtoINCUSCOPE(Str)
    End Sub
    Sub PinMotorUpCoarse()
        Str = "f" & Chr(coarsefocusinterval)
        sendtoINCUSCOPE(Str)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If RecordFocus.Checked Then

            If Label42.Text = "-" Then
                MsgBox("You must first select a position in order to save the focus")
                Exit Sub
            Else
                Try
                    If RadioStepsA.Checked Then
                        recordedfocus(CInt(Label42.Text)) = recordedfocus(CInt(Label42.Text)) + CInt(TextBox32.Text)
                    ElseIf RadioStepsB.Checked Then
                        recordedfocus(CInt(Label42.Text)) = recordedfocus(CInt(Label42.Text)) + CInt(TextBox31.Text)
                    End If

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If


        End If


        'move objective up

        'Send("4")
        'up = True
        'If AutoOff.Checked Then
        enableMotor()
        Thread.Sleep(200)
        'End If
        motorSub_UP()
        'for DC motor
        'FUp()
        'If AutoOff.Checked Then
        '    TimerMotorsOffin5sec.Start()
        'End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
       
        If RecordFocus.Checked Then
            If Label42.Text = "-" Then
                MsgBox("You must first select a position in order to save the focus")
                Exit Sub
            Else
                Try
                    If RadioStepsA.Checked Then
                        recordedfocus(CInt(Label42.Text)) = recordedfocus(CInt(Label42.Text)) - CInt(TextBox32.Text)
                    ElseIf RadioStepsB.Checked Then
                        recordedfocus(CInt(Label42.Text)) = recordedfocus(CInt(Label42.Text)) - CInt(TextBox31.Text)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End If
        End If

        'move objective down

        'Send("3")
        'up = False
        'If AutoOff.Checked Then
        enableMotor()
        Thread.Sleep(200)
        'End If
        motorSub_DOWN()
        'for DC motor
        'Fdown()
        'If AutoOff.Checked Then
        '    TimerMotorsOffin5sec.Start()  'its now actually 15seconds instead of 5
        'End If
    End Sub

    Sub motorSub_UP()

        Label6.Show()
        Label6.Refresh()
        Label7.Text = "Objective is moving up..."
        Label7.Show()
        Label7.Refresh()

        If CheckBox6.Checked Then
            If RadioStepsA.Checked Then
                ArduinoPin("mb" & TextBox32.Text)
            ElseIf RadioStepsB.Checked Then
                ArduinoPin("mb" & TextBox31.Text)
            End If

        Else
            If RadioStepsA.Checked Then
                ArduinoPin("mf" & TextBox32.Text)
            ElseIf RadioStepsB.Checked Then
                ArduinoPin("mf" & TextBox31.Text)
            End If

        End If



        'If DCmotors.Checked Then     
        '    ArduinoPin("mf" & (CInt(TextBox31.Text) * CInt(TextBox32.Text)).ToString)
        'Else

        '    If CheckBox6.Checked Then
        '        moveStepper(1, CInt(TextBox31.Text), CInt(TextBox32.Text))
        '    Else
        '        moveStepper(0, CInt(TextBox31.Text), CInt(TextBox32.Text))
        '    End If

        'End If


        ' ''5um
        'If RadioButton1.Checked = True Then
        '    'sendtoINCUMOTOR("e")
        '    If CheckBox6.Checked Then
        '        moveStepper(1, 2, 1)
        '    Else
        '        moveStepper(0, 2, 1)
        '    End If
        'End If

        ' ''20um
        'If RadioButton2.Checked = True Then
        '    'sendtoINCUMOTOR("f")
        '    If CheckBox6.Checked Then
        '        moveStepper(1, 8, 1)
        '    Else
        '        moveStepper(0, 8, 1)
        '    End If
        'End If

        ' ''100um
        'If RadioButton12.Checked = True Then
        '    'sendtoINCUMOTOR("g")
        '    If CheckBox6.Checked Then
        '        moveStepper(1, 50, 1)
        '    Else
        '        moveStepper(0, 50, 1)
        '    End If
        'End If

        ' ''continuous
        'If RadioButton3.Checked = True Then
        '    'sendtoINCUMOTOR("h")
        '    'moveStepper(3, 0, 1)
        '    If CheckBox6.Checked Then
        '        moveStepper(1, 255, 100)
        '    Else
        '        moveStepper(0, 255, 100)
        '    End If
        'End If

        Label6.Hide()
        Label7.Hide()
        Label11.Hide()

    End Sub
    Sub motorSub_DOWN()
        Label11.Show()
        Label11.Refresh()
        Label7.Text = "Objective is moving down..."
        Label7.Show()
        Label7.Refresh()
        'NOW GOING PULSES GOING DOWN:
        If CheckBox6.Checked Then
            If RadioStepsA.Checked Then
                ArduinoPin("mf" & TextBox32.Text)
            ElseIf RadioStepsB.Checked Then
                ArduinoPin("mf" & TextBox31.Text)
            End If
        Else
            If RadioStepsA.Checked Then
                ArduinoPin("mb" & TextBox32.Text)
            ElseIf RadioStepsB.Checked Then
                ArduinoPin("mb" & TextBox31.Text)
            End If
        End If

        'If DCmotors.Checked Then
        '    ArduinoPin("mb" & (CInt(TextBox31.Text) * CInt(TextBox32.Text)).ToString)

        'Else
        '    If CheckBox6.Checked Then
        '        moveStepper(0, CInt(TextBox31.Text), CInt(TextBox32.Text))
        '    Else
        '        moveStepper(1, CInt(TextBox31.Text), CInt(TextBox32.Text))
        '    End If
        'End If


        'If RadioButton1.Checked = True Then
        '    'sendtoINCUMOTOR("e")
        '    If CheckBox6.Checked Then
        '        moveStepper(0, 2, 1)
        '    Else
        '        moveStepper(1, 2, 1)
        '    End If
        'End If

        ' ''20um
        'If RadioButton2.Checked = True Then
        '    'sendtoINCUMOTOR("f")
        '    If CheckBox6.Checked Then
        '        moveStepper(0, 8, 1)
        '    Else
        '        moveStepper(1, 8, 1)
        '    End If
        'End If

        ' ''100um
        'If RadioButton12.Checked = True Then
        '    'sendtoINCUMOTOR("g")

        '    If CheckBox6.Checked Then
        '        moveStepper(0, 50, 1)
        '    Else
        '        moveStepper(1, 50, 1)
        '    End If

        'End If

        ' ''continuous
        'If RadioButton3.Checked = True Then
        '    'sendtoINCUMOTOR("h")
        '    'moveStepper(2, 0, 1)

        '    If CheckBox6.Checked Then
        '        moveStepper(0, 255, 100)
        '    Else
        '        moveStepper(1, 255, 100)
        '    End If
        'End If

        Label6.Hide()
        Label7.Hide()
        Label11.Hide()

    End Sub


    Sub motorSubforbackground_UP()
        'If up Then
        sendforbackground("d")
        'Else 'NOW GOING PULSES GOING DOWN:
        'sendforbackground("i")
        'End If
    End Sub
    Sub motorSubforbackground_DOWN()
        'If up Then
        'sendforbackground("d")
        'Else 'NOW GOING PULSES GOING DOWN:
        sendforbackground("i")
        'End If
    End Sub

    Private Sub FUp()
        FUpDCMotor()
        'FUpStepper()
    End Sub

    Sub FUpStepper()

        If RadioButton3.Checked Then  'Continuous movement

            sendtoINCUMOTOR("5")



        Else

            If RadioButton1.Checked Then   '5 microns

            End If
            If RadioButton2.Checked Then  '20 microns

            End If
            Label11.Hide()
            Label6.Hide()
            Label7.Hide()
        End If
    End Sub



    Private Sub Fdown()
        FdownDCMotor()
        'FdownStepper()
    End Sub

    Sub FdownStepper()

        If RadioButton3.Checked Then  'Continuous movement

            sendtoINCUMOTOR("6")



        Else

            If RadioButton1.Checked Then   '5 microns

            End If
            If RadioButton2.Checked Then  '20 microns

            End If
            Label11.Hide()
            Label6.Hide()
            Label7.Hide()
        End If
    End Sub




    Sub sendforbackground(ByVal str As String)
        Write2Buffer(str) 'PIC looks for "#" as delimiter
        hidWriteEx(VendorID, ProductID, BufferOut(0))

    End Sub



    Sub FUpDCMotor()
        motorisOn = True
        If RadioButton3.Checked Then  'Continuous movement
            If motorUpon = True Or motorDownon = True Then
                PinMotorUpStop()
                motorUpon = False
                motorDownon = False
                Label11.Hide()
                Label6.Hide()
                Label7.Hide()
            Else
                PinMotorUp()



                motorUpon = True
            End If
        Else
            '  motorUpon = True
            If RadioButton1.Checked Then   '5 microns
                PinMotorUpFine()
            End If
            If RadioButton2.Checked Then  '20 microns
                PinMotorUpCoarse()
            End If
            Label11.Hide()
            Label6.Hide()
            Label7.Hide()
        End If
    End Sub

    Sub FdownDCMotor()
        ' MessageBox.Show(RadioButton3.Checked)
        motorisOn = True
        If RadioButton3.Checked Then
            If motorDownon = True Or motorUpon = True Then
                'MessageBox.Show("1")
                Call PinMotorDownStop()
                motorDownon = False
                motorUpon = False
                Label11.Hide()
                Label6.Hide()
                Label7.Hide()
            Else
                'MessageBox.Show("2")

                Call PinMotorDown()
                motorDownon = True
            End If
        Else
            '   MessageBox.Show("3")

            ' motorDownon = True
            If RadioButton1.Checked Then
                Call PinMotorDownFine()
            End If
            If RadioButton2.Checked Then
                Call PinMotorDownCoarse()
            End If
            Label11.Hide()
            Label6.Hide()
            Label7.Hide()
        End If
    End Sub
    Sub AddCurrentPosDC(ByVal addtolist As Boolean) 'addtolist is true if adding value to list, false if moving stage
        getCurrent()
        'MsgBox("y")
        addDCvaluetoList = addtolist
        TimerCurrentDC.Start()
    End Sub
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click 'Add Current Position.
        buttonAddCurrent()
    End Sub

    Sub buttonAddCurrent()
        If DCmotors.Checked Then
            AddCurrentPosDC(True)
        ElseIf pololumode Then
            AddCurrentPosPololu()
        ElseIf mcl Then
            If lightisOn Then
                MsgBox("you must turn the light off for this to work on MCL stage")
                stopThread = True
                lightsOFF()
                Threading.Thread.Sleep(100)
            End If
            AddCurrentPosMcl()

        Else
            AddCurrentPos()
        End If
        savePositionstxt(directoryInfo.FullName & "\ConfigAndErrorFiles") 'saves SAVEDX.txt to Release folder. ' the same thing happens when you click Start.




        Label42.Text = ListBox2.Items.Count
        ListBox1.SelectedIndex = Label42.Text - 1
        currentListItem = Label42.Text - 1
        'Try
        '    ListBox1.SelectedIndex = Label42.Text + 1
        '    Label42.Text = ListBox1.SelectedIndex
        'Catch ex As Exception
        'End Try
        savePositionsAndConfig()
    End Sub

    Sub AddCurrentPosMcl()
        Threading.Thread.Sleep(50)
        Str = "U" & Chr(67) & Chr(13)
        SerialPort1.DiscardInBuffer()
        SerialPort1.Write(Str)
        Threading.Thread.Sleep(50)
        mytext = SerialPort1.ReadExisting
        Console.WriteLine(mytext)
        ListBox2.Items.Add(mytext)
        My.Settings.PositionsX.Add(mytext)

        Threading.Thread.Sleep(50)
        Str = "U" & Chr(68) & Chr(13)
        SerialPort1.DiscardInBuffer()
        SerialPort1.Write(Str)
        Threading.Thread.Sleep(50)
        mytext = SerialPort1.ReadExisting
        Console.WriteLine(mytext)
        ListBox3.Items.Add(mytext)
        My.Settings.PositionsY.Add(mytext)

        My.Settings.Save()
        ListBox1.Items.Clear()
        'loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
        loaddata()

    End Sub

    Dim tempXpos As String
    Dim tempYpos As String
    Dim oldXpos As String
    Dim oldYpos As String
    Dim changeXpos As String
    Dim changeYpos As String
    Sub gettempXandY()
        Try
            SerialPort1.DiscardInBuffer()
        Catch ex As Exception
        End Try
        Str = "w x"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
        End Try
        Threading.Thread.Sleep(100)
        mychar = {":", "A"}
        Try
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)

        Catch ex As Exception
            MessageBox.Show("my error765")
        End Try
        tempXpos = mytext

        Str = "w y"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
        End Try
        Threading.Thread.Sleep(100)
        mychar = {":", "A"}
        Try
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)

        Catch ex As Exception
            MessageBox.Show("my error765")
        End Try
        tempYpos = mytext


    End Sub

    Sub changeAllfollowingPositionsby(ByVal Xchange As String, ByVal Ychange As String)

        item = CInt(ListBox2.Items(currentListItem).ToString)
        'MsgBox("current x listed is " & item)
        'MsgBox("new x  is " & tempXpos)
        item2 = CInt(ListBox3.Items(currentListItem).ToString)
        'MsgBox("current y listed is " & item2)
        'MsgBox("new y  is " & tempYpos)

        For i As Integer = (currentListItem) To ListBox2.Items.Count - 1
            ListBox2.Items(i) = (CInt(ListBox2.Items(i)) + CInt(Xchange)).ToString
            My.Settings.PositionsX.Item(i) = (CInt(ListBox2.Items(i)) + CInt(Xchange)).ToString
            ListBox3.Items(i) = (CInt(ListBox3.Items(i)) + CInt(Ychange)).ToString
            My.Settings.PositionsY.Item(i) = (CInt(ListBox3.Items(i)) + CInt(Ychange)).ToString
        Next
        My.Settings.Save()

        'drawPostions()
        ListBox1.Items.Clear()
        loaddata()

    End Sub

    Sub AddCurrentPos()
        'SerialPort1.ReadExisting()
        Try
            SerialPort1.DiscardInBuffer()
        Catch ex As Exception
        End Try
        Str = "w x"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
        End Try

        Threading.Thread.Sleep(100)
        'Label29.Text = SerialPort1.ReadExisting
        mychar = {":", "A"}

        'Dim mytext As String
        Try
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)
            ListBox2.Items.Add(mytext)
            My.Settings.PositionsX.Add(mytext)
        Catch ex As Exception
            MessageBox.Show("my error345")
        End Try


        Str = "w y"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
        End Try
        Threading.Thread.Sleep(100)
        Try
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)
            ListBox3.Items.Add(mytext)
            My.Settings.PositionsY.Add(mytext)
        Catch ex As Exception
        End Try

        My.Settings.Save()
        ListBox1.Items.Clear()
        'loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
        loaddata()

        'drawPostions()
    End Sub

    Sub AddCurrentPosPololu()
        'xpos
        ListBox2.Items.Add(pololuPosX)
        My.Settings.PositionsX.Add(pololuPosX)


        'ypos
        ListBox3.Items.Add(pololuPosY)
        My.Settings.PositionsY.Add(pololuPosY)


        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()
    End Sub

    Sub loaddata()  'loads data from mysettings 
        Try
            For i As Integer = 0 To My.Settings.PositionsY.Count - 1
                ' MessageBox.Show(i)
                ' MessageBox.Show(My.Settings.PositionsX.Item(i))
                'MsgBox(My.Settings.PositionsX.Count)
                text1 = " " & i + 1 & ".(" & My.Settings.PositionsX.Item(i) & "," & My.Settings.PositionsY.Item(i) & ")"
                ListBox1.Items.Add(text1)
            Next
        Catch ex As Exception
            MessageBox.Show("No data to load..")
        End Try
        ListBox2.Items.Clear() 'X
        ListBox3.Items.Clear() 'Y
        For i As Integer = 0 To My.Settings.PositionsX.Count - 1
            ListBox2.Items.Add(My.Settings.PositionsX.Item(i))
        Next
        For i As Integer = 0 To My.Settings.PositionsY.Count - 1
            ListBox3.Items.Add(My.Settings.PositionsY.Item(i))
        Next
        drawPostions()
        savePositionstxt(sessionfolder) 'saves SAVEDX.txt to current session folder.
        savePositionstxt(directoryInfo.FullName & "\ConfigAndErrorFiles") 'saves SAVEDX.txt to Release folder.
    End Sub

    Sub loadConfigfromRelease(ByVal number As Integer)
        Try
            ' MsgBox("LOADING " & number)
            Label49.Text = number.ToString
            My.Settings.confignumber = number
            My.Settings.Save()
            Dim fileIn As New StreamReader(directoryInfo.FullName & "\ConfigAndErrorFiles\config" & number & ".txt")
            Dim strData As String = ""
            'Dim i As Long = 0

            'While (Not (fileIn.EndOfStream))
            strData = fileIn.ReadLine()


            '  Console.WriteLine(i & ": " & strData)
            '  i = i + 1
            configName = strData
            strData = fileIn.ReadLine()
            mywidth = strData
            strData = fileIn.ReadLine()
            myheigth = strData
            strData = fileIn.ReadLine()
            wellwidth = strData
            strData = fileIn.ReadLine()
            wellheight = strData

            fileIn.Close()


            TextBox17.Text = configName
            TextBox14.Text = mywidth
            TextBox13.Text = myheigth
            TextBox16.Text = wellwidth
            TextBox15.Text = wellheight



            'End While

        Catch ex As Exception

        End Try
    End Sub

    Sub loaddatafromRelease(ByVal xvalues As String, ByVal yvalues As String)



        'LOAD DATA FROM FILE SAVEDX AND SAVEDY FOUND IN RELEASE FOLDER'''''''''''''''''''''''''''''''''''''''

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        My.Settings.PositionsX.Clear()
        My.Settings.PositionsY.Clear()
        Try

            'For i As Integer = 0 To My.Settings.SavedX.Count - 1
            'text1 = " " & i + 1 & ".(" & My.Settings.SavedX.Item(i) & "," & My.Settings.SavedY.Item(i) & ")"
            'ListBox1.Items.Add(text1)
            'ListBox2.Items.Add(My.Settings.SavedX.Item(i))
            'My.Settings.PositionsX.Add(My.Settings.SavedX.Item(i))
            'ListBox3.Items.Add(My.Settings.SavedY.Item(i))
            'My.Settings.PositionsY.Add(My.Settings.SavedY.Item(i))
            'Next




            Dim fileIn As New StreamReader(directoryInfo.FullName & "\ConfigAndErrorFiles\" & xvalues)
            Dim strData As String = ""
            Dim i As Long = 0

            While (Not (fileIn.EndOfStream))
                strData = fileIn.ReadLine()

                If strData = "" Then
                Else
                    ListBox2.Items.Add(strData)
                    My.Settings.PositionsX.Add(strData)

                    Console.WriteLine(i & ": " & strData)
                    i = i + 1
                End If

            End While
            ' ListBox2.Items.RemoveAt(ListBox2.Items.Count)
            ' My.Settings.PositionsX.Remove(strData)

            Dim fileIn2 As New StreamReader(directoryInfo.FullName & "\ConfigAndErrorFiles\" & yvalues)

            i = 0

            While (Not (fileIn2.EndOfStream))
                strData = fileIn2.ReadLine()
                If strData = "" Then
                Else


                    ListBox3.Items.Add(strData)
                    My.Settings.PositionsY.Add(strData)

                    Console.WriteLine(i & ": " & strData)
                    i = i + 1
                End If
            End While
            ' ListBox3.Items.RemoveAt(ListBox3.Items.Count)
            ' My.Settings.PositionsY.Remove(strData)



            For j As Integer = 0 To ListBox2.Items.Count - 1
                text1 = " " & j + 1 & ".(" & ListBox2.Items.Item(j) & "," & ListBox3.Items.Item(j) & ")"
                ListBox1.Items.Add(text1)
            Next




        Catch ex As Exception
            MessageBox.Show("No saved data..")
        End Try
        My.Settings.Save()

        drawPostions()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)  'Go to selected Pos

    End Sub
    Sub gotoselectedposDC()
        'MsgBox("y")
        item = ListBox2.Items(ListBox1.SelectedIndex()).ToString
        item2 = ListBox3.Items(ListBox1.SelectedIndex()).ToString
        ArduinoPin("x" & item & "y" & item2)
    End Sub

    Sub gotoselectedposMcl()
        item = ListBox2.Items(ListBox1.SelectedIndex()).ToString
        item2 = ListBox3.Items(ListBox1.SelectedIndex()).ToString
        Str = "U" & Chr(7) & "r" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(0) & item & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(1) & item2 & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(80) & Chr(13)
        SerialPort1.Write(Str)
    End Sub

    Sub gotoselectedpos()
        gotoselectedposWithoutTimer()
        Timer5.Start()
    End Sub

    Sub gotoselectedposWithoutTimer()

        SetStageSpeeds()

        Label7.Text = "Moving.. Please wait.."
        Label7.Show()
        'Refresh()

        'slowStage()
        'Thread.Sleep(50)
        Str = "MOVE X=" 'this is press of right arrow (movement in X !!!)
        'str += distance * FormPos.ListBox1.Items(currentListItem).ToString
        'Dim dist As Integer = CInt(distance)
        item = CInt(ListBox2.Items(ListBox1.SelectedIndex()).ToString)
        str2 = item
        Str += str2
        str3 = " Y="  'this is press of up arrow (movement in Y !!!)
        Str += str3
        item2 = CInt(ListBox3.Items(ListBox1.SelectedIndex()).ToString)
        str4 = item2
        Str += str4
        Str += ControlChars.Cr
        SerialPort1.Write(Str)

        'QueryFinished = New System.Threading.Thread(AddressOf QueryFinishedMovement)
        'QueryFinished.Start()
        'QueryFinishedMovement()
        ' Timer5.Start()



    End Sub

    Sub moveTo(ByVal x As Integer, ByVal y As Integer)
        SetStageSpeeds()

        Label7.Text = "Moving.. Please wait.."
        Label7.Show()
        'Refresh()

        'slowStage()
        'Thread.Sleep(50)
        Str = "MOVE X=" 'this is press of right arrow (movement in X !!!)
        'str += distance * FormPos.ListBox1.Items(currentListItem).ToString
        'Dim dist As Integer = CInt(distance)
        item = x 'CInt(ListBox2.Items(ListBox1.SelectedIndex()).ToString)
        str2 = item
        Str += str2
        str3 = " Y="  'this is press of up arrow (movement in Y !!!)
        Str += str3
        item2 = y 'CInt(ListBox3.Items(ListBox1.SelectedIndex()).ToString)
        str4 = item2
        Str += str4
        Str += ControlChars.Cr
        SerialPort1.Write(Str)

    End Sub


    Sub gotoselectedposPololu()
        'x pos
        item = CInt(ListBox2.Items(ListBox1.SelectedIndex()).ToString)

        If (item - pololuPosX) > 0 Then
            'move up
            movePololu(0, pololusteps, item - pololuPosX, 1)
        Else
            'move down
            movePololu(1, pololusteps, pololuPosX - item, 1)
        End If
        pololuPosX = pololuPosX + item - pololuPosX
        Label36.Text = pololuPosX

        'y pos
        item = CInt(ListBox3.Items(ListBox1.SelectedIndex()).ToString)

        If (item - pololuPosY) > 0 Then
            'move right
            movePololu(0, pololustepsY, item - pololuPosY, 0)
        Else
            'move left
            movePololu(1, pololustepsY, pololuPosY - item, 0)
        End If
        pololuPosY = pololuPosY + item - pololuPosY
        Label37.Text = pololuPosY



    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click  'Go Home.

        If pololumode Then
            GotoPos1Pololu()
        ElseIf mcl Then
            GotoPos1Mcl()
        Else

            GotoPos1()
        End If

        'If lightisOn = False Then
        '    GrabImage()
        'End If
        Panel3.Hide()

    End Sub

    Sub GotoPos1()
        addtomyConsole("going to pos1 now")
        'SetStageSpeeds()
        'Str = "M X=0 Y=0"
        'Str += ControlChars.Cr
        'Try
        '    SerialPort1.Write(Str)
        'Catch ex As Exception
        'End Try
        'Label7.Text = "Moving to Pos 1, please wait.."
        'Label7.Show()
        'Timer5.Start()


        'MOVE TO POSITION 1:

        Try


            SetStageSpeeds()
            Label7.Text = "Moving.. Please wait.."
            Label7.Show()
            Str = "MOVE X=" 'this is press of right arrow (movement in X !!!)
            ' item = CInt(ListBox2.Items(ListBox1.SelectedIndex()).ToString)
            item = CInt(ListBox2.Items.Item(0).ToString)

            str2 = item
            Str += str2
            str3 = " Y="  'this is press of up arrow (movement in Y !!!)
            Str += str3
            'item2 = CInt(ListBox3.Items(ListBox1.SelectedIndex()).ToString)
            item2 = CInt(ListBox3.Items.Item(0).ToString)
            str4 = item2
            Str += str4
            Str += ControlChars.Cr
            SerialPort1.Write(Str)

            QueryFinishedMovement()
            'Timer5.Start()
        Catch ex As Exception
            MessageBox.Show("First add a Pos1")
        End Try

    End Sub
    Sub GotoPos1Mcl()
        item = CInt(ListBox2.Items.Item(0).ToString)
        item2 = CInt(ListBox3.Items.Item(0).ToString)
        Str = "U" & Chr(7) & "r" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(0) & item & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(1) & item2 & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(80) & Chr(13)
        SerialPort1.Write(Str)
        'Threading.Thread.Sleep(4000)
        QueryFinishedMovementThreadMCL()
        WaitEvent.WaitOne()
        Console.WriteLine("finished waiting!")
        finishedMoving = True
        startCountDown = True
        timer5tick()
    End Sub
    Sub GotoPos1Pololu()
        Console.WriteLine("GotoPos1Pololu()")

        'x pos
        item = CInt(ListBox2.Items(0).ToString)

        If item >= 0 And pololuPosX >= 0 Then
            If (item - pololuPosX) > 0 Then
                'move up
                movePololu(0, pololusteps, item - pololuPosX, 1)
            Else
                If item = 0 And pololuPosX = 0 Then
                Else
                    'move down
                    movePololu(1, pololusteps, pololuPosX - item, 1)
                End If

            End If
        End If

        If item <= 0 And pololuPosX <= 0 Then
            If (item - pololuPosX) < 0 Then
                'move down
                movePololu(1, pololusteps, pololuPosX - item, 1)
            Else
                If item = 0 And pololuPosX = 0 Then
                Else
                    'move up
                    movePololu(0, pololusteps, item - pololuPosX, 1)
                End If
            End If
        End If
        If item > 0 And pololuPosX < 0 Then
            'move up
            movePololu(0, pololusteps, item - pololuPosX, 1)
        End If
        If item < 0 And pololuPosX > 0 Then
            'move down
            movePololu(1, pololusteps, pololuPosX - item, 1)
        End If



        'y pos
        item2 = CInt(ListBox3.Items(0).ToString)

        If item2 >= 0 And pololuPosY >= 0 Then
            If (item2 - pololuPosY) > 0 Then
                'move right
                movePololu(0, pololustepsY, item2 - pololuPosY, 0)
            Else
                If item2 = 0 And pololuPosY = 0 Then

                Else
                    'move left
                    movePololu(1, pololustepsY, pololuPosY - item2, 0)
                End If
            End If
        End If
        If item2 <= 0 And pololuPosY <= 0 Then
            If (item2 - pololuPosY) < 0 Then
                'move left
                movePololu(1, pololustepsY, pololuPosY - item2, 0)
            Else
                If item2 = 0 And pololuPosY = 0 Then
                Else
                    'move right
                    movePololu(0, pololustepsY, item2 - pololuPosY, 0)
                End If
            End If
        End If
        If item2 > 0 And pololuPosY < 0 Then
            'move right
            movePololu(0, pololustepsY, item2 - pololuPosY, 0)
        End If
        If item2 < 0 And pololuPosY > 0 Then
            'move left
            movePololu(1, pololustepsY, pololuPosY - item2, 0)
        End If



        'Timer5.Start()
        Console.WriteLine("waiting for for movement to end  " & (pololusteps * (Math.Abs(pololuPosY - item2) + Math.Abs(pololuPosX - item)) * 6 / 1000).ToString & " seconds....")
        Threading.Thread.Sleep(pololusteps * (Math.Abs(pololuPosY - item2) + Math.Abs(pololuPosX - item)) * 6)
        'Threading.Thread.Sleep(1000)
        Console.WriteLine("finished waiting!")

        pololuPosX = pololuPosX + item - pololuPosX
        Label36.Text = pololuPosX

        pololuPosY = pololuPosY + item2 - pololuPosY
        Label37.Text = pololuPosY


        timer5tick()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click  'Delete selected position.
        Try
            My.Settings.PositionsX.RemoveAt(ListBox1.SelectedIndex())
            My.Settings.PositionsY.RemoveAt(ListBox1.SelectedIndex())
            ListBox2.Items.RemoveAt(ListBox1.SelectedIndex())
            ListBox3.Items.RemoveAt(ListBox1.SelectedIndex())
            My.Settings.Save()
            ListBox1.Items.Clear()
            'loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
            loaddata()
        Catch ex As Exception
            MessageBox.Show("Can't load data")
        End Try

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click  'Delete All positions

        Dim result As Integer = MessageBox.Show("Are you sure you want to delete all the positions? ", "Delete All Positions?", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            deleteAll()
        ElseIf result = DialogResult.No Then
        End If


    End Sub
    Sub deleteAll()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        My.Settings.PositionsX.Clear()
        My.Settings.PositionsY.Clear()
        'ListBox2.Items.Add(0)
        'ListBox3.Items.Add(0)
        ' My.Settings.PositionsX.Add(0)
        ' My.Settings.PositionsY.Add(0)
        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()
        PictureBox2.Image = Nothing
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click  'Set home.
        Dim result As Integer = MessageBox.Show("Are you sure you want to Zero here? ", "Zero here?", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Zero()
        ElseIf result = DialogResult.No Then
        End If
            
        



    End Sub



    Sub Zero()
        Label42.Text = "1"
        If DCmotors.Checked Then
            ArduinoPin("z")
        ElseIf My.Settings.mcl = True Then
            setXYzero()
        ElseIf pololumode Then
            ZeroPololu()
        Else
            Str = "ZERO"
            Str += ControlChars.Cr
            Try
                SerialPort1.Write(Str)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Sub ZeroPololu()
        pololuPosX = 0
        pololuPosY = 0
        Label36.Text = 0
        Label37.Text = 0
    End Sub




    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click  '3by3 button
        TextBox7.Text = 3
        If mcl Then
            stopThread = True
            lightsOFF()
            addnynMcl()
        ElseIf pololumode = True Then
            addnynPololu()

        ElseIf pololumode = False Then
            addnbynbutton()
        End If




        'AddCurrentPos()
        ''If versionMS2000 Then
        ''    mywidth = widthMS2000
        ''    myheigth = heigthMS2000
        ''Else
        ''    mywidth = widthMS4
        ''    myheigth = heigthMS4
        ''End If

        'Try
        '    SerialPort1.DiscardInBuffer()
        'Catch ex As Exception
        'End Try
        'Str = "w x"
        'Str += ControlChars.Cr
        'Try
        '    SerialPort1.Write(Str)
        'Catch ex As Exception
        'End Try

        'Threading.Thread.Sleep(100)
        ''Label29.Text = SerialPort1.ReadExisting
        'mychar = {":", "A"}

        ''Dim mytext As String
        'Try
        '    Xpos = SerialPort1.ReadExisting
        '    Xpos = Xpos.TrimStart(mychar)

        '    'ListBox2.Items.Add(Xpos)
        '    '    > x -ve  ;   \/ y +ve  ;  mywidth and mywidth are both +ve for MS2000.  
        '    'Dim widthMS2000 As Integer = 950 '925 '900 '950 '-1000 '8182
        '    'Dim heigthMS2000 As Integer = 710  '-750  '6124
        '    ListBox2.Items.Add(Xpos - mywidth)
        '    ListBox2.Items.Add(Xpos - (2 * mywidth))
        '    ListBox2.Items.Add((CInt(Xpos) + 0).ToString - (2 * mywidth))   'adding to x means moving the image to the right.
        '    ListBox2.Items.Add((CInt(Xpos) + 0).ToString - mywidth)
        '    ListBox2.Items.Add((CInt(Xpos) + 0).ToString)
        '    ListBox2.Items.Add((CInt(Xpos) + 0).ToString)
        '    ListBox2.Items.Add((CInt(Xpos) + 0).ToString - mywidth)
        '    ListBox2.Items.Add((CInt(Xpos) + 0).ToString - (2 * mywidth))
        '    '  My.Settings.PositionsX.Add(Xpos)
        '    My.Settings.PositionsX.Add(Xpos - mywidth)
        '    My.Settings.PositionsX.Add(Xpos - (2 * mywidth))
        '    My.Settings.PositionsX.Add((CInt(Xpos) + 0).ToString - (2 * mywidth))
        '    My.Settings.PositionsX.Add((CInt(Xpos) + 0).ToString - mywidth)
        '    My.Settings.PositionsX.Add((CInt(Xpos) + 0).ToString)
        '    My.Settings.PositionsX.Add((CInt(Xpos) + 0).ToString)
        '    My.Settings.PositionsX.Add((CInt(Xpos) + 0).ToString - mywidth)
        '    My.Settings.PositionsX.Add((CInt(Xpos) + 0).ToString - (2 * mywidth))
        'Catch ex As Exception

        '    'Try
        '    '    Xpos = SerialPort1.ReadExisting
        '    '    Xpos = Xpos.TrimStart(mychar)
        '    '    'ListBox2.Items.Add(Xpos)
        '    '    '    > x -ve  ;   \/ y +ve  ;  mywidth and mywidth are both +ve for MS2000.  
        '    '    'Dim widthMS2000 As Integer = 950 '925 '900 '950 '-1000 '8182
        '    '    'Dim heigthMS2000 As Integer = 710  '-750  '6124
        '    '    ListBox2.Items.Add(Xpos - mywidth)
        '    '    ListBox2.Items.Add(Xpos - (2 * mywidth))
        '    '    ListBox2.Items.Add((CInt(Xpos) + 30).ToString - (2 * mywidth))   'adding to x means moving the image to the right.
        '    '    ListBox2.Items.Add((CInt(Xpos) + 30).ToString - mywidth)
        '    '    ListBox2.Items.Add((CInt(Xpos) + 30).ToString)
        '    '    ListBox2.Items.Add((CInt(Xpos) + 60).ToString)
        '    '    ListBox2.Items.Add((CInt(Xpos) + 60).ToString - mywidth)
        '    '    ListBox2.Items.Add((CInt(Xpos) + 60).ToString - (2 * mywidth))
        '    '    '  My.Settings.PositionsX.Add(Xpos)
        '    '    My.Settings.PositionsX.Add(Xpos - mywidth)
        '    '    My.Settings.PositionsX.Add(Xpos - (2 * mywidth))
        '    '    My.Settings.PositionsX.Add((CInt(Xpos) + 30).ToString - (2 * mywidth))
        '    '    My.Settings.PositionsX.Add((CInt(Xpos) + 30).ToString - mywidth)
        '    '    My.Settings.PositionsX.Add((CInt(Xpos) + 30).ToString)
        '    '    My.Settings.PositionsX.Add((CInt(Xpos) + 60).ToString)
        '    '    My.Settings.PositionsX.Add((CInt(Xpos) + 60).ToString - mywidth)
        '    '    My.Settings.PositionsX.Add((CInt(Xpos) + 60).ToString - (2 * mywidth))
        '    'Catch ex As Exception
        'End Try
        'Str = "w y"
        'Str += ControlChars.Cr
        'Try
        '    SerialPort1.Write(Str)
        'Catch ex As Exception
        'End Try
        'Threading.Thread.Sleep(100)
        'Try
        '    Ypos = SerialPort1.ReadExisting
        '    Ypos = mytext.TrimStart(mychar)
        '    ' ListBox3.Items.Add(Ypos)
        '    '    > x -ve  ;   \/ y +ve  ;  mywidth and mywidth are both +ve for MS2000.
        '    'Dim widthMS2000 As Integer = 950 '925 '900 '950 '-1000 '8182
        '    'Dim heigthMS2000 As Integer = 710  '-750  '6124
        '    ListBox3.Items.Add((CInt(Ypos) + 0).ToString) 'ListBox3.Items.Add(Ypos)     'increaseing y means moving image to the top.
        '    ListBox3.Items.Add((CInt(Ypos) + 0).ToString) 'ListBox3.Items.Add(Ypos)
        '    ListBox3.Items.Add((CInt(Ypos) + 0).ToString + myheigth)   'ListBox3.Items.Add(Ypos + myheigth)
        '    ListBox3.Items.Add((CInt(Ypos) + 0).ToString + myheigth) 'ListBox3.Items.Add((CInt(Ypos) + 50).ToString + myheigth)  'ListBox3.Items.Add(Ypos + myheigth)
        '    ListBox3.Items.Add(Ypos + myheigth)
        '    ListBox3.Items.Add((CInt(Ypos) + 0).ToString + (2 * myheigth))
        '    ListBox3.Items.Add((CInt(Ypos) + 0).ToString + (2 * myheigth))
        '    ListBox3.Items.Add((CInt(Ypos) + 0).ToString + (2 * myheigth))

        '    'My.Settings.PositionsY.Add(Ypos)
        '    My.Settings.PositionsY.Add((CInt(Ypos) + 0).ToString)
        '    My.Settings.PositionsY.Add((CInt(Ypos) + 0).ToString) 'My.Settings.PositionsY.Add(Ypos)
        '    My.Settings.PositionsY.Add((CInt(Ypos) + 0).ToString + myheigth)   'ListBox3.Items.Add(Ypos + myheigth)
        '    My.Settings.PositionsY.Add((CInt(Ypos) + 0).ToString + myheigth)   'ListBox3.Items.Add(Ypos + myheigth)
        '    My.Settings.PositionsY.Add(Ypos + myheigth)
        '    My.Settings.PositionsY.Add((CInt(Ypos) + 0).ToString + (2 * myheigth))
        '    My.Settings.PositionsY.Add((CInt(Ypos) + 0).ToString + (2 * myheigth))
        '    My.Settings.PositionsY.Add((CInt(Ypos) + 0).ToString + (2 * myheigth))

        'Catch ex As Exception

        'End Try

        ''Try
        ''    Ypos = SerialPort1.ReadExisting
        ''    Ypos = mytext.TrimStart(mychar)
        ''    ' ListBox3.Items.Add(Ypos)
        ''    '    > x -ve  ;   \/ y +ve  ;  mywidth and mywidth are both +ve for MS2000.
        ''    'Dim widthMS2000 As Integer = 950 '925 '900 '950 '-1000 '8182
        ''    'Dim heigthMS2000 As Integer = 710  '-750  '6124
        ''    ListBox3.Items.Add((CInt(Ypos) + 40).ToString) 'ListBox3.Items.Add(Ypos)     'increaseing y means moving image to the top.
        ''    ListBox3.Items.Add((CInt(Ypos) + 80).ToString) 'ListBox3.Items.Add(Ypos)
        ''    ListBox3.Items.Add((CInt(Ypos) + 80).ToString + myheigth)   'ListBox3.Items.Add(Ypos + myheigth)
        ''    ListBox3.Items.Add((CInt(Ypos) + 40).ToString + myheigth) 'ListBox3.Items.Add((CInt(Ypos) + 50).ToString + myheigth)  'ListBox3.Items.Add(Ypos + myheigth)
        ''    ListBox3.Items.Add(Ypos + myheigth)
        ''    ListBox3.Items.Add((CInt(Ypos) + 0).ToString + (2 * myheigth))
        ''    ListBox3.Items.Add((CInt(Ypos) + 40).ToString + (2 * myheigth))
        ''    ListBox3.Items.Add((CInt(Ypos) + 80).ToString + (2 * myheigth))

        ''    'My.Settings.PositionsY.Add(Ypos)
        ''    My.Settings.PositionsY.Add((CInt(Ypos) + 40).ToString)
        ''    My.Settings.PositionsY.Add((CInt(Ypos) + 80).ToString) 'My.Settings.PositionsY.Add(Ypos)
        ''    My.Settings.PositionsY.Add((CInt(Ypos) + 80).ToString + myheigth)   'ListBox3.Items.Add(Ypos + myheigth)
        ''    My.Settings.PositionsY.Add((CInt(Ypos) + 50).ToString + myheigth)   'ListBox3.Items.Add(Ypos + myheigth)
        ''    My.Settings.PositionsY.Add(Ypos + myheigth)
        ''    My.Settings.PositionsY.Add((CInt(Ypos) + 0).ToString + (2 * myheigth))
        ''    My.Settings.PositionsY.Add((CInt(Ypos) + 40).ToString + (2 * myheigth))
        ''    My.Settings.PositionsY.Add((CInt(Ypos) + 80).ToString + (2 * myheigth))

        ''Catch ex As Exception

        ''End Try


        'My.Settings.Save()
        'ListBox1.Items.Clear()
        'loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")



    End Sub



    'Sub add2by2()
    '    AddCurrentPos()
    '    'ListBox1.Items.Clear()
    '    'ListBox2.Items.Clear()
    '    'ListBox3.Items.Clear()
    '    'My.Settings.PositionsX.Clear()
    '    'My.Settings.PositionsY.Clear()
    '    'If versionMS2000 Then
    '    '    mywidth = widthMS2000
    '    '    myheigth = heigthMS2000
    '    'Else
    '    '    mywidth = widthMS4
    '    '    myheigth = heigthMS4
    '    'End If

    '    Try
    '        SerialPort1.DiscardInBuffer()

    '    Catch ex As Exception

    '    End Try
    '    Str = "w x"
    '    Str += ControlChars.Cr
    '    Try
    '        SerialPort1.Write(Str)

    '    Catch ex As Exception

    '    End Try

    '    Threading.Thread.Sleep(100)
    '    'Label29.Text = SerialPort1.ReadExisting
    '    mychar = {":", "A"}

    '    'Dim mytext As String

    '    '    > x -ve  ;   \/ y +ve  ;  mywidth and mywidth are both +ve for MS2000.
    '    Try
    '        Xpos = SerialPort1.ReadExisting
    '        Xpos = Xpos.TrimStart(mychar)
    '        'ListBox2.Items.Add(Xpos)
    '        ListBox2.Items.Add(Xpos - mywidth)
    '        ListBox2.Items.Add((CInt(Xpos) + 0).ToString - mywidth) '(Xpos - mywidth)
    '        ListBox2.Items.Add((CInt(Xpos) + 0).ToString)

    '        '  My.Settings.PositionsX.Add(Xpos)
    '        My.Settings.PositionsX.Add(Xpos - mywidth)
    '        My.Settings.PositionsX.Add((CInt(Xpos) + 0).ToString - mywidth) '(Xpos - mywidth)
    '        My.Settings.PositionsX.Add((CInt(Xpos) + 0).ToString)
    '    Catch ex As Exception

    '    End Try

    '    'Try
    '    '    Xpos = SerialPort1.ReadExisting
    '    '    Xpos = Xpos.TrimStart(mychar)
    '    '    'ListBox2.Items.Add(Xpos)
    '    '    ListBox2.Items.Add(Xpos - mywidth)
    '    '    ListBox2.Items.Add((CInt(Xpos) + 40).ToString - mywidth) '(Xpos - mywidth)
    '    '    ListBox2.Items.Add((CInt(Xpos) + 30).ToString)

    '    '    '  My.Settings.PositionsX.Add(Xpos)
    '    '    My.Settings.PositionsX.Add(Xpos - mywidth)
    '    '    My.Settings.PositionsX.Add((CInt(Xpos) + 40).ToString - mywidth) '(Xpos - mywidth)
    '    '    My.Settings.PositionsX.Add((CInt(Xpos) + 30).ToString)
    '    'Catch ex As Exception

    '    'End Try





    '    Str = "w y"
    '    Str += ControlChars.Cr
    '    Try
    '        SerialPort1.Write(Str)

    '    Catch ex As Exception

    '    End Try

    '    Threading.Thread.Sleep(100)
    '    Try
    '        Ypos = SerialPort1.ReadExisting
    '        Ypos = mytext.TrimStart(mychar)
    '        ' ListBox3.Items.Add(Ypos)
    '        ListBox3.Items.Add((CInt(Ypos) + 0).ToString)
    '        ListBox3.Items.Add((CInt(Ypos) + 0).ToString + myheigth)
    '        ListBox3.Items.Add(Ypos + myheigth)
    '        ' My.Settings.PositionsY.Add(Ypos)
    '        My.Settings.PositionsY.Add((CInt(Ypos) + 0).ToString)
    '        My.Settings.PositionsY.Add((CInt(Ypos) + 0).ToString + myheigth)
    '        My.Settings.PositionsY.Add(Ypos + myheigth)
    '    Catch ex As Exception

    '    End Try

    '    'Try
    '    '    Ypos = SerialPort1.ReadExisting
    '    '    Ypos = mytext.TrimStart(mychar)
    '    '    ' ListBox3.Items.Add(Ypos)
    '    '    ListBox3.Items.Add((CInt(Ypos) + 40).ToString)
    '    '    ListBox3.Items.Add((CInt(Ypos) + 40).ToString + myheigth)
    '    '    ListBox3.Items.Add(Ypos + myheigth)
    '    '    ' My.Settings.PositionsY.Add(Ypos)
    '    '    My.Settings.PositionsY.Add((CInt(Ypos) + 40).ToString)
    '    '    My.Settings.PositionsY.Add((CInt(Ypos) + 40).ToString + myheigth)
    '    '    My.Settings.PositionsY.Add(Ypos + myheigth)
    '    'Catch ex As Exception

    '    'End Try


    '    My.Settings.Save()
    '    ListBox1.Items.Clear()
    '    loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")


    'End Sub

    'Sub add2by2Pololu()
    '    AddCurrentPosPololu()

    '    Try
    '        ListBox2.Items.Add((CInt(pololuPosX) + 0).ToString)
    '        ListBox2.Items.Add((CInt(pololuPosX) + 0).ToString - 1)
    '        ListBox2.Items.Add((CInt(pololuPosX) + 0).ToString - 1) '(Xpos - mywidth)

    '        '  My.Settings.PositionsX.Add(Xpos)
    '        My.Settings.PositionsX.Add((CInt(pololuPosX) + 0).ToString)
    '        My.Settings.PositionsX.Add((CInt(pololuPosX) + 0).ToString - 1)
    '        My.Settings.PositionsX.Add((CInt(pololuPosX) + 0).ToString - 1) '(Xpos - mywidth)

    '    Catch ex As Exception

    '    End Try

    '    Try
    '        ListBox3.Items.Add((CInt(pololuPosY) + 0).ToString + 1)
    '        ListBox3.Items.Add((CInt(pololuPosY) + 0).ToString + 1)
    '        ListBox3.Items.Add((CInt(pololuPosY) + 0).ToString)

    '        ' My.Settings.PositionsY.Add(Ypos)
    '        My.Settings.PositionsY.Add((CInt(pololuPosY) + 0).ToString + 1)
    '        My.Settings.PositionsY.Add((CInt(pololuPosY) + 0).ToString + 1)
    '        My.Settings.PositionsY.Add((CInt(pololuPosY) + 0).ToString)

    '    Catch ex As Exception

    '    End Try

    '    My.Settings.Save()
    '    ListBox1.Items.Clear()
    '    loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")


    'End Sub







    Private Sub Button84_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown, Button84.KeyDown
        Select Case e.KeyCode
            Case Keys.W
                Button20.PerformClick()
            Case Keys.S
                Button22.PerformClick()
            Case Keys.A
                Button23.PerformClick()
            Case Keys.D
                Button21.PerformClick()

        End Select
    End Sub


    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click 'MUp button
        If CheckBox9.Checked Then
            Dim result = MessageBox.Show("Careful!!! You are moving a big distance to the next well!", "caption", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Str = ""
                Exit Sub
            End If


            If SwitchyCheckbox.Checked Then
                MUp((0 - CInt(wellheight)).ToString)
            Else
                MUp(wellheight.ToString)
            End If
        Else

            If SwitchyCheckbox.Checked Then
                MUp((0 - CInt(myheigth)).ToString)
            Else
                MUp(myheigth.ToString)
            End If

        End If
    End Sub

    Sub MUp(ByVal Amount As String)

        If pololumode = True Then

            movePololu(0, pololusteps, pololurepetitions, 1)
            pololuPosX = pololuPosX + pololurepetitions
            Label36.Text = pololuPosX

        ElseIf pololumode = False Then

            If mcl Then

                item = Amount
                item2 = 0

                Str = "U" & Chr(7) & "v" & Chr(13)  'v is for relative, r is for absolute.
                SerialPort1.Write(Str)
                Str = "U" & Chr(0) & item2 & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(1) & -item & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(80) & Chr(13)
                SerialPort1.Write(Str)
                QueryFinishedMovementThreadMCL()
                WaitEvent.WaitOne()
            ElseIf DCmotors.Checked Then
                item = 0
                item2 = Amount
                AddCurrentPosDC(False)
            Else

                SetStageSpeeds()
                If versionMS2000 Then
                    Str = "R Y="
                Else
                    Str = "RM Y="
                End If
                ' MsgBox("Amount " & Amount)
                'MsgBox("CInt(Amount) " & CInt(Amount))
                If CInt(Amount) < 0 Then
                    ' MsgBox("CInt(Amount) is less than zero so.. ")
                    Amount = (0 - CInt(Amount)).ToString
                    ' MsgBox("Amount " & Amount)
                ElseIf CInt(Amount) > 0 Then
                    Amount = (0 - CInt(Amount)).ToString

                End If

                Str += Amount
                Str += ControlChars.Cr
                SerialPort1.DiscardInBuffer()
                SerialPort1.Write(Str)
                addtomyConsole(Str)
                If versionMS2000 Then
                    QueryFinishedMovementThreadMS2000() 'this starts a new thread that waits for the stage to respond when it has finished moving.
                Else
                    QueryFinishedMovementThread() 'this starts a new thread that waits for the stage to respond when it has finished moving.
                End If
                WaitEvent.WaitOne()


            End If

        End If


    End Sub
    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click   'MDown button
        If CheckBox9.Checked Then
            Dim result = MessageBox.Show("Careful!!! You are moving a big distance to the next well!", "caption", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Str = ""
                Exit Sub
            End If


            If SwitchyCheckbox.Checked Then
                MDown((0 - CInt(wellheight)).ToString)
            Else
                MDown(wellheight.ToString)
            End If
        Else

            If SwitchyCheckbox.Checked Then
                MDown((0 - CInt(myheigth)).ToString)
            Else
                MDown(myheigth.ToString)
            End If

        End If
    End Sub

    Sub MDown(ByVal Amount As String)
        If pololumode = True Then
            movePololu(1, pololusteps, pololurepetitions, 1)
            pololuPosX = pololuPosX - pololurepetitions
            Label36.Text = pololuPosX

        ElseIf pololumode = False Then
            If mcl Then
                item = Amount
                item2 = 0
                Console.WriteLine("moving one well down..")
                Str = "U" & Chr(7) & "v" & Chr(13)  'v is for relative, r is for absolute.
                SerialPort1.Write(Str)
                Str = "U" & Chr(0) & item2 & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(1) & item & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(80) & Chr(13)
                SerialPort1.Write(Str)
                QueryFinishedMovementThreadMCL()
                WaitEvent.WaitOne()
            ElseIf DCmotors.Checked Then
                item = 0
                item2 = Amount
                item2 = -item2
                AddCurrentPosDC(False)

            Else 'if not pololu or mcl

                SetStageSpeeds()
                If versionMS2000 Then
                    Str = "R Y="
                Else
                    Str = "RM Y="
                End If
                Str += Amount
                Str += ControlChars.Cr
                SerialPort1.DiscardInBuffer()
                SerialPort1.Write(Str)
                addtomyConsole(Str)
                If versionMS2000 Then
                    QueryFinishedMovementThreadMS2000() 'this starts a new thread that waits for the stage to respond when it has finished moving.
                Else
                    QueryFinishedMovementThread() 'this starts a new thread that waits for the stage to respond when it has finished moving.
                End If
                WaitEvent.WaitOne()
            End If
        End If

    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click  'MLeft button
        If CheckBox9.Checked Then
            Dim result = MessageBox.Show("Careful!!! You are moving a big distance to the next well!", "caption", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Str = ""
                Exit Sub
            End If


            If SwitchxCheckbox.Checked Then
                MLeft((0 - CInt(wellwidth)).ToString)
            Else
                MLeft(wellwidth.ToString)
            End If
        Else

            If SwitchxCheckbox.Checked Then
                MLeft((0 - CInt(mywidth)).ToString)
            Else
                MLeft(mywidth.ToString)
            End If

        End If
    End Sub
    Sub MLeft(ByVal Amount As String)

        If pololumode = True Then
            'Console.WriteLine("pololustepsY " & pololustepsY & "pololurepetitions " & pololurepetitions)
            movePololu(1, pololustepsY, pololurepetitions, 0)
            pololuPosY = pololuPosY - pololurepetitions
            Label37.Text = pololuPosY
        ElseIf pololumode = False Then

            If mcl Then
                item = 0
                item2 = Amount
                Str = "U" & Chr(7) & "v" & Chr(13)  'v is for relative, r is for absolute.
                SerialPort1.Write(Str)
                Str = "U" & Chr(0) & item2 & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(1) & item & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(80) & Chr(13)
                SerialPort1.Write(Str)
                QueryFinishedMovementThreadMCL()
                WaitEvent.WaitOne()
            ElseIf DCmotors.Checked Then
                item = Amount
                item = -item
                item2 = 0
                AddCurrentPosDC(False)
            Else
                If versionMS2000 Then
                    Str = "R X="
                Else
                    Str = "RM X="
                End If
                Str += Amount
                Str += ControlChars.Cr
                SerialPort1.DiscardInBuffer()
                SerialPort1.Write(Str)
                addtomyConsole(Str)
                If versionMS2000 Then
                    QueryFinishedMovementThreadMS2000() 'this starts a new thread that waits for the stage to respond when it has finished moving.
                Else
                    QueryFinishedMovementThread() 'this starts a new thread that waits for the stage to respond when it has finished moving.
                End If
                WaitEvent.WaitOne()
            End If
        End If
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click  'MRight button

        If CheckBox9.Checked Then
            Dim result = MessageBox.Show("Careful!!! You are moving a big distance to the next well!", "caption", MessageBoxButtons.OKCancel)
            If result = DialogResult.Cancel Then
                Str = ""
                Exit Sub
            End If
            If SwitchxCheckbox.Checked Then
                MRight((0 - CInt(wellwidth)).ToString)
            Else
                MRight(wellwidth.ToString)
            End If

        Else
            If SwitchxCheckbox.Checked Then
                MRight((0 - CInt(mywidth)).ToString)
            Else
                MRight(mywidth.ToString)
            End If

        End If
    End Sub
    Sub MRight(ByVal Amount As String)

        If pololumode = True Then
            '            Console.WriteLine("pololustepsY " & pololustepsY & "pololurepetitions " & pololurepetitions)
            movePololu(0, pololustepsY, pololurepetitions, 0)
            'MsgBox("pololustepsY =" & pololustepsY & "pololurepetitions =" & pololurepetitions)
            pololuPosY = pololuPosY + pololurepetitions
            Label37.Text = pololuPosY

        ElseIf pololumode = False Then
            If mcl Then
                item = 0
                item2 = Amount
                Str = "U" & Chr(7) & "v" & Chr(13)  'v is for relative, r is for absolute.
                SerialPort1.Write(Str)
                Str = "U" & Chr(0) & -item2 & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(1) & item & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(80) & Chr(13)
                SerialPort1.Write(Str)
                QueryFinishedMovementThreadMCL()
                WaitEvent.WaitOne()
            ElseIf DCmotors.Checked Then
                item = Amount
                item2 = 0
                AddCurrentPosDC(False)
            Else 'if not pololu or mcl
                If versionMS2000 Then
                    Str = "R X=-"
                Else
                    Str = "RM X=-"
                    'MessageBox.Show("ms4")
                End If
                Str += Amount
                Str += ControlChars.Cr
                SerialPort1.DiscardInBuffer()
                SerialPort1.Write(Str)
                addtomyConsole((Str))

                If versionMS2000 Then
                    QueryFinishedMovementThreadMS2000() 'this starts a new thread that waits for the stage to respond when it has finished moving.
                Else
                    QueryFinishedMovementThread() 'this starts a new thread that waits for the stage to respond when it has finished moving.
                End If

                WaitEvent.WaitOne()

            End If
        End If
    End Sub

    Sub SetStageSpeeds()  ' I commented it out because I think it's wrong.
        'Try
        '    If versionMS2000 Then
        '        Str = speedRight
        '    Else
        '        Str = speedLeft '500
        '    End If

        '    Str += ControlChars.Cr
        '    SerialPort1.Write(Str)
        'Catch ex As Exception

        'End Try


        'Threading.Thread.Sleep(100)

        'If versionMS2000 Then
        '    Str = accelRight
        'Else
        '    Str = accelLeft
        'End If

        'Str += ControlChars.Cr
        'Try
        '    SerialPort1.Write(Str)
        'Catch ex As Exception
        'End Try

        'Threading.Thread.Sleep(100)


        'Try
        '    SerialPort1.DiscardInBuffer()

        'Catch ex As Exception

        'End Try
    End Sub

    'Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
    '    Try
    '        Process.Start(directoryInfo.FullName & "\delete.bat")
    '    Catch ex As Exception
    '        MessageBox.Show("Batch file not found..")
    '    End Try
    '    'MessageBox.Show(directoryInfo.FullName)
    'End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick 'Timer to wait one second before calling next well.
        Label7.Hide()
        Timer2.Stop()
        ' MsgBox("timer2Tick")
        NextWell()
    End Sub





    Sub autofocusingButtonAlias()
        'autofocusAlgorithmV2(True)
        'autofocusAlgorithmV2(False)
        'autofocusAlgorithmFineOnlyUp()
        autofocusAlgorithmFineOnlyUpV2()

    End Sub

    Sub autofocusAlgorithmV2(ByVal isItLong)
        doThirdRound = False ' only true when doing the third and last round of autofocusing
        If isItLong Then
            Label9.Text = "Coarse Autofocusing..."
        Else
            Label9.Text = "Round 2..."

        End If
        Label9.Show()
        coarse = True
        If versionMS2000 Then
            If isItLong Then
                times = 4 '7
            Else
                times = 5
            End If

        Else
            If isItLong Then
                times = 3 '7
            Else
                times = 4
            End If
        End If

        autofocusing = True
        va = autof()
        Label8.Text = va
        Refresh()
        ' MessageBox.Show("now going initial up")
        myFocusUp(isItLong, times)

        lastmovementwasUp = True
        firstmovement = True
        endmovement = False


        vb = autof()
        Label8.Text = vb
        Refresh()

        If vb < va Then
            '  MessageBox.Show("now going down twice")
            myFocusDown(isItLong, times)
            myFocusDown(isItLong, times)
            lastmovementwasUp = False
            vb = autof()
            Label8.Text = vb
            Refresh()
        End If

        While vb > va
            If lastmovementwasUp Then


                'MessageBox.Show("now going up")
                myFocusUp(isItLong, times)   'go up 10 times.
                lastmovementwasUp = True

            Else
                ' MessageBox.Show("now going down")
                myFocusDown(isItLong, times)
                lastmovementwasUp = False
            End If
            va = vb
            vb = autof()
            Label8.Text = vb
            Refresh()
        End While
        ' MessageBox.Show("doing last movement")
        If lastmovementwasUp Then

            ' MessageBox.Show("now going down")
            myFocusDown(isItLong, times)   'go up 10 times.
            lastmovementwasUp = False

        Else
            ' MessageBox.Show("now going up")
            myFocusUp(isItLong, times)
            lastmovementwasUp = True
        End If
    End Sub

    Sub autofocusAlgorithmFineOnlyUp()
        ' MessageBox.Show("now finer")
        Label9.Text = "Now Finer..."
        myFocusDown(False, 8)
        va = autof()
        myFocusUp(False, 2)
        vb = autof()
        Label8.Text = vb
        Refresh()
        While vb > va
            myFocusUp(False, 2)
            va = vb
            vb = autof()
            Label8.Text = vb
            Refresh()

        End While
        Label9.Hide()
    End Sub

    Sub autofocusAlgorithmFineOnlyUpV2()
        ' MessageBox.Show("now finer")
        Label9.Text = "Now Fine Autofocus..."
        'myFocusDown(False, 8)
        va = autof()
        Label8.Text = va
        Refresh()
        If RadioButton4.Checked Then
            myFocusUp(False, 2)
        Else
            myFocusDown(False, 2)
        End If

        vb = autof()
        Label8.Text = vb
        Refresh()
        ' MessageBox.Show(vb)
        While vb > 6500000
            If RadioButton4.Checked Then
                myFocusUp(False, 2)
            Else
                myFocusDown(False, 2)
            End If
            va = vb
            vb = autof()
            Label8.Text = vb
            Refresh()
            'MessageBox.Show(vb)
        End While
        'While vb > va
        '    If RadioButton4.Checked Then
        '        myFocusUp(False, 2)
        '    Else
        '        myFocusDown(False, 2)
        '    End If
        '    va = vb
        '    vb = autof()
        '    Label8.Text = vb
        '    Refresh()

        'End While
        Label9.Hide()
    End Sub
    'OLDER VERSION:
    Sub autofocusAlgorithm()
        doThirdRound = False ' only true when doing the third and last round of autofocusing
        Label9.Text = "Coarse Autofocusing..."
        Label9.Show()
        coarse = True
        If versionMS2000 Then
            times = 3 '7
        Else
            times = 5
        End If

        autofocusing = True
        va = autof()

        myFocusUp(coarse, times)

        lastmovementwasUp = True
        firstmovement = True
        endmovement = False

        autofocusAlgorithm2()

    End Sub
    Sub autofocusAlgorithm2()
        ' MessageBox.Show("2")
        vb = autof()
        'MessageBox.Show("3")
        If vb > va Then
            If lastmovementwasUp Then

                'MessageBox.Show("vb " & vb & " va " & va & " vb>va " & "last movement was up so now go up")
                myFocusUp(coarse, times)   'go up 10 times.
                lastmovementwasUp = True

            Else
                'MessageBox.Show("vb " & vb & " va " & va & " vb>va " & "last movement was down so now go down")
                myFocusDown(coarse, times)
                lastmovementwasUp = False
            End If
            va = vb
            firstmovement = False
            autofocusAlgorithm2()
            Exit Sub

        Else
            If firstmovement Then
                'MessageBox.Show("vb " & vb & " va " & va & " vb<va " & "this is the first movement so go down")
                myFocusDown(coarse, times)
                motorDownon = True
                'Label14.Visible = True
                'Label37.Visible = True

                firstmovement = False
                lastmovementwasUp = False
                va = vb
                autofocusAlgorithm2()
                Exit Sub
            Else
                endmovement = True
                If lastmovementwasUp Then
                    'MessageBox.Show("vb " & vb & " va " & va & " vb<va " & "this is NOT the first movement and last one was up so go down once and stop")
                    myFocusDown(coarse, times)
                Else
                    ' MessageBox.Show("vb " & vb & " va " & va & " vb<va " & "this is NOT the first movement and last one was down so go up once and stop")
                    myFocusUp(coarse, times)
                End If

                'MessageBox.Show("finished autofocusing")
                vb = autof() 'this just to show the last position


                If coarse = True Then  'Now do fine!
                    Label9.Text = "Fine Autofocusing..."
                    coarse = False
                    doThirdRound = True 'will call a third round of autofocusing after this second round finishes.
                    times = 4 'times to repeat PinMotorUpFine() 
                    autofocusing = True
                    va = autof()

                    myFocusUp(coarse, times)

                    lastmovementwasUp = True
                    firstmovement = True
                    endmovement = False

                    autofocusAlgorithm2()
                    Exit Sub

                End If


                If doThirdRound = True Then  'Now do fine!

                    Label9.Text = "Fine Autofocusing 2..."
                    coarse = False
                    doThirdRound = False
                    times = 2 'times to repeat PinMotorUpFine() 
                    autofocusing = True
                    va = autof()

                    myFocusUp(coarse, times)

                    lastmovementwasUp = True
                    firstmovement = True
                    endmovement = False

                    autofocusAlgorithm2()

                    Exit Sub

                End If





            End If
        End If
        Label9.Hide()
        'MessageBox.Show("finished autofocusing")
        autofocusing = False

        If callerisTest Then
        Else
            'Call Grabwells()
        End If

    End Sub

    Sub myFocusUp(ByVal _long As Boolean, ByVal times As Integer)
        Label7.Text = "Objective is moving up..."
        Label7.Show()
        Label6.Show()
        Refresh()
        If _long Then
            For i As Integer = 1 To times
                PinMotorUpCoarse()
                Threading.Thread.Sleep(500)
                'Sleep200  10times is 7 units=70um
                'Sleep200 1time  is 0.6 units=7um
            Next

        Else

            For i As Integer = 1 To times
                PinMotorUpFine()
                Threading.Thread.Sleep(50)
                'Sleep50 10times is 0.8units=8um
                'Sleep50 7times is 0.5units=5um
            Next

        End If
        Label7.Hide()
        Label6.Hide()
    End Sub
    Sub myFocusDown(ByVal _long As Boolean, ByVal times As Integer)
        Label7.Text = "Objective is moving down..."
        Label7.Show()
        Label11.Show()
        Refresh()
        If _long Then
            For i As Integer = 1 To times
                PinMotorDownCoarse()
                Threading.Thread.Sleep(500)
            Next

        Else
            For i As Integer = 1 To times
                PinMotorDownFine()
                Threading.Thread.Sleep(50)
            Next

        End If
        Label7.Hide()
        Label11.Hide()
    End Sub

    Function autof() As Integer

        'GrabImage()
        'GrabImageForLoop()
        PictureBox1.Refresh()
        newimage = bitmap
        ''''''''ME. IF YOU DON'T KNOW THE NUMBER OF BYTES OF THE IMAGE:
        'Dim rect As New Rectangle(0, 0, newImage.Width, newImage.Height)
        rect.X = 0
        rect.Y = 0
        rect.Width = newimage.Width
        rect.Height = newimage.Height
        ' Dim newImageData As System.Drawing.Imaging.BitmapData = newImage.LockBits(rect, _
        'Drawing.Imaging.ImageLockMode.ReadWrite, newImage.PixelFormat)
        newImageData = newimage.LockBits(rect, _
          Drawing.Imaging.ImageLockMode.ReadWrite, newimage.PixelFormat)
        'MessageBox.Show(newimage.PixelFormat.ToString)
        ' Get the address of the first line.
        ptr = newImageData.Scan0
        'Dim ptr As IntPtr = newImageData.Scan0

        ' Declare an array to hold the bytes of the bitmap.
        ' This code is specific to a bitmap with 24 bits per pixels.

        ' Dim bytes As Integer = Math.Abs(newImageData.Stride) * newImage.Height
        bytes = Math.Abs(newImageData.Stride) * newimage.Height

        'Dim rgbValues(bytes - 1) As Byte
        'Array.Resize(rgbValues, bytes)
        Array.Resize(rgbValues, 5053440) '5053440
        'Dim rgbValues(3686400 - 1) As Byte

        ' Copy the RGB values into the array.
        'Try
        'MessageBox.Show(newImageData.Width.ToString & " " & rgbValues.Length & " " & bytes.ToString)

        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)

        ' Catch ex As Exception
        'MessageBox.Show(ex.Message)
        '  End Try
        'System.Runtime.InteropServices.Marshal.Copy(flycapRGBImage.pData, rgbValues, 0, 3686400)

        ' Unlock the bits.
        newimage.UnlockBits(newImageData)

        '''''''''ME. IF YOU DO KNOW THE NUMBER OF BYTES OF THE IMAGE:

        ''Dim rect As New Rectangle(0, 0, newImage.Width, newImage.Height)
        ''Dim newImageData As System.Drawing.Imaging.BitmapData = newImage.LockBits(rect, _
        ''   Drawing.Imaging.ImageLockMode.ReadWrite, newImage.PixelFormat)

        '' Get the address of the first line.
        ''Dim ptr As IntPtr = newImageData.Scan0

        '' Declare an array to hold the bytes of the bitmap.
        '' This code is specific to a bitmap with 24 bits per pixels.

        ''Dim bytes As Integer = Math.Abs(newImageData.Stride) * newImage.Height
        ''Dim rgbValues(bytes - 1) As Byte
        'Dim rgbValues(3686400 - 1) As Byte

        '' Copy the RGB values into the array.
        ''System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)
        'System.Runtime.InteropServices.Marshal.Copy(flycapRGBImage.pData, rgbValues, 0, 3686400)


        '' Unlock the bits.
        ''newImage.UnlockBits(newImageData)



        ''''''''''''''''
        'w = newimage.Width
        'h = newimage.Height
        'For y As Integer = 0 To h / 2
        '    offset = y * w * 3
        '    For x As Integer = 0 To (w - 1)
        '        a = rgbValues((x * 3) + offset)
        '        'Label15.Text = Label15.Text & vbCr & bitmap.GetPixel(x, y).ToString & vbCr & rgbValues((x * 3) + offset)
        '        sum += a
        '        ' Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
        '        ' objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
        '        'objWriter.Close()
        '    Next
        'Next

        '''''''''''''''''
        sum = 0
        w = newimage.Width
        h = newimage.Height
        s1 = DateTime.Now.Second.ToString()
        m1 = DateTime.Now.Millisecond.ToString()
        Try
            For y As Integer = 0 To h 'h
                offset = y * w * 3
                For x As Integer = 0 To (w - 1)
                    'Label22.Text = Label22.Text & rgbValues(x) & "," & rgbValues(x + offset)
                    'pixelcolor = newImage.GetPixel(x, 0)
                    'Label34.Text = Label34.Text & pixelcolor.R.ToString & ","
                    'pixelcolor = newImage.GetPixel(x, 1)
                    'Label34.Text = Label34.Text & pixelcolor.R.ToString & ","
                    a = rgbValues((x * 3) + offset)
                    b = rgbValues((x * 3) + 3 + offset)  ' I THINK 1 SHOULD BE 3 INSTEAD!!!!
                    c = rgbValues((x * 3) + (w * 3) + offset)

                    sum += Math.Abs(b - a) '^ 2
                    sum += Math.Abs(c - a) '^ 2
                    'Label22.Text = Label22.Text & pixelcolor.ToString & ControlChars.NewLine
                    'Label22.Text = Label22.Text & ControlChars.NewLine & " hi"
                    'iterations += 1
                    'Console.WriteLine("sum " & sum)
                    'Console.WriteLine("h " & h)
                    'Console.WriteLine("w " & w)
                    'Console.WriteLine("offset " & offset)
                    'Console.WriteLine("iterations " & iterations)
                    '7680+h*w=3686400
                Next
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'MessageBox.Show(ex.Message & " Iterations " & iterations)
            'Console.WriteLine("sum " & sum)
            'Console.WriteLine("h " & h)
            'Console.WriteLine("w " & w)
            'Console.WriteLine("offset " & offset)
            'Console.WriteLine("iterations " & iterations)
            'Console.WriteLine("total " & rgbValues.Length)
            'Console.WriteLine("total/3 " & rgbValues.Length / 3)
        End Try

        '
        Console.WriteLine("sum " & sum)
        Console.WriteLine("h " & h)
        Console.WriteLine("w " & w)
        Console.WriteLine("offset " & offset)
        'Console.WriteLine("bytes " & bytes)
        ' Console.WriteLine("iterations " & iterations)


        s2 = DateTime.Now.Second.ToString()
        m2 = DateTime.Now.Millisecond.ToString()

        difs = s2 - s1
        difm = m2 - m1
        If difs < 0 Then
            difs = difs + 60
        End If
        If difm < 0 Then
            difm = difm + 1000
            difs = difs - 1
        End If
        difs = difs.ToString("00")
        difm = difm.ToString("000")
        Console.WriteLine("Calculation time:")
        Console.WriteLine(difs & "." & difm)
        'Label22.Text = (difs & "." & difm)
        'Label34.Text = sum


        'flycaptureStop(flycapContext)

        sum = sum / 10000
        Return sum



    End Function




    'Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)

    '    'If versionMS2000 Then
    '    '    mywidth = widthMS2000
    '    '    myheigth = heigthMS2000
    '    'Else
    '    '    mywidth = widthMS4
    '    '    myheigth = heigthMS4
    '    'End If

    '    If versionMS2000 Then
    '        My.Settings.versionMS2000 = False

    '        'distancHorizontal = "-1241"
    '        'distancVertical = "-621"
    '        Label10.Text = "Using Stage MS-4..."


    '    Else
    '        My.Settings.versionMS2000 = True
    '        'distancHorizontal = "8182"
    '        'distancVertical = "6124"
    '        Label10.Text = "Using Stage MS-2000..."
    '    End If

    '    My.Settings.Save()
    '    versionMS2000 = My.Settings.versionMS2000
    'End Sub






    Sub savePositionstxt(ByVal mydir As String)
        'Saves in the current directory
        Try

        
        FILE_NAME = mydir & "\SAVEDX.txt"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, False)
        For i As Integer = 0 To My.Settings.PositionsX.Count - 1
            objWriter.WriteLine(My.Settings.PositionsX.Item(i))

        Next
        objWriter.Close()

        Dim FILE_NAME2 As String = mydir & "\SAVEDY.txt"
        Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME2, False)
        For i As Integer = 0 To My.Settings.PositionsY.Count - 1
            objWriter2.WriteLine(My.Settings.PositionsY.Item(i))

        Next


            objWriter2.Close()
        Catch ex As Exception

        End Try

    End Sub

    Sub saveConfigtxt(ByVal mydir As String)
        'Saves in the current directory
        FILE_NAME = mydir & "\Config" & loadconfigcount.ToString & ".txt"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, False)

        objWriter.WriteLine(configName)
        objWriter.WriteLine(mywidth)
        objWriter.WriteLine(myheigth)
        objWriter.WriteLine(wellwidth)
        objWriter.WriteLine(wellheight)

        objWriter.Close()
    End Sub

    Sub movepositionstoprevioustxtfiles()
        FileCopy(directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED4X.txt", directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED5X.txt")
        FileCopy(directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED4Y.txt", directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED5Y.txt")
        FileCopy(directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED3X.txt", directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED4X.txt")
        FileCopy(directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED3Y.txt", directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED4Y.txt")
        FileCopy(directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED2X.txt", directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED3X.txt")
        FileCopy(directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED2Y.txt", directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED3Y.txt")
        FileCopy(directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVEDX.txt", directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED2X.txt")
        FileCopy(directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVEDY.txt", directoryInfo.FullName & "\ConfigAndErrorFiles" & "\SAVED2Y.txt")
    End Sub
    'Sub moveConfigstoprevioustxtfiles()
    '    FileCopy(Directory.GetCurrentDirectory & "\Config4.txt", Directory.GetCurrentDirectory & "\Config5.txt")
    '    FileCopy(Directory.GetCurrentDirectory & "\Config3.txt", Directory.GetCurrentDirectory & "\Config4.txt")
    '    FileCopy(Directory.GetCurrentDirectory & "\Config2.txt", Directory.GetCurrentDirectory & "\Config3.txt")
    '    FileCopy(Directory.GetCurrentDirectory & "\Config.txt", Directory.GetCurrentDirectory & "\Config2.txt")
    'End Sub


    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click
        loadposcount = 1  'this is to set 0 the counter for loading previously saved positions
        loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")

    End Sub



    Sub drawPostions()
        'PictureBox2.Image.Dispose()
        'PictureBox2.Image = Nothing
        gr = Graphics.FromImage(bm)
        gr2 = Graphics.FromImage(bm2)
        gr.Clear(PictureBox2.BackColor)
        gr2.Clear(PictureBox3.BackColor)
        Try

            ListBox4.Items.Clear()
            ListBox5.Items.Clear()
            ListBox6.Items.Clear()
            ListBox7.Items.Clear()

            If CheckBox17.Checked = True Then 'switch displayed xy
                For i = 0 To ListBox2.Items.Count - 1
                    ListBox7.Items.Add(ListBox2.Items.Item(i))
                Next
                For i = 0 To ListBox3.Items.Count - 1
                    ListBox6.Items.Add(ListBox3.Items.Item(i))
                Next
            ElseIf SwitchX.Checked Then
                For i = 0 To ListBox2.Items.Count - 1
                    ListBox6.Items.Add(ListBox2.Items.Item(i))
                Next
                For i = 0 To ListBox3.Items.Count - 1
                    ListBox7.Items.Add(ListBox3.Items.Item(i))
                Next
            ElseIf SwitchY.Checked Then
                For i = 0 To ListBox2.Items.Count - 1
                    ListBox6.Items.Add(ListBox2.Items.Item(i))
                Next
                For i = 0 To ListBox3.Items.Count - 1
                    ListBox7.Items.Add(-ListBox3.Items.Item(i))
                Next

            Else
                For i = 0 To ListBox2.Items.Count - 1
                    ListBox6.Items.Add(-ListBox2.Items.Item(i))
                Next
                For i = 0 To ListBox3.Items.Count - 1
                    ListBox7.Items.Add(ListBox3.Items.Item(i))
                Next
            End If

            PictureBox2.Image = Nothing
            PictureBox3.Image = Nothing
            Refresh()
            Dim boxwidth = PictureBox2.Height
            Dim boxwidth2 = PictureBox3.Height
            Dim maxItemX As Double
            Dim minItemX As Double = 0
            Dim maxItemY As Double
            Dim minItemY As Double = 0
            Dim rangeX As Double
            Dim rangeY As Double
            Dim factorX As Double
            Dim factorY As Double
            Dim factorX2 As Double
            Dim factorY2 As Double
            Dim drawPosX As Double
            Dim drawPosY As Double

            'look for max in x
            maxItemX = ListBox6.Items.Item(0)
            For i = 1 To ListBox6.Items.Count - 1

                If ListBox6.Items.Item(i) > maxItemX Then
                    maxItemX = ListBox6.Items.Item(i)
                End If

            Next
            'now look for minimum in x

            minItemX = ListBox6.Items.Item(0)

            For i = 1 To ListBox6.Items.Count - 1


                If ListBox6.Items.Item(i) < minItemX Then
                    minItemX = ListBox6.Items.Item(i)
                End If
            Next

            'look for max in Y

            For i = 1 To ListBox7.Items.Count - 1
                If ListBox7.Items.Item(i) > maxItemY Then
                    maxItemY = ListBox7.Items.Item(i)
                End If
            Next

            'now look for minimum in y

            For i = 1 To ListBox7.Items.Count - 1

                If ListBox7.Items.Item(i) < minItemY Then
                    minItemY = ListBox7.Items.Item(i)
                End If

            Next

            rangeX = maxItemX - minItemX
            rangeY = maxItemY - minItemY

            If rangeX = 0 Then
            Else

                factorX = (boxwidth - 35) / rangeX
                factorX2 = (boxwidth2 - 35) / rangeX
            End If


            If rangeY = 0 Then
            Else
                factorY = (boxwidth - 35) / rangeY
                factorY2 = (boxwidth2 - 35) / rangeY
            End If

            ListBox4.Items.Clear()
            ListBox5.Items.Clear()

            For i = 0 To ListBox6.Items.Count - 1

                ListBox4.Items.Add(ListBox6.Items.Item(i) - minItemX)

            Next
            For i = 0 To ListBox7.Items.Count - 1

                ListBox5.Items.Add(ListBox7.Items.Item(i) - minItemY)
            Next

            ' Try
            For i = 0 To ListBox6.Items.Count - 1
                drawPosX = ListBox4.Items.Item(i) * factorX + 10
                drawPosY = ListBox5.Items.Item(i) * factorY + 20
                ' PictureBox2.CreateGraphics.DrawString(i + 1, myFont, Brushes.Black, drawPosX, boxwidth - drawPosY)
                gr = Graphics.FromImage(bm)
                gr.DrawString(i + 1, myFont, Brushes.Black, drawPosX, boxwidth - drawPosY)
                PictureBox2.Image = bm


                drawPosX = ListBox4.Items.Item(i) * factorX2 + 10
                drawPosY = ListBox5.Items.Item(i) * factorY2 + 20
                gr2 = Graphics.FromImage(bm2)
                gr2.DrawString(i + 1, myFont, Brushes.Black, drawPosX, boxwidth2 - drawPosY)
                PictureBox3.Image = bm2
            Next
            'Catch ex As Exception
            ' End Try
        Catch ex As Exception

        End Try

    End Sub


    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        Timer4.Stop()
        drawPostions()
    End Sub

    Private Sub Button28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button28.Click
        MessageBox.Show(versionMS2000 & " versionMS2000")
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        autofocusCounter = TextBox4.Text
    End Sub



    'Sub liveONbuttonForFocus()

    '    grabThread = New System.Threading.Thread(AddressOf GrabLoopForFocus)
    '    grabThread.Start() 'GrabLoop()
    'End Sub


    Private Sub Button29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button29.Click
        startFocusing()

    End Sub

    Dim resultChange As Integer
    Sub startFocusing()

        'liveONbuttonForFocus()

        ' Power on the camera
        cam.WriteRegister(k_cameraPower, k_powerVal)
        ' Wait for camera to complete power-up
        regVal = 0
        Do While ((regVal And k_powerVal) = 0)
            System.Threading.Thread.Sleep(k_millisecondsToSleep)
            Try
                regVal = cam.ReadRegister(k_cameraPower)
            Catch ex As Exception
            End Try
        Loop
        'RadioButton9.Checked = True 'Df radiobutton.
        lightON()
        cam.StartCapture()
        enableMotor()
        Thread.Sleep(200)


        Label7.Text = "Autofocusing..."
        Label7.Show()
        PictureBox1.Refresh()
        Label7.Refresh()
        numberOfSteps = AutoFocusSteps.Text
        focusingMotorSoDontDisableIt = True

        resultChange = 0
        GrabLoopForFocus()

        cam.StopCapture()
        ' Power off the camera
        cam.WriteRegister(k_cameraPower, k_powerValoff2)

        lightsOFF()

        'move back extra 40 steps always:
        ArduinoPin("mb" & TextBoxMoveBack.Text)

        'save the new focus position as the new place to focus the next time:
        recordedfocus(mypos) = recordedfocus(mypos) + ((CInt(StepSize.Text)) * (resultChange)) - CInt(TextBoxMoveBack.Text)

        'disableMotor()  'it will disable on its own
        Thread.Sleep(400)
        focusingMotorSoDontDisableIt = False
        disableMotor()
        Thread.Sleep(400)
        'grabThread.Abort()
    End Sub

    Dim focusingList As New List(Of Integer)
    Dim previous As Integer
    Dim maxvalue As Integer
    Dim count As Integer
    Dim position As Integer
    Dim regVal As UInteger
    Const k_cameraPower As UInteger = &H610
    Const k_powerVal As UInt32 = &H80000000UI
    Const k_millisecondsToSleep = 100
    Const k_powerValoff2 As UInt32 = &H0UI
    Dim numberOfSteps As Integer
    Dim change As Integer

    Private Sub GrabLoopForFocus()

        focusingList.Clear()

        'FIRST MOVE BACKWARDS (half the number of total steps):
        motorIsStopped = True
        addtomyConsoleMain("Moving Backwards")
        ArduinoPin("mb" & ((CInt(StepSize.Text)) * (numberOfSteps / 2)).ToString)
        Thread.Sleep(1000)
        QueryStoppedMotor()
        WaitEvent.WaitOne()
        addtomyConsoleMain("Done")

        'NOW MOVE FORWARDS 
        addtomyConsoleMain("Moving Forwards")
        For i = 1 To numberOfSteps
            motorIsStopped = True
            ArduinoPin("mf" & StepSize.Text)
            QueryStoppedMotor()
            WaitEvent.WaitOne()
            GrabImageForLoop()
            va = autof()
            focusingList.Add(va)
            addtomyConsoleMain(va)
            'SaveImageforVa(i)
            'saveVaFile()
        Next
        'va = autof()
        'addtomyConsole(va)
        addtomyConsoleMain("-done")

        'FIND MAX VALUE IN LIST:

        count = 0
        maxvalue = 0
        For Each i As Integer In focusingList
            'addtomyConsoleErrorMessages(i.ToString)
            count += 1
            If i > maxvalue Then
                maxvalue = i
                position = count
            End If
        Next

        'MOVE BACK TO BEST FOCUSED POSITION:
        addtomyConsoleMain("Moving Back To Final")
        motorIsStopped = True
        ArduinoPin("mb" & ((CInt(StepSize.Text)) * (numberOfSteps - position)).ToString)
        Thread.Sleep(700)
        QueryStoppedMotor()
        WaitEvent.WaitOne()
        addtomyConsoleMain("Done")


        change = numberOfSteps - position
        resultChange = resultChange + position - (numberOfSteps / 2)  'positive is forward.
        addtomyConsoleErrorMessages(maxvalue & " is max, at pos " & position & ", move back " & change)
        'MsgBox("found max " & ".  List count is " & focusingList.Count)
        Label7.Hide()

        If change = 0 Or change = 1 Or change = 8 Or change = (numberOfSteps - 1) Or change = numberOfSteps Then  'if change in focus is too big then repeat..
            'repeat the focusing
            addtomyConsoleMain("REPEATNG...in 1secs") 'it seems to be important to wait 1 second
            Thread.Sleep(1000)
            GrabLoopForFocus()
            Exit Sub
        End If


        'GrabImageForLoop()





    End Sub












    Sub SaveImageforVa(ByVal i As Integer)
        ' mydate = DateTime.Now.ToString("MM-dd-yyyy_(HH-mm-ss-tt)")
        mydate = va
        If RadioButton6.Checked Then
            typeoflight = "_bf"
        End If
        If RadioButton7.Checked Then
            typeoflight = "_bf2"
        End If
        If RadioButton8.Checked Then
            typeoflight = "_fl"
        End If
        If RadioButton9.Checked Then
            typeoflight = "_df"
        End If
        'If (Not System.IO.Directory.Exists(imagefolder & "\Now")) Then
        '    System.IO.Directory.CreateDirectory(imagefolder & "\Now")
        'End If
        If (Not System.IO.Directory.Exists(sessionfolder & "\Pos" & currentListItem + 1)) Then
            System.IO.Directory.CreateDirectory(sessionfolder & "\Pos" & currentListItem + 1 & typeoflight)
        End If
        'PictureBox1.Image.Save(imagefolder & "\Now\Pos" & currentListItem + 1 & typeoflight & "\" & i & "-" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        PictureBox1.Image.Save(sessionfolder & "\Pos" & currentListItem + 1 & typeoflight & "\" & i & "-" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        'PictureBox1.Image.Save(sessionfolder & "\Pos" & currentListItem + 1 & typeoflight & camtype & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg) 'ImageFormat.Jpeg



    End Sub

    Sub saveVaFile()
        'Saves in the current directory

        FILE_NAME = sessionfolder & "\Pos" & currentListItem + 1 & typeoflight & "\SAVEDVa.txt"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
        'For i As Integer = 0 To My.Settings.PositionsX.Count - 1
        objWriter.WriteLine(va)
        'Next  
        objWriter.Close()
    End Sub

    Private Sub Button30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        vb = autof()
        Label8.Text = vb
    End Sub


    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        Dim bm As New Bitmap(imagefolder & "\Now\Pos1_bf\1.jpg")
        PictureBox1.Image = bm

        newimage = bm



        ''''''''ME. IF YOU DON'T KNOW THE NUMBER OF BYTES OF THE IMAGE:
        'Dim rect As New Rectangle(0, 0, newImage.Width, newImage.Height)
        rect.X = 0
        rect.Y = 0
        rect.Width = newimage.Width
        rect.Height = newimage.Height



        ' Dim newImageData As System.Drawing.Imaging.BitmapData = newImage.LockBits(rect, _
        'Drawing.Imaging.ImageLockMode.ReadWrite, newImage.PixelFormat)
        newImageData = newimage.LockBits(rect, _
          Drawing.Imaging.ImageLockMode.ReadWrite, newimage.PixelFormat)
        'MessageBox.Show(newimage.PixelFormat.ToString)
        ' Get the address of the first line.
        ptr = newImageData.Scan0
        'Dim ptr As IntPtr = newImageData.Scan0

        ' Declare an array to hold the bytes of the bitmap.
        ' This code is specific to a bitmap with 24 bits per pixels.

        ' Dim bytes As Integer = Math.Abs(newImageData.Stride) * newImage.Height
        bytes = Math.Abs(newImageData.Stride) * newimage.Height


        'Dim rgbValues(bytes - 1) As Byte
        Array.Resize(rgbValues, bytes)
        'Dim rgbValues(3686400 - 1) As Byte

        ' Copy the RGB values into the array.
        'Try
        'MessageBox.Show(newImageData.Width.ToString & " " & rgbValues.Length & " " & bytes.ToString)

        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)

        ' Catch ex As Exception
        'MessageBox.Show(ex.Message)
        '  End Try
        'System.Runtime.InteropServices.Marshal.Copy(flycapRGBImage.pData, rgbValues, 0, 3686400)

        ' Unlock the bits.
        newimage.UnlockBits(newImageData)


        '''''''''ME. IF YOU DO KNOW THE NUMBER OF BYTES OF THE IMAGE:

        ''Dim rect As New Rectangle(0, 0, newImage.Width, newImage.Height)
        ''Dim newImageData As System.Drawing.Imaging.BitmapData = newImage.LockBits(rect, _
        ''   Drawing.Imaging.ImageLockMode.ReadWrite, newImage.PixelFormat)

        '' Get the address of the first line.
        ''Dim ptr As IntPtr = newImageData.Scan0

        '' Declare an array to hold the bytes of the bitmap.
        '' This code is specific to a bitmap with 24 bits per pixels.

        ''Dim bytes As Integer = Math.Abs(newImageData.Stride) * newImage.Height
        ''Dim rgbValues(bytes - 1) As Byte
        'Dim rgbValues(3686400 - 1) As Byte

        '' Copy the RGB values into the array.
        ''System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)
        'System.Runtime.InteropServices.Marshal.Copy(flycapRGBImage.pData, rgbValues, 0, 3686400)


        '' Unlock the bits.
        ''newImage.UnlockBits(newImageData)



        ''''''''''''''''

        sum = 0
        w = newimage.Width * 3
        'w = 960 * 3
        h = newimage.Height - 3
        'h = 1280 - 3
        s1 = DateTime.Now.Second.ToString()
        m1 = DateTime.Now.Millisecond.ToString()
        Try

            'Console.WriteLine("last value" & rgbValues(w + w + h * w))
            'Console.WriteLine("last real value" & rgbValues(3686400 - 1))
            'Dim pixelcolor As Color

            'Label22.Text = Label22.Text & ControlChars.NewLine & " hi"

            For y As Integer = 0 To h 'h
                offset = y * w
                For x As Integer = 0 To w Step 3
                    'Label22.Text = Label22.Text & rgbValues(x) & "," & rgbValues(x + offset)
                    'pixelcolor = newImage.GetPixel(x, 0)
                    'Label34.Text = Label34.Text & pixelcolor.R.ToString & ","
                    'pixelcolor = newImage.GetPixel(x, 1)
                    'Label34.Text = Label34.Text & pixelcolor.R.ToString & ","
                    a = rgbValues(x + offset)
                    b = rgbValues(x + 1 + offset)
                    c = rgbValues(x + w + offset)

                    sum += Math.Abs(b - a)
                    sum += Math.Abs(c - a)
                    'Label22.Text = Label22.Text & pixelcolor.ToString & ControlChars.NewLine
                    'Label22.Text = Label22.Text & ControlChars.NewLine & " hi"
                    'iterations += 1
                    'Console.WriteLine("sum " & sum)
                    'Console.WriteLine("h " & h)
                    'Console.WriteLine("w " & w)
                    'Console.WriteLine("offset " & offset)
                    'Console.WriteLine("iterations " & iterations)
                    '7680+h*w=3686400
                Next
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'MessageBox.Show(ex.Message & " Iterations " & iterations)
            'Console.WriteLine("sum " & sum)
            'Console.WriteLine("h " & h)
            'Console.WriteLine("w " & w)
            'Console.WriteLine("offset " & offset)
            'Console.WriteLine("iterations " & iterations)
            'Console.WriteLine("total " & rgbValues.Length)
            'Console.WriteLine("total/3 " & rgbValues.Length / 3)
        End Try

        '
        Console.WriteLine("sum " & sum)
        Console.WriteLine("h " & h)
        Console.WriteLine("w " & w)
        Console.WriteLine("offset " & offset)
        'Console.WriteLine("bytes " & bytes)
        ' Console.WriteLine("iterations " & iterations)


        s2 = DateTime.Now.Second.ToString()
        m2 = DateTime.Now.Millisecond.ToString()

        difs = s2 - s1
        difm = m2 - m1
        If difs < 0 Then
            difs = difs + 60
        End If
        If difm < 0 Then
            difm = difm + 1000
            difs = difs - 1
        End If
        difs = difs.ToString("00")
        difm = difm.ToString("000")
        Console.WriteLine("Calculation time:")
        Console.WriteLine(difs & "." & difm)
        'Label22.Text = (difs & "." & difm)
        'Label34.Text = sum


        'flycaptureStop(flycapContext)


        'Return sum



        Label8.Text = vb


    End Sub



    Private Sub LinkLabel6_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel6.LinkClicked
        'lightsOFF()

        vb = autof()
        Label8.Text = vb
    End Sub



    Private Sub Button30_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button30.Click
        If Panel3.Visible = True Then
            Panel3.Visible = False
            My.Settings.Panel3Shown = False
            'Panel3.Show()
        Else
            Panel3.Visible = True
            My.Settings.Panel3Shown = True
            'Panel3.Hide()
        End If

        My.Settings.Save()
    End Sub


    Private Sub Button33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button33.Click

        Try
            mytext = TextBox5.Text
            ListBox2.Items.Add(mytext)
            My.Settings.PositionsX.Add(mytext)
        Catch ex As Exception
            MessageBox.Show("my error345")
        End Try

        Try
            mytext = TextBox6.Text
            ListBox3.Items.Add(mytext)

            My.Settings.PositionsY.Add(mytext)
        Catch ex As Exception

        End Try


        My.Settings.Save()
        ListBox1.Items.Clear()
        'loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
        'drawPostions()
        loaddata()

    End Sub

    Private Sub Button34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button34.Click
        AddZero()
    End Sub

    Sub AddZero()
        Try
            mytext = "0"
            ListBox2.Items.Add(mytext)
            My.Settings.PositionsX.Add(mytext)
        Catch ex As Exception
            MessageBox.Show("my error345")
        End Try
        Try
            mytext = "0"
            ListBox3.Items.Add(mytext)
            My.Settings.PositionsY.Add(mytext)
        Catch ex As Exception
        End Try
        My.Settings.Save()
        ListBox1.Items.Clear()
        'loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
        loaddata()

        'drawPostions()
    End Sub




    Private Sub Button35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button35.Click
        Panel3.Visible = False
        My.Settings.Panel3Shown = False
        My.Settings.Save()
    End Sub







    'Function CenteringAlgorithm()

    '    GrabImage()
    '    Refresh()
    '    newimage = bitmap

    '    ''''''''ME. IF YOU DON'T KNOW THE NUMBER OF BYTES OF THE IMAGE:
    '    'Dim rect As New Rectangle(0, 0, newImage.Width, newImage.Height)
    '    rect.X = 0
    '    rect.Y = 0
    '    rect.Width = newimage.Width
    '    rect.Height = newimage.Height



    '    ' Dim newImageData As System.Drawing.Imaging.BitmapData = newImage.LockBits(rect, _
    '    'Drawing.Imaging.ImageLockMode.ReadWrite, newImage.PixelFormat)
    '    newImageData = newimage.LockBits(rect, _
    '      Drawing.Imaging.ImageLockMode.ReadWrite, newimage.PixelFormat)
    '    'MessageBox.Show(newimage.PixelFormat.ToString)
    '    ' Get the address of the first line.
    '    ptr = newImageData.Scan0
    '    'Dim ptr As IntPtr = newImageData.Scan0

    '    ' Declare an array to hold the bytes of the bitmap.
    '    ' This code is specific to a bitmap with 24 bits per pixels.

    '    ' Dim bytes As Integer = Math.Abs(newImageData.Stride) * newImage.Height
    '    bytes = Math.Abs(newImageData.Stride) * newimage.Height


    '    'Dim rgbValues(bytes - 1) As Byte
    '    'Array.Resize(rgbValues, bytes) '5053440
    '    Array.Resize(rgbValues, 5053440) '5053440

    '    'Dim rgbValues(3686400 - 1) As Byte

    '    ' Copy the RGB values into the array.
    '    'Try
    '    'MessageBox.Show(newImageData.Width.ToString & " " & rgbValues.Length & " " & bytes.ToString)

    '    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)

    '    ' Catch ex As Exception
    '    'MessageBox.Show(ex.Message)
    '    '  End Try
    '    'System.Runtime.InteropServices.Marshal.Copy(flycapRGBImage.pData, rgbValues, 0, 3686400)

    '    ' Unlock the bits.
    '    newimage.UnlockBits(newImageData)


    '    '''''''''ME. IF YOU DO KNOW THE NUMBER OF BYTES OF THE IMAGE:
    '    ''Dim rect As New Rectangle(0, 0, newImage.Width, newImage.Height)
    '    ''Dim newImageData As System.Drawing.Imaging.BitmapData = newImage.LockBits(rect, _
    '    ''   Drawing.Imaging.ImageLockMode.ReadWrite, newImage.PixelFormat)

    '    '' Get the address of the first line.
    '    ''Dim ptr As IntPtr = newImageData.Scan0

    '    '' Declare an array to hold the bytes of the bitmap.
    '    '' This code is specific to a bitmap with 24 bits per pixels.

    '    ''Dim bytes As Integer = Math.Abs(newImageData.Stride) * newImage.Height
    '    ''Dim rgbValues(bytes - 1) As Byte
    '    'Dim rgbValues(3686400 - 1) As Byte

    '    '' Copy the RGB values into the array.
    '    ''System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)
    '    'System.Runtime.InteropServices.Marshal.Copy(flycapRGBImage.pData, rgbValues, 0, 3686400)

    '    '' Unlock the bits.
    '    ''newImage.UnlockBits(newImageData)
    '    ''''''''''''''''

    '    w = newimage.Width
    '    h = newimage.Height
    '    Try
    '        sum = 0
    '        a = 0
    '        Dim FILE_NAME As String = "C:\numbs.txt"

    '        For y As Integer = 0 To h / 2
    '            offset = y * w * 3
    '            For x As Integer = 0 To (w - 1)
    '                a = rgbValues((x * 3) + offset)
    '                'Label15.Text = Label15.Text & vbCr & bitmap.GetPixel(x, y).ToString & vbCr & rgbValues((x * 3) + offset)
    '                sum += a
    '                ' Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
    '                ' objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
    '                'objWriter.Close()
    '            Next
    '        Next
    '        sumb = 0
    '        a = 0
    '        For y As Integer = (h / 2) + 1 To (h - 1)
    '            offset = y * w * 3
    '            For x As Integer = 0 To (w - 1)
    '                a = rgbValues((x * 3) + offset)
    '                'Label15.Text = Label15.Text & vbCr & bitmap.GetPixel(x, y).ToString & vbCr & rgbValues((x * 3) + offset)
    '                sumb += a
    '                'Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
    '                'objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
    '                'objWriter.Close()
    '            Next
    '        Next
    '        valueA = sum / 500000
    '        valueB = sumb / 500000
    '        ' MessageBox.Show(sum / b & " " & sumb / b & " " & sum / b - sumb / b)
    '        Label21.Text = valueA
    '        Label22.Text = valueB
    '        Return (valueA - valueB)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Return 0
    '    End Try
    'End Function


    Private Sub Button40_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button40.Click
        CenteringAlgorithm()
    End Sub

    Sub CenteringAlgorithm()
        Try
            Thread.Sleep(500) 'this wait is not necessary in good computers apparently!!!!
            GrabImage()
        Catch ex As Exception
            stopThread = True
            lightsOFF()
            MessageBox.Show("turning live off")
        End Try

        PictureBox1.Refresh()
        newimage = bitmap


        'FOR EXTERNAL FILE:'''''''''''''''''''''''''''''''''''''''''''''
        'Dim fs As System.IO.FileStream
        'fs = New System.IO.FileStream("C:\1.bmp",
        '     IO.FileMode.Open, IO.FileAccess.Read)
        'newimage = System.Drawing.Image.FromStream(fs)
        'fs.Close()
        'PictureBox1.Image = newimage
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''



        ''''''''ME. IF YOU DON'T KNOW THE NUMBER OF BYTES OF THE IMAGE:
        'Dim rect As New Rectangle(0, 0, newImage.Width, newImage.Height)
        rect.X = 0
        rect.Y = 0
        rect.Width = newimage.Width
        rect.Height = newimage.Height



        ' Dim newImageData As System.Drawing.Imaging.BitmapData = newImage.LockBits(rect, _
        'Drawing.Imaging.ImageLockMode.ReadWrite, newImage.PixelFormat)
        newImageData = newimage.LockBits(rect, _
          Drawing.Imaging.ImageLockMode.ReadWrite, newimage.PixelFormat)
        'MessageBox.Show(newimage.PixelFormat.ToString)
        ' Get the address of the first line.
        ptr = newImageData.Scan0
        'Dim ptr As IntPtr = newImageData.Scan0

        ' Declare an array to hold the bytes of the bitmap.
        ' This code is specific to a bitmap with 24 bits per pixels.

        ' Dim bytes As Integer = Math.Abs(newImageData.Stride) * newImage.Height
        bytes = Math.Abs(newImageData.Stride) * newimage.Height


        'Dim rgbValues(bytes - 1) As Byte
        'Array.Resize(rgbValues, bytes) '5053440
        Array.Resize(rgbValues, 5053440) '5053440

        'Dim rgbValues(3686400 - 1) As Byte

        ' Copy the RGB values into the array.
        'Try
        'MessageBox.Show(newImageData.Width.ToString & " " & rgbValues.Length & " " & bytes.ToString)

        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)

        ' Catch ex As Exception
        'MessageBox.Show(ex.Message)
        '  End Try
        'System.Runtime.InteropServices.Marshal.Copy(flycapRGBImage.pData, rgbValues, 0, 3686400)

        ' Unlock the bits.
        newimage.UnlockBits(newImageData)


        '''''''''ME. IF YOU DO KNOW THE NUMBER OF BYTES OF THE IMAGE:
        ''Dim rect As New Rectangle(0, 0, newImage.Width, newImage.Height)
        ''Dim newImageData As System.Drawing.Imaging.BitmapData = newImage.LockBits(rect, _
        ''   Drawing.Imaging.ImageLockMode.ReadWrite, newImage.PixelFormat)

        '' Get the address of the first line.
        ''Dim ptr As IntPtr = newImageData.Scan0

        '' Declare an array to hold the bytes of the bitmap.
        '' This code is specific to a bitmap with 24 bits per pixels.

        ''Dim bytes As Integer = Math.Abs(newImageData.Stride) * newImage.Height
        ''Dim rgbValues(bytes - 1) As Byte
        'Dim rgbValues(3686400 - 1) As Byte

        '' Copy the RGB values into the array.
        ''System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)
        'System.Runtime.InteropServices.Marshal.Copy(flycapRGBImage.pData, rgbValues, 0, 3686400)

        '' Unlock the bits.
        ''newImage.UnlockBits(newImageData)
        ''''''''''''''''

        w = newimage.Width
        h = newimage.Height
        w = w * 3 'because this is a 24bit per pixel image, which is 3 numbers
        'Try
        sum = 0
        a = 0
        ' Dim FILE_NAME As String = drive & "\numbs.txt"
        Dim halfwidthofarea As Integer = h / 2
        ' MsgBox("h " & h)
        ' MsgBox("w " & w)
        'MsgBox("halfwidthofarea" & halfwidthofarea)
        Dim divideby As Integer = 765 '500
        For y As Integer = ((h / 2) - halfwidthofarea) To (h / 2) - 1
            offset = y * w
            For x As Integer = ((w / 2) - halfwidthofarea * 3) To ((w / 2) + halfwidthofarea * 3 - 1)
                a = rgbValues((x) + offset)
                'b = rgbValues((x) + 1 + offset)
                'c = rgbValues((x) + (w) + offset)
                sum += a
                'sum += Math.Abs(c - a)
                ' Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                ' objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
                'objWriter.Close()
            Next
        Next
        sumb = 0
        a = 0
        For y As Integer = (h / 2) To ((h / 2) + halfwidthofarea) - 1
            offset = y * w
            For x As Integer = ((w / 2) - halfwidthofarea * 3) To ((w / 2) + halfwidthofarea * 3 - 1)
                a = rgbValues((x) + offset)
                'b = rgbValues((x) + 1 + offset)
                'c = rgbValues((x) + (w) + offset)
                sumb += a
                'sumb += Math.Abs(c - a)
                'Label15.Text = Label15.Text & vbCr & bitmap.GetPixel(x, y).ToString & vbCr & rgbValues((x * 3) + offset)
                'Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                'objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
                'objWriter.Close()
            Next
        Next
        valueA = sum / divideby
        valueB = sumb / divideby
        Label21.Text = valueA
        Label22.Text = valueB
        yvalue = (valueA - valueB)
        Label25.Text = yvalue

        'NOW WE DO THE X-AXIS LEFT/RIGHT DIFFERENCE......................:

        sum = 0
        a = 0
        ' Dim FILE_NAME As String = "C:\numbs.txt"

        For y As Integer = ((h / 2) - halfwidthofarea) To ((h / 2) + halfwidthofarea) - 1
            offset = y * w
            For x As Integer = ((w / 2) - halfwidthofarea * 3) To (w / 2) - 1
                a = rgbValues((x) + offset)
                'b = rgbValues((x) + 1 + offset)
                'c = rgbValues((x) + (w) + offset)
                sum += a
                ' Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                ' objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
                'objWriter.Close()
            Next
        Next
        sumb = 0
        a = 0
        For y As Integer = ((h / 2) - halfwidthofarea) To ((h / 2) + halfwidthofarea) - 1
            offset = y * w
            For x As Integer = (w / 2) To ((w / 2) + halfwidthofarea * 3) - 1
                a = rgbValues((x) + offset)
                'b = rgbValues((x) + 1 + offset)
                'c = rgbValues((x) + (w) + offset)
                sumb += a
                'Label15.Text = Label15.Text & vbCr & bitmap.GetPixel(x, y).ToString & vbCr & rgbValues((x * 3) + offset)
                'Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                'objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
                'objWriter.Close()
            Next
        Next
        valueA = sum / divideby
        valueB = sumb / divideby
        Label23.Text = valueA
        Label24.Text = valueB
        xvalue = (valueA - valueB)
        Label26.Text = xvalue
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
        GroupBox12.Refresh()
    End Sub



    Private Sub Button42_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button42.Click
        CenteringAlgorithm2()
    End Sub

    Sub CenteringAlgorithm2()
        Try
            GrabImage()
        Catch ex As Exception
            stopThread = True
            lightsOFF()
            MessageBox.Show("turning live off")
        End Try

        Refresh()
        newimage = bitmap


        ''''''''ME. IF YOU DON'T KNOW THE NUMBER OF BYTES OF THE IMAGE:
        'Dim rect As New Rectangle(0, 0, newImage.Width, newImage.Height)
        rect.X = 0
        rect.Y = 0
        rect.Width = newimage.Width
        rect.Height = newimage.Height



        ' Dim newImageData As System.Drawing.Imaging.BitmapData = newImage.LockBits(rect, _
        'Drawing.Imaging.ImageLockMode.ReadWrite, newImage.PixelFormat)
        newImageData = newimage.LockBits(rect, _
          Drawing.Imaging.ImageLockMode.ReadWrite, newimage.PixelFormat)
        'MessageBox.Show(newimage.PixelFormat.ToString)
        ' Get the address of the first line.
        ptr = newImageData.Scan0
        'Dim ptr As IntPtr = newImageData.Scan0

        ' Declare an array to hold the bytes of the bitmap.
        ' This code is specific to a bitmap with 24 bits per pixels.

        ' Dim bytes As Integer = Math.Abs(newImageData.Stride) * newImage.Height
        bytes = Math.Abs(newImageData.Stride) * newimage.Height


        'Dim rgbValues(bytes - 1) As Byte
        'Array.Resize(rgbValues, bytes) '5053440
        Array.Resize(rgbValues, 5053440) '5053440

        'Dim rgbValues(3686400 - 1) As Byte

        ' Copy the RGB values into the array.
        'Try
        'MessageBox.Show(newImageData.Width.ToString & " " & rgbValues.Length & " " & bytes.ToString)

        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)

        ' Catch ex As Exception
        'MessageBox.Show(ex.Message)
        '  End Try
        'System.Runtime.InteropServices.Marshal.Copy(flycapRGBImage.pData, rgbValues, 0, 3686400)

        ' Unlock the bits.
        newimage.UnlockBits(newImageData)


        '''''''''ME. IF YOU DO KNOW THE NUMBER OF BYTES OF THE IMAGE:
        ''Dim rect As New Rectangle(0, 0, newImage.Width, newImage.Height)
        ''Dim newImageData As System.Drawing.Imaging.BitmapData = newImage.LockBits(rect, _
        ''   Drawing.Imaging.ImageLockMode.ReadWrite, newImage.PixelFormat)

        '' Get the address of the first line.
        ''Dim ptr As IntPtr = newImageData.Scan0

        '' Declare an array to hold the bytes of the bitmap.
        '' This code is specific to a bitmap with 24 bits per pixels.

        ''Dim bytes As Integer = Math.Abs(newImageData.Stride) * newImage.Height
        ''Dim rgbValues(bytes - 1) As Byte
        'Dim rgbValues(3686400 - 1) As Byte

        '' Copy the RGB values into the array.
        ''System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)
        'System.Runtime.InteropServices.Marshal.Copy(flycapRGBImage.pData, rgbValues, 0, 3686400)

        '' Unlock the bits.
        ''newImage.UnlockBits(newImageData)
        ''''''''''''''''

        w = newimage.Width
        h = newimage.Height
        w = w * 3 'because this is a 24bit per pixel image, which is 3 numbers
        'Try
        sum = 0
        a = 0
        FILE_NAME = drive & "\numbs.txt"
        Dim halfwidthofarea As Integer = h / 2
        Dim divideby As Integer = 500
        For y As Integer = ((h / 2) - halfwidthofarea) To (h / 2) - 1
            offset = y * w
            For x As Integer = ((w / 2) - halfwidthofarea * 3) To ((w / 2) + halfwidthofarea * 3 - 1)
                a = rgbValues((x) + offset)
                b = rgbValues((x) + 1 + offset)
                c = rgbValues((x) + (w) + offset)
                sum += Math.Abs(b - a)
                sum += Math.Abs(c - a)
                ' Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                ' objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
                'objWriter.Close()
            Next
        Next
        sumb = 0
        a = 0
        For y As Integer = (h / 2) To ((h / 2) + halfwidthofarea) - 1
            offset = y * w
            For x As Integer = ((w / 2) - halfwidthofarea * 3) To ((w / 2) + halfwidthofarea * 3 - 1)
                a = rgbValues((x) + offset)
                b = rgbValues((x) + 1 + offset)
                c = rgbValues((x) + (w) + offset)
                sumb += Math.Abs(b - a)
                sumb += Math.Abs(c - a)
                'Label15.Text = Label15.Text & vbCr & bitmap.GetPixel(x, y).ToString & vbCr & rgbValues((x * 3) + offset)
                'Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                'objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
                'objWriter.Close()
            Next
        Next
        valueA = sum / divideby
        valueB = sumb / divideby
        Label21.Text = valueA
        Label22.Text = valueB
        yvalue = (valueA - valueB)
        Label25.Text = yvalue

        'NOW WE DO THE X-VALUE......................:

        sum = 0
        a = 0
        ' Dim FILE_NAME As String = "C:\numbs.txt"

        For y As Integer = ((h / 2) - halfwidthofarea) To ((h / 2) + halfwidthofarea) - 1
            offset = y * w
            For x As Integer = ((w / 2) - halfwidthofarea * 3) To (w / 2) - 1
                a = rgbValues((x) + offset)
                b = rgbValues((x) + 1 + offset)
                c = rgbValues((x) + (w) + offset)
                sum += Math.Abs(b - a)
                sum += Math.Abs(c - a)
                ' Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                ' objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
                'objWriter.Close()
            Next
        Next
        sumb = 0
        a = 0
        For y As Integer = ((h / 2) - halfwidthofarea) To ((h / 2) + halfwidthofarea) - 1
            offset = y * w
            For x As Integer = (w / 2) To ((w / 2) + halfwidthofarea * 3) - 1
                a = rgbValues((x) + offset)
                b = rgbValues((x) + 1 + offset)
                c = rgbValues((x) + (w) + offset)
                sumb += Math.Abs(b - a)
                sumb += Math.Abs(c - a)
                'Label15.Text = Label15.Text & vbCr & bitmap.GetPixel(x, y).ToString & vbCr & rgbValues((x * 3) + offset)
                'Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
                'objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
                'objWriter.Close()
            Next
        Next
        valueA = sum / divideby
        valueB = sumb / divideby
        Label23.Text = valueA
        Label24.Text = valueB
        xvalue = (valueA - valueB)
        Label26.Text = xvalue
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    'Function CenteringAlgorithm2x()
    '    Try
    '        GrabImage()
    '    Catch ex As Exception
    '        stopThread = True
    '        lightsOFF()
    '        MessageBox.Show("turning live off")
    '    End Try

    '    Refresh()
    '    newimage = bitmap


    '    ''''''''ME. IF YOU DON'T KNOW THE NUMBER OF BYTES OF THE IMAGE:
    '    'Dim rect As New Rectangle(0, 0, newImage.Width, newImage.Height)
    '    rect.X = 0
    '    rect.Y = 0
    '    rect.Width = newimage.Width
    '    rect.Height = newimage.Height



    '    ' Dim newImageData As System.Drawing.Imaging.BitmapData = newImage.LockBits(rect, _
    '    'Drawing.Imaging.ImageLockMode.ReadWrite, newImage.PixelFormat)
    '    newImageData = newimage.LockBits(rect, _
    '      Drawing.Imaging.ImageLockMode.ReadWrite, newimage.PixelFormat)
    '    'MessageBox.Show(newimage.PixelFormat.ToString)
    '    ' Get the address of the first line.
    '    ptr = newImageData.Scan0
    '    'Dim ptr As IntPtr = newImageData.Scan0

    '    ' Declare an array to hold the bytes of the bitmap.
    '    ' This code is specific to a bitmap with 24 bits per pixels.

    '    ' Dim bytes As Integer = Math.Abs(newImageData.Stride) * newImage.Height
    '    bytes = Math.Abs(newImageData.Stride) * newimage.Height


    '    'Dim rgbValues(bytes - 1) As Byte
    '    'Array.Resize(rgbValues, bytes) '5053440
    '    Array.Resize(rgbValues, 5053440) '5053440

    '    'Dim rgbValues(3686400 - 1) As Byte

    '    ' Copy the RGB values into the array.
    '    'Try
    '    'MessageBox.Show(newImageData.Width.ToString & " " & rgbValues.Length & " " & bytes.ToString)

    '    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)

    '    ' Catch ex As Exception
    '    'MessageBox.Show(ex.Message)
    '    '  End Try
    '    'System.Runtime.InteropServices.Marshal.Copy(flycapRGBImage.pData, rgbValues, 0, 3686400)

    '    ' Unlock the bits.
    '    newimage.UnlockBits(newImageData)


    '    '''''''''ME. IF YOU DO KNOW THE NUMBER OF BYTES OF THE IMAGE:
    '    ''Dim rect As New Rectangle(0, 0, newImage.Width, newImage.Height)
    '    ''Dim newImageData As System.Drawing.Imaging.BitmapData = newImage.LockBits(rect, _
    '    ''   Drawing.Imaging.ImageLockMode.ReadWrite, newImage.PixelFormat)

    '    '' Get the address of the first line.
    '    ''Dim ptr As IntPtr = newImageData.Scan0

    '    '' Declare an array to hold the bytes of the bitmap.
    '    '' This code is specific to a bitmap with 24 bits per pixels.

    '    ''Dim bytes As Integer = Math.Abs(newImageData.Stride) * newImage.Height
    '    ''Dim rgbValues(bytes - 1) As Byte
    '    'Dim rgbValues(3686400 - 1) As Byte

    '    '' Copy the RGB values into the array.
    '    ''System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)
    '    'System.Runtime.InteropServices.Marshal.Copy(flycapRGBImage.pData, rgbValues, 0, 3686400)

    '    '' Unlock the bits.
    '    ''newImage.UnlockBits(newImageData)
    '    ''''''''''''''''

    '    w = newimage.Width
    '    h = newimage.Height
    '    Try
    '        sum = 0
    '        a = 0
    '        Dim FILE_NAME As String = "C:\numbs.txt"

    '        For y As Integer = 0 To (h - 1)
    '            offset = y * w * 3
    '            For x As Integer = w * 3 / 3 To (((w * 3 / 3) + (w * 3 / 6)))
    '                a = rgbValues((x * 3) + offset)
    '                b = rgbValues((x * 3) + 1 + offset)
    '                c = rgbValues((x * 3) + (w * 3) + offset)
    '                sum += Math.Abs(b - a)
    '                sum += Math.Abs(c - a)
    '                ' Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
    '                ' objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
    '                'objWriter.Close()
    '            Next
    '        Next
    '        sumb = 0
    '        a = 0
    '        For y As Integer = 0 To (h - 1)
    '            offset = y * w * 3
    '            For x As Integer = (((w * 3 / 3) + (w * 3 / 6))) + 1 To (2 * w * 3 / 3)
    '                a = rgbValues((x * 3) + offset)
    '                b = rgbValues((x * 3) + 1 + offset)
    '                c = rgbValues((x * 3) + (w * 3) + offset)
    '                sumb += Math.Abs(b - a)
    '                sumb += Math.Abs(c - a)
    '                'Label15.Text = Label15.Text & vbCr & bitmap.GetPixel(x, y).ToString & vbCr & rgbValues((x * 3) + offset)
    '                'Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
    '                'objWriter.WriteLine(a & "  " & bitmap.GetPixel(x, y).ToString)
    '                'objWriter.Close()
    '            Next
    '        Next
    '        valueA = sum / 5000
    '        valueB = sumb / 5000
    '        Label23.Text = valueA
    '        Label24.Text = valueB
    '        Return (valueA - valueB)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Return 0
    '    End Try
    'End Function




    'Sub runAlgorithm()
    '    length = lengthlow
    '    CenteringAlgorithm()
    '    Dim wait As Integer = 100
    '    While (yvalue < -limit) Or (yvalue > limit) Or (xvalue < -limit) Or (xvalue > limit)
    '        If yvalue < -limit Then
    '            MclUp()
    '            'Threading.Thread.Sleep(wait)
    '        End If
    '        If yvalue > limit Then
    '            MclDown()
    '            'Threading.Thread.Sleep(wait)
    '        End If
    '        If xvalue < -limit Then
    '            'MclRight()
    '            MclLeft()
    '            ' Threading.Thread.Sleep(wait)
    '        End If
    '        If xvalue > limit Then
    '            'MclLeft()
    '            MclRight()
    '            ' Threading.Thread.Sleep(wait)
    '        End If
    '        CenteringAlgorithm()
    '    End While
    'End Sub





    Private Sub Button46_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button46.Click
        proc()
        Timer6.Start()
    End Sub

    Private Sub Timer6_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer6.Tick
        proc()
    End Sub

    Private Sub Button47_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button47.Click
        Timer6.Stop()
    End Sub

    Sub proc()
        ' runAlgorithm()
        GrabandSave()
        Threading.Thread.Sleep(1500)
        Str = "U" & Chr(7) & "r" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(0) & "-3000" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(1) & "-3000" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(80) & Chr(13)
        SerialPort1.Write(Str)
        Threading.Thread.Sleep(2500)
        GrabandSave()
        Threading.Thread.Sleep(1000)

        Str = "U" & Chr(7) & "r" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(0) & "-1000" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(1) & "-1000" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(80) & Chr(13)
        SerialPort1.Write(Str)
        Threading.Thread.Sleep(2000)
        GrabandSave()
        gotoZero()
        Threading.Thread.Sleep(2500)
        GrabImage()
        calibrateMCL()
    End Sub

    Private Sub Button48_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button48.Click
        'PictureBox1.ImageLocation = drive & "\bullseye.jpg"
        If Label18.Visible = False Then
            Label18.Show()
        Else
            Label18.Hide()
        End If

        Refresh()
        'Threading.Thread.Sleep(1000)
        'GrabImage()
        'Refresh()
    End Sub

    Dim WaitEvent As AutoResetEvent
    Dim thread1 As Thread
    Private Sub Button41_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button41.Click 'test

        For j As Integer = 1 To 3
            Str = "RM X=-"
            Str += smallstep.ToString
            Str += ControlChars.Cr
            SerialPort1.DiscardInBuffer()
            SerialPort1.Write(Str)
            addtomyConsole(Str)

            QueryFinishedMovementThread() 'this starts a new thread that waits for the stage to respond when it has finished moving.
            WaitEvent.WaitOne()  ' this waits for the previous thread to end.
            'writetoTextFile(j)
            ' writetoTextFile(Str)
        Next
    End Sub


    Sub StartSleepingThread()
        thread1 = New Thread(AddressOf sleepingThread)
        thread1.Start()
    End Sub

    Sub sleepingThread()
        Thread.Sleep(5000)
        WaitEvent.Set()
    End Sub

    Sub QueryFinishedMovementThread()
        addtomyConsole("moving........")
        thread1 = New Thread(AddressOf querythread)
        thread1.Start()
    End Sub

    Sub QueryFinishedMovementThreadMS2000()
        addtomyConsole("moving........")
        thread1 = New Thread(AddressOf querythreadMS2000)
        thread1.Start()
    End Sub

    Sub querythread()

        Thread.Sleep(100)
        mytext = ""
        While mytext.Contains("A") = False
            mytext = SerialPort1.ReadExisting
            addtomyConsole(mytext)
            Thread.Sleep(100)
        End While
        addtomyConsole("done moving")
        WaitEvent.Set()
        ' thread1 = Nothing
    End Sub

    Sub QueryStoppedMotor()
        thread1 = New Thread(AddressOf queryMotor)
        thread1.Start()
    End Sub

    Sub queryMotor()
        'Thread.Sleep(300)
        While motorIsStopped = False
            'mytext = SerialPort1.ReadExisting
            addtomyConsole("querying motor..")
            Thread.Sleep(700)
        End While
        addtomyConsole("confirmed stopped")

        WaitEvent.Set()
        ' thread1 = Nothing
    End Sub



    Sub querythreadMS2000()

        Thread.Sleep(100)
        mytext = ""
        While mytext.Contains("N") = False
            Str = "STATUS"
            Str += ControlChars.Cr
            SerialPort1.Write(Str)
            mytext = SerialPort1.ReadExisting
            Console.WriteLine(mytext)
            addtomyConsole(mytext)
            Thread.Sleep(100)
        End While
        addtomyConsole("done moving")
        WaitEvent.Set()

    End Sub

    Sub QueryFinishedMovementThreadDC()
        addtomyConsole("moving........")
        thread1 = New Thread(AddressOf querythreadDC)
        thread1.Start()
    End Sub

    Sub querythreadDC()
        While mytext.Contains("S") = False
            mytext = SerialPort1.ReadExisting
            addtomyConsole(mytext)
            Thread.Sleep(100)
        End While
        addtomyConsole("done moving")
        WaitEvent.Set()
    End Sub


    Sub QueryFinishedMovementThreadMCL()
        addtomyConsole("moving........")
        thread1 = New Thread(AddressOf querythreadMCL)
        thread1.Start()
    End Sub

    Sub querythreadMCL()

        Thread.Sleep(100)
        mytext = ""
        Dim num As Integer = 0
        While mytext.Contains("@") = False And num < 150  'only 15 seconds!!!  change this!
            mytext = SerialPort1.ReadExisting
            addtomyConsole(mytext)
            Thread.Sleep(100)
            num = num + 1
        End While
        If num > 145 Then
            Label57.Text = "You did not leave enought time for mcl stage to finish it's movement (currently only 15 secs)!"
            Label57.Show()
        End If
        addtomyConsole("done moving")
        WaitEvent.Set()
        ' thread1 = Nothing
    End Sub

    Private Sub Button39_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button39.Click
        alignX()
        alignY()
    End Sub

    Dim limit As Integer
    Dim largestep As Integer
    Dim smallstep As Integer
    Dim tinystep As Integer

    Private Sub Button51_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button51.Click  'X-ALIGNMENT BUTTON
        alignX()
    End Sub

    Dim num As Integer


    Dim initialX As String
    Dim initialY As String

    Sub alignX()
        initialX = getXpos()

        addtomyConsole("setting up step values")
        If RadioButton20.Checked Then  'ms4new radiobutton
            largestep = CInt(My.Settings.largestepMS4)
            smallstep = CInt(My.Settings.smallstepMS4)
            tinystep = CInt(My.Settings.tinystepMS4)
        ElseIf RadioButton24.Checked Then  'mcl radiobutton
            largestep = CInt(My.Settings.largestepMCL)
            smallstep = CInt(My.Settings.smallstepMCL)
            tinystep = CInt(My.Settings.tinystepMCL)
        End If
        addtomyConsole("finished setting up step values")
        writetoTextFileX("*****")
        RadioButton9.Checked = True  'this turns on darkfield
        limit = CInt(TextBox18.Text)
        'length = lengthlow
        CenteringAlgorithm()
        'Dim wait As Integer = 100

        'large-step approximation:
        addtomyConsoleMain("******************************")
        addtomyConsoleMain("X-AXIS...")
        addtomyConsoleMain("NOW FIRST APPROXIMATION......")
        mytext = getXpos()
        addtomyConsoleMain("now at " & mytext)
        writetoTextFileX(mytext)
        num = 0
        If (xvalue < -limit) Then
            addtomyConsoleMain("Moving object to the left")

            While (xvalue < -limit)
                MRight(largestep.ToString)
                CenteringAlgorithm()
                num = num + 1
            End While
            addtomyConsoleMain(num.ToString & " times")
            mytext = getXpos()
            addtomyConsoleMain("now at " & mytext)
            writetoTextFileX(mytext)
            num = 0
        ElseIf (xvalue > limit) Then
            addtomyConsoleMain("Moving object to the right")
            While (xvalue > limit)
                MLeft(largestep.ToString)
                CenteringAlgorithm()
                num = num + 1
            End While
            addtomyConsoleMain(num.ToString & " times")
            mytext = getXpos()
            addtomyConsoleMain("now at " & mytext)
            writetoTextFileX(mytext)
        End If
        addtomyConsoleMain("DONE FIRST APPROXIMATION X......")


        ''medium-step approximation:
        'addtomyConsole("NOW 2ND APPROXIMATION......")
        'If (xvalue < -limit) Then
        '    While (xvalue < -limit)
        '        Str = "RM X=-"
        '        Str += mediumstep.ToString
        '        Str += ControlChars.Cr
        '        SerialPort1.Write(Str)
        '        addtomyConsole(Str)
        '        QueryFinishedMovementThread() 'this starts a new thread that waits for the stage to respond when it has finished moving.
        '        WaitEvent.WaitOne()  ' this waits for the previous thread to end.
        '        CenteringAlgorithm()
        '    End While
        'ElseIf (xvalue > limit) Then
        '    While (xvalue > limit)
        '        Str = "RM X="
        '        Str += mediumstep.ToString
        '        Str += ControlChars.Cr
        '        SerialPort1.Write(Str)
        '        addtomyConsole(Str)
        '        QueryFinishedMovementThread() 'this starts a new thread that waits for the stage to respond when it has finished moving.
        '        WaitEvent.WaitOne()  ' this waits for the previous thread to end.
        '        CenteringAlgorithm()
        '    End While
        'End If
        'addtomyConsole("DONE 2ND APPROXIMATION......")

        addtomyConsoleMain("NOW BACKLASH CORRECTION X......")
        '

        'now move it left by one field width and right by half a field width (to remove backlash)'''''''''''''''''''''''''''''''''''
        addtomyConsoleMain("Moving object to the right")
        MLeft(mywidth.ToString)
        addtomyConsole("DONE 1ST HALF OF BACKLASH CORRECTION X.....")
        GrabImage()
        addtomyConsoleMain("Moving object to the left")
        MRight((Math.Round((mywidth * 39) / 40)).ToString)  ' I want it to come back a  more than 1/2 the width but less than 1.
        GrabImage()
        addtomyConsoleMain("DONE BACKLASH CORRECTION X.....")
        mytext = getXpos()
        addtomyConsoleMain("now at " & mytext)
        writetoTextFileX(mytext)

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'now fine-step approximation only to the right:
        CenteringAlgorithm()
        addtomyConsoleMain("NOW FINE-STEPPING X...")
        num = 0
        If (xvalue < -limit) Then
            addtomyConsoleMain("Moving object to the left")
            While (xvalue < -limit)

                mytext = getXpos()
                addtomyConsoleMain(mytext)
                If Math.Abs(CInt(mytext) - CInt(initialX)) >= 70 Then
                    MRight(largestep.ToString)
                    CenteringAlgorithm()
                    num = num + 1
                End If

                mytext = getXpos()
                addtomyConsoleMain(mytext)

                If Math.Abs(CInt(mytext) - CInt(initialX)) < 70 Then   'if it's very close to zero then slow
                    While (xvalue < -limit) And Math.Abs(CInt(mytext) - CInt(initialX)) > 8
                        MRight(smallstep.ToString)
                        CenteringAlgorithm()
                        num = num + 1
                        mytext = getXpos()
                    End While
                End If

                If Math.Abs(CInt(mytext) - CInt(initialX)) < 9 Then   'if it's very close to zero then go step by step
                    While (xvalue < -limit)
                        MRight(tinystep.ToString)
                        CenteringAlgorithm()
                        num = num + 1
                    End While
                End If

            End While
            addtomyConsoleMain(num.ToString & " times")
            mytext = getXpos()
            addtomyConsoleMain("now at " & mytext)
            writetoTextFileX(mytext)

        End If
        addtomyConsoleMain("DONE FINE-STEPPING X...")
        CenteringAlgorithm()
    End Sub

    Sub writetoTextFile(ByVal str As String)
        Using writer As StreamWriter = New StreamWriter("MYDATA.txt", True)
            'writer.Write("One ")
            ' writer.WriteLine("two 2")
            writer.WriteLine(str)
        End Using
    End Sub
    Sub writetoTextFileX(ByVal str As String)
        Using writer As StreamWriter = New StreamWriter("MYDATA_X.txt", True)
            'writer.Write("One ")
            ' writer.WriteLine("two 2")
            writer.WriteLine(str)
        End Using
    End Sub
    Sub writetoTextFileY(ByVal str As String)
        Using writer As StreamWriter = New StreamWriter("MYDATA_Y.txt", True)
            'writer.Write("One ")
            ' writer.WriteLine("two 2")
            writer.WriteLine(str)
        End Using
    End Sub


    Function getXpos()
        If RadioButton20.Checked Then  'ms4new radiobutton
            SerialPort1.DiscardInBuffer()
            Str = "w x"
            Str += ControlChars.Cr
            SerialPort1.Write(Str)
            Threading.Thread.Sleep(50)
            mychar = {":", "A"}
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)
            ' addtomyConsoleMain("now at " & mytext)
            'writetoTextFileX(mytext)

        ElseIf RadioButton24.Checked Then  'mcl radiobutton
            Str = "U" & Chr(67) & Chr(13)
            SerialPort1.DiscardInBuffer()
            SerialPort1.Write(Str)
            Threading.Thread.Sleep(50)
            mytext = SerialPort1.ReadExisting
            'addtomyConsole(mytext)
        End If

        Return mytext


    End Function

    Function getYpos()
        If RadioButton20.Checked Then  'ms4new radiobutton
            SerialPort1.DiscardInBuffer()
            Str = "w y"
            Str += ControlChars.Cr
            SerialPort1.Write(Str)
            Threading.Thread.Sleep(50)
            mychar = {":", "A"}
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)
            ' addtomyConsoleMain("now at " & mytext)
            'writetoTextFileY(mytext)

        ElseIf RadioButton24.Checked Then  'mcl radiobutton
            Str = "U" & Chr(68) & Chr(13)
            SerialPort1.DiscardInBuffer()
            SerialPort1.Write(Str)
            Threading.Thread.Sleep(50)
            mytext = SerialPort1.ReadExisting
            ' addtomyConsole(mytext)
        End If

        Return mytext



    End Function


    Private Sub Button52_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button52.Click  'Y-alignment
        alignY()
    End Sub

    Sub alignY()
        initialY = getYpos()
        If RadioButton20.Checked Then  'ms4new radiobutton
            largestep = CInt(My.Settings.largestepMS4)
            smallstep = CInt(My.Settings.smallstepMS4)
            tinystep = CInt(My.Settings.tinystepMS4)
        ElseIf RadioButton24.Checked Then  'mcl radiobutton
            largestep = CInt(My.Settings.largestepMCL)
            smallstep = CInt(My.Settings.smallstepMCL)
            tinystep = CInt(My.Settings.tinystepMCL)
        End If

        writetoTextFileY("*****")
        RadioButton9.Checked = True  'this turns on darkfield
        limit = CInt(TextBox18.Text)
        'length = lengthlow
        CenteringAlgorithm()
        'Dim wait As Integer = 100

        'large-step approximation:
        addtomyConsoleMain("******************************")
        addtomyConsoleMain("Y-AXIS...")
        addtomyConsoleMain("NOW FIRST APPROXIMATION......")
        mytext = getYpos()
        addtomyConsoleMain("now at " & mytext)
        writetoTextFileY(mytext)
        num = 0
        If (yvalue < -limit) Then
            addtomyConsoleMain("Moving object up")
            While (yvalue < -limit)
                MUp(largestep.ToString)
                CenteringAlgorithm()
                num = num + 1
            End While
            addtomyConsoleMain(num.ToString & " times")
            mytext = getYpos()
            addtomyConsoleMain("now at " & mytext)
            writetoTextFileY(mytext)
            num = 0
        ElseIf (yvalue > limit) Then
            addtomyConsoleMain("Moving object down")
            While (yvalue > limit)
                MDown(largestep.ToString)
                CenteringAlgorithm()
                num = num + 1
            End While
            addtomyConsoleMain(num.ToString & " times")
            mytext = getYpos()
            addtomyConsoleMain("now at " & mytext)
            writetoTextFileY(mytext)
        End If
        addtomyConsoleMain("DONE FIRST APPROXIMATION Y......")

        addtomyConsoleMain("NOW BACKLASH CORRECTION Y......")
        'now move it left by one field width and right by half a field width (to remove backlash)'''''''''''''''''''''''''''''''''''
        addtomyConsoleMain("Moving object down")
        MDown(mywidth.ToString)
        addtomyConsole("DONE 1ST HALF OF BACKLASH CORRECTION Y.....")
        GrabImage()
        addtomyConsoleMain("Moving object up")
        MUp((Math.Round((mywidth * 39) / 40)).ToString) ' I want it to come back a  more than 1/2 the width but less than 1.
        GrabImage()
        addtomyConsoleMain("DONE BACKLASH CORRECTION Y.....")
        mytext = getYpos()
        addtomyConsoleMain("now at " & mytext)
        writetoTextFileY(mytext)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'now fine-step approximation only to the right:
        CenteringAlgorithm()
        addtomyConsoleMain("NOW FINE-STEPPING Y...")
        num = 0
        If (yvalue < -limit) Then
            addtomyConsoleMain("Moving object up")
            While (yvalue < -limit)

                mytext = getYpos()
                addtomyConsoleMain(mytext)
                If Math.Abs(CInt(mytext) - CInt(initialY)) >= 70 Then
                    MUp(largestep)
                    CenteringAlgorithm()
                    num = num + 1
                End If

                mytext = getYpos()
                addtomyConsoleMain(mytext)
                If Math.Abs(CInt(mytext) - CInt(initialY)) < 70 Then
                    While (yvalue < -limit) And Math.Abs(CInt(mytext) - CInt(initialY)) > 8
                        MUp(smallstep.ToString)
                        CenteringAlgorithm()
                        num = num + 1

                        mytext = getYpos()
                    End While
                End If

                If Math.Abs(CInt(mytext) - CInt(initialY)) < 9 Then   'if it's very close to zero then go step by step
                    While (yvalue < -limit)
                        MUp(tinystep.ToString)
                        CenteringAlgorithm()
                        num = num + 1

                    End While
                End If

            End While
            addtomyConsoleMain(num.ToString & " times")
            mytext = getYpos()
            addtomyConsoleMain("now at " & mytext)
            writetoTextFileY(mytext)

        End If
        addtomyConsoleMain("DONE FINE-STEPPING Y...")
        CenteringAlgorithm()
    End Sub

    'Sub alignY() 'Old
    '    RadioButton9.Checked = True  'this turns on darkfield
    '    limit = CInt(TextBox18.Text)
    '    'length = lengthlow
    '    CenteringAlgorithm()
    '    'Dim wait As Integer = 100

    '    'medium-step approYimation:
    '    addtomyConsole("now first approY...")
    '    If (yvalue < -limit) Then
    '        While (yvalue < -limit)
    '            Str = "RM Y=-"
    '            Str += mediumstep.ToString
    '            Str += ControlChars.Cr
    '            SerialPort1.Write(Str)
    '            addtomyConsole(Str)
    '            Threading.Thread.Sleep(500)
    '            CenteringAlgorithm()
    '        End While
    '    ElseIf (yvalue > limit) Then
    '        While (yvalue > limit)
    '            Str = "RM Y="
    '            Str += mediumstep.ToString
    '            Str += ControlChars.Cr
    '            SerialPort1.Write(Str)

    '            Threading.Thread.Sleep(500)
    '            CenteringAlgorithm()
    '        End While
    '    End If
    '    addtomyConsole("done first approY")
    '    addtomyConsole("now backlash correction...")
    '    'now move it left by one field width and right by half a field width (to remove backlash)'''''''''''''''''''''''''''''''''''
    '    Str = "RM Y=-"
    '    Str += mywidth.ToString
    '    Str += ControlChars.Cr
    '    SerialPort1.Write(Str)
    '    Threading.Thread.Sleep(700)

    '    GrabImage()
    '    Str = "RM Y="
    '    Str += (Math.Round((mywidth * 9) / 10)).ToString ' I want it to come back a litle more tha 1/2 which is 4/6.
    '    Str += ControlChars.Cr

    '    SerialPort1.Write(Str)
    '    Threading.Thread.Sleep(600)
    '    GrabImage()
    '    addtomyConsole("done backlash correction")
    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '    'now fine-step approYimation only to the right:
    '    CenteringAlgorithm()
    '    addtomyConsole("now fine-stepping..")
    '    If (yvalue > limit) Then
    '        While (yvalue > limit)
    '            'MsgBoY("moving right")
    '            Str = "RM Y="
    '            Str += smallstep.ToString
    '            Str += ControlChars.Cr
    '            SerialPort1.Write(Str)
    '            Threading.Thread.Sleep(400)
    '            CenteringAlgorithm()
    '        End While
    '        'ElseIf (Yvalue > limit) Then
    '        '    While (Yvalue > limit)
    '        '        Str = "RM Y="
    '        '        Str += smallstep.ToString
    '        '        Str += ControlChars.Cr
    '        '        SerialPort1.Write(Str)
    '        '        QueryFinishedMovementB()
    '        '        'Threading.Thread.Sleep(500)
    '        '        CenteringAlgorithm()
    '        '    End While
    '    End If
    '    addtomyConsole("done fine-stepping")

    'End Sub


    Private Sub Button53_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button53.Click
        setXzero()
    End Sub
    Sub setXzero()
        Str = "U" & Chr(3) & "0" & Chr(13)
        SerialPort1.Write(Str)
        'Str = "U" & Chr(80) & Chr(13)
        'SerialPort1.Write(Str)
    End Sub
    Private Sub Button54_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button54.Click
        setYzero()
    End Sub
    Sub setYzero()
        Str = "U" & Chr(4) & "0" & Chr(13)
        SerialPort1.Write(Str)
        'Str = "U" & Chr(80) & Chr(13)
        'SerialPort1.Write(Str)
    End Sub

    Private Sub Button60_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button60.Click
        calibrateMS2000()
    End Sub

    Sub calibrateMS2000()
        Dim separation As String = "1100"
        Dim length1 As String = "300"
        Dim length2 As String = "50"
        Dim wait2 As Integer = 600
        ' length = lengthlow
        SetStageSpeeds()
        If versionMS2000 Then
            Str = "R X=-" & separation & " Y=" & separation
        Else
            Str = "RM X=-" & separation & " Y=" & separation
        End If
        Str += ControlChars.Cr
        SerialPort1.Write(Str)
        Threading.Thread.Sleep(2000)

        CenteringAlgorithm()
        While (yvalue > -3 * limit / 4)
            If yvalue > -limit Then
                'MclUp()
                If versionMS2000 Then
                    Str = "R Y=-" & length1
                Else
                    Str = "RM Y=-" & length1
                End If
                Str += ControlChars.Cr
                SerialPort1.Write(Str)
                Threading.Thread.Sleep(wait2)
            End If
            CenteringAlgorithm()
        End While
        length = 2
        While (yvalue > -limit)
            If yvalue > -limit Then
                'MclUp()
                If versionMS2000 Then
                    Str = "R Y=-" & length2
                Else
                    Str = "RM Y=-" & length2
                End If
                Str += ControlChars.Cr
                SerialPort1.Write(Str)
                Threading.Thread.Sleep(wait2)
            End If
            CenteringAlgorithm()
        End While

        'MOVE BACK:

        'length = lengthhi

        'MclDown()
        If versionMS2000 Then
            Str = "R Y=" & separation
        Else
            Str = "RM Y=" & separation
        End If
        Str += ControlChars.Cr
        SerialPort1.Write(Str)
        Threading.Thread.Sleep(1000)
        GrabImage()
        Refresh()
        Threading.Thread.Sleep(1000)

        'NOW X:
        length = lengthlow
        CenteringAlgorithm()
        While (xvalue > -1 * limit / 2)
            If xvalue > -limit Then
                'MclLeft()
                If versionMS2000 Then
                    Str = "R X=" & length1
                Else
                    Str = "RM X=" & length1
                End If
                Str += ControlChars.Cr
                SerialPort1.Write(Str)
                Threading.Thread.Sleep(wait2)
            End If
            CenteringAlgorithm()
        End While
        length = 2
        While (xvalue > -limit)
            If xvalue > -limit Then
                'MclLeft()
                If versionMS2000 Then
                    Str = "R X=" & length2
                Else
                    Str = "RM X=" & length2
                End If
                Str += ControlChars.Cr
                SerialPort1.Write(Str)
                Threading.Thread.Sleep(wait2)
            End If
            CenteringAlgorithm()
        End While

        'setXYzero()
        Threading.Thread.Sleep(wait2)
        Zero()
        Threading.Thread.Sleep(wait2)
        ' MessageBox.Show("done")

    End Sub


    Private Sub Button55_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button55.Click
        calibrateMCL()
    End Sub
    Sub calibrateMCL()
        length = lengthlow
        Str = "U" & Chr(7) & "r" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(0) & "-100" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(1) & "-100" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(80) & Chr(13)
        SerialPort1.Write(Str)
        Threading.Thread.Sleep(1000)


        CenteringAlgorithm()
        While (yvalue > -limit / 2)
            If yvalue > -limit Then
                ' MclUp()
            End If
            CenteringAlgorithm()
        End While
        length = 2
        While (yvalue > -limit)
            If yvalue > -limit Then
                'MclUp()
            End If
            CenteringAlgorithm()
        End While

        'MOVE BACK:
        length = lengthhi
        ' MclDown()
        Threading.Thread.Sleep(1000)
        GrabImage()
        Refresh()
        Threading.Thread.Sleep(1000)

        'NOW X:
        length = lengthlow
        CenteringAlgorithm()
        While (xvalue > -limit / 2)
            If xvalue > -limit Then
                ' MclLeft()
            End If
            CenteringAlgorithm()
        End While
        length = 2
        While (xvalue > -limit)
            If xvalue > -limit Then
                'MclLeft()
            End If
            CenteringAlgorithm()
        End While


        setXYzero()
    End Sub



    Private Sub Button58_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button58.Click
        gotoZero()
    End Sub

    Sub gotoZero()
        Str = "U" & Chr(7) & "r" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(0) & "0" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(1) & "0" & Chr(13)
        SerialPort1.Write(Str)
        Str = "U" & Chr(80) & Chr(13)
        SerialPort1.Write(Str)
        Threading.Thread.Sleep(1000)
        GrabImage()
    End Sub
    Private Sub Button59_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button59.Click
        setXYzero()
    End Sub
    Sub setXYzero()
        setYzero()
        Threading.Thread.Sleep(100)
        setXzero()
    End Sub

    Private Sub Button61_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sendtoINCUSCOPE("1")
    End Sub

    Private Sub Button62_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sendtoINCUSCOPE("3")
    End Sub

    Private Sub Button63_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button63.Click
        FormSkipPosition.Show()
    End Sub

    Private Sub Button64_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button64.Click
        ExtraExposure.Show()
    End Sub

    Private Sub PictureBox2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox2.Paint
        'drawPostions()
    End Sub





    Dim up As Boolean


    Dim runnumup As Integer = 1
    Dim runnumdown As Integer = 1










    'Sub makeChart()

    '    ''''''


    '    If up Then
    '        Send("4")

    '        Dim FILE_NAME As String = "C:\Programs\IncuScope\txt\runup" & runnumup & ".txt"  'creates a text file with the errrors.
    '        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
    '        objWriter.WriteLine(vb.ToString)
    '        objWriter.Close()

    '    Else
    '        Send("3")
    '        Dim FILE_NAME As String = "C:\Programs\IncuScope\txt\rundown" & runnumdown & ".txt"  'creates a text file with the errrors.
    '        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
    '        objWriter.WriteLine(vb.ToString)
    '        objWriter.Close()
    '    End If
    '    ''''''
    '    'after next:
    '    '''''

    '    If up Then
    '        runnumup = runnumup + 1

    '    Else
    '        runnumdown = runnumdown + 1

    '    End If

    '    'MsgBox(myArray(0) & ", " & myArray(1) & ", " & myArray(2))

    '    Array.Sort(myArraySorted)
    '    For i = 0 To n  'normalize to the minimum value of the array, which is the element 0 of myArraySorted:
    '        myArray(i) = myArray(i) - myArraySorted(0) + 1
    '    Next
    '    'MsgBox(myArray(0) & ", " & myArray(1) & ", " & myArray(2))


    '    Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()

    '    Dim DataPoint1 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0, myArray(0))
    '    Dim DataPoint2 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0R, myArray(1))
    '    Dim DataPoint3 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0R, myArray(2))
    '    Dim DataPoint4 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0R, myArray(3))
    '    Dim DataPoint5 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0R, myArray(4))
    '    Dim DataPoint6 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0R, myArray(5))
    '    Dim DataPoint7 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0R, myArray(6))
    '    Dim DataPoint8 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0R, myArray(7))
    '    Dim DataPoint9 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0R, myArray(8))
    '    Dim DataPoint10 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0R, myArray(9))
    '    Dim DataPoint11 As System.Windows.Forms.DataVisualization.Charting.DataPoint = New System.Windows.Forms.DataVisualization.Charting.DataPoint(0.0R, myArray(10))



    '    Series2.Points.Add(DataPoint1)
    '    Series2.Points.Add(DataPoint2)
    '    Series2.Points.Add(DataPoint3)
    '    Series2.Points.Add(DataPoint4)
    '    Series2.Points.Add(DataPoint5)
    '    Series2.Points.Add(DataPoint6)
    '    Series2.Points.Add(DataPoint7)
    '    Series2.Points.Add(DataPoint8)
    '    Series2.Points.Add(DataPoint9)
    '    Series2.Points.Add(DataPoint10)
    '    Series2.Points.Add(DataPoint11)


    '    Series2.ChartArea = "ChartArea1"
    '    Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
    '    Series2.BorderWidth = 5
    '    Series2.Legend = "Legend1"
    '    Series2.Name = "BarChart2"
    '    Me.Chart1.Series.Add(Series2)

    'End Sub

    Private Sub Button68_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button68.Click
        PictureBox1.Image = Image.FromFile(drive & "\Images\Now\Pos1_bf\03-31-2012_(15-47-12-PM)_Pos1_bf_5_1826.jpg")
    End Sub

    Private Sub Button61_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button61.Click
        doautofhere()
    End Sub

    Sub doautofhere()
        bitmap = PictureBox1.Image
        addtomyConsole(autofhere())
        'va = autof()
    End Sub

    Function autofhere()
        'GrabImage()
        'Refresh()
        'newimage = PictureBox1.Image
        newimage = bitmap
        rect.X = 0
        rect.Y = 0
        rect.Width = newimage.Width
        rect.Height = newimage.Height
        newImageData = newimage.LockBits(rect, _
        Drawing.Imaging.ImageLockMode.ReadWrite, newimage.PixelFormat)
        ptr = newImageData.Scan0
        bytes = Math.Abs(newImageData.Stride) * newimage.Height
        Array.Resize(rgbValues, 5053440) '5053440
        System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes)
        newimage.UnlockBits(newImageData)
        sum = 0
        w = newimage.Width
        h = newimage.Height
        Try
            For y As Integer = 0 To h 'h
                offset = y * w * 3
                For x As Integer = 0 To (w - 1)
                    a = rgbValues((x * 3) + offset)
                    b = rgbValues((x * 3) + 3 + offset)  ' I THINK 1 SHOULD BE 3 INSTEAD!!!!
                    c = rgbValues((x * 3) + (w * 3) + offset)
                    sum += (Math.Abs(b - a)) ^ 2
                    sum += (Math.Abs(c - a)) ^ 2
                Next
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        sum = sum / 10000
        Label8.Text = sum
        Return sum
    End Function
    Private Sub Button62_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button62.Click
        lightON()
    End Sub


    Private Sub ListBox1_DragEnter(ByVal sender As Object, ByVal e As  _
System.Windows.Forms.DragEventArgs) Handles ListBox1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    Private Sub ListBox1_DragDrop(ByVal sender As Object, ByVal e As  _
    System.Windows.Forms.DragEventArgs) Handles ListBox1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim MyFiles() As String
            Dim i As Integer

            ' Assign the files to an array.
            MyFiles = e.Data.GetData(DataFormats.FileDrop)
            ' Loop through the array and add the files to the list.
            For i = 0 To MyFiles.Length - 1
                ListBox1.Items.Add(MyFiles(i))
            Next
        End If
    End Sub


    Dim n As Integer = 7
    Dim myArray(n) As Integer
    Dim myArraySorted(n) As Integer
    Dim ntimerfortest As Integer = 1
    'Dim repeats As Integer 'is now in module
    Dim vari As Integer = 1

    Private Sub Button69_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button69.Click
        'series of focusing button
        repeats = 2
        'lightON()
        ' up = True
        focusingSeries()
        'Threading.Thread.Sleep(1000)
        'MsgBox("series done")
        'ntimerfortest = ntimerfortest + 1
        'up = False
        'focusingSeries()
        ' you must disconnect from hid!
    End Sub

    Private Sub Button66_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button66.Click
        'up
        'lightON()
        repeats = 1
        'up = True
        focusingSeries()

    End Sub

    Private Sub Button67_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button67.Click
        'down
        ' lightON()
        repeats = 1
        'up = False
        focusingSeries()

    End Sub

    Private Sub Button70_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button70.Click
        'do focus
        'callerisTest = True 
        repeats = 0
        'dofocus()
    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        stopThread = True
        lightsOFF()
        ntimerfortest = 1
        callerisTest = True
        'lightsOFF()
        'autofocusingButtonAlias()
        dofocus()
    End Sub

    Sub dofocus()
        'up = False 'down
        sendtoINCUMOTOR("k") '20um down
        'Threading.Thread.Sleep(300) '400
        'lightON()
        'up = True
        If callerisTest Then
            repeats = 8
        Else
            repeats = 1
        End If
        'so that focusingSeries() only goes up once.
        focusingSeries()
    End Sub



    Sub focusingSeriesNEW()
        GrabImage()
        Refresh()
        Thread.Sleep(500)
        vb = autofhere()
        Label8.Refresh()
        motorSub_UP()
        Thread.Sleep(500)
        GrabImage()
        Refresh()
        Thread.Sleep(500)
        vb2 = autofhere()
        Label8.Refresh()

        If vb > vb2 Then
            motorSub_DOWN()
            Thread.Sleep(500)

        Else
            motorSub_UP()
            Thread.Sleep(500)
            GrabImage()
            Refresh()
            Thread.Sleep(500)
            vb3 = autofhere()
            Label8.Refresh()
            If vb3 < vb2 Then
                motorSub_DOWN()
                'Thread.Sleep(500)

            End If
        End If



    End Sub



    Sub focusingSeries()
        'Me.Chart1.Series.Clear()
        RadioButton1.Checked = True 'STEP TYPE 5um.

        'makeChart()


        'ConnectToHID(Me)
        'Dim pHandle As Integer = hidGetHandle(VendorID, ProductID)
        'Dim NameStr As String = "incuMOtor"
        'Dim NameLength As Integer = NameStr.Length
        'Dim Name As Integer = hidGetProductName(pHandle, NameStr, NameLength)
        '

        If callerisTest Then
            FormTestFocus.Show()
        End If

        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        For Me.vari = 0 To n
            Threading.Thread.Sleep(100) '50

            'GrabImageForLoop_NoGUI()

            'GrabimageNO_GUI()
            RadioButton7.Checked = True
            GrabImage()

            'motorSubforbackground_UP()
            motorSub_UP()

            vb = autofhere() 'this now takes the image from the bitmap instead of from the picturebox
            'saveImageWithName(vb.ToString, vari)
            myArray(vari) = vb
            myArraySorted(vari) = vb
            BackgroundWorker1.ReportProgress(10)

            'Label8.Text = vb  

            'GUI PART:
            'Label10.Text = vari
            'Label10.Refresh()
            'PictureBox1.Image = bitmap   'this line is the GrabImage_GUI_part()
            'PictureBox1.Refresh()
            'Label8.Text = vb 'this was part of autofhere()
            'Label8.Refresh()

            ''motorsub part:
            'Label6.Hide()
            'Label7.Hide()
            'Label11.Hide()
            '
            'Refresh()
        Next
    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        'Label10.Text = vari
        PictureBox1.Image = bitmap   'this line is the GrabImage_GUI_part()
        Label8.Text = vb 'this was part of autofhere()
        'motorsub part:
        Label6.Hide()
        Label7.Show()
        Label11.Hide()
        '
        Refresh()


    End Sub

    Function goodorbad()
        If nmax = 4 Then
            previousnmax = 4
            Return Drawing.Color.Green
        Else

            If previousnmax = 4 Then
                MsgBox("Bad Location, select a new one..")
                repeats = 0 'stops the test series
                FormTestFocus.Close()
            End If
            Return Drawing.Color.Red
        End If
    End Function



    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        DisconnectFromHID() 'disconnect motor control in order to connect to lights
        lightsOFF()
        'now connect again for motor control:
        ConnectToHID(Me)
        ' myGetDeviceHandles()
        pName = hidGetProductName(pHandleIncumotor, NameStr, NameLength)
        If NameStr <> "INCUMOTOR" Then
            If testmode = False Then
                'MsgBox(NameStr & "  You did not connect the PIC with device name INCUMOTOR")
                timedMessage = NameStr & "  You did not connect the PIC with device name INCUMOTOR"
                addtomyConsoleErrorMessages(timedMessage)
                time = 4
                FormTimedDialog.Show()
            End If
        Else
            Write2Buffer(Str) 'PIC looks for "#" as delimiter
            hidWrite(pHandleIncumotor, BufferOut(0))
        End If

        'Array.Sort(myArraySorted)
        'Dim max As Integer = myArraySorted(myArraySorted.GetUpperBound(0))
        'Dim FILE_NAME As String = "C:\Images\Now\Pos1_bf\maxvalues.txt"  'creates a text file with the values.
        'Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
        'objWriter.WriteLine(ntimerfortest.ToString & "- " & max.ToString)
        'objWriter.Close()


        Dim max As Integer = myArray(0)
        nmax = 1
        For i As Integer = 1 To myArray.GetUpperBound(0)
            If myArray(i) > max Then
                max = myArray(i)
                nmax = i + 1
            End If
        Next

        '
        If callerisTest Then
            If ntimerfortest = 1 Then
                FormTestFocus.Label2.ForeColor = goodorbad()
                FormTestFocus.Label2.Show()
                FormTestFocus.Label1.Text = "Testing Focus...  " & "(" & ntimerfortest & "/8)"
            ElseIf ntimerfortest = 2 Then
                FormTestFocus.Label3.ForeColor = goodorbad()
                FormTestFocus.Label3.Show()
                FormTestFocus.Label1.Text = "Testing Focus...  " & "(" & ntimerfortest & "/8)"
            ElseIf ntimerfortest = 3 Then
                FormTestFocus.Label4.ForeColor = goodorbad()
                FormTestFocus.Label4.Show()
                FormTestFocus.Label1.Text = "Testing Focus...  " & "(" & ntimerfortest & "/8)"
            ElseIf ntimerfortest = 4 Then
                FormTestFocus.Label5.ForeColor = goodorbad()
                FormTestFocus.Label5.Show()
                FormTestFocus.Label1.Text = "Testing Focus...  " & "(" & ntimerfortest & "/8)"
            ElseIf ntimerfortest = 5 Then
                FormTestFocus.Label6.ForeColor = goodorbad()
                FormTestFocus.Label6.Show()
                FormTestFocus.Label1.Text = "Testing Focus...  " & "(" & ntimerfortest & "/8)"
            ElseIf ntimerfortest = 6 Then
                FormTestFocus.Label7.ForeColor = goodorbad()
                FormTestFocus.Label7.Show()
                FormTestFocus.Label1.Text = "Testing Focus...  " & "(" & ntimerfortest & "/8)"
            ElseIf ntimerfortest = 7 Then
                FormTestFocus.Label8.ForeColor = goodorbad()
                FormTestFocus.Label8.Show()
                FormTestFocus.Label1.Text = "Testing Focus...  " & "(" & ntimerfortest & "/8)"
            ElseIf ntimerfortest = 8 Then
                FormTestFocus.Label9.ForeColor = goodorbad()
                FormTestFocus.Label9.Show()
                FormTestFocus.Label1.Text = "Testing Focus...  " & "done"
                If previousnmax = 0 Then
                    MsgBox("Bad Location, select a new one..")
                End If
                MsgBox("Test passed !..  (This is a good location to focus)")
                FormTestFocus.Close()
            End If

        End If
        '
        Dim Path As String = imagefolder & "\Now\" & "FocusedImages" & "\"
        If (Not System.IO.Directory.Exists(Path)) Then
            System.IO.Directory.CreateDirectory(Path)
        End If
        FILE_NAME = imagefolder & "\Now\FocusedImages\FocusingTest-maxvalues.txt"  'creates a text file with the values.
        Dim objWriter3 As New System.IO.StreamWriter(FILE_NAME, True)
        objWriter3.WriteLine(ntimerfortest.ToString & "- " & max.ToString)
        objWriter3.WriteLine("At position " & nmax.ToString)
        objWriter3.Close()


        If nmax <> 4 And dontsendemail > 1 Then   'dontsendemail starts at 0 when you press start and increments each cycle.
            'sendemail(nmax.ToString)
            Label30.Show()
        End If


        FILE_NAME = imagefolder & "\Now\FocusedImages\FocusingTest-allvalues.txt"  'creates a text file with the values.
        Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME, True)
        For i As Integer = 0 To myArray.GetUpperBound(0)
            objWriter2.WriteLine(ntimerfortest.ToString & "- " & (i + 1).ToString & " - " & myArray(i).ToString)
        Next
        objWriter2.Close()

        'go back the neccessare number of steps to the best focused position:
        If (n - nmax) >= 0 Then
            ' MsgBox("(n - nmax) is" & (n - nmax) & "so going down now")
            For i As Integer = 0 To (n - nmax)
                'up = False
                motorSubforbackground_DOWN()
                'MsgBox("gone down")
                Threading.Thread.Sleep(200) '300
            Next
        Else
            'MsgBox("(n - nmax) is" & (n - nmax) & "so NOT going down now")
        End If


        'If up = True Then
        '    up = False
        'Else
        '    up = True
        'End If
        'ntimerfortest = ntimerfortest + 1
        'If ntimerfortest <= repeats Then
        '    focusingSeries()
        'Else
        DisconnectFromHID()

        GrabImage()
        'save focused image
        Path = imagefolder & "\Now\FocusedImages\Images\"
        If (Not System.IO.Directory.Exists(Path)) Then
            System.IO.Directory.CreateDirectory(Path)
        End If
        'vb = vb + 1
        'PictureBox1.Image.Save("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & "_" & vb.ToString & "_" & name & ".bmp", System.Drawing.Imaging.ImageFormat.Bmp)
        bitmap.Save(Path & ntimerfortest & " - " & vb.ToString & "_" & ".bmp", System.Drawing.Imaging.ImageFormat.Bmp)

        ntimerfortest = ntimerfortest + 1
        '
        'MsgBox("done")

        'End If

        'Add this for testing focusing non stop:
        'If ntimerfortest <= repeats Then
        '    up = False 'down
        '    Send("k") '20um
        '    Threading.Thread.Sleep(500)
        '    lightON()
        '    up = True
        '    'repeats = 1 'so that focusingSeries() only goes up once.
        '    focusingSeries()
        'End If



        Label7.Hide()

        If callerisTest = True Then

        Else
            continuation()

        End If

        If ntimerfortest <= repeats Then
            ' MsgBox("repeating focus: ntimerfortest < repeats " & ntimerfortest & " < " & repeats)
            dofocus()
        End If

    End Sub



    Private Sub LinkLabel7_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel7.LinkClicked
        newfocus()
    End Sub

    Sub newfocus()
        enableMotor()
        Thread.Sleep(300)

        If ComboBox1.SelectedItem.ToString = "BF2" Then
            RadioButton7.Checked = True  'BF2 radiobutton
        ElseIf ComboBox1.SelectedItem.ToString = "Fluo" Then
            RadioButton8.Checked = True  'Fluo radiobutton
        ElseIf ComboBox1.SelectedItem.ToString = "DarkF" Then
            RadioButton9.Checked = True  'DarkF radiobutton
        Else
            MsgBox("Can't do autofocus if no light type is selected! (Combobox)")
        End If





        'single focus.
        'up = False 'down

        'sendtoINCUMOTOR("k") '20um down
        RadioButton1.Checked = True
        motorSub_DOWN()





        'Threading.Thread.Sleep(300) '400
        'lightON()
        ' up = True
        callerisTest = True
        repeats = 1
        'so that focusingSeries() only goes up once.
        focusingSeriesNEW()

        'turn the motor off after focusing to avoid heating problems!!!!:
        disableMotor()
    End Sub






    Private Sub Button71_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button71.Click 'test button
        sendemailCameraDisconnected()
    End Sub

    Sub sendemailCameraDisconnected()
        If CheckBox29.Checked Then 'sendEmail check box
            sendemail("camera")
        End If

    End Sub
    Sub sendemailMemory()
        sendemail("memory")
    End Sub
    Sub sendemailBackupError()
        sendemail("backup")
    End Sub

    Sub sendemail(ByVal i As String)

        Try
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New  _
  Net.NetworkCredential("julian202@gmail.com", "jpgCaixa08")
            SmtpServer.Port = 587
            SmtpServer.Host = "smtp.gmail.com"
            SmtpServer.EnableSsl = True
            mail = New MailMessage()
            mail.From = New MailAddress("julian202@gmail.com")
            mail.To.Add("julian202@gmail.com")
            If i = "memory" Then
                mail.Subject = "INCUSCOPE INSUFFICIENT MEMORY IN " & thisPC
                mail.Body = "INCUSCOPE INSUFFICIENT MEMORY" & thisPC
            End If
            If i = "camera" Then
                mail.Subject = "INCUSCOPE CAMERA DISCONNECTED IN " & thisPC
                mail.Body = "INCUSCOPE CAMERA DISCONNECTED IN " & thisPC
            End If
            If i = "backup" Then
                mail.Subject = numbackuperrors.ToString & "INCUSCOPE VANADIUM BACKUP ERROR IN " & thisPC
                mail.Body = numbackuperrors.ToString & "INCUSCOPE VANADIUM BACKUP ERROR IN " & thisPC
            End If



            SmtpServer.Send(mail)
            'MsgBox("mail send")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub




    Private Sub Button72_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button72.Click
        Str = "r" & Chr(1)
        sendtoINCUSCOPE(Str)
    End Sub

    Private Sub Button73_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button73.Click
        Str = "f" & Chr(1)
        sendtoINCUSCOPE(Str)
    End Sub

    Private Sub Button74_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button74.Click
        Str = "f" & Chr(0)
        sendtoINCUSCOPE(Str)
    End Sub

    Private Sub Button75_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button75.Click
        Str = "r" & Chr(0)
        sendtoINCUSCOPE(Str)
    End Sub



    Sub movePololu(ByVal forward As Byte, ByVal steps As Byte, ByVal repetitions As Byte, ByVal xy As Byte)

        'Dim b As Byte = 0
        'Dim c As Byte = 2
        If steps > 256 And repetitions > 256 Then
            MsgBox("number cannot be more than 255")
        Else


            ConnectToHID(Me)
            nitems = hidGetItemCount()
            For i = 0 To (nitems - 1)
                pHandlePololu = hidGetItem(i)
                pName = hidGetProductName(pHandlePololu, NameStr, NameLength)
                If NameStr = "POLOLUPIC" Then
                    Exit For
                End If
            Next
            If NameStr <> "POLOLUPIC" Then
                'MsgBox(NameStr & "  You did not connect the PIC with device name POLOLUPIC")
                timedMessage = NameStr & "  You did not connect the PIC with device name POLOLUPIC"
                addtomyConsoleErrorMessages(timedMessage)
                time = 4
                FormTimedDialog.Show()
            Else
                'MyWrite2Buffer(b) 'PIC looks for "#" as delimiter
                BufferOut(1) = forward
                BufferOut(2) = steps
                BufferOut(3) = repetitions
                BufferOut(4) = xy
                hidWrite(pHandlePololu, BufferOut(0))
            End If
            DisconnectFromHID()

        End If
    End Sub

    Sub moveStepper(ByVal forward As Byte, ByVal steps As Byte, ByVal repetitions As Byte)

        'Dim b As Byte = 0
        'Dim c As Byte = 2

        ConnectToHID(Me)
        nitems = hidGetItemCount()
        For i = 0 To (nitems - 1)
            pHandleIncumotor = hidGetItem(i)
            pName = hidGetProductName(pHandleIncumotor, NameStr, NameLength)
            If NameStr = "INCUMOTOR" Then
                Exit For
            End If
        Next
        If NameStr <> "INCUMOTOR" Then
            ' MsgBox(NameStr & "  You did not connect the PIC with device name INCUMOTOR")
            Label50.Show()
            Label50.Refresh()
            Threading.Thread.Sleep(1000)
            Label50.Hide()
        Else
            'MyWrite2Buffer(b) 'PIC looks for "#" as delimiter
            BufferOut(1) = forward
            BufferOut(2) = steps
            BufferOut(3) = repetitions
            hidWrite(pHandleIncumotor, BufferOut(0))
        End If
        DisconnectFromHID()


    End Sub




    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    Sub camchange()
        stopThread = True
        lightsOFF()
        Thread.Sleep(600)
        WaitEvent.Set()
    End Sub




    Dim nbyn As Integer = 4
    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        nbyn = TextBox7.Text
        TextBox44.Text = TextBox7.Text
    End Sub

    Private Sub Button78_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button78.Click
        addnbynbutton()
    End Sub

    Sub addnbynbutton()

        If CheckBox15.Checked Then 'this is the switch xy checkbox.
            If pololumode = True Then
                addnbynOLDstage() 'uni-directional checkbox is included here!
            Else
                addnbyn() 'you dont have uni-directional for non-pololu
            End If

        Else
            If CheckBox22.Checked = True Then 'this is add only in x, keep y zero
                'addnONLYoneLINE()
                addnbynNEWstageOneDir2()
            Else
                If CheckBox14.Checked = True Then  'this is the uni-directional checkbox.
                    addnbynNEWstageONEdir()

                Else
                    addnbynNEWstage()
                End If

            End If


        End If

        Try
            ListBox1.SelectedIndex = Label42.Text + 1
            Label42.Text = ListBox1.SelectedIndex
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click '2by2 button
        'If pololumode = True Then
        '    add2by2Pololu()
        'ElseIf pololumode = False Then
        '    add2by2()
        'End If
        TextBox7.Text = 2
        If mcl Then
            stopThread = True
            lightsOFF()
            addnynMcl()
        ElseIf pololumode = True Then
            addnynPololu()

        ElseIf pololumode = False Then
            addnbynbutton()
        End If
    End Sub
    Sub addnynMcl()
        AddCurrentPosMcl()
        '   deleteAll()
        'AddCurrentPos()
        Console.WriteLine("now geting nbyn..")
        nbyn = CInt(TextBox7.Text)


        Threading.Thread.Sleep(100)
        Console.WriteLine("now assempling string..")
        Str = "U" & Chr(67).ToString & Chr(13).ToString
        Console.WriteLine("now writing x..")
        SerialPort1.Write(Str)
        Console.WriteLine("now reading x..")
        Xpos = SerialPort1.ReadExisting
        Console.WriteLine("now writing x..")
        Console.WriteLine(Xpos)
        Threading.Thread.Sleep(100)
        Console.WriteLine("now reading x..")
        Xpos = SerialPort1.ReadExisting
        Console.WriteLine(Xpos)

        Threading.Thread.Sleep(100)
        Str = "U" & Chr(68) & Chr(13)
        SerialPort1.Write(Str)
        Console.WriteLine("now reading y..")
        Ypos = SerialPort1.ReadExisting
        Console.WriteLine(Ypos)
        Threading.Thread.Sleep(100)
        Console.WriteLine("now reading y..")
        Ypos = SerialPort1.ReadExisting
        Console.WriteLine(Ypos)
        If Ypos = "" Or Xpos = "" Then
            MsgBox("Empty string")
        Else
            addnynCommonCode2()
        End If
    End Sub

    Sub addnynCommonCode()
        Console.WriteLine("now common code:")
        For j As Integer = 1 To nbyn
            Console.WriteLine("j " & j)
            If (j Mod 2) <> 0 Then   'this is j odd 

                For i As Integer = 1 To nbyn
                    Console.WriteLine("i " & i)
                    If j = 1 And i = 1 Then
                        'do nothing because we already have added the first position
                    Else
                        ListBox2.Items.Add((CInt(Xpos) + ((i - 1) * 0)).ToString + (j - 1) * myheigth)
                        My.Settings.PositionsX.Add((CInt(Xpos) + ((i - 1) * 0)).ToString + (j - 1) * myheigth)
                    End If
                Next

            Else

                For i As Integer = nbyn To 1 Step -1
                    ListBox2.Items.Add((CInt(Xpos) + ((i - 1) * 0)).ToString + (j - 1) * myheigth)
                    My.Settings.PositionsX.Add((CInt(Xpos) + ((i - 1) * 0)).ToString + (j - 1) * myheigth)
                Next

            End If
        Next

        ' now y:
        For j As Integer = 1 To nbyn
            'Console.WriteLine(j & " mod 2 is " & (j Mod 2))
            If (j Mod 2) <> 0 Then   'this is j odd 

                ' Console.WriteLine(j & " is odd")
                For i As Integer = 0 To nbyn - 1
                    If j = 1 And i = 0 Then
                        'do nothing because we already have added the first position
                    Else
                        ListBox3.Items.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * mywidth)
                        My.Settings.PositionsY.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * mywidth)
                    End If
                Next
            Else  'this is j even
                ' Console.WriteLine(j & " is even")
                For i As Integer = nbyn - 1 To 0 Step -1
                    ListBox3.Items.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * mywidth)
                    My.Settings.PositionsY.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * mywidth)
                Next
            End If
        Next

        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
    End Sub
    Sub addnynCommonCode2()
        Console.WriteLine("now common code2:")
        For j As Integer = 1 To nbyn
            If (j Mod 2) <> 0 Then   'this is j odd 

                For i As Integer = 1 To nbyn
                    If j = 1 And i = 1 Then
                        'do nothing because we already have added the first position
                    Else
                        ListBox3.Items.Add((CInt(Ypos) + ((i - 1) * 0)).ToString + (j - 1) * myheigth)
                        My.Settings.PositionsY.Add((CInt(Ypos) + ((i - 1) * 0)).ToString + (j - 1) * myheigth)
                    End If
                Next

            Else

                For i As Integer = nbyn To 1 Step -1
                    ListBox3.Items.Add((CInt(Ypos) + ((i - 1) * 0)).ToString + (j - 1) * myheigth)
                    My.Settings.PositionsY.Add((CInt(Ypos) + ((i - 1) * 0)).ToString + (j - 1) * myheigth)
                Next

            End If
        Next
        For j As Integer = 1 To nbyn
            'Console.WriteLine(j & " mod 2 is " & (j Mod 2))
            If (j Mod 2) <> 0 Then   'this is j odd 

                ' Console.WriteLine(j & " is odd")
                For i As Integer = 0 To nbyn - 1
                    If j = 1 And i = 0 Then
                        'do nothing because we already have added the first position
                    Else
                        ListBox2.Items.Add((CInt(Xpos) + (j - 1) * 0).ToString - i * mywidth)
                        My.Settings.PositionsX.Add((CInt(Xpos) + (j - 1) * 0).ToString - i * mywidth)
                    End If
                Next
            Else  'this is j even
                ' Console.WriteLine(j & " is even")
                For i As Integer = nbyn - 1 To 0 Step -1
                    ListBox2.Items.Add((CInt(Xpos) + (j - 1) * 0).ToString - i * mywidth)
                    My.Settings.PositionsX.Add((CInt(Xpos) + (j - 1) * 0).ToString - i * mywidth)
                Next
            End If
        Next
        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
    End Sub


    Sub ycolumnONEdir()
        ycolumn()
    End Sub
    Sub ycolumn()
        For j As Integer = 1 To nbyn
            If (j Mod 2) <> 0 Then   'this is j odd 

                For i As Integer = 1 To nbyn
                    If j = 1 And i = 1 Then
                        'do nothing because we already have added the first position
                    Else
                        ListBox3.Items.Add((CInt(Ypos) + ((i - 1) * 0)) + CInt((j - 1) * myheigth))
                        My.Settings.PositionsY.Add((CInt(Ypos) + ((i - 1) * 0)) + CInt((j - 1) * myheigth))
                    End If
                Next
            Else
                For i As Integer = nbyn To 1 Step -1
                    ListBox3.Items.Add((CInt(Ypos) + ((i - 1) * 0)) + CInt((j - 1) * myheigth))
                    My.Settings.PositionsY.Add((CInt(Ypos) + ((i - 1) * 0)) + CInt((j - 1) * myheigth))
                Next
            End If
        Next
    End Sub



    Sub xcolumn()
        For j As Integer = 1 To nbyn
            'Console.WriteLine(j & " mod 2 is " & (j Mod 2))
            If (j Mod 2) <> 0 Then   'this is j odd 

                ' Console.WriteLine(j & " is odd")
                For i As Integer = 0 To nbyn - 1
                    If j = 1 And i = 0 Then
                        'do nothing because we already have added the first position
                    Else
                        ListBox2.Items.Add((CInt(Xpos) + (j - 1) * 0) - CInt(i * mywidth))
                        My.Settings.PositionsX.Add((CInt(Xpos) + (j - 1) * 0) - CInt(i * mywidth))
                    End If
                Next
            Else  'this is j even
                ' Console.WriteLine(j & " is even")
                For i As Integer = nbyn - 1 To 0 Step -1
                    ListBox2.Items.Add((CInt(Xpos) + (j - 1) * 0) - CInt(i * mywidth))
                    My.Settings.PositionsX.Add((CInt(Xpos) + (j - 1) * 0) - CInt(i * mywidth))
                Next
            End If
        Next
    End Sub

    Sub xcolumnONEdir()
        For j As Integer = 1 To nbyn 'nbyn
            'Console.WriteLine(j & " mod 2 is " & (j Mod 2))
            ' If (j Mod 2) <> 0 Then   'this is j odd 

            ' Console.WriteLine(j & " is odd")
            For i As Integer = 0 To nbyn - 1
                If j = 1 And i = 0 Then
                    'do nothing because we already have added the first position
                Else
                    ListBox2.Items.Add((CInt(Xpos) + (j - 1) * 0).ToString - i * mywidth)
                    My.Settings.PositionsX.Add((CInt(Xpos) + (j - 1) * 0).ToString - i * mywidth)
                End If
            Next
            'Else  'this is j even
            '    ' Console.WriteLine(j & " is even")
            '    For i As Integer = nbyn - 1 To 0 Step -1
            '        ListBox2.Items.Add((CInt(Xpos) + (j - 1) * 0).ToString - i * mywidth)
            '        My.Settings.PositionsX.Add((CInt(Xpos) + (j - 1) * 0).ToString - i * mywidth)
            '    Next
            ' End If
        Next
    End Sub


    Sub xcolumnONEdir2()
        For j As Integer = 1 To 1 'nbyn
            'Console.WriteLine(j & " mod 2 is " & (j Mod 2))
            ' If (j Mod 2) <> 0 Then   'this is j odd 

            ' Console.WriteLine(j & " is odd")
            For i As Integer = 0 To nbyn - 1
                If j = 1 And i = 0 Then
                    'do nothing because we already have added the first position
                Else
                    ListBox2.Items.Add((CInt(Xpos) + (j - 1) * 0) - i * CInt(mywidth))
                    My.Settings.PositionsX.Add((CInt(Xpos) + (j - 1) * 0) - i * CInt(mywidth))
                End If
            Next
            'Else  'this is j even
            '    ' Console.WriteLine(j & " is even")
            '    For i As Integer = nbyn - 1 To 0 Step -1
            '        ListBox2.Items.Add((CInt(Xpos) + (j - 1) * 0).ToString - i * mywidth)
            '        My.Settings.PositionsX.Add((CInt(Xpos) + (j - 1) * 0).ToString - i * mywidth)
            '    Next
            ' End If
        Next
    End Sub

    Sub ycolumnOLD()
        For j As Integer = 1 To nbyn
            If (j Mod 2) <> 0 Then   'this is j odd 

                For i As Integer = 1 To nbyn
                    If j = 1 And i = 1 Then
                        'do nothing because we already have added the first position
                    Else
                        ListBox2.Items.Add((CInt(Xpos) + ((i - 1) * 0)).ToString + (j - 1) * mywidth)
                        My.Settings.PositionsX.Add((CInt(Xpos) + ((i - 1) * 0)).ToString + (j - 1) * mywidth)
                    End If
                Next
            Else
                For i As Integer = nbyn To 1 Step -1
                    ListBox2.Items.Add((CInt(Xpos) + ((i - 1) * 0)).ToString + (j - 1) * mywidth)
                    My.Settings.PositionsX.Add((CInt(Xpos) + ((i - 1) * 0)).ToString + (j - 1) * mywidth)
                Next
            End If
        Next
    End Sub

    Sub xcolumnOLD()
        For j As Integer = 1 To nbyn
            'Console.WriteLine(j & " mod 2 is " & (j Mod 2))

            If CheckBox14.Checked = True Then  'this is the uni-directional checkbox.
                For i As Integer = 0 To nbyn - 1
                    If j = 1 And i = 0 Then
                        'do nothing because we already have added the first position
                    Else
                        ListBox3.Items.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * myheigth)
                        My.Settings.PositionsY.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * myheigth)
                    End If
                Next
            Else


                If (j Mod 2) <> 0 Then   'this is j odd 

                    ' Console.WriteLine(j & " is odd")
                    For i As Integer = 0 To nbyn - 1
                        If j = 1 And i = 0 Then
                            'do nothing because we already have added the first position
                        Else
                            ListBox3.Items.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * myheigth)
                            My.Settings.PositionsY.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * myheigth)
                        End If
                    Next
                Else  'this is j even
                    ' Console.WriteLine(j & " is even")
                    For i As Integer = nbyn - 1 To 0 Step -1
                        ListBox3.Items.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * myheigth)
                        My.Settings.PositionsY.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * myheigth)
                    Next
                End If

            End If


        Next
    End Sub

    Sub addnbynOLDstage()

        repeats = TextBox11.Text

        'AddCurrentPos()
        nbyn = TextBox7.Text
        If pololumode Then
            Xpos = Label36.Text
            Ypos = Label37.Text
        Else
            Try
                SerialPort1.DiscardInBuffer()
            Catch ex As Exception
            End Try
            Str = "w y"
            Str += ControlChars.Cr
            Try
                SerialPort1.Write(Str)
            Catch ex As Exception
            End Try

            Threading.Thread.Sleep(100)
            mychar = {":", "A"}

            Try
                Ypos = SerialPort1.ReadExisting
                Ypos = Ypos.TrimStart(mychar)

                'ycolumn()

            Catch ex As Exception
            End Try
            Str = "w x"
            Str += ControlChars.Cr
            Try
                SerialPort1.Write(Str)
            Catch ex As Exception
            End Try
            Threading.Thread.Sleep(100)
            Try
                Xpos = SerialPort1.ReadExisting
                Xpos = Xpos.TrimStart(mychar)

                'xcolumn()

            Catch ex As Exception
            End Try
        End If

        For j = 1 To CInt(TextBox12.Text)

            If (j Mod 2) <> 0 Then   'this is j odd

                For k = 1 To CInt(TextBox11.Text)
                    If k <> 1 Then
                        Xpos = Xpos - wellwidth
                    End If
                    Ypos = Ypos
                    ListBox2.Items.Add(Xpos)
                    My.Settings.PositionsX.Add(Xpos)
                    ListBox3.Items.Add(Ypos)
                    My.Settings.PositionsY.Add(Ypos)
                    ycolumnOLD()
                    xcolumnOLD()
                Next

            Else  'this is j even
                Ypos = Ypos - wellheight
                For k = 1 To CInt(TextBox11.Text)
                    If k <> 1 Then
                        Xpos = Xpos + wellwidth
                    End If
                    ListBox2.Items.Add(Xpos)
                    My.Settings.PositionsX.Add(Xpos)
                    ListBox3.Items.Add(Ypos)
                    My.Settings.PositionsY.Add(Ypos)
                    ycolumnOLD()
                    xcolumnOLD()
                Next
                Ypos = Ypos - wellheight

            End If




        Next




        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")

    End Sub

    Sub addnbynNEWstageONEdir()

        repeats = TextBox11.Text

        getcurrentXposandYpos()



        For j = 1 To CInt(TextBox12.Text) 'TextBox12 is number of grids in y direction

            If (j Mod 2) <> 0 Then   'this is j odd. 

                For k = 1 To CInt(TextBox11.Text)  'TextBox11 is number of grids in x direction
                    If k <> 1 Then
                        Xpos = (CInt(Xpos) - CInt(wellwidth)).ToString
                    End If
                    Ypos = Ypos
                    ListBox2.Items.Add(Xpos)
                    My.Settings.PositionsX.Add(Xpos)
                    ListBox3.Items.Add(Ypos)
                    My.Settings.PositionsY.Add(Ypos)
                    ycolumnONEdir()
                    xcolumnONEdir()
                Next

            Else  'this is j even
                Ypos = (CInt(Ypos) + CInt(wellheight)).ToString
                For k = 1 To CInt(TextBox11.Text)
                    If k <> 1 Then
                        Xpos = (CInt(Xpos) + CInt(wellwidth)).ToString
                    End If
                    ListBox2.Items.Add(Xpos)
                    My.Settings.PositionsX.Add(Xpos)
                    ListBox3.Items.Add(Ypos)
                    My.Settings.PositionsY.Add(Ypos)
                    ycolumnONEdir()
                    xcolumnONEdir()
                Next
                Ypos = (CInt(Ypos) + CInt(wellheight)).ToString

            End If

        Next




        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()
    End Sub

    Sub addnbynNEWstageOneDir2()

        repeats = TextBox11.Text
        nbyn = TextBox7.Text
        getcurrentXposandYpos()


        For j = 1 To CInt(TextBox12.Text)

            If (j Mod 2) <> 0 Then   'this is j odd

                For k = 1 To CInt(TextBox11.Text)
                    If k <> 1 Then
                        Xpos = (CInt(Xpos) - CInt(wellwidth)).ToString
                    End If
                    Ypos = Ypos
                    ListBox2.Items.Add(Xpos)
                    My.Settings.PositionsX.Add(Xpos)
                    ListBox3.Items.Add(Ypos)
                    My.Settings.PositionsY.Add(Ypos)
                    'ycolumn()
                    'xcolumn()
                    For m As Integer = 1 To nbyn - 1
                        ListBox3.Items.Add(Ypos)
                        My.Settings.PositionsY.Add(Ypos)
                    Next

                    xcolumnONEdir2()

                Next

            Else  'this is j even
                Ypos = (CInt(Ypos) + CInt(wellheight)).ToString
                For k = 1 To CInt(TextBox11.Text)
                    If k <> 1 Then
                        Xpos = (CInt(Xpos) + CInt(wellwidth)).ToString
                    End If
                    ListBox2.Items.Add(Xpos)
                    My.Settings.PositionsX.Add(Xpos)
                    ListBox3.Items.Add(Ypos)
                    My.Settings.PositionsY.Add(Ypos)
                    'ycolumn()
                    'xcolumn()
                    For m As Integer = 1 To nbyn - 1
                        ListBox3.Items.Add(Ypos)
                        My.Settings.PositionsY.Add(Ypos)
                    Next

                    xcolumnONEdir2()
                Next
                Ypos = (CInt(Ypos) + CInt(wellheight)).ToString
            End If
        Next
        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()

    End Sub


    Sub addnbynNEWstage()

        repeats = TextBox11.Text
        nbyn = TextBox7.Text
        getcurrentXposandYpos()


        For j = 1 To CInt(TextBox12.Text)

            If (j Mod 2) <> 0 Then   'this is j odd

                For k = 1 To CInt(TextBox11.Text)
                    If k <> 1 Then
                        Xpos = (CInt(Xpos) - CInt(wellwidth)).ToString
                    End If
                    Ypos = Ypos
                    ListBox2.Items.Add(Xpos)
                    My.Settings.PositionsX.Add(Xpos)
                    ListBox3.Items.Add(Ypos)
                    My.Settings.PositionsY.Add(Ypos)
                    ycolumn()
                    xcolumn()


                Next

            Else  'this is j even
                Ypos = (CInt(Ypos) + CInt(wellheight)).ToString
                For k = 1 To CInt(TextBox11.Text)
                    If k <> 1 Then
                        Xpos = (CInt(Xpos) + CInt(wellwidth)).ToString
                    End If
                    ListBox2.Items.Add(Xpos)
                    My.Settings.PositionsX.Add(Xpos)
                    ListBox3.Items.Add(Ypos)
                    My.Settings.PositionsY.Add(Ypos)
                    ycolumn()
                    xcolumn()

                Next
                Ypos = (CInt(Ypos) + CInt(wellheight)).ToString
            End If
        Next
        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()

    End Sub

    Sub addnONLYoneLINE()

        repeats = TextBox11.Text
        nbyn = TextBox7.Text
        getcurrentXposandYpos()
        'Xpos = "0"
        'Ypos = "0"


        ListBox2.Items.Add(Xpos)
        My.Settings.PositionsX.Add(Xpos)
        ListBox3.Items.Add(Ypos)
        My.Settings.PositionsY.Add(Ypos)
        'ycolumn()

        For j As Integer = 1 To nbyn - 1
            ListBox3.Items.Add(Ypos)
            My.Settings.PositionsY.Add(Ypos)
        Next

        xcolumnONEdir()


        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()

    End Sub


    Sub getcurrentXposandYpos()
        If pololumode Then
            Xpos = Label36.Text
            Ypos = Label37.Text
        ElseIf DCmotors.Checked Then
            Xpos = "0"
            Ypos = "0"
        Else

            Try
                SerialPort1.DiscardInBuffer()
            Catch ex As Exception
            End Try
            Str = "w y"
            Str += ControlChars.Cr
            Try
                SerialPort1.Write(Str)
            Catch ex As Exception
            End Try

            Threading.Thread.Sleep(100)
            mychar = {":", "A"}

            Try
                Ypos = SerialPort1.ReadExisting
                Ypos = Ypos.TrimStart(mychar)

                'ycolumn()

            Catch ex As Exception

            End Try



            Str = "w x"
            Str += ControlChars.Cr
            Try
                SerialPort1.Write(Str)
            Catch ex As Exception
            End Try
            Threading.Thread.Sleep(100)
            Try
                Xpos = SerialPort1.ReadExisting
                Xpos = Xpos.TrimStart(mychar)

                'xcolumn()

            Catch ex As Exception
            End Try

        End If
    End Sub

    Sub addnbyn()
        AddCurrentPos()
        '   deleteAll()
        'AddCurrentPos()
        nbyn = TextBox7.Text
        'If versionMS2000 Then
        'mywidth = widthMS2000
        ' myheigth = heigthMS2000
        'Else
        'mywidth = widthMS4
        'myheigth = heigthMS4
        'End If

        getcurrentXposandYpos()

        For j As Integer = 1 To nbyn
            If (j Mod 2) <> 0 Then   'this is j odd 

                For i As Integer = 1 To nbyn
                    If j = 1 And i = 1 Then
                        'do nothing because we already have added the first position
                    Else
                        ListBox2.Items.Add((CInt(Xpos) + ((i - 1) * 0)).ToString + (j - 1) * myheigth)
                        My.Settings.PositionsX.Add((CInt(Xpos) + ((i - 1) * 0)).ToString + (j - 1) * myheigth)
                    End If
                Next

            Else

                For i As Integer = nbyn To 1 Step -1
                    ListBox2.Items.Add((CInt(Xpos) + ((i - 1) * 0)).ToString + (j - 1) * myheigth)
                    My.Settings.PositionsX.Add((CInt(Xpos) + ((i - 1) * 0)).ToString + (j - 1) * myheigth)
                Next

            End If
        Next



        'For j As Integer = 1 To n
        '    'Console.WriteLine(j & " mod 2 is " & (j Mod 2))
        '    If (j Mod 2) <> 0 Then   'this is j odd 

        '        ' Console.WriteLine(j & " is odd")
        '        For i As Integer = 0 To n - 1
        '            ListBox2.Items.Add(Xpos - i * mywidth)
        '            My.Settings.PositionsX.Add(Xpos - i * mywidth)
        '        Next
        '    Else  'this is j even
        '        ' Console.WriteLine(j & " is even")
        '        For i As Integer = n - 1 To 0 Step -1
        '            ListBox2.Items.Add(Xpos - i * mywidth)
        '            My.Settings.PositionsX.Add(Xpos - i * mywidth)
        '        Next
        '    End If
        'Next




        'ListBox2.Items.Add(Xpos - mywidth)
        'ListBox2.Items.Add(Xpos - (2 * mywidth))
        'ListBox2.Items.Add(Xpos - (2 * mywidth))
        'ListBox2.Items.Add(Xpos - mywidth)
        'ListBox2.Items.Add(Xpos)
        'ListBox2.Items.Add(Xpos)
        'ListBox2.Items.Add(Xpos - mywidth)
        'ListBox2.Items.Add(Xpos - (2 * mywidth))
        ''  My.Settings.PositionsX.Add(Xpos)
        'My.Settings.PositionsX.Add(Xpos - mywidth)
        'My.Settings.PositionsX.Add(Xpos - (2 * mywidth))
        'My.Settings.PositionsX.Add(Xpos - (2 * mywidth))
        'My.Settings.PositionsX.Add(Xpos - mywidth)
        'My.Settings.PositionsX.Add(Xpos)
        'My.Settings.PositionsX.Add(Xpos)
        'My.Settings.PositionsX.Add(Xpos - mywidth)
        'My.Settings.PositionsX.Add(Xpos - (2 * mywidth))
        '    Catch ex As Exception
        'End Try




        For j As Integer = 1 To nbyn
            'Console.WriteLine(j & " mod 2 is " & (j Mod 2))
            If (j Mod 2) <> 0 Then   'this is j odd 

                ' Console.WriteLine(j & " is odd")
                For i As Integer = 0 To nbyn - 1
                    If j = 1 And i = 0 Then
                        'do nothing because we already have added the first position
                    Else
                        ListBox3.Items.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * mywidth)
                        My.Settings.PositionsY.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * mywidth)
                    End If
                Next
            Else  'this is j even
                ' Console.WriteLine(j & " is even")
                For i As Integer = nbyn - 1 To 0 Step -1
                    ListBox3.Items.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * mywidth)
                    My.Settings.PositionsY.Add((CInt(Ypos) + (j - 1) * 0).ToString - i * mywidth)
                Next
            End If
        Next

        'For i As Integer = 0 To n - 1
        '    For j As Integer = 1 To n
        '        ListBox3.Items.Add(Ypos + i * myheigth)
        '        My.Settings.PositionsY.Add(Ypos + i * myheigth)
        '    Next
        'Next


        '' ListBox3.Items.Add(Ypos)
        'ListBox3.Items.Add(Ypos)
        'ListBox3.Items.Add(Ypos)
        'ListBox3.Items.Add(Ypos + myheigth)
        'ListBox3.Items.Add(Ypos + myheigth)
        'ListBox3.Items.Add(Ypos + myheigth)
        'ListBox3.Items.Add(Ypos + (2 * myheigth))
        'ListBox3.Items.Add(Ypos + (2 * myheigth))
        'ListBox3.Items.Add(Ypos + (2 * myheigth))

        ''My.Settings.PositionsY.Add(Ypos)
        'My.Settings.PositionsY.Add(Ypos)
        'My.Settings.PositionsY.Add(Ypos)
        'My.Settings.PositionsY.Add(Ypos + myheigth)
        'My.Settings.PositionsY.Add(Ypos + myheigth)
        'My.Settings.PositionsY.Add(Ypos + myheigth)
        'My.Settings.PositionsY.Add(Ypos + (2 * myheigth))
        'My.Settings.PositionsY.Add(Ypos + (2 * myheigth))
        'My.Settings.PositionsY.Add(Ypos + (2 * myheigth))
        '    Catch ex As Exception
        'End Try

        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()
    End Sub

    Sub addnynPololu()
        'add a preposition to avoid backlash:
        ListBox2.Items.Add(pololuPosX + 1)
        My.Settings.PositionsX.Add(pololuPosX + 1)
        ListBox3.Items.Add(pololuPosY - 1)
        My.Settings.PositionsY.Add(pololuPosY - 1)

        'AddCurrentPosPololu()
        'current pos:
        ListBox2.Items.Add(pololuPosX)
        My.Settings.PositionsX.Add(pololuPosX)
        ListBox3.Items.Add(pololuPosY)
        My.Settings.PositionsY.Add(pololuPosY)




        'MsgBox(pololuPosX & ", " & pololuPosY)
        'now the horizontal position (Y):
        For j As Integer = 1 To nbyn
            'Console.WriteLine(j & " mod 2 is " & (j Mod 2))
            'If (j Mod 2) <> 0 Then   'this is j odd 

            ' Console.WriteLine(j & " is odd")


            If j = 1 Then
                'do nothing because we already have added the first position
            Else
                'add a preposition on x-1 to avoid backlash
                ListBox3.Items.Add(pololuPosY - 1)
                My.Settings.PositionsY.Add(pololuPosY - 1)
            End If

            For i As Integer = 0 To nbyn - 1
                If j = 1 And i = 0 Then
                    'do nothing because we already have added the first position
                Else
                    ListBox3.Items.Add(pololuPosY + i)
                    My.Settings.PositionsY.Add(pololuPosY + i)
                End If
            Next
            'Else  'this is j even
            '    ' Console.WriteLine(j & " is even")
            '    For i As Integer = nbyn - 1 To 0 Step -1
            '        ListBox3.Items.Add(pololuPosY + i)
            '        My.Settings.PositionsY.Add(pololuPosY + i)
            '    Next
            'End If
        Next

        'now the vertical position (X):
        For j As Integer = 1 To nbyn
            'If (j Mod 2) <> 0 Then   'this is j odd 



            If j = 1 Then
                'do nothing because we already have added the first position
            Else
                'add a preposition on x-1 to avoid backlash
                ListBox2.Items.Add(pololuPosX - 1)
                My.Settings.PositionsX.Add(pololuPosX - 1)
            End If



            For i As Integer = 1 To nbyn
                If j = 1 And i = 1 Then
                    'do nothing because we already have added the first position
                Else
                    ListBox2.Items.Add(pololuPosX - (j - 1))
                    My.Settings.PositionsX.Add(pololuPosX - (j - 1))
                End If
            Next

            'Else

            'For i As Integer = nbyn To 1 Step -1
            '    ListBox2.Items.Add(pololuPosX - (j - 1))
            '    My.Settings.PositionsX.Add(pololuPosX - (j - 1))
            'Next

            'End If
        Next


        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()













        'AddCurrentPosPololu()
        ''MsgBox(pololuPosX & ", " & pololuPosY)
        'For j As Integer = 1 To nbyn
        '    'Console.WriteLine(j & " mod 2 is " & (j Mod 2))
        '    If (j Mod 2) <> 0 Then   'this is j odd 

        '        ' Console.WriteLine(j & " is odd")
        '        For i As Integer = 0 To nbyn - 1
        '            If j = 1 And i = 0 Then
        '                'do nothing because we already have added the first position
        '            Else
        '                ListBox3.Items.Add(pololuPosY + i)
        '                My.Settings.PositionsY.Add(pololuPosY + i)
        '            End If
        '        Next
        '    Else  'this is j even
        '        ' Console.WriteLine(j & " is even")
        '        For i As Integer = nbyn - 1 To 0 Step -1
        '            ListBox3.Items.Add(pololuPosY + i)
        '            My.Settings.PositionsY.Add(pololuPosY + i)
        '        Next
        '    End If
        'Next

        'For j As Integer = 1 To nbyn
        '    If (j Mod 2) <> 0 Then   'this is j odd 

        '        For i As Integer = 1 To nbyn
        '            If j = 1 And i = 1 Then
        '                'do nothing because we already have added the first position
        '            Else
        '                ListBox2.Items.Add(pololuPosX - (j - 1))
        '                My.Settings.PositionsX.Add(pololuPosX - (j - 1))
        '            End If
        '        Next

        '    Else

        '        For i As Integer = nbyn To 1 Step -1
        '            ListBox2.Items.Add(pololuPosX - (j - 1))
        '            My.Settings.PositionsX.Add(pololuPosX - (j - 1))
        '        Next

        '    End If
        'Next

        'My.Settings.Save()
        'ListBox1.Items.Clear()
        'loaddata()
    End Sub

    Private Sub Button79_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button79.Click
        lightTestON()
    End Sub

    Private Sub Button82_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button82.Click
        lightsOFF()
    End Sub

    'Private Sub CheckBox10_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox10.CheckedChanged
    '    If CheckBox10.Checked Then
    '        pololumode = True
    '        Label18.Text = "Pololu"
    '        My.Settings.pololumode = True
    '        Panel5.Visible = True

    '        'select ms-4 because it has the right orientation for pololu
    '        My.Settings.versionMS2000 = False
    '        'distancHorizontal = "-1241"
    '        'distancVertical = "-621"
    '        Label10.Text = "Using Stage MS-4..."

    '        My.Settings.Save()
    '        versionMS2000 = My.Settings.versionMS2000

    '    Else
    '        pololumode = False
    '        If IncuLeft Then
    '            Label18.Text = "Left Version"
    '            '  Me.Text = Me.Text & " Left Version"
    '            'SerialPort1.PortName = "COM3"
    '        Else
    '            Label18.Text = "Right Version"
    '            ' Me.Text = Me.Text & " Right Version"
    '            'SerialPort1.PortName = "COM5"
    '        End If
    '        My.Settings.pololumode = False
    '        My.Settings.Save()
    '        'MsgBox("pololu mode set to false")
    '    End If

    'End Sub

    Private Sub Button83_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button83_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button83.Click
        If Panel5.Visible = True Then
            Panel5.Visible = False
        Else
            Panel5.Visible = True
        End If
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged
        pololusteps = TextBox8.Text
        My.Settings.pololusteps = pololusteps
        My.Settings.Save()

        If CheckBox11.Checked Then
            pololustepsY = pololusteps * 0.744
            TextBox10.Text = pololustepsY
            My.Settings.pololustepsY = pololustepsY
            My.Settings.Save()
        End If
    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        Try
            pololurepetitions = TextBox9.Text
        Catch ex As Exception
            pololurepetitions = 1
            TextBox9.Text = "1"
        End Try

        My.Settings.pololurepetitions = pololurepetitions
        My.Settings.Save()
    End Sub


    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        pololustepsY = TextBox10.Text
        My.Settings.pololustepsY = pololustepsY
        My.Settings.Save()
    End Sub

    Private Sub ListBox1_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseDoubleClick

        Label42.Text = (ListBox1.SelectedIndex + 1).ToString
        currentListItem = Label42.Text - 1
        If DCmotors.Checked Then
            gotoselectedposDC()

        ElseIf pololumode Then
            gotoselectedposPololu()
        ElseIf mcl Then
            gotoselectedposMcl()

        Else
            gotoselectedpos()
            'gotoselectedposWithoutTimer()
        End If
    End Sub

    Dim PictureBox3Showed As Boolean = False

    Private Sub LinkLabel8_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel8.LinkClicked
        showzoom()
    End Sub

    Private Sub LinkLabel9_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel9.LinkClicked
        showzoom()
    End Sub

    Sub showzoom()
        If PictureBox3Showed Then
            GroupBox9.Hide()
            PictureBox3Showed = False
            'MsgBox("hiding")
        Else
            GroupBox9.Show()
            PictureBox3Showed = True
            'MsgBox("showing")
        End If
    End Sub


    Sub addtomyConsole(ByVal value As String)
        If ListBox8.Items.Count > 150 Then
            ListBox8.Items.RemoveAt(0)
        End If
        ListBox8.Items.Add(value)
        ListBox8.SelectedIndex = (ListBox8.Items.Count - 1)
        ListBox8.Refresh()
    End Sub

    Sub addtomyConsoleMain(ByVal value As String)
        If ListBox9.Items.Count > 50 Then
            ListBox9.Items.RemoveAt(0)
        End If
        ListBox9.Items.Add(value)
        ListBox9.SelectedIndex = (ListBox9.Items.Count - 1)
        ListBox9.Refresh()
        'addtomyConsole(value)
        writetoTextFile(value)
    End Sub

    Sub addtomyConsoleErrorMessages(ByVal value As String)
        If ListBox10.Items.Count > 50 Then
            ListBox10.Items.RemoveAt(0)
        End If
        ListBox10.Items.Add(value)
        ListBox10.SelectedIndex = (ListBox10.Items.Count - 1)
        ListBox10.Refresh()
    End Sub

    Private Sub Button37_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button37.Click
        getStageParameters()
    End Sub

    Sub getStageParameters()

        mytext = SerialPort1.ReadExisting
        Str = "minspeed"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
            addtomyConsole("error")
        End Try
        addtomyConsole(Str)
        Threading.Thread.Sleep(200)
        mytext = SerialPort1.ReadExisting
        mychar = {":", "A"}
        mytext = mytext.TrimStart(mychar)
        TextBox19.Text = mytext
        TextBox19.Refresh()
        addtomyConsole(mytext)
        Threading.Thread.Sleep(200)

        Str = "speed"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
            addtomyConsole("error")
        End Try
        addtomyConsole(Str)
        Threading.Thread.Sleep(200)
        mytext = SerialPort1.ReadExisting
        mychar = {":", "A"}
        mytext = mytext.TrimStart(mychar)
        TextBox20.Text = mytext
        TextBox20.Refresh()
        addtomyConsole(mytext)
        Threading.Thread.Sleep(200)

        Str = "rampslope"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
            addtomyConsole("error")
        End Try
        addtomyConsole(Str)
        Threading.Thread.Sleep(200)
        mytext = SerialPort1.ReadExisting
        mychar = {":", "A"}
        mytext = mytext.TrimStart(mychar)
        TextBox21.Text = mytext
        TextBox21.Refresh()
        addtomyConsole(mytext)


        Str = "units"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
            addtomyConsole("error")
        End Try
        addtomyConsole(Str)
        Threading.Thread.Sleep(200)
        mytext = SerialPort1.ReadExisting
        mychar = {":", "A"}
        mytext = mytext.TrimStart(mychar)
        TextBox22.Text = mytext

        addtomyConsole(mytext)
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged  'set high speed temporarily checkbox
        If CheckBox3.Checked Then
            CheckBox3.Refresh()
            TextBox19.Text = My.Settings.minspeedHIGH
            TextBox19.Refresh()
            TextBox20.Text = My.Settings.maxspeedHIGH
            TextBox20.Refresh()
            TextBox21.Text = My.Settings.rampslopeHIGH
            TextBox21.Refresh()
            TextBox22.Text = My.Settings.UNITS
            setallStageValues()
            My.Settings.StageHiSpeed = True
        Else
            CheckBox3.Refresh()
            TextBox19.Text = My.Settings.minspeedLOW
            TextBox19.Refresh()
            TextBox20.Text = My.Settings.maxspeedLOW
            TextBox20.Refresh()
            TextBox21.Text = My.Settings.rampslopeLOW
            TextBox21.Refresh()
            TextBox22.Text = My.Settings.UNITS
            setallStageValues()
            My.Settings.StageHiSpeed = False
        End If
    End Sub

    Private Sub LinkLabel10_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel10.LinkClicked
        ListBox8.Items.Clear()
    End Sub

    'Sub QueryResponseThread()
    '    addtomyConsole("waiting........")
    '    thread1 = New Thread(AddressOf queryResponse)
    '    thread1.Start()
    'End Sub

    'Sub queryResponse()
    '    num = 0
    '    Thread.Sleep(4)
    '    mytext = ""
    '    While mytext.Contains("A") = False
    '        mytext = SerialPort1.ReadExisting
    '        If mytext <> "" Then
    '            addtomyConsole(num)
    '            addtomyConsole(mytext)
    '        End If

    '        Thread.Sleep(4)
    '        num = num + 1
    '        If num = 300 Then
    '            addtomyConsole("time out")
    '            addtomyConsole("no response")
    '            Exit While
    '        End If

    '    End While
    '    addtomyConsole("done")
    '    WaitEvent.Set()
    '    ' thread1 = Nothing
    'End Sub

    Private Sub Button38_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button38.Click   'Sets stage parameters
        setallStageValues()
    End Sub

    Sub setallStageValues()
        Try
            setminspeed()
            setmaxspeed()
            setrampslope()
            setunits()
        Catch ex As Exception
            If CheckBox23.Checked Then
            Else
                MsgBox(" can't connect to stage, possibly uncheck test checkbox")
            End If
        End Try

    End Sub

    Private Sub Button44_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button44.Click  'set minspeed
        setminspeed()
    End Sub

    Sub setminspeed()
        If TextBox19.Text < 50 Then
            MsgBox("Minspeed cannot be lower than 50!")
        End If
        If (CInt(TextBox19.Text) <= CInt(TextBox20.Text)) Then
            MsgBox("Minspeed cannot be a lower or equal value to maxspeed!")
        End If
        Str = "minspeed "
        Str += TextBox19.Text
        Str += " "
        Str += ControlChars.Cr
        'Str += " \r"
        addtomyConsole(Str)
        addtomyConsoleMain(Str)
        SerialPort1.DiscardInBuffer()
        SerialPort1.DiscardOutBuffer()
        SerialPort1.Write(Str)

        'QueryResponseThread()
        'WaitEvent.WaitOne()  ' this waits for the previous thread to end.
        Threading.Thread.Sleep(200)
        Str = SerialPort1.ReadExisting
        addtomyConsole(Str)
        If CheckBox3.Checked Then
            My.Settings.minspeedHIGH = TextBox19.Text
        Else
            My.Settings.minspeedLOW = TextBox19.Text
        End If
        My.Settings.Save()

    End Sub

    Private Sub Button49_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button49.Click   'set maxspeed
        setmaxspeed()
    End Sub

    Sub setmaxspeed()
        If (CInt(TextBox19.Text) <= CInt(TextBox20.Text)) Then
            MsgBox("Minspeed cannot be a lower or equal value to maxspeed!")
        End If

        Str = "speed "
        Str += TextBox20.Text
        Str += " "
        Str += ControlChars.Cr
        'Str += "\r"

        SerialPort1.DiscardInBuffer()
        SerialPort1.DiscardOutBuffer()
        SerialPort1.Write(Str)

        addtomyConsole(Str)
        Threading.Thread.Sleep(200)
        Str = SerialPort1.ReadExisting
        addtomyConsole(Str)
        If CheckBox3.Checked Then
            My.Settings.maxspeedHIGH = TextBox20.Text
        Else
            My.Settings.maxspeedLOW = TextBox20.Text
        End If
        My.Settings.Save()
    End Sub

    Private Sub Button45_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button45.Click 'set rampslope
        setrampslope()
    End Sub

    Sub setrampslope()
        Str = "rampslope "
        Str += TextBox21.Text
        Str += " "
        Str += ControlChars.Cr
        'Str += "\r"
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
            addtomyConsole("error")
        End Try
        addtomyConsole(Str)
        Threading.Thread.Sleep(200)
        Str = SerialPort1.ReadExisting
        addtomyConsole(Str)
        If CheckBox3.Checked Then
            My.Settings.rampslopeHIGH = TextBox21.Text
        Else
            My.Settings.rampslopeLOW = TextBox21.Text
        End If
        My.Settings.Save()
    End Sub

    Private Sub Button36_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button36.Click  'set units
        setunits()
    End Sub

    Sub setunits()
        Str = "units "
        Str += TextBox22.Text
        Str += ControlChars.Cr
        'Str += "\r"
        SerialPort1.DiscardInBuffer()
        SerialPort1.DiscardOutBuffer()
        SerialPort1.Write(Str)
        addtomyConsole(Str)
        Threading.Thread.Sleep(200)
        Str = SerialPort1.ReadExisting
        addtomyConsole(Str)
        My.Settings.UNITS = TextBox22.Text
        My.Settings.Save()
    End Sub






    Private Sub LinkLabel11_LinkClicked_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel11.LinkClicked
        'MsgBox(directoryInfo.FullName & "\ConfigAndErrorFiles\")
        'Process.Start(directoryInfo.FullName & "\ConfigAndErrorFiles")
        'MsgBox(directoryInfo.FullName & "\ConfigAndErrorFiles")
        Process.Start(directoryInfo.FullName & "\ConfigAndErrorFiles")
    End Sub

    Private Sub CheckBox15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox15.CheckedChanged
        If CheckBox15.Checked Then
            My.Settings.checkb15 = True
        Else
            My.Settings.checkb15 = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox17.CheckedChanged
        If CheckBox17.Checked Then
            My.Settings.checkb17 = True
        Else
            My.Settings.checkb17 = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox14.CheckedChanged
        If CheckBox14.Checked Then
            My.Settings.checkb14 = True
        Else
            My.Settings.checkb14 = False
        End If
        My.Settings.Save()
    End Sub



    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked Then
            My.Settings.checkb5 = True
        Else
            My.Settings.checkb5 = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            My.Settings.checkb1 = True
        Else
            My.Settings.checkb1 = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            My.Settings.checkb4 = True
        Else
            My.Settings.checkb4 = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox19_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox19.CheckedChanged
        If CheckBox19.Checked Then
            My.Settings.checkb19 = True
        Else
            My.Settings.checkb19 = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox20.CheckedChanged
        If CheckBox20.Checked Then
            My.Settings.checkb20 = True
        Else
            My.Settings.checkb20 = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub RadioButton20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton20.CheckedChanged   'MS4new radiobutton
        If RadioButton20.Checked Then
            My.Settings.radiob20 = True

            TextBox23.Text = My.Settings.largestepMS4
            TextBox24.Text = My.Settings.smallstepMS4
            TextBox25.Text = My.Settings.tinystepMS4
        Else
            My.Settings.radiob20 = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub RadioButton25_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton25.CheckedChanged
        If RadioButton25.Checked Then
            My.Settings.radiob25 = True


        Else
            My.Settings.radiob25 = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox21_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox21.CheckedChanged
        If CheckBox21.Checked Then
            My.Settings.checkb21 = True
        Else
            My.Settings.checkb21 = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub RadioButton26_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton26.CheckedChanged
        If RadioButton26.Checked Then
            My.Settings.drive = "C:"
        End If
        My.Settings.Save()
        drive = My.Settings.drive
        imagefolder = drive & "\Images"
    End Sub

    Private Sub RadioButton27_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton27.CheckedChanged
        If RadioButton27.Checked Then
            My.Settings.drive = "S:"
        End If
        My.Settings.Save()
        drive = My.Settings.drive
        imagefolder = drive & "\Images"
    End Sub



    Private Sub RadioButton24_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton24.CheckedChanged
        If RadioButton24.Checked Then
            My.Settings.mcl = True

            TextBox23.Text = My.Settings.largestepMCL
            TextBox24.Text = My.Settings.smallstepMCL
            TextBox25.Text = My.Settings.tinystepMCL
        Else
            My.Settings.mcl = False
        End If
        My.Settings.Save()
        mcl = My.Settings.mcl '




    End Sub



    Private Sub TextBox23_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox23.TextChanged
        If RadioButton20.Checked Then  'ms4new radiobutton
            My.Settings.largestepMS4 = TextBox23.Text

        ElseIf RadioButton24.Checked Then  'mcl radiobutton
            My.Settings.largestepMCL = TextBox23.Text


        End If
        My.Settings.Save()
    End Sub

    Private Sub TextBox24_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox24.TextChanged
        If RadioButton20.Checked Then  'ms4new radiobutton

            My.Settings.smallstepMS4 = TextBox24.Text

        ElseIf RadioButton24.Checked Then  'mcl radiobutton

            My.Settings.smallstepMCL = TextBox24.Text

        End If
        My.Settings.Save()
    End Sub

    Private Sub TextBox25_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox25.TextChanged
        If RadioButton20.Checked Then  'ms4new radiobutton

            My.Settings.tinystepMS4 = TextBox25.Text

        ElseIf RadioButton24.Checked Then  'mcl radiobutton

            My.Settings.tinystepMCL = TextBox25.Text

        End If
        My.Settings.Save()
    End Sub



    Private Sub CheckBox23_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox23.CheckedChanged
        If CheckBox23.Checked Then
            My.Settings.testmode = True
            'Label18.Text = "Test Mode"
            testmode = True
            'MsgBox(My.Settings.testmode)
        ElseIf CheckBox23.Checked = False Then
            My.Settings.testmode = False
            testmode = False
            'MsgBox(My.Settings.testmode)
        End If
        My.Settings.Save()
    End Sub



    Private Sub Button50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button50.Click
        Dim n As Integer = TextBox26.Text.ToString

        While n > 0
            Try
                ListBox1.SelectedIndex = (ListBox1.Items.Count - 1)
                My.Settings.PositionsX.RemoveAt(ListBox1.SelectedIndex())
                My.Settings.PositionsY.RemoveAt(ListBox1.SelectedIndex())
                ListBox2.Items.RemoveAt(ListBox1.SelectedIndex())
                ListBox3.Items.RemoveAt(ListBox1.SelectedIndex())
                My.Settings.Save()
                ListBox1.Items.Clear()
                loaddata()
            Catch ex As Exception
                MessageBox.Show("Can't load data")
            End Try
            ' Thread.Sleep(300)
            n = n - 1
        End While
    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked Then
            My.Settings.checkb8 = True
        Else
            My.Settings.checkb8 = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Try
            Label32.Text = TextBox3.Text
        Catch ex As Exception
        End Try
    End Sub


    Private Sub Button57_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button57.Click
        enableMotor()
    End Sub

    Private Sub Button56_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button56.Click
        disableMotor()
    End Sub

    Sub enableMotor()
        Button56.FlatStyle = FlatStyle.Standard
        Button57.FlatStyle = FlatStyle.Flat
        ArduinoPin("e")
        'If DCmotors.Checked Then
        '    ArduinoPin("e")
        'Else
        '    moveStepper(3, 0, 0)
        'End If
    End Sub

    Sub disableMotor()
        Button56.FlatStyle = FlatStyle.Flat
        Button57.FlatStyle = FlatStyle.Standard
        ArduinoPin("d")
        'If DCmotors.Checked Then
        '    ArduinoPin("d")
        'Else
        '    moveStepper(2, 0, 0)
        'End If
        'MsgBox("motor disabled")
    End Sub

    Private Sub TextBox27_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox27.TextChanged
        Try
            If TextBox27.Text <> "" Then
                If RadioButton6.Checked Then
                    If cam1selected Then  'cam1 radiobutton
                        shutterBF.absValue = Convert.ToSingle(TextBox27.Text)
                        My.Settings.shutterBF = shutterBF.absValue
                        cam.SetProperty(shutterBF)
                    ElseIf cam2selected Then  'else cam2 radiobutton is selected so:
                        shutterBFb.absValue = Convert.ToSingle(TextBox27.Text)
                        My.Settings.shutterBFb = shutterBFb.absValue
                        cam.SetProperty(shutterBFb)
                    End If
                End If
                If RadioButton7.Checked Then
                    If cam1selected Then  'cam1 radiobutton
                        shutterBF2.absValue = Convert.ToSingle(TextBox27.Text)
                        My.Settings.shutterBF2 = shutterBF2.absValue
                        cam.SetProperty(shutterBF2)
                    ElseIf cam2selected Then 'else cam2 radiobutton is selected so:
                        shutterBF2b.absValue = Convert.ToSingle(TextBox27.Text)
                        My.Settings.shutterBF2b = shutterBF2b.absValue
                        cam.SetProperty(shutterBF2b)
                    End If
                End If
                If RadioButton8.Checked Then
                    If cam1selected Then  'cam1 radiobutton
                        shutterfluo.absValue = Convert.ToSingle(TextBox27.Text)
                        My.Settings.shutterfluo = shutterfluo.absValue
                        cam.SetProperty(shutterfluo)
                    ElseIf cam2selected Then 'else cam2 radiobutton is selected so:
                        shutterfluob.absValue = Convert.ToSingle(TextBox27.Text)
                        My.Settings.shutterfluob = shutterfluob.absValue
                        cam.SetProperty(shutterfluob)
                    End If

                End If
                If RadioButton9.Checked Then
                    If cam1selected Then  'cam1 radiobutton
                        shutterDarkF.absValue = Convert.ToSingle(TextBox27.Text)
                        My.Settings.shutterDarkF = shutterDarkF.absValue
                        cam.SetProperty(shutterDarkF)
                    ElseIf cam2selected Then 'else cam2 radiobutton is selected so:
                        shutterDarkFb.absValue = Convert.ToSingle(TextBox27.Text)
                        My.Settings.shutterDarkFb = shutterDarkFb.absValue
                        cam.SetProperty(shutterDarkFb)
                    End If
                End If
            End If
        Catch ex As Exception
            If CheckBox23.Checked Then  'Test radiobutton.
            Else
                'MsgBox("Camera not connected " & ex.Message)
                timedMessage = "Camera not connected"
                addtomyConsoleErrorMessages(timedMessage)
                time = 4
                FormTimedDialog.Show()

            End If

        End Try
    End Sub

    Dim loadingForm As Boolean = True
    Private Sub RadioButton6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton6.CheckedChanged
        'don't count the first checked change that occurs in the loading form as checked change:
        If loadingForm = False Then
            lightBFchecked()
        End If
        loadingForm = False  'this way from now on it will be false (while it was true while loading)
    End Sub
    Sub lightBFchecked()
        If RadioButton6.Checked Then
            Try
                If cam1selected Then 'this is camera1
                    cam.SetProperty(shutterBF)
                    TextBox27.Text = shutterBF.absValue.ToString
                ElseIf cam2selected Then
                    cam.SetProperty(shutterBFb)
                    TextBox27.Text = shutterBFb.absValue.ToString
                End If
            Catch ex As Exception
                timedMessage = "Camera not connected"
                addtomyConsoleErrorMessages(timedMessage)
                time = 4
                FormTimedDialog.Show()
            End Try
            TextBox27.Refresh()

            restartLiveONifNeeded()
        End If
    End Sub
    Private Sub RadioButton7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton7.CheckedChanged
        lightBF2checked()
    End Sub
    Sub lightBF2checked()
        If RadioButton7.Checked Then
            Try
                If cam1selected Then 'this is camera1
                    cam.SetProperty(shutterBF2)
                    TextBox27.Text = shutterBF2.absValue.ToString
                ElseIf cam2selected Then
                    cam.SetProperty(shutterBF2b)
                    TextBox27.Text = shutterBF2b.absValue.ToString
                End If
            Catch ex As Exception
                timedMessage = "Camera not connected"
                addtomyConsoleErrorMessages(timedMessage)
                time = 4
                FormTimedDialog.Show()
            End Try
            TextBox27.Refresh()
        End If
    End Sub
    Private Sub RadioButton8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton8.CheckedChanged
        lightFluoChecked()
    End Sub
    Sub lightFluoChecked()
        If RadioButton8.Checked Then
            Try
                If cam1selected Then 'this is camera1
                    cam.SetProperty(shutterfluo)
                    TextBox27.Text = shutterfluo.absValue.ToString
                ElseIf cam2selected Then
                    cam.SetProperty(shutterfluob)
                    TextBox27.Text = shutterfluob.absValue.ToString
                End If

            Catch ex As Exception
                timedMessage = "Camera not connected"
                addtomyConsoleErrorMessages(timedMessage)
                time = 4
                FormTimedDialog.Show()
            End Try
            TextBox27.Refresh()

            restartLiveONifNeeded()
        End If
    End Sub
    Private Sub RadioButton9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton9.CheckedChanged
        lightDarkFChecked()
    End Sub

    Sub lightDarkFChecked()
        If RadioButton9.Checked Then
            Try
                If cam1selected Then 'this is camera1
                    cam.SetProperty(shutterDarkFb)
                    TextBox27.Text = shutterDarkF.absValue.ToString
                ElseIf cam2selected Then
                    cam.SetProperty(shutterDarkFb)
                    TextBox27.Text = shutterDarkFb.absValue.ToString
                End If
            Catch ex As Exception
                timedMessage = "Camera not connected"
                addtomyConsoleErrorMessages(timedMessage)
                time = 4
                FormTimedDialog.Show()
            End Try
            TextBox27.Refresh()

            restartLiveONifNeeded()
        End If





    End Sub


    Sub restartLiveONifNeeded()
        If stopThread = False Then  'live is on, turn it off and then on again to reset light source:
            stopThread = True
            lightsOFF()
            TimerLiveRestart.Start()
        End If

    End Sub


    Private bmpScreenShot As Bitmap
    Private gfxScreenshot As Graphics




    Private Sub RadioButton23_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton23.CheckedChanged
        If RadioButton23.Checked Then
            My.Settings.radiob23 = True
            versionMS2000 = True

            TextBox23.Text = My.Settings.largestepMS2000
            TextBox24.Text = My.Settings.smallstepMS2000
            TextBox25.Text = My.Settings.tinystepMS2000
        Else
            My.Settings.radiob23 = False
            versionMS2000 = False
        End If
        My.Settings.Save()
    End Sub


    Private Sub Button65_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button65.Click
        addzeroas1st()
    End Sub

    Sub addzeroas1st()
        Try
            mytext = "0"
            ListBox2.Items.Insert(0, mytext)

            My.Settings.PositionsX.Insert(0, mytext)
        Catch ex As Exception
            MessageBox.Show("my error345")
        End Try
        Try
            mytext = "0"
            ListBox2.Items.Insert(0, mytext)
            My.Settings.PositionsY.Insert(0, mytext)
        Catch ex As Exception
        End Try
        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()
        'drawPostions()
    End Sub

    Private Sub Button77_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button77.Click 'add current pos as 1st in list and recalculate other positions

        Try
            SerialPort1.DiscardInBuffer()

        Catch ex As Exception

        End Try
        Str = "w x"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)

        Catch ex As Exception

        End Try

        Threading.Thread.Sleep(100)
        'Label29.Text = SerialPort1.ReadExisting
        mychar = {":", "A"}

        'Dim mytext As String
        Try
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)


        Catch ex As Exception
            MessageBox.Show("my error345")
        End Try
        For i = 0 To ListBox2.Items.Count - 1
            ListBox2.Items.Item(i) = ListBox2.Items.Item(i) - CInt(mytext)
            My.Settings.PositionsX.Item(i) = ListBox2.Items.Item(i).ToString
        Next


        Str = "w y"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)

        Catch ex As Exception

        End Try

        Threading.Thread.Sleep(100)
        Try
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)

        Catch ex As Exception

        End Try

        For i = 0 To ListBox3.Items.Count - 1
            ListBox3.Items.Item(i) = ListBox3.Items.Item(i) - CInt(mytext)
            My.Settings.PositionsY.Item(i) = ListBox3.Items.Item(i).ToString
        Next


        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()

        addzeroas1st()
        Zero()
        'drawPostions()
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Dim loadposcount As Integer = 1
    Private Sub Button85_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button85.Click
        loadposcount = loadposcount + 1
        If loadposcount = 6 Then
            loadposcount = 1
        End If

        If loadposcount = 1 Then
            loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
        ElseIf loadposcount = 2 Then
            loaddatafromRelease("SAVED2X.txt", "SAVED2Y.txt")
        ElseIf loadposcount = 3 Then
            loaddatafromRelease("SAVED3X.txt", "SAVED3Y.txt")
        ElseIf loadposcount = 4 Then
            loaddatafromRelease("SAVED4X.txt", "SAVED4Y.txt")
        ElseIf loadposcount = 5 Then
            loaddatafromRelease("SAVED5X.txt", "SAVED5Y.txt")

        End If



    End Sub

    Private Sub TextBox17_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox17.TextChanged
        configName = TextBox17.Text
        saveConfigtxt(directoryInfo.FullName & "\ConfigAndErrorFiles") 'save Config.txt to Release folder
        My.Settings.configname = configName
        My.Settings.Save()
    End Sub

    Private Sub TextBox14_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox14.TextChanged
        mywidth = TextBox14.Text
        saveConfigtxt(directoryInfo.FullName & "\ConfigAndErrorFiles") 'save Config.txt to Release folder
        ' MsgBox(mywidth)
        My.Settings.mywidth = mywidth
        My.Settings.Save()
    End Sub

    Private Sub TextBox13_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox13.TextChanged
        myheigth = TextBox13.Text
        saveConfigtxt(directoryInfo.FullName & "\ConfigAndErrorFiles") 'save Config.txt to Release folder
        My.Settings.myheight = myheigth
        My.Settings.Save()
    End Sub

    Private Sub TextBox16_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox16.TextChanged
        wellwidth = TextBox16.Text
        saveConfigtxt(directoryInfo.FullName & "\ConfigAndErrorFiles") 'save Config.txt to Release folder
        My.Settings.wellwidth = wellwidth
        My.Settings.Save()
    End Sub

    Private Sub TextBox15_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox15.TextChanged
        wellheight = TextBox15.Text
        saveConfigtxt(directoryInfo.FullName & "\ConfigAndErrorFiles") 'save Config.txt to Release folder
        My.Settings.wellheight = wellheight
        My.Settings.Save()
    End Sub

    Dim loadconfigcount
    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click 'load other Configs button
        loadconfigcount = loadconfigcount + 1
        If loadconfigcount = 6 Then
            loadconfigcount = 1
        End If

        If loadconfigcount = 1 Then
            loadConfigfromRelease(1)
        ElseIf loadconfigcount = 2 Then
            loadConfigfromRelease(2)
        ElseIf loadconfigcount = 3 Then
            loadConfigfromRelease(3)
        ElseIf loadconfigcount = 4 Then
            loadConfigfromRelease(4)
        ElseIf loadconfigcount = 5 Then
            loadConfigfromRelease(5)
        End If


        ' MsgBox(mywidth)
    End Sub



    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Label27.Hide()
        Label50.Hide()
        Label57.Hide()
        ListBox10.Items.Clear()
    End Sub




    Dim autofocusatposition1 As Boolean

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked Then
            autofocusatposition1 = True
        Else
            autofocusatposition1 = False
        End If
    End Sub

    Dim positiontoautofocus As Integer
    Dim positiontoautofocus2 As Integer
    Dim positiontoautoalign As Integer
    Private Sub TextBox28_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox28.TextChanged
        positiontoautofocus = CInt(TextBox28.Text)
    End Sub


    Private Sub CheckBox16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox16.CheckedChanged


        If cam1selected Then 'cam1 checked
            If CheckBox16.Checked Then
                My.Settings.FlipYCam1 = True
            Else
                My.Settings.FlipYCam1 = False
            End If
            My.Settings.Save()
        End If
        If cam2selected Then 'cam2 checked
            If CheckBox16.Checked Then
                My.Settings.FlipYCam2 = True
            Else
                My.Settings.FlipYCam2 = False
            End If
            My.Settings.Save()
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked Then
            My.Settings.InvertMotor = True
        Else
            My.Settings.InvertMotor = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        My.Settings.FocusingLightType = ComboBox1.SelectedIndex
        My.Settings.Save()
    End Sub




    Private Sub TextBox29_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox29.TextChanged
        'the position to do 2nd alignment in addition to at Pos1 is:
        positiontoautoalign = CInt(TextBox29.Text)
    End Sub

    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        'delete files locally:
        Dim dirs As String() = Directory.GetDirectories(sessionfolder, "Pos*") 'selects all folders starting wth Pos
        Dim dir As String
        For Each dir In dirs
            Try
                System.IO.Directory.Delete(dir, True)
            Catch ex As Exception
                addtomyConsoleErrorMessages("Some file not deleted in Session folder")

            End Try

        Next
        'delete files in Vanadium:
        If vanadiumfolder <> "" Then
            Dim dirs2 As String() = Directory.GetDirectories(vanadiumfolder, "Pos*") 'selects all folders starting wth Pos
            Dim dir2 As String
            For Each dir2 In dirs2
                Try
                    System.IO.Directory.Delete(dir2, True)
                Catch ex As Exception
                    addtomyConsoleErrorMessages("Some file not deleted in Vanadium")
                End Try

            Next
        End If

        MsgBox("Finished deleting files in the session folder of this computer and Vanadium")

    End Sub

    Private Sub Button86_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button86.Click
        FormFocusing.Show()
    End Sub


    Private Sub TextBox30_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox30.TextChanged
        'the position to do 2nd focusing in addition to the one shown in the left panel is:
        positiontoautofocus2 = CInt(TextBox30.Text)
    End Sub

    Private Sub CheckBox27_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox27.CheckedChanged

        If cam1selected Then 'cam1 checked
            If CheckBox27.Checked Then
                My.Settings.FlipXCam1 = True
            Else
                My.Settings.FlipXCam1 = False
            End If
            My.Settings.Save()
        End If
        If cam2selected Then 'cam2 checked
            If CheckBox27.Checked Then
                My.Settings.FlipXCam2 = True
            Else
                My.Settings.FlipXCam2 = False
            End If
            My.Settings.Save()
        End If




    End Sub


    Private Sub LinkLabel5_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        Try
            myControlDialog2.Show()

        Catch ex As Exception

        End Try
    End Sub



    Public s As New System.IO.Ports.SerialPort
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click 'arduino
        s.Close()
        s.PortName = "com5"  'will need to change to your port number
        s.BaudRate = 9600
        s.DataBits = 8
        s.Parity = System.IO.Ports.Parity.None
        s.StopBits = System.IO.Ports.StopBits.One
        s.Handshake = System.IO.Ports.Handshake.None
        s.Encoding = System.Text.Encoding.Default 'very important!
        s.Open()
        s.RtsEnable = True

        s.Write(Chr(4))
        s.Close()
    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        s.Close()
        s.PortName = "com5"  'will need to change to your port number
        s.BaudRate = 9600
        s.DataBits = 8
        s.Parity = System.IO.Ports.Parity.None
        s.StopBits = System.IO.Ports.StopBits.One
        s.Handshake = System.IO.Ports.Handshake.None
        s.Encoding = System.Text.Encoding.Default 'very important!
        s.Open()
        s.RtsEnable = True

        s.Write(Chr(0))
        s.Close()
    End Sub

    Private Sub Button25_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
        Const k_cameraPower As UInteger = &H610
        Dim regVal As UInteger = 0
        regVal = cam.ReadRegister(k_cameraPower)
        MsgBox(regVal.ToString)
    End Sub

    Private Sub Button43_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button43.Click
        ' Power on the camera
        Const k_cameraPower As UInteger = &H610
        Const k_powerVal As UInt32 = &H80000000UI
        cam.WriteRegister(k_cameraPower, k_powerVal)

        ' Wait for camera to complete power-up
        Const k_millisecondsToSleep = 100
        Dim regVal As UInteger = 0

        Do While ((regVal And k_powerVal) = 0)
            System.Threading.Thread.Sleep(k_millisecondsToSleep)
            regVal = cam.ReadRegister(k_cameraPower)
        Loop
    End Sub

    Private Sub Button80_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button80.Click
        ' Power off the camera
        Const k_cameraPower As UInteger = &H610
        Const k_powerVal As UInt32 = &H0UI
        cam.WriteRegister(k_cameraPower, k_powerVal)
    End Sub

    Private Sub CheckBox28_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox28.CheckedChanged
        If CheckBox28.Checked Then
            My.Settings.ArduinoMode = True
        Else
            My.Settings.ArduinoMode = False
        End If
        My.Settings.Save()
    End Sub



    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        SerialPort1.Close()

        My.Settings.myserialport = ComboBox2.SelectedItem

        My.Settings.Save()
        SerialPort1.PortName = My.Settings.myserialport
    End Sub


    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged

        My.Settings.arduinoComPort = ComboBox3.SelectedItem

        My.Settings.Save()
        SerialPort.Close()

        'MsgBox("My.Settings.arduinoComPort is now" & My.Settings.arduinoComPort)
        ConnectSerial()

        'setupArduinoComport()
        ' s.PortName = My.Settings.arduinoComPort
    End Sub

    Private Sub CheckBox29_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox29.CheckedChanged
        If CheckBox29.Checked Then
            My.Settings.sendEmails = True
        Else
            My.Settings.sendEmails = False
        End If
        My.Settings.Save()
    End Sub


    Private Sub CheckBox30_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox30.CheckedChanged
        If CheckBox30.Checked Then
            My.Settings.tinkerkitmode = True
        Else
            My.Settings.tinkerkitmode = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub LinkLabel12_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel12.LinkClicked
        GrabandSaveAllSelected()
    End Sub

    Private Sub CheckBox25_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox25.CheckedChanged

    End Sub

    Private Sub CheckBox31_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox31.CheckedChanged
        If CheckBox31.Checked Then
            My.Settings.ArduinoIncuMode = True
        Else
            My.Settings.ArduinoIncuMode = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub CheckBox32_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox32.CheckedChanged
        If CheckBox32.Checked Then
            My.Settings.Cam1RED = True
        Else
            My.Settings.Cam1RED = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged

    End Sub

    Private Sub TextBox31_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox31.TextChanged
        If TextBox31.Text <> "" Then
            My.Settings.MotorSteps = CInt(TextBox31.Text)
            My.Settings.Save()
        End If
    End Sub

    Private Sub TextBox32_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox32.TextChanged
        If TextBox32.Text <> "" Then
            My.Settings.MotorStepsB = CInt(TextBox32.Text)
            My.Settings.Save()
        End If

    End Sub


    Private Sub RadioButton22_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton22.CheckedChanged
        'If RadioButton22.Checked Then

        '    CheckBox10.Checked = True
        '    pololumode = True
        '    My.Settings.radiob22 = True
        '    My.Settings.Save()
        'Else
        '    CheckBox10.Checked = False
        '    pololumode = False
        '    My.Settings.radiob22 = False
        '    My.Settings.Save()
        'End If

    End Sub

    Private Sub Label33_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label33.Click

    End Sub

    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged

    End Sub

    Private Sub CheckBox18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox18.CheckedChanged
        If CheckBox18.Checked Then
            My.Settings.PololuBacklash = True
        Else
            My.Settings.PololuBacklash = False
        End If
        My.Settings.Save()

    End Sub





    Sub motorforward()
        forward = True
        ArduinoPin(1)
    End Sub

    Sub motorbackward()
        forward = False
        ArduinoPin(2)
    End Sub


    Private Sub Button81_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button81.Click
        getCurrent()

    End Sub
    Sub getCurrent()
        TextBox33.Clear()
        ArduinoPin("c")
        'this now will lead to Sub SerialPort_DataReceived which is listening the port
    End Sub




    Dim WithEvents SerialPort As New IO.Ports.SerialPort

    'Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
    '    If SerialPort.IsOpen Then
    '        SerialPort.Write(txtSend.Text)
    '    Else
    '        ConnectSerial()
    '        SerialPort.Write(txtSend.Text)
    '    End If
    'End Sub

    Private Sub ConnectSerial()
        Try
            SerialPort.BaudRate = 115200
            'SerialPort.BaudRate = 1000000
            SerialPort.ReadBufferSize = 120000
            'SerialPort.PortName = "COM7" 'notice how the ports are named? they HAVE to have COM in front of the number
            SerialPort.PortName = My.Settings.arduinoComPort()
            SerialPort.Open()
            Thread.Sleep(100)
        Catch
            SerialPort.Close()
        End Try
    End Sub

    Delegate Sub myMethodDelegate(ByVal [text] As String)
    Dim myD1 As New myMethodDelegate(AddressOf myShowStringMethod)

    Sub myShowStringMethod(ByVal myString As String)
        'display text to our textbox called SerialText
        TextBox33.Clear() 'test
        TextBox33.AppendText(myString)
        'TextBox33.Text = myString

    End Sub

    Dim serialin As String
    Dim objWriter3 As System.IO.StreamWriter

    Dim motorIsStopped As Boolean = True
    Dim motorStatus As String
    Dim focusingMotorSoDontDisableIt As Boolean = False
    ' Private Sub SerialPort_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort.DataReceived
    ' motorIsStopped = True
    'Try
    '    motorStatus = SerialPort.ReadLine()
    '    addtomyConsole("Data:" & motorStatus)
    '    If motorStatus.Contains("Stop") Then
    '        motorIsStopped = True
    '        If focusingMotorSoDontDisableIt = False Then
    '            'disableMotor()
    '            addtomyConsole("Disabled")
    '        End If

    '    End If



    'Catch ex As Exception
    '    MsgBox(ex.Message)
    'End Try









    'Try
    '    serialin = SerialPort.ReadLine()
    '    addtomyConsole(serialin)
    'Catch ex As Exception
    '    MsgBox(ex.Message)
    'End Try



    'If DCmotors.Checked Then

    '    'Dim str As String = SerialPort.ReadExisting()
    '    serialin = SerialPort.ReadLine()
    '    'SerialPort.DiscardInBuffer() 'test
    '    'Invoke(myD1, serialin)
    '    Try
    '        TextBox33.Text = serialin
    '        If serialin.StartsWith("S") Then
    '            FILE_NAME = directoryInfo.FullName & "\IncomingSerial.txt"  'creates a text file
    '            objWriter3 = New System.IO.StreamWriter(FILE_NAME, True)
    '            objWriter3.WriteLine(TextBox33.Text)
    '            objWriter3.Close()
    '        End If

    '        'MsgBox(directoryInfo.FullName & "IncomingSerial.txt")
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    '    If UpdateCheckBox.Checked Then
    '        Try
    '            mytextArray = TextBox33.Text.Split("Y")
    '            X = mytextArray(0).Substring(2)
    '            Y = mytextArray(1).Substring(1)
    '            X = X.TrimEnd(" ")
    '            TextBox35.Text = X
    '            TextBox34.Text = Y
    '        Catch ex As Exception
    '        End Try
    '        If serialin.StartsWith("S") Then
    '            tempcount = tempcount + 1
    '            Label65.Text = tempcount
    '            mytextArray = serialin.Split(" ")
    '            mytextArray = mytextArray(2).Split(",")
    '            TextBox35.Text = mytextArray(0)
    '            TextBox34.Text = mytextArray(1)
    '        End If
    '    End If

    'End If
    '   End Sub
    Dim tempcount As Integer = 0
    Private Sub Button90_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'TextBox33.Clear()
        ArduinoPin("z") 'zero arduino total count
    End Sub
    Dim xtarget As Long
    Dim direction As String


    Dim forward As Boolean = True

    Private Sub Button92_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button92.Click
        buttonGo2()
    End Sub

    Sub buttonGo2()
        ArduinoPin("x" & TextBox35.Text & "y" & TextBox34.Text)
    End Sub

    Private Sub LinkLabel13_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel13.LinkClicked
        TimerTest.Interval = CInt(TextBox36.Text)  '16000 '26000
        TimerTest.Start()
        mytimertesttick()
    End Sub
    Dim testcount As Boolean = True
    Private Sub TimerTest_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerTest.Tick
        mytimertesttick()
    End Sub

    Sub mytimertesttick()
        If testcount = True Then
            currentListItem = 0
            GrabandSaveAllSelected()
            Thread.Sleep(1500)
            'xtarget = TextBox34.Text
            ' ArduinoPin(TextBox34.Text)
            buttonGo2()
            testcount = False

        Else
            currentListItem = 1
            GrabandSaveAllSelected()
            Thread.Sleep(1500)
            ''xtarget = TextBox35.Text
            ''ArduinoPin(TextBox35.Text)
            'buttonGo2()
            DcgotoZero()
            testcount = True

        End If

    End Sub

    Private Sub Button93_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button93.Click
        TimerSerial.Interval = 1
        TimerSerial.Start()
    End Sub

    Private Sub TimerSerial_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerSerial.Tick
        serialin = SerialPort.ReadLine()
        serialin = SerialPort.ReadLine() 'I do it twice to make sure I'm not getting a chunked value! (genius)
        SerialPort.DiscardInBuffer() 'test

        'Invoke(myD1, serialin)
        TextBox33.Text = serialin
    End Sub



    Private Sub Button96_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button96.Click
        Try
            SerialPort.Open()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button95_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button95.Click
        SerialPort.Close()
    End Sub

    Private Sub LinkLabel14_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel14.LinkClicked
        TimerTest.Stop()
    End Sub

    Private Sub TextBox34_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox34.TextChanged
        My.Settings.testPos1 = TextBox34.Text
        My.Settings.Save()
    End Sub

    Private Sub TextBox35_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox35.TextChanged
        My.Settings.testPos2 = TextBox35.Text
        My.Settings.Save()
    End Sub



    Private Sub Button87_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button87.Click
        DcgotoZero()
    End Sub

    Sub DcgotoZero()
        ArduinoPin("x0y0")
    End Sub



    Private Sub DCmotors_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DCmotors.CheckedChanged
        'If DCmotors.Checked Then
        '    My.Settings.DCmotors = True
        '    pololumode = False
        '    My.Settings.pololumode = False
        '    RadioButton22.Checked = False
        'Else
        '    My.Settings.DCmotors = False
        'End If
        'My.Settings.Save()
    End Sub

    Dim addDCvaluetoList As Boolean

    Dim mytextArray() As String
    Dim X As String
    Dim Y As String
    Private Sub TimerCurrentDC_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerCurrentDC.Tick
        TimerCurrentDC.Stop()
        mytextArray = TextBox33.Text.Split("Y")
        Try
            X = mytextArray(0).Substring(2)
            Y = mytextArray(1).Substring(1)
        Catch ex As Exception

        End Try

        X = X.TrimEnd(" ")
        ' MsgBox(X)
        '  MsgBox(Y)
        If addDCvaluetoList Then
            ListBox2.Items.Add(X)
            My.Settings.PositionsX.Add(X)
            ListBox3.Items.Add(Y)
            My.Settings.PositionsY.Add(Y)
            My.Settings.Save()
            ListBox1.Items.Clear()
            'loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
            loaddata()
        Else 'else dont add to list, instead move stage
            Try
                item = CInt(X) + item
                item2 = CInt(Y) + item2
            Catch ex As Exception

            End Try

            ArduinoPin("x" & item & "y" & item2)
        End If

    End Sub



    Private Sub Button11_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Label42.Text = (ListBox1.SelectedIndex + 1).ToString
        currentListItem = Label42.Text - 1
        If pololumode Then

            gotoselectedposPololu()
        ElseIf mcl Then

            gotoselectedposMcl()
        ElseIf DCmotors.Checked Then

            gotoselectedposDC()
        Else
            gotoselectedpos()
            'gotoselectedposWithoutTimer()
        End If
    End Sub

    Private Sub TextBox37_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox37.TextChanged
        My.Settings.FlCam1Pin = TextBox37.Text
        My.Settings.Save()
    End Sub

    Private Sub TextBox38_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox38.TextChanged
        My.Settings.FlCam2Pin = TextBox38.Text
        My.Settings.Save()
    End Sub
    Dim panel2shown As Boolean
    Private Sub Button97_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button97.Click
        If panel2shown Then
            Panel2.Hide()
            panel2shown = False
            My.Settings.Panel2Shown = False
        Else
            Panel2.Show()
            panel2shown = True
            My.Settings.Panel2Shown = True
        End If
        My.Settings.Save()
    End Sub

    Private Sub Button88_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button88.Click
        panel2shown = False
        Panel2.Visible = False
        My.Settings.Panel2Shown = False
        My.Settings.Save()
    End Sub

    Private Sub TimerMotorsOffin5sec_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerMotorsOffin5sec.Tick
        'TimerMotorsOffin5sec.Stop()
        'disableMotor()
    End Sub

    Private Sub Button89_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button89.Click
        MsgBox(pololumode.ToString)
    End Sub

    Private Sub Button90_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button90.Click
        FormFocusingRecorded.Show()
    End Sub

    Private Sub RecordFocus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecordFocus.CheckedChanged

        If RecordFocus.Checked Then
            Label83.Text = "Recording Focus Positions.."
            Label83.Visible = True
        Else
            Label83.Visible = False
        End If


    End Sub


    Private Sub SwitchX_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwitchX.CheckedChanged
        If SwitchX.Checked Then
            My.Settings.SwitchX = True
        Else
            My.Settings.SwitchX = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub SwitchY_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwitchY.CheckedChanged
        If SwitchY.Checked Then
            My.Settings.SwitchY = True
        Else
            My.Settings.SwitchY = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub SwitchxCheckbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwitchxCheckbox.CheckedChanged
        If SwitchxCheckbox.Checked Then
            My.Settings.SwitchxCheckbox = True
        Else
            My.Settings.SwitchxCheckbox = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub SwitchyCheckbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwitchyCheckbox.CheckedChanged
        If SwitchyCheckbox.Checked Then
            My.Settings.SwitchyCheckbox = True
        Else
            My.Settings.SwitchyCheckbox = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub Button94_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button94.Click
        'savePositionstxt(directoryInfo.FullName & "\ConfigAndErrorFiles")
        'Saves in the current directory
        Dim mydir As String = directoryInfo.FullName & "\ConfigAndErrorFiles"

        FILE_NAME = mydir & "\SAVEDXA.txt"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, False)
        For i As Integer = 0 To My.Settings.PositionsX.Count - 1
            objWriter.WriteLine(My.Settings.PositionsX.Item(i))

        Next
        objWriter.Close()

        Dim FILE_NAME2 As String = mydir & "\SAVEDYA.txt"
        Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME2, False)
        For i As Integer = 0 To My.Settings.PositionsY.Count - 1
            objWriter2.WriteLine(My.Settings.PositionsY.Item(i))

        Next


        objWriter2.Close()
    End Sub

    Private Sub Button91_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button91.Click
        Dim mydir As String = directoryInfo.FullName & "\ConfigAndErrorFiles"

        FILE_NAME = mydir & "\SAVEDXB.txt"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, False)
        For i As Integer = 0 To My.Settings.PositionsX.Count - 1
            objWriter.WriteLine(My.Settings.PositionsX.Item(i))

        Next
        objWriter.Close()

        Dim FILE_NAME2 As String = mydir & "\SAVEDYB.txt"
        Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME2, False)
        For i As Integer = 0 To My.Settings.PositionsY.Count - 1
            objWriter2.WriteLine(My.Settings.PositionsY.Item(i))

        Next


        objWriter2.Close()
    End Sub

    Private Sub Button99_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button99.Click
        loaddatafromRelease("SAVEDXA.txt", "SAVEDYA.txt")
    End Sub

    Private Sub Button98_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button98.Click
        loaddatafromRelease("SAVEDXB.txt", "SAVEDYB.txt")
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim mydir As String = directoryInfo.FullName & "\ConfigAndErrorFiles"

        FILE_NAME = mydir & "\SAVEDXC.txt"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, False)
        For i As Integer = 0 To My.Settings.PositionsX.Count - 1
            objWriter.WriteLine(My.Settings.PositionsX.Item(i))

        Next
        objWriter.Close()

        Dim FILE_NAME2 As String = mydir & "\SAVEDYC.txt"
        Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME2, False)
        For i As Integer = 0 To My.Settings.PositionsY.Count - 1
            objWriter2.WriteLine(My.Settings.PositionsY.Item(i))

        Next


        objWriter2.Close()
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        loaddatafromRelease("SAVEDXC.txt", "SAVEDYC.txt")
    End Sub

    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        scheduled()
    End Sub

    Sub scheduled()
        If ScheduleCheck.Checked And Date.Now.Day = DayBox.Text And Date.Now.Hour = HourBox.Text And Date.Now.Minute >= MinuteBox.Text Then
            'schedule load positions2 (button98) and recalculate focusing at 1:
            loaddatafromRelease("SAVEDXB.txt", "SAVEDYB.txt")
            FormFocusingRecorded.calcForPos1()
        End If
    End Sub

    Private Sub LinkLabel15_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel15.LinkClicked
        DayBox.Text = Date.Now.Day + 1
        HourBox.Text = Date.Now.Hour
        MinuteBox.Text = Date.Now.Minute
        ScheduleCheck.Checked = True
        ' MsgBox(Date.Now.Hour)
    End Sub


    Private Sub TextBox39_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox39.TextChanged

    End Sub


    Private Sub Button26_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click
        Dim n As String = TextBox40.Text
        n = n - 1


        'SerialPort1.ReadExisting()
        Try
            SerialPort1.DiscardInBuffer()

        Catch ex As Exception

        End Try
        Str = "w x"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)

        Catch ex As Exception

        End Try

        Threading.Thread.Sleep(100)
        'Label29.Text = SerialPort1.ReadExisting
        mychar = {":", "A"}

        'Dim mytext As String
        Try
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)
            ListBox2.Items.RemoveAt(n)
            ListBox2.Items.Insert(n, mytext)
            My.Settings.PositionsX.RemoveAt(n)
            My.Settings.PositionsX.Insert(n, mytext)
        Catch ex As Exception
            MessageBox.Show("my error999")
        End Try


        Str = "w y"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)

        Catch ex As Exception

        End Try

        Threading.Thread.Sleep(100)
        Try
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)
            ListBox3.Items.RemoveAt(n)
            ListBox3.Items.Insert(n, mytext)
            My.Settings.PositionsY.RemoveAt(n)
            My.Settings.PositionsY.Insert(n, mytext)
        Catch ex As Exception

        End Try


        My.Settings.Save()
        ListBox1.Items.Clear()
        'loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
        loaddata()

        'MsgBox("Done")

    End Sub



    Private Sub TextBox41_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox41.TextChanged
        My.Settings.DfCam1Pin = TextBox41.Text
        My.Settings.Save()
    End Sub

    Private Sub CheckBox33_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox33.CheckedChanged
        If CheckBox3.Checked Then
            My.Settings.UseBFCam1 = False
            My.Settings.Save()
        Else
            My.Settings.UseBFCam1 = True
            My.Settings.Save()
        End If
    End Sub

    Private Sub CheckBox34_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox34.CheckedChanged
        If CheckBox34.Checked Then   'Fl_A4988
            My.Settings.Fl_A4988 = True
        Else
            My.Settings.Fl_A4988 = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub Button100_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button100.Click

        'SerialPort1.ReadExisting()
        Try
            SerialPort1.DiscardInBuffer()

        Catch ex As Exception

        End Try
        Str = "w x"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)

        Catch ex As Exception

        End Try

        Threading.Thread.Sleep(100)
        'Label29.Text = SerialPort1.ReadExisting
        mychar = {":", "A"}

        'Dim mytext As String
        Try
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)
            ListBox2.Items.Insert((Convert.ToInt32(TextBox42.Text)) - 1, mytext)
            My.Settings.PositionsX.Insert((Convert.ToInt32(TextBox42.Text)) - 1, mytext)
        Catch ex As Exception
            MessageBox.Show("my error345")
        End Try


        Str = "w y"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)

        Catch ex As Exception

        End Try

        Threading.Thread.Sleep(100)
        Try
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)
            ListBox3.Items.Insert((Convert.ToInt32(TextBox42.Text)) - 1, mytext)
            My.Settings.PositionsY.Insert((Convert.ToInt32(TextBox42.Text)) - 1, mytext)
        Catch ex As Exception

        End Try


        My.Settings.Save()
        ListBox1.Items.Clear()
        'loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
        loaddata()


        savePositionstxt(directoryInfo.FullName & "\ConfigAndErrorFiles") 'saves SAVEDX.txt to Release folder. ' the same thing happens when you click Start.
    End Sub

    Private Sub LinkLabel16_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel16.LinkClicked
        nextPoslink()
    End Sub


    Sub nextPoslink()
        Try
            If Label42.Text = "-" Or Label42.Text = "" Then
                Label42.Text = 1
            End If
            ListBox1.SelectedIndex = Convert.ToInt32(Label42.Text)

            Label42.Text = (ListBox1.SelectedIndex + 1).ToString
            If DCmotors.Checked Then
                gotoselectedposDC()

            ElseIf pololumode Then
                gotoselectedposPololu()
            ElseIf mcl Then
                gotoselectedposMcl()

            Else
                gotoselectedpos()
                'gotoselectedposWithoutTimer()
            End If
        Catch ex As Exception
            MsgBox("Can't go to next position, reached end?")
        End Try
    End Sub

    Sub nextPosPlusN()
        If Label42.Text = "-" Or Label42.Text = "" Then
            Label42.Text = 1
        End If
        ListBox1.SelectedIndex = Convert.ToInt32(Label42.Text) + Convert.ToInt32(TextBox46.Text)

        Label42.Text = (ListBox1.SelectedIndex + 1).ToString
        If DCmotors.Checked Then
            gotoselectedposDC()

        ElseIf pololumode Then
            gotoselectedposPololu()
        ElseIf mcl Then
            gotoselectedposMcl()

        Else
            gotoselectedpos()
            'gotoselectedposWithoutTimer()
        End If
    End Sub

    Dim selected As Integer
    Private Sub Button101_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button101.Click
        selected = ListBox1.SelectedIndex()
        ''''''''''first delete selected position:
        Try
            My.Settings.PositionsX.RemoveAt(ListBox1.SelectedIndex())
            My.Settings.PositionsY.RemoveAt(ListBox1.SelectedIndex())
            ListBox2.Items.RemoveAt(ListBox1.SelectedIndex())
            ListBox3.Items.RemoveAt(ListBox1.SelectedIndex())
            My.Settings.Save()
            ListBox1.Items.Clear()
            'loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
            loaddata()
        Catch ex As Exception
            MessageBox.Show("Can't load data")
        End Try

        ''''''''now add the current position to the deleted spot:


        ListBox1.SelectedIndex = selected


        Label42.Text = (ListBox1.SelectedIndex + 1).ToString
        'SerialPort1.ReadExisting()
        Try
            SerialPort1.DiscardInBuffer()

        Catch ex As Exception

        End Try
        Str = "w x"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)

        Catch ex As Exception

        End Try

        Threading.Thread.Sleep(100)
        'Label29.Text = SerialPort1.ReadExisting
        mychar = {":", "A"}

        'Dim mytext As String
        Try
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)
            ListBox2.Items.Insert(ListBox1.SelectedIndex, mytext)
            My.Settings.PositionsX.Insert(ListBox1.SelectedIndex, mytext)
        Catch ex As Exception
            MessageBox.Show("my error345")
        End Try


        Str = "w y"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)

        Catch ex As Exception

        End Try

        Threading.Thread.Sleep(100)
        Try
            mytext = SerialPort1.ReadExisting
            mytext = mytext.TrimStart(mychar)
            ListBox3.Items.Insert(ListBox1.SelectedIndex, mytext)
            My.Settings.PositionsY.Insert(ListBox1.SelectedIndex, mytext)
        Catch ex As Exception

        End Try


        My.Settings.Save()
        ListBox1.Items.Clear()
        'loaddatafromRelease("SAVEDX.txt", "SAVEDY.txt")
        loaddata()


        savePositionstxt(directoryInfo.FullName & "\ConfigAndErrorFiles") 'saves SAVEDX.txt to Release folder. ' the same thing happens when you click Start.
        ListBox1.SelectedIndex = selected
    End Sub

    Dim smallsize As Boolean = False
    Private Sub LinkLabel17_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel17.LinkClicked
        If smallsize Then
            PictureBox1.Size = New Size(1280, 960)
            smallsize = False
        Else
            PictureBox1.Size = New Size(960, 720)
            smallsize = True
        End If

    End Sub


    Private Sub Button102_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button102.Click

        MsgBox("From Pos" & Label42.Text & TextBox43.Text)
        ListBox1.SelectedIndex = CInt(TextBox43.Text - 1)
        Label42.Text = (ListBox1.SelectedIndex + 1).ToString
        gotoselectedpos()
        currentListItem = CInt(Label42.Text) - 1
        changeFocusNew()
    End Sub

    Private Sub Button103_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button103.Click
        addnbynbutton()
    End Sub

    Private Sub TextBox44_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox44.TextChanged
        nbyn = TextBox44.Text
        TextBox7.Text = TextBox44.Text
    End Sub


    Private Sub TextBox32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox32.Click
        RadioStepsA.Checked = True
        RadioStepsB.Checked = False
    End Sub

    Private Sub TextBox31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox31.Click
        RadioStepsB.Checked = True
        RadioStepsA.Checked = False
    End Sub

    Private Sub LinkLabel18_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel18.LinkClicked
        nextPoslink()
    End Sub


    Private Sub TimerLiveRestart_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerLiveRestart.Tick
        liveONbutton()
        TimerLiveRestart.Stop()
    End Sub



    Private Sub LinkLabel19_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel19.LinkClicked
        ListBox8.Items.Clear()
        ListBox9.Items.Clear()
        ListBox10.Items.Clear()
    End Sub

    Private Sub LinkLabel20_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel20.LinkClicked
        ListBox10.Items.Clear()
    End Sub



    'Private Sub FocusEvery_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FocusEvery.TextChanged
    '    My.Settings.FocusEvery = FocusEvery.Text
    'End Sub


    Private Sub Button104_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button104.Click
        changeCam()
    End Sub


    Sub changeCam()

        If loaded = True Then

            'myControlDialog.Disconnect()
            ' Thread.Sleep(300)
            If cam1selected Then
                connectToCam2()
            ElseIf cam2selected Then
                connectToCam1()

                ' myControlDialog.Connect(cam2)
            End If

            If cam1selected Then 'this is camera1
                cam.SetProperty(gainCam1)
                GainTextBox.Text = gainCam1.absValue.ToString
            ElseIf cam2selected Then
                cam.SetProperty(gainCam2)
                GainTextBox.Text = gainCam2.absValue.ToString
            End If

        End If



        'If loaded = True Then
        '    If RadioButton13.Checked Then
        '        selectedCamSerial = serial1
        '    Else
        '        selectedCamSerial = serial2
        '    End If

        '    guid = busMgr.GetCameraFromSerialNumber(selectedCamSerial)

        '    'cam = New ManagedCamera()
        '    cam.Disconnect()
        '    cam.Connect(guid)
        '    camInfo = cam.GetCameraInfo()
        '    PrintCameraInfo(camInfo)

        '    ' Get embedded image info from camera
        '    embeddedInfo = cam.GetEmbeddedImageInfo()

        '    '' Enable timestamp collection	
        '    'If (embeddedInfo.timestamp.available = True) Then
        '    '    embeddedInfo.timestamp.onOff = True
        '    'End If

        '    ' Set embedded image info to camera
        '    cam.SetEmbeddedImageInfo(embeddedInfo)
        '    myControlDialog.Disconnect()
        '    myControlDialog.Connect(cam)
        'End If
    End Sub
    Sub connectToCam1()
        cam1selected = True
        Cam1Label.Font = New Font(Cam1Label.Font, FontStyle.Bold)
        Cam1Label.Refresh()
        cam2selected = False
        Cam2Label.Font = New Font(Cam2Label.Font, FontStyle.Regular)
        Cam2Label.Refresh()
        'myControlDialog.Connect(cam)
        ' usingCam1 = True
        'usingCam2 = False
        Label7.Text = "Connecting to Cam1.."
        Label7.Show()
        PictureBox1.Refresh()
        Label7.Refresh()

        thread1 = New Thread(AddressOf camchange)
        thread1.Start()
        WaitEvent.WaitOne()

        myConnectToCam(serial1)

        If My.Settings.FlipXCam1 Then
            CheckBox27.Checked = True
        Else
            CheckBox27.Checked = False
        End If
        If My.Settings.FlipYCam1 Then
            CheckBox16.Checked = True
        Else
            CheckBox16.Checked = False

        End If
        lightBFchecked()
        lightBF2checked()
        lightFluoChecked()
        lightDarkFChecked()
        Label7.Hide()
    End Sub
    Sub connectToCam2()
        cam1selected = False
        Cam1Label.Font = New Font(Cam1Label.Font, FontStyle.Regular)
        Cam1Label.Refresh()
        cam2selected = True
        Cam2Label.Font = New Font(Cam2Label.Font, FontStyle.Bold)
        Cam2Label.Refresh()

        'usingCam1 = False
        'usingCam2 = True
        Label7.Text = "Connecting to Cam2.."
        Label7.Show()
        Label7.Refresh()

        thread1 = New Thread(AddressOf camchange)
        thread1.Start()
        WaitEvent.WaitOne()


        myConnectToCam(serial2)


        If My.Settings.FlipXCam2 Then
            CheckBox27.Checked = True
        Else
            CheckBox27.Checked = False
        End If
        If My.Settings.FlipYCam2 Then
            CheckBox16.Checked = True
        Else
            CheckBox16.Checked = False
        End If

        lightBFchecked()
        lightBF2checked()
        lightFluoChecked()
        lightDarkFChecked()
        Label7.Hide()
    End Sub



    Private Sub Button105_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button105.Click
        specialSesssion = False
        currentListItem = CInt(TextBox45.Text) - 1
        startSession()
    End Sub

    Private Sub LinkLabel21_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel21.LinkClicked
        If GroupBox12.Visible = False Then
            GroupBox12.Visible = True
        Else
            GroupBox12.Visible = False
        End If
    End Sub


    Private Sub LinkLabel22_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel22.LinkClicked
        nextPosPlusN()
    End Sub

    Private Sub GainTextBox_TextChanged(sender As Object, e As EventArgs) Handles GainTextBox.TextChanged
        Try
            If GainTextBox.Text <> "" Then

                If cam1selected Then  'cam1 radiobutton
                    gainCam1.absValue = Convert.ToSingle(GainTextBox.Text)
                    My.Settings.gainCam1 = gainCam1.absValue
                    cam.SetProperty(gainCam1)
                ElseIf cam2selected Then  'else cam2 radiobutton is selected so:
                    gainCam2.absValue = Convert.ToSingle(GainTextBox.Text)
                    My.Settings.gainCam2 = gainCam2.absValue
                    cam.SetProperty(gainCam2)
                End If

            End If
        Catch ex As Exception
            If CheckBox23.Checked Then  'Test radiobutton.
            Else
                'MsgBox("Camera not connected " & ex.Message)
                timedMessage = "Camera not connected"
                addtomyConsoleErrorMessages(timedMessage)
                time = 4
                FormTimedDialog.Show()

            End If

        End Try
    End Sub


    Private Sub Button106_Click(sender As Object, e As EventArgs) Handles Button106.Click
        specialSesssion = True
        specialSesssionCount = 0
        startSession()
    End Sub

    Private Sub LinkLabel23_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel23.LinkClicked
        If Label42.Text = "-" Or Label42.Text = "" Then
            Label42.Text = 1
        End If
        ListBox1.SelectedIndex = Convert.ToInt32(Label42.Text) + CInt(TextBox47.Text) - 1

        Label42.Text = (ListBox1.SelectedIndex + 1).ToString


        If DCmotors.Checked Then
            gotoselectedposDC()

        ElseIf pololumode Then
            gotoselectedposPololu()
        ElseIf mcl Then
            gotoselectedposMcl()

        Else
            gotoselectedpos()
            'gotoselectedposWithoutTimer()
        End If

    End Sub

    Private Sub Button108_Click(sender As Object, e As EventArgs) Handles Button108.Click
        Dim mydir As String = directoryInfo.FullName & "\ConfigAndErrorFiles"

        FILE_NAME = mydir & "\SAVEDXD.txt"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, False)
        For i As Integer = 0 To My.Settings.PositionsX.Count - 1
            objWriter.WriteLine(My.Settings.PositionsX.Item(i))

        Next
        objWriter.Close()

        Dim FILE_NAME2 As String = mydir & "\SAVEDYD.txt"
        Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME2, False)
        For i As Integer = 0 To My.Settings.PositionsY.Count - 1
            objWriter2.WriteLine(My.Settings.PositionsY.Item(i))

        Next


        objWriter2.Close()

    End Sub

    Private Sub Button107_Click(sender As Object, e As EventArgs) Handles Button107.Click
        loaddatafromRelease("SAVEDXD.txt", "SAVEDYD.txt")
    End Sub

    Dim tempPosArrayX As String()
    Dim tempPosArrayY As String()
    Private Sub Button109_Click(sender As Object, e As EventArgs) Handles Button109.Click

        ReDim tempPosArrayX(ListBox2.Items.Count - 1)
        ReDim tempPosArrayY(ListBox3.Items.Count - 1)
        Dim i As Integer = 0
        For Each item As String In ListBox2.Items
            tempPosArrayX(i) = item
            i = i + 1
        Next
        i = 0
        For Each item As String In ListBox3.Items
            tempPosArrayY(i) = item
            i = i + 1
        Next
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        My.Settings.PositionsX.Clear()
        My.Settings.PositionsY.Clear()
        ListBox2.Items.Add("0")
        ListBox2.Refresh()
        ListBox3.Items.Add("0")
        ListBox3.Refresh()
        My.Settings.PositionsX.Add("0")
        My.Settings.PositionsY.Add("0")

        Dim value As Double
        Dim min As Double
        Dim minpos As Integer = 0
        Dim prevminpos As Integer
        Dim m As Integer = 0

        'MsgBox("tempPosArrayX.Length=" & tempPosArrayX.Length)
        For j As Integer = 0 To tempPosArrayX.Length - 2
            min = 100000000
            'MsgBox("tempPosArrayX=" & tempPosArrayX(0) & ", " & tempPosArrayX(1) & ", " & tempPosArrayX(2) & ", " & tempPosArrayX(3) & " m=" & m & " minpos=" & minpos)
            prevminpos = minpos
            For k As Integer = 0 To tempPosArrayX.Length - 1
                If ((k <> m) And ((CInt(tempPosArrayX(k)) <> 0) Or (CInt(tempPosArrayY(k)) <> 0))) Then
                    value = (CInt(tempPosArrayX(m)) - CInt(tempPosArrayX(k))) ^ 2 + (CInt(tempPosArrayY(m)) - CInt(tempPosArrayY(k))) ^ 2
                    If value < min Then
                        min = value
                        minpos = k
                    End If
                End If
            Next
            ListBox2.Items.Add(tempPosArrayX(minpos).ToString)
            ListBox2.Refresh()
            My.Settings.PositionsX.Add(tempPosArrayX(minpos).ToString)
            ListBox3.Items.Add(tempPosArrayY(minpos).ToString)
            ' MsgBox("minpos=" & minpos & " tempPosArrayX(minpos)=" & tempPosArrayX(minpos).ToString)
            ListBox3.Refresh()
            My.Settings.PositionsY.Add(tempPosArrayY(minpos).ToString)
            My.Settings.Save()
            Refresh()

            tempPosArrayX(prevminpos) = 0
            tempPosArrayY(prevminpos) = 0
            m = minpos
        Next

        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()
        'now add them to settings:
        'My.Settings.PositionsX.Add(mytext) etc...

    End Sub


    Private Sub LinkLabel24_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel24.LinkClicked

        If stopThread = True Then  'if live is off
            startFocusing()
            GrabImage()
        Else
            stopThread = True
            lightsOFF()
            TimerLiveStop.Start()
        End If






    End Sub

    Private Sub TimerLiveStop_Tick(sender As Object, e As EventArgs) Handles TimerLiveStop.Tick
        TimerLiveStop.Stop()
        startFocusing()
        liveONbutton()
    End Sub

    Private Sub Button110_Click(sender As Object, e As EventArgs) Handles Button110.Click
        startSession()
    End Sub

    Private Sub Button111_Click(sender As Object, e As EventArgs) Handles Button111.Click
        GotoPos1AndWaitForStage()
        buttonStartSession()

    End Sub

    Sub GotoPos1AndWaitForStage()
        SetStageSpeeds()
        Label7.Text = "Moving.. Please wait.."
        Label7.Show()
        Str = "MOVE X=" 'this is press of right arrow (movement in X !!!)
        ' item = CInt(ListBox2.Items(ListBox1.SelectedIndex()).ToString)
        item = CInt(ListBox2.Items.Item(0).ToString)
        str2 = item
        Str += str2
        str3 = " Y="  'this is press of up arrow (movement in Y !!!)
        Str += str3
        'item2 = CInt(ListBox3.Items(ListBox1.SelectedIndex()).ToString)
        item2 = CInt(ListBox3.Items.Item(0).ToString)
        str4 = item2
        Str += str4
        Str += ControlChars.Cr
        SerialPort1.Write(Str)
        QueryFinishedMovementThread() 'this starts a new thread that waits for the stage to respond when it has finished moving.
        WaitEvent.WaitOne()  ' this waits for the previous thread to end.
    End Sub

    Private Sub AutoStartCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles AutoStartCheckBox.CheckedChanged
        If AutoStartCheckBox.Checked Then
            My.Settings.AutoStartCheckBox = True
        Else
            My.Settings.AutoStartCheckBox = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        FormAutostartImaging.Show()
        'If AutoStartCheckBox.Checked Then
        '    MsgBox("Warning: The program is set to automatically start imaging on startup. Uncheck the Autostart Imaging on Startup checkbox to disable this.")
        'End If
    End Sub

    Private Sub TimerPressEnter_Tick(sender As Object, e As EventArgs) Handles TimerPressEnter.Tick
        SendKeys.Send("{ENTER}")
        TimerPressEnter.Stop()
    End Sub

    Private Sub UseRecorded_CheckedChanged(sender As Object, e As EventArgs) Handles UseRecorded.CheckedChanged
        If UseRecorded.Checked Then
            My.Settings.UseRecorded = True
        Else
            My.Settings.UseRecorded = False
        End If
        My.Settings.Save()
    End Sub

    Private Sub LinkLabel25_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel25.LinkClicked
        'buttonAddCurrent()
        UseRecorded.Checked = False
        GrabandSaveAllSelected()
    End Sub

    Private Sub LinkLabel26_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel26.LinkClicked
        'savePositionstxt(directoryInfo.FullName & "\ConfigAndErrorFiles")
        'Saves in the current directory
        Dim mydir As String = directoryInfo.FullName & "\ConfigAndErrorFiles"

        FILE_NAME = mydir & "\SAVEDXE.txt"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, False)
        For i As Integer = 0 To My.Settings.PositionsX.Count - 1
            objWriter.WriteLine(My.Settings.PositionsX.Item(i))

        Next
        objWriter.Close()

        Dim FILE_NAME2 As String = mydir & "\SAVEDYE.txt"
        Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME2, False)
        For i As Integer = 0 To My.Settings.PositionsY.Count - 1
            objWriter2.WriteLine(My.Settings.PositionsY.Item(i))

        Next


        objWriter2.Close()
    End Sub

    Private Sub LinkLabel27_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel27.LinkClicked
        loaddatafromRelease("SAVEDXE.txt", "SAVEDYE.txt")
    End Sub
End Class



' SerialPort1.close()
' disconnect from cam
'disconnecty from hid