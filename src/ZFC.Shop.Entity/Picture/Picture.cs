using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    [Table("Picture")]
    public class Picture : BaseEntity
    {
        /// <summary>
        ///PictureBinary
        /// </summary>
        public byte[] PictureBinary { get; set; }
        /// <summary>
        ///MimeType
        /// </summary>
        public string MimeType { get; set; }
        /// <summary>
        ///SeoFilename
        /// </summary>
        public string SeoFilename { get; set; }
        /// <summary>
        ///AltAttribute
        /// </summary>
        public string AltAttribute { get; set; }
        /// <summary>
        ///TitleAttribute
        /// </summary>
        public string TitleAttribute { get; set; }
        /// <summary>
        ///IsNew
        /// </summary>
        public bool IsNew { get; set; }
    }
}
