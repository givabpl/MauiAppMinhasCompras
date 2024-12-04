using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
    // Construtor da página.
    public EditarProduto()
    {
        // Inicializa os componentes da interface definidos no arquivo XAML associado.
        InitializeComponent();
    }

    // Método chamado quando o botão "Salvar" na Toolbar é clicado.
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtém o objeto Produto que está sendo usado como BindingContext da página.
            Produto produto_anexado = BindingContext as Produto;

            // Cria um novo objeto Produto com os dados preenchidos nos campos de texto.
            Produto p = new Produto
            {
                Id = produto_anexado.Id, // Mantém o mesmo ID, para atualizar o registro existente.
                Descricao = txt_descricao.Text, // Obtém o texto digitado no campo de descrição.
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte o texto da quantidade para double.
                Preco = Convert.ToDouble(txt_preco.Text) // Converte o texto do preço para double.
            };

            // Chama o método `Update` no banco de dados para salvar as alterações no produto.
            await App.Db.Update(p);

            // Exibe uma mensagem de sucesso para o usuário.
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");

            // Retorna para a página anterior na pilha de navegação.
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Exibe uma mensagem de erro caso ocorra uma exceção.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
