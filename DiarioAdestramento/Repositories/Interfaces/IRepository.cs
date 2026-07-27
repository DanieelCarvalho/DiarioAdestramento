using DiarioAdestramento.Pagination;
using System.Linq.Expressions;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface  IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> GetAllQueryable();

    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);


}
