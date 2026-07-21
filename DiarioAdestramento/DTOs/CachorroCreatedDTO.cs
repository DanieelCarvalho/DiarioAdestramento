using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DiarioAdestramento.DTOs;

public class CachorroCreatedDTO
{
    //[JsonIgnore]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do cachorro é obrigatório.")]
    [MaxLength(10, ErrorMessage = "O nome do cachorro não pode ter mais de 10 caracteres.")]
    [MinLength(2, ErrorMessage = "O nome do cachorro deve ter pelo menos 2 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A idade do cachorro é obrigatória. Caso não saiba diga uma idade aproximada.")]
    [Range(1, 20, ErrorMessage = "A idade do cachorro deve estar entre 1 e 20 anos")]
    public int Idade { get; set; }

    private string _raca = "Sem Raça Definida";

    public string Raca
    {
        get => _raca;
        set => _raca = string.IsNullOrWhiteSpace(value) ? "Sem Raça Definida" : value;
    }

}
