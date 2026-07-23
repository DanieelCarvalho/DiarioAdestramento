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

    public  Task<PagedList<Cachorro>> GetCachorroFiltroNome(CachorroFiltroNome nome)
    {
        var cachorro =  GetAllAsync().AsQueryable();

        if(!string.IsNullOrEmpty(nome.Nome))
        {
            cachorro = cachorro.Where(c => c.Nome.Contains(nome.Nome));
        }
        var cachorroFiltrados = PagedList<Cachorro>.ToPagedListAsync(cachorro, nome.PageNumber, nome.PageSize);

        return cachorroFiltrados;
    }

    public Task<PagedList<Cachorro>> GetCachorrosAsync(CachorrosParameters parametros)
    => GetPagedAsync(parametros.PageNumber, parametros.PageSize, c => c.Nome);

}
