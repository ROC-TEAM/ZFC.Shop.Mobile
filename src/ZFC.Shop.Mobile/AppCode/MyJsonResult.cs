using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZFC.Shop.Utility;

namespace ZFC.Shop.Mobile
{
    public class MyJsonResult : JsonResult
    {
        public MyJsonResult()
        {
        }

        public MyJsonResult(bool encode)
        {
            this.JsEncode = encode;
        }

        public MyJsonResult(string fmt, bool encode)
        {
            this.JsEncode = encode;
            this.DateFormat = fmt;
        }
        /// <summary>
        /// 日期格式
        /// </summary>
        public string DateFormat { get; set; }
        /// <summary>
        /// 是否Javascript编码
        /// </summary>
        public bool JsEncode { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            string contentType = "application/json";
            if (!string.IsNullOrEmpty(ContentType))
            {
                contentType = ContentType;
            }
            response.ContentType = contentType;

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
#pragma warning disable 0618
                response.Write(JsonHelper.ToJson(Data, this.DateFormat, this.JsEncode));
#pragma warning restore 0618
            }
        }
    }
}