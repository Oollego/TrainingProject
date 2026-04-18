using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //var builder = modelBuilder.Entity<Vehicle>();

            //builder.HasKey(v => v.Id);
        }
    }


    public class MyDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles2 { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);


        }

    }
}
