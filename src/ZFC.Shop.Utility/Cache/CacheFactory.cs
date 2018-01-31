using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Utility
{
    public enum CacheType
    {
        Memory = 1,
        Http = 2
    }

    public class CacheFactory
    {
        public static IStaticCacheManager GetStaticCacheManager(CacheType type = CacheType.Memory)
        {
            IStaticCacheManager cache = null;
            switch (type)
            {
                case CacheType.Memory:
                    cache = new MemoryCacheManager();
                    break;
                case CacheType.Http:
                    cache = new HttpCacheManager();
                    break;
            }
            return cache;
        }

        public static ICacheManager GetCacheManager(CacheType type = CacheType.Memory)
        {
            return GetStaticCacheManager(type);
        }
    }
}
