using DiarioAdestramento.Context;
using DiarioAdestramento.Pagination;
using DiarioAdestramento.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DiarioAdestramento.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
       _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
       return await _context.Set<T>()
         .AsNoTracking()
         .ToListAsync();
       
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>()
         .AsNoTracking()
         .FirstOrDefaultAsync(predicate);
    }

    public async Task<PagedList<T>> GetPagedAsync(int pageNumber, 
                                                  int pageSize, 
                                                  Expression<Func<T, object>> orderBy)
    {
       var query = _context.Set<T>()
            .AsNoTracking()
            .OrderBy(orderBy);

        return await PagedList<T>.ToPagedListAsync(query, pageNumber, pageSize);
    }

    public async Task<T> UpdateAsync(T entity)
    {
       _context.Set<T>().Update(entity);
       await _context.SaveChangesAsync();
        return entity;
    }

  
}
