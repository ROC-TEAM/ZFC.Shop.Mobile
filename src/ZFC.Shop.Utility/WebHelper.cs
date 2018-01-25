using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace ZFC.Shop.Utility
{
    public class WebHelper
    {
        public string Url { get; set; }
        public Encoding Encoding { get; set; }
        public string Error { get; set; }
        public Dictionary<string, object> Parameters { get; set; }

        public WebHelper()
        { }

        public WebHelper(string url)
        {
            Url = url;
            Encoding = Encoding.UTF8;
        }

        public WebHelper(string url, Dictionary<string,object> parameters)
        {
            Url = url;
            Parameters = parameters;
            Encoding = Encoding.UTF8;
        }

        public WebHelper(string url, Encoding encoding)
        {
            Url = url;
            Encoding = encoding;
        }

        public string GetUrl()
        {
            return string.Format("{0}?{1}", Url, GetParametersString(Parameters));
        }

        public string GetParametersString(Dictionary<string, object> dic)
        {
            if (dic == null || dic.Count < 1)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (var item in dic.Keys)
            {
                sb.Append(item);
                sb.Append("=");
                sb.Append(dic[item]);
                sb.Append("&");
            }
            if (sb.Length > 0)
                sb = sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        /// <summary>
        /// 模拟 goole 爬虫 爬行网页
        /// </summary>
        /// <returns></returns>
        public string GetContent()
        {
            string url = GetUrl();
            Uri uri = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            request.AllowAutoRedirect = true;
            request.UserAgent = "Googlebot/2.1 (+http://www.google.com/bot.html)";
            request.Referer = string.Concat("http://", uri.Host);
            string content = string.Empty;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(streamReceive, Encoding);
                content = streamReader.ReadToEnd();
                streamReader.Close();
                streamReceive.Close();
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return content;
        }

        public Stream GetStream()
        {
            Uri uri = new Uri(Url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            request.AllowAutoRedirect = true;
            request.UseDefaultCredentials = true;
            request.UserAgent = "Googlebot/2.1 (+http://www.google.com/bot.html)";
            request.Referer = string.Concat("http://", uri.Host);
            Stream streamReceive = null;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                streamReceive = response.GetResponseStream();
                response.Close();
            }
            catch (Exception e)
            {
                Error = e.Message;
            }

            return streamReceive;
        }

        public string GetHttpResult(string url, bool isPost = false, string postData = "", Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            try
            {
                WebClient wc = new WebClient();
                byte[] bytes = Encoding.UTF8.GetBytes(postData);
                if (isPost) wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可
                string method = isPost ? "POST" : "GET";
                byte[] responseData = wc.UploadData(url, method, bytes);
                return encoding.GetString(responseData);
            }
            catch
            {
                return null;
            }
        }
    }
}
