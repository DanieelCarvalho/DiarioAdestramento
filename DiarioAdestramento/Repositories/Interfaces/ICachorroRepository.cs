using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface ICachorroRepository : IRepository<Cachorro>
{
    Task <PagedList<Cachorro>> GetCachorrosAsync(CachorrosParameters cachorrosParameters, string adestradorId);
    Task<Cachorro?> GetPorIdEAdestradorAsync(int id, string adestradorId);
    Task<IEnumerable<Cachorro>> GetTodosDoAdestradorAsync(string adestradorId);
    //Task<PagedList<Cachorro>> GetFiltroNome(CachorroFiltroNome nome);
}
