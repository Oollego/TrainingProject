using System.Runtime.CompilerServices;
using TrainingProject.Application.Dto;
using TrainingProject.Application.Interfaces;
using TrainingProject.Application.Mappers;
using TrainingProject.Domain.Entities;
using TrainingProject.Domain.Interfaces;

namespace TrainingProject.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public async Task<VehicleDto> CreateVehicleAsync(CreateRequestVehicleDto vehicle)
        {
            var newVehicle = Vehicle.Create(vehicle.Make, vehicle.Model, vehicle.Year, vehicle.Mileage, vehicle.Price);

            newVehicle = await _vehicleRepository.CreateVehicleAsync(newVehicle);

            return VehicleMapper.MapToVehicleDto(newVehicle);
        }

        public async Task DeleteVehicleAsync(Guid Id)
        {
            if(Id == Guid.Empty)
            {
                throw new ArgumentException("Guid cannot be empty");
            }

            await _vehicleRepository.DeleteVehicleAsync(Id);
        }

        public async Task<List<VehicleDto>> GetListOfVehiclesAsync(int quantity, int page)
        {
            if (quantity < 0 || page < 0)
            {
                throw new ArgumentException("Quantity and page must be non-negative.");
            }

            List<Vehicle> vehicles = await _vehicleRepository.GetVehiclesAsync(quantity, page);

            return VehicleMapper.MapToVehicleDtoList(vehicles);
        }

        public async Task<VehicleDto?> GetVehicleDtoAsync(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentException("Guid cannot be empty");
            }

            Vehicle? vehicle = await _vehicleRepository.GetVehicleByIdAsync(Id);

            return vehicle is null ? null : VehicleMapper.MapToVehicleDto(vehicle);

        }

        public async Task<int> UpdateVehicleAsync(VehicleDto vehicle)
        {


            return await _vehicleRepository.UpdateVehicleAsync(VehicleMapper.FromVehicleDtoToVehicle(vehicle));
        }
    }
}
