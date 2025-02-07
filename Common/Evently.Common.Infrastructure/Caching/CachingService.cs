using System.Text.Json;
using Evently.Common.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace Evently.Common.Infrastructure.Caching;
public class CachingService(IDistributedCache cache) : ICachingService
{
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        byte[]? bytes = await cache.GetAsync(key, cancellationToken);   
        return bytes is null ? default :  Deserialize<T>(bytes);
    }


    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        byte[] bytes = Serialize(value);

       return cache.SetAsync(key, bytes, CacheOptions.Create(expiration), cancellationToken);
    }
    public Task RemoveAsync(string key, CancellationToken cancellationToken = default) =>
        cache.RemoveAsync(key, cancellationToken); 


    private static T Deserialize<T>(byte[] bytes)
    {
        return JsonSerializer.Deserialize<T>(bytes);
    }

    private static byte[] Serialize<T>(T value)
    {
        string jsonString = JsonSerializer.Serialize(value);
        return System.Text.Encoding.UTF8.GetBytes(jsonString);
    }
}
