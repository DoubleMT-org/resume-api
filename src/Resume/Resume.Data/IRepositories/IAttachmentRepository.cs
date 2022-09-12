using Resume.Domain.Entities.Attachments;
using System.Linq.Expressions;

namespace Resume.Data.IRepositories;
public interface IAttachmentRepository : IRepository<Attachment>
{
    Task<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression);
}
