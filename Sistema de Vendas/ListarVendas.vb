Imports MySql.Data.MySqlClient

Public Class ListarVendas
    Private Sub ListarVendas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        listarVendas()
    End Sub

    Sub listarVendas()
        Dim dt As New DataTable
        Dim da As New MySqlDataAdapter

        Try
            Abrir()
            da = New MySqlDataAdapter("SELECT * FROM vendidos", con)
            da.Fill(dt)
            dgVendas.DataSource = dt

            formataDG()
        Catch ex As Exception
            MessageBox.Show("Erro ao listar" + ex.Message)
            Fechar()
        End Try
    End Sub

    Sub formataDG()
        dgVendas.Columns(0).Visible = False
    End Sub
End Class