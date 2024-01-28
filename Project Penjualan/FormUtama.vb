Public Class FormUtama

    Private Sub LogoutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        End
    End Sub

    Private Sub BarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BarangToolStripMenuItem.Click
        Dim FormBarang = New FormBarang
        FormBarang.MdiParent = Me
        FormBarang.Show()
    End Sub

    Private Sub PembeliToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PembeliToolStripMenuItem.Click
        Dim FormPembeli = New FormPembeli
        FormPembeli.MdiParent = Me
        FormPembeli.Show()
    End Sub
End Class
