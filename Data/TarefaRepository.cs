using KanbanAPI.Models;
using Microsoft.Data.SqlClient;

namespace KanbanAPI.Data
{
    public class TarefaRepository
    {
        private readonly string _connectionString;
        // private readonly string _stringConexao = "Server=(localdb)\\MSSQLLocalDB;Database=KanbanDb;Trusted_Connection=True;";

        public TarefaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Criar uma nova tarefa
        public void AddTarefa(Tarefa tarefa)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Tarefas (Titulo, Descricao, Status, Data, UserId) VALUES (@Titulo, @Descricao, @Status, @Data, @UserId)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", tarefa.Titulo);
                    command.Parameters.AddWithValue("@Descricao", tarefa.Descricao);
                    command.Parameters.AddWithValue("@Status", tarefa.Status);
                    command.Parameters.AddWithValue("@Data", tarefa.Data);
                    command.Parameters.AddWithValue("@UserId", tarefa.UserId);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Buscar todas as tarefas de um usuário
        public List<Tarefa> GetTarefasByUserId(long userId)
        {
            var tarefas = new List<Tarefa>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                        SELECT 
                        t.Id AS TarefaId, t.Titulo, t.Descricao, t.Status, t.Data, t.UserId,
                        u.Id AS UsuarioId, u.Nome, u.Email, u.SenhaHash
                        FROM 
                        Tarefas t
                        INNER JOIN 
                        Usuarios u ON t.UserId = u.Id
                        WHERE 
                        t.UserId = @UserId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tarefas.Add(new Tarefa
                            {
                                Id = reader.GetInt64(reader.GetOrdinal("TarefaId")),
                                Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                Data = reader.GetDateTime(reader.GetOrdinal("Data")),
                                UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                                Usuario = new Usuario
                                {
                                    Id = reader.GetInt64(reader.GetOrdinal("UsuarioId")),
                                    Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    SenhaHash = reader.GetString(reader.GetOrdinal("SenhaHash"))

                                }
                            });
                        }
                    }
                }
            }

            return tarefas;
        }

        // Atualizar uma tarefa existente
        public void UpdateTarefa(Tarefa tarefa)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Tarefas SET Titulo = @Titulo, Descricao = @Descricao, Status = @Status WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titulo", tarefa.Titulo);
                    command.Parameters.AddWithValue("@Descricao", tarefa.Descricao);
                    command.Parameters.AddWithValue("@Status", tarefa.Status);
                    command.Parameters.AddWithValue("@Id", tarefa.Id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Excluir uma tarefa
        public void DeleteTarefa(long tarefaId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Tarefas WHERE Id = @Id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", tarefaId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}