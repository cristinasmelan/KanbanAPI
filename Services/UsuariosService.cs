using KanbanAPI.Data;
using KanbanAPI.Models;

namespace KanbanAPI.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;

        public UsuarioService(UsuarioRepository repository)
        {
            _repository = repository;
        }


        // Obter todas as usuarios de um usuário
        public List<Usuario> ObterUsuarios()
        {
            // Validações ou filtros podem ser aplicados aqui
            return _repository.GetUsuarios();
        }

    }
}