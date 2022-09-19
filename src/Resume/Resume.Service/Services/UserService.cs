using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Resume.Data.IRepositories;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Users;
using Resume.Service.DTOs.UserDTOs;
using Resume.Service.DTOs.Users;
using Resume.Service.Exceptions;
using Resume.Service.Extentions;
using Resume.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using State = Resume.Domain.Enums.EntityState;

namespace Resume.Service.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IConfiguration configuration;

    public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        this.unitOfWork = unitOfWork;
        this.configuration = configuration;
    }

    public async ValueTask<User> ChangePasswordAsync(UserForChangePassword dto)
    {
        User existUser = await unitOfWork.Users.GetAsync
            (user => user.Email == dto.EmailOrPhone
            || user.Phone == dto.EmailOrPhone
            && user.State != State.Deleted);

        if (existUser is null)
            throw new EventException(404, "This email or phone is not exists");

        else if (dto.NewPassword != dto.ComfirmPassword)
            throw new EventException(400, "New password and comfirm password is not equal");

        else if (!dto.NewPassword.IsValidPassword(out string errorMessage))
            throw new EventException(400, errorMessage);

        else if (existUser.Password != dto.OldPassword.GetHashVersion())
            throw new EventException(400, "Password is incorrect!");

        existUser.Password = dto.NewPassword.GetHashVersion();
        await unitOfWork.SaveChangesAsync();

        return existUser;
    }

    public async ValueTask<UserTokenViewModel> CheckLoginAsync(UserForLoginDto dto)
    {
        User existUser = await unitOfWork.Users.GetAsync
            (user => user.Email == dto.EmailOrPhone
            || user.Phone == dto.EmailOrPhone
            && user.State != State.Deleted);

        if (existUser is null)
            throw new EventException(404, "This email or phone is not exists");

        else if (existUser.Password != dto.Password.GetHashVersion())
            throw new EventException(400, "Password is incorrect!");

        #region Else we generate JSON Web Token

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", existUser.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        #endregion

        return new UserTokenViewModel(tokenHandler.WriteToken(token));
    }

    public async ValueTask<User> CreateAsync(UserForCreationDto user)
    {
        User existUser = await unitOfWork.Users.GetAsync
            (u => u.Email == user.Email
            || u.Phone == user.Phone
            && u.State != State.Deleted);

        if (existUser is not null)
            throw new EventException(400, "This email or phone is already registered!");

        else if (!user.Password.IsValidPassword(out string errorMessage))
            throw new EventException(400, errorMessage);

        User mappedUser = user.Adapt<User>();
        mappedUser.Password = mappedUser.Password.GetHashVersion();
        mappedUser.Create();

        User newUser = await unitOfWork.Users.CreateAsync(mappedUser);
        await unitOfWork.SaveChangesAsync();

        return newUser;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<User, bool>> expression)
    {
        var existUser = await unitOfWork.Users.GetAsync(expression);

        if (existUser is null || existUser.State == State.Deleted)
            throw new EventException(404, "User not found");

        existUser.Delete();

        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async ValueTask<IEnumerable<User>> GetAllAsync
        (PagenationParams @params, Expression<Func<User, bool>> expression = null)
    {
        return await unitOfWork.Users.GetAll(expression, false, "Languages")
            .ToPagedList(@params)
            .ToListAsync();
    }

    public async ValueTask<IEnumerable<User>> GetAllFullyAsync
        (PagenationParams @params, Expression<Func<User, bool>> expression = null)
    {
        return await unitOfWork.Users.GetAll(expression, false)
            .Include(user => user.Companies)
            .ThenInclude(company => company.Projects)
            .Include(user => user.Projects)
            .Include(user => user.Educations)
            .Include(user => user.Languages)
            .Include(user => user.Skills)
            .ToPagedList(@params)
            .ToListAsync();
    }

    public async ValueTask<User> GetAsync(Expression<Func<User, bool>> expression)
    {
        var existUser = await unitOfWork.Users.GetAsync(expression);

        if (existUser is null || existUser.State == State.Deleted)
            throw new EventException(404, "User not found");

        return existUser;
    }

    public async ValueTask<User> GetFullyAsync(Expression<Func<User, bool>> expression)
    {
        return await unitOfWork.Users.GetAll(expression, false)
            .Include(user => user.Companies)
            .ThenInclude(company => company.Projects)
            .Include(user => user.Projects)
            .Include(user => user.Educations)
            .Include(user => user.Languages)
            .Include(user => user.Skills)
            .Include(user => user.SocialMedias)
            .FirstOrDefaultAsync();
    }

    public async ValueTask<User> UpdateAsync(long id, UserForUpdateDto user)
    {
        User existUser = await unitOfWork.Users.GetAsync
            (u => u.Id == id && u.State != State.Deleted);
        if (existUser is null)
            throw new EventException(404, "User not found!");

        User checkedUser = await unitOfWork.Users.GetAsync
            (u => u.Phone == user.Phone && u.State != State.Deleted);
        if (checkedUser is not null)
            throw new EventException(400, "This phone number is already exist.");


        User mappedUser = user.Adapt(existUser);
        mappedUser.Password = mappedUser.Password.GetHashVersion();
        mappedUser.Update();

        User updatedUser = unitOfWork.Users.Update(mappedUser);
        await unitOfWork.SaveChangesAsync();

        return updatedUser;
    }
}
