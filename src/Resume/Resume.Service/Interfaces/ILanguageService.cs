using Resume.Domain.Configurations;
using Resume.Domain.Entities.Languages;
using Resume.Service.DTOs.LanguageDTOs;
using System.Linq.Expressions;

namespace Resume.Service.Interfaces
{
    public interface ILanguageService
    {
        ValueTask<Language> GetAsync(Expression<Func<Language, bool>> expression);
        ValueTask<IEnumerable<Language>> GetAllAsync(PagenationParams @params, Expression<Func<Language, bool>> expression = null);
        // IFormFile Settings
        ValueTask<bool> DeleteAsync(Expression<Func<Language, bool>> expression);
        // IFormFile Settings
        ValueTask<Language> CreateAsync(LanguageForCreationDto SocialMedia);
        // IFormFile Settings
        ValueTask<Language> UpdateAsync(long id, LanguageForCreationDto SocialMedia);
    }
}
