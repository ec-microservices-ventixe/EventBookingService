namespace WebApi.Data.Interfaces;

public interface IRepository<TModel> where TModel : class
{
    public Task<TModel> CreateAsync(TModel entity);

    public Task<TModel> GetAsync(string id, string partitionKey);

    public Task<IEnumerable<TModel>> GetAllAsync(Func<TModel, bool>? predicate);

    public Task<TModel> UpdateAsync(TModel entity, string key, string partitionKey);

    public Task<bool> DeleteAsync(string id, string partitionKey);
}