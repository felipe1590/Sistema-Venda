Imports MySql.Data.MySqlClient

Public Class Produto
    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        If txtProduto.Text = "" Then
            MessageBox.Show("Preenxa o nome do produto")
            txtProduto.Focus()
        ElseIf txtPreco.Text = "" Then
            MessageBox.Show("Preenxa o preço do produto")
            txtPreco.Focus()
        ElseIf txtCodigo.Text = "" Then
            MessageBox.Show("Preenxa o estoque do produto")
            txtCodigo.Focus()
        Else
            Dim cmd As MySqlCommand
            Dim sql As String
            Try
                Abrir()
                sql = "INSERT INTO produtos (produto, codigo, preco) VALUES ('" & txtProduto.Text & "', '" & txtCodigo.Text & "','" & txtPreco.Text & "')"
                cmd = New MySqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Produto Cadastrado com Sucesso!")
            Catch ex As Exception
                MessageBox.Show("Erro ao Salvar" + ex.Message)
                Fechar()
            End Try
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub
End Class