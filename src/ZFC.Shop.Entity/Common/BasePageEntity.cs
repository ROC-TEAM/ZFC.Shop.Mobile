using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    public class BasePageEntity
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }

        public BasePageEntity() : this(1, 10)
        {

        }

        public BasePageEntity(int pi, int ps)
        {
            this.PageIndex = pi;
            this.PageSize = ps;
        }

        public virtual int GetTotalPageCount()
        {
            return (Total + PageSize - 1) / PageSize;
        }
    }
}
