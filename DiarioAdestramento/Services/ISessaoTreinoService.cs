using DiarioAdestramento.Dtos;
using DiarioAdestramento.DTOs;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Services.Interfaces;

public interface ISessaoTreinoService
{
    Task<(IEnumerable<SessaoTreinoResponseDTO> items, PaginationMetadata metadata)> GetAllSessoesAsync(SessoesParameters parametros);

    Task<SessaoTreinoResponseDTO?> GetSessaoByIdAsync(int id);

    Task<(CachorroComSessoesResponseDTO? cachorro, PaginationMetadata? metadata)> GetSessoesByCachorroIdAsync(
        int cachorroId, SessoesParameters parametros);

    Task<(SessaoTreinoResponseDTO? sessao, string? erro)> CreateSessaoAsync(CriarSessaoTreinoDto dto);

    Task<SessaoTreinoResponseDTO?> UpdateSessaoAsync(int id, UpdateSessaoTreinoDTO dto);

    Task<SessaoTreinoResponseDTO?> DeleteSessaoAsync(int id);
}