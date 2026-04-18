using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TrainingProject.Application.Dto;
using TrainingProject.Application.Interfaces;

namespace TrainingProject.Presentation.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleService _vehicleService;
        private readonly IValidator<CreateRequestVehicleDto> _validator;

        public VehicleController(IVehicleService vehicleService, ILogger<VehicleController> logger, IValidator<CreateRequestVehicleDto> validator)
        {
            _vehicleService = vehicleService;
            _logger = logger;
            _validator = validator;
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult> GetVehicle(Guid id, CancellationToken ct)
        {
            _logger.LogInformation("Getting vehicle with id {Id}", id);


            var vehicle = await _vehicleService.GetVehicleDtoAsync(id, ct);


            if (vehicle == null)
            {
                _logger.LogWarning("Vehicle with id {Id} not found", id);
                return NotFound();
            }

            return Ok(vehicle);
        }

        [HttpGet()]
        public async Task<ActionResult> GetVehicles(CancellationToken ct, [FromQuery]int pageSize = 20, [FromQuery]int page = 0)
        {
            if(pageSize > 100) 
            {
                _logger.LogWarning("Page size {pageSize} is too large. Setting to maximum of 100.", pageSize);
                pageSize = 100;
            }

            _logger.LogInformation("Getting list of vehicles with quantity {quantity} and page {page}", pageSize, page);

            var vehicles = await _vehicleService.GetListOfVehiclesAsync(ct, pageSize, page);
            return Ok(vehicles);
        }


        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteVehicle(Guid id, CancellationToken ct)
        {
            _logger.LogInformation("Deletting vehicle by Id: {Id}", id);
        
            await _vehicleService.DeleteVehicleAsync(id, ct);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<VehicleDto>> CreateVehicle(CreateRequestVehicleDto vehicleDto, CancellationToken ct)
        {
            var result = await _validator.ValidateAsync(vehicleDto, ct);

            if(!result.IsValid)
            {
                _logger.LogWarning("Validation failed for creating vehicle: {Errors}", string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));
                return ValidationProblem(new ValidationProblemDetails(result.ToDictionary()));
            }

            _logger.LogInformation($"Creating vehicle");

            var vehicle = await _vehicleService.CreateVehicleAsync(vehicleDto, ct);

            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
        }


        [HttpPut]
        public async Task<ActionResult> UpdateVehicle(VehicleDto vehicle, CancellationToken ct)
        {
            _logger.LogInformation($"Updatting vehicle {vehicle.Id}");

            int rows = await _vehicleService.UpdateVehicleAsync(vehicle, ct);

            if (rows > 0 )
            {
                return Ok($"Rows updated {rows}");
            }

            return NotFound();
        }

    }
}
