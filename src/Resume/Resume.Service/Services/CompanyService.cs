using Mapster;
using Microsoft.EntityFrameworkCore;
using Resume.Data.IRepositories;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Attachments;
using Resume.Domain.Entities.Companies;
using Resume.Domain.Enums;
using Resume.Service.DTOs.AttachmentDTOs;
using Resume.Service.DTOs.CompanyDTOs;
using Resume.Service.Exceptions;
using Resume.Service.Extentions;
using Resume.Service.Helpers;
using Resume.Service.Interfaces;
using System.Linq.Expressions;
using State = Resume.Domain.Enums.EntityState;

namespace Resume.Service.Services;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly FileHelper fileHelpers;
    private readonly IAttachmentService attachmentService;

    public CompanyService(IUnitOfWork unitOfWork, FileHelper fileHelpers, IAttachmentService attachmentService)
    {
        this.unitOfWork = unitOfWork;
        this.fileHelpers = fileHelpers;
        this.attachmentService = attachmentService;
    }

    public async ValueTask<Company> CreateAsync(CompanyForCreationDto company)
    {
        Company existCompany = await unitOfWork.Companies.GetAsync
            (c => c.Name == company.Name
            || c.Url == company.Url
            && c.UserId == company.UserId
            && c.State != Domain.Enums.EntityState.Deleted);

        if (company is not null)
            throw new EventException(400, "This company informations is already exists");

        var mappedCompany = company.Adapt<Company>();
        mappedCompany.Create();

        var newCompany = await unitOfWork.Companies.CreateAsync(mappedCompany);

        await unitOfWork.SaveChangesAsync();

        return newCompany;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Company, bool>> expression)
    {
        Company existCompany = unitOfWork.Companies.GetAll(expression, include: "Projects").FirstOrDefault();

        if (existCompany is null)
            throw new EventException(404, "Company not found");

        for (int i = 0; i < existCompany.Projects?.Count; i++)
            existCompany.Projects.ElementAt(i).Delete();

        existCompany.Delete();
        await unitOfWork.SaveChangesAsync();


        return true;
    }

    public async ValueTask<IEnumerable<Company>> GetAllAsync(
        PagenationParams @params,
        Expression<Func<Company, bool>> expression = null)
    {
        return await unitOfWork.Companies.GetAll(expression, false)
                                            .ToPagedList(@params)
                                            .ToListAsync();
    }

    public async ValueTask<IEnumerable<Company>> GetAllFullyAsync(
        PagenationParams @params,
        Expression<Func<Company, bool>> expression = null)
    {
        return await unitOfWork.Companies.GetAll(expression, false, "Projects")
                                            .ToPagedList(@params)
                                            .ToListAsync();
    }

    public async ValueTask<Company> GetAsync(Expression<Func<Company, bool>> expression)
    {
        Company existCompany = await unitOfWork.Companies.GetAsync(expression);

        if (existCompany is null)
            throw new EventException(404, "Company not found");

        return existCompany;
    }

    public async ValueTask<Company> UpdateAsync(long id, CompanyForUpdateDto company)
    {
        Company existCompany = await unitOfWork.Companies.GetAsync(
            c => c.Id == id
            && c.State != Domain.Enums.EntityState.Deleted);

        if (existCompany is null)
            throw new EventException(404, "Company fot found.");

        Company checkedCompany = await unitOfWork.Companies.GetAsync(
            c => c.Name == company.Name
            || c.Url == company.Url
            || c.UserId == existCompany.UserId);

        if (checkedCompany is not null)
            throw new EventException(400, "This company informations is already exists.");

        var mappedCompany = company.Adapt(existCompany);
        mappedCompany.Update();

        var updatedCompany = unitOfWork.Companies.Update(mappedCompany);
        await unitOfWork.SaveChangesAsync();

        return updatedCompany;
    }

    public async ValueTask<Company> UpploadAsync(long id, AttachmentForCreationDto dto)
    {
        Company existCompany = await unitOfWork.Companies.GetAsync(
        sm => sm.Id == id
        && sm.State != State.Deleted);

        if (existCompany is null)
            throw new EventException(400, "This company not found!");

        Attachment checkingAttachment = await unitOfWork.Attachments.GetAsync(
            a => a.Id == existCompany.AttachmentId
            && a.Type == AttachmentReference.Comppany);

        if (checkingAttachment is not null)
            await attachmentService.DeleteAsync(checkingAttachment.Path);

        Attachment attachment = await attachmentService.CreateAsync(
            dto, id, AttachmentReference.Comppany);

        await unitOfWork.SaveChangesAsync();

        existCompany.Attachment = attachment;

        return existCompany;
    }
}
