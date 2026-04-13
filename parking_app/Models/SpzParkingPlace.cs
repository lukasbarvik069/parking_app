using parking_app.Models.ValueObjects;

namespace parking_app.Models
{
    public class SpzParkingPlace
    {
        public int Id { get; set; }

        public int SpzId { get; set; }
        public int ParkingPlaceId { get; set; }

        public Date Date { get; set; }

        public int Price { get; set; }

        public Spz? Spz { get; set; }
        public ParkingPlace? ParkingPlace { get; set; }

    }
}
