using Resume.Data.IRepositories;
using Resume.Data.Repositories;
using Resume.Service.Helpers;
using Resume.Service.Interfaces;
using Resume.Service.Services;

namespace Resume.Api.Extentions;
public static class CollectionServiceExtentions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAttachmentService, AttachmentService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IEducationService, EducationService>();
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<ISocialMediaService, SocialMediaService>();
        services.AddScoped<IUserService, UserService>();

        services.AddTransient<FileHelpers>();
    }

}

