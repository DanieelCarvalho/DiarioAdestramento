using DiarioAdestramento.Enums;
using System.Text.Json.Serialization;

namespace DiarioAdestramento.Models;

public class RegistroClima
{
    public int Id { get; set; }
    public int SessaoTreinoId { get; set; }

    [JsonIgnore]
    public SessaoTreino? Sessao { get; set; }

    /// <summary>
    /// Indica o momento inicial e final do clima durante a sessão de treino.
    /// 0 = Inicio, 1 = Fim
    /// </summary>
    public MomentoClima? Momento { get; set; }
    public double TemperaturaCelsius { get; set; }

    public string? CondicaoTempo { get; set; }
    public double? Precipitacao { get; set; }

    public double? VelocidadeDeVento { get; set; }

}
