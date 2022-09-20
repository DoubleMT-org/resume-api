using System.ComponentModel.DataAnnotations;

namespace Resume.Service.DTOs.UserDTOs;

public class UserForLoginDto
{
    [MaxLength(64, ErrorMessage = "Email must not be less 64!")]
    [Required(ErrorMessage = "Email must not be null or empty!")]
    public string EmailOrPhone { get; set; }

    [Required(ErrorMessage = "Password must not be null or empty!")]
    public string Password { get; set; }

}
