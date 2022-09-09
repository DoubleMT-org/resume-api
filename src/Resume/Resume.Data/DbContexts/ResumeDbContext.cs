using Microsoft.EntityFrameworkCore;
using Resume.Domain.Entities.Attachments;
using Resume.Domain.Entities.Companies;
using Resume.Domain.Entities.Educations;
using Resume.Domain.Entities.Languages;
using Resume.Domain.Entities.Projects;
using Resume.Domain.Entities.Skills;
using Resume.Domain.Entities.SocialMedias;
using Resume.Domain.Entities.Users;

namespace Resume.Data.DbContexts
{
    public class ResumeDbContext : DbContext
    {
        public ResumeDbContext(DbContextOptions<ResumeDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SocialMedia> SocialMedias { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
