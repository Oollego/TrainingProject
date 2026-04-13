using TrainingProject.Application.Dto;
using TrainingProject.Domain.Entities;

namespace TrainingProject.Application.Interfaces
{
    public interface IVehicleService
    {
        Task<Vehicle> CreateVehicleAsync(CreateRequestVehicleDto vehicle);

        Task<int> UpdateVehicleAsync(VehicleDto vehicle);

        Task DeleteVehicleAsync(Guid Id);

        Task<VehicleDto?> GetVehicleDtoAsync(Guid Id);

        Task<List<VehicleDto>> GetListOfVehiclesAsync(int Quantity, int page);
    }
}
