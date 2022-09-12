using Mapster;
using Microsoft.EntityFrameworkCore;
using Resume.Data.IRepositories;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Educations;
using Resume.Service.DTOs.EducationDTOs;
using Resume.Service.Exceptions;
using Resume.Service.Extentions;
using Resume.Service.Interfaces;
using System.Linq.Expressions;
using State = Resume.Domain.Enums.EntityState;

namespace Resume.Service.Services;
public class EducationService : IEducationService
{
    private readonly IUnitOfWork unitOfWork;

    public EducationService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async ValueTask<Education> CreateAsync(EducationForCreationDto education)
    {
        Education existEducation = await unitOfWork.Educations.GetAsync(
            l => l.Name == education.Name
            && l.UserId == education.UserId
            && l.State != State.Deleted);

        if (existEducation is not null)
            throw new EventException(400, "This education is already exists.");

        var mappedEducation = education.Adapt<Education>();
        mappedEducation.Create();

        var newEducation = await unitOfWork.Educations.CreateAsync(mappedEducation);
        await unitOfWork.SaveChangesAsync();

        return newEducation;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Education, bool>> expression)
    {
        Education existEducation = await unitOfWork.Educations.GetAsync(expression);

        if (existEducation is null || existEducation.State == State.Deleted)
            throw new EventException(404, "This education not found.");

        existEducation.Delete();
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async ValueTask<IEnumerable<Education>> GetAllAsync(
        PagenationParams @params,
        Expression<Func<Education, bool>> expression = null)
    {
        return await unitOfWork.Educations.GetAll(expression, false)
                                            .ToPagedList(@params)
                                                .ToListAsync();
    }

    public async ValueTask<Education> GetAsync(Expression<Func<Education, bool>> expression)
    {
        var existEducation = await unitOfWork.Educations.GetAsync(expression);

        if (existEducation is null || existEducation.State == State.Deleted)
            throw new EventException(404, "Education not found");

        return existEducation;
    }

    public async ValueTask<Education> UpdateAsync(long id, EducationForUpdateDto education)
    {
        Education existEducation = await unitOfWork.Educations.GetAsync(
            e => e.Id == id
            && e.State != State.Deleted);

        if (existEducation is null || existEducation.State == State.Deleted)
            throw new EventException(404, "Education not found");

        Education checkedEducation = await unitOfWork.Educations.GetAsync(
            e => e.Name == education.Name
            && e.UserId == existEducation.UserId
            && e.State != State.Deleted);

        if (checkedEducation is not null)
            throw new EventException(400, "This education informations already exist");

        var mappedEducation = education.Adapt(existEducation);
        mappedEducation.Update();

        unitOfWork.Educations.Update(mappedEducation);
        await unitOfWork.SaveChangesAsync();

        return existEducation;
    }
}
