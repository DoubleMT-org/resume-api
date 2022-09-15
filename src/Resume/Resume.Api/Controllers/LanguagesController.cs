using Microsoft.AspNetCore.Mvc;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Languages;
using Resume.Service.DTOs.LanguageDTOs;
using Resume.Service.Interfaces;

namespace Resume.Api.Controllers
{
    public class LanguagesController : BaseController
    {
        private readonly ILanguageService languageService;

        public LanguagesController(ILanguageService languageService)
        {
            this.languageService = languageService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Language>> CreateAsync(LanguageForCreationDto dto) =>
         Ok(await languageService.CreateAsync(dto));

        [HttpGet("{Id}")]
        public async ValueTask<ActionResult<Language>> GetAsync([FromRoute(Name = "Id")] long id) =>
            Ok(await languageService.GetAsync(user => user.Id == id));

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<Language>>> GetAllAsync([FromQuery] PagenationParams @params) =>
            Ok(await languageService.GetAllAsync(@params));

        [HttpDelete("{Id}")]
        public async ValueTask<ActionResult<bool>> DeleteAdsync([FromRoute(Name = "Id")] long id) =>
            Ok(await languageService.DeleteAsync(user => user.Id == id));

        [HttpPatch("{Id}")]
        public async ValueTask<ActionResult<Language>> UpdateAsync([FromRoute(Name = "Id")] long id, LanguageForUpdateDto dto) =>
            Ok(await languageService.UpdateAsync(id, dto));
    }
}
