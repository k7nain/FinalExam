using FinalExam.Models;
using System.ComponentModel.DataAnnotations;

namespace FinalExam.Areas.Admin.ViewModels.Place
{
    public class CreatePlaceVM
    {
        [Required(ErrorMessage = "Name is required")]
        [
            StringLength(30, ErrorMessage = "Name must be max 30 ch"),
            MinLength(2, ErrorMessage = "Name must be min 2 ch")
        ]
        public string Name { get; set; }
        [Required(ErrorMessage = "FullAddress is required")]
        [
            StringLength(30, ErrorMessage = "FullAddress must be max 30 ch"),
            MinLength(2, ErrorMessage = "FullAddress must be min 2 ch")
        ]
        public string FullAddress { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [
            StringLength(30, ErrorMessage = "Description must be max 30 ch"),
            MinLength(2, ErrorMessage = "Description must be min 2 ch")
        ]
        public string Description { get; set; }
        [Required(ErrorMessage = "CityId is required")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "ImageFile is required")]
        public IFormFile ImageFile { get; set; }
    }
}
