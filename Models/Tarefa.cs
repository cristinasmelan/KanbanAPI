namespace KanbanAPI.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Status { get; set; } = "A Fazer"; // Exemplo de valor padrão
        public string UserId { get; set; } = string.Empty;
    }
}