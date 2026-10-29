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
    private const long TamanhoMaximo = 2 * 1024 * 1024;
    // Descobre o tipo pelos primeiros bytes do arquivo. O Content-Type e o nome
    // vêm do cliente e podem mentir, os bytes não.
    private static string? DetectarTipo(byte[] h) =>
        h.Length >= 3 && h[0] == 0xFF && h[1] == 0xD8 && h[2] == 0xFF ? "image/jpeg" :
        h.Length >= 4 && h[0] == 0x89 && h[1] == 0x50 && h[2] == 0x4E && h[3] == 0x47 ? "image/png" :
        h.Length >= 12 && h[0] == 'R' && h[1] == 'I' && h[2] == 'F' && h[3] == 'F'
                       && h[8] == 'W' && h[9] == 'E' && h[10] == 'B' && h[11] == 'P' ? "image/webp" :
        null;



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


   
    public async Task<(bool ok, string? erro)> SalvarFotoAsync(
        int id, Stream conteudo, long tamanho, string adestradorId)
    {
        if (tamanho <= 0 || tamanho > TamanhoMaximo)
            return (false, "A foto deve ter até 2 MB.");

        var cachorro = await _cachorroRepository.GetPorIdEAdestradorAsync(id, adestradorId);
        if (cachorro is null)
            return (false, "Cachorro não encontrado.");

        using var ms = new MemoryStream();
        await conteudo.CopyToAsync(ms);
        var bytes = ms.ToArray();

        if (DetectarTipo(bytes) is null)
            return (false, "Formato inválido. Use JPG, PNG ou WebP.");

        cachorro.Foto = bytes;
        await _cachorroRepository.UpdateAsync(cachorro);
        return (true, null);
    }

    public async Task<(byte[]? foto, string? tipo)> ObterFotoAsync(int id, string adestradorId)
    {
        var cachorro = await _cachorroRepository.GetPorIdEAdestradorAsync(id, adestradorId);
        if (cachorro?.Foto is null)
            return (null, null);

        return (cachorro.Foto, DetectarTipo(cachorro.Foto));
    }

    public async Task<bool> ExcluirFotoAsync(int id, string adestradorId)
    {
        var cachorro = await _cachorroRepository.GetPorIdEAdestradorAsync(id, adestradorId);
        if (cachorro?.Foto is null)
            return false;

        cachorro.Foto = null;
        await _cachorroRepository.UpdateAsync(cachorro);
        return true;
    }


}
