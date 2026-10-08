using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface ILocalRepository : IRepository<Local>
{
    Task<PagedList<Local>> GetLocaisAsync(LocalParameters parametros, string adestradorId);

    Task<Local?> GetPorIdEAdestradorAsync(int id, string adestradorId);

    Task<IEnumerable<Local>> GetTodosDoAdestradorAsync(string adestradorId);
}
