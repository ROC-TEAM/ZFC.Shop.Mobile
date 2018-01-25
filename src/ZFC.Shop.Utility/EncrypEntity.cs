using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZFC.Shop.Utility
{
    public sealed class EncrypEntity
    {
        //默认密钥向量
        private byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private string _key;

        /// <summary>
        /// 加密解密Key
        /// </summary>
        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        /// <summary>
        /// 待加密文本
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 字符编码
        /// </summary>
        public Encoding Encoding { get; set; }

        public EncrypEntity()
        {
            Encoding = Encoding.Unicode;
            _key = "ABCDEKEY";
        }

        public EncrypEntity(string value)
            : this()
        {
            this.Value = value;
        }

        public EncrypEntity(string key, string value)
            : this()
        {
            this.Key = key;
            this.Value = value;
        }
    }

    public enum CharType
    {
        /// <summary>
        /// 大写
        /// </summary>
        UpperCase = 1,
        /// <summary>
        /// 小写
        /// </summary>
        LowerCase = 2
    }

    public enum EncryptMD5Type
    {
        /// <summary>
        /// 32位
        /// </summary>
        MD5_32 = 1,
        /// <summary>
        /// 16位
        /// </summary>
        MD5_16 = 2
    }
}
