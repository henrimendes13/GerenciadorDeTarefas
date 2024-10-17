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

namespace GerenciadorDeTarefasApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarefaController : ControllerBase
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public TarefaController(ApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<IEnumerable<TarefaReadDTO>>> GetTarefas()
    {
        if (_context.Tarefas  == null)
        {
            return NotFound();
        }

        var tarefas = await _context.Tarefas.ToListAsync();
        var tarefaDtos = _mapper.Map<List<TarefaReadDTO>>(tarefas);
        return Ok(tarefaDtos);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<TarefaReadDTO>> GetTarefa(int id)
    {
        if (_context.Tarefas == null)
        {
            return NotFound();
        }

        var tarefa = await _context.Tarefas.FindAsync(id);

        if (tarefa == null)
        {
            return NotFound();
        }

        return _mapper.Map<TarefaReadDTO>(tarefa);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<TarefaReadDTO>> PostTarefa([FromBody] TarefaCreateDTO tarefaCreateDTO)
    {
        if (_context.Tarefas == null) 
        {
            return Problem("Erro ao criar uma tarefa, contate o suporte!");
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var tarefa = _mapper.Map<Tarefa>(tarefaCreateDTO);
        tarefa.DataCriacao = DateTime.Now;

        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        var tarefaReadDto = _mapper.Map<TarefaReadDTO>(tarefa);
        return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefaReadDto);
    }


    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutTarefa(int id, Tarefa tarefa)
    {
        if (id != tarefa.Id)
        {
            return BadRequest();
        }

        _context.Entry(tarefa).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TarefaExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteTarefa(int id)
    {
        if (_context.Tarefas == null)
        {
            return NotFound();
        }

        var tarefa = await _context.Tarefas.FindAsync(id);

        if (tarefa == null)
        {
            return NotFound();
        }

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TarefaExists(int id)
    {
        return _context.Tarefas.Any(e => e.Id == id);
    }
}
