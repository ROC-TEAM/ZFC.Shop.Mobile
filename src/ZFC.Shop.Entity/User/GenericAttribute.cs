using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    [Table("GenericAttribute")]
    public class GenericAttribute : BaseEntity
    {
        ///// <summary>
        /////Id
        ///// </summary>
        //[Column(true)]
        //public int Id { get; set; }
        /// <summary>
        ///EntityId
        /// </summary>
        public int EntityId { get; set; }
        /// <summary>
        ///KeyGroup
        /// </summary>
        public string KeyGroup { get; set; }
        /// <summary>
        ///Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        ///Value
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        ///StoreId
        /// </summary>
        public int StoreId { get; set; }
    }
}
