using Mapster;
using Microsoft.EntityFrameworkCore;
using Resume.Data.IRepositories;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Skills;
using Resume.Service.DTOs.SkillDTOs;
using Resume.Service.Exceptions;
using Resume.Service.Extentions;
using System.Linq.Expressions;
using State = Resume.Domain.Enums.EntityState;

namespace Resume.Service.Services;
public class SkillService
{
    private readonly IUnitOfWork unitOfWork;

    public SkillService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async ValueTask<Skill> CreateAsync(SkillForCreationDto skill)
    {
        Skill existSkill = await unitOfWork.Skills.GetAsync(
           s => s.Name == skill.Name
            && s.UserId == skill.UserId
            && s.State != State.Deleted);

        if (existSkill is not null)
            throw new EventException(400, "Thisskill is already exists.");

        var mappedSkill = skill.Adapt<Skill>();
        mappedSkill.Create();

        var newSkill = await unitOfWork.Skills.CreateAsync(mappedSkill);
        await unitOfWork.SaveChangesAsync();

        return newSkill;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Skill, bool>> expression)
    {
        Skill existSkill = await unitOfWork.Skills.GetAsync(expression);

        if (existSkill is null || existSkill.State == State.Deleted)
            throw new EventException(404, "This skill not found.");

        existSkill.Delete();
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async ValueTask<IEnumerable<Skill>> GetAllAsync(
        PagenationParams @params,
        Expression<Func<Skill, bool>> expression = null)
    {
        return await unitOfWork.Skills.GetAll(expression, false)
                                            .ToPagedList(@params)
                                                .ToListAsync();
    }

    public async ValueTask<Skill> GetAsync(Expression<Func<Skill, bool>> expression)
    {
        var existSkill = await unitOfWork.Skills.GetAsync(expression);

        if (existSkill is null || existSkill.State == State.Deleted)
            throw new EventException(404, "Skill not found");

        return existSkill;
    }

    public async ValueTask<Skill> UpdateAsync(long id, SkillForUpdateDto skill)
    {
        Skill existSkill = await unitOfWork.Skills.GetAsync(
            s => s.Id == id
            && s.State != State.Deleted);

        if (existSkill is null || existSkill.State == State.Deleted)
            throw new EventException(404, "Skill not found");

        Skill checkedSkill = await unitOfWork.Skills.GetAsync(
           s => s.Name == skill.Name
            && s.UserId == existSkill.UserId
            && s.State != State.Deleted);

        if (checkedSkill is not null)
            throw new EventException(400, "This skill informations already exist");

        var mappedSkill = skill.Adapt(existSkill);
        mappedSkill.Update();

        unitOfWork.Skills.Update(mappedSkill);
        await unitOfWork.SaveChangesAsync();

        return existSkill;
    }
}
