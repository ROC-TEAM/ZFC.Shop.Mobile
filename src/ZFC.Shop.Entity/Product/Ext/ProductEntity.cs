using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    public class ProductEntity
    {
        public static int ProductThumbPictureSize = 210;

        public Product Product { get; set; }

        public Picture Picture { get; set; }

        public string ProductPicURL { get; set; }

        public ProductEntity()
        { }

        public ProductEntity(Product p, Picture pp)
        {
            this.Product = p;
            this.Picture = pp;
        }
    }
}
