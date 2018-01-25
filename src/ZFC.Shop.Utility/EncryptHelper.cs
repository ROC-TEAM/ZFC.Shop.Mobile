using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ZFC.Shop.Utility
{
    public sealed class EncryptHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string Encrypt(EncrypEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            string encryptKey = entity.Key;
            Encoding encoding = entity.Encoding;
            byte[] bs = encoding.GetBytes(entity.Value); // 原字符串转换成字节数组
            byte[] keys = encoding.GetBytes(encryptKey); // 密钥转换成字节数组

            // 异或
            for (int i = 0; i < bs.Length; i++)
            {
                bs[i] = (byte)(bs[i] ^ keys[i % keys.Length]);
            }
            // 编码成16进制数组
            foreach (byte b in bs)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="key">加密KEY</param>
        /// <param name="value">加密文本</param>
        /// <returns></returns>
        public static string Encrypt(string key, string value)
        {
            EncrypEntity entity = new EncrypEntity(key, value);
            return Encrypt(entity);
        }

        public static string Encrypt(string value)
        {
            EncrypEntity entity = new EncrypEntity(value);
            return Encrypt(entity);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string Decrypt(EncrypEntity entity)
        {
            string txt = entity.Value;
            int len = txt.Length;
            byte[] bs = new byte[len / 2];

            // 16进制数组转换会byte数组
            for (int i = 0; i < len / 2; i++)
            {
                bs[i] = (byte)(Convert.ToInt32(txt.Substring(i * 2, 2), 16));
            }
            byte[] keys = entity.Encoding.GetBytes(entity.Key); // 密钥转换成字节数组
            // 异或
            for (int i = 0; i < bs.Length; i++)
            {
                bs[i] = (byte)(bs[i] ^ keys[i % keys.Length]);
            }
            // byte数组还原成字符串
            return entity.Encoding.GetString(bs);
        }

        public static string Decrypt(string key, string value)
        {
            EncrypEntity entity = new EncrypEntity();
            if (!string.IsNullOrEmpty(key)) entity.Key = key;
            entity.Value = value;
            return Decrypt(entity);
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="txt">待加密字符串</param>
        /// <param name="ct">加密后字符类型</param>
        /// <param name="type">加密类型[16/32]</param>
        /// <returns></returns>
        public static string Md5Encrypt(string txt, CharType ct = CharType.UpperCase, EncryptMD5Type type = EncryptMD5Type.MD5_32)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(txt);//将字符编码为一个字节序列 
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            md5.Clear();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5data.Length; i++)
            {
                sb.Append(md5data[i].ToString("X2"));//X 为 16进制
            }
            string result = sb.ToString();
            if (ct == CharType.UpperCase)
                result = result.ToUpper();
            else
                result = result.ToLower();
            if (type == EncryptMD5Type.MD5_16)
                result = result.Substring(8, 16);
            return result;
        }
    }
}
