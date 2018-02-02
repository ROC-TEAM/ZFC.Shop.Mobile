using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZFC.Shop.Entity;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Data
{
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// 获得产品分页列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<ProductEntity> GetProductList(ProductQueryEntity model);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository() { }

        public IEnumerable<ProductEntity> GetProductList(ProductQueryEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(model.CategoryIds))
                dic.Add("@CategoryIds", model.CategoryIds);
            if (model.VendorId > 0)
                dic.Add("@VendorId", model.VendorId);
            if (!string.IsNullOrEmpty(model.Key))
                dic.Add("@Keywords", model.Key);
            dic.Add("@PageIndex", model.PageIndex);
            dic.Add("@PageSize", model.PageSize);

            IEnumerable<ProductEntity> list = null;
            var reader = GetReader("[GetProductPageList]", dic, System.Data.CommandType.StoredProcedure);
            if (reader != null)
            {
                model.Total = reader.Read<int>().FirstOrDefault();
                list = reader.Read<Product, Picture, ProductEntity>((p, pp) => new ProductEntity(p, pp), new string[] { "PP" }).ToList();
            }
            return list;
        }
    }
}
