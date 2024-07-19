using Payment.API.Entities;
using System.Linq.Expressions;

namespace Payment.API.Repositories.Interface;

public interface IGenericRepository<TEntity> where TEntity : class, IEntity
{
    IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(int id, TEntity entity);
    Task DeleteAsync(int id);
}