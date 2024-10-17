using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeTarefasApi.Models;

public class Tarefa
{
    [Key]
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public bool Finalizada { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime DataParaEntrega { get; set; }
}