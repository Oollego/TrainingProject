
using System.ComponentModel.DataAnnotations;

namespace TrainingProject.Domain.Entities
{
    public class Vehicle
    {
        public Guid Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;

        public int Year { get; set; }

        public int Mileage { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }


        public static Vehicle Create(string make, string model, int year, int mileage, decimal price)
        {
            if (string.IsNullOrWhiteSpace(make))
            {
                throw new ArgumentNullException(nameof(make));
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (year < 1900 || year > DateTime.UtcNow.Year + 1)
            {
                throw new ArgumentOutOfRangeException(nameof(year), "Year must be between 1900 and next year.");
            }

            if (mileage < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(mileage), "Mileage cannot be negative.");
            }

            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
            }


            return new Vehicle
            {
                Make = make,
                Model = model,
                Year = year,
                Mileage = mileage,
                Price = price,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
