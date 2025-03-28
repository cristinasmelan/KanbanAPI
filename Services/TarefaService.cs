using KanbanAPI.Data;
using KanbanAPI.Models;

namespace KanbanAPI.Services
{
    public class TarefaService
    {
        private readonly TarefaRepository _repository;

        public TarefaService(TarefaRepository repository)
        {
            _repository = repository;
        }

        // Adicionar uma nova tarefa
        public void AdicionarTarefa(Tarefa tarefa)
        {
            // Validação de exemplo: Título não pode ser vazio
            if (string.IsNullOrWhiteSpace(tarefa.Titulo))
                throw new ArgumentException("O título da tarefa não pode ser vazio.");

            _repository.AddTarefa(tarefa);
        }

        // Obter todas as tarefas de um usuário
        public List<Tarefa> ObterTarefasDoUsuario(long userId)
        {
            // Validações ou filtros podem ser aplicados aqui
            return _repository.GetTarefasByUserId(userId);
        }

        // Atualizar uma tarefa existente
        public void AtualizarTarefa(Tarefa tarefa)
        {
            // Validação de exemplo: Status deve ser válido
            var statusPermitidos = new[] { "A Fazer", "Fazendo", "Finalizado" };
            if (!Array.Exists(statusPermitidos, s => s == tarefa.Status))
                throw new ArgumentException("O status da tarefa é inválido.");

            _repository.UpdateTarefa(tarefa);
        }

        // Excluir uma tarefa
        public void ExcluirTarefa(long tarefaId)
        {
            _repository.DeleteTarefa(tarefaId);
        }


    }
}