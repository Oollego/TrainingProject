using TrainingProject.Domain.Entities;

namespace TrainingProject.Domain.Interfaces
{
    public interface IVehicleRepository
    {

        Task<Vehicle?> GetVehicleByIdAsync(Guid Id);

        Task<List<Vehicle>> GetVehiclesAsync(int quantity, int page);

        Task<Vehicle> CreateVehicleAsync(Vehicle vehicle);

        Task DeleteVehicleAsync(Guid Id);

        Task<int> UpdateVehicleAsync(Vehicle vehicle);
    }
}
