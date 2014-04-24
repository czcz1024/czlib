namespace CZLib.Auth
{
    /// <summary>
    /// 身份信息的加密解密
    /// </summary>
    public interface IAuthInfoEncryptor
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        string Encrypt(string src);
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        string Decrypt(string src);
    }
}