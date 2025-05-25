using Microsoft.Azure.Cosmos;
using System.Diagnostics;
using WebApi.Data.Interfaces;

namespace WebApi.Data.Repositories;

public abstract class Repository<TModel> : IRepository<TModel> where TModel : class
{
    public readonly IConfiguration _configuration;
    public CosmosClient _client;
    public Database _db;
    public Container _container;
    public Repository(IConfiguration configuration)
    {
        _configuration = configuration;
        _client = new(_configuration["CosmosDb:Endpoint"], _configuration["CosmosDb:PrimaryKey"]);
        _db = _client.GetDatabase(_configuration["CosmosDb:Database"]);
        _container = _db.GetContainer("bookings");
    }

    public virtual async Task<TModel> CreateAsync(TModel model)
    {
        try
        {
            ItemResponse<TModel> response = await _container.CreateItemAsync(model);
            return response;
        } catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    public virtual async Task<bool> DeleteAsync(string id, string partitionKey)
    {
        try
        {
            await _container.DeleteItemAsync<TModel>(id, new PartitionKey(partitionKey));
            return true;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
    }

    public virtual async Task<IEnumerable<TModel>> GetAllAsync(Func<TModel, bool>? predicate)
    {
        try
        {
            IList<TModel> items = [];

            FeedIterator<TModel> feedIterator = _container.GetItemQueryIterator<TModel>();

            while (feedIterator.HasMoreResults)
            {
                FeedResponse<TModel> response = await feedIterator.ReadNextAsync();
                foreach (TModel item in response)
                {
                    items.Add(item);
                }
            }

            if(predicate != null)
                items = items.Where(predicate).ToList();

            return items;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    public virtual async Task<TModel> GetAsync(string key, string partitionKey)
    {
        try
        {
            ItemResponse<TModel> response = await _container.ReadItemAsync<TModel>(key, new PartitionKey(partitionKey));
            return response;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }

    public virtual async Task<TModel> UpdateAsync(TModel entity, string id, string partitionKey)
    {
        try
        {
            ItemResponse<TModel> response = await _container.ReplaceItemAsync<TModel>(entity, id, new PartitionKey(partitionKey));
            return response;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null!;
        }
    }
}
