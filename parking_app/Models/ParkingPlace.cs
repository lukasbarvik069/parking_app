using parking_app.Models.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace parking_app.Models
{
    public class ParkingPlace
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int Capacity { get; set; }
        public int PricePerHour { get; set; }

        [Required]
        public Address Address { get; set; }

        public ICollection<SpzParkingPlace> SpzParkingPlaces { get; set; } = new List<SpzParkingPlace>();
    }
}
