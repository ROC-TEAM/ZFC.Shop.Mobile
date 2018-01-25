using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    public class ApiResult
    {
        /// <summary>
        /// 操作结果
        /// </summary>
        public bool Flag { get; set; }
        /// <summary>
        /// 附加数据 （之前可能 通过data 获取Msg ）
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 操作结果描述 新增
        /// </summary>
        public string Msg { get; set; }

        public ApiResult() { }

        public ApiResult(bool flag)
        {
            this.Flag = flag;
        }

        public ApiResult(bool flag, string msg)
        {
            this.Flag = flag;
            this.Msg = msg;
        }

        public ApiResult(bool flag, string msg, object data)
        {
            this.Flag = flag;
            this.Msg = msg;
            this.Data = data;
        }
    }
}
