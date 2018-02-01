using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFC.Shop.Entity;
using ZFC.Shop.Data;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Service
{
    public interface IVendorService : IDependency
    {
        IEnumerable<VendorEntity> GetVendorList(VendorQueryEntity model);
    }

    public class VendorService : IVendorService
    {
        readonly IVendorRepository vendorRep;
        readonly IPictureService pictureService;
        public VendorService(IVendorRepository ivr, IPictureService ips)
        {
            vendorRep = ivr;
            pictureService = ips;
        }

        public IEnumerable<VendorEntity> GetVendorList(VendorQueryEntity model)
        {
            var list = vendorRep.GetVendorList(model);
            if (list != null)
            {
                foreach (var item in list)
                {
                    item.Vendor.Description = UtilityHepler.LostHTML(item.Vendor.Description);
                    if (item.Picture != null && item.Vendor.PictureId > 0)
                    {
                        item.Picture.Id = item.Vendor.PictureId;
                        item.VendorPicURL = pictureService.GetPictureUrl(item.Picture, VendorEntity.VendorThumbPictureSize);
                    }
                }
            }
            return list;
        }
    }
}
