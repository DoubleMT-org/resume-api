using Resume.Domain.Entities.Users;
using Resume.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Attachments
{
    public class Attachment
    {
        public long Id { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(128)]
        public string Path { get; set; }

        public long? EntityId { get; set; }
        public AttachmentReference Type { get; set; }
    }
}
