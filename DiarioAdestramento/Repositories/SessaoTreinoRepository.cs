using DiarioAdestramento.Context;
using DiarioAdestramento.Models;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories;
using DiarioAdestramento.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class SessaoTreinoRepository : Repository<SessaoTreino>, ISessaoTreinoRepository
{
    public SessaoTreinoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<SessaoTreino?> GetComDetalhesAsync(int id, string adestradorId)
    {
        return await _context.Set<SessaoTreino>()
            .Include(s => s.Cachorro)
            .Include(s => s.Local)
            .Include(s => s.RegistrosClima)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id && s.Cachorro!.AdestradorId == adestradorId);
    }

    public async Task<PagedList<SessaoTreino>> GetAllComDetalhesAsync(SessoesParameters parametros,
                                                                      string adestradorId)
    {
        var query = _context.Set<SessaoTreino>()
            .Include(s => s.Cachorro)
            .Include(s => s.Local)
            .Include(s => s.RegistrosClima)
            .Where(s => s.Cachorro!.AdestradorId == adestradorId)
            .AsNoTracking();

        return await PagedList<SessaoTreino>.ToPagedListAsync(query,
                                                              parametros.PageNumber,
                                                              parametros.PageSize);
    }

    public async Task<SessaoTreino?> GetPorIdEAdestradorAsync(int id, string adestradorId)
    {
        // Sem AsNoTracking: usada em Update/Delete, onde a entidade precisa continuar rastreada.
        return await _context.Set<SessaoTreino>()
            .Include(s => s.Cachorro)
            .FirstOrDefaultAsync(s => s.Id == id && s.Cachorro!.AdestradorId == adestradorId);
    }

    public async Task<PagedList<SessaoTreino>> GetPorCachorroAsync(int cachorroId,
                                                                   string adestradorId,
                                                                   int pageNum,
                                                                   int pageSize)
    {
        var query = _context.Set<SessaoTreino>()
            .Where(s => s.CachorroId == cachorroId && s.Cachorro!.AdestradorId == adestradorId)
            .Include(s => s.Cachorro)
            .Include(s => s.Local)
            .Include(s => s.RegistrosClima)
            .OrderByDescending(s => s.Data)
            .AsNoTracking();

        return await PagedList<SessaoTreino>.ToPagedListAsync(query,
                                                              pageNum,
                                                              pageSize);
    }

    
}