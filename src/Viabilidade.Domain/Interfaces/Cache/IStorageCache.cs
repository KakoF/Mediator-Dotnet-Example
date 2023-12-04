namespace Viabilidade.Domain.Interfaces.Cache
{
    public interface IStorageCache
    {
        Task<T> GetAsync<T>(string key);
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> function, TimeSpan? timeExpiration = null);
        void Set<T>(string key, T data, TimeSpan? timeExpiration = null);
        void Remove(string key);
    }
}