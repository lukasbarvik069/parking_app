using parking_app.Models.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace parking_app.ViewModels
{
    public class BuySpotViewModel
    {
        [Required(ErrorMessage = "Vyberte SPZ")]
        public int SpzId { get; set; }

        public int Duration { get; set; } = 1;
        public int Price { get; set; }
    }
}
