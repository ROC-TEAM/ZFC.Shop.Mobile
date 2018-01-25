using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace ZFC.Shop.Entity
{
    public class Web
    {
        public static dynamic GetDynamicModel(dynamic baseInfo, IEnumerable<dynamic> logs, dynamic files = null)
        {
            dynamic obj = new ExpandoObject();
            obj.Base = baseInfo;
            obj.Logs = logs;
            obj.Files = files;
            return obj;
        }
    }
}
