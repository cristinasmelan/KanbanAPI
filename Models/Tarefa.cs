using System.Text.Json.Serialization;

namespace KanbanAPI.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string SenhaHash { get; set; }
        public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }

    public class Tarefa
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public DateTime Data { get; set; }
        public long UserId { get; set; }

        [JsonIgnore]
        public Usuario? Usuario { get; set; }
    }

}