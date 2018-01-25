using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ZFC.Shop.Data;

namespace ZFC.Shop.Service
{
    public class ConfigService
    {
        public static List<Assembly> GetAssemblys()
        {
            List<Assembly> list = new List<Assembly>();
            list.Add(typeof(RegisterConfig).Assembly);
            list.Add(typeof(ConfigService).Assembly);
            return list;
        }

        public static void RegisterTables()
        {
            TableConfig.RegisterTables();
        }
    }
}
