using Resume.Data.DbContexts;
using Resume.Data.IRepositories;
using Resume.Domain.Entities.Attachments;
using System.Linq.Expressions;

namespace Resume.Data.Repositories;
public class AttachmentRepository : Repository<Attachment>, IAttachmentRepository
{
    public AttachmentRepository(ResumeDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> DeleteAsync(Expression<Func<Attachment, bool>> expression)
    {
        var existAttachment = await GetAsync(expression);

        if (existAttachment == null)
            return false;

        dbSet.Remove(existAttachment);

        return true;
    }
}

