using Resume.Domain.Configurations;
using Resume.Domain.Entities.Skills;
using Resume.Service.DTOs.SkillDTOs;
using System.Linq.Expressions;

namespace Resume.Service.Interfaces;
public interface ISkillService 
{
    ValueTask<Skill> GetAsync(Expression<Func<Skill, bool>> expression);
    ValueTask<IEnumerable<Skill>> GetAllAsync(
        PagenationParams @params,
        Expression<Func<Skill, bool>> expression = null);
    ValueTask<bool> DeleteAsync(Expression<Func<Skill, bool>> expression);
    ValueTask<Skill> CreateAsync(SkillForCreationDto skill);
    ValueTask<Skill> UpdateAsync(long id, SkillForUpdateDto skill);
}
