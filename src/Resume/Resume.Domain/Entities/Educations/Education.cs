using Resume.Domain.Commons;
using Resume.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Educations;
public class Education : Auditable<long>
{ 
    [MaxLength(32)]
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Duration { get; set; }

    [MaxLength(32)]
    public string Faculty { get; set; }
   
    [MaxLength(32)]
    public string Location { get; set; }

    [MaxLength(32)]
    public string Url { get; set; }

    public long? UserId { get; set; }
    public User User { get; set; }
}

