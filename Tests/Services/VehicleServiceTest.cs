using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TrainingProject.Application.Dto;
using TrainingProject.Application.Services;
using TrainingProject.Domain.Entities;
using TrainingProject.Domain.Interfaces;

namespace Tests.Services
{
    public class VehicleServiceTest
    {
        private readonly Mock<IVehicleRepository> _repositoryMock;
        private readonly VehicleService _service;


        public VehicleServiceTest()
        {
            _repositoryMock = new Mock<IVehicleRepository>();
            _service = new VehicleService(_repositoryMock.Object);
        }

        // 1. GetVehicleDtoAsync — найдено
        [Fact]
        public async Task GetVehicalDtoAsync_VehicleExists_ReturnVehicleDto()
        {
            //Arrange
            var id = Guid.NewGuid();
            var vehicle = Vehicle.Create("Toyota", "Camry", 2020, 50000, 25000);
            vehicle.Id = id;

            _repositoryMock.Setup(x => x.GetVehicleByIdAsync(id, default)).ReturnsAsync(vehicle);

            //Act
            var result = await _service.GetVehicleDtoAsync(id, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Toyota", result.Make);
            Assert.Equal(id, result.Id);
        }

        // 2. GetVehicleDtoAsync — не найдено
        [Fact]
        public async Task GetVehicleDtoAsync_VehicleNotFoundReturnsNull()
        {
            //Arrange
            var id = Guid.NewGuid();
            var vehicle = Vehicle.Create("Toyota", "Camry", 2020, 50000, 25000);
            vehicle.Id = id;

            _repositoryMock.Setup(x => x.GetVehicleByIdAsync(id, default)).ReturnsAsync((Vehicle?)null);

            //Act
            var result = await _service.GetVehicleDtoAsync(id, default);

            //Assert
            Assert.Null(result);
        }

        // 3. GetVehicleDtoAsync — пустой Guid
        [Fact]
        public async Task GetVehicleDtoAsync_EmptyGuid_ThrowsArgumentException()
        {
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetVehicleDtoAsync(Guid.Empty, default));
        }

        // 4. DeleteVehicleAsync — пустой Guid
        [Fact]
        public async Task DeleteVehicleAsync_EmptyGuid_ThrowsArgumentException()
        {
            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.DeleteVehicleAsync(Guid.Empty, default));
        }

        // 5. DeleteVehicleAsync — вызывает репозиторий
        [Fact]
        public async Task DeleteVehicleAsync_ValidId_CallsRepository()
        {
            //Arrange
            var id = Guid.NewGuid();
            //Act
            await _service.DeleteVehicleAsync(id, default);
            // Assert — проверяем что репозиторий был вызван ровно 1 раз
            _repositoryMock.Verify(x => x.DeleteVehicleAsync(id, default), Times.Once);
        }

        // 6. CreateVehicleAsync — возвращает DTO
        [Fact]
        public async Task CreateVehicleAsync_ValidDto_ReturnsVehicleDto() 
        {
            //Arrange
            var dto = new CreateRequestVehicleDto
            {
                Make = "Bmw",
                Model = "X5",
                Year = 2021,
                Mileage = 30000,
                Price = 45000
            };

            _repositoryMock
                .Setup(x => x.CreateVehicleAsync(It.IsAny<Vehicle>(), default))
                .ReturnsAsync((Vehicle vehicle, CancellationToken _) => vehicle);

            //Act
            var result = await _service.CreateVehicleAsync(dto, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Bmw", result.Make);
            Assert.Equal("X5", result.Model);

        }
    }
}
