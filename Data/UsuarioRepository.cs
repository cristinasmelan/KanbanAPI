using KanbanAPI.Models;
using Microsoft.Data.SqlClient;

namespace KanbanAPI.Data
{
    public class UsuarioRepository
    {
        private readonly string _connectionString;
        
        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Usuario> GetUsuarios()
        {
            var usuarios = new List<Usuario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @" SELECT Id, Nome, Email, SenhaHash FROM Usuarios";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario
                            {
                                Id = reader.GetInt64(reader.GetOrdinal("Id")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                SenhaHash = reader.GetString(reader.GetOrdinal("SenhaHash"))

                            });
                        }
                    }
                }
            }

            return usuarios;
        }

    }
}