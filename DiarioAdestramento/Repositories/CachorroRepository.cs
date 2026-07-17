using DiarioAdestramento.Context;
using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DiarioAdestramento.Repositories;

public class CachorroRepository : Repository<Cachorro>, ICachorroRepository
{
    public CachorroRepository(AppDbContext context) : base(context)
    {
    }

    public Task<PagedList<Cachorro>> GetCachorrosAsync(CachorrosParameters parametros)
    => GetPagedAsync(parametros.PageNumber, parametros.PageSize, c => c.Nome);



}
