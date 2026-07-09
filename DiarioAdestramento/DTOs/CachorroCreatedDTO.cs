using System.Text.Json.Serialization;

namespace DiarioAdestramento.DTOs;

public class CachorroCreatedDTO
{
    //[JsonIgnore]
    public int Id { get; set; }

    public int Idade { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Raca { get; set; }
}
