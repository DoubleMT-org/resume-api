using System.ComponentModel.DataAnnotations;

namespace Resume.Service.DTOs.SocialMediaDTOs;
public class SocialMediaForUpdateDto
{
    [MaxLength(32, ErrorMessage = "Name must be less than 32!")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name must not be null or empty!")]
    public string Name { get; set; }

    [MaxLength(32, ErrorMessage = "Url must be less than 32!")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Url must not be null or empty!")]
    public string Url { get; set; }
}
