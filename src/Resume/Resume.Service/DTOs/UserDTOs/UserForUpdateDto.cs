using System.ComponentModel.DataAnnotations;

namespace Resume.Service.DTOs.UserDTOs;
public class UserForUpdateDto
{
    [MaxLength(32, ErrorMessage = "FirstName must be less than 32!")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName must not be null or empty!")]
    public string FirstName { get; set; }

    [MaxLength(32, ErrorMessage = "LastName must be less than 32!")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "LastName must not be null or empty!")]
    public string LastName { get; set; }

    [MaxLength(16, ErrorMessage = "Phone must be less than 16!")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Phone must not be null or empty!")]
    [Phone(ErrorMessage = "Phone number is not valid")]
    public string Phone { get; set; }
}
