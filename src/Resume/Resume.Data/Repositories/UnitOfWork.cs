using Resume.Data.DbContexts;
using Resume.Data.IRepositories;
using Resume.Domain.Entities.Attachments;
using Resume.Domain.Entities.Companies;
using Resume.Domain.Entities.Educations;
using Resume.Domain.Entities.Languages;
using Resume.Domain.Entities.Projects;
using Resume.Domain.Entities.Skills;
using Resume.Domain.Entities.SocialMedias;
using Resume.Domain.Entities.Users;

namespace Resume.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAttachmentRepository Attachments { get; }

        public IRepository<Company> Companies { get; }

        public IRepository<Education> Educations { get; }

        public IRepository<Language> Languages { get; }

        public IRepository<Project> Projects { get; }
        public IRepository<Skill> Skills { get; }
        public IRepository<SocialMedia> SocialMedias { get; }
        public IRepository<User> Users { get; }

        private ResumeDbContext dbContext;

        public UnitOfWork(ResumeDbContext dbContext)
        {
            this.dbContext = dbContext;

            Attachments = new AttachmentRepository(dbContext);
            Companies = new Repository<Company>(dbContext);
            Educations = new Repository<Education>(dbContext);
            Languages = new Repository<Language>(dbContext);
            Projects = new Repository<Project>(dbContext);
            Users = new Repository<User>(dbContext);
            Skills = new Repository<Skill>(dbContext);
            SocialMedias = new Repository<SocialMedia>(dbContext);
        }

        public async ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
