using System;
using Danske.Service.Core;
using Microsoft.Extensions.Caching.Memory;

namespace Danske.Service.Services.Adapters
{
    public class CachedCalculationService : ICalculationService
    {
        private readonly ICalculationService _calculationService;
        private readonly IMemoryCache _memoryCache;

        public CachedCalculationService(ICalculationService calculationService, IMemoryCache memoryCache)
        {
            _calculationService = calculationService;
            _memoryCache = memoryCache;
        }
        public Graph Traverse(int[] input)
        {
            var key = CacheKeyHelper.ArrayHash(input);

            return _memoryCache.GetOrCreate(key, entry =>
            {
                entry.SlidingExpiration = new TimeSpan(0, 1, 0, 0);

                var response = _calculationService.Traverse(input);
                return response;
            });
        }
    }
}
