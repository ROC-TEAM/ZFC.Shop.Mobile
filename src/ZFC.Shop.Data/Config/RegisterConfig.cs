using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ZFC.Shop.Data
{
    public class RegisterConfig
    {
        public static List<Assembly> GetAssemblys()
        {
            List<Assembly> list = new List<Assembly>();
            list.Add(typeof(RegisterConfig).Assembly);
            return list;
        }
    }
}
