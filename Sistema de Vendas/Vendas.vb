Imports MySql.Data.MySqlClient

Public Class Vendas

    Dim valor_total As Decimal
    Dim total_venda As Decimal

    Sub limparCampos()
        txtCodigo.Text = ""
        txtProduto.Text = ""
        txtPreco.Text = ""
        txtQuantidade.Text = ""
    End Sub

    Sub codigoVenda()
        Dim cmd As MySqlCommand
        Dim reader As MySqlDataReader
        Dim sql As String
        Dim codigo As Integer

        Try
            Abrir()
            sql = "SELECT venda FROM vendidos ORDER BY venda DESC"
            cmd = New MySqlCommand(sql, con)
            reader = cmd.ExecuteReader

            If reader.Read = True Then
                codigo = reader("venda").ToString
                reader.Close()
            End If

            txtCodigoVenda.Text = codigo + 1
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Erro ao listar " + ex.Message)
            Fechar()
        End Try
    End Sub

    Sub buscaProduto()
        Dim cmd As MySqlCommand
        Dim reader As MySqlDataReader
        Dim sql As String

        Try
            Abrir()
            sql = "SELECT * FROM produtos WHERE codigo = '" & txtCodigo.Text & "'"
            cmd = New MySqlCommand(sql, con)
            reader = cmd.ExecuteReader

            If reader.Read = True Then
                txtProduto.Text = reader("produto").ToString
                txtPreco.Text = reader("preco").ToString

                txtQuantidade.Enabled = True
                txtQuantidade.Focus()
                reader.Close()
            Else
                MessageBox.Show("Produto não cadastrado")
                limparCampos()
                reader.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Erro ao executar" + ex.Message)
            Fechar()
        End Try
    End Sub

    Private Sub Vendas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtProduto.Enabled = False
        txtPreco.Enabled = False
        txtQuantidade.Enabled = False

        codigoVenda()
        limparCampos()

        btnCancelar.Enabled = False
    End Sub

    Private Sub btnInserir_Click(sender As Object, e As EventArgs) Handles btnInserir.Click
        If txtCodigo.Text <> "" And txtProduto.Text <> "" And txtPreco.Text <> "" And txtQuantidade.Text <> "" Then

            valor_total = CDec(txtQuantidade.Text) * CDec(txtPreco.Text)

            Dim cmd As MySqlCommand
            Dim sql As String

            Try
                Abrir()
                sql = "INSERT INTO vendidos (venda, item, quantidade, valor) VALUES ('" & txtCodigoVenda.Text & "', '" & txtProduto.Text & "', '" & txtQuantidade.Text & "', '" & valor_total & "')"
                cmd = New MySqlCommand(sql, con)
                cmd.ExecuteNonQuery()

                dgItens.Rows.Add(txtCodigoVenda.Text, txtProduto.Text, txtQuantidade.Text, valor_total)

                codigoVenda()
                limparCampos()

                total_venda += valor_total
                txtTotal.Text = FormatCurrency(total_venda)
                txtCodigo.Focus()
            Catch ex As Exception
                MessageBox.Show("Erro ao inserir " + ex.Message)
                Fechar()
            End Try

        End If
    End Sub

    Private Sub txtCodigo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCodigo.KeyDown
        If e.KeyCode = Keys.Enter Then
            buscaProduto()
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Dim cmd As MySqlCommand
        Dim sql As String

        Try
            Abrir()
            sql = "DELETE FROM vendidos WHERE venda = '" & dgItens.CurrentRow.Cells(0).Value & "'"
            cmd = New MySqlCommand(sql, con)
            cmd.ExecuteNonQuery()

            limparCampos()
            total_venda -= dgItens.CurrentRow.Cells(3).Value
            txtTotal.Text = FormatCurrency(total_venda)
            txtCodigo.Focus()

            dgItens.Rows.Remove(dgItens.CurrentRow)
        Catch ex As Exception
            MessageBox.Show("Erro ao inserir " + ex.Message)
            Fechar()
        End Try

    End Sub

    Private Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        If dgItens.RowCount <> 0 Then
            dgItens.Rows.Clear()

            total_venda = 0
            txtTotal.Text = "R$ 0,00"

            limparCampos()
            MessageBox.Show("Venda Finalizada com Sucesso!")
        Else
            MessageBox.Show("Sem produtos Adicinados")
        End If
    End Sub

    Private Sub dgItens_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgItens.CellClick
        btnCancelar.Enabled = True
    End Sub
End Class