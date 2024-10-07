using GenericCrud.Db.Config;
using GenericCrud.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace GenericCrud.Repositories.Base
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;

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
        public async Task UpdateAsync(T entity) => _entities.Update(entity);
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) _entities.Remove(entity);
        }
    }
}
