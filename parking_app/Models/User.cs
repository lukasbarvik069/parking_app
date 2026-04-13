using Microsoft.AspNetCore.Identity;
using parking_app.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace parking_app.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string FName { get; set; } = String.Empty;
        [Required]
        public string LName { get; set; } = String.Empty;

        public ICollection<Spz> Spzs { get; set; } = new List<Spz>();
    }
}
