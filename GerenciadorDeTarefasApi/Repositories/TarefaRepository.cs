using GerenciadorDeTarefasApi.Data;
using GerenciadorDeTarefasApi.Models;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefasApi.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly ApiDbContext _context;

    public TarefaRepository(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<Tarefa> CreateTarefaAsync(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();
        return tarefa;
    }

    public async Task<bool> DeleteTarefaAsync(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null)
        {
            return false;
        }

        _context.Tarefas.Remove(tarefa);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Tarefa?> GetTarefaByIdAsync(int id)
    {
        return await _context.Tarefas.FindAsync(id);
    }

    public async Task<IEnumerable<Tarefa>> GetTarefasAsync()
    {
        return await _context.Tarefas.ToListAsync();
    }

    public async Task<bool> NomeTarefaExistente(string titulo)
    {
        return await _context.Tarefas.AnyAsync(t => t.Titulo == titulo);
    }

    public async Task<bool> UpdateTarefaAsync(int id, Tarefa tarefa)
    {
        if (!await _context.Tarefas.AnyAsync(t => t.Id == id))
            return false;

        _context.Tarefas.Update(tarefa);
        return await _context.SaveChangesAsync() > 0;
    }
}