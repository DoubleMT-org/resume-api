using Resume.Domain.Configurations;
using Resume.Domain.Entities.Users;
using Resume.Service.DTOs.Users;
using System.Linq.Expressions;

namespace Resume.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<User> GetAsync(Expression<Func<User, bool>> expression);
        ValueTask<IEnumerable<User>> GetAllAsync(PagenationParams @params, Expression<Func<User, bool>> expression = null);
        ValueTask<IEnumerable<User>> GetAllFullyAsync(PagenationParams @params, Expression<Func<User, bool>> expression = null);
        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        ValueTask<User> CreateAsync(UserForCreationDTO user);
        ValueTask<User> UpdateAsync(long id, UserForCreationDTO user);
    }
}
