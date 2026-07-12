using DiarioAdestramento.Context;
using DiarioAdestramento.Models;
using DiarioAdestramento.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiarioAdestramento.Repositories;

public class CachorroRepository : Repository<Cachorro>, ICachorroRepository
{
    public CachorroRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Cachorro?> GetComSessoesAsync(int id)
    {
        return await _context.Set<Cachorro>()
            .Include(c => c.Sessao)
            .ThenInclude(s => s.RegistrosClima)
            .Include(l => l.Sessao)
            .ThenInclude(l => l.Local)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);


    }
}
