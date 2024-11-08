using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeTarefasApi.Data;
using GerenciadorDeTarefasApi.Models;
using AutoMapper;
using GerenciadorDeTarefasApi.DTOs;
using GerenciadorDeTarefasApi.Services;
using GerenciadorDeTarefasApi.Services.Exceptions;

namespace GerenciadorDeTarefasApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarefaController : ControllerBase
{
    private readonly ITarefaService _tarefaService;

    public TarefaController(ITarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TarefaReadDTO>>> GetTarefas()
    {
        var tarefas = await _tarefaService.GetTarefasAsync();
        return Ok(tarefas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<TarefaReadDTO>> GetTarefa(int id)
    {
        try
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);
            return Ok(tarefa);
        }
        catch (TarefaNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<TarefaReadDTO>> PostTarefa(TarefaCreateDTO tarefaCreateDto)
    {
        try
        {
            var tarefaReadDto = await _tarefaService.CreateTarefaAsync(tarefaCreateDto);
            return CreatedAtAction(nameof(GetTarefa), new { id = tarefaReadDto.Id }, tarefaReadDto);
        }
        catch (TituloJaExistenteException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PutTarefa(int id, TarefaUpdateDTO tarefaUpdateDto)
    {
        var result = await _tarefaService.UpdateTarefaAsync(id, tarefaUpdateDto);
        if (!result)
        {
            return NotFound();
        }
        return Ok("Tarefa atualizada.");
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteTarefa(int id)
    {
        var result = await _tarefaService.DeleteTarefaAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok("Tarefa deletada.");
    }

}
