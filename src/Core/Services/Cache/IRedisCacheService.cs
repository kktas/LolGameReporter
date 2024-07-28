using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Cache
{
    public interface IRedisCacheService : ICacheService
    {
        public Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> acquire, DistributedCacheEntryOptions options);
        public Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> acquire, DistributedCacheEntryOptions options, TimeSpan lockTimeout, TimeSpan lockWait, TimeSpan lockRetryDelay);
        public Task<T> GetAsync<T>(string key);
        public Task<T> SetAsync<T>(string key, T value, DistributedCacheEntryOptions options);
        public Task<T> SetAsync<T>(string key, T value, DistributedCacheEntryOptions options, TimeSpan lockTimeout, TimeSpan lockWait, TimeSpan lockRetryDelay);
        public Task RemoveAsync(string key);

    }
}
