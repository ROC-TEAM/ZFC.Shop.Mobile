using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Utility
{
    public class LogFactory
    {
        private static ILog log;

        public static ILog GetLogger()
        {
            if (log == null) log = new LogHelper();
            return log;
        }
    }
}
