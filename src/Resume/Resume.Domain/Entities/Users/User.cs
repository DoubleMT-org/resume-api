using Resume.Domain.Commons;
using Resume.Domain.Entities.Companies;
using Resume.Domain.Entities.Educations;
using Resume.Domain.Entities.Languages;
using Resume.Domain.Entities.Projects;
using Resume.Domain.Entities.Skills;
using Resume.Domain.Entities.SocialMedias;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Resume.Domain.Entities.Users
{
    public class User : Auditable<long>
    {
        [MaxLength(32), Required]
        public string FirstName { get; set; }

        [MaxLength(32), Required]
        public string LastName { get; set; }

        [MaxLength(64), Required]
        public string Email { get; set; }

        [MaxLength(16), Required]
        public string Phone { get; set; }

        [MaxLength(256), Required, JsonIgnore]
        public string Password { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public ICollection<Language> Languages { get; set; }

        public ICollection<SocialMedia> SocialMedias { get; set; }
        public ICollection<Company> Companies { get; set; }

        public ICollection<Project> Projects { get; set; }
        public ICollection<Education> Educations { get; set; }
    }
}
