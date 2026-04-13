using System.ComponentModel.DataAnnotations;

namespace parking_app.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Zadejte email")]
        [RegularExpression(@"^.{2,}@.{3,}\..{2,}$", ErrorMessage = "Neplatný email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zadejte heslo")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Persistent { get; set; } = false;
    }
}
