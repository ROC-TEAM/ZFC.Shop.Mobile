using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZFC.Shop.Entity;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Data
{
    public interface IVendorRepository : IRepository<Vendor>
    {
        /// <summary>
        /// 获得供应商分页列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IEnumerable<VendorEntity> GetVendorList(VendorQueryEntity model);
    }

    public class VendorRepository : RepositoryBase<Vendor>, IVendorRepository
    {
        public IEnumerable<VendorEntity> GetVendorList(VendorQueryEntity model)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("@pageIndex", model.PageIndex);
            dic.Add("@pageSize", model.PageSize);
            dic.Add("@provinceId", model.ProvinceId);
            dic.Add("@city", model.City);
            dic.Add("@key", model.Key);

            var reader = base.GetReader("[GetVendorPageList]", dic, System.Data.CommandType.StoredProcedure);
            IEnumerable<VendorEntity> list = null;
            if (reader != null)
            {
                model.Total = reader.Read<int>().FirstOrDefault();
                list = reader.Read<Vendor, Picture, Address, VendorProductEntity, VendorEntity>((v, p, a, vp) =>
                  {
                      return new VendorEntity(v, p, a, vp);
                  }, new string[] { "VP", "PA", "AC" }).ToList();
            }
            return list;
        }
    }
}
