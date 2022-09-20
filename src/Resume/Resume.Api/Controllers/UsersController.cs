using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Users;
using Resume.Service.DTOs.UserDTOs;
using Resume.Service.DTOs.Users;
using Resume.Service.Interfaces;

namespace Resume.Api.Controllers;

#pragma warning disable
[Authorize]
public class UsersController : BaseController
{
    private readonly IUserService userService;
    private readonly IConfiguration configuration;
    public UsersController(IUserService userService, IConfiguration configuration)
    {
        this.userService = userService;
        this.configuration = configuration;
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async ValueTask<ActionResult<User>> CreateAsync(UserForCreationDto dto) =>
         Ok(await userService.CreateAsync(dto));

    [HttpGet("{Id}/Full")]
    public async ValueTask<ActionResult<User>> GetFullyAsync([FromRoute(Name = "Id")] long id) =>
        Ok(await userService.GetFullyAsync(user => user.Id == id));

    [HttpGet("{Id}")]
    public async ValueTask<ActionResult<User>> GetAsync([FromRoute(Name = "Id")] long id) =>
        Ok(await userService.GetAsync(user => user.Id == id));

    [HttpGet, Authorize]
    public async ValueTask<ActionResult<IEnumerable<User>>> GetAllAsync([FromQuery] PagenationParams @params) =>
        Ok(await userService.GetAllAsync(@params)); 

    [HttpDelete("{Id}"), Authorize(Roles = "Admin")]
    public async ValueTask<ActionResult<bool>> DeleteAsync([FromRoute(Name = "Id")] long id) =>
        Ok(await userService.DeleteAsync(user => user.Id == id));

    [HttpGet("Full"), Authorize(Roles = "Admin")]
    public async ValueTask<ActionResult<IEnumerable<User>>> GetAllFullyAsync([FromQuery] PagenationParams @params) =>
        Ok(await userService.GetAllFullyAsync(@params));

    [HttpPost("Change/Password"), Authorize]
    public async ValueTask<ActionResult<User>> ChangePasswordAsync(UserForChangePassword dto) =>
        Ok(await userService.ChangePasswordAsync(dto));

    [HttpPut("{Id}"), Authorize(Roles = "Admin")]
    public async ValueTask<ActionResult<User>> UpdateAsync([FromRoute(Name = "Id")] long id,[FromBody] UserForUpdateDto dto) =>
        Ok(await userService.UpdateAsync( dto, id));

    [HttpPut, Authorize]
    public async ValueTask<ActionResult<User>> UpdateAsync([FromBody] UserForUpdateDto dto) =>
        Ok(await userService.UpdateAsync( user: dto));


    [HttpPost("Login")]
    [AllowAnonymous]
    public async ValueTask<ActionResult<UserTokenViewModel>> CheckLogin(UserForLoginDto dto) =>
        Ok(userService.CheckLoginAsync(dto, configuration));

}
