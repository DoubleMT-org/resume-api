using System.ComponentModel.DataAnnotations;

namespace Resume.Service.DTOs.SkillDTOs;
public class SkillForUpdateDto
{
    [MaxLength(32, ErrorMessage = "Name must be less than 32!")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name must not be null or empty!")]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Persantage must not be null or empty!")]
    public int Persantage { get; set; }
}
