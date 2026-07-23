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


    public async Task<SessaoTreino?> GetComDetalhesAsync(int id)
    {
        return await _context.Set<SessaoTreino>()
            .Include(s => s.Cachorro)
            .Include(s => s.Local)
            .Include(s => s.RegistrosClima)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<PagedList<SessaoTreino>> GetAllComDetalhesAsync(SessoesParameters parametros)
    {
        var query = _context.Set<SessaoTreino>()
            .Include(s => s.Cachorro)
            .Include(s => s.Local)
            .Include(s => s.RegistrosClima)
            .AsNoTracking();

        return await PagedList<SessaoTreino>.ToPagedListAsync(query, parametros.PageNumber, parametros.PageSize);
    }

    public async Task<PagedList<SessaoTreino>> GetPorCachorroAsync(int cachorroId, int pageNum, int pageSize)
    {
        var query = _context.Set<SessaoTreino>()
            .Where(s => s.CachorroId == cachorroId)
            .Include(s => s.Cachorro)
            .Include(s => s.Local)
            .Include(s => s.RegistrosClima)
            .OrderByDescending(s => s.Data)
            .AsNoTracking();

        return await PagedList<SessaoTreino>.ToPagedListAsync(query, pageNum, pageSize);
    }
}