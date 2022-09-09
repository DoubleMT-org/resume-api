using Resume.Domain.Commons;
using Resume.Domain.Entities.Companies;
using Resume.Domain.Entities.Users;

namespace Resume.Domain.Entities.Projects
{
    public class Project : Auditable<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public long? CompanyId { get; set; }
        public Company Company { get; set; }

        public long? UserId { get; set; }
        public User User { get; set; }
    }
}
