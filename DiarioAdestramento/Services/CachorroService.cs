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
    public async Task<IEnumerable<CachorroResponseDTO>> GetAllCachorrosAsync(string adestradorId)
    {
        var cachorros = await _cachorroRepository.GetTodosDoAdestradorAsync(adestradorId);
        return cachorros.ToCachorroResponseDTOList();
    }


    public async Task<(IEnumerable<CachorroResponseDTO> items, PaginationMetadata metadata)> GetAllPagination(
      CachorrosParameters parametros, string adestradorId)
    {
        var cachorros = await _cachorroRepository.GetCachorrosAsync(parametros, adestradorId);
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

    public async Task<CachorroResponseDTO?> GetCachorroByIdAsync(int id, string adestradorId)
    {
        var cachorro = await _cachorroRepository.GetPorIdEAdestradorAsync(id, adestradorId);
        if (cachorro is null)
            return null;

        return cachorro.ToCachorroResponseDTO();
    }


    public async Task<CachorroResponseDTO> CreateCachorroAsync(CachorroCreatedDTO cachorroCreatedDTO, string adestradorId)
    {
        var cachorro = cachorroCreatedDTO.ToCachorro();
        cachorro.AdestradorId = adestradorId; 

        await _cachorroRepository.AddAsync(cachorro);

        return cachorro.ToCachorroResponseDTO();
    }


    public async Task<CachorroResponseDTO?> UpdateCachorroAsync(CachorroCreatedDTO cachorroUpdatedDTO, string adestradorId)
    {
        var cachorroExistente = await _cachorroRepository.GetPorIdEAdestradorAsync(cachorroUpdatedDTO.Id, adestradorId);
        if (cachorroExistente is null)
            return null;

        var cachorro = cachorroUpdatedDTO.ToCachorro();
        cachorro.AdestradorId = adestradorId; 

        var cachorroAtualizado = await _cachorroRepository.UpdateAsync(cachorro);
        return cachorroAtualizado.ToCachorroResponseDTO();
    }


    public async Task<CachorroResponseDTO?> DeleteCachorroAsync(int id, string adestradorId)
    {
        var cachorro = await _cachorroRepository.GetPorIdEAdestradorAsync(id, adestradorId);
        if (cachorro is null)
            return null;

        var cachorroExcluido = await _cachorroRepository.DeleteAsync(cachorro);
        return cachorroExcluido.ToCachorroResponseDTO();
    }



}
