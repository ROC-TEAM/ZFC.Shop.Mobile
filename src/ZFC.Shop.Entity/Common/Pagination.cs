using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    public class PaginationIn
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public string sidx { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string sord { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int records { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int total
        {
            get
            {
                if (rows <= 0) return 0;
                if (records > 0) return (records + rows - 1) / rows;
                else return 0;
            }
        }
        /// <summary>
        /// 查询条件Json
        /// </summary>
        public string queryJson { get; set; }

        public string costtime { get; set; }

        private System.Diagnostics.Stopwatch watch;

        public PaginationIn()
        {
            watch = new System.Diagnostics.Stopwatch();
        }

        public void WatchStart()
        {
            watch.Start();
        }

        public void WatchEnd()
        {
            watch.Stop();

            this.costtime = watch.Elapsed.TotalMilliseconds.ToString("f2");
        }
    }

    public class PaginationOut
    {
        public PaginationOut() { }

        public PaginationOut(PaginationIn p, object data)
        {
            this.rows = data;
            this.total = p.total;
            this.page = p.page;
            this.records = p.records;
            this.costtime = p.costtime;
        }
        /// <summary>
        /// 每页行数
        /// </summary>
        public object rows { get; set; }

        public int total { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int records { get; set; }

        /// <summary>
        /// 花费时间
        /// </summary>
        public string costtime { get; set; }
    }
}
