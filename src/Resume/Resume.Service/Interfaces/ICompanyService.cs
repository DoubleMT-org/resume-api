using Resume.Domain.Configurations;
using Resume.Domain.Entities.Companies;
using Resume.Service.DTOs.CompanyDTOs;
using System.Linq.Expressions;

namespace Resume.Service.Interfaces;
public interface ICompanyService
{
    ValueTask<Company> GetAsync(Expression<Func<Company, bool>> expression);
    ValueTask<IEnumerable<Company>> GetAllAsync(PagenationParams @params, Expression<Func<Company, bool>> expression = null);
    ValueTask<IEnumerable<Company>> GetAllFullyAsync(PagenationParams @params, Expression<Func<Company, bool>> expression = null);

    // IFormFile Settings
    ValueTask<bool> DeleteAsync(Expression<Func<Company, bool>> expression);

    // IFormFile Settings
    ValueTask<Company> CreateAsync(CompanyForCreationDto Company);

    // IFormFile Settings
    ValueTask<Company> UpdateAsync(long id, CompanyForCreationDto Company);
}
