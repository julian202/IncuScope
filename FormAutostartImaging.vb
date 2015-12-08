Public Class FormAutostartImaging

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Incuscope.My.Settings.AutoStartCheckBox = True
        Incuscope.My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Incuscope.My.Settings.AutoStartCheckBox = False
        Incuscope.My.Settings.Save()
        Me.Close()
    End Sub
End Class