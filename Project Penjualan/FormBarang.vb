Imports MySql.Data.MySqlClient

Public Class FormBarang

    Public strSQL As String
    Public DB As MySqlConnection
    Public CMD As MySqlCommand
    Public ADP As MySqlDataAdapter
    Public DR As MySqlDataReader
    Public DS As New DataSet

    Public simpan, hapus As String

    Sub tambah()
        DataGridView1.Enabled = False
        TextBoxNamaBarang.ReadOnly = False

        TextBoxNamaBarang.Clear()
        TextBoxHarga.Clear()
        TextBoxStok.Clear()
        TextBoxNamaBarang.Enabled = True
        TextBoxHarga.Enabled = True
        TextBoxStok.Enabled = True
        TextBoxNamaBarang.Focus()

        ButtonTambah.Enabled = False
        ButtonHapus.Enabled = False
        ButtonUbah.Enabled = True
        ButtonSimpan.Enabled = True
        ButtonBatal.Enabled = True
    End Sub

    Sub ubah()
        TextBoxNamaBarang.Enabled = True
        TextBoxHarga.Enabled = True
        TextBoxStok.Enabled = True
        TextBoxNamaBarang.Focus()

        ButtonTambah.Enabled = True
        ButtonHapus.Enabled = True
        ButtonUbah.Enabled = True
        ButtonSimpan.Enabled = False
        ButtonBatal.Enabled = True
    End Sub

    Sub batal()
        DataGridView1.Enabled = True
        TextBoxNamaBarang.Clear()
        TextBoxHarga.Clear()
        TextBoxStok.Clear()

        TextBoxNamaBarang.Enabled = True
        TextBoxHarga.Enabled = True
        TextBoxStok.Enabled = True
        TextBoxNamaBarang.Focus()

        ButtonTambah.Enabled = True
        ButtonHapus.Enabled = True
        ButtonUbah.Enabled = True
        ButtonSimpan.Enabled = False
        ButtonBatal.Enabled = True
    End Sub

    Sub bersih()
        TextBoxNamaBarang.Clear()
        TextBoxHarga.Clear()
        TextBoxStok.Clear()
        TextBoxNamaBarang.Focus()
    End Sub

    Sub isiGrid()
        ModuleKoneksi.bukaDB()
        DA = New MySqlDataAdapter("select nama_barang, harga, stok from barang", Koneksi)

        DS = New DataSet
        DA.Fill(DS, "barang")

        DataGridView1.DataSource = DS.Tables("barang")
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
        CMD = New MySqlCommand("select nama_barang from barang where nama_barang = '" & TextBoxNamaBarang.Text & "'", Koneksi)
        RD = CMD.ExecuteReader
        RD.Read()

        If ButtonTambah.Enabled = False Then
            If RD.HasRows Then
                MsgBox("Maaf, Data dengan Kode Barang tersebut telah ada", MsgBoxStyle.Exclamation, "Peringatan")
            Else
                If (MessageBox.Show("Simpan data barang baru?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = DialogResult.Yes Then
                    Call bukaDB()
                    simpan = "insert into barang (nama_barang, harga, stok) values ('" & TextBoxNamaBarang.Text & "', '" & TextBoxHarga.Text & "', '" & TextBoxStok.Text & "')"

                    CMD = New MySqlCommand(simpan, Koneksi)

                    CMD.ExecuteNonQuery()

                    Call isiGrid()
                    Call batal()
                    TextBoxNamaBarang.Focus()

                    MsgBox("Data barang sudah tersimpan.", MsgBoxStyle.Information, "Info")
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
                Dim command As New MySqlCommand("update barang set nama_barang=@nama_barang, harga=@harga, stok=@stok where nama_barang=@nama_barang", conn)

                With command.Parameters
                    .AddWithValue("@nama_barang", TextBoxNamaBarang.Text)
                    .AddWithValue("@harga", TextBoxHarga.Text)
                    .AddWithValue("@stok", TextBoxStok.Text)
                End With

                command.ExecuteNonQuery()
                MessageBox.Show("Data barang sukses tersimpan", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Kesalahan dalam ubah data", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Call isiGrid()
        Call bersih()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBoxNamaBarang.ReadOnly = True
        bukaDB()
        CMD = New MySqlCommand("select nama_barang, harga, stok from barang where nama_barang = '" & TextBoxNamaBarang.Text & "'", Koneksi)

        Try
            Dim gridbaris As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            TextBoxNamaBarang.Text = gridbaris.Cells(0).Value.ToString
            TextBoxHarga.Text = gridbaris.Cells(1).Value.ToString
            TextBoxStok.Text = gridbaris.Cells(2).Value.ToString
        Catch ex As Exception
            MsgBox("Pilih data barang yang ada di dalam tabel", MsgBoxStyle.Information, "Info")
        End Try
    End Sub


    Private Sub ButtonHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonHapus.Click
        If TextBoxNamaBarang.Text = "" Then
            MessageBox.Show("Pilih dahulu data barang yang akan Anda hapus pada tabel di atas!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If TextBoxNamaBarang.Text <> "" Then
                If MessageBox.Show("Apakah Anda yakin akan menghapus data barang ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Call bukaDB()
                    hapus = "delete from barang where nama_barang = '" & TextBoxNamaBarang.Text & "'"
                    CMD = New MySqlCommand(hapus, Koneksi)

                    CMD.ExecuteNonQuery()
                    Call isiGrid()
                    Call bersih()
                    TextBoxNamaBarang.Focus()
                    MessageBox.Show("Data barang berhasil dihapus", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        End If
    End Sub

    Private Sub ButtonBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBatal.Click
        Call batal()
    End Sub
End Class

