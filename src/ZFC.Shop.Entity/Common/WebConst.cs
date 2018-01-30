using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    public class WebConst
    {
        public static readonly string UserLoginSessionKey = "ZFC.SHOP.SESSIONKEY_{0}";

        public static readonly string UserLoginCookieKey = "ZFC_UID";

        public static string DateTimeFmt1 = "yy/MM/dd";


        #region cache key

        /// <summary>
        ///扩展字段列表缓存key
        /// </summary>
        public static string GenericAttibuteListCacheKey = "GenericAttibuteListCache";

        #endregion
    }
}
