using System.ComponentModel.DataAnnotations;

namespace FinalExam.Areas.Admin.ViewModels.City
{
    public class CreateCityVM
    {
        [Required(ErrorMessage = "Name is required")]
        [
            StringLength(30, ErrorMessage = "Name must be max 30 ch"),
            MinLength(2, ErrorMessage = "Name must be min 2 ch")
        ]
        public string Name { get; set; }
    }
}
