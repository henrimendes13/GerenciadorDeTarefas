using AutoMapper;
using GerenciadorDeTarefasApi.Data;
using GerenciadorDeTarefasApi.DTOs;
using GerenciadorDeTarefasApi.Models;
using GerenciadorDeTarefasApi.Repositories;
using GerenciadorDeTarefasApi.Services.Exceptions;
using Microsoft.EntityFrameworkCore;



namespace GerenciadorDeTarefasApi.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IMapper _mapper;

    public TarefaService(ITarefaRepository tarefaRepository, IMapper mapper)
    {
        _tarefaRepository = tarefaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TarefaReadDTO>> GetTarefasAsync()
    {
        var tarefas = await _tarefaRepository.GetTarefasAsync();
        return _mapper.Map<IEnumerable<TarefaReadDTO>>(tarefas);
    }

    public async Task<TarefaReadDTO> GetTarefaByIdAsync(int id)
    {
        var tarefa = await _tarefaRepository.GetTarefaByIdAsync(id);

        if (tarefa == null)
        {
            throw new TarefaNotFoundException("Tarefa ID not found");
        }

        return _mapper.Map<TarefaReadDTO>(tarefa);
    }

    public async Task<TarefaReadDTO> CreateTarefaAsync(TarefaCreateDTO tarefaCreateDto)
    {
        var tarefa = _mapper.Map<Tarefa>(tarefaCreateDto);
       
        var nomeTarefaJaExiste = await NomeTarefaExistente(tarefaCreateDto.Titulo);
        if (nomeTarefaJaExiste)
        {
            throw new TituloJaExistenteException("O Nome da tarefa já existe em outra tarefa.");
        }

        await _tarefaRepository.CreateTarefaAsync(tarefa);

        return _mapper.Map<TarefaReadDTO>(tarefa);
    }

    public async Task<bool> UpdateTarefaAsync(int id, TarefaUpdateDTO tarefaUpdateDto)
    {
        var tarefa = await _tarefaRepository.GetTarefaByIdAsync(id);

        if (tarefa == null)
        {
            return false;
        }

        _mapper.Map(tarefaUpdateDto, tarefa);

        return await _tarefaRepository.UpdateTarefaAsync(id, tarefa);

    }


    public async Task<bool> DeleteTarefaAsync(int id)
    {
        var tarefa = await _tarefaRepository.GetTarefaByIdAsync(id);

        if (tarefa == null)
        {
            return false;
        }

        await _tarefaRepository.DeleteTarefaAsync(id);

        return true;
    }

    public async Task<bool> NomeTarefaExistente(string titulo)
    {
        return await _tarefaRepository.NomeTarefaExistente(titulo);
    }
}