using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZFC.Shop.Entity;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Data
{
    public interface IPictureRepository : IRepository<Picture>
    {
        Picture GetById(int id);

        IEnumerable<Picture> GetPicturesByProductId(int productId, int top = 0);
    }

    public class PictureRepository : RepositoryBase<Picture>, IPictureRepository
    {
        public PictureRepository()
        {

        }

        public Picture GetById(int id)
        {
            return GetEntity(m => m.Id == id);
        }

        public IEnumerable<Picture> GetPicturesByProductId(int productId, int top = 0)
        {
            var pictureSql = base.GetSqlLam<Picture>();
            var productPictureSql = pictureSql.Join<ProductPicture>((p, pp) => p.Id == pp.PictureId, aliasName: "b");
            productPictureSql.Where(m => m.ProductId == productId).OrderBy(m => m.DisplayOrder, m => m.ProductId);

            if (top > 0) pictureSql.Top(top);
            pictureSql.SelectAll();

            return GetList(pictureSql.GetSql(), pictureSql.GetParameters());
        }
    }
}
