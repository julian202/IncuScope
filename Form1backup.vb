Imports System
Imports System.Text
Imports System.IO
Imports System.Drawing
Imports FlyCapture2Managed
Imports System.Threading

'Imports EASendMail


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
    Dim wellwidth As Integer = My.Settings.wellwidth
    Dim wellheight As Integer = My.Settings.wellheight
    Dim mywidth As Integer = My.Settings.mywidth
    Dim myheigth As Integer = My.Settings.myheigth



    Dim versionMS2000 As Boolean = My.Settings.versionMS2000 'versionMS2000 is MS-2000 stage.
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
    Dim camInfo As CameraInfo
    Dim embeddedInfo As EmbeddedImageInfo
    Dim rawImage As ManagedImage = New ManagedImage()
    Dim convertedImage As ManagedImage = New ManagedImage()
    Dim bitmap As Bitmap
    Dim myControlDialog As FlyCapture2Managed.Gui.CameraControlDialog
    'Dim myControlDialog2 As FlyCapture2Managed.Gui.CameraControlDialog
    Dim interval As Integer
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


    Private Sub Form1_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        stopThread = True
        lightsOFF()
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CheckBox6.Checked = True 'autofocusing checkbox
        SerialPort1.PortName = myserialport
        IncuLeft = myinculeft



        If (My.Settings.SelectedConfig = "A") Then
            RadioButton17.Checked = True

        ElseIf (My.Settings.SelectedConfig = "B") Then
            RadioButton18.Checked = True

        ElseIf (My.Settings.SelectedConfig = "C") Then
            RadioButton19.Checked = True

        End If


        'TextBox14.Text = mywidth
        'TextBox13.Text = myheigth
        'TextBox16.Text = wellwidth
        'TextBox15.Text = wellheight


        If My.Settings.pololumode = True Then
            'MsgBox("1")
            CheckBox10.Checked = True

        Else
            'MsgBox("2")
            CheckBox10.Checked = False

        End If
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
        TextBox1.Text = 3 'interval

        If versionMS2000 Then
            'mywidth = widthMS2000
            ' myheigth = heigthMS2000
            Label10.Text = "Using Stage MS-2000..."
        Else
            ' mywidth = widthMS4
            ' myheigth = heigthMS4
            Label10.Text = "Using Stage MS-4..."
        End If


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

        busMgr = New ManagedBusManager()
        numCameras = busMgr.GetNumOfCameras()
        Console.WriteLine("Number of cameras detected: {0}", numCameras)

        numCamerasDetected = numCameras


        Try
            serial1 = busMgr.GetCameraSerialNumberFromIndex(0)
            serial2 = busMgr.GetCameraSerialNumberFromIndex(1)

        Catch ex As Exception

        End Try

        ''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'FormCamSelection.ShowDialog()
        'or just select the one on the right/left:

        selectedCamSerial = serial1
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''''''''''''''
        '''''''''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''''''''''
        ''''''''''''''''''''''''''''''''
        testmode = My.Settings.testmode
        If testmode = True Then
            ' SerialPort1.PortName = "COM3"
            Label18.Text = "Test Version"
        Else
            If IncuLeft Then
                Label18.Text = "Left Version"
                ' Me.Text = Me.Text & " Left Version"
                ' SerialPort1.PortName = "COM3"
            Else
                Label18.Text = "Right Version"
                'Me.Text = Me.Text & " Right Version"
                'SerialPort1.PortName = "COM5"
            End If
        End If

        If pololumode = False Then
            Try
                SerialPort1.Open()  'COM3'
            Catch ex As Exception
                MessageBox.Show("Error: Please connect the Stage Controller to the PC or change the  Com port. ")
            End Try
        End If

        Try
            'guid = busMgr.GetCameraFromIndex(0)
            guid = busMgr.GetCameraFromSerialNumber(selectedCamSerial)

            cam = New ManagedCamera()
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
            Me.Refresh()
            addtomyConsole("Grabbing Image..")
            GrabImage()

            myControlDialog = New FlyCapture2Managed.Gui.CameraControlDialog

            myControlDialog.Connect(cam)

        Catch ex As Exception
            If testmode = False Then
                MessageBox.Show("You haven't connected the camera to the PC!")
            End If

        End Try

        loaddata()
        'SetStageSpeeds()
        'highSpeed()
        Timer4.Start()
        loaded = True

        If testmode = True Then
            CheckBox8.Checked = True
        Else
            CheckBox8.Checked = False
        End If

        getStageParameters()

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


        lightON()
        cam.StartCapture()
        Do While True 'stopThread = False
            GrabImageForLoop()
            'Refresh()
            If stopThread = True Then
                cam.StopCapture()
                lightsOFF()
                grabThread.Abort()
            End If
        Loop
    End Sub

    Sub lightON()
        Try

            If RadioButton6.Checked Then
                cam.RestoreFromMemoryChannel(1)
                light1ON()
            ElseIf RadioButton7.Checked Then
                cam.RestoreFromMemoryChannel(2)
                light2ON()
            ElseIf RadioButton8.Checked Then
                cam.RestoreFromMemoryChannel(2)
                lightFlON()
            ElseIf RadioButton9.Checked Then
                cam.RestoreFromMemoryChannel(2)
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
        sendtoINCUSCOPE("2")
        Button1.BackColor = Color.Yellow
        Label5.Show()
        Label5.Refresh()
        Button1.Refresh()
    End Sub
    Sub lightFlON()
        lightisOn = True
        sendtoINCUSCOPE("3") 'sendtoINCUSCOPE("w")
        Button1.BackColor = Color.Yellow
        Label5.Show()
        Label5.Refresh()
        Button1.Refresh()
    End Sub
    Sub lightDfON()
        lightisOn = True
        sendtoINCUSCOPE("d")
        Button1.BackColor = Color.Yellow
        Label5.Show()
        Label5.Refresh()
        Button1.Refresh()
    End Sub

    Sub lightsOFF()
        'stopThread = True
        lightisOn = False
        sendtoINCUSCOPE("a")
        Button1.UseVisualStyleBackColor = True
        Button1.Refresh()
        Label5.Hide()
        Label5.Refresh()
    End Sub

    Sub setCameraShutters()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click 'Grab one
        GrabImage()
    End Sub
    Sub GrabImage()




        'rawImage = New ManagedImage()
        'convertedImage = New ManagedImage()
        s1 = DateTime.Now.Second.ToString()
        m1 = DateTime.Now.Millisecond.ToString()

        lightON()


        'Threading.Thread.Sleep(100) '20 is not enough
        'Dim my2 As New FC2Config
        'my2.grabMode = GrabMode.DropFrames
        'my2.numBuffers = 1
        'cam.SetConfiguration(my2)

        'Dim b As New FlyCapture2Managed.TriggerMode
        'b.onOff = True
        'cam.SetTriggerMode(b)
        'cam.FireSoftwareTrigger(False)



        Try
            cam.StartCapture()
        Catch ex As Exception
            stopThread = True
            lightsOFF()
            MsgBox("cam not connected")
            'MessageBox.Show("turning live off")
            Exit Sub
        End Try


        Try
            cam.RetrieveBuffer(rawImage)
            cam.RetrieveBuffer(rawImage)
        Catch ex As Exception
            Dim FILE_NAME As String = drive & "\Programs\IncuScope\MYERRORS.txt"  'creates a text file with the errrors.
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            objWriter.WriteLine("Image Consistency Error! " & DateAndTime.Now)
            objWriter.Close()
            numberOfErrors = numberOfErrors + 1
            cam.StopCapture()
            lightsOFF()
            Threading.Thread.Sleep(5000)
            lightON()
            cam.StartCapture()
            ' Threading.Thread.Sleep(500)
            Try
                cam.RetrieveBuffer(rawImage)
                cam.RetrieveBuffer(rawImage)
            Catch
            End Try

        End Try
        Try
            cam.StopCapture()
        Catch ex As Exception
            ' Dim FILE_NAME As String = "C:\Programs\IncuScope\MYERRORS.txt"  'creates a text file with the errrors.
            ' Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            ' objWriter.WriteLine("Image Consistency Error2! " & DateAndTime.Now)
            ' objWriter.Close()
            'numberOfErrors = numberOfErrors + 1
        End Try


        lightsOFF()

        s2 = DateTime.Now.Second.ToString()
        m2 = DateTime.Now.Millisecond.ToString()

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
            cam.RetrieveBuffer(rawImage)
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
        ElseIf IncuLeft Then
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipXY) 'RotateFlipType.RotateNoneFlipX
        Else
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX) 'RotateFlipType.RotateNoneFlipX
        End If


    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click  'Stop
        stopThread = True
        lightsOFF()
    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click 'Settings
        myControlDialog.Show()
    End Sub

    Dim dontsendemail As Integer
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click 'Timer
        Console.WriteLine("*****START****")
        If pololumode Then
            start()
            ' startpololu()
        Else
            start()
        End If


    End Sub

    'Sub startpololu()
    '    Button5.FlatStyle = FlatStyle.Flat
    '    Button5.BackColor = Color.Red
    '    currentListItem = 0
    '    Label7.Text = "Grabbing Image at Pos " & currentListItem + 1
    '    Console.WriteLine(Label7.Text)
    '    Label7.Show()
    '    GrabandSaveAllSelected()

    '    'this may not be necessary
    '    Label7.Hide()
    '    PictureBox1.Refresh()
    '    NextWellPololu()
    'End Sub

    'Sub NextWellPololu()
    '    If ListBox2.Items.Count - 1 > currentListItem Then
    '        Label7.Text = "Moving to Pos " & currentListItem + 2 & "..."
    '        Console.WriteLine(Label7.Text)
    '        Label7.Show()

    '        If pololumode Then
    '            currentListItem = currentListItem + 1
    '            item = CInt(ListBox2.Items(currentListItem).ToString)
    '            item2 = CInt(ListBox3.Items(currentListItem).ToString)
    '            MsgBox("next well is " & item & ", " & item2)
    '            'x pos
    '            MsgBox("starting mov")
    '            If (item - pololuPosX) > 0 Then
    '                'move up
    '                movePololu(1, pololusteps, item - pololuPosX, 1)
    '                MsgBox("mov1")
    '            ElseIf (item - pololuPosX) < 0 Then
    '                'move down
    '                movePololu(0, pololusteps, pololuPosX - item, 1)
    '                MsgBox("mov2")
    '            ElseIf (item - pololuPosX) = 0 Then

    '                MsgBox("mov3")
    '            End If

    '            pololuPosX = pololuPosX + item - pololuPosX
    '            Label36.Text = pololuPosX

    '            'y pos
    '            MsgBox("starting mov")
    '            If (item2 - pololuPosY) > 0 Then
    '                'move right
    '                movePololu(0, pololusteps, item2 - pololuPosY, 0)
    '                MsgBox("mov1")
    '            ElseIf (item2 - pololuPosY) < 0 Then
    '                'move left
    '                movePololu(1, pololusteps, pololuPosY - item2, 0)
    '                MsgBox("mov2")
    '            ElseIf (item2 - pololuPosY) = 0 Then
    '                MsgBox("mov3")
    '            End If
    '            pololuPosY = pololuPosY + item2 - pololuPosY
    '            Label37.Text = pololuPosY

    '            'Threading.Thread.Sleep(1000)
    '            'finishedMoving = True
    '        End If
    '    End If


    'End Sub
    Sub start()
        If pololumode = False And CheckBox8.Checked = False Then  'CheckBox8 is test mode.
            MsgBox("Make sure you have cleared Vanadium")

        End If

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
            GotoPos1Pololu()
        Else
            GotoPos1()

        End If

        currentListItem = 0

        timerStopped = False
        startCountDown = False
        'gotoNextwell = Tr


        drawPostions()



        If CheckBox6.Checked And CheckBox7.Checked Then
            Label7.Text = "Refocusing at Pos " & currentListItem + 1
            Label7.Show()
            'autofocusingButtonAlias()
            callerisTest = False
            dofocus()
        Else
            Label7.Text = "Grabbing Image at Pos " & currentListItem + 1
            Console.WriteLine(Label7.Text)
            Label7.Show()
            continuation()
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

    Sub continuation()

        If pololumode And CheckBox18.Checked Then  'backlash correction. (CheckBox18)
            backlashcorrectionleftup()
        End If

        GrabandSaveAllSelected()

        'this may not be necessary
        Label7.Hide()
        PictureBox1.Refresh()
        '

        NextWell()

    End Sub



    Sub GrabandSaveAllSelected()

        If CheckBox2.Checked Then

            RadioButton7.Checked = True
            GrabandSave()
        End If

        If CheckBox4.Checked Then

            RadioButton8.Checked = True
            GrabandSave()
        End If
        If CheckBox5.Checked Then

            RadioButton9.Checked = True
            GrabandSave()
        End If
        Threading.Thread.Sleep(500)
        If CheckBox1.Checked Then

            RadioButton6.Checked = True
            GrabandSave()
        End If
    End Sub

    Sub changeFocus()
        mypos = currentListItem + 1


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
                    For j As Integer = 0 To (Ftimes(i) - 1)
                        'PinMotorUpCoarse()
                        'sendtoINCUMOTOR("i")
                        RadioButton1.Checked = True
                        motorSub_UP()
                        Threading.Thread.Sleep(300)
                    Next
                Else
                    Label9.Text = "Changing Focus for Pos" & mypos
                    Label9.Show()
                    Label9.Refresh()
                    Label7.Text = "Objective is moving down..."
                    Label11.Show()
                    Label7.Show()
                    Refresh()
                    For j As Integer = 0 To (Ftimes(i) - 1)
                        'PinMotorDownCoarse()
                        'sendtoINCUMOTOR("d")
                        RadioButton1.Checked = True
                        motorSub_DOWN()
                        Threading.Thread.Sleep(300)
                    Next
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
            GrabImage()
            PictureBox1.Refresh()
            SaveImage()
        End If



    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        t1 = t1 - 1
        Label7.Text = "Next Image in... " & t1
        Label7.Show()


        TextBox2.Text = t1
        TextBox2.Refresh()

        If t1 = 0 Then
            dontsendemail = dontsendemail + 1
            Label7.Hide()
            Timer1.Stop()
            'changeFocus()

            If CheckBox19.Checked Then
                Label7.Text = "Auto-aligning..."
                Label7.Show()
                addtomyConsole(Label7.Text)
                alignX()
                alignY()
                Zero() 'it's important to zero after aligning!


                ' calibrateMS2000()
            End If



            If CheckBox6.Checked And CheckBox7.Checked Then
                autofocusCounter = autofocusCounter - 1
                If autofocusCounter = 0 Then
                    'autofocusingButtonAlias()
                    autofocusCounter = TextBox4.Text
                    callerisTest = False
                    Label7.Text = "Refocusing at Pos " & currentListItem + 1
                    Label7.Show()
                    dofocus()
                End If
            Else
                Label7.Text = "Grabbing Image at Pos " & currentListItem + 1
                Label7.Show()
                Console.WriteLine(Label7.Text)
                continuation()

            End If

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

        'Label31.Show()
        SetStageSpeeds()


        'If onlyoneimage = True Then

        '    'do nothing
        '    currentListItem = 0
        '    Label11.Text = 1
        'Else

        '    onlyoneimage = False

        'MessageBox.Show("called nextwell")

        While skip(currentListItem + 2) = True And skipOnlyLight(currentListItem + 2) = False
            currentListItem = currentListItem + 1
            ' MessageBox.Show("SKIP position" + currentListItem + 2)

        End While



        If ListBox2.Items.Count - 1 > currentListItem Then
            Label7.Text = "Moving to Pos " & currentListItem + 2 & "..."
            Console.WriteLine(Label7.Text)
            Label7.Show()
            PictureBox1.Refresh()
            Label7.Refresh()

            'Label33.Hide()
            'Label31.Text = "Moving to Position " & mypos + 1
            'Label31.Show()
            'Refresh()
            'MessageBox.Show("loop1")
            If pololumode Then
                currentListItem = currentListItem + 1
                item = CInt(ListBox2.Items(currentListItem).ToString)
                item2 = CInt(ListBox3.Items(currentListItem).ToString)
                'MsgBox("next well is " & item & ", " & item2)
                'x pos
                'MsgBox("starting mov")
                If (item - pololuPosX) > 0 Then
                    'move up
                    movePololu(0, pololusteps, item - pololuPosX, 1)
                    ' MsgBox("mov1")
                ElseIf (item - pololuPosX) < 0 Then
                    'move down
                    movePololu(1, pololusteps, pololuPosX - item, 1)
                    '  MsgBox("mov2")
                ElseIf (item - pololuPosX) = 0 Then

                    ' MsgBox("mov3")
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




                'Console.WriteLine("pololusteps " & pololusteps.ToString)
                ' Console.WriteLine("pololuPosY - item2 " & (pololuPosY - item2).ToString)
                ' Console.WriteLine(" (Math.Abs(pololuPosY - item2) " & (Math.Abs(pololuPosY - item2)).ToString)

                'pololu sleep, pololu wait time
                Console.WriteLine("waiting for for movement to end  " & (pololusteps * (Math.Abs(pololuPosY - item2) + Math.Abs(pololuPosX - item)) * 6 / 1000).ToString & " seconds....")
                Threading.Thread.Sleep(pololusteps * (Math.Abs(pololuPosY - item2) + Math.Abs(pololuPosX - item)) * 6)
                Console.WriteLine(".")

                'backlash correction:
                'If (currentListItem Mod nbyn) = 0 Then
                backlashcorrectionleftup()
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
                Threading.Thread.Sleep(4000)
                finishedMoving = True
            Else


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
            End If



            'Label8.Text = ListBox2.Items(currentListItem).ToString
            'Label9.Text = ListBox3.Items(currentListItem).ToString
            '' Me.Timer3.Start()
            'Label11.Text = currentListItem + 1


            'This sends beep when the stage stops moving:
            'QueryFinished = New System.Threading.Thread(AddressOf QueryFinishedMovement)
            'QueryFinished.Start()
            gotoNextwell = True

            If pololumode Then

                timer5tick()
            ElseIf mcl Then
                timer5tick()
            Else

                QueryFinishedMovement()
            End If


        Else
            'MsgBox("this is last pos, now going to 1")
            Console.WriteLine("this is last pos, now going to 1")
            'Console.WriteLine("finishedMoving " & finishedMoving)

            gotoNextwell = False
            Label7.Hide()
            currentListItem = 0

            If CheckBox6.Checked Then
                changeFocus()
            End If


            startCountDown = True
            If pololumode Then
                finishedMoving = True
                GotoPos1Pololu()
            ElseIf mcl Then
                GotoPos1Mcl()
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

    Sub timer5tick() 'this is queryfinishedmovement

        If finishedMoving Then
            'MsgBox("1")
            Console.WriteLine("finishedmoving true")
            Timer5.Stop()
            finishedMoving = False

            If timerStopped Then
                Label7.Text = "Stopped at Pos " & currentListItem + 1
                timerStopped = False
            Else
                ' Console.WriteLine("finishedmoving false")

                If gotoNextwell Then
                    Console.WriteLine("gotonextwell true")
                    'Label7.Hide()
                    'MsgBox("2")


                    If CheckBox6.Checked Then  'autofocusing checkbox
                        changeFocus()
                    End If
                    Label7.Text = "Grabbing Image at Pos " & currentListItem + 1 & "..."
                    Console.WriteLine(Label7.Text)
                    Label7.Show()
                    Label7.Refresh()
                    GrabandSaveAllSelected()

                    Timer2.Start() 'Waits 1sec and calls Next well.
                    'Threading.Thread.Sleep(500)
                Else 'else go home
                    'Console.WriteLine("gotonextwell false")
                    ' MsgBox("3 start countdown" & startCountDown.ToString)

                    Label7.Hide()

                    If startCountDown Then
                        Console.WriteLine("startcountdown true")
                        'If CheckBox6.Checked Then 'autofocusing checkbox
                        '    callerisTest = False
                        '    'autofocusingButtonAlias()
                        '    MessageBox.Show("I would autofocus now")
                        'End If

                        'MsgBox("4")

                        interval = TextBox1.Text
                        t1 = interval
                        Label7.Text = "Next Image in... " & t1
                        Label7.Show()
                        Timer1.Start()
                        startCountDown = False
                    Else
                        Console.WriteLine("startcountdown false")
                        If lightisOn = False Then
                            GrabImage()
                        End If
                        'highSpeed()
                    End If

                End If
            End If

        ElseIf finishedMoving = False Then

            If pololumode Then
            ElseIf mcl Then

            Else


                Console.WriteLine("finishedmoving false")
                Label7.Show()
                'Refresh()
                'Threading.Thread.Sleep(400)
                'Label7.Hide()
                'Refresh()
                'Threading.Thread.Sleep(200)
                Str = "STATUS"
                Str += ControlChars.Cr
                SerialPort1.Write(Str)

                mytext = SerialPort1.ReadExisting
                Console.WriteLine(mytext)
                addtomyConsole(mytext)
                ' mytext = mytext.TrimStart(mychar)
                'MessageBox.Show(mytext)
                If mytext.Contains("N") Then
                    Console.WriteLine("Stage has stopped")
                    addtomyConsole("Stage has stopped")
                    'Label31.Hide()
                    'Label33.Show()

                    'Refresh()

                    'MessageBox.Show("Stage has stopped, now will call grabwells in 1 sec")
                    'Threading.Thread.Sleep(1000)
                    'If CheckBox6.Checked Then  'autofocusing checkbox
                    'autofocusingButtonAlias()  ' calls autofocus and then grabwells()
                    'Else
                    finishedMoving = True
                    'End If
                End If
            End If

        End If

    End Sub
    Sub QueryFinishedMovement()
        mytext = SerialPort1.ReadExisting
        Timer5.Start()
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


        If (Not System.IO.Directory.Exists(imagefolder & "\Now")) Then
            System.IO.Directory.CreateDirectory(imagefolder & "\Now")
        End If
        If (Not System.IO.Directory.Exists(imagefolder & "\Now\Pos" & currentListItem + 1)) Then
            System.IO.Directory.CreateDirectory(imagefolder & "\Now\Pos" & currentListItem + 1 & typeoflight)
        End If
        'PictureBox1.Image.Save("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        'PictureBox1.Image.Save("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & Label8.Text & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        'PictureBox1.Image.Save("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
        Try
            'PictureBox1.Image.Save("C:\Images\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".bmp", System.Drawing.Imaging.ImageFormat.Bmp)
            PictureBox1.Image.Save(imagefolder & "\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg) 'ImageFormat.Jpeg
            'MsgBox(imagefolder)
        Catch ex As Exception
            'sendemail("memory")  'SENDS AN EMAIL STATING THAT THERE IS PROBABLY NO MORE MEMORY IN C:
            Label57.Visible = True  '"Probably not enough memory"
        End Try


        'NOW SAVE TO VANADIUM
        Try
            If testmode Then
                'dailyfolder = "\\VANADIUM\d\Images\ByDay\Left\" & DateTime.Now.ToString("MM-dd-yyyy")


                'If (Not System.IO.Directory.Exists("\\VANADIUM\d\Images\Now\Test")) Then
                '    System.IO.Directory.CreateDirectory("\\VANADIUM\d\Images\Now\Test")
                'End If
                'If (Not System.IO.Directory.Exists("\\VANADIUM\d\Images\Now\Test\Pos" & currentListItem + 1 & typeoflight)) Then
                '    System.IO.Directory.CreateDirectory("\\VANADIUM\d\Images\Now\Test\Pos" & currentListItem + 1 & typeoflight)
                'End If
                'PictureBox1.Image.Save("\\VANADIUM\d\Images\Now\Test\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

                If (Not System.IO.Directory.Exists("Z:\Julian\Test")) Then
                    System.IO.Directory.CreateDirectory("Z:\Julian\Test")
                End If
                If (Not System.IO.Directory.Exists("Z:\Julian\Test\Pos" & currentListItem + 1 & typeoflight)) Then
                    System.IO.Directory.CreateDirectory("Z:\Julian\Test\Pos" & currentListItem + 1 & typeoflight)
                End If
                PictureBox1.Image.Save("Z:\Julian\Test\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)


                ''save a copy also to a daily folder in VANADIUM
                'If (Not System.IO.Directory.Exists(dailyfolder)) Then
                '    System.IO.Directory.CreateDirectory(dailyfolder)
                'End If
                'If (Not System.IO.Directory.Exists(dailyfolder & "\Pos" & currentListItem + 1 & typeoflight)) Then
                '    System.IO.Directory.CreateDirectory(dailyfolder & "\Pos" & currentListItem + 1 & typeoflight)
                'End If
                'PictureBox1.Image.Save(dailyfolder & "\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)


            Else
                If IncuLeft Then  'Save to a folder called Left in Vanadium
                    'dailyfolder = "\\VANADIUM\d\Images\ByDay\Left\" & DateTime.Now.ToString("MM-dd-yyyy")


                    If (Not System.IO.Directory.Exists("\\VANADIUM\d\Images\Now\Left")) Then
                        System.IO.Directory.CreateDirectory("\\VANADIUM\d\Images\Now\Left")
                    End If
                    If (Not System.IO.Directory.Exists("\\VANADIUM\d\Images\Now\Left\Pos" & currentListItem + 1 & typeoflight)) Then
                        System.IO.Directory.CreateDirectory("\\VANADIUM\d\Images\Now\Left\Pos" & currentListItem + 1 & typeoflight)
                    End If
                    PictureBox1.Image.Save("\\VANADIUM\d\Images\Now\Left\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

                    'save a copy also to a daily folder in VANADIUM
                    'If (Not System.IO.Directory.Exists(dailyfolder)) Then
                    '    System.IO.Directory.CreateDirectory(dailyfolder)
                    'End If
                    'If (Not System.IO.Directory.Exists(dailyfolder & "\Pos" & currentListItem + 1 & typeoflight)) Then
                    '    System.IO.Directory.CreateDirectory(dailyfolder & "\Pos" & currentListItem + 1 & typeoflight)
                    'End If
                    'PictureBox1.Image.Save(dailyfolder & "\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)



                Else  'Save to a folder called Rigth in Vanadium
                    'dailyfolder = "\\VANADIUM\d\Images\ByDay\Right\" & DateTime.Now.ToString("MM-dd-yyyy")


                    If (Not System.IO.Directory.Exists("\\VANADIUM\d\Images\Now\Right")) Then
                        System.IO.Directory.CreateDirectory("\\VANADIUM\d\Images\Now\Right")
                    End If
                    If (Not System.IO.Directory.Exists("\\VANADIUM\d\Images\Now\Right\Pos" & currentListItem + 1 & typeoflight)) Then
                        System.IO.Directory.CreateDirectory("\\VANADIUM\d\Images\Now\Right\Pos" & currentListItem + 1 & typeoflight)
                    End If
                    PictureBox1.Image.Save("\\VANADIUM\d\Images\Now\Right\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

                    'save a copy also to a daily folder in VANADIUM
                    'If (Not System.IO.Directory.Exists(dailyfolder)) Then
                    '    System.IO.Directory.CreateDirectory(dailyfolder)
                    'End If
                    'If (Not System.IO.Directory.Exists(dailyfolder & "\Pos" & currentListItem + 1 & typeoflight)) Then
                    '    System.IO.Directory.CreateDirectory(dailyfolder & "\Pos" & currentListItem + 1 & typeoflight)
                    'End If
                    'PictureBox1.Image.Save(dailyfolder & "\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)

                End If

            End If



        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            Dim FILE_NAME As String = "Backup_errors.txt"  'creates a text file with the errrors in the .exe folder.
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME, True)
            objWriter.WriteLine("Backup error " & DateAndTime.Now)
            objWriter.Close()

            Label27.Visible = True
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
        Button5.BackColor = SystemColors.ControlDark
        Button5.FlatStyle = FlatStyle.System

        timerStopped = True
        Timer1.Stop()
        Label7.Hide()
        startCountDown = False
        gotoNextwell = False
        'highSpeed()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Try
            interval = TextBox1.Text
            TextBox2.Text = TextBox1.Text
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            If (Not System.IO.Directory.Exists(imagefolder & "\Now")) Then
                System.IO.Directory.CreateDirectory(imagefolder & "\Now")
            End If


            Process.Start(imagefolder & "\Now")

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
                MsgBox(NameStr & "  You did not connect the PIC with device name INCUSCOPE")
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
                MsgBox(NameStr & "  You did not connect the PIC with device name INCUMOTOR")
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
                MsgBox(NameStr & "  You did not connect the PIC with device name INCUMOTOR")
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
        'move objective up



        Label6.Show()
        Label6.Refresh()
        Label7.Text = "Objective is moving up..."
        Label7.Show()
        Label7.Refresh()

        'Send("4")
        'up = True
        motorSub_DOWN()

        'for DC motor
        'FUp()

    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        'move objective down
        Label11.Show()
        Label11.Refresh()
        Label7.Text = "Objective is moving down..."
        Label7.Show()
        Label7.Refresh()
        'Send("3")
        'up = False
        motorSub_UP()

        'for DC motor
        'Fdown()

    End Sub

    Sub motorSub_UP()
        'Threading.Thread.Sleep(100)

        ''2.5um
        'If RadioButton1.Checked = True Then
        '    Send("c")
        'End If


        ''5um
        If RadioButton1.Checked = True Then
            'sendtoINCUMOTOR("e")
            moveStepper(1, 2, 1)
        End If

        ''10um
        'If RadioButton3.Checked = True Then
        '    sendtoINCUMOTOR("j")
        'End If

        ''20um
        If RadioButton2.Checked = True Then
            'sendtoINCUMOTOR("f")
            moveStepper(1, 8, 1)
        End If

        ''100um
        If RadioButton12.Checked = True Then
            'sendtoINCUMOTOR("g")
            moveStepper(1, 50, 1)
        End If

        ''continuous
        If RadioButton3.Checked = True Then
            'sendtoINCUMOTOR("h")
            'moveStepper(3, 0, 1)
            moveStepper(1, 255, 100)
        End If
        Label6.Hide()
        Label7.Hide()
        Label11.Hide()

    End Sub
    Sub motorSub_DOWN()
        'NOW GOING PULSES GOING DOWN:

        ''2.5um
        'If RadioButton1.Checked = True Then
        '    sendtoINCUMOTOR("h")
        'End If

        ''5um
        If RadioButton1.Checked = True Then
            'sendtoINCUMOTOR("e")
            moveStepper(0, 2, 1)
        End If

        ''10um
        'If RadioButton3.Checked = True Then
        '    sendtoINCUMOTOR("j")
        'End If

        ''20um
        If RadioButton2.Checked = True Then
            'sendtoINCUMOTOR("f")
            moveStepper(0, 8, 1)
        End If

        ''100um
        If RadioButton12.Checked = True Then
            'sendtoINCUMOTOR("g")
            moveStepper(0, 50, 1)
        End If

        ''continuous
        If RadioButton3.Checked = True Then
            'sendtoINCUMOTOR("h")
            'moveStepper(2, 0, 1)
            moveStepper(0, 255, 100)
        End If

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

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click 'Add Current Position.
        If pololumode Then
            AddCurrentPosPololu()
        ElseIf mcl Then
            MsgBox("you must turn the light on for this to work on MCL stage")
            stopThread = True
            lightsOFF()
            AddCurrentPosMcl()
        Else

            AddCurrentPos()

        End If

    End Sub
    Sub AddCurrentPosMcl()

        Threading.Thread.Sleep(100)
        Str = "U" & Chr(67) & Chr(13)
        SerialPort1.Write(Str)

        mytext = SerialPort1.ReadExisting
        Console.WriteLine(mytext)
        Threading.Thread.Sleep(100)
        mytext = SerialPort1.ReadExisting
        Console.WriteLine(mytext)
        ListBox2.Items.Add(mytext)
        My.Settings.PositionsX.Add(mytext)


        Threading.Thread.Sleep(100)
        Str = "U" & Chr(68) & Chr(13)
        SerialPort1.Write(Str)

        mytext = SerialPort1.ReadExisting
        Console.WriteLine(mytext)
        Threading.Thread.Sleep(100)
        mytext = SerialPort1.ReadExisting
        Console.WriteLine(mytext)
        ListBox3.Items.Add(mytext)
        My.Settings.PositionsY.Add(mytext)


        My.Settings.Save()
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

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        loaddata()
    End Sub
    Sub loaddata()
        Try
            For i As Integer = 0 To My.Settings.PositionsY.Count - 1
                ' MessageBox.Show(i)
                ' MessageBox.Show(My.Settings.PositionsX.Item(i))
                'MsgBox(My.Settings.PositionsX.Count)
                text1 = " " & i + 1 & ".(" & My.Settings.PositionsX.Item(i) & "," & My.Settings.PositionsY.Item(i) & ")"
                'TextBox3.AppendText(text1)
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
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click 'Go to selected Pos
        If pololumode Then
            gotoselectedposPololu()
        ElseIf mcl Then
            gotoselectedposMcl()
        Else
            gotoselectedpos()
        End If
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
        Timer5.Start()

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

            Timer5.Start()
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
        Threading.Thread.Sleep(4000)
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
            loaddata()
        Catch ex As Exception
            MessageBox.Show("Can't load data")
        End Try

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click  'Delete All positions
        deleteAll()
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
        If pololumode Then
            ZeroPololu()
        Else
            Zero()
        End If


    End Sub
    Sub Zero()
        Str = "ZERO"
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)

        Catch ex As Exception

        End Try
    End Sub

    Sub ZeroPololu()
        pololuPosX = 0
        pololuPosY = 0
        Label36.Text = 0
        Label37.Text = 0
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click  'Save Positions.
        Try
            My.Settings.SavedX.Clear()
            My.Settings.SavedY.Clear()
            For i As Integer = 0 To My.Settings.PositionsX.Count - 1
                My.Settings.SavedX.Add(My.Settings.PositionsX.Item(i))
                My.Settings.SavedY.Add(My.Settings.PositionsY.Item(i))
            Next
            My.Settings.Save()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click 'Load Positions

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        My.Settings.PositionsX.Clear()
        My.Settings.PositionsY.Clear()
        Try

            For i As Integer = 0 To My.Settings.SavedX.Count - 1
                text1 = " " & i + 1 & ".(" & My.Settings.SavedX.Item(i) & "," & My.Settings.SavedY.Item(i) & ")"
                'TextBox3.AppendText(text1)
                ListBox1.Items.Add(text1)
                ListBox2.Items.Add(My.Settings.SavedX.Item(i))
                My.Settings.PositionsX.Add(My.Settings.SavedX.Item(i))
                ListBox3.Items.Add(My.Settings.SavedY.Item(i))
                My.Settings.PositionsY.Add(My.Settings.SavedY.Item(i))
            Next

        Catch ex As Exception
            MessageBox.Show("No saved data..")
        End Try
        My.Settings.Save()
        drawPostions()
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
            addnbyn()
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
        'loaddata()



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
    '    loaddata()


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
    '    loaddata()


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


    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        MUp()
    End Sub

    Sub MUp()

        If pololumode = True Then

            movePololu(0, pololusteps, pololurepetitions, 1)
            pololuPosX = pololuPosX + pololurepetitions
            Label36.Text = pololuPosX

        ElseIf pololumode = False Then

            If mcl Then

                If CheckBox9.Checked Then
                    item = wellheight
                    item2 = 0
                Else
                    item = -myheigth
                    item2 = 0
                End If

                Str = "U" & Chr(7) & "v" & Chr(13)  'v is for relative, r is for absolute.
                SerialPort1.Write(Str)
                Str = "U" & Chr(0) & item2 & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(1) & item & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(80) & Chr(13)
                SerialPort1.Write(Str)




            Else

                SetStageSpeeds()
                If versionMS2000 Then
                    Str = "R Y="
                Else
                    Str = "RM Y="
                End If
                ' Str += (0 - myheigth).ToString

                If CheckBox9.Checked Then 'checkbox9 is to go to the next well.

                    If CheckBox16.Checked Then 'this is if using the new ms-4 stage. (up becomes down and down becomes up)
                        Str += wellheight.ToString
                    Else
                        Str += (0 - wellheight).ToString
                    End If

                Else

                    If CheckBox16.Checked Then 'this is if using the new ms-4 stage. (up becomes down and down becomes up)
                        Str += myheigth.ToString
                    Else
                        Str += (0 - myheigth).ToString
                    End If

                End If
                Str += ControlChars.Cr
                SerialPort1.Write(Str)

                'currentwellY = Label9.Text
            End If

        End If


    End Sub
    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        MDown()
    End Sub

    Sub MDown()
        If pololumode = True Then
            movePololu(1, pololusteps, pololurepetitions, 1)
            pololuPosX = pololuPosX - pololurepetitions
            Label36.Text = pololuPosX

        ElseIf pololumode = False Then
            If mcl Then
                If CheckBox9.Checked Then
                    item = wellheight
                    item2 = 0
                Else
                    item = -myheigth
                    item2 = 0
                End If
                Console.WriteLine("moving one well down..")

                Str = "U" & Chr(7) & "v" & Chr(13)  'v is for relative, r is for absolute.
                SerialPort1.Write(Str)
                Str = "U" & Chr(0) & item2 & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(1) & -item & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(80) & Chr(13)
                SerialPort1.Write(Str)




            Else

                SetStageSpeeds()
                If versionMS2000 Then
                    Str = "R Y="
                Else
                    Str = "RM Y="
                End If

                If CheckBox9.Checked Then 'checkbox9 is to go to the next well.

                    If CheckBox16.Checked Then 'this is if using the new ms-4 stage. (up becomes down and down becomes up)
                        Str += (0 - wellheight).ToString
                    Else
                        Str += wellheight.ToString
                    End If

                Else

                    If CheckBox16.Checked Then 'this is if using the new ms-4 stage. (up becomes down and down becomes up)
                        Str += (0 - myheigth).ToString
                    Else
                        Str += myheigth.ToString
                    End If


                End If
                Str += ControlChars.Cr
                SerialPort1.Write(Str)
            End If
        End If

    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        MLeft()
    End Sub
    Sub MLeft()

        If pololumode = True Then
            'Console.WriteLine("pololustepsY " & pololustepsY & "pololurepetitions " & pololurepetitions)
            movePololu(1, pololustepsY, pololurepetitions, 0)
            pololuPosY = pololuPosY - pololurepetitions
            Label37.Text = pololuPosY
        ElseIf pololumode = False Then
            If mcl Then

                If CheckBox9.Checked Then
                    item = 0
                    item2 = wellwidth
                Else
                    item = 0
                    item2 = mywidth
                End If

                Str = "U" & Chr(7) & "v" & Chr(13)  'v is for relative, r is for absolute.
                SerialPort1.Write(Str)
                Str = "U" & Chr(0) & item2 & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(1) & item & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(80) & Chr(13)
                SerialPort1.Write(Str)




            Else
                'SetStageSpeeds()
                If versionMS2000 Then
                    Str = "R X="
                Else
                    Str = "RM X="
                End If
                If CheckBox9.Checked Then
                    Str += wellwidth.ToString
                Else
                    Str += mywidth.ToString

                End If
                'MsgBox(Str)
                addtomyConsole((Str))
                Str += ControlChars.Cr
                SerialPort1.Write(Str)

                'currentwellX = Label8.Text

            End If
        End If


    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        MRight()
    End Sub
    Sub MRight()

        If pololumode = True Then
            '            Console.WriteLine("pololustepsY " & pololustepsY & "pololurepetitions " & pololurepetitions)
            movePololu(0, pololustepsY, pololurepetitions, 0)
            pololuPosY = pololuPosY + pololurepetitions
            Label37.Text = pololuPosY

        ElseIf pololumode = False Then
            If mcl Then

                If CheckBox9.Checked Then
                    item = 0
                    item2 = wellwidth
                Else
                    item = 0
                    item2 = mywidth
                End If

                Str = "U" & Chr(7) & "v" & Chr(13)  'v is for relative, r is for absolute.
                SerialPort1.Write(Str)
                Str = "U" & Chr(0) & -item2 & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(1) & item & Chr(13)
                SerialPort1.Write(Str)
                Str = "U" & Chr(80) & Chr(13)
                SerialPort1.Write(Str)

            Else
                SetStageSpeeds()
                If versionMS2000 Then
                    Str = "R X=-"
                Else
                    Str = "RM X=-"
                    'MessageBox.Show("ms4")
                End If
                If CheckBox9.Checked Then
                    Str += wellwidth.ToString
                Else
                    Str += mywidth.ToString

                End If
                'MsgBox(Str)
                addtomyConsole((Str))
                Str += ControlChars.Cr
                SerialPort1.Write(Str)
                ' MessageBox.Show(Str)


                'currentwellX = Label8.Text

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

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            Process.Start(Directory.GetCurrentDirectory() & "\delete.bat")
        Catch ex As Exception
            MessageBox.Show("Batch file not found..")
        End Try
        'MessageBox.Show(Directory.GetCurrentDirectory())
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick 'Timer to wait one second before calling next well.
        Label7.Hide()
        Timer2.Stop()
        ' MsgBox("timer2Tick")
        NextWell()
    End Sub

    Private Sub Button24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button24.Click
        FormFocusing.Show()
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

        GrabImage()
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
                    b = rgbValues((x * 3) + 1 + offset)
                    c = rgbValues((x * 3) + (w * 3) + offset)

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

        '''
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


        Return sum



    End Function




    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked

        'If versionMS2000 Then
        '    mywidth = widthMS2000
        '    myheigth = heigthMS2000
        'Else
        '    mywidth = widthMS4
        '    myheigth = heigthMS4
        'End If

        If versionMS2000 Then
            My.Settings.versionMS2000 = False

            'distancHorizontal = "-1241"
            'distancVertical = "-621"
            Label10.Text = "Using Stage MS-4..."


        Else
            My.Settings.versionMS2000 = True
            'distancHorizontal = "8182"
            'distancVertical = "6124"
            Label10.Text = "Using Stage MS-2000..."
        End If

        My.Settings.Save()
        versionMS2000 = My.Settings.versionMS2000
    End Sub




    Private Sub Button26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button26.Click  'Save B button

        'Saves in the current directory
        Dim FILE_NAME As String = Directory.GetCurrentDirectory() & "\SAVEDX.txt"
        Dim objWriter As New System.IO.StreamWriter(FILE_NAME, False)
        For i As Integer = 0 To My.Settings.PositionsX.Count - 1
            objWriter.WriteLine(My.Settings.PositionsX.Item(i))

        Next
        objWriter.Close()

        Dim FILE_NAME2 As String = Directory.GetCurrentDirectory() & "\SAVEDY.txt"
        Dim objWriter2 As New System.IO.StreamWriter(FILE_NAME2, False)
        For i As Integer = 0 To My.Settings.PositionsY.Count - 1
            objWriter2.WriteLine(My.Settings.PositionsY.Item(i))

        Next


        objWriter2.Close()

    End Sub

    Private Sub Button27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button27.Click

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        My.Settings.PositionsX.Clear()
        My.Settings.PositionsY.Clear()
        Try

            'For i As Integer = 0 To My.Settings.SavedX.Count - 1
            'text1 = " " & i + 1 & ".(" & My.Settings.SavedX.Item(i) & "," & My.Settings.SavedY.Item(i) & ")"
            ''TextBox3.AppendText(text1)
            'ListBox1.Items.Add(text1)
            'ListBox2.Items.Add(My.Settings.SavedX.Item(i))
            'My.Settings.PositionsX.Add(My.Settings.SavedX.Item(i))
            'ListBox3.Items.Add(My.Settings.SavedY.Item(i))
            'My.Settings.PositionsY.Add(My.Settings.SavedY.Item(i))
            'Next




            Dim fileIn As New StreamReader(Directory.GetCurrentDirectory() & "\SAVEDX.txt")
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

            Dim fileIn2 As New StreamReader(Directory.GetCurrentDirectory() & "\SAVEDY.txt")

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
                'TextBox3.AppendText(text1)
                ListBox1.Items.Add(text1)
            Next




        Catch ex As Exception
            MessageBox.Show("No saved data..")
        End Try
        My.Settings.Save()

        drawPostions()
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

            If CheckBox17.Checked = True Then
                For i = 0 To ListBox2.Items.Count - 1
                    ListBox7.Items.Add(ListBox2.Items.Item(i))
                Next
                For i = 0 To ListBox3.Items.Count - 1
                    ListBox6.Items.Add(ListBox3.Items.Item(i))
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

    Private Sub LinkLabel4_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        Try
            Process.Start(Directory.GetCurrentDirectory() & "\Import1BF.ijm")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel5_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        Try
            Process.Start(Directory.GetCurrentDirectory() & "\Import2BF.ijm")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub Button29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button29.Click
        For i = 0 To 50
            'GrabImage()
            va = autof()
            myFocusUp(False, 1)
            SaveImageforVa()
            saveVaFile()
        Next
    End Sub

    Sub SaveImageforVa()
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
        If (Not System.IO.Directory.Exists(imagefolder & "\Now")) Then
            System.IO.Directory.CreateDirectory(imagefolder & "\Now")
        End If
        If (Not System.IO.Directory.Exists(imagefolder & "\Now\Pos" & currentListItem + 1)) Then
            System.IO.Directory.CreateDirectory(imagefolder & "\Now\Pos" & currentListItem + 1 & typeoflight)
        End If
        PictureBox1.Image.Save(imagefolder & "\Now\Pos" & currentListItem + 1 & typeoflight & "\" & mydate & "_Pos" & currentListItem + 1 & typeoflight & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
    End Sub

    Sub saveVaFile()
        'Saves in the current directory
        Dim FILE_NAME As String = Directory.GetCurrentDirectory() & "\SAVEDVa.txt"
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

        '''
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

            'Panel3.Show()
        Else
            Panel3.Visible = True

            'Panel3.Hide()
        End If


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
        loaddata()
        'drawPostions()
    End Sub

    Private Sub Button34_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button34.Click
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
        loaddata()
        'drawPostions()
    End Sub

    Private Sub Button35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button35.Click
        Panel3.Hide()
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


    Private Sub Button40_Click(sender As System.Object, e As System.EventArgs) Handles Button40.Click
        CenteringAlgorithm()
    End Sub

    Sub CenteringAlgorithm()
        Try
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



    Private Sub Button42_Click(sender As System.Object, e As System.EventArgs) Handles Button42.Click
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
        Dim FILE_NAME As String = drive & "\numbs.txt"
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

    Private Sub Button43_Click(sender As System.Object, e As System.EventArgs) Handles Button43.Click
        runAlgorithm2()
    End Sub


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

    Sub runAlgorithm2()
        length = lengthlow
        CenteringAlgorithm2()
        Dim wait As Integer = 100
        While (yvalue < -limit) Or (yvalue > limit) Or (xvalue < -limit) Or (xvalue > limit)
            If yvalue < -limit Then
                'MclUp()
                'Threading.Thread.Sleep(wait)
            End If
            If yvalue > limit Then
                ' MclDown()
                'Threading.Thread.Sleep(wait)
            End If
            If xvalue < -limit Then
                'MclRight()
                ' MclLeft()
                ' Threading.Thread.Sleep(wait)
            End If
            If xvalue > limit Then
                'MclLeft()
                ' MclRight()
                ' Threading.Thread.Sleep(wait)
            End If
            CenteringAlgorithm2()
        End While
    End Sub



    Private Sub Button46_Click(sender As System.Object, e As System.EventArgs) Handles Button46.Click
        proc()
        Timer6.Start()
    End Sub

    Private Sub Timer6_Tick(sender As System.Object, e As System.EventArgs) Handles Timer6.Tick
        proc()
    End Sub

    Private Sub Button47_Click(sender As System.Object, e As System.EventArgs) Handles Button47.Click
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

    Private Sub Button48_Click(sender As System.Object, e As System.EventArgs) Handles Button48.Click
        PictureBox1.ImageLocation = drive & "\bullseye.jpg"
        Refresh()
        'Threading.Thread.Sleep(1000)
        'GrabImage()
        'Refresh()
    End Sub

    Dim WaitEvent As AutoResetEvent


    Dim thread1 As Thread
    Dim thread2 As Thread
    'Private Sub Button41_Click(sender As System.Object, e As System.EventArgs) Handles Button41.Click 'test
    '    Str = "RM X=-"
    '    Str += mediumstep.ToString
    '    Str += ControlChars.Cr
    '    SerialPort1.Write(Str)
    '    addtomyConsole(Str)


    '    ' QueryFinishedMovementB()

    '    thread1 = New Thread(AddressOf querythread)

    '    ' WaitEvent = New AutoResetEvent(False)

    '    thread1.Start()


    '    'WaitEvent.WaitOne()
    '    'Str = "RM X="
    '    'Str += mediumstep.ToString
    '    'Str += ControlChars.Cr
    '    'SerialPort1.Write(Str)
    '    'addtomyConsole(Str)



    'End Sub

    Private Sub Button41_Click(sender As System.Object, e As System.EventArgs) Handles Button41.Click 'test



        For i As Integer = 1 To 2
            addtomyConsole("ok")
            Thread.Sleep(500)
        Next



        thread1 = New Thread(AddressOf querythread1)
        WaitEvent = New AutoResetEvent(False)
        thread1.Start()

        'WaitEvent.WaitOne()
        'Str = "RM X="
        'Str += mediumstep.ToString
        'Str += ControlChars.Cr
        'SerialPort1.Write(Str)
        'addtomyConsole(Str)

        WaitEvent.WaitOne()
        addtomyConsole("finished")
    End Sub

    Sub querythread1()

        For i As Integer = 5 To 9
            addtomyConsole(i.ToString)
            Thread.Sleep(500)
        Next
        WaitEvent.Set()
    End Sub




    Sub QueryFinishedMovementB() 'it's like QueryFinishedMovement but shorter ticks!

        SerialPort1.DiscardInBuffer()

        'While mytext.Contains("N") = False
        Str = "Checking end of movement..." 'this will NOT be recognised by MS-4 new, but that doesn't matter because it will respond back only when the movement is finished!
        Str += ControlChars.Cr
        SerialPort1.Write(Str)
        addtomyConsole(Str)
        addtomyConsole("..")
        Timer8.Start()


    End Sub

    Private Sub Timer8_Tick_1(sender As System.Object, e As System.EventArgs) Handles Timer8.Tick  'this timer is for queryfinishedmovementB()
        addtomyConsole("..")

        mytext = SerialPort1.ReadExisting
        'addtomyConsole(mytext)
        ' mytext = mytext.TrimStart(mychar)
        If mytext.Contains("A") Then
            addtomyConsole("Stage has stopped")

            Timer8.Stop()
        End If

    End Sub




    Private Sub Button39_Click(sender As System.Object, e As System.EventArgs) Handles Button39.Click
        alignX()
        alignY()
    End Sub

    Dim limit As Integer
    Dim mediumstep As Integer = 9350
    Dim smallstep As Integer = 1


    Private Sub Button51_Click(sender As System.Object, e As System.EventArgs) Handles Button51.Click  'X-ALIGNMENT BUTTON
        alignX()
    End Sub

    Sub alignX()
        RadioButton9.Checked = True  'this turns on darkfield
        limit = CInt(TextBox18.Text)
        'length = lengthlow
        CenteringAlgorithm()
        'Dim wait As Integer = 100

        'medium-step approximation:
        addtomyConsole("now first approx...")
        If (xvalue < -limit) Then
            While (xvalue < -limit)
                Str = "RM X=-"
                Str += mediumstep.ToString
                Str += ControlChars.Cr
                SerialPort1.Write(Str)
                addtomyConsole(Str)
                Threading.Thread.Sleep(500)
                CenteringAlgorithm()
            End While
        ElseIf (xvalue > limit) Then
            While (xvalue > limit)
                Str = "RM X="
                Str += mediumstep.ToString
                Str += ControlChars.Cr
                SerialPort1.Write(Str)

                Threading.Thread.Sleep(500)
                CenteringAlgorithm()
            End While
        End If
        addtomyConsole("done first approx")
        addtomyConsole("now backlash correction...")
        'now move it left by one field width and right by half a field width (to remove backlash)'''''''''''''''''''''''''''''''''''
        Str = "RM X=-"
        Str += mywidth.ToString
        Str += ControlChars.Cr
        SerialPort1.Write(Str)
        Threading.Thread.Sleep(700)

        GrabImage()
        Str = "RM X="
        Str += (Math.Round((mywidth * 9) / 10)).ToString ' I want it to come back a litle more tha 1/2 which is 4/6.
        Str += ControlChars.Cr

        SerialPort1.Write(Str)
        Threading.Thread.Sleep(600)
        GrabImage()
        addtomyConsole("done backlash correction")
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'now fine-step approximation only to the right:
        CenteringAlgorithm()
        addtomyConsole("now fine-stepping..")
        If (xvalue > limit) Then
            While (xvalue > limit)
                'MsgBox("moving right")
                Str = "RM X="
                Str += smallstep.ToString
                Str += ControlChars.Cr
                SerialPort1.Write(Str)
                Threading.Thread.Sleep(400)
                CenteringAlgorithm()
            End While
            'ElseIf (xvalue > limit) Then
            '    While (xvalue > limit)
            '        Str = "RM X="
            '        Str += smallstep.ToString
            '        Str += ControlChars.Cr
            '        SerialPort1.Write(Str)
            '        QueryFinishedMovementB()
            '        'Threading.Thread.Sleep(500)
            '        CenteringAlgorithm()
            '    End While
        End If
        addtomyConsole("done fine-stepping")

    End Sub

    Private Sub Button52_Click(sender As System.Object, e As System.EventArgs) Handles Button52.Click  'Y-alignment
        alignY()
    End Sub

    Sub alignY()
        RadioButton9.Checked = True  'this turns on darkfield
        limit = CInt(TextBox18.Text)
        'length = lengthlow
        CenteringAlgorithm()
        'Dim wait As Integer = 100

        'medium-step approYimation:
        addtomyConsole("now first approY...")
        If (yvalue < -limit) Then
            While (yvalue < -limit)
                Str = "RM Y=-"
                Str += mediumstep.ToString
                Str += ControlChars.Cr
                SerialPort1.Write(Str)
                addtomyConsole(Str)
                Threading.Thread.Sleep(500)
                CenteringAlgorithm()
            End While
        ElseIf (yvalue > limit) Then
            While (yvalue > limit)
                Str = "RM Y="
                Str += mediumstep.ToString
                Str += ControlChars.Cr
                SerialPort1.Write(Str)

                Threading.Thread.Sleep(500)
                CenteringAlgorithm()
            End While
        End If
        addtomyConsole("done first approY")
        addtomyConsole("now backlash correction...")
        'now move it left by one field width and right by half a field width (to remove backlash)'''''''''''''''''''''''''''''''''''
        Str = "RM Y=-"
        Str += mywidth.ToString
        Str += ControlChars.Cr
        SerialPort1.Write(Str)
        Threading.Thread.Sleep(700)

        GrabImage()
        Str = "RM Y="
        Str += (Math.Round((mywidth * 9) / 10)).ToString ' I want it to come back a litle more tha 1/2 which is 4/6.
        Str += ControlChars.Cr

        SerialPort1.Write(Str)
        Threading.Thread.Sleep(600)
        GrabImage()
        addtomyConsole("done backlash correction")
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'now fine-step approYimation only to the right:
        CenteringAlgorithm()
        addtomyConsole("now fine-stepping..")
        If (yvalue > limit) Then
            While (yvalue > limit)
                'MsgBoY("moving right")
                Str = "RM Y="
                Str += smallstep.ToString
                Str += ControlChars.Cr
                SerialPort1.Write(Str)
                Threading.Thread.Sleep(400)
                CenteringAlgorithm()
            End While
            'ElseIf (Yvalue > limit) Then
            '    While (Yvalue > limit)
            '        Str = "RM Y="
            '        Str += smallstep.ToString
            '        Str += ControlChars.Cr
            '        SerialPort1.Write(Str)
            '        QueryFinishedMovementB()
            '        'Threading.Thread.Sleep(500)
            '        CenteringAlgorithm()
            '    End While
        End If
        addtomyConsole("done fine-stepping")

    End Sub


    Private Sub Button53_Click(sender As System.Object, e As System.EventArgs) Handles Button53.Click
        setXzero()
    End Sub
    Sub setXzero()
        Str = "U" & Chr(3) & "0" & Chr(13)
        SerialPort1.Write(Str)
        'Str = "U" & Chr(80) & Chr(13)
        'SerialPort1.Write(Str)
    End Sub
    Private Sub Button54_Click(sender As System.Object, e As System.EventArgs) Handles Button54.Click
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


    Private Sub Button55_Click(sender As System.Object, e As System.EventArgs) Handles Button55.Click
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



    Private Sub Button58_Click(sender As System.Object, e As System.EventArgs) Handles Button58.Click
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
    Private Sub Button59_Click(sender As System.Object, e As System.EventArgs) Handles Button59.Click
        setXYzero()
    End Sub
    Sub setXYzero()
        setYzero()
        Threading.Thread.Sleep(100)
        setXzero()
    End Sub

    Private Sub Button61_Click(sender As System.Object, e As System.EventArgs)
        sendtoINCUSCOPE("1")
    End Sub

    Private Sub Button62_Click(sender As System.Object, e As System.EventArgs)
        sendtoINCUSCOPE("3")
    End Sub

    Private Sub Button63_Click(sender As System.Object, e As System.EventArgs) Handles Button63.Click
        FormSkipPosition.Show()
    End Sub

    Private Sub Button64_Click(sender As System.Object, e As System.EventArgs) Handles Button64.Click
        ExtraExposure.Show()
    End Sub

    Private Sub PictureBox2_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles PictureBox2.Paint
        'drawPostions()
    End Sub


    Private Sub Button65_Click(sender As System.Object, e As System.EventArgs) Handles Button65.Click
        sendtoINCUMOTOR("6")
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

    Private Sub Button68_Click(sender As System.Object, e As System.EventArgs) Handles Button68.Click
        PictureBox1.Image = Image.FromFile(drive & "\Images\Now\Pos1_bf\03-31-2012_(15-47-12-PM)_Pos1_bf_5_1826.jpg")
    End Sub

    Private Sub Button61_Click_1(sender As System.Object, e As System.EventArgs) Handles Button61.Click
        autofhere()
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
                    b = rgbValues((x * 3) + 1 + offset)
                    c = rgbValues((x * 3) + (w * 3) + offset)
                    sum += Math.Abs(b - a)
                    sum += Math.Abs(c - a)
                Next
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        sum = sum / 1000
        'Label8.Text = sum
        Return sum
    End Function
    Private Sub Button62_Click_1(sender As System.Object, e As System.EventArgs) Handles Button62.Click
        lightON()
    End Sub

    Private Sub ListBox8_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)

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

    Private Sub Button69_Click(sender As System.Object, e As System.EventArgs) Handles Button69.Click
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

    Private Sub Button66_Click(sender As System.Object, e As System.EventArgs) Handles Button66.Click
        'up
        'lightON()
        repeats = 1
        'up = True
        focusingSeries()

    End Sub

    Private Sub Button67_Click(sender As System.Object, e As System.EventArgs) Handles Button67.Click
        'down
        ' lightON()
        repeats = 1
        'up = False
        focusingSeries()

    End Sub

    Private Sub Button70_Click(sender As System.Object, e As System.EventArgs) Handles Button70.Click
        'do focus
        'callerisTest = True 
        repeats = 0
        'dofocus()
    End Sub

    Private Sub Button25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button25.Click
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

    Sub focusingSeries()
        'Me.Chart1.Series.Clear()
        RadioButton1.Checked = True 'STEP TYPE 5um.

        'makeChart()


        'ConnectToHID(Me)
        'Dim pHandle As Integer = hidGetHandle(VendorID, ProductID)
        'Dim NameStr As String = "incuMOtor"
        'Dim NameLength As Integer = NameStr.Length
        'Dim Name As Integer = hidGetProductName(pHandle, NameStr, NameLength)
        lightON()

        ConnectToHID(Me)
        'myGetDeviceHandles()
        pName = hidGetProductName(pHandleIncumotor, NameStr, NameLength)
        If NameStr <> "INCUMOTOR" Then
            If testmode = False Then
                MsgBox(NameStr & "  You did not connect the PIC with device name INCUMOTOR")
            End If
        Else
            'Write2Buffer(Str) 'PIC looks for "#" as delimiter
            'hidWrite(pHandleIncumotor, BufferOut(0))
        End If

        If callerisTest Then
            FormTestFocus.Show()
        End If

        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        For Me.vari = 0 To n
            Threading.Thread.Sleep(100) '50

            'GrabImageForLoop_NoGUI()

            GrabimageNO_GUI()
            motorSubforbackground_UP()
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

    Private Sub BackgroundWorker1_ProgressChanged(sender As System.Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        Label10.Text = vari
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



    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        DisconnectFromHID() 'disconnect motor control in order to connect to lights
        lightsOFF()
        'now connect again for motor control:
        ConnectToHID(Me)
        ' myGetDeviceHandles()
        pName = hidGetProductName(pHandleIncumotor, NameStr, NameLength)
        If NameStr <> "INCUMOTOR" Then
            If testmode = False Then
                MsgBox(NameStr & "  You did not connect the PIC with device name INCUMOTOR")
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
        Dim FILE_NAME As String = imagefolder & "\Now\FocusedImages\FocusingTest-maxvalues.txt"  'creates a text file with the values.
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



    Private Sub LinkLabel7_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel7.LinkClicked
        'single focus.
        'up = False 'down
        sendtoINCUMOTOR("k") '20um down
        'Threading.Thread.Sleep(300) '400
        'lightON()
        ' up = True
        callerisTest = True
        repeats = 1
        'so that focusingSeries() only goes up once.
        focusingSeries()
    End Sub

    Private Sub Button71_Click(sender As System.Object, e As System.EventArgs) Handles Button71.Click
        'sendemail("")
    End Sub
    'Sub sendemail(ByVal i As String)
    '    Try

    '        Dim oMail As New SmtpMail("TryIt")
    '        Dim oSmtp As New SmtpClient()

    '        ' Your gmail email address
    '        oMail.From = "julian202@gmail.com"

    '        ' Set recipient email address, please change it to yours
    '        oMail.To = "julian202@gmail.com"

    '        If i = "memory" Then
    '            ' Set email subject
    '            oMail.Subject = "INCUSCOPE IS PROBABLY OUT OF MEMORY! "
    '            ' Set email body
    '            oMail.TextBody = "FREE UP SPACE IN THE COMPUTER TO STORE MORE IMAGES!!!"
    '        Else
    '            ' Set email subject
    '            oMail.Subject = "FOCUSING " & i
    '            ' Set email body
    '            oMail.TextBody = "The value was not 4, it was.. " & i
    '        End If




    '        'Gmail SMTP server address
    '        Dim oServer As New SmtpServer("smtp.gmail.com")

    '        ' If you want to use direct SSL 465 port, 
    '        ' please add this line, otherwise TLS will be used.
    '        ' oServer.Port = 465

    '        ' detect SSL/TLS automatically
    '        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto

    '        ' Gmail user authentication should use your 
    '        ' Gmail email address as the user name. 
    '        ' For example: your email is "gmailid@gmail.com", then the user should be "gmailid@gmail.com"
    '        oServer.User = "julian202@gmail.com"
    '        oServer.Password = "Caixa08jp"

    '        Try

    '            Console.WriteLine("start to send email over SSL ...")
    '            oSmtp.SendMail(oServer, oMail)
    '            Console.WriteLine("email was sent successfully!")

    '        Catch ep As Exception

    '            Console.WriteLine("failed to send email with the following error:")
    '            Console.WriteLine(ep.Message)
    '        End Try


    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '    End Try
    'End Sub


    Private Sub Button72_Click(sender As System.Object, e As System.EventArgs) Handles Button72.Click
        Str = "r" & Chr(1)
        sendtoINCUSCOPE(Str)
    End Sub

    Private Sub Button73_Click(sender As System.Object, e As System.EventArgs) Handles Button73.Click
        Str = "f" & Chr(1)
        sendtoINCUSCOPE(Str)
    End Sub

    Private Sub Button74_Click(sender As System.Object, e As System.EventArgs) Handles Button74.Click
        Str = "f" & Chr(0)
        sendtoINCUSCOPE(Str)
    End Sub

    Private Sub Button75_Click(sender As System.Object, e As System.EventArgs) Handles Button75.Click
        Str = "r" & Chr(0)
        sendtoINCUSCOPE(Str)
    End Sub





    Dim nstep As Integer = 0
    Private Sub Timer8_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'nstep = nstep + 1
        'If nstep = 1 Then
        '    moveStepper(0, 10)

        'ElseIf nstep = 2 Then
        '    moveStepper(1, 10)
        'Else
        '    nstep = 0
        '    'GrabImage()
        '    'SaveImage()
        'End If
        'GrabImage()
        'SaveImage()



        'nstep = nstep + 1
        'If nstep < 11 Then
        '    MUp()
        '    'moveStepper(0, 1, 1)

        'ElseIf nstep > 10 And nstep < 21 Then

        '    ' moveStepper(1, 1, 1)
        'Else
        '    nstep = 0
        '    'GrabImage()
        '    'SaveImage()
        'End If
        'GrabImage()
        'SaveImage()








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
                MsgBox(NameStr & "  You did not connect the PIC with device name POLOLUPIC")
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

    Sub moveSlide(ByVal forward As Byte, ByVal steps As Byte, ByVal repetitions As Byte)

        'Dim b As Byte = 0
        'Dim c As Byte = 2

        ConnectToHID(Me)
        nitems = hidGetItemCount()
        For i = 0 To (nitems - 1)
            pHandleIncumotor = hidGetItem(i)
            pName = hidGetProductName(pHandleIncumotor, NameStr, NameLength)
            If NameStr = "INCUSLIDE" Then
                Exit For
            End If
        Next
        If NameStr <> "INCUSLIDE" Then
            MsgBox(NameStr & "  You did not connect the PIC with device name INCUSLIDE")
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

    Private Sub RadioButton13_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton13.CheckedChanged
        If loaded = True Then
            If RadioButton13.Checked Then
                selectedCamSerial = serial1

            End If
            If RadioButton14.Checked Then
                selectedCamSerial = serial2



            End If

            guid = busMgr.GetCameraFromSerialNumber(selectedCamSerial)

            'cam = New ManagedCamera()
            cam.Connect(guid)
            myControlDialog.Connect(cam)
        End If

    End Sub




    Private Sub Button80_Click(sender As System.Object, e As System.EventArgs) Handles Button80.Click
        If RadioButton15.Checked Then
            moveSlide(0, 255, 1)
        ElseIf RadioButton16.Checked Then
            moveSlide(0, 255, 26)
        End If


    End Sub

    Private Sub Button81_Click(sender As System.Object, e As System.EventArgs) Handles Button81.Click
        If RadioButton15.Checked Then
            moveSlide(1, 255, 1)
        ElseIf RadioButton16.Checked Then
            moveSlide(1, 255, 24)
        End If

    End Sub

    Dim nbyn As Integer = 4
    Private Sub TextBox7_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox7.TextChanged
        nbyn = TextBox7.Text
    End Sub

    Private Sub Button78_Click(sender As System.Object, e As System.EventArgs) Handles Button78.Click


        If CheckBox15.Checked Then 'this is the switch xy checkbox.
            If pololumode = True Then
                addnbynOLDstage() 'uni-directional checkbox is included here!
            Else
                addnbyn() 'you dont have uni-directional for non-pololu
            End If

        Else
            If CheckBox14.Checked = True Then  'this is the uni-directional checkbox.
                addnbynNEWstageONEdir()
            Else
                addnbynNEWstage()
            End If
        End If







        'If pololumode = True Then
        '    If CheckBox15.Checked Then  'this is the switch xy checkbox.
        '        addnbynOLDstage()
        '    Else
        '        If CheckBox14.Checked = True Then  'this is the uni-directional checkbox.
        '            addnbynNEWstageONEdir()
        '        Else
        '            addnbynNEWstage()
        '        End If
        '    End If

        'ElseIf pololumode = False Then
        '    If CheckBox15.Checked Then
        '        addnbyn()
        '    Else
        '        If CheckBox14.Checked = True Then  'this is the uni-directional checkbox.
        '            addnbynNEWstageONEdir()
        '        Else
        '            addnbynNEWstage()
        '        End If
        '    End If
        'End If


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
            addnbyn()
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
        loaddata()
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
        loaddata()
    End Sub



    Sub ycolumn()
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
    End Sub

    Sub ycolumnONEdir()
        ycolumn()
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
    End Sub

    Sub xcolumnONEdir()
        For j As Integer = 1 To nbyn
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
        loaddata()

    End Sub

    Sub addnbynNEWstageONEdir()

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

        For j = 1 To CInt(TextBox12.Text) 'TextBox12 is number of grids in y direction

            If (j Mod 2) <> 0 Then   'this is j odd. 

                For k = 1 To CInt(TextBox11.Text)  'TextBox11 is number of grids in x direction
                    If k <> 1 Then
                        Xpos = Xpos - wellwidth
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
                Ypos = Ypos - wellheight
                For k = 1 To CInt(TextBox11.Text)
                    If k <> 1 Then
                        Xpos = Xpos + wellwidth
                    End If
                    ListBox2.Items.Add(Xpos)
                    My.Settings.PositionsX.Add(Xpos)
                    ListBox3.Items.Add(Ypos)
                    My.Settings.PositionsY.Add(Ypos)
                    ycolumnONEdir()
                    xcolumnONEdir()
                Next
                Ypos = Ypos - wellheight

            End If

        Next




        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()
    End Sub

    Sub addnbynNEWstage()

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
                    ycolumn()
                    xcolumn()
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
                    ycolumn()
                    xcolumn()
                Next
                Ypos = Ypos - wellheight

            End If




        Next




        My.Settings.Save()
        ListBox1.Items.Clear()
        loaddata()

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
        If pololumode Then
            Xpos = Label36.Text
            Ypos = Label37.Text
        Else
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
                Xpos = SerialPort1.ReadExisting
                Xpos = Xpos.TrimStart(mychar)

                'ListBox2.Items.Add(Xpos)
            Catch ex As Exception
            End Try


        End If

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

        If pololumode Then
            'Xpos = Label36.Text
            'Ypos = Label37.Text
        Else


            Str = "w y"
            Str += ControlChars.Cr
            Try
                SerialPort1.Write(Str)
            Catch ex As Exception
            End Try
            Threading.Thread.Sleep(100)
            Try
                Ypos = SerialPort1.ReadExisting
                Ypos = mytext.TrimStart(mychar)
            Catch ex As Exception
            End Try
        End If


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
    Private Sub CheckBox8_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox8.CheckedChanged

        If CheckBox8.Checked Then
            testmode = True
            Label18.Text = "Test Version"
            My.Settings.testmode = True

            ' SerialPort1.PortName = "COM3"
        Else
            testmode = False
            My.Settings.testmode = False
            If IncuLeft Then
                Label18.Text = "Left Version"
                '  Me.Text = Me.Text & " Left Version"
                'SerialPort1.PortName = "COM3"
            Else
                Label18.Text = "Right Version"
                ' Me.Text = Me.Text & " Right Version"
                'SerialPort1.PortName = "COM5"
            End If
        End If
        My.Settings.Save()

    End Sub

    Private Sub Label32_MouseHover(sender As System.Object, e As System.EventArgs) Handles Label32.MouseHover
        ToolTip2.Show("Test mode: shows no error dialogs and does not save images to vanadium.", Label32)
    End Sub


    Private Sub CheckBox8_MouseHover(sender As System.Object, e As System.EventArgs) Handles CheckBox8.MouseHover
        ToolTip2.Show("Test mode: shows no error dialogs and does not save to vanadium.", Label32)
    End Sub



    Private Sub Button79_Click(sender As System.Object, e As System.EventArgs) Handles Button79.Click
        lightTestON()

    End Sub

    Private Sub Button82_Click(sender As System.Object, e As System.EventArgs) Handles Button82.Click
        lightsOFF()
    End Sub

    Private Sub CheckBox10_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox10.CheckedChanged
        If CheckBox10.Checked Then
            pololumode = True
            Label18.Text = "Pololu"
            My.Settings.pololumode = True
            Panel5.Visible = True

            'select ms-4 because it has the right orientation for pololu
            My.Settings.versionMS2000 = False
            'distancHorizontal = "-1241"
            'distancVertical = "-621"
            Label10.Text = "Using Stage MS-4..."

            My.Settings.Save()
            versionMS2000 = My.Settings.versionMS2000

        Else
            pololumode = False
            If IncuLeft Then
                Label18.Text = "Left Version"
                '  Me.Text = Me.Text & " Left Version"
                'SerialPort1.PortName = "COM3"
            Else
                Label18.Text = "Right Version"
                ' Me.Text = Me.Text & " Right Version"
                'SerialPort1.PortName = "COM5"
            End If
            My.Settings.pololumode = False
            My.Settings.Save()
            'MsgBox("pololu mode set to false")
        End If

    End Sub

    Private Sub Button83_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub Button83_Click_1(sender As System.Object, e As System.EventArgs) Handles Button83.Click
        If Panel5.Visible = True Then
            Panel5.Visible = False
        Else
            Panel5.Visible = True
        End If
    End Sub

    Private Sub TextBox8_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox8.TextChanged
        pololusteps = TextBox8.Text
        My.Settings.pololusteps = pololusteps
        My.Settings.Save()

        If CheckBox11.Checked Then
            pololustepsY = pololusteps * 1.344
            TextBox10.Text = pololustepsY
            My.Settings.pololustepsY = pololustepsY
            My.Settings.Save()
        End If
    End Sub

    Private Sub TextBox9_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox9.TextChanged
        Try
            pololurepetitions = TextBox9.Text
        Catch ex As Exception
            pololurepetitions = 1
        End Try

        My.Settings.pololurepetitions = pololurepetitions
        My.Settings.Save()
    End Sub


    Private Sub TextBox10_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox10.TextChanged
        pololustepsY = TextBox10.Text
        My.Settings.pololustepsY = pololustepsY
        My.Settings.Save()
    End Sub

    Private Sub ListBox1_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles ListBox1.MouseDoubleClick
        If pololumode Then
            gotoselectedposPololu()
        ElseIf mcl Then
            gotoselectedposMcl()
        Else
            gotoselectedpos()
        End If
    End Sub



    Private Sub CheckBox12_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox12.CheckedChanged
        If CheckBox12.Checked Then
            mcl = True
            Label18.Text = "mcl"
            My.Settings.mcl = True
            ' Panel5.Visible = True

            'select ms-4 because it has the right orientation for pololu
            My.Settings.versionMS2000 = False
            'distancHorizontal = "-1241"
            'distancVertical = "-621"
            Label10.Text = "Using Stage MS-4..."

            versionMS2000 = My.Settings.versionMS2000
            My.Settings.Save()
        Else
            mcl = False
            If IncuLeft Then
                Label18.Text = "Left Version"
                '  Me.Text = Me.Text & " Left Version"
                'SerialPort1.PortName = "COM3"
            Else
                Label18.Text = "Right Version"
                ' Me.Text = Me.Text & " Right Version"
                'SerialPort1.PortName = "COM5"
            End If
            My.Settings.mcl = False
            My.Settings.Save()
            'MsgBox("pololu mode set to false")
        End If

    End Sub

    Dim PictureBox3Showed As Boolean = False



    Private Sub LinkLabel8_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel8.LinkClicked
        showzoom()
    End Sub

    Private Sub LinkLabel9_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel9.LinkClicked
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




    Private Sub Button76_Click(sender As System.Object, e As System.EventArgs) Handles Button76.Click
        'event clicked save button.

        mywidth = TextBox14.Text.ToString
        myheigth = TextBox13.Text.ToString
        wellwidth = TextBox16.Text.ToString
        wellheight = TextBox15.Text.ToString


        If RadioButton17.Checked Then  'radiobuttonAclicked
            My.Settings.wellwidth = wellwidth
            My.Settings.wellheight = wellheight
            My.Settings.mywidth = mywidth
            My.Settings.myheigth = myheigth
            My.Settings.ConfigNameA = TextBox17.Text
            My.Settings.Save()
        ElseIf RadioButton18.Checked Then  'radiobuttonBclicked
            My.Settings.wellwidthB = wellwidth
            My.Settings.wellheightB = wellheight
            My.Settings.mywidthB = mywidth
            My.Settings.myheigthB = myheigth
            My.Settings.ConfigNameB = TextBox17.Text
            My.Settings.Save()
        ElseIf RadioButton19.Checked Then  'radiobuttonCclicked
            My.Settings.wellwidthC = wellwidth
            My.Settings.wellheightC = wellheight
            My.Settings.mywidthC = mywidth
            My.Settings.myheigthC = myheigth
            My.Settings.ConfigNameC = TextBox17.Text
            My.Settings.Save()
        End If





    End Sub

    Private Sub RadioButton18_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton18.CheckedChanged
        'radiobuttonBclicked
        If RadioButton18.Checked Then
            buttonBclicked()
        End If
    End Sub

    Sub buttonBclicked()

        wellwidth = My.Settings.wellwidthB
        wellheight = My.Settings.wellheightB
        mywidth = My.Settings.mywidthB
        myheigth = My.Settings.myheigthB
        TextBox17.Text = My.Settings.ConfigNameB
        TextBox14.Text = mywidth
        TextBox13.Text = myheigth
        TextBox16.Text = wellwidth
        TextBox15.Text = wellheight
        My.Settings.SelectedConfig = "B"
        My.Settings.Save()



    End Sub

    Private Sub RadioButton17_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton17.CheckedChanged
        'radiobuttonAclicked
        If RadioButton17.Checked Then
            buttonAclicked()
        End If
    End Sub
    Sub buttonAclicked()
        wellwidth = My.Settings.wellwidth
        wellheight = My.Settings.wellheight
        mywidth = My.Settings.mywidth
        myheigth = My.Settings.myheigth
        TextBox17.Text = My.Settings.ConfigNameA
        TextBox14.Text = mywidth
        TextBox13.Text = myheigth
        TextBox16.Text = wellwidth
        TextBox15.Text = wellheight
        My.Settings.SelectedConfig = "A"
        My.Settings.Save()
    End Sub

    Private Sub RadioButton19_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RadioButton19.CheckedChanged
        'radiobuttonCclicked
        If RadioButton19.Checked Then
            buttonCclicked()
        End If
    End Sub

    Sub buttonCclicked()
        wellwidth = My.Settings.wellwidthC
        wellheight = My.Settings.wellheightC
        mywidth = My.Settings.mywidthC
        myheigth = My.Settings.myheigthC
        TextBox17.Text = My.Settings.ConfigNameC
        TextBox14.Text = mywidth
        TextBox13.Text = myheigth
        TextBox16.Text = wellwidth
        TextBox15.Text = wellheight
        My.Settings.SelectedConfig = "C"
        My.Settings.Save()
    End Sub






    Sub addtomyConsole(value As String)
        If ListBox8.Items.Count > 50 Then
            ListBox8.Items.RemoveAt(0)
        End If
        ListBox8.Items.Add(value)
        ListBox8.SelectedIndex = (ListBox8.Items.Count - 1)
        ListBox8.Refresh()
    End Sub


    Private Sub Button36_Click(sender As System.Object, e As System.EventArgs) Handles Button36.Click

        For i As Integer = 1 To 70
            'If ListBox8.Items.Count > 50 Then
            '    ListBox8.Items.RemoveAt(0)
            'End If

            'ListBox8.Items.Add(i.ToString)
            ''ListBox8.n(Environment.NewLine)
            '' ListBox8.SelectionStart = RichTextBox1.Text.Length
            '' ListBox8.ScrollToCaret()
            'ListBox8.SelectedIndex = (ListBox8.Items.Count - 1)
            'ListBox8.Refresh()

            addtomyConsole(i.ToString)

            Threading.Thread.Sleep(30)
        Next


    End Sub

    Private Sub Button37_Click(sender As System.Object, e As System.EventArgs) Handles Button37.Click
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
        addtomyConsole(mytext)
    End Sub




    Private Sub Button38_Click(sender As System.Object, e As System.EventArgs) Handles Button38.Click   'Sets stage parameters

        Str = "minspeed "
        Str += TextBox19.Text
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
            addtomyConsole("error")
        End Try
        addtomyConsole(Str)
        Threading.Thread.Sleep(400)
        Str = SerialPort1.ReadExisting
        addtomyConsole(Str)

        Str = "speed "
        Str += TextBox20.Text
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
            addtomyConsole("error")
        End Try
        addtomyConsole(Str)
        Threading.Thread.Sleep(400)
        Str = SerialPort1.ReadExisting
        addtomyConsole(Str)

        Str = "rampslope "
        Str += TextBox21.Text
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
            addtomyConsole("error")
        End Try
        addtomyConsole(Str)
        Threading.Thread.Sleep(400)
        Str = SerialPort1.ReadExisting
        addtomyConsole(Str)


    End Sub




    Private Sub LinkLabel10_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel10.LinkClicked
        ListBox8.Items.Clear()
    End Sub

    Private Sub Button49_Click(sender As System.Object, e As System.EventArgs) Handles Button49.Click
        Str = "speed "
        Str += TextBox20.Text
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
            addtomyConsole("error")
        End Try
        addtomyConsole(Str)
        Threading.Thread.Sleep(400)
        Str = SerialPort1.ReadExisting
        addtomyConsole(Str)
    End Sub

    Private Sub Button45_Click(sender As System.Object, e As System.EventArgs) Handles Button45.Click

        Str = "rampslope "
        Str += TextBox21.Text
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
            addtomyConsole("error")
        End Try
        addtomyConsole(Str)
        Threading.Thread.Sleep(400)
        Str = SerialPort1.ReadExisting
        addtomyConsole(Str)
    End Sub

    Private Sub Button44_Click(sender As System.Object, e As System.EventArgs) Handles Button44.Click
        Str = "minspeed "
        Str += TextBox19.Text
        Str += ControlChars.Cr
        Try
            SerialPort1.Write(Str)
        Catch ex As Exception
            addtomyConsole("error")
        End Try
        addtomyConsole(Str)
        Threading.Thread.Sleep(400)
        Str = SerialPort1.ReadExisting
        addtomyConsole(Str)
    End Sub



End Class



' SerialPort1.close()
' disconnect from cam
'disconnecty from hid