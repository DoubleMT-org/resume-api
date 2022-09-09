using Resume.Domain.Commons;
using Resume.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Skills
{
    public class Skill : Auditable<long>
    {
        [MaxLength(32)]
        public string Name { get; set; }

        public int Persantage { get; set; }

        public long? UserId { get; set; }
        public User User { get; set; }
    }
}
