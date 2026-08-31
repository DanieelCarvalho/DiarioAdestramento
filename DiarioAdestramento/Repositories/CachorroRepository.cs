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

   

    public Task<PagedList<Cachorro>> GetCachorrosAsync(CachorrosParameters parametros, string adestradorId)
    {

        var query = _context.Set<Cachorro>()
            .AsNoTracking()
            .AsQueryable()
            .Where(c => c.AdestradorId == adestradorId);

        if (!string.IsNullOrEmpty(parametros.Nome))
        {
            var nomeLower = parametros.Nome.ToLower();
            query = query.Where(c => c.Nome.ToLower().Contains(nomeLower));
        }
            //query = query.Where(c => c.Nome.Contains(parametros.Nome, StringComparison.OrdinalIgnoreCase));

        query = query.OrderBy(c => c.Nome);

        return PagedList<Cachorro>.ToPagedListAsync(query, parametros.PageNumber, parametros.PageSize);
    }

    public async Task<Cachorro?> GetPorIdEAdestradorAsync(int id, string adestradorId)
    {
        return await _context.Set<Cachorro>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id && c.AdestradorId == adestradorId);
    }

    public async Task<IEnumerable<Cachorro>> GetTodosDoAdestradorAsync(string adestradorId)
    {
        return await _context.Set<Cachorro>()
            .Where(c => c.AdestradorId == adestradorId)
            .AsNoTracking()
            .ToListAsync();
    }
}

    
