Imports MySql.Data.MySqlClient

Public Class FormPenjualan
    Public strSQL As String
    Public db As MySqlConnection
    Public cmd As MySqlCommand
    Public dadapter As MySqlDataAdapter
    Public datardr As MySqlDataReader
    Public ds As DataSet
    Dim conn As AksesData.KoneksiDB

    Public display, simpan, ubah, hapus As String

    Private Sub FormPenjualan_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        FormUtama.Enabled = True
    End Sub

    Private Sub tambah()
        DataGridView1.Enabled = False

        TextBoxIDTransaksi.Clear()
        TextBoxTanggal.Clear()
        TextBoxPembeli.Clear()
        TextBoxBarang.Clear()
        TextBoxKeterangan.Clear()

        TextBoxIDTransaksi.Enabled = False
        TextBoxTanggal.Enabled = True
        TextBoxPembeli.Enabled = True
        TextBoxBarang.Enabled = True
        TextBoxKeterangan.Enabled = True

        ComboBoxPembeli.Enabled = True
        ComboBoxBarang.Enabled = True

        TextBoxTanggal.Focus()

        ButtonTambah.Enabled = False
        ButtonHapus.Enabled = False
        ButtonUbah.Enabled = True
        ButtonSimpan.Enabled = True
        ButtonBatal.Enabled = True
    End Sub

    Private Sub comboPembeli()
        Call bukaDB()
        cmd = New MySqlCommand("select * from pembeli", Koneksi)
        datardr = cmd.ExecuteReader
        ComboBoxPembeli.Items.Clear()
        Do While datardr.Read
            ComboBoxPembeli.Items.Add(datardr("id_pembeli"))
        Loop
        datardr.Close()
    End Sub

    Private Sub comboBarang()
        Call bukaDB()
        cmd = New MySqlCommand("select * from barang", Koneksi)
        datardr = cmd.ExecuteReader
        ComboBoxBarang.Items.Clear()
        Do While datardr.Read
            ComboBoxBarang.Items.Add(datardr("id_barang"))
        Loop
        datardr.Close()
    End Sub


    Private Sub bersih()
        TextBoxIDTransaksi.Text = ""
        TextBoxTanggal.Text = ""
        TextBoxPembeli.Text = ""
        TextBoxBarang.Text = ""
        TextBoxKeterangan.Text = ""
        ComboBoxBarang.Text = ""
        ComboBoxPembeli.Text = ""
        TextBoxTanggal.Focus()
    End Sub

    Private Sub displayData()
        ModuleKoneksi.bukaDB()
        display = "select * from transaksi"
        DA = New MySqlDataAdapter(display, Koneksi)
        ds = New DataSet
        DA.Fill(ds, "transaksi")
        DataGridView1.Refresh()
        DataGridView1.DataSource = ds.Tables("transaksi")
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.ReadOnly = True
    End Sub

    Private Sub ButtonKeluar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKeluar.Click
        FormUtama.Show()
        Me.Hide()
    End Sub

    Private Sub FormPenjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        bukaDB()
        bersih()
        comboPembeli()
        comboBarang()
        DataGridView1.Refresh()
        displayData()
        ButtonSimpan.Enabled = False
    End Sub

    Private Sub ComboBoxPembeli_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxPembeli.LostFocus
        Try
            Call bukaDB()
            cmd = New MySqlCommand("select * from pembeli where id_pembeli = '" & ComboBoxPembeli.Text & "'", Koneksi)

            datardr = cmd.ExecuteReader
            If datardr.HasRows Then
                datardr.Read()
                TextBoxPembeli.Text = datardr("nama_pembeli")
                ComboBoxPembeli.Focus()
            Else
                ComboBoxPembeli.Text = ""
            End If
            datardr.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ComboBoxPembeli_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxPembeli.SelectedIndexChanged
        Try
            Call bukaDB()
            cmd = New MySqlCommand("select * from pembeli where id_pembeli = '" & ComboBoxPembeli.Text & "'", Koneksi)

            datardr = cmd.ExecuteReader
            If datardr.HasRows Then
                datardr.Read()
                TextBoxPembeli.Text = datardr("nama_pembeli")
                ComboBoxPembeli.Focus()
            Else
                ComboBoxPembeli.Text = ""
            End If
            datardr.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ComboBoxBarang_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxBarang.LostFocus
        Try
            Call bukaDB()
            cmd = New MySqlCommand("select * from barang where id_barang = '" & ComboBoxBarang.Text & "'", Koneksi)

            datardr = cmd.ExecuteReader
            If datardr.HasRows Then
                datardr.Read()
                TextBoxBarang.Text = datardr("nama_barang")
                ComboBoxBarang.Focus()
            Else
                ComboBoxBarang.Text = ""
            End If
            datardr.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ComboBoxBarang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxBarang.SelectedIndexChanged
        Try
            Call bukaDB()
            cmd = New MySqlCommand("select * from barang where id_barang = '" & ComboBoxBarang.Text & "'", Koneksi)

            datardr = cmd.ExecuteReader
            If datardr.HasRows Then
                datardr.Read()
                TextBoxBarang.Text = datardr("nama_barang")
                ComboBoxBarang.Focus()
            Else
                ComboBoxBarang.Text = ""
            End If
            datardr.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ButtonBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBatal.Click
        bersih()
    End Sub

    Private Sub ButtonTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTambah.Click
        tambah()
    End Sub

    Private Sub ButtonSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpan.Click
        Call bukaDB()
        cmd = New MySqlCommand("select * from transaksi where id_transaksi = '" & TextBoxIDTransaksi.Text & "'", Koneksi)
        RD = cmd.ExecuteReader
        RD.Read()

        If ButtonSimpan.Enabled = True Then
            If RD.HasRows Then
                MsgBox("Maaf, Data Transaksi dengan ID Transaksi tersebut telah ada", MsgBoxStyle.Exclamation, "Peringatan")
                Call bukaDB()
                Call displayData()
            Else
                If MessageBox.Show("Simpan data Transksi Penjualan Baru?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Call bukaDB()
                    simpan = "insert into transaksi(id_pembeli, id_barang, tanggal, keterangan) values ('" &
                                ComboBoxPembeli.Text & "','" &
                                ComboBoxBarang.Text & "','" &
                                TextBoxTanggal.Text & "','" &
                                TextBoxKeterangan.Text & "')"

                    cmd = New MySqlCommand(simpan, Koneksi)
                    cmd.ExecuteNonQuery()
                    Call displayData()
                    TextBoxTanggal.Focus()
                    MsgBox("Data Transaksi Penjualan sudah tersimpan", MsgBoxStyle.Information, "Info")
                    RD.Close()
                End If
                Call displayData()
                Call bersih()
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBoxIDTransaksi.ReadOnly = True
        bukaDB()
        cmd = New MySqlCommand("select * from transaksi where id_transaksi = '" & TextBoxIDTransaksi.Text & "'", Koneksi)

        Try
            Dim gridbaris As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            TextBoxIDTransaksi.Text = gridbaris.Cells(0).Value.ToString
            ComboBoxPembeli.Text = gridbaris.Cells(1).Value.ToString
            ComboBoxBarang.Text = gridbaris.Cells(2).Value.ToString
            TextBoxTanggal.Text = gridbaris.Cells(3).Value.ToString
            TextBoxKeterangan.Text = gridbaris.Cells(4).Value.ToString
        Catch ex As Exception
            MsgBox("Pilih data transaksi penjualan yang ada di dalam tabel", MsgBoxStyle.Information, "Info")
        End Try
    End Sub

    Private Sub ButtonUbah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUbah.Click
        Call bukaDB()
        Try
            Using conn As New MySqlConnection(Koneksi.ConnectionString)
                conn.Open()

                ubah = "update transaksi set " &
                                "id_pembeli = '" & ComboBoxPembeli.Text & "'," &
                                "id_barang = '" & ComboBoxBarang.Text & "'," &
                                "tanggal = '" & TextBoxTanggal.Text & "'," &
                                "keterangan = '" & TextBoxKeterangan.Text & "'" &
                                "where id_transaksi = '" & TextBoxIDTransaksi.Text & "'"

                Dim command As New MySqlCommand(ubah, conn)
                command.ExecuteNonQuery()
                MessageBox.Show("Data transaksi penjualan sukses tersimpan", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Kesalahan dalam ubah data", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Call displayData()
        Call bersih()
    End Sub


    Private Sub ButtonHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHapus.Click
        If TextBoxIDTransaksi.Text = "" Then
            MessageBox.Show("Pilih dahulu data transaksi penjuaan yang akan Anda hapus pada tabel di atas!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If TextBoxIDTransaksi.Text <> "" Then
                If MessageBox.Show("Apakah Anda yakin akan menghapus data transaksi penjualan ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Call bukaDB()
                    hapus = "delete from transaksi where id_transaksi = '" & TextBoxIDTransaksi.Text & "'"
                    cmd = New MySqlCommand(hapus, Koneksi)

                    cmd.ExecuteNonQuery()
                    Call displayData()
                    Call bersih()
                    TextBoxIDTransaksi.Focus()
                    MessageBox.Show("Data transaksi penjualan berhasil dihapus", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End If
    End Sub





    
End Class