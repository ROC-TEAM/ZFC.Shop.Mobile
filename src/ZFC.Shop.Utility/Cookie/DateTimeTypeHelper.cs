using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Utility
{
    public sealed class DateTimeTypeHelper
    {
        /// <summary>
        /// 获得DateTime
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="type"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(DateTime dt, DateTimeType type, double num)
        {
            switch (type)
            {
                case DateTimeType.Day:
                    dt = dt.AddDays(num);
                    break;
                case DateTimeType.Hour:
                    dt = dt.AddHours(num);
                    break;
                case DateTimeType.Minute:
                    dt = dt.AddMinutes(num);
                    break;
                case DateTimeType.Month:
                    dt = dt.AddMonths((int)num);
                    break;
                case DateTimeType.Second:
                    dt = dt.AddSeconds(num);
                    break;
            }
            return dt;
        }
        /// <summary>
        /// 获得DateTime
        /// </summary>
        /// <param name="type"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(DateTimeType type, double num)
        {
            DateTime dt = DateTime.Now;
            return GetDateTime(dt, type, num);
        }
    }
}
