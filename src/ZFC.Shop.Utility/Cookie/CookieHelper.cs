using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ZFC.Shop.Utility
{
    /// <summary>
    /// Cookie操作类
    /// </summary>
    public sealed class CookieHelper
    {
        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public static void Add<T>(CookieEntity<T> entity) where T : class
        {
            HttpCookie cookie = new HttpCookie(entity.CookieName);
            string v = typeof(T) == typeof(string) ? entity.Value.ToString() : JsonHelper.ToJson(entity.Value);
            if (entity.IsEncryp)
                v = EncryptHelper.Encrypt(entity.EncrypKey, v);
            if (entity.IsEncode)
                v = HttpUtility.UrlEncode(v, entity.Encoding);
            cookie.Value = v;
            cookie.Path = entity.Path;
            cookie.Domain = entity.Domain;
            cookie.Expires = DateTimeTypeHelper.GetDateTime(entity.Type, entity.Number);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void Add<T>(string name, T value, DateTimeType type = DateTimeType.Day, int number = 1) where T : class
        {
            CookieEntity<T> entity = new CookieEntity<T>(name, value);
            entity.Type = type;
            entity.Number = number;
            Add<T>(entity);
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <param name="entity"></param>
        public static void Add(CookieEntity entity)
        {
            Add<string>(entity);
        }

        public static void Add(string name, string value, DateTimeType type = DateTimeType.Day, int number = 1, bool encryp = false)
        {
            CookieEntity entity = new CookieEntity(name, value);
            entity.Type = type;
            entity.Number = number;
            entity.IsEncryp = encryp;
            Add(entity);
        }
        /// <summary>
        /// 根据CookieName 获取Cookie
        /// </summary>
        /// <param name="CookieName">cookie名称</param>
        /// <returns></returns>
        public static string Get(CookieEntity entity)
        {
            string cookieName = entity.CookieName;
            var cookies = HttpContext.Current.Request.Cookies;
            if (cookies != null && cookies[cookieName] != null)
            {
                string value = cookies[cookieName].Value;
                if (string.IsNullOrEmpty(value)) return string.Empty;
                if (entity.IsEncode)
                    value = HttpUtility.UrlDecode(value, entity.Encoding); //2013-11-08修改
                if (entity.IsEncryp)
                    return EncryptHelper.Decrypt(entity.EncrypKey, value);
                else
                    return value;
            }
            return string.Empty;
        }

        public static string Get(string name, bool isEncryp = false)
        {
            CookieEntity entity = new CookieEntity(name);
            entity.IsEncryp = isEncryp;
            return Get(entity);
        }

        public static T Get<T>(string name, bool isEncryp = false) where T : class
        {
            CookieEntity<T> entity = new CookieEntity<T>(name);
            entity.IsEncryp = isEncryp;
            return Get<T>(entity);
        }

        /// <summary>
        /// 根据CookieName 获取Cookie
        /// </summary>
        /// <param name="CookieName">cookie名称</param>
        /// <returns></returns>
        public static T Get<T>(CookieEntity<T> entity) where T : class
        {
            string cookieName = entity.CookieName;
            var cookies = HttpContext.Current.Request.Cookies;
            string value = string.Empty;
            if (cookies != null && cookies[cookieName] != null)
            {
                value = cookies[cookieName].Value;
                if (!string.IsNullOrEmpty(value))
                {
                    if (entity.IsEncode)
                        value = HttpUtility.UrlDecode(value, entity.Encoding);
                    if (entity.IsEncryp)
                        value = EncryptHelper.Decrypt(entity.EncrypKey, value);
                }
            }
            if (string.IsNullOrEmpty(value)) return default(T);
            return JsonHelper.ToObject<T>(value);
        }
        /// <summary>
        /// 清除指定名称的Cookie
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        public static void Remove(string cookieName, string domain = "")
        {
            HttpCookieCollection hc = HttpContext.Current.Request.Cookies;
            if (hc.Count > 0 && hc[cookieName] != null)
            {
                hc[cookieName].Expires = DateTime.Now.AddDays(-1);
                hc[cookieName].Domain = domain;
                HttpContext.Current.Response.Cookies.Add(hc[cookieName]);
            }
        }

        /// <summary>
        /// 清除所有cookie
        /// </summary>
        public static void Clear(string domain)
        {
            HttpCookieCollection hc = HttpContext.Current.Request.Cookies;
            if (hc != null && hc.Count > 0)
            {
                foreach (var item in hc.AllKeys)
                {
                    HttpCookie cookie = new HttpCookie(hc[item].Name);
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    cookie.Domain = domain;
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
        }
    }

    public class CookieEntity<T> where T : class
    {
        /// <summary>
        /// Cookie名称
        /// </summary>
        public string CookieName { get; set; }
        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// Path
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 日期类型
        /// </summary>
        public DateTimeType Type { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public double Number { get; set; }
        /// <summary>
        /// 是否加密 默认不加密
        /// </summary>
        public bool IsEncryp { get; set; }
        /// <summary>
        /// 是否encode 默认不encode
        /// </summary>
        public bool IsEncode { get; set; }
        /// <summary>
        /// 加密KEY
        /// </summary>
        public string EncrypKey { get; set; }

        public Encoding Encoding { get; set; }

        public CookieEntity()
        {
            IsEncryp = false;
            IsEncode = false;
            Number = 1;
            Type = DateTimeType.Day;
            //Domain = "/";
            Path = "/";
            EncrypKey = "ROCCORECOOKIEKEY123!#%";
            this.Encoding = System.Text.Encoding.UTF8;
        }

        public CookieEntity(string name, T value = null)
            : this()
        {
            this.CookieName = name;
            this.Value = value;
        }
    }

    public sealed class CookieEntity : CookieEntity<string>
    {
        public CookieEntity() : base() { }

        public CookieEntity(string name, string value = "")
            : base()
        {
            this.CookieName = name;
            this.Value = value;
        }
    }

    public enum DateTimeType
    {
        /// <summary>
        /// 月
        /// </summary>
        Month = 5,
        /// <summary>
        /// 天
        /// </summary>
        Day = 4,
        /// <summary>
        /// 时
        /// </summary>
        Hour = 1,
        /// <summary>
        /// 分
        /// </summary>
        Minute = 2,
        /// <summary>
        /// 秒
        /// </summary>
        Second = 3
    }
}
