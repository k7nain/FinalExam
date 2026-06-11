using System.ComponentModel.DataAnnotations;

namespace FinalExam.ViewModels.Account
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = " Email is valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
