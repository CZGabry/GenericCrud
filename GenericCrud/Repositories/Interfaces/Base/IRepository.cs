using GenericCrud.Filter.Sort;
using GenericCrud.Result;
using System.Linq.Expressions;

namespace GenericCrud.Repositories.Interfaces.Base
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        FindResponse<T> Find(Expression<Func<T, bool>> where, CountType countType, SortInfo<T> orderBy = null,
            int limit = 0, int offset = 0, params Expression<Func<T, object>>[] navigationProperties);
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
