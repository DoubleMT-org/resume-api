using Mapster;
using Microsoft.EntityFrameworkCore;
using Resume.Data.IRepositories;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Users;
using Resume.Service.DTOs.UserDTOs;
using Resume.Service.DTOs.Users;
using Resume.Service.Exceptions;
using Resume.Service.Extentions;
using Resume.Service.Interfaces;
using System.Linq.Expressions;
using State = Resume.Domain.Enums.EntityState;

namespace Resume.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

        public async ValueTask<User> CheckLoginAsync(UserForLoginDto dto)
        {
            User existUser = await unitOfWork.Users.GetAsync
                (user => user.Email == dto.EmailOrPhone 
                || user.Phone == dto.EmailOrPhone
                && user.State != State.Deleted);

            if (existUser is null)
                throw new EventException(404, "This email or phone is not exists");

            else if (existUser.Password != dto.Password.GetHashVersion())
                throw new EventException(400, "Password is incorrect!");

            return existUser;
        }

        public async ValueTask<User> CreateAsync(UserForCreationDto user)
        {
            User existUser = await unitOfWork.Users.GetAsync
                (u => u.Email == user.Email && u.State != State.Deleted);

            if (existUser is not null)
                throw new EventException(400, "This email is already registered!");
            
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

        public async ValueTask<User> UpdateAsync(long id, UserUpdatingDto user)
        {
            User existUser = await unitOfWork.Users.GetAsync
                (u => u.Id == id && u.State != State.Deleted);

            if (existUser is null)
                throw new EventException(404, "User not found!");

            User mappedUser = user.Adapt(existUser);
            mappedUser.Password = mappedUser.Password.GetHashVersion();
            mappedUser.Update();

            User updatedUser = unitOfWork.Users.Update(mappedUser);
            await unitOfWork.SaveChangesAsync();

            return updatedUser;
        }
    }
}
