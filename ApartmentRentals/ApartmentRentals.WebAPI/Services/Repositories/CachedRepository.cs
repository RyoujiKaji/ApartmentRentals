using ApartmentRentals.Data.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ApartmentRentals.WebAPI.Services.Repositories
{
    public class CachedRepository<T> : IRepository<T> where T : IModel
    {
        private IRepository<T> repository;
        private IDistributedCache cache;
        private readonly string cacheNamespace = typeof(T).Name;

        private const int absoluteCachedTime = 2;
        private const int slidingCachedTime = 1;

        public CachedRepository(IRepository<T> spaceRepository, IDistributedCache cache)
        {
            this.repository = spaceRepository;
            this.cache = cache;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> objects;
            var cacheKey = $"{cacheNamespace}:all";

            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                objects = JsonSerializer.Deserialize<List<T>>(cachedData) ?? new List<T>();
            }
            else
            {
                objects = await repository.GetAllAsync();

                if (objects.Count() != 0)
                {
                    var serializedData = JsonSerializer.Serialize(objects);
                    var cacheOptions = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteCachedTime));
                    await cache.SetStringAsync(cacheKey, serializedData, cacheOptions);
                }
            }

            return objects;
        }

        public async Task<IEnumerable<T>> GetFilteredByPropertyAsync(string propertyName, string value)
        {
            IEnumerable<T> objects;
            var cacheKey = $"{cacheNamespace}:filter:{propertyName}:{value}";

            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                objects = JsonSerializer.Deserialize<List<T>>(cachedData) ?? new List<T>();
            }
            else
            {
                objects = await repository.GetFilteredByPropertyAsync(propertyName, value);

                if (objects.Count() != 0)
                {
                    var serializedData = JsonSerializer.Serialize(objects);
                    var cacheOptions = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteCachedTime));
                    await cache.SetStringAsync(cacheKey, serializedData, cacheOptions);
                }
            }

            return objects;
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            T? _object;
            var cacheKey = $"{cacheNamespace}:id:{id}";

            var cachedData = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                _object = JsonSerializer.Deserialize<T>(cachedData);
            }
            else
            {
                _object = await repository.GetByIdAsync(id);

                if (_object != null)
                {
                    var serializedData = JsonSerializer.Serialize(_object);
                    var cacheOptions = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteCachedTime))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(slidingCachedTime));
                    await cache.SetStringAsync(cacheKey, serializedData, cacheOptions);
                }
            }

            return _object;
        }

        public async Task CreateAsync(T newSpace) => await repository.CreateAsync(newSpace);

        public async Task<bool> UpdateAsync(string id, T updatedSpace)
        {
            var success = await repository.UpdateAsync(id, updatedSpace);
            if (success)
            {
                var cacheKey = $"{cacheNamespace}:id:{id}";
                await cache.RemoveAsync(cacheKey);
            }

            return success;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var success = await repository.DeleteByIdAsync(id);
            if (success)
            {
                var cacheKey = $"{cacheNamespace}:id:{id}";
                await cache.RemoveAsync(cacheKey);
            }

            return success;
        }

        public async Task<bool> DeleteAllAsync() => await repository.DeleteAllAsync();
    }
}
