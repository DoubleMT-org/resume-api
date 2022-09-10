using Mapster;
using Microsoft.EntityFrameworkCore;
using Resume.Data.IRepositories;
using Resume.Domain.Configurations;
using Resume.Domain.Entities.Companies;
using Resume.Service.DTOs.CompanyDTOs;
using Resume.Service.Exceptions;
using Resume.Service.Extentions;
using Resume.Service.Interfaces;
using System.Linq.Expressions;


namespace Resume.Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async ValueTask<Company> CreateAsync(CompanyForCreationDto company)
        {
            Company existCompany = await unitOfWork.Companies.GetAsync
                (c => c.Name == company.Name
                || c.Url == company.Url);

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
            Company existCompany = unitOfWork.Companies.GetAll(expression, include: "Project").FirstOrDefault();

            if (existCompany is null)
                throw new EventException(404, "Company not found");

            for (int i = 0; i < existCompany.Projects?.Count; i++)
                existCompany.Projects.ElementAt(i).Delete();

            existCompany.Delete();
            await unitOfWork.SaveChangesAsync();


            return true;
        }

        public async ValueTask<IEnumerable<Company>> GetAllAsync(PagenationParams @params, Expression<Func<Company, bool>> expression = null)
        {
            return await unitOfWork.Companies.GetAll(expression, false)
                                                .ToPagedList(@params)
                                                .ToListAsync();
        }

        public async ValueTask<IEnumerable<Company>> GetAllFullyAsync(PagenationParams @params, Expression<Func<Company, bool>> expression = null)
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

        public async ValueTask<Company> UpdateAsync(long id, CompanyForCreationDto company)
        {
            Company existCompany = await unitOfWork.Companies.GetAsync(c => c.Id == id
                                                                        && c.State != Domain.Enums.EntityState.Deleted);

            if (existCompany is null)
                throw new EventException(404, "Company fot found");

            Company chechedCompany = await unitOfWork.Companies.GetAsync
                (c => c.Name == company.Name
                || c.Url == company.Url);

            if (company is not null)
                throw new EventException(400, "This company informations is already exists");

            var mappedCompany = company.Adapt(existCompany);
            mappedCompany.Update();

            var updatedCompany = unitOfWork.Companies.Update(mappedCompany);
            await unitOfWork.SaveChangesAsync();

            return updatedCompany;
        }
    }
}
