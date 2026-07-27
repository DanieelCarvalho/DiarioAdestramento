using DiarioAdestramento.DTOs;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface IEstatisticaRepository
{
    Task<IEnumerable<DesempenhoPorClimaDTO>> GetDesempenhoPorClimaAsync(int cachorroId);
    Task<IEnumerable<EvolucaoComandoDTO>> GetEvolucaoPorComandoAsync(int cachorroId, string comando);
}
