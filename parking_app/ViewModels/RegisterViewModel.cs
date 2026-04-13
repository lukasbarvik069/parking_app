using System.ComponentModel.DataAnnotations;

namespace parking_app.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Zadejte email")]
        [RegularExpression(@"^.{2,}@.{3,}\..{2,}$", ErrorMessage = "Neplatný email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zadejte heslo")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Heslo musí mít aspoň 6 znaků")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hesla se neshodují")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Zadejte jméno")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Zadejte příjmení")]
        public string LName { get; set; }
    }
}
