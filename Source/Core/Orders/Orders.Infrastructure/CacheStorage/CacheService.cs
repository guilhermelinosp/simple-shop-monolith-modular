using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Order.Infrastructure.CacheStorage;

public class CacheService(IDistributedCache cache) : ICacheService
{
	public async Task<T?> GetAsync<T>(string key)
	{
		var objectString = await cache.GetStringAsync(key);

		if (string.IsNullOrWhiteSpace(objectString))
		{
			Console.WriteLine($"cache key {key} not found");

			return default(T);
		}

		Console.WriteLine($"Cache key found for key {key}");

		return JsonConvert.DeserializeObject<T>(objectString);
	}

	public async Task SetAsync<T>(string key, T data)
	{
		var memoryCacheEntryOptions = new DistributedCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
			SlidingExpiration = TimeSpan.FromSeconds(1200)
		};

		var objectString = JsonConvert.SerializeObject(data);

		Console.WriteLine($"cache set for key {key}");

		await cache.SetStringAsync(key, objectString, memoryCacheEntryOptions);
	}
}