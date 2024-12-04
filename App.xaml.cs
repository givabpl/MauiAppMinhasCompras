using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        // Definição de um campo estático para o helper de banco de dados.
        static SQLiteDatabaseHelper _db;

        // Propriedade Db (base de dados) que retorna uma instância de SQLiteDatabaseHelper.
        public static SQLiteDatabaseHelper Db
        {
            get
            {
                // Verifica se o campo _db ainda não foi inicializado.
                if (_db == null)
                {
                    // Se não foi inicializado, cria o caminho para o banco de dados.
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),  // Obtém o caminho da pasta de dados do aplicativo.
                        "banco_sqlite_compras.db3");  // Define o nome do arquivo do banco de dados.

                    // Cria uma nova instância de SQLiteDatabaseHelper, passando o caminho do banco de dados.
                    _db = new SQLiteDatabaseHelper(path);
                }

                // Retorna a instância do helper de banco de dados.
                return _db;
            }
        }

        // Construtor do aplicativo.
        public App()
        {
            InitializeComponent();  // Inicializa os componentes da aplicação (necessário em um projeto MAUI).

            // Define a página principal do aplicativo.
            // MainPage pode ser um Shell (para navegação baseada em tabs, flyout, etc.) ou um NavigationPage.
            // Aqui está configurado para abrir a página ListaProduto como a página inicial.
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
