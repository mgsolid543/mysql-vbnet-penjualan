Imports MySql.Data.MySqlClient

Public Class FormPembeli

    Public strSQL As String
    Public DB As MySqlConnection
    Public CMD As MySqlCommand
    Public ADP As MySqlDataAdapter
    Public DR As MySqlDataReader
    Public DS As New DataSet

    Public simpan, hapus As String

    Sub tambah()
        DataGridView1.Enabled = False
        TextBoxNamaPembeli.ReadOnly = False

        TextBoxNamaPembeli.Clear()
        TextBoxJenisKelamin.Clear()
        TextBoxNoTelepon.Clear()
        TextBoxAlamat.Clear()

        TextBoxNamaPembeli.Enabled = True
        TextBoxJenisKelamin.Enabled = True
        TextBoxNoTelepon.Enabled = True
        TextBoxAlamat.Enabled = True

        TextBoxNamaPembeli.Focus()

        ButtonTambah.Enabled = False
        ButtonHapus.Enabled = False
        ButtonUbah.Enabled = True
        ButtonSimpan.Enabled = True
        ButtonBatal.Enabled = True
    End Sub

    Sub ubah()
        TextBoxNamaPembeli.Enabled = True
        TextBoxJenisKelamin.Enabled = True
        TextBoxNoTelepon.Enabled = True
        TextBoxAlamat.Enabled = True

        TextBoxNamaPembeli.Focus()

        ButtonTambah.Enabled = True
        ButtonHapus.Enabled = True
        ButtonUbah.Enabled = True
        ButtonSimpan.Enabled = False
        ButtonBatal.Enabled = True
    End Sub

    Sub batal()
        DataGridView1.Enabled = True
        TextBoxNamaPembeli.Clear()
        TextBoxJenisKelamin.Clear()
        TextBoxNoTelepon.Clear()
        TextBoxAlamat.Clear()

        TextBoxNamaPembeli.Enabled = True
        TextBoxJenisKelamin.Enabled = True
        TextBoxNoTelepon.Enabled = True
        TextBoxAlamat.Enabled = True

        TextBoxNamaPembeli.Focus()

        ButtonTambah.Enabled = True
        ButtonHapus.Enabled = True
        ButtonUbah.Enabled = True
        ButtonSimpan.Enabled = False
        ButtonBatal.Enabled = True
    End Sub

    Sub bersih()
        TextBoxNamaPembeli.Clear()
        TextBoxJenisKelamin.Clear()
        TextBoxNoTelepon.Clear()
        TextBoxAlamat.Clear()
        TextBoxNamaPembeli.Focus()
    End Sub

    Sub isiGrid()
        ModuleKoneksi.bukaDB()
        DA = New MySqlDataAdapter("select nama_pembeli, jk, no_telp, alamat from pembeli", Koneksi)

        DS = New DataSet
        DA.Fill(DS, "pembeli")

        DataGridView1.DataSource = DS.Tables("pembeli")
        DataGridView1.ReadOnly = True
    End Sub


    Private Sub ButtonKeluar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonKeluar.Click
        FormUtama.Show()
        Me.Hide()
    End Sub

    Private Sub FormBarang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call isiGrid()
        ubah()
    End Sub

    Private Sub ButtonTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTambah.Click
        Call tambah()
    End Sub

    Private Sub ButtonSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSimpan.Click
        Call bukaDB()
        CMD = New MySqlCommand("select nama_pembeli from pembeli where nama_pembeli = '" & TextBoxNamaPembeli.Text & "'", Koneksi)
        RD = CMD.ExecuteReader
        RD.Read()

        If ButtonTambah.Enabled = False Then
            If RD.HasRows Then
                MsgBox("Maaf, Data dengan Nama Pembeli tersebut telah ada", MsgBoxStyle.Exclamation, "Peringatan")
            Else
                If (MessageBox.Show("Simpan data pembeli baru?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = DialogResult.Yes Then
                    Call bukaDB()
                    simpan = "insert into pembeli (nama_pembeli, jk, no_telp, alamat) values ('" & TextBoxNamaPembeli.Text & "', '" & TextBoxJenisKelamin.Text & "', '" & TextBoxNoTelepon.Text & "', '" & TextBoxAlamat.Text & "')"

                    CMD = New MySqlCommand(simpan, Koneksi)

                    CMD.ExecuteNonQuery()

                    Call isiGrid()
                    Call batal()
                    TextBoxNamaPembeli.Focus()

                    MsgBox("Data pembeli sudah tersimpan.", MsgBoxStyle.Information, "Info")
                    RD.Close()
                End If
            End If
        End If
    End Sub

    Private Sub ButtonUbah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonUbah.Click
        bukaDB()
        Try
            Using conn As New MySqlConnection(Koneksi.ConnectionString)
                conn.Open()
                Dim command As New MySqlCommand("update pembeli set nama_pembeli=@nama_pembeli, jk=@jk, no_telp=@no_telp, alamat=@alamat where nama_pembeli=@nama_pembeli", conn)

                With command.Parameters
                    .AddWithValue("@nama_pembeli", TextBoxNamaPembeli.Text)
                    .AddWithValue("@jk", TextBoxJenisKelamin.Text)
                    .AddWithValue("@no_telp", TextBoxNoTelepon.Text)
                    .AddWithValue("@alamat", TextBoxAlamat.Text)
                End With

                command.ExecuteNonQuery()
                MessageBox.Show("Data pembeli sukses tersimpan", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Kesalahan dalam ubah data", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Call isiGrid()
        Call bersih()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBoxNamaPembeli.ReadOnly = True
        bukaDB()
        CMD = New MySqlCommand("select nama_pembeli, jk, no_telp, alamat from pembeli where nama_pembeli = '" & TextBoxNamaPembeli.Text & "'", Koneksi)

        Try
            Dim gridbaris As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            TextBoxNamaPembeli.Text = gridbaris.Cells(0).Value.ToString
            TextBoxJenisKelamin.Text = gridbaris.Cells(1).Value.ToString
            TextBoxNoTelepon.Text = gridbaris.Cells(2).Value.ToString
            TextBoxAlamat.Text = gridbaris.Cells(3).Value.ToString
        Catch ex As Exception
            MsgBox("Pilih data pembeli yang ada di dalam tabel", MsgBoxStyle.Information, "Info")
        End Try
    End Sub


    Private Sub ButtonHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHapus.Click
        If TextBoxNamaPembeli.Text = "" Then
            MessageBox.Show("Pilih dahulu data pembeli yang akan Anda hapus pada tabel di atas!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If TextBoxNamaPembeli.Text <> "" Then
                If MessageBox.Show("Apakah Anda yakin akan menghapus data pembeli ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Call bukaDB()
                    hapus = "delete from pembeli where nama_pembeli = '" & TextBoxNamaPembeli.Text & "'"
                    CMD = New MySqlCommand(hapus, Koneksi)

                    CMD.ExecuteNonQuery()
                    Call isiGrid()
                    Call bersih()
                    TextBoxNamaPembeli.Focus()
                    MessageBox.Show("Data pembeli berhasil dihapus", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End If
    End Sub

    Private Sub ButtonBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBatal.Click
        Call batal()
    End Sub
End Class

