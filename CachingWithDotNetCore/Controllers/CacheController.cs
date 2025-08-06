using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace CachingWithDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {

        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;
        public CacheController(IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        [HttpGet("memory")]
        public IActionResult GetMemoryCache()
        {
            string cacheKey = "today";
            if (!_memoryCache.TryGetValue(cacheKey, out string cachedData))
            {
                cachedData = $"In-Memory Cached Time: {DateTime.Now}";
                _memoryCache.Set(cacheKey, cachedData, TimeSpan.FromSeconds(60));
            }

            return Ok(cachedData);
        }


        [HttpGet("redis")]
        public async Task<IActionResult> GetRedisCache()
        {
            string cacheKey = "redisTime";
            var cachedData = await _distributedCache.GetStringAsync(cacheKey);
            if (cachedData == null)
            {
                cachedData = $"Redis Cached Time: {DateTime.Now}";
                await _distributedCache.SetStringAsync(cacheKey, cachedData, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
                });
            }

            return Ok(cachedData);
        }
    }
}