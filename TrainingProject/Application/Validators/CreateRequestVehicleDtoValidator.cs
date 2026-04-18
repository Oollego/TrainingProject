using FluentValidation;
using TrainingProject.Application.Dto;

namespace TrainingProject.Application.Validators
{
    public class CreateRequestVehicleDtoValidator : AbstractValidator<CreateRequestVehicleDto>
    {

        public CreateRequestVehicleDtoValidator()
        {
            RuleFor(x => x.Make).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(128);
            RuleFor(x => x.Year).InclusiveBetween(1900, DateTime.UtcNow.Year + 1);
            RuleFor(x => x.Mileage).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        }
    }

    public class CreateRequestValidator: AbstractValidator<CreateRequestVehicleDto>
    {
       public CreateRequestValidator()
       {
            RuleFor(x => x.Price).GreaterThan(10).WithMessage("Price shoud be greater then 0");
       }
    }
}
