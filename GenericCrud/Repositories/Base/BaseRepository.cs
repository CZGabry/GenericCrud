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
