using DiarioAdestramento.Pagination;
using System.Linq.Expressions;

namespace DiarioAdestramento.Repositories.Interfaces;

public interface  IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

    Task<PagedList<T>> GetPagedAsync(int pageNumber, 
                          int pageSize, 
                          Expression<Func<T, object>> orderBy);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);


}
