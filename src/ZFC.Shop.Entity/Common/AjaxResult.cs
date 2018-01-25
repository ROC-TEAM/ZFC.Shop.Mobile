using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Entity
{
    public class AjaxResult
    {
        public ResultType Type { get; set; }

        /// <summary>
        /// 获取 Ajax操作结果编码
        /// </summary>
        public ErrorType ErrorType { get; set; }

        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 获取 返回数据
        /// </summary>
        public object Data { get; set; }

        public AjaxResult()
        {
            this.Type = ResultType.Info;
            this.ErrorType = Entity.ErrorType.None;
        }

        public AjaxResult(bool flag, string msg = "", object data = null)
        {
            this.Type = flag ? ResultType.Success : ResultType.Error;
            this.Msg = msg;
            this.Data = data;
        }

        public AjaxResult(ResultType type, string msg = "", object data = null)
        {
            this.Type = type;
            this.Msg = msg;
            this.Data = data;
        }
    }

    /// <summary>
    /// 表示 ajax 操作结果类型的枚举
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// 消息结果类型
        /// </summary>
        Info = 0,

        /// <summary>
        /// 成功结果类型
        /// </summary>
        Success = 1,

        /// <summary>
        /// 警告结果类型
        /// </summary>
        Warning = 2,

        /// <summary>
        /// 异常结果类型
        /// </summary>
        Error = 3
    }

    public enum ErrorType
    {
        /// <summary>
        /// 未知
        /// </summary>
        None = -1,
        /// <summary>
        /// 存在错误
        /// </summary>
        Exist = 100
    }
}
