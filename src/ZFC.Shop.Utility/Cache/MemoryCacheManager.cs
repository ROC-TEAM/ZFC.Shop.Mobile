using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace ZFC.Shop.Utility
{
    /// <summary>
    /// Represents a memory cache manager 
    /// </summary>
    public partial class MemoryCacheManager : IStaticCacheManager
    {
        private readonly MemoryCache _cache = MemoryCache.Default;
        private int defaultCacheTime = 30;

        public T Get<T>(string key)
        {
            object obj = _cache.Get(key);
            if (obj != null) return (T)obj;
            return default(T);
        }

        public void Set(string key, object data, int cacheTime)
        {
            if (string.IsNullOrEmpty(key)) return;
            if (data == null) return;

            if (cacheTime < 1) cacheTime = defaultCacheTime;

            Remove(key);

            _cache.Set(key, data, DateTime.Now.AddMinutes(cacheTime));
        }

        public bool IsSet(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;
            return _cache.Contains(key);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key)) return;
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            try
            {
                foreach (var item in _cache)
                {
                    if (regex.IsMatch(item.Key))
                    {
                        _cache.Remove(item.Key);
                    }
                }
            }
            catch { }
        }

        public void Clear()
        {
            try
            {
                foreach (var item in _cache)
                {
                    _cache.Remove(item.Key);
                }
            }
            catch { }
        }

        public virtual void Dispose()
        {

        }
    }
}
