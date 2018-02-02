using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFC.Shop.Data;
using ZFC.Shop.Entity;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Service
{
    public interface IProductService : IDependency
    {
        /// <summary>
        /// 获得产品分页列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<ProductEntity> GetProductList(ProductQueryEntity model);
    }

    public class ProductService : IProductService
    {
        readonly IProductRepository productRep;
        readonly IPictureService pictureService;

        public ProductService(IProductRepository ipr, IPictureService ips)
        {
            productRep = ipr;
            pictureService = ips;
        }

        public IEnumerable<ProductEntity> GetProductList(ProductQueryEntity model)
        {
            var list = productRep.GetProductList(model);
            if (list != null)
            {
                foreach (var item in list)
                {
                    item.Product.ShortDescription = UtilityHepler.LostHTML(item.Product.ShortDescription);
                    if (item.Picture != null && item.Picture.Id > 0)
                    {
                        item.ProductPicURL = pictureService.GetPictureUrl(item.Picture, ProductEntity.ProductThumbPictureSize);
                    }
                }
            }
            return list;
        }
    }
}
