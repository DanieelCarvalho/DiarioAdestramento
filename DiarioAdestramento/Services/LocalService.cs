using DiarioAdestramento.DTOs;
using DiarioAdestramento.DTOs.Mappings;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories.Interfaces;
using DiarioAdestramento.Services.Interfaces;

namespace DiarioAdestramento.Services;

public class LocalService : ILocalService
{
    private readonly ILocalRepository _repository;

    public LocalService(ILocalRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LocalResponseDTO>> GetAllLocaisAsync()
    {
        var locais = await _repository.GetAllAsync();
        if (locais == null) return null;

        var locaisDTO = locais.ToLocalResponseDTOList();

        return locaisDTO;

    }
    public async Task<(IEnumerable<LocalResponseDTO> items, PaginationMetadata metadata)> GetPaginateLocaisAsync(LocalParameters parameters)
    {
        var locais = await _repository.GetLocaisAsync(parameters);

        var locaisDTO = locais.ToLocalResponseDTOList();

        var metadata = new PaginationMetadata
        {
            TotalCount = locais.TotalCount,
            PageSize = locais.PageSize,
            CurrentPage = locais.CurrentPage,
            TotalPages = locais.TotalPages,
            HasNext = locais.HasNext,
            HasPrevious = locais.HasPrevious
        };

        return(locaisDTO, metadata);

    }
    public async Task<LocalResponseDTO> GetByIdLocalAsync(int id)
    {
        var local = await _repository.GetAsync(l => l.Id == id);
        var localDTO = local.ToLocalResponseDTO();
        return localDTO;
    }

    public async Task<LocalResponseDTO> CreatedLocalAsync(LocalCreatedDTO localCreated)
    {
        var local = localCreated.ToLocal();
        await _repository.AddAsync(local);
        var createdLocalDTO = local.ToLocalCreatedDTO();

        return createdLocalDTO;
    }

    public async Task<LocalResponseDTO> UpdateLocalAsync(LocalUpdateRequestDTO localDTO)
    {
        var local = localDTO.ToLocalUpdadte();
        var localAtualizado = await _repository.UpdateAsync(local);

        var localAtualizadoDTO = localAtualizado.ToLocalResponseDTO();
        return localAtualizadoDTO;

    }
    public async Task<LocalResponseDTO> DeleteLocalAsync(int id)
    {
        var local = await _repository.GetAsync(l => l.Id == id);

        var LocalExcluido = await _repository.DeleteAsync(local);
        var localExcluidoDTO = LocalExcluido.ToLocalResponseDTO();

        return localExcluidoDTO;

    }

}
