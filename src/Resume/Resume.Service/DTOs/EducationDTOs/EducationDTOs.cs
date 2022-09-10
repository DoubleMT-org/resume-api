using System.ComponentModel.DataAnnotations;

namespace Resume.Service.DTOs.EducationDTOs
{
    public class EducationDTOs
    {
        [MaxLength(32, ErrorMessage = "Name must be less than 32!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must not be null or empty!")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Duration must not be null or empty!")]
        public string Duration { get; set; }

        [MaxLength(32, ErrorMessage = "Faculty must be less than 32!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Faculty must not be null or empty!")]
        public string Faculty { get; set; }

        [MaxLength(32, ErrorMessage = "Location must be less than 32!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Location must not be null or empty!")]
        public string Location { get; set; }

        [MaxLength(256, ErrorMessage = "Url must be less than 256!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Url must not be null or empty!")]
        public string Url { get; set; }
    }
}
