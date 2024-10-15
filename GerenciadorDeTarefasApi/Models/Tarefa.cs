using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeTarefasApi.Models;

public class Tarefa
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(100, ErrorMessage = "O título não pode exceder 30 caracteres.")]
    public string Titulo { get; set; }
    
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
    public string Descricao { get; set; }
    public bool Finalizada { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    
    [Required(ErrorMessage = "A data de entrega é obrigatória.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DataParaEntrega { get; set; }
}
