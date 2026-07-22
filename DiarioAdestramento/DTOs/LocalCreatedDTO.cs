using DiarioAdestramento.Enums;
using DiarioAdestramento.Models;
using System.ComponentModel.DataAnnotations;

namespace DiarioAdestramento.DTOs;

public class LocalCreatedDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Latitude é obrigatório.")]
    public double Latitude { get; set; }

    [Required(ErrorMessage = "O campo Longitude é obrigatório.")]
    public double Longitude { get; set; }

    [Required(ErrorMessage = "O campo TipoDoLocal é obrigatório.")]
    /// <summary>
    /// Indica se o local de treino é aberto ou fechado. 0 = Aberto, 1 = Fechado
    /// </summary>
    public TipoDoLocal? TipoDoLocal { get; set; }
    public string? Obs { get; set; }

}
