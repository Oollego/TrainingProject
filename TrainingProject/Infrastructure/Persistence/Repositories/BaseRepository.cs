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
        public async Task<T> CreateAsync(T vehicle, CancellationToken ct)
        {
            _dbSet.Add(vehicle);
            await _context.SaveChangesAsync(ct);

            return vehicle;
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var vehicle = await _dbSet.FindAsync(id, ct);

            if (vehicle is not null)
            {
                _dbSet.Remove(vehicle);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task<T> GetValueAsync(Guid id, CancellationToken ct)
        {
                
             var vehicle = await _dbSet.FindAsync(id, ct);
            if(vehicle is null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }

            return vehicle;
        }

        public Task UpdateAsync(T vehicle, CancellationToken ct)
        {
            _dbSet.Update(vehicle);
            return _context.SaveChangesAsync(ct);
        }
    }
}
