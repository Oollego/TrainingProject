using Microsoft.EntityFrameworkCore;
using TrainingProject.Domain.Entities;

namespace TrainingProject.Infrastructure.Persistence
{
    public class TrainDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; } = null!;

        public TrainDbContext(DbContextOptions<TrainDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Vehicle>();
        }
    }
}
