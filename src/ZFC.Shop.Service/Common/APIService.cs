using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web.Configuration;
using ZFC.Shop.Entity;
using ZFC.Shop.Data;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Service
{
    public interface IAPIService : IDependency
    {
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="wwid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        ApiResult ValidPwd(string wwid, string pwd);
    }

    public class APIService : IAPIService
    {
        public ApiResult ValidPwd(string wwid, string pwd)
        {
            Dictionary<string, object> ps = new Dictionary<string, object>();
            ps.Add("action", "ValidPassword");
            ps.Add("ad", wwid);
            ps.Add("password", pwd);
            WebHelper web = new WebHelper(GetURL(), ps);
            string result = web.GetContent();
            return GetResult(result);
        }

        private ApiResult GetResult(string s)
        {
            if (string.IsNullOrEmpty(s)) return new ApiResult(false, "从接口返回数据为空");
            return JsonHelper.ToObject<ApiResult>(s);
        }

        private string GetURL()
        {
            string domain = WebConfigurationManager.AppSettings["Domain"];
            string eprocessApiURL = WebConfigurationManager.AppSettings["EprocessApiURL"];

            return string.Format("{0}{1}", domain, eprocessApiURL);
        }
    }
}
