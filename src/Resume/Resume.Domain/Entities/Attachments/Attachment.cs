using Resume.Domain.Entities.Companies;
using Resume.Domain.Entities.SocialMedias;
using Resume.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Attachments
{
    public class Attachment
    {
        public long Id { get; set; }

        
        [MaxLength(32)]
        public string Name { get; set; }
        
        [MaxLength(64)]
        public string Path { get; set; }

        public long? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
