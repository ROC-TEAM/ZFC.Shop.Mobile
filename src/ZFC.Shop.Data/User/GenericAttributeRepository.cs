using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZFC.Shop.Entity;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Data
{
    public interface IGenericAttributeRepository : IRepository<GenericAttribute>
    {

    }

    public class GenericAttributeRepository : RepositoryBase<GenericAttribute>, IGenericAttributeRepository
    {

    }
}
