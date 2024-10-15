using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeTarefasApi.Data;
using GerenciadorDeTarefasApi.Models;

namespace GerenciadorDeTarefasApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarefaController : ControllerBase
{
    private readonly ApiDbContext _context;

    public TarefaController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
    {
        return await _context.Tarefa.ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Tarefa>> GetTarefa(int id)
    {
        var tarefa = await _context.Tarefa.FindAsync(id);

        if (tarefa == null)
        {
            return NotFound();
        }

        return tarefa;
    }


    [HttpPost]
    public async Task<ActionResult<Tarefa>> PostTarefa([FromBody] Tarefa tarefa)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        tarefa.DataCriacao = DateTime.Now;

        _context.Tarefa.Add(tarefa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTarefa), new { id = tarefa.Id }, tarefa);
    }


    [HttpPut("{id}")]
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
    public async Task<IActionResult> DeleteTarefa(int id)
    {
        var tarefa = await _context.Tarefa.FindAsync(id);
        if (tarefa == null)
        {
            return NotFound();
        }

        _context.Tarefa.Remove(tarefa);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TarefaExists(int id)
    {
        return _context.Tarefa.Any(e => e.Id == id);
    }
}
