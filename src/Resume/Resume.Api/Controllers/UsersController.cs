using Microsoft.AspNetCore.Mvc;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Users;
using Resume.Service.DTOs.UserDTOs;
using Resume.Service.DTOs.Users;
using Resume.Service.Interfaces;

namespace Resume.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<User>> CreateAsync(UserForCreationDto dto) =>
             Ok(await userService.CreateAsync(dto));

        [HttpGet("{Id}/Full")]
        public async ValueTask<ActionResult<User>> GetFullyAsync([FromRoute(Name = "Id")] long id) =>
            Ok(await userService.GetFullyAsync(user => user.Id == id));

        [HttpGet("{Id}")]
        public async ValueTask<ActionResult<User>> GetAsync([FromRoute(Name = "Id")] long id) =>
            Ok(await userService.GetAsync(user => user.Id == id));

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<User>>> GetAllAsync([FromQuery]PagenationParams @params) =>
            Ok(await userService.GetAllAsync(@params));

        [HttpDelete("{Id}")]
        public async ValueTask<ActionResult<bool>> DeleteAdsync([FromRoute(Name = "Id")] long id) =>
            Ok(await userService.DeleteAsync(user => user.Id == id));

        [HttpGet("Full")]
        public async ValueTask<ActionResult<IEnumerable<User>>> GetAllFullyAsync([FromQuery] PagenationParams @params) =>
            Ok(await userService.GetAllFullyAsync(@params));

        [HttpPost("Change/Password")]
        public async ValueTask<ActionResult<User>> ChangePasswordAsync(UserForChangePassword dto) =>
            Ok(await userService.ChangePasswordAsync(dto));

        [HttpPatch("{Id}")]
        public async ValueTask<ActionResult<User>> UpdateAsync([FromRoute(Name = "Id")] long id, UserForUpdateDto dto) =>
            Ok(await userService.UpdateAsync(id, dto));

        [HttpPost("Login")]
        public async ValueTask<ActionResult<UserTokenViewModel>> CheckLogin(UserForLoginDto dto) =>
            Ok(userService.CheckLoginAsync(dto));

    }
}
