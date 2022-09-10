using System.ComponentModel.DataAnnotations;

namespace Resume.Service.DTOs.CompanyDTOs
{
    //IFormFile Settings
    public class CompanyForCreationDto
    {
        [MaxLength(32, ErrorMessage = "FirstName must be less than 32!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName must not be null or empty!")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Duration must not be null or empty!")]
        public string Duration { get; set; }


        [MaxLength(32, ErrorMessage = "Position must be less than 32!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Position must not be null or empty!")]
        public string Position { get; set; }


        [MaxLength(32, ErrorMessage = "Location must be less than 32!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Location must not be null or empty!")]
        public string Location { get; set; }

        [MaxLength(256, ErrorMessage = "Url must be less than 256!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Url must not be null or empty!")]
        public string Url { get; set; }
    }
}
