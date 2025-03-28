using Microsoft.Data.SqlClient;

namespace KanbanAPI.Data
{
    public class DatabaseConnection
    {
        // String de conexão: Atualize com o nome do seu servidor e banco de dados
        private readonly string _stringConexao = "Server=(localdb)\\MSSQLLocalDB;Database=KanbanDb;Trusted_Connection=True;";


        // Método para obter a conexão
        public SqlConnection GetConnection()
        {
            try
            {
                // Cria e retorna uma conexão
                var connection = new SqlConnection(_stringConexao);
                return connection;
            }
            catch (Exception ex)
            {
                // Exibe mensagem de erro
                Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
                throw;
            }
        }

        // Método de exemplo: Executar comandos no banco de dados
        public void ExecuteQuery(string query)
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open(); // Abre a conexão
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery(); // Executa o comando
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao executar consulta: " + ex.Message);
                    throw;
                }
            }
        }
    }
}