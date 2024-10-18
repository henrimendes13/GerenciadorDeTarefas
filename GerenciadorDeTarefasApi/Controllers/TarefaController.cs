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
        var tarefa = await _tarefaService.GetTarefaByIdAsync(id);

        if (tarefa == null)
        {
            return NotFound();
        }

        return Ok(tarefa);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<TarefaReadDTO>> PostTarefa(TarefaCreateDTO tarefaCreateDto)
    {
        var tarefaReadDto = await _tarefaService.CreateTarefaAsync(tarefaCreateDto);
        return CreatedAtAction(nameof(GetTarefa), new { id = tarefaReadDto.Id }, tarefaReadDto);
    }


    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutTarefa(int id, TarefaUpdateDTO tarefaUpdateDto)
    {
        var result = await _tarefaService.UpdateTarefaAsync(id, tarefaUpdateDto);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteTarefa(int id)
    {
        var result = await _tarefaService.DeleteTarefaAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
        
}
