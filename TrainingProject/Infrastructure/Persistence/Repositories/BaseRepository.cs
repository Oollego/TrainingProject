using Microsoft.EntityFrameworkCore;
using TrainingProject.Domain.Interfaces;

namespace TrainingProject.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly TrainDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(TrainDbContext context) 
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<T> CreateAsync(T vehicle)
        {
            _dbSet.Add(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        public async Task DeleteAsync(Guid id)
        {
            var vehicle = await _dbSet.FindAsync(id);

            if (vehicle is not null)
            {
                _dbSet.Remove(vehicle);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> GetValueAsync(Guid id)
        {
                
             var vehicle = await _dbSet.FindAsync(id);
            if(vehicle is null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }

            return vehicle;
        }

        public Task UpdateAsync(T vehicle)
        {
            _dbSet.Update(vehicle);
            return _context.SaveChangesAsync();
        }
    }
}
