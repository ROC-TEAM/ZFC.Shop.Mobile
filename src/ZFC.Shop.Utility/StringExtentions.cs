using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Reflection;

namespace ZFC.Shop.Utility
{
    public static class StringExtentions
    {
        public static T ToObject<T>(this string json) where T : class
        {
            if (string.IsNullOrEmpty(json)) return null;

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T ToValue<T>(this string json, string key)
        {
            T t = default(T);
            if (json.IsEmpty()) return t;
            var obj = JObject.Parse(json);
            if (obj != null)
            {
               t= obj.Value<T>(key);
            }
            return t;
        }

        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool Equals(this string s, string value, StringComparison comparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return s.Equals(value, comparison);
        }

        public static bool EqualsY(this string s)
        {
            return s.Equals("Y", StringComparison.CurrentCultureIgnoreCase);
        }

        public static int ToInt(this string s, int defaultValue = 0)
        {
            int result = defaultValue;
            if (!int.TryParse(s, out result))
                result = defaultValue;
            return result;
        }

        public static long ToInt64(this string s, long defaultValue = 0)
        {
            long result = defaultValue;
            if (!long.TryParse(s, out result))
                result = defaultValue;
            return result;
        }

        public static decimal ToDecimal(this string s, decimal defaultValue = 0)
        {
            decimal result = defaultValue;
            if (!decimal.TryParse(s, out result))
                result = defaultValue;
            return result;
        }

        public static double ToDouble(this string s, double dvalue = 0)
        {
            double result = dvalue;
            if (!double.TryParse(s, out result))
                result = dvalue;
            return result;
        }

        public static DateTime ToDateTime(this string s, DateTime? dValue = null)
        {
            DateTime dt = dValue.HasValue ? dValue.Value : DateTime.MinValue;
            DateTime.TryParse(s, out dt);
            return dt;
        }

        public static DateTime? ToNullableDateTime(this string s, DateTime? dValue = null)
        {
            DateTime value;
            if (!DateTime.TryParse(s, out value))
            {
                return dValue;
            }
            return value;
        }

        public static bool ToBoolean(this string s, bool dValue = false)
        {
            if (string.IsNullOrEmpty(s)) return dValue;
            s = s.ToLower();
            if (s == "1" || s == "true" || s == "真") return true;
            if (s.ToInt(0) > 0) return true;
            return false;
        }
    }
}
