using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Skills;
using Resume.Service.DTOs.SkillDTOs;
using Resume.Service.Interfaces;

namespace Resume.Api.Controllers
{
    [Authorize]
    public class SkillsController : BaseController
    {
        private readonly ISkillService skillService;
        public SkillsController(ISkillService skillService)
        {
            this.skillService = skillService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Skill>> CreateAsync(SkillForCreationDto dto) =>
         Ok(await skillService.CreateAsync(dto));

        [HttpGet("{Id}")]
        public async ValueTask<ActionResult<Skill>> GetAsync([FromRoute(Name = "Id")] long id) =>
            Ok(await skillService.GetAsync(user => user.Id == id));

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<Skill>>> GetAllAsync([FromQuery] PagenationParams @params) =>
            Ok(await skillService.GetAllAsync(@params));

        [HttpDelete("{Id}")]
        public async ValueTask<ActionResult<bool>> DeleteAdsync([FromRoute(Name = "Id")] long id) =>
            Ok(await skillService.DeleteAsync(user => user.Id == id));

        [HttpPatch("{Id}")]
        public async ValueTask<ActionResult<Skill>> UpdateAsync([FromRoute(Name = "Id")] long id, SkillForUpdateDto dto) =>
            Ok(await skillService.UpdateAsync(id, dto));
    }
}
