using DiarioAdestramento.Context;
using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories.Interfaces;

namespace DiarioAdestramento.Repositories;

public class LocalRepository : Repository<Local>, ILocalRepository
{
    public LocalRepository(AppDbContext context) : base(context)
    {
    }

    public Task<PagedList<Local>> GetLocaisAsync(LocalParameters parametros)
        =>GetPagedAsync(parametros.PageNumber, parametros.PageSize , l => l.Nome);


}
