using System.ComponentModel.DataAnnotations;

namespace FinalExam.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Username is required")]
        [
            StringLength(30, ErrorMessage = "Username must be max 30 ch"),
            MinLength(2, ErrorMessage = "Username must be min 2 ch")
        ]
        public string Username { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [
            StringLength(30, ErrorMessage = "Name must be max 30 ch"),
            MinLength(2, ErrorMessage = "Name must be min 2 ch")
        ]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        [
            StringLength(30, ErrorMessage = "Surname must be max 30 ch"),
            MinLength(2, ErrorMessage = "Surname must be min 2 ch")
        ]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = " Email is valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("Password", ErrorMessage = "Password don't match")]
        public string ConfirmPassword { get; set; }
    }
}
