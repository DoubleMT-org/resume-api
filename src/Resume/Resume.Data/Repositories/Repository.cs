using Microsoft.EntityFrameworkCore;
using Resume.Data.DbContexts;
using Resume.Data.IRepositories;
using System.Linq.Expressions;

namespace Resume.Data.Repositories
{
    public class Repository<TSourse> : IRepository<TSourse>
        where TSourse : class
    {
        private readonly ResumeDbContext dbContext;
        private readonly DbSet<TSourse> dbSet;

        public Repository(ResumeDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TSourse>();
        }

        public async Task<TSourse> CreateAsync(TSourse extity)
            => (await dbSet.AddAsync(extity)).Entity;

        public async Task<bool> DeleteAsync(Expression<Func<TSourse, bool>> expression)
        {
            var existEntity = await GetAsync(expression);

            if (existEntity is null)
                return false;

            dbSet.Remove(existEntity);
            return true;
        }

        public IQueryable<TSourse> GetAll(Expression<Func<TSourse, bool>> expression = null, bool isTracking = true, string include = null)
        {
            IQueryable<TSourse> query = expression is null ? dbSet : dbSet.Where(expression);

            if (!isTracking)
                query = query.AsNoTracking();

            if (include is not null)
                query = query.Include(include);

            return query;
        }

        public Task<TSourse> GetAsync(Expression<Func<TSourse, bool>> expression)
            => dbSet.FirstOrDefaultAsync(expression);

        public TSourse Update(TSourse extity)
            => dbSet.Update(extity).Entity;
    }
}
