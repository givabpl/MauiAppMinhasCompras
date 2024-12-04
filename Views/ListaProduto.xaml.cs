using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    // Lista observável que será vinculada à interface (ListView) para exibir os produtos.
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        // Vincula a lista observável ao ListView (lst_produtos) definido no XAML.
        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        try
        {
            // Limpa a lista observável para evitar duplicidade ao recarregar a página.
            lista.Clear();

            // Busca todos os produtos no banco de dados.
            List<Produto> tmp = await App.Db.GetAll();

            // Adiciona cada produto da lista retornada para a coleção observável.
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            // Exibe um alerta caso ocorra algum erro.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Navega para a página NovoProduto para adicionar um novo item.
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            // Exibe um alerta caso ocorra algum erro.
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            // Obtém o texto digitado na barra de pesquisa.
            string q = e.NewTextValue;

            // Limpa a lista atual.
            lista.Clear();

            // Realiza a busca no banco de dados com base no texto digitado.
            List<Produto> tmp = await App.Db.Search(q);

            // Adiciona os resultados encontrados na lista observável.
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            // Exibe um alerta caso ocorra algum erro.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        // Calcula o total dos produtos na lista somando a propriedade `Total`.
        double soma = lista.Sum(i => i.Total);

        // Exibe o valor total formatado como moeda em uma mensagem.
        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtém o item do menu que foi clicado.
            MenuItem selecionado = sender as MenuItem;

            // Recupera o produto associado ao item do menu clicado.
            Produto p = selecionado.BindingContext as Produto;

            // Solicita confirmação do usuário para remover o produto.
            bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if
