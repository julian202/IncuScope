Public Class FormTimedDialog



    Private Sub FormTimedDialog_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        RichTextBox1.SelectionAlignment = HorizontalAlignment.Center
        RichTextBox1.Text = timedMessage

        Label3.Text = time.ToString
        Timer1.Start()

    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        If time = 0 Then
            Me.Close()
        End If

        time = time - 1
        Label3.Text = time.ToString
    End Sub

    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged

    End Sub

End Class