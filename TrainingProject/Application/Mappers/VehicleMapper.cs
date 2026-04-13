using TrainingProject.Application.Dto;
using TrainingProject.Domain.Entities;

namespace TrainingProject.Application.Mappers
{
    public class VehicleMapper
    {
        public static VehicleDto MapToVehicleDto(Vehicle vehicle)
        {
            return new VehicleDto
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Mileage = vehicle.Mileage,
                Price = vehicle.Price,
                CreatedAt = vehicle.CreatedAt
            };
        }


        public static List<VehicleDto> MapToVehicleDtoList(List<Vehicle> vehicles)
        {
            return vehicles.Select(MapToVehicleDto).ToList();
        }

        public static Vehicle FromVehicleDtoToVehicle(VehicleDto vehicleDto)
        {
            return Vehicle.Create(
                vehicleDto.Make,
                vehicleDto.Model,
                vehicleDto.Year,
                vehicleDto.Mileage,
                vehicleDto.Price
            );
        }
    }
}
