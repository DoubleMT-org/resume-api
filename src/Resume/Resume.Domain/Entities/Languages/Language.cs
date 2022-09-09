using Resume.Domain.Commons;
using Resume.Domain.Entities.Users;
using Resume.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Resume.Domain.Entities.Languages
{
    public class Language : Auditable<long>
    {
        [MaxLength(32)]
        public string Name { get; set; }

        public LanguageLevel Level { get; set; }
        public int Persantage { get; set; }

        public long? UserId { get; set; }
        public User User { get; set; }
    }
}
