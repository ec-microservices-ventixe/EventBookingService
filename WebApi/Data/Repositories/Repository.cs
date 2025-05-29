using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq.Expressions;
using WebApi.Data.Interfaces;

namespace WebApi.Data.Repositories;

public class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _dbContext = context;
    private readonly DbSet<TEntity> _table = context.Set<TEntity>();
    public virtual async Task<TEntity> Create(TEntity entity)
    {
        try
        {
            await _table.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;

        } catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
    public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
    {
        return await _table.FirstOrDefaultAsync(predicate) ?? null!;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _table.ToListAsync();
    }

    public virtual async Task<TEntity> Update(TEntity entity)
    {
        if (entity is null) return null!;
        try
        {
            _table.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    public virtual async Task<bool> Delete(TEntity entity)
    {
        if (entity == null) return false;
        try
        {

            _table.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}
