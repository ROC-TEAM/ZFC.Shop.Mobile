using System.Web;
using System.Web.SessionState;

namespace ZFC.Shop.Utility
{
    public class SessionHelper
    {
        public static T Get<T>(string key) where T : class
        {
            return Get(key) as T;
        }

        public static object Get(string key)
        {
            return HttpContext.Current.Session[key];
        }

        public static void Add(string key, object obj)
        {
            if (string.IsNullOrEmpty(key)) return;
            if (obj != null) HttpContext.Current.Session[key] = obj;
        }

        public static void Remove(string key)
        {
            HttpSessionState hss = HttpContext.Current.Session;
            if (hss == null) return;
            for (int i = 0; i < hss.Keys.Count; i++)
            {
                if (string.Equals(hss.Keys[i], key, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    hss.Remove(key);
                }
            }
        }

        public static void Clear()
        {
            HttpSessionState hss = HttpContext.Current.Session;
            if (hss != null)
            {
                hss.Clear();
            }
        }
    }
}
