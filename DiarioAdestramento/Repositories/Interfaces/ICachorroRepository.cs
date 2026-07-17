using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface ICachorroRepository : IRepository<Cachorro>
{
  

    Task <PagedList<Cachorro>> GetCachorros(CachorrosParameters cachorrosParameters);

}
