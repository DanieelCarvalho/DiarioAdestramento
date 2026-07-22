using DiarioAdestramento.Enums;

namespace DiarioAdestramento.DTOs;

public class RegistroClimaResumoDTO
{
    /// <summary> Indica o momento inicial e final do clima durante a sessão de treino. 0 = Inicio, 1 = Fim /// </summary>
    public MomentoClima? Momento { get; set; }
    public double TemperaturaCelsius { get; set; }
    public string? CondicaoTempo { get; set; }
}
