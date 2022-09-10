using Mapster;
using Resume.Data.IRepositories;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Languages;
using Resume.Service.DTOs.LanguageDTOs;
using Resume.Service.Exceptions;
using Resume.Service.Extentions;
using Resume.Service.Interfaces;
using System.Linq.Expressions;

namespace Resume.Service.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork unitOfWork;

        public LanguageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async ValueTask<Language> CreateAsync(LanguageForCreationDto language)
        {
            Language existLanguage = await unitOfWork.Languages
                                       .GetAsync(l => l.Name == language.Name);

            if (existLanguage is not null)
                throw new EventException(400, "This language is already exists");

            var mappedLanguage = language.Adapt<Language>();
            mappedLanguage.Create();

            var newSocialMedia = await unitOfWork.Languages.CreateAsync(mappedLanguage);
            await unitOfWork.SaveChangesAsync();

            return existLanguage;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Language, bool>> expression)
        {
            Language existLanguage = await unitOfWork.Languages.GetAsync(expression);

            if (existLanguage is null)
                throw new EventException(404, "This language not found.");

            existLanguage.Delete();
            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async ValueTask<IEnumerable<Language>> GetAllAsync(PagenationParams @params,
            Expression<Func<Language, bool>> expression = null)
        {
            return await unitOfWork.Languages.GetAll(expression, false)
                                            .ToPagedList(@params)
                                            .ToListAsync();
        }

        public async ValueTask<Language> GetAsync(Expression<Func<Language, bool>> expression)
        {
            var existLanguage = await unitOfWork.Languages.GetAsync(expression);

            if (existLanguage is null)
                throw new EventException(404, "Language not found");

            return existLanguage;
        }

        public async ValueTask<Language> UpdateAsync(long id, LanguageForCreationDto language)
        {
            Language existLanguage = await unitOfWork.Languages.GetAsync(p => p.Id == id
                                                                              && p.State != Domain.Enums.EntityState.Deleted);

            if (existLanguage is not null)
                throw new EventException(404, "Project not found");

            Language checkedLanguage = await unitOfWork.Languages.GetAsync(sm => sm.Name == language.Name
            && sm.State != Domain.Enums.EntityState.Deleted);

            if (checkedLanguage is not null)
                throw new EventException(400, "This language informations already exist");

            var mappedLanguage = language.Adapt(existLanguage);
            mappedLanguage.Update();

            unitOfWork.Languages.Update(mappedLanguage);
            await unitOfWork.SaveChangesAsync();

            return existLanguage;
        }
    }
}
