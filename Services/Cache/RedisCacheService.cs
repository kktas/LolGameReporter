using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedLockNet;

public class RedisCacheService(IDistributedCache cache, IDistributedLockFactory distributedLockFactory, TimeSpan? defaultLockTimeout = null, TimeSpan? defaultLockRetryDelay = null)
{
    private readonly IDistributedCache _cache = cache;
    private readonly IDistributedLockFactory _distributedLockFactory = distributedLockFactory;
    private readonly TimeSpan _defaultLockTimeout = defaultLockTimeout ?? TimeSpan.FromSeconds(30);
    private readonly TimeSpan _defaultLockRetryDelay = defaultLockRetryDelay ?? TimeSpan.FromMilliseconds(500);

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> acquire, DistributedCacheEntryOptions options)
    {
        return await GetOrSetAsync(key, acquire, options, _defaultLockTimeout, _defaultLockRetryDelay);
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> acquire, DistributedCacheEntryOptions options, TimeSpan lockTimeout, TimeSpan lockRetryDelay)
    {
        // Try to get the cached value
        var cachedData = await _cache.GetStringAsync(key);

        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonConvert.DeserializeObject<T>(cachedData);
        }

        // Locking mechanism with Redlock
        var lockKey = $"{key}_lock";
        using var redLock = await _distributedLockFactory.CreateLockAsync(lockKey, lockTimeout);
        if (!redLock.IsAcquired)
        {
            // If unable to acquire the lock, wait and retry getting the cached value
            await Task.Delay(lockRetryDelay);
            cachedData = await _cache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonConvert.DeserializeObject<T>(cachedData);
            }
        }

        try
        {
            // Get the data from the source
            var result = await acquire();

            // Cache the data
            var serializedData = JsonConvert.SerializeObject(result);
            await _cache.SetStringAsync(key, serializedData, options);

            return result;
        }
        finally
        {
            // Release the lock (automatically handled by the using statement)
        }
    }
}
