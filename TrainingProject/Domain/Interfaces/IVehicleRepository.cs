using TrainingProject.Domain.Entities;

namespace TrainingProject.Domain.Interfaces
{
    public interface IVehicleRepository
    {

        Task<Vehicle?> GetVehicleByIdAsync(Guid Id, CancellationToken ct);

        Task<List<Vehicle>> GetVehiclesAsync(CancellationToken ct, int quantity, int page);

        Task<Vehicle> CreateVehicleAsync(Vehicle vehicle, CancellationToken ct);

        Task DeleteVehicleAsync(Guid Id, CancellationToken ct);

        Task<int> UpdateVehicleAsync(Vehicle vehicle, CancellationToken ct);
    }
}
