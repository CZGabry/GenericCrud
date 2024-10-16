using GenericCrud.Db.Config;
using GenericCrud.Filter.Sort;
using GenericCrud.Helper;
using GenericCrud.Repositories.Interfaces.Base;
using GenericCrud.Result;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GenericCrud.Repositories.Base
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;
        protected readonly bool _viewEntity;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _entities.ToListAsync();
        public async Task<T> GetByIdAsync(int id) => await _entities.FindAsync(id);
        public async Task AddAsync(T entity)
        {
            try
            {
                await _entities.AddAsync(entity);

                int result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    Console.WriteLine("Entity added successfully.");
                }
                else
                {
                    Console.WriteLine("Entity addition failed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding the entity: {ex.Message}");
                throw;
            }
        }

        protected virtual IQueryable<T> GetQuery(bool enableTracking = false)
        {
            IQueryable<T> query = _context.Set<T>();
            if (enableTracking == false) query = query.AsNoTracking();

            return query;
        }
        public virtual FindResponse<T> Find(Expression<Func<T, bool>> where, CountType countType,
            SortInfo<T> orderBy = null,
            int limit = 0, int offset = 0, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = GetQuery();

            foreach (var navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include(navigationProperty);

            return BaseFind(dbQuery, where, countType, orderBy, limit, offset);
        }

        protected FindResponse<T> BaseFind(IQueryable<T> dbQuery, Expression<Func<T, bool>> where,
            CountType countType,
            SortInfo<T> orderBy = null, int limit = 0, int offset = 0)
        {
            dbQuery = dbQuery.Where(where);

            long count = 0;

            if (countType == CountType.Yes) count = CountResults(dbQuery, where, countEstimate: false);
            else if (countType == CountType.Estimate) count = CountResults(dbQuery, where, countEstimate: true);

            if (orderBy != null) dbQuery = QueryableHelper.UpdateQueryWithOrderBy(dbQuery, orderBy);

            // Apply the pagination filters to the query.
            dbQuery = QueryableHelper.UpdateQueryWithPagination(dbQuery, limit, offset);

            return new FindResponse<T> { Data = dbQuery.ToList(), Count = count };
        }

        protected long CountResults(IQueryable<T> dbQuery, Expression<Func<T, bool>> where,
            bool countEstimate)
        {
            // If estimate count is requested, and can be used (empty where and not a view), then use it.
            if (countEstimate && !_viewEntity && (where == null || where.Body.GetType() == typeof(ConstantExpression)))
            {
                var mapping = _context.Model.FindEntityType(typeof(T));
                string tableFullName = (mapping.GetSchema() ?? "public") + "." + mapping.GetTableName();
                //string tableFullName = "public" + "." + mapping.Name;// .GetTableName();

                string sqlQuery =
                    $"SELECT reltuples::bigint as Count FROM pg_class WHERE oid = '{tableFullName}'::regclass;";
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = sqlQuery;
                    _context.Database.OpenConnection();
                    try
                    {
                        using (var result = command.ExecuteReader())
                        {
                            result.Read();
                            return result.GetInt64(0);
                        }
                    }
                    finally
                    {
                        _context.Database.CloseConnection();
                    }
                }
            }
            else
                return dbQuery.LongCount();
        }
    
        public async Task UpdateAsync(int id, T entity)
            {
                try
                {
                    var originalEntity = await _entities.FindAsync(id);

                    if (originalEntity == null)
                    {
                        Console.WriteLine("Entity not found.");
                        return;
                    }

                    foreach (var property in _context.Entry(originalEntity).Properties)
                    {
                        if (!property.Metadata.IsPrimaryKey())
                        {
                            var newValue = _context.Entry(entity).Property(property.Metadata.Name).CurrentValue;
                            property.CurrentValue = newValue;
                        }
                    }

                    int result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        Console.WriteLine("Entity updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Entity update failed.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while updating the entity: {ex.Message}");
                    throw;
                }
            }


        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);

                if (entity == null)
                {
                    Console.WriteLine("Entity not found.");
                    return;
                }

                _entities.Remove(entity);

                int result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    Console.WriteLine("Entity removed successfully.");
                }
                else
                {
                    Console.WriteLine("Entity removal failed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing the entity: {ex.Message}");
                throw;
            }
        }
    }
}
