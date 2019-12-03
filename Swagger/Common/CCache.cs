using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Caching.Memory;

namespace Swagger.Common
{
    public class CCache
    {
        private static readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public static bool SetCache(string key, object value, int seconds)
        {
            if (value == null) return false;

            if (!_cache.TryGetValue(key, out object tmpValue))
            {
                _cache.Remove(key);
            }

            if (seconds <= 0)
            {
                _cache.Set(key, value);
            }
            else
            {
                _cache.Set(key, value, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(seconds)));
            }

            return true;
        }

        public static object GetCache(string key)
        {
            _cache.TryGetValue(key, out object value);
            return value;
        }


        public static bool Exists(string key)
        {
            return _cache.TryGetValue(key, out object tmpValue);
        }

        public static void Remove(string key)
        {
           if(Exists(key))
               _cache.Remove(key);
        }
    }
}
