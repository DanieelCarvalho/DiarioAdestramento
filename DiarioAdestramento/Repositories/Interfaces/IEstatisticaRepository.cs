using DiarioAdestramento.DTOs;
using DiarioAdestramento.DTOs.Estatisticas;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface IEstatisticaRepository
{
    Task<IEnumerable<DesempenhoPorClimaDTO>> GetDesempenhoPorClimaAsync(int cachorroId, string adestradorId);
    Task<IEnumerable<DesempenhoPorLocalDTO>> GetDesempenhoPorLocalAsync(int cachorroId, string adestradorId);
    Task<IEnumerable<EvolucaoComandoDTO>> GetEvolucaoPorComandoAsync(int cachorroId, string comando, string adestradorId);
}
