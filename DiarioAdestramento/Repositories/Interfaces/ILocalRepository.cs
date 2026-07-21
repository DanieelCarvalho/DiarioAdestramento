using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface ILocalRepository : IRepository<Local>
{
    Task<PagedList<Local>> GetLocaisAsync(LocalParameters parametros);
}
