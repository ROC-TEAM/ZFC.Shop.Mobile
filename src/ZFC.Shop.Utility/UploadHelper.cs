using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace ZFC.Shop.Utility
{
    public class UploadHelper
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static UploadResult UploadFile(UploadParameter p)
        {
            var file = p.File;
            if (file == null || file.ContentLength < 1) return new UploadResult(UploadStatus.NoFile, "没有选择上传文件");
            // 检查文件大小
            int size = file.ContentLength;
            double maxLen = p.MaxLength * 1024d * 1024d;
            if (size > maxLen)
            {
                string msg = string.Format("文件大小超出限制,最大允许上传[{0}]MB", p.MaxLength);
                return new UploadResult(UploadStatus.OutOfSize, msg);
            }
            //取得上传文件之扩展文件名，并转换成小写字母
            string fileExtension = Path.GetExtension(file.FileName);
            bool fileAllow = p.AllowExtension(fileExtension);

            if (!fileAllow)
            {
                string msg = string.Format("文件类型受限制,只允许上传[{0}]格式", string.Join(",", p.Extensions));
                return new UploadResult(UploadStatus.OutOfFormat, msg);
            }
            try
            {
                string savePath = p.SavePath;
                DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(savePath));
                if (!di.Exists)
                {
                    di.Create();
                }
                file.SaveAs(savePath);
                return new UploadResult(UploadStatus.Success, "上传成功");
            }
            catch (Exception e)
            {
                return new UploadResult(UploadStatus.Error, "上传出现错误--" + e.Message);
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="p">其他参数</param>
        /// <returns></returns>
        public static UploadResult UploadFile(UploadType type, UploadParameter p)
        {
            switch (type)
            {
                case UploadType.IMAGE:
                    p.Extensions = new string[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
                    p.MaxLength = 20;
                    break;
                case UploadType.RAR:
                    p.Extensions = new string[] { ".rar", ".zip" };
                    break;
                case UploadType.EXCEL:
                    p.Extensions = new string[] { ".xls", ".xlsx" };
                    break;
                case UploadType.FILE:
                    break;
            }

            return UploadFile(p);
        }
    }

    public class UploadParameter
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        public HttpPostedFileBase File { get; set; }
        /// <summary>
        /// 允许上传文件后缀 默认所有文件都允许 包括 .
        /// </summary>
        public string[] Extensions { get; set; }
        /// <summary>
        /// 上传大小限制 单位 MB 默认500MB
        /// </summary>
        public int MaxLength { get; set; }
        /// <summary>
        /// 保存路径 包括文件名的 绝对路径
        /// </summary>
        public string SavePath { get; set; }

        public UploadParameter()
        {
            this.MaxLength = 500;
            this.Extensions = null;
        }

        public UploadParameter(HttpPostedFileBase file)
            : this()
        {
            this.File = file;
        }

        public bool AllowExtension(string ext)
        {
            var exts = this.Extensions;
            if (exts == null || ext.Length == 1) return true;
            ext = ext.ToLower();
            return exts.Contains(ext);
        }

        public string GetDefaultFileName()
        {
            string name = Path.GetFileNameWithoutExtension(this.File.FileName);
            string ext = Path.GetExtension(this.File.FileName);
            Random rand = new Random();
            int num = rand.Next(1000, 10000);
            string dt = DateTime.Now.ToString("yyMMddHHmmssfff");
            return string.Format("{0}_{1}{2}{3}", name, dt, num, ext);
        }

        /// <summary>
        /// 获得文件大小 
        /// </summary>
        /// <param name="type">1=KB 2=MB 3=GB 其他=B</param>
        /// <returns></returns>
        public int GetFileSize(int type)
        {
            double len = 0;
            if (this.File != null) len = this.File.ContentLength;
            switch (type)
            {
                case 1://Kb
                    len = len / 1024d;
                    break;
                case 2://Mb
                    len = len / 1024d / 1024d;
                    break;
                case 3://Gb
                    len = len / 1024d / 1024d / 1024d;
                    break;
            }
            return (int)Math.Round(len, 0, MidpointRounding.AwayFromZero);
        }
    }

    public class UploadResult
    {
        public UploadStatus Status { get; set; }

        public int Result
        {
            get { return (int)Status; }
        }

        public string Msg { get; set; }

        public UploadResult()
        {

        }

        public UploadResult(UploadStatus s)
        {
            this.Status = s;
        }

        public UploadResult(UploadStatus s, string msg)
        {
            this.Status = s;
            this.Msg = msg;
        }
    }

    public enum UploadStatus
    {
        NoFile = -1,
        Success = 1,
        OutOfSize = 2,
        OutOfFormat = 3,
        Error = 4
    }

    public enum UploadType
    {
        NONE = 0,
        /// <summary>
        /// 图片
        /// </summary>
        IMAGE = 1,
        /// <summary>
        /// 压缩包 包括 rar, zip
        /// </summary>
        RAR = 2,
        /// <summary>
        /// Excel 
        /// </summary>
        EXCEL = 3,
        /// <summary>
        /// 其他文件
        /// </summary>
        FILE = 100
    }
}
