using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Resume.Data.IRepositories;
using Resume.Data.Repositories;
using Resume.Service.Helpers;
using Resume.Service.Interfaces;
using Resume.Service.Services;
using System.Runtime.CompilerServices;
using System.Text;

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

        services.AddTransient<FileHelper>();
    }

    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(p =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            p.SaveToken = true;
            p.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }

}

