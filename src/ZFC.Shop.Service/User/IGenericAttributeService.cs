using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZFC.Shop.Entity;
using ZFC.Shop.Utility;
using ZFC.Shop.Data;

namespace ZFC.Shop.Service
{
    public interface IGenericAttributeService : IDependency
    {
        /// <summary>
        /// 获得所有扩展字段列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<GenericAttribute> GetAllList();

        /// <summary>
        /// 获得扩展属性列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        IEnumerable<GenericAttribute> GetList(BaseEntity model, int storeId = 0);

        /// <summary>
        /// 获得扩展属性值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="key"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        string GetGenericAttributeValue(BaseEntity model, string key, int storeId = 0);
    }

    public class GenericAttributeService : IGenericAttributeService
    {
        private readonly IGenericAttributeRepository genRep;
        private readonly ICacheManager cacheManager;
        public GenericAttributeService(IGenericAttributeRepository igar)
        {
            genRep = igar;
            cacheManager = CacheFactory.GetCacheManager();
        }

        public IEnumerable<GenericAttribute> GetAllList()
        {
            string key = WebConst.GenericAttibuteListCacheKey;

            var list = cacheManager.Get(key, () =>
            {
                return genRep.GetList();
            });
            return list;
        }

        public IEnumerable<GenericAttribute> GetList(BaseEntity model, int storeId = 0)
        {
            if (model == null) return null;
            IEnumerable<GenericAttribute> tempList = null;
            var list = GetAllList();
            if (list != null && list.Count() > 0)
            {
                Func<GenericAttribute, bool> func = (m) =>
                {
                    bool flag = m.StoreId == storeId && m.EntityId == model.Id;
                    var type = model.GetType();
                    if (type.IsSubclassOf(typeof(BaseEntity)))
                    {
                        flag = flag && m.KeyGroup == type.Name;
                    }
                    return flag;
                };

                tempList = list.Where(func);
            }
            return tempList;
        }

        public string GetGenericAttributeValue(BaseEntity model, string key, int storeId = 0)
        {
            if (model == null) return string.Empty;
            if (key.IsEmpty()) return string.Empty;

            var list = GetList(model, storeId);
            if (list != null && list.Count() > 0)
            {
                var obj = list.FirstOrDefault(m => m.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
                if (obj != null) return obj.Value;
            }
            return string.Empty;
        }
    }
}
