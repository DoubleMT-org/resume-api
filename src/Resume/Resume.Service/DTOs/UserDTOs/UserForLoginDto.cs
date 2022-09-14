using System.ComponentModel.DataAnnotations;

namespace Resume.Service.DTOs.UserDTOs;

public class UserForLoginDto
{
    [MaxLength(64, ErrorMessage = "Email must not be less 64!")]
    [Required(ErrorMessage = "Email must not be null or empty!")]
    public string EmailOrPhone { get; set; }

    [MinLength(8, ErrorMessage = "Password must not be longer than 8")]
    [MaxLength(15, ErrorMessage = "Password must not be less 15!")]
    [Required(ErrorMessage = "Password must not be null or empty!")]
    public string Password { get; set; }

}
