using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Viabilidade.Domain.Interfaces.Cache;

namespace Viabilidade.Infrastructure.Cache
{
    public class StorageMemoryCache : IStorageCache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _timeExpiration;
        private readonly ILogger<StorageMemoryCache> _logger;
        private readonly string _cache_key;
        public StorageMemoryCache(IMemoryCache memoryCache, IConfiguration configuration, ILogger<StorageMemoryCache> logger)
        {
            _memoryCache = memoryCache;
            _cache_key = configuration["MemoryCache:Key"];
            _timeExpiration = TimeSpan.FromMinutes(Convert.ToInt32(configuration["MemoryCache:Expriration_Minutes"]));
            _logger = logger;
        }

        public Task<T> GetAsync<T>(string key)
        {
            try
            {
                return Task.FromResult(_memoryCache.Get<T>($"{_cache_key}:{key}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Não foi possível recuperar o cache do Memory Cache, chave -> {_cache_key}:{key}");
                return Task.FromResult(default(T));
            }
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> function, TimeSpan? timeExpiration = null)
        {

            var data = await _memoryCache.GetOrCreateAsync($"{_cache_key}:{key}", async entry => {

                entry.SlidingExpiration = (timeExpiration ?? _timeExpiration);
                entry.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1);
                return await function();
            });

            return data;
        }

        public void Remove(string key)
        {
            try
            {
                _memoryCache.Remove($"{_cache_key}:{key}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Não foi possível limpar o cache do Memory Cache, chave -> {_cache_key}:{key}");
            }
        }

        public void RemoveAll()
        {
            try
            {
                _memoryCache.Remove($"{_cache_key}:*");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Não foi possível limpar o cache do Memory Cache, chave -> {_cache_key}:*");
            }
        }

        public void Set<T>(string key, T data, TimeSpan? timeExpiration = null)
        {
            try
            {
                _memoryCache.Set($"{_cache_key}:{key}", data, (timeExpiration ?? _timeExpiration));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Não foi possível setar o cache do Memory Cache, chave -> {_cache_key}:{key}");
            }
        }
    }
}