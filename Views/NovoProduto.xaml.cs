using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    // Construtor da página NovoProduto.
    public NovoProduto()
    {
        InitializeComponent();
        // Inicializa os componentes da página (vincula os controles XAML ao código-behind).
    }

    // Evento chamado quando o botão "Salvar" da toolbar é clicado.
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Cria uma nova instância de Produto e preenche as propriedades com os dados dos campos de entrada.
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,  // Obtém o texto do campo de descrição.
                Quantidade = Convert.ToDouble(txt_quantidade.Text),  // Converte o texto da quantidade para número.
                Preco = Convert.ToDouble(txt_preco.Text)  // Converte o texto do preço para número.
            };

            // Chama o método Insert no banco de dados para adicionar o produto.
            await App.Db.Insert(p);

            // Exibe um alerta informando que o registro foi inserido com sucesso.
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
        }
        catch (Exception ex)
        {
            // Se ocorrer um erro, exibe um alerta com a mensagem do erro.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
