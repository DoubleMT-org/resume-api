using Mapster;
using Microsoft.EntityFrameworkCore;
using Resume.Data.IRepositories;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Projects;
using Resume.Service.DTOs.ProjectDTOs;
using Resume.Service.Exceptions;
using Resume.Service.Extentions;
using Resume.Service.Interfaces;
using System.Linq.Expressions;
using State = Resume.Domain.Enums.EntityState;

namespace Resume.Service.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork unitOfWork;

    public ProjectService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Project> CreateAsync(ProjectForCreationDto project)
    {
        if (project.UserId == null && project.CompanyId is null)
            throw new EventException(400, "Company id and user id mustn't be null at the same time.");

        Project existProject = await unitOfWork.Projects.GetAsync(
            p => p.Name == project.Name
            || p.Url == project.Url
            && p.UserId == project.UserId
            && p.State != State.Deleted);

        if (existProject is not null)
            throw new EventException(400, "This project is already exists.");

        var mappedProject = project.Adapt<Project>();
        mappedProject.Create();

        var newProject = await unitOfWork.Projects.CreateAsync(mappedProject);
        await unitOfWork.SaveChangesAsync();

        return newProject;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Project, bool>> expression)
    {
        Project existProject = await unitOfWork.Projects.GetAsync(expression);

        if (existProject is null || existProject.State == State.Deleted)
            throw new EventException(404, "This project not found.");

        existProject.Delete();
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async ValueTask<IEnumerable<Project>> GetAllAsync(
        PagenationParams @params,
        Expression<Func<Project, bool>> expression = null)
    {
        return await unitOfWork.Projects.GetAll(expression, false)
                                    .ToPagedList(@params)
                                    .ToListAsync();
    }

    public async ValueTask<Project> GetAsync(Expression<Func<Project, bool>> expression)
    {
        var existProject = await unitOfWork.Projects.GetAsync(expression);

        if (existProject is null || existProject.State == State.Deleted)
            throw new EventException(404, "Project not found.");

        return existProject;
    }

    public async ValueTask<Project> UpdateAsync(long id, ProjectForUpdateDto project)
    {
        Project existProject = await unitOfWork.Projects.GetAsync(
            p => p.Id == id
            && p.State != State.Deleted);

        if (existProject is not null)
            throw new EventException(404, "Project not found.");

        Project checkedProject = await unitOfWork.Projects.GetAsync(
            p => p.Name == project.Name
            || p.Url == project.Url
            && (p.UserId == existProject.UserId
            || p.CompanyId == project.CompanyId)
            && p.State != State.Deleted);

        if (checkedProject is not null)
            throw new EventException(400, "This project informations already exist");

        var mappedSocialMedia = project.Adapt(existProject);
        mappedSocialMedia.Update();

        unitOfWork.Projects.Update(mappedSocialMedia);
        await unitOfWork.SaveChangesAsync();

        return existProject;
    }
}
