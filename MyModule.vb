Module MyModule
    Public interval As Integer
    Public shutterfluob As New FlyCapture2Managed.CameraProperty
    Public shutterfluo As New FlyCapture2Managed.CameraProperty
    Public shutterBF As New FlyCapture2Managed.CameraProperty
    Public shutterBFb As New FlyCapture2Managed.CameraProperty
    Public gainCam1 As New FlyCapture2Managed.CameraProperty
    Public gainCam2 As New FlyCapture2Managed.CameraProperty

    Public recordedfocus(200) As Integer
    Public AutofocusArray(200) As Integer
    Public usingCam1 = True
    Public usingCam2 = False
    Public drive As String
    Public time As Integer = 4
    Public timedMessage As String
    'Public objective As String
    Public imageSubfolder As String
    Public imagefolder As String
    Public sessionfolder As String
    Public sessionfolderOriginal As String
    Public infotext As String
    Public thisPC As String
    Public vanadiumName As String
    Public vanadiumfolder As String
    Public vanadiumfolderOriginal As String
    Public cancelSession As Boolean = False
    Public stopThread As Boolean
    Public directoryInfo As System.IO.DirectoryInfo
    'Public positionFchange1 As Integer
    'Public positionFchange2 As Integer
    'Public positionFchange3 As Integer
    'Public positionFchange4 As Integer

    Public positionFchange(11) As Integer


    'Public FchangeUp1 As Boolean
    'Public FchangeUp2 As Boolean
    'Public FchangeUp3 As Boolean
    'Public FchangeUp4 As Boolean

    Public FchangeUp(11) As Boolean

    'Public Ftimes1 As Integer
    'Public Ftimes2 As Integer
    'Public Ftimes3 As Integer
    'Public Ftimes4 As Integer

    Public Ftimes(11) As Integer


    ' Public IncuLeft As Boolean
    Public numCamerasDetected As Integer
    Public serial1 As Integer
    Public serial2 As Integer
    Public serial3 As Integer
    Public selectedCamSerial As Integer

    Public skip(1000) As Boolean  'lets suppose there's never going to be more than 100 positions.
    Public skipOnlyLight(1000) As Boolean  'lets suppose there's never going to be more than 100 positions.

    Public exposureArray(1000) As Double

    Public repeats As Integer = 8

    Public nmax As Integer = 0
    'Dim testcolor As Drawing.Color
    Public previousnmax As Integer = 0
End Module
