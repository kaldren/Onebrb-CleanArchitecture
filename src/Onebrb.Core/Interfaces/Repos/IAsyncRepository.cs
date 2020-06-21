using Onebrb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onebrb.Core.Interfaces.Repos
{
    public interface IAsyncRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(TPrimaryKey id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(object obj);
        Task<int> SaveChangesAsync();
    }
}
