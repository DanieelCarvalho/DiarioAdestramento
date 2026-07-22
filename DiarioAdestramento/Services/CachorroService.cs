using DiarioAdestramento.DTOs;
using DiarioAdestramento.DTOs.Mappings;
using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories.Interfaces;
using DiarioAdestramento.Services.Interfaces;
using System.Text.Json;

namespace DiarioAdestramento.Services;

public class CachorroService : ICachorroService
{
    private readonly ICachorroRepository _cachorroRepository;

    public CachorroService(ICachorroRepository cachorroRepository)
    {
        _cachorroRepository = cachorroRepository;
    }
    public async Task<IEnumerable<CachorroResponseDTO>> GetAllCachorrosAsync()
    {
        var cachorros = await _cachorroRepository.GetAllAsync();

        var cachorrosDTO = cachorros.ToCachorroResponseDTOList();
        return cachorrosDTO;
    }

    public async Task<(IEnumerable<CachorroResponseDTO> items, PaginationMetadata metadata)> GetAllPagination(CachorrosParameters parametros)
    {
        var cachorros = await _cachorroRepository.GetCachorrosAsync(parametros);
        var cachorrosDTO = cachorros.ToCachorroResponseDTOList();
        var metadata = new PaginationMetadata
        {
            TotalCount = cachorros.TotalCount,
            PageSize = cachorros.PageSize,
            CurrentPage = cachorros.CurrentPage,
            TotalPages = cachorros.TotalPages,
            HasNext = cachorros.HasNext,
            HasPrevious = cachorros.HasPrevious
        };

        return (cachorrosDTO, metadata);
    }

    public async Task<CachorroResponseDTO?> GetCachorroByIdAsync(int id)
    {
        var cachorro = await _cachorroRepository.GetAsync(c => c.Id == id);

        if (cachorro is null)
            return null;

        return cachorro.ToCachorroResponseDTO();
    }

    public async Task<CachorroResponseDTO> CreateCachorroAsync(CachorroCreatedDTO cachorroCreatedDTO)
    {
        var cachorro = cachorroCreatedDTO.ToCachorro();

        await _cachorroRepository.AddAsync(cachorro);

        var cachorroDTO = cachorro.ToCachorroResponseDTO();

        return cachorroDTO;
    }

    public async Task<CachorroResponseDTO?> UpdateCachorroAsync(CachorroCreatedDTO cachorroUpdatedDTO)
    {
        var cachorro = cachorroUpdatedDTO.ToCachorro();

        var cachorroExiste = await _cachorroRepository.UpdateAsync(cachorro);

        var cachorroAtualizado = cachorroExiste.ToCachorroResponseDTO();

        return cachorroAtualizado;
    }

    public async Task<CachorroResponseDTO?> DeleteCachorroAsync(int id)
    {
        var cachorro = await _cachorroRepository.GetAsync(c => c.Id == id);

        if (cachorro is null)
            return null;

        var cachorroExcluido = await _cachorroRepository.DeleteAsync(cachorro);

        return cachorroExcluido.ToCachorroResponseDTO();
    }

}
