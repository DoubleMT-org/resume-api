using Resume.Domain.Configurations;
using Resume.Domain.Entities.Users;
using Resume.Service.DTOs.UserDTOs;
using Resume.Service.DTOs.Users;
using System.Linq.Expressions;

namespace Resume.Service.Interfaces
{
    public interface IUserService
    {
        ValueTask<User> GetAsync(Expression<Func<User, bool>> expression);
        ValueTask<IEnumerable<User>> GetAllAsync(PagenationParams @params, Expression<Func<User, bool>> expression = null);
        ValueTask<IEnumerable<User>> GetAllFullyAsync(PagenationParams @params, Expression<Func<User, bool>> expression = null);
        ValueTask<User> GetFullyAsync(Expression<Func<User, bool>> expression);

        ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        ValueTask<User> CreateAsync(UserForCreationDto user);
        ValueTask<User> UpdateAsync(long id, UserForUpdateDto user);

        ValueTask<User> ChangePasswordAsync(UserForChangePassword dto);
        ValueTask<UserTokenViewModel> CheckLoginAsync(UserForLoginDto dto);
    }
}
