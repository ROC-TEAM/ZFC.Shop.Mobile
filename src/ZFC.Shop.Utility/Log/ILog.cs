using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Utility
{
    public interface ILog
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="title">标题</param>
        /// <param name="msg">内容</param>
        void Log(LogType type, string title, string msg);

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="title">标题</param>
        /// <param name="msg">内容</param>
        void Log(string fileName, string title, string msg);

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">内容</param>
        void Info(string title, string msg);

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">内容</param>
        void Error(string title, string msg);
    }

    public enum LogType
    {
        Info = 1,
        Warn = 2,
        Debug = 3,
        Error = 4
    }
}
