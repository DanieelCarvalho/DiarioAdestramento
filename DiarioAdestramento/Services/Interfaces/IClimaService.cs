using DiarioAdestramento.DTOs;
using DiarioAdestramento.Models;

namespace DiarioAdestramento.Services;

public interface IClimaService
{
    /// <summary>
    /// Busca o clima histórico para uma data/hora específica no passado.
    /// Retorna null se não for possível obter o dado (falha na API externa ou hora não encontrada).
    /// </summary>
    Task<ClimaResultadoDto?> ObterClimaHistoricoAsync(double latitude, double longitude, DateTime dataHora);
}