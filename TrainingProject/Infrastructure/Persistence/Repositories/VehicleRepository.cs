using Microsoft.EntityFrameworkCore;
using TrainingProject.Domain.Entities;
using TrainingProject.Domain.Interfaces;

namespace TrainingProject.Infrastructure.Persistence.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly TrainDbContext _context;
        public VehicleRepository(TrainDbContext context )
        {
            _context = context;
        }
        public async Task<Vehicle> CreateVehicleAsync(Vehicle vehicle, CancellationToken ct)
        {
            vehicle.CreatedAt = DateTime.UtcNow;
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync(ct);  
            return vehicle;
        }

        public async Task DeleteVehicleAsync(Guid Id, CancellationToken ct)
        {
            var vehicle = await _context.Vehicles.FindAsync(Id, ct);

            if (vehicle is not null)
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task<List<Vehicle>> GetVehiclesAsync(CancellationToken ct, int quantity, int page)
        {
            return await _context.Vehicles.Skip(page * quantity).Take(quantity).ToListAsync(ct);

        }

        public async Task<Vehicle?> GetVehicleByIdAsync(Guid Id, CancellationToken ct)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == Id, ct);
        }

        public async Task<int> UpdateVehicleAsync(Vehicle vehicle, CancellationToken ct)
        {
            return await _context.Vehicles.Where(x => x.Id == vehicle.Id).ExecuteUpdateAsync(x => x
                .SetProperty(p => p.Make, vehicle.Make)
                .SetProperty(p => p.Model, vehicle.Model)
                .SetProperty(p => p.Year, vehicle.Year)
                .SetProperty(p => p.Mileage, vehicle.Mileage)
                .SetProperty(p => p.Price, vehicle.Price)
            , ct);
        }
    }
}
