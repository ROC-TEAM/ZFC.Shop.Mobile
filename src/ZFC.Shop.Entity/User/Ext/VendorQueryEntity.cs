using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    public class VendorQueryEntity : BasePageEntity
    {
        public VendorQueryEntity()
        {
            this.ProvinceId = 0;
            this.City = "";
            this.Key = "";
        }

        public int ProvinceId { get; set; }

        public string City { get; set; }

        public string Key { get; set; }
    }
}
