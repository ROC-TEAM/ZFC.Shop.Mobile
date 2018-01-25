using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web;

namespace ZFC.Shop.Utility
{
    public class JsonHelper
    {
        public static string ToJson(object o, string format = "", bool jsEncode = true)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings();
            if (string.IsNullOrEmpty(format)) format = "yyyy-MM-dd HH:mm:ss";
            jss.DateFormatString = format;
            string json = JsonConvert.SerializeObject(o, jss);
            if (jsEncode) json = HttpUtility.JavaScriptStringEncode(json);
            return json;
        }

        public static object ToObject(string s, string fmt = "yyyy-MM-dd")
        {
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.Converters.Add(new MyDateTimeConverter(fmt));
            return JsonConvert.DeserializeObject(s, jss);
        }

        public static T ToObject<T>(string s, string fmt = "yyyy-MM-dd")
        {
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.Converters.Add(new MyDateTimeConverter(fmt));
            return JsonConvert.DeserializeObject<T>(s, jss);
        }

        public static Dictionary<string, object> ToDictionary(string json)
        {
            if (json.IsEmpty()) return null;
            Dictionary<string, object> dic = null;
            try
            {
                dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
            catch { }
            return dic;
        }
    }

    public class MyDateTimeConverter : IsoDateTimeConverter
    {
        public MyDateTimeConverter() { }

        public MyDateTimeConverter(string fmt)
        {
            if (string.IsNullOrEmpty(fmt)) fmt = "yyyy-MM-dd HH:mm:ss";
            this.DateTimeFormat = fmt;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object s = reader.Value;
            if (s == null || string.IsNullOrEmpty(s.ToString())) return DateTime.MinValue;
            return base.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            base.WriteJson(writer, value, serializer);
        }
    }
}
