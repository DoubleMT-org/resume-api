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
    ValueTask<bool> DeleteAsync(Expression<Func<Company, bool>> expression);
    ValueTask<Company> CreateAsync(CompanyDTOs Company);
    ValueTask<Company> UpdateAsync(long id, CompanyDTOs Company);
}
