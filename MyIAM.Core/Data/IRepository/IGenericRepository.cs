using MyIAM.Core.Domain;
using System.Linq.Expressions;

namespace MyIAM.Core.Databases.Contracts
{
    public interface IGenericRepository<T> where T : class , IAMDatabaseKey
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate);
        Task InsertAsync(T entity);
        Task InsertRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        Task Remove(Guid id);
    }
}
