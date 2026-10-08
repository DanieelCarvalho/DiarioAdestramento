using DiarioAdestramento.Context;
using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiarioAdestramento.Repositories;

public class LocalRepository : Repository<Local>, ILocalRepository
{
    public LocalRepository(AppDbContext context) : base(context)
    {
    }

    public Task<PagedList<Local>> GetLocaisAsync(LocalParameters parametros, string adestradorId)
    {
        var query = _context.Set<Local>().AsNoTracking().AsQueryable();

        if (!string.IsNullOrEmpty(parametros.Nome))
            query = query.Where(l => l.Nome.Contains(parametros.Nome));

        query = query.Where(l => l.AdestradorId == adestradorId);

        query = query.OrderBy(l => l.Nome);

        return PagedList<Local>.ToPagedListAsync(query, 
                                                 parametros.PageNumber, 
                                                 parametros.PageSize);
    }

    public Task<Local?> GetPorIdEAdestradorAsync(int id, string adestradorId)
    {
        return _context.Set<Local>()
            .AsNoTracking()
                       .FirstOrDefaultAsync(l => l.Id == id && l.AdestradorId == adestradorId);
    }

    public async Task<IEnumerable<Local>> GetTodosDoAdestradorAsync(string adestradorId)
    {
        return await _context.Set<Local>()
                       .Where(l => l.AdestradorId == adestradorId)
                       .AsNoTracking()
                       .ToListAsync();
                       
    }
}
