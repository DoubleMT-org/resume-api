using Resume.Domain.Configurations;
using Resume.Domain.Entities.Educations;
using Resume.Service.DTOs.EducationDTOs;
using System.Linq.Expressions;

namespace Resume.Service.Interfaces;
public interface IEducationService
{
    ValueTask<Education> GetAsync(Expression<Func<Education, bool>> expression);
    ValueTask<IEnumerable<Education>> GetAllAsync(PagenationParams @params, Expression<Func<Education, bool>> expression = null);
    ValueTask<bool> DeleteAsync(Expression<Func<Education, bool>> expression);
    ValueTask<Education> CreateAsync(EducationForCreationDto SocialMedia);
    ValueTask<Education> UpdateAsync(long id, EducationForUpdateDto SocialMedia);
}
