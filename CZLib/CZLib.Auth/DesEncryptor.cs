﻿namespace CZLib.Auth
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// Des加密方式
    /// </summary>
    public class DesEncryptor : IAuthInfoEncryptor
    {
        public string Encrypt(string src)
        {
            var key = this.GetEncryptKey();
            var iv = this.GetEncryptVi();
            var enc = DES.Create();
            enc.Key = Convert.FromBase64String(key);
            enc.IV = Convert.FromBase64String(iv);

            var str = src;
            var ms = new MemoryStream();
            var encStream = new CryptoStream(ms, enc.CreateEncryptor(), CryptoStreamMode.Write);
            var sw = new StreamWriter(encStream);
            sw.WriteLine(str);
            sw.Close();
            encStream.Close();
            var encstr = Convert.ToBase64String(ms.ToArray());
            return encstr;
        }

        public string Decrypt(string src)
        {
            var key = this.GetEncryptKey();
            var iv = this.GetEncryptVi();
            var enc = DES.Create();
            enc.Key = Convert.FromBase64String(key);
            enc.IV = Convert.FromBase64String(iv);

            var ms = new MemoryStream(Convert.FromBase64String(src));
            var encStream = new CryptoStream(ms, enc.CreateDecryptor(), CryptoStreamMode.Read);
            var sr = new StreamReader(encStream);
            var val = sr.ReadLine();
            sr.Close();
            encStream.Close();
            ms.Close();
            return val;
        }

        /// <summary>
        /// dec加密方式中用到的key
        /// </summary>
        /// <returns></returns>
        protected virtual string GetEncryptKey()
        {
            return "LJZEe4Q/YZQ=";
        }

        /// <summary>
        /// dec加密方式中用到的vi
        /// </summary>
        /// <returns></returns>
        protected virtual string GetEncryptVi()
        {
            return "ZXwYJkS4U3M=";
        }
    }
}