using System.ComponentModel.DataAnnotations;

namespace parking_app.Models.ValueObjects
{
    public class Address
    {
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        public string PostalCode { get; set; } = string.Empty;
        [Required]
        public string HouseNumber { get; set; } = string.Empty;
    }
}
