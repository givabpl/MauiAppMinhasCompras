using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    // Classe responsável por gerenciar as operações no banco de dados SQLite.
    public class SQLiteDatabaseHelper
    {
        // Conexão assíncrona com o banco de dados SQLite.
        readonly SQLiteAsyncConnection _conn;

        // Construtor da classe que inicializa a conexão e cria a tabela "Produto" se ainda não existir.
        public SQLiteDatabaseHelper(string path)
        {
            // Inicializa a conexão com o caminho do banco de dados recebido como parâmetro.
            _conn = new SQLiteAsyncConnection(path);

            // Garante que a tabela "Produto" exista. A execução do método é bloqueante (usa Wait).
            _conn.CreateTableAsync<Produto>().Wait();
        }

        // Método para inserir um novo produto no banco de dados.
        public Task<int> Insert(Produto p)
        {
            // Usa o método InsertAsync para adicionar o objeto "Produto" à tabela.
            return _conn.InsertAsync(p);
        }

        // Método para atualizar um produto no banco de dados.
        public Task<List<Produto>> Update(Produto p)
        {
            // SQL que define a operação de atualização, substituindo os valores pelos parâmetros.
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            // Executa o comando SQL passando os valores do objeto "Produto".
            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }

        // Método para deletar um produto pelo ID.
        public Task<int> Delete(int id)
        {
            // Busca na tabela "Produto" e remove o item que possui o ID correspondente.
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        // Método para obter todos os produtos cadastrados no banco.
        public Task<List<Produto>> GetAll()
        {
            // Converte a tabela "Produto" para uma lista de objetos.
            return _conn.Table<Produto>().ToListAsync();
        }

        // Método para pesquisar produtos com base em uma string de consulta.
        public Task<List<Produto>> Search(string q)
        {
            // SQL que realiza a busca no campo "descricao" utilizando LIKE para encontrar resultados parciais.
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";

            // Executa o comando SQL e retorna os produtos encontrados.
            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
