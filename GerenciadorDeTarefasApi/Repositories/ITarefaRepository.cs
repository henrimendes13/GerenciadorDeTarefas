using GerenciadorDeTarefasApi.Models;

namespace GerenciadorDeTarefasApi.Repositories
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> GetTarefasAsync();
        Task<Tarefa?> GetTarefaByIdAsync(int id);
        Task<Tarefa> CreateTarefaAsync(Tarefa tarefa);
        Task<bool> UpdateTarefaAsync(int id, Tarefa tarefa); 
        Task<bool> DeleteTarefaAsync(int id);
        Task<bool> NomeTarefaExistente(string titulo);

    }
}
