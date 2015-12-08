Public Class FormTestFocus

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        repeats = 0
        Label1.Text = "Cancelling..."
        Me.Close()
    End Sub

    Private Sub FormTestFocus_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        previousnmax = 0
        nmax = 0
        Label1.Text = "Testing Focus... "
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False
        Label6.Visible = False
        Label7.Visible = False
        Label8.Visible = False
        Label9.Visible = False
    End Sub
End Class