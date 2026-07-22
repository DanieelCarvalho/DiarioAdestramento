using DiarioAdestramento.DTOs;
using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Services.Interfaces;

public interface ICachorroService
{
    Task<IEnumerable<CachorroResponseDTO>> GetAllCachorrosAsync();

    Task<(IEnumerable<CachorroResponseDTO> items, PaginationMetadata metadata)> GetAllPagination(CachorrosParameters parametros);

    Task<CachorroResponseDTO?> GetCachorroByIdAsync(int id);

    Task<CachorroResponseDTO> CreateCachorroAsync(CachorroCreatedDTO cachorroCreatedDTO);

    Task<CachorroResponseDTO?> UpdateCachorroAsync(CachorroCreatedDTO cachorroRequestDTO);

    Task<CachorroResponseDTO?> DeleteCachorroAsync(int  id);

}
