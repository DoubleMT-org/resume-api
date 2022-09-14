using Resume.Domain.Configurations;
using Resume.Domain.Entities.Projects;
using Resume.Service.DTOs.ProjectDTOs;
using System.Linq.Expressions;

namespace Resume.Service.Interfaces;

public interface IProjectService
{
    ValueTask<Project> GetAsync(Expression<Func<Project, bool>> expression);
    ValueTask<IEnumerable<Project>> GetAllAsync(PagenationParams @params, Expression<Func<Project, bool>> expression = null);
    ValueTask<bool> DeleteAsync(Expression<Func<Project, bool>> expression);
    ValueTask<Project> CreateAsync(ProjectForCreationDto SocialMedia);
    ValueTask<Project> UpdateAsync(long id, ProjectForUpdateDto SocialMedia);
}
