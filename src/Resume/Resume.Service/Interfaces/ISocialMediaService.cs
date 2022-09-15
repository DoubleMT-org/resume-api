using Resume.Domain.Configurations;
using Resume.Domain.Entities.SocialMedias;
using Resume.Service.DTOs.AttachmentDTOs;
using Resume.Service.DTOs.SocialMediaDTOs;
using System.Linq.Expressions;

namespace Resume.Service.Interfaces;
public interface ISocialMediaService
{
    ValueTask<SocialMedia> GetAsync(Expression<Func<SocialMedia, bool>> expression);
    ValueTask<IEnumerable<SocialMedia>> GetAllAsync(PagenationParams @params, Expression<Func<SocialMedia, bool>> expression = null);
    // IFormFile Settings
    ValueTask<bool> DeleteAsync(Expression<Func<SocialMedia, bool>> expression);
    // IFormFile Settings
    ValueTask<SocialMedia> CreateAsync(SocialMediaForCreationDto socialMedia);
    // IFormFile Settings
    ValueTask<SocialMedia> UpdateAsync(long id, SocialMediaForUpdateDto socialMedia);

    ValueTask<SocialMedia> UpploadAsync(long id, AttachmentForCreationDto dto);
}
