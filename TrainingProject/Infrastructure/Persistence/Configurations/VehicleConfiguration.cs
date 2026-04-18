using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingProject.Domain.Entities;

namespace TrainingProject.Infrastructure.Persistence.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
                builder.HasKey(v => v.Id);
    
                builder.Property(v => v.Make)
                    .IsRequired()
                    .HasMaxLength(255);
    
                builder.Property(v => v.Model)
                    .IsRequired()
                    .HasMaxLength(128);
    
                builder.Property(v => v.Year)
                    .IsRequired();
    
                builder.Property(v => v.Mileage)
                    .IsRequired();
    
                builder.Property(v => v.Price)
                    .IsRequired();

                builder.Property(v => v.CreatedAt)
                    .IsRequired();
        }
    }
}
