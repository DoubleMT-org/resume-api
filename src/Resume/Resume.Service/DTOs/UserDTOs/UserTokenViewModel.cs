namespace Resume.Service.DTOs.UserDTOs;

public class UserTokenViewModel
{
    public string Token { get; set; }

    public UserTokenViewModel(string token)
    {
        Token = token;
    }
}
