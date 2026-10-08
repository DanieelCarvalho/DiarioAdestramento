using DiarioAdestramento.DTOs;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Services.Interfaces;

public interface ILocalService
{
    Task<IEnumerable<LocalResponseDTO>> GetTodosDoAdestradorAsync(string adestradorId);
    Task<(IEnumerable<LocalResponseDTO> items, PaginationMetadata metadata)> GetPaginateLocaisAsync(LocalParameters parameters, 
                                                                                                    string adestradorId);

    Task<LocalResponseDTO> GetPorIdEAdestradorAsync(int id, string adestradorId);
    Task<LocalResponseDTO> CreatedLocalAsync(LocalCreatedDTO localCreated, string adestradorId);
    Task<LocalResponseDTO> UpdateLocalAsync(LocalUpdateRequestDTO localCreated, string adestradorId);
    Task<LocalResponseDTO> DeleteLocalAsync(int id, string adestradorId);






}
