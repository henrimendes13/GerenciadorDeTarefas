using GerenciadorDeTarefasApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefasApi.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }
    public DbSet<Tarefa> Tarefa { get; set; }  
}
