using DiarioAdestramento.Dtos;
using DiarioAdestramento.DTOs;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Services.Interfaces;

public interface ISessaoTreinoService
{
    Task<(IEnumerable<SessaoTreinoResponseDTO> items, PaginationMetadata metadata)> GetAllSessoesAsync(
        SessoesParameters parametros, string adestradorId);

    Task<SessaoTreinoResponseDTO?> GetSessaoByIdAsync(int id, string adestradorId);

    Task<(CachorroComSessoesResponseDTO? cachorro, PaginationMetadata? metadata)> GetSessoesByCachorroIdAsync(
        int cachorroId, SessoesParameters parametros, string adestradorId);

    Task<(SessaoTreinoResponseDTO? sessao, string? erro)> CreateSessaoAsync(CriarSessaoTreinoDto dto, string adestradorId);

    Task<SessaoTreinoResponseDTO?> UpdateSessaoAsync(int id, UpdateSessaoTreinoDTO dto, string adestradorId);

    Task<SessaoTreinoResponseDTO?> DeleteSessaoAsync(int id, string adestradorId);
}