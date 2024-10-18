using GerenciadorDeTarefasApi.DTOs;

namespace GerenciadorDeTarefasApi.Services;

public interface ITarefaService
{
    Task<IEnumerable<TarefaReadDTO>> GetTarefasAsync();
    Task<TarefaReadDTO> GetTarefaByIdAsync(int id);
    Task<TarefaReadDTO> CreateTarefaAsync(TarefaCreateDTO tarefaCreateDto);
    Task<bool> UpdateTarefaAsync(int id, TarefaUpdateDTO tarefaUpdateDto);
    Task<bool> DeleteTarefaAsync(int id);
}
