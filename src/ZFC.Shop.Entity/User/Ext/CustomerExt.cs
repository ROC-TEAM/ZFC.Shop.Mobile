using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    public class CustomerExt
    {
        public static string FirstName = "FirstName";

        public static string LastName = "LastName";

        public static string AvatarPictureId = "AvatarPictureId";

        public static int AvatarPictureSize = 150;

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string AvatarUrl { get; set; }
    }
}
