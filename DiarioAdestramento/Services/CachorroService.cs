using DiarioAdestramento.DTOs;
using DiarioAdestramento.DTOs.Mappings;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories.Interfaces;
using DiarioAdestramento.Services.Interfaces;
using DiarioAdestramento.Exceptions;

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

    public async Task<CachorroResponseDTO> GetCachorroByIdAsync(int id, string adestradorId)
    {
        var cachorro = await _cachorroRepository.GetPorIdEAdestradorAsync(id, adestradorId) ??
                       throw new NaoEncontradoException("Cachorro não encontrado.");
       

        return cachorro.ToCachorroResponseDTO();
    }


    public async Task<CachorroResponseDTO> CreateCachorroAsync(CachorroCreatedDTO cachorroCreatedDTO, string adestradorId)
    {
        var cachorro = cachorroCreatedDTO.ToCachorro();
        cachorro.Id = 0;
        cachorro.AdestradorId = adestradorId; 

        await _cachorroRepository.AddAsync(cachorro);

        return cachorro.ToCachorroResponseDTO();
    }


    public async Task<CachorroResponseDTO> UpdateCachorroAsync(CachorroCreatedDTO cachorroUpdatedDTO, string adestradorId)
    {
        var cachorroExistente = await _cachorroRepository.GetPorIdEAdestradorAsync(cachorroUpdatedDTO.Id, adestradorId) 
                                ?? throw new NaoEncontradoException("Cachorro não encontrado.");
      

        cachorroExistente.Nome = cachorroUpdatedDTO.Nome;
        cachorroExistente.Idade = cachorroUpdatedDTO.Idade;
        cachorroExistente.Raca = cachorroUpdatedDTO.Raca;

        var cachorroAtualizado = await _cachorroRepository.UpdateAsync(cachorroExistente);
        return cachorroAtualizado.ToCachorroResponseDTO();
    }

    public async Task<CachorroResponseDTO> DeleteCachorroAsync(int id, string adestradorId)
    {
        var cachorro = await _cachorroRepository.GetPorIdEAdestradorAsync(id, adestradorId) ??
                       throw new NaoEncontradoException("Cachorro não encontrado.");
       

        var cachorroExcluido = await _cachorroRepository.DeleteAsync(cachorro);
        return cachorroExcluido.ToCachorroResponseDTO();
    }


   
    public async Task SalvarFotoAsync(
        int id, Stream conteudo, long tamanho, string adestradorId)
    {
        if (tamanho <= 0 || tamanho > TamanhoMaximo)
            throw new ValidacaoException("Foto inválida",
            [new DetalheErro("arquivo", "tamanho_maximo_2mb")]);

        var cachorro = await _cachorroRepository.GetPorIdEAdestradorAsync(id, adestradorId)
                       ?? throw new NaoEncontradoException("Cachorro não encontrado.");
                       
       

        using var ms = new MemoryStream();
        await conteudo.CopyToAsync(ms);
        var bytes = ms.ToArray();

        if (DetectarTipo(bytes) is null)
            throw new ValidacaoException("Foto inválida",
                [new DetalheErro("arquivo", "formato_invalido_use_jpg_png_ou_webp")]);

        cachorro.Foto = bytes;
        await _cachorroRepository.UpdateAsync(cachorro);
       
    }

    public async Task<(byte[]foto, string tipo)> ObterFotoAsync(int id, string adestradorId)
    {
        var cachorro = await _cachorroRepository.GetPorIdEAdestradorAsync(id, adestradorId)
            ?? throw new NaoEncontradoException("Cachorro não encontrado.");

        if (cachorro.Foto is null)
            throw new NaoEncontradoException("Cachorro não possui foto.");
      
        

        return (cachorro.Foto, DetectarTipo(cachorro.Foto)?? "application/octet-stream");
    }

    public async Task ExcluirFotoAsync(int id, string adestradorId)
    {
        var cachorro = await _cachorroRepository.GetPorIdEAdestradorAsync(id, adestradorId)
            ?? throw new NaoEncontradoException("Cachorro não encontrado.");
    
        if (cachorro.Foto is null)
          throw new NaoEncontradoException("Cachorro não possui foto.");
        cachorro.Foto = null;
        await _cachorroRepository.UpdateAsync(cachorro);
    }


}
