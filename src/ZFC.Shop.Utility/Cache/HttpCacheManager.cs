using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Web;
using System.Text.RegularExpressions;

namespace ZFC.Shop.Utility
{
    public partial class HttpCacheManager : IStaticCacheManager
    {
        private readonly Cache _cache = HttpRuntime.Cache;
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

            _cache.Insert(key, data, null, DateTime.Now.AddMinutes(cacheTime), TimeSpan.Zero);
        }

        public bool IsSet(string key)
        {
            if (string.IsNullOrEmpty(key)) return false;

            var keys = GetAllKeys();

            return keys.Contains(key);
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
                var keys = GetAllKeys();
                foreach (var item in keys)
                {
                    if (regex.IsMatch(item))
                    {
                        _cache.Remove(item);
                    }
                }
            }
            catch { }
        }

        public void Clear()
        {
            try
            {
                var keys = GetAllKeys();
                foreach (var item in keys)
                {
                    _cache.Remove(item);
                }
            }
            catch { }
        }

        public virtual void Dispose()
        {

        }

        private List<string> GetAllKeys()
        {
            List<string> keys = new List<string>();
            var em = _cache.GetEnumerator();
            while (em.MoveNext())
            {
                keys.Add(em.Key.ToString());
            }
            return keys;
        }
    }
}
