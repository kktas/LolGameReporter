namespace Core.Services.Cache
{
    public interface ICacheService
    {
        public Task<T> GetOrSetAsync<T>();
    }
}
