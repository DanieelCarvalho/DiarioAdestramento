using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface ICachorroRepository : IRepository<Cachorro>
{
    Task<Cachorro?> GetComSessoesAsync(int id);

    Task <PagedList<Cachorro>> GetCachorros(CachorrosParameters cachorrosParameters);

}
