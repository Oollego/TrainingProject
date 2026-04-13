namespace TrainingProject.Application.Dto
{
    public class CreateRequestVehicleDto
    {
        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }

        public int Mileage { get; set; }

        public decimal Price { get; set; }

    }
}
