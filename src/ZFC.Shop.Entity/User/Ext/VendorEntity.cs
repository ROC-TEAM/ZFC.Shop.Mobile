using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    public class VendorEntity
    {
        /// <summary>
        /// 摄影师列表页面 缩略图大小
        /// </summary>
        public static int VendorThumbPictureSize = 210;

        public Vendor Vendor { get; set; }

        public Picture Picture { get; set; }

        public Address Address { get; set; }

        public string VendorPicURL { get; set; }

        public VendorEntity() { }

        public VendorEntity(Vendor v, Picture p, Address a)
        {
            this.Vendor = v;
            this.Picture = p;
            this.Address = a;
        }

        public string GetVendorAddress()
        {
            if (this.Address != null)
            {
                return Address.Province + Address.City + Address.Address1;
            }
            return string.Empty;
        }
    }
}
