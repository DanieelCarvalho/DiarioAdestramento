using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface ICachorroRepository : IRepository<Cachorro>
{
    Task <PagedList<Cachorro>> GetCachorrosAsync(CachorrosParameters cachorrosParameters);
    Task<PagedList<Cachorro>> GetCachorroFiltroNome(CachorroFiltroNome nome);
}
