using Resume.Domain.Commons;
using Resume.Domain.Entities.Attachments;
using Resume.Domain.Entities.Projects;
using Resume.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Companies;
public class Company : Auditable<long>
{
    [MaxLength(32)]
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Duration { get; set; }

    [MaxLength(32)]
    public string Position { get; set; }

    [MaxLength(32)]
    public string Location { get; set; }

    [MaxLength(64)]
    public string Url { get; set; }

    public long? UserId { get; set; }
    public User User { get; set; }

    public int AttachmentId { get; set; }

    public ICollection<Project> Projects { get; set; }
    public Attachment Attachment { get; set; }
}
