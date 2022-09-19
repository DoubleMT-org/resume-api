using Microsoft.AspNetCore.Mvc;
using Resume.Api.Helpers;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.SocialMedias;
using Resume.Service.DTOs.SocialMediaDTOs;
using Resume.Service.Interfaces;

namespace Resume.Api.Controllers;
public class SocialMediasController : BaseController
{
    private readonly ISocialMediaService socialMediaService;

    public SocialMediasController(ISocialMediaService socialMediaService)
    {
        this.socialMediaService = socialMediaService;
    }

    [HttpPost]
    public async ValueTask<ActionResult<SocialMedia>> CreateAsync(SocialMediaForCreationDto dto) =>
        await this.socialMediaService.CreateAsync(dto);

    [HttpGet("{Id}")]
    public async ValueTask<ActionResult<SocialMedia>> GetAsync([FromRoute(Name = "Id")] long id) =>
        await socialMediaService.GetAsync(sm => sm.Id == id);

    [HttpGet]
    public async ValueTask<ActionResult<IEnumerable<SocialMedia>>> GetAllAsync([FromQuery] PagenationParams @params) =>
            Ok(await socialMediaService.GetAllAsync(@params));

    [HttpDelete("{Id}")]
    public async ValueTask<ActionResult<bool>> DeleteAdsync([FromRoute(Name = "Id")] long id) =>
        Ok(await socialMediaService.DeleteAsync(sm => sm.Id == id));

    [HttpPatch("{Id}")]
    public async ValueTask<ActionResult<SocialMedia>> UpdateAsync([FromRoute(Name = "Id")] long id, SocialMediaForUpdateDto dto) =>
        Ok(await socialMediaService.UpdateAsync(id, dto));

    [HttpPost("Attachments/{Id}")]
    public async ValueTask<ActionResult<SocialMedia>> UploadLogoAsync([FromRoute(Name = "Id")] long id, IFormFile formFile) =>
        Ok(await socialMediaService.UpploadAsync(id, formFile.GetAsAttachment()));

}
