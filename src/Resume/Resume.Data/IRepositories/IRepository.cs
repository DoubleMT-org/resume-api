using Resume.Domain.Entities.Languages;
using System.Linq.Expressions;

namespace Resume.Data.IRepositories
{
    public interface IRepository<TSourse>
        where TSourse : class
    {
        Task<TSourse> GetAsync(Expression<Func<TSourse, bool>> expression);
        IQueryable<TSourse> GetAll(Expression<Func<TSourse, bool>> expression = null, bool isTracking = true, string include = null);
        Task<TSourse> CreateAsync(TSourse extity);
        TSourse Update(TSourse extity);
        Task<bool> DeleteAsync(Expression<Func<TSourse, bool>> expression);
    }
}
