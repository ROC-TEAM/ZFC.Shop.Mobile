using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    public class ProductQueryEntity : BasePageEntity
    {
        public string CategoryIds { get; set; }

        public int VendorId { get; set; }

        public int OrderBy { get; set; }

        public string Key { get; set; }
    }
}
