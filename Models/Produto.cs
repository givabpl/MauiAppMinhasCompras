using SQLite;

namespace MauiAppMinhasCompras.Models
{
    // Define a classe Produto que representa um item no banco de dados.
    public class Produto
    {
        // Campo privado para armazenar a descrição do produto.
        string _descricao;

        // Define o campo "Id" como chave primária e auto-incrementável no banco de dados SQLite.
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // Propriedade para a descrição do produto.
        public string Descricao
        {
            get => _descricao; // Retorna o valor armazenado em _descricao.
            set
            {
                // Valida se o valor fornecido é nulo.
                if (value == null)
                {
                    // Lança uma exceção se a descrição não for preenchida.
                    throw new Exception("Por favor, preencha a descrição");
                }

                // Caso seja válido, armazena o valor em _descricao.
                _descricao = value;
            }
        }

        // Quantidade do produto. Utiliza autoimplementação, ou seja, o C# gerencia o campo interno automaticamente.
        public double Quantidade { get; set; }

        // Preço unitário do produto. Também é uma propriedade autoimplementada.
        public double Preco { get; set; }

        // Propriedade somente leitura que calcula o total com base na quantidade e no preço.
        public double Total { get => Quantidade * Preco; }
    }
}
