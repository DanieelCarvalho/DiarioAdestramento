using DiarioAdestramento.DTOs;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Services.Interfaces;

public interface ILocalService
{
    Task<IEnumerable<LocalResponseDTO>> GetAllLocaisAsync();
    Task<(IEnumerable<LocalResponseDTO> items, PaginationMetadata metadata)> GetPaginateLocaisAsync(LocalParameters parameters);
    Task<LocalResponseDTO> GetByIdLocalAsync(int id);
    Task<LocalResponseDTO> CreatedLocalAsync(LocalCreatedDTO localCreated);
    Task<LocalResponseDTO> UpdateLocalAsync(LocalUpdateRequestDTO localCreated);
    Task<LocalResponseDTO> DeleteLocalAsync(int id);






}
