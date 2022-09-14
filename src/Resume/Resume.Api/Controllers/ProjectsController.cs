using Microsoft.AspNetCore.Mvc;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Projects;
using Resume.Service.DTOs.ProjectDTOs;
using Resume.Service.Interfaces;

namespace Resume.Api.Controllers
{
    public class ProjectsController : BaseController
    {
        private readonly IProjectService projectService;
        [HttpPost]
        public async ValueTask<ActionResult<Project>> CreateAsync(ProjectForCreationDto dto) =>
         Ok(await projectService.CreateAsync(dto));

        [HttpGet("{Id}")]
        public async ValueTask<ActionResult<Project>> GetAsync([FromRoute(Name = "Id")] long id) =>
            Ok(await projectService.GetAsync(user => user.Id == id));

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<Project>>> GetAllAsync([FromQuery] PagenationParams @params) =>
            Ok(await projectService.GetAllAsync(@params));

        [HttpDelete("{Id}")]
        public async ValueTask<ActionResult<bool>> DeleteAdsync([FromRoute(Name = "Id")] long id) =>
            Ok(await projectService.DeleteAsync(user => user.Id == id));

        [HttpPatch("{Id}")]
        public async ValueTask<ActionResult<Project>> UpdateAsync([FromRoute(Name = "Id")] long id, ProjectForUpdateDto dto) =>
            Ok(await projectService.UpdateAsync(id, dto));
    }
}
