using Resume.Domain.Configurations;
using Resume.Domain.Entities.Users;
using System.Linq.Expressions;

namespace Resume.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<User> GetAsync(Expression<Func<User, bool>> expression);
        ValueTask<IEnumerable<User>> GetAllAsync(PagenationParams @params, Expression<Func<User, bool>> expression = null);
        ValueTask<IEnumerable<User>> GetAllFullyAsync(PagenationParams @params, Expression<Func<User, bool>> expression = null);
        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
    }
}
