using System.Text.Json.Serialization;

namespace DiarioAdestramento.Models;

public class Cachorro
{
    public int Id { get; set; }

    public int Idade { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? Raca { get; set; }

    public string AdestradorId { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<SessaoTreino>? Sessao { get; set; }

}
