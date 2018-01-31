using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Utility
{
    public static class CacheExtensions
    {
        /// <summary>
        /// Get default cache time in minutes
        /// </summary>
        private static int DefaultCacheTimeMinutes { get { return 60; } }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            //use default cache time
            return Get(cacheManager, key, DefaultCacheTimeMinutes, acquire);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="cacheTime">Cache time in minutes (0 - do not cache)</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            //item already is in cache, so return it
            if (cacheManager.IsSet(key))
                return cacheManager.Get<T>(key);

            //or create it using passed function
            var result = acquire();

            //and set in cache (if cache time is defined)
            if (cacheTime < 1) cacheTime = DefaultCacheTimeMinutes;

            cacheManager.Set(key, result, cacheTime);

            return result;
        }
    }
}
