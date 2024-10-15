using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeTarefasApi.Models;

public class Tarefa
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(30, ErrorMessage = "O título não pode exceder 30 caracteres.")]
    public string Titulo { get; set; }
    
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
    public string Descricao { get; set; }
    public bool Finalizada { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    
    [Required(ErrorMessage = "A data de entrega é obrigatória.")]
    [DataType(DataType.Date, ErrorMessage = "Data em formato inválido. Utilize yyyy-mm-dd")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [FutureDate(ErrorMessage = "A data de entrega nao pode ser uma data passada.")]

    public DateTime DataParaEntrega { get; set; }
}

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime date)
        {
            return date >= DateTime.Today;
        }
        return false;
    }
}
