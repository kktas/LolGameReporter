using Core.Services.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedLockNet;
using System;

public class RedisCacheService(
    IDistributedCache cache,
    IDistributedLockFactory distributedLockFactory,
    TimeSpan? defaultLockTimeout = null,
    TimeSpan? defaultLockWait = null,
    TimeSpan? defaultLockRetryDelay = null
) : IRedisCacheService
{
    private readonly IDistributedCache _cache = cache;
    private readonly IDistributedLockFactory _distributedLockFactory = distributedLockFactory;
    private readonly TimeSpan _defaultLockTimeout = defaultLockTimeout ?? TimeSpan.FromSeconds(30);
    private readonly TimeSpan _defaultLockWait = defaultLockWait ?? TimeSpan.FromSeconds(10);
    private readonly TimeSpan _defaultLockRetryDelay = defaultLockRetryDelay ?? TimeSpan.FromMilliseconds(500);

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> acquire, DistributedCacheEntryOptions options)
    {
        return await GetOrSetAsync<T>(key, acquire, options, _defaultLockTimeout, _defaultLockWait, _defaultLockRetryDelay);
    }

    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> acquire, DistributedCacheEntryOptions options, TimeSpan lockTimeout, TimeSpan lockWait, TimeSpan lockRetryDelay)
    {
        // Try to get the cached value
        var cachedData = await _cache.GetStringAsync(key);
        //cachedData.is
        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonConvert.DeserializeObject<T>(cachedData);
        }

        var value = await acquire();

        return await SetAsync<T>(key, value, options, lockTimeout, lockWait, lockRetryDelay);
    }

    public async Task<T> GetAsync<T>(string key)
    {
        // Try to get the cached value
        var cachedData = await _cache.GetStringAsync(key);

        if (string.IsNullOrEmpty(cachedData))
            return default;

        return JsonConvert.DeserializeObject<T>(cachedData);
    }

    public async Task<T> SetAsync<T>(string key, T value, DistributedCacheEntryOptions options)
    {
        return await SetAsync<T>(key, value, options, _defaultLockTimeout, _defaultLockWait, _defaultLockRetryDelay);
    }
    public async Task<T> SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, TimeSpan lockTimeout, TimeSpan lockWait, TimeSpan lockRetryDelay)
    {
        // Locking mechanism with Redlock
        var lockKey = $"lock:{key}";
        using var redLock = await _distributedLockFactory.CreateLockAsync(lockKey, lockTimeout, lockWait, lockRetryDelay);
        if (redLock.IsAcquired)
        {
            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(value), options);

            return await GetAsync<T>(key);
        }
        else
        {
            throw new Exception("Couldn't cache the key, failed to acquire lock!");
        }
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }

}
