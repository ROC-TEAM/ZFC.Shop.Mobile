using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    [Table("Vendor")]
    public class Vendor : BaseEntity
    {
        /// <summary>
        ///Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        ///Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///PictureId
        /// </summary>
        public int PictureId { get; set; }
        /// <summary>
        ///AddressId
        /// </summary>
        public int AddressId { get; set; }
        /// <summary>
        ///AdminComment
        /// </summary>
        public string AdminComment { get; set; }
        /// <summary>
        ///Active
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        ///Deleted
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        ///DisplayOrder
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        ///MetaKeywords
        /// </summary>
        public string MetaKeywords { get; set; }
        /// <summary>
        ///MetaDescription
        /// </summary>
        public string MetaDescription { get; set; }
        /// <summary>
        ///MetaTitle
        /// </summary>
        public string MetaTitle { get; set; }
        /// <summary>
        ///PageSize
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        ///AllowCustomersToSelectPageSize
        /// </summary>
        public bool AllowCustomersToSelectPageSize { get; set; }
        /// <summary>
        ///PageSizeOptions
        /// </summary>
        public string PageSizeOptions { get; set; }
    }
}
