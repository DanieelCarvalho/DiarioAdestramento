using DiarioAdestramento.Enums;
using System.Text.Json.Serialization;

namespace DiarioAdestramento.Models;

public class RegistroClima
{
    public int Id { get; set; }
    public int SessaoTreinoId { get; set; }

    [JsonIgnore]
    public SessaoTreino? Sessao { get; set; }

    public MomentoClima? Momento { get; set; }
    public double TemperaturaCelsius { get; set; }

    public string? CondicaoTempo { get; set; }
    public double? Precipitacao { get; set; }

    public double? VelocidadeDeVento { get; set; }

}
