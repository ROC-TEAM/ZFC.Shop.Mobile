using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ZFC.Shop.Utility
{
    /// <summary>
    /// 缓存操作类
    /// </summary>
    public sealed class CacheHelper
    {
        /// <summary>
        /// 根据key获得缓存
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static T Get<T>(string key) where T : class
        {
            var obj = HttpRuntime.Cache.Get(key);
            return obj as T;
        }
        /// <summary>
        /// 根据key获得缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return Get<object>(key);
        }
        /// <summary>
        /// 根据key移除缓存
        /// </summary>
        /// <param name="key">key</param>
        public static void Remove(string key)
        {
            var obj = HttpRuntime.Cache.Get(key);
            if (obj != null)
                HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// 根据指定字符串 移除缓存
        /// </summary>
        /// <param name="prefix"></param>
        public static void RemoveByPrefix(string prefix)
        {
            List<string> keys = new List<string>();
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }
            foreach (var item in keys)
            {
                if (item.Contains(prefix))
                {
                    HttpRuntime.Cache.Remove(item);
                }
            }
        }
        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void Clear()
        {
            List<string> keys = new List<string>();
            // retrieve application Cache enumerator
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            // copy all keys that currently exist in Cache
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }
            // delete every key from cache
            for (int i = 0; i < keys.Count; i++)
            {
                HttpRuntime.Cache.Remove(keys[i]);
            }
        }
        /// <summary>
        /// 添加缓存  默认添加 30分钟
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">对象</param>
        public static void Add(string key, object obj)
        {
            Add<object>(key, obj, DateTimeType.Minute, 30);
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="obj">对象</param>
        /// <param name="type">日期类型</param>
        /// <param name="num">长度</param>
        public static void Add(string key, object obj, DateTimeType type, int num)
        {
            Add<object>(key, obj, type, num);
        }
        /// <summary>
        /// 添加缓存 默认添加 30分钟
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="obj">对象实体</param>
        public static void Add<T>(string key, T obj) where T : class
        {
            Add<T>(key, obj, DateTimeType.Minute, 30);
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="obj">对象</param>
        /// <param name="type"></param>
        /// <param name="num"></param>
        public static void Add<T>(string key, T obj, DateTimeType type, int num) where T : class
        {
            if (obj == null) return;
            DateTime dt = DateTimeTypeHelper.GetDateTime(type, num);
            HttpRuntime.Cache.Insert(key, obj, null, dt, TimeSpan.Zero);
        }
    }
}
