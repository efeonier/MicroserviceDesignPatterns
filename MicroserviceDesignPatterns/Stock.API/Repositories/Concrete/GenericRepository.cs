using Microsoft.EntityFrameworkCore;
using Stock.API.Context;
using Stock.API.Entities;
using Stock.API.Repositories.Interface;
using System.Linq.Expressions;

namespace Stock.API.Repositories.Concrete;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly StockDbContext _dbContext;

    protected GenericRepository(StockDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null)
    {
        return predicate == null ? _dbContext.Set<TEntity>() : _dbContext.Set<TEntity>().Where(predicate);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().FindAsync(predicate);
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(int id, TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}