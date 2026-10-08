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

    public async Task<IEnumerable<LocalResponseDTO>> GetTodosDoAdestradorAsync(string adestradorId)
    {
        var locais = await _repository.GetTodosDoAdestradorAsync(adestradorId);
        if (locais == null) return null;
        

        var locaisDTO = locais.ToLocalResponseDTOList();

        return locaisDTO;

    }
    public async Task<(IEnumerable<LocalResponseDTO> items, PaginationMetadata metadata)> GetPaginateLocaisAsync(
           LocalParameters parameters, string adestradorId)
    {
        var locais = await _repository.GetLocaisAsync(parameters, adestradorId);

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
  

    public async Task<LocalResponseDTO> CreatedLocalAsync(LocalCreatedDTO localCreated, string adestradorId)
    {
        var local = localCreated.ToLocal();
        local.AdestradorId = adestradorId;
        await _repository.AddAsync(local);
        

        return local.ToLocalCreatedDTO();
    }

    public async Task<LocalResponseDTO> UpdateLocalAsync(LocalUpdateRequestDTO localDTO, 
                                                         string adestradorId)
    {
        var localExistente = await _repository.GetPorIdEAdestradorAsync(localDTO.Id, adestradorId);
        if (localExistente == null)
        {
            throw new Exception("Local não encontrado ou não pertence ao adestrador.");
        }

        var local = localDTO.ToLocalUpdadte();

        local.AdestradorId = adestradorId;
        var localAtualizado = await _repository.UpdateAsync(local);


        var localAtualizadoDTO = localAtualizado.ToLocalResponseDTO();
        return localAtualizadoDTO;

    }
    public async Task<LocalResponseDTO> DeleteLocalAsync(int id, string adestradorId)
    {
        var localExistente = await _repository.GetPorIdEAdestradorAsync(id, adestradorId);
        if (localExistente == null)
        {
            throw new Exception("Local não encontrado ou não pertence ao adestrador.");
        }
        var local = await _repository.GetAsync(l => l.Id == id);

        var LocalExcluido = await _repository.DeleteAsync(local);
        var localExcluidoDTO = LocalExcluido.ToLocalResponseDTO();

        return localExcluidoDTO;

    }

    public async Task<LocalResponseDTO> GetPorIdEAdestradorAsync(int id, string adestradorId)
    {
       var local = await _repository.GetPorIdEAdestradorAsync(id, adestradorId);
        if(local is null) return null;

        return local.ToLocalResponseDTO();
    }
}
