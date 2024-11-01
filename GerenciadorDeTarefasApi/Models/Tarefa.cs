using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeTarefasApi.Models;

public class Tarefa
{
    [Key]
    public int Id { get; set; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public bool Finalizada { get; set; } = false;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime DataParaEntrega { get; set; }
}