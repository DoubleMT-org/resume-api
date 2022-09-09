using Resume.Domain.Entities.Attachments;
using Resume.Domain.Entities.Companies;
using Resume.Domain.Entities.Educations;
using Resume.Domain.Entities.Languages;
using Resume.Domain.Entities.Projects;
using Resume.Domain.Entities.Skills;
using Resume.Domain.Entities.SocialMedias;
using Resume.Domain.Entities.Users;

namespace Resume.Data.IRepositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<Attachment> Attachments { get; }
        IRepository<Company> Companies { get; }
        IRepository<Education> Educations { get; }
        IRepository<Language> Languages { get; }
        IRepository<Project> Projects { get; }
        IRepository<Skill> Skills { get; }
        IRepository<SocialMedia> SocialMedias { get; }
        IRepository<User> Users { get; }

        Task SaveChangesAsync();


    }
}
