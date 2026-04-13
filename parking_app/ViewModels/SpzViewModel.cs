using System.ComponentModel.DataAnnotations;

namespace parking_app.ViewModels
{
    public class SpzViewModel
    {
        [Required(ErrorMessage = "Chybí název SPZ")]
        [RegularExpression(@"^[a-zA-Z0-9]{5}$", ErrorMessage = "SPZ musí obsahovat 5 znaků")]
        public string Name { get; set; }
    }
}
