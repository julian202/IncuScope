Public Class FormCamSelection

    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'IncuLeft = True
        selectedCamSerial = ComboBox1.Text
        Me.Close()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'IncuLeft = False
        selectedCamSerial = ComboBox2.Text
        Me.Close()
    End Sub

    Private Sub FormCamSelection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = Label3.Text & "" & numCamerasDetected
        Try
            ComboBox1.Text = serial1
            ComboBox1.Items.Add(serial1)
            ComboBox1.Items.Add(serial2)
        Catch ex As Exception
        End Try

        Try
            ComboBox2.Text = serial1
            ComboBox2.Items.Add(serial1)
            ComboBox2.Items.Add(serial2)
        Catch ex As Exception
        End Try

        ComboBox3.Text = "MS-4"
        ComboBox3.Items.Add("MS-4")
        ComboBox3.Items.Add("MS-2000")
        ComboBox4.Text = "MS-2000"
        ComboBox4.Items.Add("MS-4")
        ComboBox4.Items.Add("MS-2000")



        'ComboBox1.Text = 10441693 '10441654



        
    End Sub

  
    
End Class