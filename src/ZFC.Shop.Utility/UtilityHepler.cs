using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace ZFC.Shop.Utility
{
    public class UtilityHepler
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="flag">是否是相对路径</param>
        /// <param name="path">路径</param>
        /// <param name="fileName">文件下载名称</param>
        public static void DownLoadFile(bool flag, string path, string fileName = "")
        {
            string filePath = flag ? HttpContext.Current.Server.MapPath(path) : path;//路径
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Path.GetFileName(path);
            }
            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            string userIp = string.Empty;
            if (HttpContext.Current == null) return userIp;
            var request = HttpContext.Current.Request;
            if (request == null) return userIp;
            string http_via = request.ServerVariables["HTTP_VIA"];
            if (string.IsNullOrEmpty(http_via))
            {
                userIp = request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                string http_x_forwarded_for = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(http_x_forwarded_for))
                    userIp = request.UserHostAddress;
                else
                    userIp = http_x_forwarded_for;
            }
            return userIp;
        }
    }
}
