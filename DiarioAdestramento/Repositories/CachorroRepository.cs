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
    {

        var query = _context.Set<Cachorro>().AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(parametros.Nome))
        {
            var nomeLower = parametros.Nome.ToLower();
            query = query.Where(c => c.Nome.ToLower().Contains(nomeLower));
        }
            //query = query.Where(c => c.Nome.Contains(parametros.Nome, StringComparison.OrdinalIgnoreCase));

        query = query.OrderBy(c => c.Nome);

        return PagedList<Cachorro>.ToPagedListAsync(query, parametros.PageNumber, parametros.PageSize);
    }
   

}
