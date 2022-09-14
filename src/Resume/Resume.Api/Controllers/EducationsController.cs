using Microsoft.AspNetCore.Mvc;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Educations;
using Resume.Service.DTOs.EducationDTOs;
using Resume.Service.Interfaces;

namespace Resume.Api.Controllers
{
    public class EducationsController : BaseController
    {
        private readonly IEducationService educationService;
        [HttpPost]
        public async ValueTask<ActionResult<Education>> CreateAsync(EducationForCreationDto dto) =>
         Ok(await educationService.CreateAsync(dto));

        [HttpGet("{Id}")]
        public async ValueTask<ActionResult<Education>> GetAsync([FromRoute(Name = "Id")] long id) =>
            Ok(await educationService.GetAsync(user => user.Id == id));

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<Education>>> GetAllAsync([FromQuery] PagenationParams @params) =>
            Ok(await educationService.GetAllAsync(@params));

        [HttpDelete("{Id}")]
        public async ValueTask<ActionResult<bool>> DeleteAdsync([FromRoute(Name = "Id")] long id) =>
            Ok(await educationService.DeleteAsync(user => user.Id == id));

        [HttpPatch("{Id}")]
        public async ValueTask<ActionResult<Education>> UpdateAsync([FromRoute(Name = "Id")] long id, EducationForUpdateDto dto) =>
            Ok(await educationService.UpdateAsync(id, dto));
    }
}
