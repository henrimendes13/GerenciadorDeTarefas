using GerenciadorDeTarefasApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GerenciadorDeTarefasApi.DTOs;

public class TarefaCreateDTO
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(30, ErrorMessage = "O título não pode exceder 30 caracteres.")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
    public string Descricao { get; set; }

    public bool Finalizada { get; set; } = false;

    [Required(ErrorMessage = "A data de entrega é obrigatória.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [DateFormat(ErrorMessage = "Data em formato inválido. Utilize yyyy-MM-dd.")]
    [FutureDate(ErrorMessage = "A data de entrega nao pode ser uma data passada.")]
    public string DataParaEntrega { get; set; }
}

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string dateString)
        {
            if (DateTime.TryParse(dateString, out DateTime date))
            {
                return date.Date >= DateTime.Today;
            }
            return false;
        }

        return false;
    }
}

public class DateFormatAttribute : ValidationAttribute
{
    private const string _format = "yyyy-MM-dd";

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string dateString)
        {
            if (DateTime.TryParseExact(dateString, _format,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime _))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? $"A data deve estar no formato {_format}.");
        }

        return ValidationResult.Success;
    }
}
