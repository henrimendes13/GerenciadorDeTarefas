using AutoMapper;
using GerenciadorDeTarefasApi.Data;
using GerenciadorDeTarefasApi.DTOs;
using GerenciadorDeTarefasApi.Models;
using Microsoft.EntityFrameworkCore;


namespace GerenciadorDeTarefasApi.Services;

public class TarefaService : ITarefaService
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public TarefaService(ApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TarefaReadDTO>> GetTarefasAsync()
    {
        var tarefas = await _context.Tarefas.ToListAsync();
        return _mapper.Map<IEnumerable<TarefaReadDTO>>(tarefas);
    }

    public async Task<TarefaReadDTO> GetTarefaByIdAsync(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);

        if (tarefa == null)
        {
            return null;
        }

        return _mapper.Map<TarefaReadDTO>(tarefa);
    }

    public async Task<TarefaReadDTO> CreateTarefaAsync(TarefaCreateDTO tarefaCreateDto)
    {
        var tarefa = _mapper.Map<Tarefa>(tarefaCreateDto);
        tarefa.DataCriacao = DateTime.Now;

        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        return _mapper.Map<TarefaReadDTO>(tarefa);
    }

    public async Task<bool> UpdateTarefaAsync(int id, TarefaUpdateDTO tarefaUpdateDto)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);

        if (tarefa == null)
        {
            return false;
        }

        _mapper.Map(tarefaUpdateDto, tarefa);
        _context.Entry(tarefa).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Tarefas.Any(e => e.Id == id))
            {
                return false;
            }
            else
            {
                throw;
            }
        }

        return true;
    }

    public async Task<bool> DeleteTarefaAsync(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);

        if (tarefa == null)
        {
            return false;
        }

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();

        return true;
    }
    private bool TarefaExists(int id)
    {
        return _context.Tarefas.Any(e => e.Id == id);
    }
}
