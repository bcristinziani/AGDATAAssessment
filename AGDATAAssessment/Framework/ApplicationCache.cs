using AGDATAAssessment.Data.Models;
using AGDATAAssessment.Interfaces;
using AGDATAAssessment.Services;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;

namespace AGDATAAssessment.Framework
{
    public class ApplicationCache : IApplicationCache
    {
        private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
        private readonly IMemoryCache _cache;
        private readonly IPersonService _personService;
        private const double _expirationInMinutes = 30;


        public ApplicationCache(IMemoryCache cache, IPersonService personService)
        {
            _cache = cache;
            _personService = personService;
        }

        public async Task<IList<T>> GetList<T>(string cacheKey) where T : Person
        {
            if (_cache.TryGetValue(cacheKey, out List<T> list)) 
                return list;

            try
            {
                await Semaphore.WaitAsync();

                if (_cache.TryGetValue(cacheKey, out list)) 
                    return list;

                list = (List<T>?)_personService.GetPeople();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(_expirationInMinutes)
                };

                _cache.Set(cacheKey, list, cacheEntryOptions);
            }
            catch
            {
                throw;
            }
            finally
            {
                Semaphore.Release();
            }

            return list;
        }

        public void Refresh(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }
    }
}
