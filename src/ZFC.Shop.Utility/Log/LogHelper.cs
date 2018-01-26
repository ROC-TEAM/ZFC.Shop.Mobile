using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ZFC.Shop.Utility
{
    internal sealed class LogHelper : ILog
    {
        public static object locker = new object();

        public void Info(string title, string msg)
        {
            this.Log(LogType.Info, title, msg);
        }

        public void Error(string title, string msg)
        {
            this.Log(LogType.Error, title, msg);
        }
        /// <summary>
        /// 写日志 使用默认的日志模板
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="message">内容</param>
        public void Log(LogType type, string title, string msg)
        {
            this.Log(type.ToString(), title, msg);
        }


        public void Log(string fileName, string title, string msg)
        {
            LogEntity entity = new LogEntity();
            string content = entity.GetDefaultContentTemplate(title, msg);
            entity.Content = content;
            entity.FileName = fileName;
            this.WriteLog(entity);
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="entity">参数</param>
        private void WriteLog(LogEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Content)) return;
            lock (locker)
            {
                CheckDirectory(entity.Path);
                string filePath = string.Format(@"{0}\{1}", entity.Path, entity.GetFileName());
                try
                {
                    FileMode fm = ExistsFile(filePath) ? FileMode.Append : FileMode.Create;
                    FileStream fs = new FileStream(filePath, fm, FileAccess.Write, FileShare.ReadWrite, 8);
                    StreamWriter sw = new StreamWriter(fs, entity.Encoding);
                    sw.WriteLine(entity.Content);
                    sw.Close();
                }
                catch (Exception e)
                {
                    entity.Error = e.Message;
                }
            }
        }

        /// <summary>
        /// 检查是否存在目录
        /// </summary>
        /// <param name="path"></param>
        private void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        /// <summary>
        /// 检查是否存在文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private bool ExistsFile(string filePath)
        {
            return File.Exists(filePath);
        }
    }

    public sealed class LogEntity
    {
        private readonly string LogString = "Log";
        /// <summary>
        /// 日志所在路径 不包括日志文件名 默认 获取基目录，它由程序集冲突解决程序用来探测程序集 +\\Log
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 日志文件名 默认 Log
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 日志文件后缀 不包括 . 默认 txt
        /// </summary>
        public string FileExt { get; set; }
        /// <summary>
        /// 写日志 字符编码 默认 Encoding.UTF8
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 使用默认的 日志文件名 格式 默认 是
        /// </summary>
        public bool UseDefaultFileNameForamt { get; set; }

        public string Error { get; set; }

        public LogEntity(bool useFormat)
        {
            DateTime dt = DateTime.Now;
            Encoding = Encoding.UTF8;
            FileExt = "txt";
            Path = string.Format(@"{0}\{1}\{2}", AppDomain.CurrentDomain.BaseDirectory, LogString, dt.ToString("yyyy-MM-dd"));
            //FileName = LogString;
            UseDefaultFileNameForamt = useFormat;
        }

        public LogEntity()
            : this(true)
        {

        }

        /// <summary>
        /// 获得文件名 带 后缀
        /// </summary>
        /// <returns></returns>
        public string GetFileName()
        {
            string fileName = string.Empty;
            if (this.UseDefaultFileNameForamt)
            {
                DateTime dt = DateTime.Now;
                fileName = string.Format("{0}_{1}", this.FileName, dt.ToString("HH"));
            }
            else fileName = this.FileName;
            fileName = string.Format("{0}.{1}", fileName, this.FileExt);
            return fileName;
        }

        /// <summary>
        /// 获得默认日志内容 模板
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public string GetDefaultContentTemplate(string title, string content)
        {
            StringBuilder sb = new StringBuilder(200);
            sb.AppendFormat("标题: {0}", title);
            sb.AppendLine();
            sb.AppendFormat("时间: {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine();
            sb.AppendFormat("内容: {0}", content);
            sb.AppendLine();
            return sb.ToString();
        }
    }
}
