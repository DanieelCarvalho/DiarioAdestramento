using DiarioAdestramento.DTOs;
using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Services.Interfaces;

public interface ICachorroService
{
    Task<IEnumerable<CachorroResponseDTO>> GetAllCachorrosAsync(string adestradorId);

    Task<(IEnumerable<CachorroResponseDTO> items, PaginationMetadata metadata)> GetAllPagination(
        CachorrosParameters parametros, string adestradorId);

    Task<CachorroResponseDTO?> GetCachorroByIdAsync(int id, string adestradorId);

    Task<CachorroResponseDTO> CreateCachorroAsync(CachorroCreatedDTO cachorroCreatedDTO, string adestradorId);

    Task<CachorroResponseDTO?> UpdateCachorroAsync(CachorroCreatedDTO cachorroUpdatedDTO, string adestradorId);

    Task<CachorroResponseDTO?> DeleteCachorroAsync(int id, string adestradorId);

    Task<(bool ok, string? erro)> SalvarFotoAsync(int id, Stream conteudo, long tamanho, string adestradorId);
    Task<(byte[]? foto, string? tipo)> ObterFotoAsync(int id, string adestradorId);
    Task<bool> ExcluirFotoAsync(int id, string adestradorId);

}
