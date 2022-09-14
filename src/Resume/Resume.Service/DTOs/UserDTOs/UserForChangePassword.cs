using System.ComponentModel.DataAnnotations;

namespace Resume.Service.DTOs.UserDTOs;

public class UserForChangePassword
{
    [MaxLength(64, ErrorMessage = "Value must not be less 64!")]
    [Required(ErrorMessage = "Value must not be null or empty!")]
    public string EmailOrPhone { get; set; }


    [MinLength(8, ErrorMessage = "Old password must not be longer than 8")]
    [MaxLength(15, ErrorMessage = "Old password must not be less 15!")]
    [Required(ErrorMessage = "Old password must not be null or empty!")]
    public string OldPassword { get; set; }


    [MinLength(8, ErrorMessage = "New password must not be longer than 8")]
    [MaxLength(15, ErrorMessage = "New password must not be less 15!")]
    [Required(ErrorMessage = "New password must not be null or empty!")]
    public string NewPassword { get; set; }

    public string ComfirmPassword { get; set; }
}
