using Microsoft.EntityFrameworkCore;
using MyIAM.Core.Databases.Contracts;
using MyIAM.Core.Domain;
using System.Linq.Expressions;

namespace MyIAM.Persistance.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class , IAMDatabaseKey
    {
        private readonly DbSet<T> _dbSet;
        private readonly IAMDbContext _context;

        public GenericRepository(IAMDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Remove(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if(entity != null) 
                _dbSet.Remove(entity);
        }
    }
}
