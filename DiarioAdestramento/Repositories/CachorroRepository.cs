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

    public async Task<PagedList<Cachorro>> GetCachorros(CachorrosParameters cachorrosParameters)
    {
        var query = _context.Set<Cachorro>()
            .AsNoTracking()
            .OrderBy(c => c.Nome);   

        return await PagedList<Cachorro>.ToPagedListAsync(
            query,
            cachorrosParameters.PageNumber,
            cachorrosParameters.PageSize);
    }
    public async Task<Cachorro?> GetComSessoesAsync(int id)
    {
        return await _context.Set<Cachorro>()
            .Include(c => c.Sessao)
                .ThenInclude(s => s.RegistrosClima)
            .Include(c => c.Sessao)
                .ThenInclude(s => s.Local)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

 
}
