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

    Private Sub PenjualanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PenjualanToolStripMenuItem.Click
        Dim FormPenjualan = New FormPenjualan
        FormPenjualan.MdiParent = Me
        FormPenjualan.Show()
    End Sub

    Private Sub DataSemiaPembeliToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataSemiaPembeliToolStripMenuItem.Click
        Dim FormReportSemuaPembeli = New FormReportSemuaPembeli
        FormReportSemuaPembeli.MdiParent = Me
        FormReportSemuaPembeli.Show()
    End Sub

    Private Sub DataSemuaBarangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataSemuaBarangToolStripMenuItem.Click
        Dim FormReportSemuaBarang = New FormReportSemuaBarang
        FormReportSemuaBarang.MdiParent = Me
        FormReportSemuaBarang.Show()
    End Sub

    Private Sub LaporanTransaksiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LaporanTransaksiToolStripMenuItem.Click
        Dim FormReportSemuaTransaksi = New FormReportSemuaTransaksi
        FormReportSemuaTransaksi.MdiParent = Me
        FormReportSemuaTransaksi.Show()
    End Sub
End Class
