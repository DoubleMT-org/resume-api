using Resume.Domain.Commons;
using Resume.Domain.Entities.Attachments;
using Resume.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.SocialMedias
{
    public class SocialMedia : Auditable<long>
    {
        [MaxLength(32)]
        public string Name { get; set; }
        public string Url { get; set; }
        
        public int? AttachmentId { get; set; }

        public long? UserId { get; set; }
        public User User { get; set; }

        public Attachment Attachment { get; set; }
    }
}
