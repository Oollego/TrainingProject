using TrainingProject.Application.Dto;
using TrainingProject.Domain.Entities;

namespace TrainingProject.Application.Interfaces
{
    public interface IVehicleService
    {
        Task<VehicleDto> CreateVehicleAsync(CreateRequestVehicleDto vehicle, CancellationToken ct);

        Task<int> UpdateVehicleAsync(VehicleDto vehicle, CancellationToken ct);

        Task DeleteVehicleAsync(Guid Id, CancellationToken ct);

        Task<VehicleDto?> GetVehicleDtoAsync(Guid Id, CancellationToken ct);

        Task<List<VehicleDto>> GetListOfVehiclesAsync(CancellationToken ct, int Quantity, int page);
    }
}
