using System.ComponentModel.DataAnnotations;

namespace Resume.Service.DTOs.ProjectDTOs
{
    public class ProjectForCreationDto
    {
        [MaxLength(32, ErrorMessage = "Name must be less than 32!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must not be null or empty!")]
        public string Name { get; set; }

        public string Description { get; set; }

        [MaxLength(256, ErrorMessage = "Url must be less than 256!")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Url must not be null or empty!")]
        public string Url { get; set; }
    }
}
