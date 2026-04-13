using System.ComponentModel.DataAnnotations;

namespace parking_app.Models
{
    public class Spz
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public User? User { get; set; }
        public ICollection<SpzParkingPlace> SpzParkingPlaces { get; set; } = new List<SpzParkingPlace>();
    }
}
