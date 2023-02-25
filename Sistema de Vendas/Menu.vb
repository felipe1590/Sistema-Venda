Public Class Menu
    Private Sub ProdutosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProdutosToolStripMenuItem.Click
        Produto.ShowDialog()
    End Sub

    Private Sub RegistrarVendaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegistrarVendaToolStripMenuItem.Click
        Vendas.ShowDialog()
    End Sub

    Private Sub ListarDeVendasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListarDeVendasToolStripMenuItem.Click
        ListarVendas.ShowDialog()
    End Sub

    Private Sub SairToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SairToolStripMenuItem.Click
        Dim result As DialogResult = MessageBox.Show("Tem Certeza que Deseja sair?", "Sair do Programa", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
End Class
