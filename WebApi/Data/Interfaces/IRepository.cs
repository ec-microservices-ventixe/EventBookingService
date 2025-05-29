using System.Linq.Expressions;

namespace WebApi.Data.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    public Task<TEntity> Create(TEntity entity);

    public Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

    public Task<IEnumerable<TEntity>> GetAll();

    public Task<TEntity> Update(TEntity entity);

    public Task<bool> Delete(TEntity entity);
}
